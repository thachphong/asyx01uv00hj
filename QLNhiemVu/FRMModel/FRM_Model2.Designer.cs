namespace QLNhiemVu.FRMModel
{
    partial class FRM_Model2
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panelHeader1 = new QLNhiemVu.panelHeader();
            this.lblHeadTitle1 = new QLNhiemVu.lblHeadTitle();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.panelHeader2 = new QLNhiemVu.panelHeader();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.panel1.SuspendLayout();
            this.panelHeader1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panelHeader1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1036, 184);
            this.panel1.TabIndex = 0;
            // 
            // panelHeader1
            // 
            this.panelHeader1.Controls.Add(this.lblHeadTitle1);
            this.panelHeader1.Location = new System.Drawing.Point(84, 3);
            this.panelHeader1.Name = "panelHeader1";
            this.panelHeader1.Size = new System.Drawing.Size(872, 175);
            this.panelHeader1.TabIndex = 0;
            // 
            // lblHeadTitle1
            // 
            this.lblHeadTitle1.AutoSize = true;
            this.lblHeadTitle1.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold);
            this.lblHeadTitle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.lblHeadTitle1.Location = new System.Drawing.Point(342, 6);
            this.lblHeadTitle1.Name = "lblHeadTitle1";
            this.lblHeadTitle1.Size = new System.Drawing.Size(130, 24);
            this.lblHeadTitle1.TabIndex = 0;
            this.lblHeadTitle1.Text = "lblHeadTitle1";
            // 
            // groupControl1
            // 
            this.groupControl1.AppearanceCaption.Font = new System.Drawing.Font("Times New Roman", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupControl1.AppearanceCaption.ForeColor = System.Drawing.Color.Blue;
            this.groupControl1.AppearanceCaption.Options.UseFont = true;
            this.groupControl1.AppearanceCaption.Options.UseForeColor = true;
            this.groupControl1.Controls.Add(this.panelHeader2);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupControl1.Location = new System.Drawing.Point(0, 351);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(1036, 154);
            this.groupControl1.TabIndex = 2;
            this.groupControl1.Text = "Chi tiết phân công trong cơ quan";
            // 
            // panelHeader2
            // 
            this.panelHeader2.Location = new System.Drawing.Point(185, 49);
            this.panelHeader2.Name = "panelHeader2";
            this.panelHeader2.Size = new System.Drawing.Size(632, 100);
            this.panelHeader2.TabIndex = 0;
            // 
            // groupControl2
            // 
            this.groupControl2.AppearanceCaption.Font = new System.Drawing.Font("Times New Roman", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupControl2.AppearanceCaption.ForeColor = System.Drawing.Color.Blue;
            this.groupControl2.AppearanceCaption.Options.UseFont = true;
            this.groupControl2.AppearanceCaption.Options.UseForeColor = true;
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl2.Location = new System.Drawing.Point(0, 184);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(1036, 167);
            this.groupControl2.TabIndex = 3;
            this.groupControl2.Text = "Danh Sách";
            // 
            // FRM_Model2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1036, 505);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.panel1);
            this.Name = "FRM_Model2";
            this.Text = "FRM_Model2";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FRM_Model2_Load);
            this.panel1.ResumeLayout(false);
            this.panelHeader1.ResumeLayout(false);
            this.panelHeader1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Panel panel1;
        public panelHeader panelHeader1;
        public DevExpress.XtraEditors.GroupControl groupControl1;
        public lblHeadTitle lblHeadTitle1;
        public DevExpress.XtraEditors.GroupControl groupControl2;
        public panelHeader panelHeader2;
    }
}