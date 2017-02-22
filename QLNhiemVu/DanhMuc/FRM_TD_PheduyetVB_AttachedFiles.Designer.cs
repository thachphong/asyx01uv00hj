namespace QLNhiemVu.DanhMuc
{
    partial class FRM_TD_PheduyetVB_AttachedFiles
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FRM_TD_PheduyetVB_AttachedFiles));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panelHeader1 = new QLNhiemVu.panelHeader();
            this.btn_chontep = new DevExpress.XtraEditors.SimpleButton();
            this.btn_thoat = new DevExpress.XtraEditors.SimpleButton();
            this.btn_capnhat = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.checkEdit1 = new DevExpress.XtraEditors.CheckEdit();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemImageComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.repositoryItemCheckEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.panel1.SuspendLayout();
            this.panelHeader1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panelHeader1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 397);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(409, 50);
            this.panel1.TabIndex = 5;
            // 
            // panelHeader1
            // 
            this.panelHeader1.Controls.Add(this.btn_chontep);
            this.panelHeader1.Controls.Add(this.btn_thoat);
            this.panelHeader1.Controls.Add(this.btn_capnhat);
            this.panelHeader1.Location = new System.Drawing.Point(26, 3);
            this.panelHeader1.Name = "panelHeader1";
            this.panelHeader1.Size = new System.Drawing.Size(313, 36);
            this.panelHeader1.TabIndex = 0;
            // 
            // btn_chontep
            // 
            this.btn_chontep.Appearance.Font = new System.Drawing.Font("Times New Roman", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_chontep.Appearance.Options.UseFont = true;
            this.btn_chontep.Image = ((System.Drawing.Image)(resources.GetObject("btn_chontep.Image")));
            this.btn_chontep.ImageIndex = 4;
            this.btn_chontep.Location = new System.Drawing.Point(205, 3);
            this.btn_chontep.Name = "btn_chontep";
            this.btn_chontep.Size = new System.Drawing.Size(103, 29);
            this.btn_chontep.TabIndex = 92;
            this.btn_chontep.Text = "Chọn tệp";
            this.btn_chontep.Click += new System.EventHandler(this.btn_chontep_Click);
            // 
            // btn_thoat
            // 
            this.btn_thoat.Appearance.Font = new System.Drawing.Font("Times New Roman", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_thoat.Appearance.Options.UseFont = true;
            this.btn_thoat.Image = ((System.Drawing.Image)(resources.GetObject("btn_thoat.Image")));
            this.btn_thoat.ImageIndex = 4;
            this.btn_thoat.Location = new System.Drawing.Point(104, 3);
            this.btn_thoat.Name = "btn_thoat";
            this.btn_thoat.Size = new System.Drawing.Size(95, 29);
            this.btn_thoat.TabIndex = 91;
            this.btn_thoat.Text = "Hủy";
            this.btn_thoat.Click += new System.EventHandler(this.btn_thoat_Click);
            // 
            // btn_capnhat
            // 
            this.btn_capnhat.Appearance.Font = new System.Drawing.Font("Times New Roman", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_capnhat.Appearance.Options.UseFont = true;
            this.btn_capnhat.Image = ((System.Drawing.Image)(resources.GetObject("btn_capnhat.Image")));
            this.btn_capnhat.ImageIndex = 7;
            this.btn_capnhat.Location = new System.Drawing.Point(3, 3);
            this.btn_capnhat.Name = "btn_capnhat";
            this.btn_capnhat.Size = new System.Drawing.Size(95, 29);
            this.btn_capnhat.TabIndex = 89;
            this.btn_capnhat.Text = "Lưu";
            this.btn_capnhat.Click += new System.EventHandler(this.btn_capnhat_Click);
            // 
            // groupControl1
            // 
            this.groupControl1.AppearanceCaption.Font = new System.Drawing.Font("Times New Roman", 12.75F);
            this.groupControl1.AppearanceCaption.ForeColor = System.Drawing.Color.Blue;
            this.groupControl1.AppearanceCaption.Options.UseFont = true;
            this.groupControl1.AppearanceCaption.Options.UseForeColor = true;
            this.groupControl1.Controls.Add(this.checkEdit1);
            this.groupControl1.Controls.Add(this.gridControl1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(409, 397);
            this.groupControl1.TabIndex = 6;
            this.groupControl1.Text = "Danh sách";
            // 
            // checkEdit1
            // 
            this.checkEdit1.Location = new System.Drawing.Point(9, 29);
            this.checkEdit1.Name = "checkEdit1";
            this.checkEdit1.Properties.Caption = "";
            this.checkEdit1.Size = new System.Drawing.Size(16, 19);
            this.checkEdit1.TabIndex = 5;
            this.checkEdit1.CheckedChanged += new System.EventHandler(this.checkEdit1_CheckedChanged);
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(2, 27);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemImageComboBox1,
            this.repositoryItemCheckEdit1,
            this.repositoryItemCheckEdit2});
            this.gridControl1.Size = new System.Drawing.Size(405, 368);
            this.gridControl1.TabIndex = 4;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView1.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView1.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView1.Appearance.Row.Options.UseFont = true;
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn10,
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.AutoPopulateColumns = false;
            this.gridView1.OptionsCustomization.AllowGroup = false;
            this.gridView1.OptionsDetail.ShowDetailTabs = false;
            this.gridView1.OptionsMenu.EnableColumnMenu = false;
            this.gridView1.OptionsSelection.CheckBoxSelectorColumnWidth = 100;
            this.gridView1.OptionsView.ShowDetailButtons = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.OptionsView.ShowIndicator = false;
            // 
            // gridColumn10
            // 
            this.gridColumn10.ColumnEdit = this.repositoryItemCheckEdit1;
            this.gridColumn10.FieldName = "IsChecked";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.OptionsColumn.FixedWidth = true;
            this.gridColumn10.OptionsColumn.ShowCaption = false;
            this.gridColumn10.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn10.OptionsFilter.AllowFilter = false;
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 0;
            this.gridColumn10.Width = 30;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "ID";
            this.gridColumn1.FieldName = "Id";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.OptionsColumn.AllowFocus = false;
            this.gridColumn1.Width = 131;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Tên tệp";
            this.gridColumn2.FieldName = "Filename";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.OptionsColumn.AllowFocus = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Đường dẫn";
            this.gridColumn3.FieldName = "Path";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.OptionsColumn.AllowFocus = false;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            this.gridColumn3.Width = 300;
            // 
            // repositoryItemImageComboBox1
            // 
            this.repositoryItemImageComboBox1.AutoHeight = false;
            this.repositoryItemImageComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemImageComboBox1.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Mới thêm mới", "010", -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Thêm mới", "011", -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Đang giảm", "020", -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Đã giảm", "021", -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Đang sửa", "030", -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Đã sửa", "031", -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", null, -1)});
            this.repositoryItemImageComboBox1.Name = "repositoryItemImageComboBox1";
            // 
            // repositoryItemCheckEdit2
            // 
            this.repositoryItemCheckEdit2.AutoHeight = false;
            this.repositoryItemCheckEdit2.Name = "repositoryItemCheckEdit2";
            this.repositoryItemCheckEdit2.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            this.repositoryItemCheckEdit2.ValueChecked = '1';
            this.repositoryItemCheckEdit2.ValueUnchecked = '0';
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Multiselect = true;
            this.openFileDialog1.SupportMultiDottedExtensions = true;
            // 
            // FRM_TD_PheduyetVB_AttachedFiles
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(409, 447);
            this.ControlBox = false;
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(425, 483);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(425, 483);
            this.Name = "FRM_TD_PheduyetVB_AttachedFiles";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FRM_TD_PheduyetVB_AttachedFiles";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FRM_TD_PheduyetVB_AttachedFiles_Load);
            this.panel1.ResumeLayout(false);
            this.panelHeader1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private panelHeader panelHeader1;
        public DevExpress.XtraEditors.SimpleButton btn_capnhat;
        public DevExpress.XtraEditors.SimpleButton btn_thoat;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        public DevExpress.XtraEditors.SimpleButton btn_chontep;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit2;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repositoryItemImageComboBox1;
        private DevExpress.XtraEditors.CheckEdit checkEdit1;
    }
}