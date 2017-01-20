namespace QLNhiemVu.DanhMuc
{
    partial class FRM_DM_ThuTuc_NhiemVu_Truongcon
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FRM_DM_ThuTuc_NhiemVu_Truongcon));
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panelHeader1 = new QLNhiemVu.panelHeader();
            this.btn_thoat = new DevExpress.XtraEditors.SimpleButton();
            this.btn_capnhat = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.gridControl3 = new DevExpress.XtraGrid.GridControl();
            this.gridView3 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ledTruongdulieu_Kieutruong = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.gridColumn16 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btn_truongdulieu_kieutruong = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.led_truongdulieu_truongcha = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.btn_truongdulieu_truongcon = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.thêmToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sửaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.xóaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.panelHeader1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ledTruongdulieu_Kieutruong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_truongdulieu_kieutruong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.led_truongdulieu_truongcha)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_truongdulieu_truongcon)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panelHeader1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 397);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(884, 50);
            this.panel1.TabIndex = 5;
            // 
            // panelHeader1
            // 
            this.panelHeader1.Controls.Add(this.btn_thoat);
            this.panelHeader1.Controls.Add(this.btn_capnhat);
            this.panelHeader1.Location = new System.Drawing.Point(26, 3);
            this.panelHeader1.Name = "panelHeader1";
            this.panelHeader1.Size = new System.Drawing.Size(203, 36);
            this.panelHeader1.TabIndex = 0;
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
            this.groupControl1.Controls.Add(this.gridControl3);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(884, 397);
            this.groupControl1.TabIndex = 6;
            this.groupControl1.Text = "Danh sách";
            // 
            // gridControl3
            // 
            this.gridControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl3.Location = new System.Drawing.Point(2, 27);
            this.gridControl3.MainView = this.gridView3;
            this.gridControl3.Name = "gridControl3";
            this.gridControl3.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1,
            this.btn_truongdulieu_kieutruong,
            this.ledTruongdulieu_Kieutruong,
            this.led_truongdulieu_truongcha,
            this.btn_truongdulieu_truongcon});
            this.gridControl3.Size = new System.Drawing.Size(880, 368);
            this.gridControl3.TabIndex = 6;
            this.gridControl3.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView3});
            // 
            // gridView3
            // 
            this.gridView3.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView3.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView3.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView3.Appearance.Row.Options.UseFont = true;
            this.gridView3.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn6,
            this.gridColumn7,
            this.gridColumn8,
            this.gridColumn10,
            this.gridColumn11,
            this.gridColumn12,
            this.gridColumn13,
            this.gridColumn9,
            this.gridColumn16});
            this.gridView3.GridControl = this.gridControl3;
            this.gridView3.Name = "gridView3";
            this.gridView3.OptionsView.ShowGroupPanel = false;
            this.gridView3.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridView3_FocusedRowChanged);
            this.gridView3.ValidateRow += new DevExpress.XtraGrid.Views.Base.ValidateRowEventHandler(this.gridView3_ValidateRow);
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "Mã số";
            this.gridColumn6.FieldName = "DM016204";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 0;
            this.gridColumn6.Width = 92;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "Tên trường";
            this.gridColumn7.FieldName = "DM016205";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 1;
            this.gridColumn7.Width = 91;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "Tên trường hiển thị";
            this.gridColumn8.FieldName = "DM016206";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 2;
            this.gridColumn8.Width = 148;
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "Công thức tính";
            this.gridColumn10.FieldName = "DM016213";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 3;
            this.gridColumn10.Width = 98;
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "Độ rộng";
            this.gridColumn11.FieldName = "DM016208";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.Visible = true;
            this.gridColumn11.VisibleIndex = 4;
            this.gridColumn11.Width = 60;
            // 
            // gridColumn12
            // 
            this.gridColumn12.Caption = "Sắp xếp";
            this.gridColumn12.FieldName = "DM016214";
            this.gridColumn12.Name = "gridColumn12";
            this.gridColumn12.Visible = true;
            this.gridColumn12.VisibleIndex = 5;
            this.gridColumn12.Width = 60;
            // 
            // gridColumn13
            // 
            this.gridColumn13.Caption = "Bắt buộc nhập";
            this.gridColumn13.ColumnEdit = this.repositoryItemCheckEdit1;
            this.gridColumn13.FieldName = "DM016215";
            this.gridColumn13.Name = "gridColumn13";
            this.gridColumn13.Visible = true;
            this.gridColumn13.VisibleIndex = 6;
            this.gridColumn13.Width = 66;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Caption = "";
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            this.repositoryItemCheckEdit1.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            this.repositoryItemCheckEdit1.NullText = "0";
            this.repositoryItemCheckEdit1.Tag = '0';
            this.repositoryItemCheckEdit1.ValueChecked = '1';
            this.repositoryItemCheckEdit1.ValueGrayed = '0';
            this.repositoryItemCheckEdit1.ValueUnchecked = '0';
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "Kiểu trường";
            this.gridColumn9.ColumnEdit = this.ledTruongdulieu_Kieutruong;
            this.gridColumn9.FieldName = "DM016207";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 7;
            this.gridColumn9.Width = 150;
            // 
            // ledTruongdulieu_Kieutruong
            // 
            this.ledTruongdulieu_Kieutruong.AutoHeight = false;
            this.ledTruongdulieu_Kieutruong.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ledTruongdulieu_Kieutruong.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Description", "Name1")});
            this.ledTruongdulieu_Kieutruong.Name = "ledTruongdulieu_Kieutruong";
            this.ledTruongdulieu_Kieutruong.ShowFooter = false;
            this.ledTruongdulieu_Kieutruong.ShowHeader = false;
            // 
            // gridColumn16
            // 
            this.gridColumn16.Caption = "Điều kiện";
            this.gridColumn16.ColumnEdit = this.btn_truongdulieu_kieutruong;
            this.gridColumn16.Name = "gridColumn16";
            this.gridColumn16.OptionsColumn.FixedWidth = true;
            this.gridColumn16.ToolTip = "Chọn điều kiện dữ liệu hoặc công thức tính";
            this.gridColumn16.Visible = true;
            this.gridColumn16.VisibleIndex = 8;
            // 
            // btn_truongdulieu_kieutruong
            // 
            this.btn_truongdulieu_kieutruong.AutoHeight = false;
            this.btn_truongdulieu_kieutruong.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "...", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "", null, null, true)});
            this.btn_truongdulieu_kieutruong.Name = "btn_truongdulieu_kieutruong";
            this.btn_truongdulieu_kieutruong.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            // 
            // led_truongdulieu_truongcha
            // 
            this.led_truongdulieu_truongcha.AutoHeight = false;
            this.led_truongdulieu_truongcha.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.led_truongdulieu_truongcha.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("DM016206", "Name2")});
            this.led_truongdulieu_truongcha.Name = "led_truongdulieu_truongcha";
            this.led_truongdulieu_truongcha.NullText = "";
            this.led_truongdulieu_truongcha.ShowFooter = false;
            this.led_truongdulieu_truongcha.ShowHeader = false;
            // 
            // btn_truongdulieu_truongcon
            // 
            this.btn_truongdulieu_truongcon.AutoHeight = false;
            this.btn_truongdulieu_truongcon.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.btn_truongdulieu_truongcon.Name = "btn_truongdulieu_truongcon";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.thêmToolStripMenuItem,
            this.sửaToolStripMenuItem,
            this.xóaToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(106, 70);
            // 
            // thêmToolStripMenuItem
            // 
            this.thêmToolStripMenuItem.Name = "thêmToolStripMenuItem";
            this.thêmToolStripMenuItem.Size = new System.Drawing.Size(105, 22);
            this.thêmToolStripMenuItem.Text = "Thêm";
            // 
            // sửaToolStripMenuItem
            // 
            this.sửaToolStripMenuItem.Name = "sửaToolStripMenuItem";
            this.sửaToolStripMenuItem.Size = new System.Drawing.Size(105, 22);
            this.sửaToolStripMenuItem.Text = "Sửa";
            // 
            // xóaToolStripMenuItem
            // 
            this.xóaToolStripMenuItem.Name = "xóaToolStripMenuItem";
            this.xóaToolStripMenuItem.Size = new System.Drawing.Size(105, 22);
            this.xóaToolStripMenuItem.Text = "Xóa";
            // 
            // FRM_DM_ThuTuc_NhiemVu_Truongcon
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(884, 447);
            this.ControlBox = false;
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(900, 483);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(900, 483);
            this.Name = "FRM_DM_ThuTuc_NhiemVu_Truongcon";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FRM_DM_ThuTuc_NhiemVu_Truongcon";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FRM_DM_ThuTuc_NhiemVu_Truongcon_Load);
            this.panel1.ResumeLayout(false);
            this.panelHeader1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ledTruongdulieu_Kieutruong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_truongdulieu_kieutruong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.led_truongdulieu_truongcha)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_truongdulieu_truongcon)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private panelHeader panelHeader1;
        public DevExpress.XtraEditors.SimpleButton btn_capnhat;
        public DevExpress.XtraEditors.SimpleButton btn_thoat;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraGrid.GridControl gridControl3;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit ledTruongdulieu_Kieutruong;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn16;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit btn_truongdulieu_kieutruong;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit btn_truongdulieu_truongcon;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit led_truongdulieu_truongcha;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem thêmToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sửaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem xóaToolStripMenuItem;
    }
}