namespace QLNhiemVu.User_Control
{
    partial class UC_MenuBtn
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UC_MenuBtn));
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_boqua = new DevExpress.XtraEditors.SimpleButton();
            this.btn_sua = new DevExpress.XtraEditors.SimpleButton();
            this.btn_them = new DevExpress.XtraEditors.SimpleButton();
            this.btn_in = new DevExpress.XtraEditors.SimpleButton();
            this.btn_capnhat = new DevExpress.XtraEditors.SimpleButton();
            this.btn_thoat = new DevExpress.XtraEditors.SimpleButton();
            this.btn_xoa = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageSize = new System.Drawing.Size(24, 24);
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "add2.png");
            this.imageCollection1.InsertGalleryImage("undo_32x32.png", "office2013/history/undo_32x32.png", DevExpress.Images.ImageResourceCache.Default.GetImage("office2013/history/undo_32x32.png"), 1);
            this.imageCollection1.Images.SetKeyName(1, "undo_32x32.png");
            this.imageCollection1.Images.SetKeyName(2, "delete.png");
            this.imageCollection1.InsertGalleryImage("edit_32x32.png", "images/edit/edit_32x32.png", DevExpress.Images.ImageResourceCache.Default.GetImage("images/edit/edit_32x32.png"), 3);
            this.imageCollection1.Images.SetKeyName(3, "edit_32x32.png");
            this.imageCollection1.Images.SetKeyName(4, "Exit.png");
            this.imageCollection1.Images.SetKeyName(5, "Find.png");
            this.imageCollection1.InsertGalleryImage("print_32x32.png", "images/print/print_32x32.png", DevExpress.Images.ImageResourceCache.Default.GetImage("images/print/print_32x32.png"), 6);
            this.imageCollection1.Images.SetKeyName(6, "print_32x32.png");
            this.imageCollection1.InsertGalleryImage("saveto_32x32.png", "images/save/saveto_32x32.png", DevExpress.Images.ImageResourceCache.Default.GetImage("images/save/saveto_32x32.png"), 7);
            this.imageCollection1.Images.SetKeyName(7, "saveto_32x32.png");
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_boqua);
            this.groupBox1.Controls.Add(this.btn_sua);
            this.groupBox1.Controls.Add(this.btn_them);
            this.groupBox1.Controls.Add(this.btn_in);
            this.groupBox1.Controls.Add(this.btn_capnhat);
            this.groupBox1.Controls.Add(this.btn_thoat);
            this.groupBox1.Controls.Add(this.btn_xoa);
            this.groupBox1.Location = new System.Drawing.Point(2, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(648, 52);
            this.groupBox1.TabIndex = 87;
            this.groupBox1.TabStop = false;
            // 
            // btn_boqua
            // 
            this.btn_boqua.Appearance.Font = new System.Drawing.Font("Times New Roman", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_boqua.Appearance.Options.UseFont = true;
            this.btn_boqua.ImageIndex = 1;
            this.btn_boqua.ImageList = this.imageCollection1;
            this.btn_boqua.Location = new System.Drawing.Point(268, 15);
            this.btn_boqua.Name = "btn_boqua";
            this.btn_boqua.Size = new System.Drawing.Size(85, 29);
            this.btn_boqua.TabIndex = 93;
            this.btn_boqua.Text = "Bỏ qua";
            this.btn_boqua.ToolTip = "ESC";
            // 
            // btn_sua
            // 
            this.btn_sua.Appearance.Font = new System.Drawing.Font("Times New Roman", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_sua.Appearance.Options.UseFont = true;
            this.btn_sua.ImageIndex = 3;
            this.btn_sua.ImageList = this.imageCollection1;
            this.btn_sua.Location = new System.Drawing.Point(98, 15);
            this.btn_sua.Name = "btn_sua";
            this.btn_sua.Size = new System.Drawing.Size(75, 29);
            this.btn_sua.TabIndex = 87;
            this.btn_sua.Text = "Sửa";
            this.btn_sua.ToolTip = "Ctrl+M";
            // 
            // btn_them
            // 
            this.btn_them.Appearance.Font = new System.Drawing.Font("Times New Roman", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_them.Appearance.Options.UseFont = true;
            this.btn_them.ImageIndex = 0;
            this.btn_them.ImageList = this.imageCollection1;
            this.btn_them.Location = new System.Drawing.Point(11, 15);
            this.btn_them.Name = "btn_them";
            this.btn_them.Size = new System.Drawing.Size(77, 29);
            this.btn_them.TabIndex = 86;
            this.btn_them.Text = "Thêm";
            this.btn_them.ToolTip = "Ctrl+N";
            // 
            // btn_in
            // 
            this.btn_in.Appearance.Font = new System.Drawing.Font("Times New Roman", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_in.Appearance.Options.UseFont = true;
            this.btn_in.ImageIndex = 6;
            this.btn_in.ImageList = this.imageCollection1;
            this.btn_in.Location = new System.Drawing.Point(468, 15);
            this.btn_in.Name = "btn_in";
            this.btn_in.Size = new System.Drawing.Size(77, 29);
            this.btn_in.TabIndex = 92;
            this.btn_in.Text = "In";
            this.btn_in.ToolTip = "Ctrl+F";
            // 
            // btn_capnhat
            // 
            this.btn_capnhat.Appearance.Font = new System.Drawing.Font("Times New Roman", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_capnhat.Appearance.Options.UseFont = true;
            this.btn_capnhat.ImageIndex = 7;
            this.btn_capnhat.ImageList = this.imageCollection1;
            this.btn_capnhat.Location = new System.Drawing.Point(363, 15);
            this.btn_capnhat.Name = "btn_capnhat";
            this.btn_capnhat.Size = new System.Drawing.Size(95, 29);
            this.btn_capnhat.TabIndex = 88;
            this.btn_capnhat.Text = "Cập nhật";
            this.btn_capnhat.ToolTip = "Ctrl+S";
            // 
            // btn_thoat
            // 
            this.btn_thoat.Appearance.Font = new System.Drawing.Font("Times New Roman", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_thoat.Appearance.Options.UseFont = true;
            this.btn_thoat.ImageIndex = 4;
            this.btn_thoat.ImageList = this.imageCollection1;
            this.btn_thoat.Location = new System.Drawing.Point(555, 15);
            this.btn_thoat.Name = "btn_thoat";
            this.btn_thoat.Size = new System.Drawing.Size(80, 29);
            this.btn_thoat.TabIndex = 90;
            this.btn_thoat.Text = "Thoát";
            this.btn_thoat.ToolTip = "Ctrl+E";
            // 
            // btn_xoa
            // 
            this.btn_xoa.Appearance.Font = new System.Drawing.Font("Times New Roman", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_xoa.Appearance.Options.UseFont = true;
            this.btn_xoa.ImageIndex = 2;
            this.btn_xoa.ImageList = this.imageCollection1;
            this.btn_xoa.Location = new System.Drawing.Point(183, 15);
            this.btn_xoa.Name = "btn_xoa";
            this.btn_xoa.Size = new System.Drawing.Size(75, 29);
            this.btn_xoa.TabIndex = 89;
            this.btn_xoa.Text = "Xóa";
            this.btn_xoa.ToolTip = "Ctrl+D";
            // 
            // UC_MenuBtn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "UC_MenuBtn";
            this.Size = new System.Drawing.Size(656, 61);
            this.Load += new System.EventHandler(this.UC_MenuBtn_Load);
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.Utils.ImageCollection imageCollection1;
        private System.Windows.Forms.GroupBox groupBox1;
        public DevExpress.XtraEditors.SimpleButton btn_boqua;
        public DevExpress.XtraEditors.SimpleButton btn_sua;
        public DevExpress.XtraEditors.SimpleButton btn_them;
        public DevExpress.XtraEditors.SimpleButton btn_in;
        public DevExpress.XtraEditors.SimpleButton btn_capnhat;
        public DevExpress.XtraEditors.SimpleButton btn_thoat;
        public DevExpress.XtraEditors.SimpleButton btn_xoa;
    }
}
