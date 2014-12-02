using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EApp.Common.Util;
using EApp.Core;
using EApp.Core.Application;
using EApp.Core.Plugin;
using EApp.Plugin.Generic;
using EApp.UI.Controls.UIHandler;

namespace EApp.Plugin.Generic.RibbonStyle
{
    public class RibbonModulePluginHost : PluginHost<CoreNavigationForm, RibbonModulePluginItem>
    {
        protected IPluginController<RibbonModulePluginItem> tickedControllerToBeExecuted;

        public RibbonModulePluginHost(IPluginProvider pluginProvider, CoreNavigationForm serviceProvider) 
            : base(pluginProvider, serviceProvider) 
        { 
        
        }

        public override void Exit()
        {
            this.ServiceProvider.Close();
        }

        protected override void RunCore()
        {
            Application.Run(this.ServiceProvider);
        }

        protected override void OnPluginAdded(object sender, EventArgs<RibbonModulePluginItem> e)
        {
            this.ServiceProvider.RegisterRibbonButtonClickCommand(e.Data.Name, this.OnRibbonButtonClickCommand);
        }

        protected virtual void OnRibbonButtonClickCommand(object sender, EventArgs e) 
        {
            RibbonButton ribbonButton = sender as RibbonButton;

            if (ribbonButton == null)
            {
                return;
            }

            string modulePluginItemName = ribbonButton.Name;

            this.tickedControllerToBeExecuted = this.PluginManager.PluginControllers[modulePluginItemName];

            this.tickedControllerToBeExecuted.Loading -= new CancelEventHandler(this.OnTickedControllerToBeExecutedLoading);
            
            this.tickedControllerToBeExecuted.Loading += new CancelEventHandler(this.OnTickedControllerToBeExecutedLoading);

            if (!this.tickedControllerToBeExecuted.Running)
            {
                this.tickedControllerToBeExecuted.Load();
            }

            this.SetRibbonButtonStyleOnModulePluginLoaded(ribbonButton);
        }

        protected virtual void OnTickedControllerToBeExecutedLoading(object sender, CancelEventArgs e) 
        {
            IPluginController<RibbonModulePluginItem> currentRunningModulePluginController =
                this.PluginManager.PluginControllers.RunningPluginController;

            // If the current running plugin is UI screen based on user control or popup overlay form
            if (currentRunningModulePluginController != null &&
                currentRunningModulePluginController.PluginInstance != null &&
                currentRunningModulePluginController.PluginInstance is IView)
            {
                IView currentRunningModulePluginView = (IView)currentRunningModulePluginController.PluginInstance;

                IPluginUnloadingContextAction pluginUnloadingContextAction = null;

                if (currentRunningModulePluginView is IPluginUnloadingContextAction)
                {
                    pluginUnloadingContextAction = currentRunningModulePluginView as IPluginUnloadingContextAction;

                    if (pluginUnloadingContextAction != null)
                    {
                        pluginUnloadingContextAction.ExecuteActionBeforeUnloading(e);

                        if (e.Cancel)
                        {
                            return;
                        }
                    }
                }

                if (currentRunningModulePluginView is IEditableView)
                {
                    IEditableView currentRunningModulePluginEditableView = (IEditableView)currentRunningModulePluginView;

                    if (currentRunningModulePluginEditableView.ViewChanged)
                    {
                        DialogResult confirmationResult = MessageBox.Show(
                            @"You have not saved your changes on this screen yet.  
                             If you go to another screeb before saving your changes they will be discarded. 
                             Do you wish to save them now?", Application.ProductName, 
                             MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                        if (confirmationResult == DialogResult.Yes)
                        { 
                            //Execute save
                        }
                        else if (confirmationResult == DialogResult.No)
                        { 
                            // to do...
                        }
                        else if (confirmationResult == DialogResult.Cancel)
                        {
                            e.Cancel = true;

                            if (pluginUnloadingContextAction != null)
                            {
                                pluginUnloadingContextAction.ExecuteActionAfterUnloadCancelled();
                            }
                        }
                    }
                }
            }

            if (!e.Cancel)
            {
                // Havn't executed debug yet.

                //IPluginLoadingContextAction<XpressModulePluginItem> viewLoadingContextAction =
                //    EAppRuntime.Instance.App.ObjectContainer.Resolve<IPluginLoadingContextAction<XpressModulePluginItem>>
                //    (this.tickedControllerToBeExecuted.PluginItem.Name);
    
                //if (viewLoadingContextAction != null)
                //{
                //    viewLoadingContextAction.ExecuteActionOnLoading(currentRunningModulePluginController, this.tickedControllerToBeExecuted, e);

                //    if (e.Cancel)
                //    {
                //        return;
                //    }
                //}

                if (currentRunningModulePluginController != null)
                {
                    DetachSubPluginItems(currentRunningModulePluginController.PluginItem);
                }

                this.AttachSubPluginItems(this.tickedControllerToBeExecuted.PluginItem);
            }

        }

        protected virtual void AttachSubPluginItems(RibbonModulePluginItem tickedModulePluginItemToRun)
        {
            if (tickedModulePluginItemToRun.SubItems == null ||
                tickedModulePluginItemToRun.SubItems.Count.Equals(0))
            {
                return;
            }

            foreach (RibbonModulePluginItem subPluginItem in tickedModulePluginItemToRun.SubItems)
            {
                RibbonExtensionHelper.AddRibbonButton(this.ServiceProvider.RibbonMenu, subPluginItem, this.OnRibbonButtonClickCommand);
            }
        }

        protected virtual void DetachSubPluginItems(RibbonModulePluginItem runningModulePluginItem)
        {
            if (runningModulePluginItem.SubItems == null ||
                runningModulePluginItem.SubItems.Count.Equals(0))
            {
                return;
            }

            foreach (RibbonModulePluginItem subPluginItem in runningModulePluginItem.SubItems)
            {
                RibbonExtensionHelper.RemoveRibbonButton(this.ServiceProvider.RibbonMenu, subPluginItem);
            }
        }

        protected virtual void SetRibbonButtonStyleOnModulePluginLoaded(RibbonButton ribbonButton)
        {
            if (!this.tickedControllerToBeExecuted.Running)
            {
                RibbonButtonClickStatus clickStatus = this.tickedControllerToBeExecuted.PluginItem.ClickStatus;

                if (clickStatus == RibbonButtonClickStatus.NoCheckedOnClick)
                {
                    ribbonButton.Checked = false;
                }
                else if (clickStatus == RibbonButtonClickStatus.ToggledCheckedOnClick)
                {
                    ribbonButton.Checked = !ribbonButton.Checked;
                }
                else
                {
                    ribbonButton.Checked = true;
                }
            }
            else
            {
                // if the parent (i.e. owner) of the ribbon button is not Quick Access bar
                if (ribbonButton.OwnerTab != null)
                {
                    RibbonExtensionHelper.SetSingleRibbonButtonToCheckedStatus(this.ServiceProvider.RibbonMenu, 
                                                                               ribbonButton.OwnerTab.Text,
                                                                               ribbonButton.Name);
                }
                else
                {
                    RibbonExtensionHelper.SetAllRibbonButtonsToUncheckedStatus(this.ServiceProvider.RibbonMenu);
                }
            }
        }
    }
}
