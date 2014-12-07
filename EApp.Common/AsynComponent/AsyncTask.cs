using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EApp.Common.AsynComponent
{
    public abstract class AsyncTask : IAsyncTask
    {
        private Guid taskId;

        private AsyncOperation asyncOperation;

        private delegate void WorkerEventHandler(AsyncOperation asyncOp, params object[] arguments);

        private HybridDictionary userStateToLifetime = new HybridDictionary();

        private SendOrPostCallback onProgressChangedDelegate;

        private SendOrPostCallback onCompletedDelegate;

        public event ProgressChangedEventHandler ProgressChanged;

        public event AsyncCompletedEventHandler Completed;

        protected AsyncTask()
        {
            this.InitializeSendOrPostCallback();
        }

        public bool CancellationPending
        {
            get 
            {
                return !this.userStateToLifetime.Contains(taskId);
            }
        }

        public void RunAsync(params object[] arguments)
        {
            // Create an AsyncOperation for taskId.
            this.taskId = Guid.NewGuid();

            this.asyncOperation = AsyncOperationManager.CreateOperation(taskId);

            // Multiple threads will access the task dictionary,
            // so it must be locked to serialize access.
            lock (this.userStateToLifetime.SyncRoot)
            {
                if (this.userStateToLifetime.Contains(taskId))
                {
                    throw new ArgumentException("Task ID parameter must be unique", "taskId");
                }

                this.userStateToLifetime[taskId] = this.asyncOperation;
            }

            // Start the asynchronous operation.
            WorkerEventHandler workerDelegate = new WorkerEventHandler(this.DoWork);

            workerDelegate.BeginInvoke(asyncOperation, arguments, null, null);
        }

        public void CancelAsync()
        {
            AsyncOperation asyncOp = this.userStateToLifetime[taskId] as AsyncOperation;

            if (asyncOp != null)
            {
                lock (this.userStateToLifetime.SyncRoot)
                {
                    this.userStateToLifetime.Remove(taskId);
                }
            }
        }

        public void ReportProgress(int progressPercentage, object userState)
        {
            this.ReportProgress(progressPercentage, userState, this.asyncOperation);
        }

        protected virtual void DoWork(AsyncOperation asyncOp, params object[] arguments) 
        { 
            Exception error = null;

            if (!this.CancellationPending)
            {
                try
                {
                    this.DoWork(arguments);
                }
                catch (Exception e)
                {
                    error = e;
                }
            }

            this.TriggerCompletionEvent(error, this.CancellationPending, this.asyncOperation);
        }

        #region Private members

        private void InitializeSendOrPostCallback() 
        {
            this.onProgressChangedDelegate = new SendOrPostCallback(this.OnTaskProgressChanged);
            this.onCompletedDelegate = new SendOrPostCallback(this.OnTaskCompleted);
        }

        private void OnTaskProgressChanged(object state) 
        {
            ProgressChangedEventArgs e = state as ProgressChangedEventArgs;

            this.OnTaskProgressChanged(e);
        }

        private void OnTaskCompleted(object operationState)
        {
            AsyncCompletedEventArgs e = operationState as AsyncCompletedEventArgs;

            this.OnTaskCompleted(e);
        }

        #endregion

        #region Protected members

        protected virtual void OnTaskProgressChanged(ProgressChangedEventArgs e)
        {
            if (this.ProgressChanged != null)
            {
                this.ProgressChanged(this, e);
            }
        }

        protected virtual void OnTaskProgressChanged(int progressPercentage, object userState)
        {
            this.OnTaskProgressChanged(new ProgressChangedEventArgs(progressPercentage, userState));
        }

        protected virtual void OnTaskCompleted(AsyncCompletedEventArgs e)
        {
            if (this.Completed != null)
            {
                this.Completed(this, e);
            }
        }

        protected virtual void OnTaskCompleted(Exception error, bool cancelled, object userState) 
        {
            this.OnTaskCompleted(new AsyncCompletedEventArgs(error, cancelled, userState));
        }

        protected virtual void ReportProgress(int progressPercentage, object userState, AsyncOperation asyncOperation)
        {
            Thread.Sleep(1000);

            ProgressChangedEventArgs e = new ProgressChangedEventArgs(progressPercentage, userState);

            asyncOperation.Post(this.onProgressChangedDelegate, e);
        }

        protected virtual void TriggerCompletionEvent(Exception exception, bool cancelled, AsyncOperation asyncOp)
        {
            // If the task was not previously canceled,
            // remove the task from the lifetime collection.
            if (!cancelled)
            {
                lock (userStateToLifetime.SyncRoot)
                {
                    userStateToLifetime.Remove(asyncOp.UserSuppliedState);
                }
            }

            AsyncCompletedEventArgs e =
                new AsyncCompletedEventArgs(
                exception,
                cancelled,
                asyncOp.UserSuppliedState);

            asyncOp.PostOperationCompleted(this.onCompletedDelegate, e);
        }

        protected abstract void DoWork(params object[] arguments);

        #endregion
    }
}
