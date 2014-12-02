namespace Xpress.UI.Plugins.Cost
{
    partial class UCCostBase<TCostLineItem>
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.roundedCornerPanelContainer = new EApp.UI.Controls.Common.RoundedCornerPanel();
            this.roundedCornerPanelTop = new EApp.UI.Controls.Common.RoundedCornerPanel();
            this.dgvCostLine = new System.Windows.Forms.DataGridView();
            this.Selection = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewImageColumn();
            this.Group = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Subgroup = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.HPOr3rd = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.TotalCost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.roundedCornerPanelContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCostLine)).BeginInit();
            this.SuspendLayout();
            // 
            // roundedCornerPanelContainer
            // 
            this.roundedCornerPanelContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.roundedCornerPanelContainer.ArcRadius = 20;
            this.roundedCornerPanelContainer.BorderColor = System.Drawing.Color.RoyalBlue;
            this.roundedCornerPanelContainer.BorderWidth = 1;
            this.roundedCornerPanelContainer.Controls.Add(this.roundedCornerPanelTop);
            this.roundedCornerPanelContainer.Controls.Add(this.dgvCostLine);
            this.roundedCornerPanelContainer.EndColor = System.Drawing.Color.LightCyan;
            this.roundedCornerPanelContainer.GradientType = EApp.UI.Controls.Common.GradientType.Vertical;
            this.roundedCornerPanelContainer.Location = new System.Drawing.Point(14, 15);
            this.roundedCornerPanelContainer.Name = "roundedCornerPanelContainer";
            this.roundedCornerPanelContainer.OwnerDraw = true;
            this.roundedCornerPanelContainer.Size = new System.Drawing.Size(969, 487);
            this.roundedCornerPanelContainer.StartColor = System.Drawing.Color.LightSkyBlue;
            this.roundedCornerPanelContainer.TabIndex = 0;
            // 
            // roundedCornerPanelTop
            // 
            this.roundedCornerPanelTop.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.roundedCornerPanelTop.ArcRadius = 20;
            this.roundedCornerPanelTop.BackColor = System.Drawing.Color.Transparent;
            this.roundedCornerPanelTop.BorderColor = System.Drawing.Color.White;
            this.roundedCornerPanelTop.BorderWidth = 1;
            this.roundedCornerPanelTop.EndColor = System.Drawing.Color.LightCyan;
            this.roundedCornerPanelTop.GradientType = EApp.UI.Controls.Common.GradientType.Vertical;
            this.roundedCornerPanelTop.Location = new System.Drawing.Point(21, 12);
            this.roundedCornerPanelTop.Name = "roundedCornerPanelTop";
            this.roundedCornerPanelTop.OwnerDraw = true;
            this.roundedCornerPanelTop.Size = new System.Drawing.Size(922, 67);
            this.roundedCornerPanelTop.StartColor = System.Drawing.Color.Pink;
            this.roundedCornerPanelTop.TabIndex = 1;
            // 
            // dgvCostLine
            // 
            this.dgvCostLine.AllowUserToAddRows = false;
            this.dgvCostLine.AllowUserToDeleteRows = false;
            this.dgvCostLine.AllowUserToResizeRows = false;
            this.dgvCostLine.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvCostLine.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvCostLine.ColumnHeadersHeight = 22;
            this.dgvCostLine.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Selection,
            this.ID,
            this.Status,
            this.Group,
            this.Subgroup,
            this.HPOr3rd,
            this.TotalCost,
            this.TotalPrice});
            this.dgvCostLine.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvCostLine.Location = new System.Drawing.Point(21, 85);
            this.dgvCostLine.Name = "dgvCostLine";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvCostLine.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvCostLine.RowHeadersVisible = false;
            this.dgvCostLine.Size = new System.Drawing.Size(922, 380);
            this.dgvCostLine.TabIndex = 0;
            this.dgvCostLine.VirtualMode = true;
            this.dgvCostLine.CellValueNeeded += new System.Windows.Forms.DataGridViewCellValueEventHandler(this.dgvCostLine_CellValueNeeded);
            this.dgvCostLine.CellValuePushed +=new System.Windows.Forms.DataGridViewCellValueEventHandler(this.dgvCostLine_CellValuePushed);
            this.dgvCostLine.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(dgvCostLine_DataError);
            // 
            // Selection
            // 
            this.Selection.HeaderText = "Sel";
            this.Selection.Name = "Selection";
            this.Selection.Width = 30;
            // 
            // ID
            // 
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            // 
            // Status
            // 
            this.Status.HeaderText = "Stat";
            this.Status.Name = "Status";
            this.Status.Width = 50;
            // 
            // Group
            // 
            this.Group.HeaderText = "Group";
            this.Group.Name = "Group";
            this.Group.Width = 150;
            // 
            // Subgroup
            // 
            this.Subgroup.HeaderText = "Subgroup";
            this.Subgroup.Name = "Subgroup";
            this.Subgroup.Width = 150;
            // 
            // HPOr3rd
            // 
            this.HPOr3rd.HeaderText = "HP/3rd";
            this.HPOr3rd.Name = "HPOr3rd";
            // 
            // TotalCost
            // 
            this.TotalCost.HeaderText = "Total Cost";
            this.TotalCost.Name = "TotalCost";
            // 
            // TotalPrice
            // 
            this.TotalPrice.HeaderText = "Total Price";
            this.TotalPrice.Name = "TotalPrice";

            // 
            // UCCostBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.roundedCornerPanelContainer);
            this.Name = "UCCostBase";
            this.Size = new System.Drawing.Size(1006, 515);
            this.roundedCornerPanelContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCostLine)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private EApp.UI.Controls.Common.RoundedCornerPanel roundedCornerPanelContainer;
        protected System.Windows.Forms.DataGridView dgvCostLine;
        protected EApp.UI.Controls.Common.RoundedCornerPanel roundedCornerPanelTop;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Selection;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewImageColumn Status;
        private System.Windows.Forms.DataGridViewComboBoxColumn Group;
        private System.Windows.Forms.DataGridViewComboBoxColumn Subgroup;
        private System.Windows.Forms.DataGridViewComboBoxColumn HPOr3rd;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalCost;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalPrice;

    }
}
