namespace QLNhiemVu.DanhMuc
{
    partial class FRM_DM_Thutuc_Saochep
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
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FRM_DM_Thutuc_Saochep));
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.panelHeader1 = new QLNhiemVu.panelHeader();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.label7 = new System.Windows.Forms.Label();
            this.m_loai_thutuc_lk = new DevExpress.XtraEditors.LookUpEdit();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.gridControl2 = new DevExpress.XtraGrid.GridControl();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.d1_cachnhap_lk = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.btn_noidung_cauhinh = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.simpleButton3 = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.panelHeader1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_loai_thutuc_lk.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.d1_cachnhap_lk)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_noidung_cauhinh)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.panelHeader1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(609, 63);
            this.panelControl1.TabIndex = 0;
            // 
            // panelHeader1
            // 
            this.panelHeader1.Controls.Add(this.simpleButton3);
            this.panelHeader1.Controls.Add(this.simpleButton2);
            this.panelHeader1.Controls.Add(this.simpleButton1);
            this.panelHeader1.Controls.Add(this.label7);
            this.panelHeader1.Controls.Add(this.m_loai_thutuc_lk);
            this.panelHeader1.Location = new System.Drawing.Point(5, 5);
            this.panelHeader1.Name = "panelHeader1";
            this.panelHeader1.Size = new System.Drawing.Size(577, 53);
            this.panelHeader1.TabIndex = 0;
            // 
            // simpleButton2
            // 
            this.simpleButton2.Appearance.Font = new System.Drawing.Font("Times New Roman", 12.75F);
            this.simpleButton2.Appearance.Options.UseFont = true;
            this.simpleButton2.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton2.Image")));
            this.simpleButton2.Location = new System.Drawing.Point(427, 10);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(66, 29);
            this.simpleButton2.TabIndex = 71;
            this.simpleButton2.Text = "Chọn";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Appearance.Font = new System.Drawing.Font("Times New Roman", 12.75F);
            this.simpleButton1.Appearance.Options.UseFont = true;
            this.simpleButton1.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton1.Image")));
            this.simpleButton1.Location = new System.Drawing.Point(359, 10);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(62, 29);
            this.simpleButton1.TabIndex = 70;
            this.simpleButton1.Text = "Tìm";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Times New Roman", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(7, 16);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(115, 19);
            this.label7.TabIndex = 69;
            this.label7.Text = "Loại thủ thủ tục";
            // 
            // m_loai_thutuc_lk
            // 
            this.m_loai_thutuc_lk.Location = new System.Drawing.Point(128, 13);
            this.m_loai_thutuc_lk.Name = "m_loai_thutuc_lk";
            this.m_loai_thutuc_lk.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_loai_thutuc_lk.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.m_loai_thutuc_lk.Properties.Appearance.Options.UseFont = true;
            this.m_loai_thutuc_lk.Properties.Appearance.Options.UseForeColor = true;
            this.m_loai_thutuc_lk.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Times New Roman", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_loai_thutuc_lk.Properties.AppearanceDropDown.ForeColor = System.Drawing.Color.Black;
            this.m_loai_thutuc_lk.Properties.AppearanceDropDown.Options.UseFont = true;
            this.m_loai_thutuc_lk.Properties.AppearanceDropDown.Options.UseForeColor = true;
            this.m_loai_thutuc_lk.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.m_loai_thutuc_lk.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("DM016004", "DM016004")});
            this.m_loai_thutuc_lk.Properties.NullText = "";
            this.m_loai_thutuc_lk.Properties.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoComplete;
            this.m_loai_thutuc_lk.Properties.ShowHeader = false;
            this.m_loai_thutuc_lk.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.m_loai_thutuc_lk.Size = new System.Drawing.Size(225, 26);
            this.m_loai_thutuc_lk.TabIndex = 68;
            // 
            // groupControl2
            // 
            this.groupControl2.AppearanceCaption.Font = new System.Drawing.Font("Times New Roman", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupControl2.AppearanceCaption.ForeColor = System.Drawing.Color.Blue;
            this.groupControl2.AppearanceCaption.Options.UseFont = true;
            this.groupControl2.AppearanceCaption.Options.UseForeColor = true;
            this.groupControl2.Controls.Add(this.gridControl2);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl2.Location = new System.Drawing.Point(0, 63);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(609, 506);
            this.groupControl2.TabIndex = 5;
            this.groupControl2.Text = "Danh sách nội dung";
            // 
            // gridControl2
            // 
            this.gridControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl2.Location = new System.Drawing.Point(2, 27);
            this.gridControl2.MainView = this.gridView2;
            this.gridControl2.Name = "gridControl2";
            this.gridControl2.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.d1_cachnhap_lk,
            this.btn_noidung_cauhinh});
            this.gridControl2.Size = new System.Drawing.Size(605, 477);
            this.gridControl2.TabIndex = 2;
            this.gridControl2.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView2});
            // 
            // gridView2
            // 
            this.gridView2.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView2.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView2.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView2.Appearance.Row.Options.UseFont = true;
            this.gridView2.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5});
            this.gridView2.GridControl = this.gridControl2;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.True;
            this.gridView2.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.True;
            this.gridView2.OptionsDetail.ShowDetailTabs = false;
            this.gridView2.OptionsView.ShowDetailButtons = false;
            this.gridView2.OptionsView.ShowGroupPanel = false;
            this.gridView2.FocusedRowObjectChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventHandler(this.gridView2_FocusedRowObjectChanged);
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Mã nội dung";
            this.gridColumn3.FieldName = "DM016103";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 0;
            this.gridColumn3.Width = 287;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Tên nội dung";
            this.gridColumn4.FieldName = "DM016104";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 1;
            this.gridColumn4.Width = 287;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Cách nhập";
            this.gridColumn5.FieldName = "Cachnhap";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 2;
            this.gridColumn5.Width = 289;
            // 
            // d1_cachnhap_lk
            // 
            this.d1_cachnhap_lk.AppearanceDropDown.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.d1_cachnhap_lk.AppearanceDropDown.Options.UseFont = true;
            this.d1_cachnhap_lk.AutoHeight = false;
            this.d1_cachnhap_lk.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.d1_cachnhap_lk.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Description", "Description")});
            this.d1_cachnhap_lk.Name = "d1_cachnhap_lk";
            this.d1_cachnhap_lk.NullText = "";
            this.d1_cachnhap_lk.ShowHeader = false;
            // 
            // btn_noidung_cauhinh
            // 
            this.btn_noidung_cauhinh.AutoHeight = false;
            this.btn_noidung_cauhinh.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "...", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "", null, null, true)});
            this.btn_noidung_cauhinh.Name = "btn_noidung_cauhinh";
            this.btn_noidung_cauhinh.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            // 
            // simpleButton3
            // 
            this.simpleButton3.Appearance.Font = new System.Drawing.Font("Times New Roman", 12.75F);
            this.simpleButton3.Appearance.Options.UseFont = true;
            this.simpleButton3.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton3.Image")));
            this.simpleButton3.Location = new System.Drawing.Point(499, 10);
            this.simpleButton3.Name = "simpleButton3";
            this.simpleButton3.Size = new System.Drawing.Size(66, 29);
            this.simpleButton3.TabIndex = 72;
            this.simpleButton3.Text = "Hủy";
            this.simpleButton3.Click += new System.EventHandler(this.simpleButton3_Click);
            // 
            // FRM_DM_Thutuc_Saochep
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(609, 569);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.panelControl1);
            this.Name = "FRM_DM_Thutuc_Saochep";
            this.Text = "FRM_DM_Thutuc_Saochep";
            this.Load += new System.EventHandler(this.FRM_DM_Thutuc_Saochep_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelHeader1.ResumeLayout(false);
            this.panelHeader1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_loai_thutuc_lk.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.d1_cachnhap_lk)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_noidung_cauhinh)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraGrid.GridControl gridControl2;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit d1_cachnhap_lk;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit btn_noidung_cauhinh;
        private panelHeader panelHeader1;
        private System.Windows.Forms.Label label7;
        private DevExpress.XtraEditors.LookUpEdit m_loai_thutuc_lk;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.SimpleButton simpleButton3;

    }
}