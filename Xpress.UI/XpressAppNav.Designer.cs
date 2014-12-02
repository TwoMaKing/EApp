using System;
namespace Xpress.UI
{
    partial class XpressAppNav
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.ribbonImageList = new System.Windows.Forms.ImageList(this.components);
            this.mdiUIContainer = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // ribbonImageList
            // 
            this.ribbonImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
            this.ribbonImageList.ImageSize = new System.Drawing.Size(32, 32);
            this.ribbonImageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // mdiUIContainer
            // 
            this.mdiUIContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mdiUIContainer.Location = new System.Drawing.Point(0, 142);
            this.mdiUIContainer.Name = "mdiUIContainer";
            this.mdiUIContainer.Size = new System.Drawing.Size(1196, 514);
            this.mdiUIContainer.TabIndex = 0;
            // 
            // XpressAppNav
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1240, 656);
            this.Controls.Add(this.mdiUIContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "XpressAppNav";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Xpress 6.0.0.0";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Ribbon ribbonMenu;
        private System.Windows.Forms.ImageList ribbonImageList;
        private System.Windows.Forms.Panel mdiUIContainer;
    }
}

