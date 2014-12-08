using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.Common.AsynComponent
{
    public interface IAsyncTask
    {
        event ProgressChangedEventHandler ProgressChanged;

        event AsyncCompletedEventHandler Completed;

        bool CancellationPending { get; }

        void RunAsync(params object[] arguments);

        void CancelAsync();

        void ReportProgress(int progressPercentage, object userState);
    }
}
