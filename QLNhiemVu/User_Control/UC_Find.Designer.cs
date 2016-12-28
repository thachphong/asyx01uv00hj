namespace QLNhiemVu.User_Control
{
    partial class UC_Find
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UC_Find));
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            this.btn_tim = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            this.SuspendLayout();
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageSize = new System.Drawing.Size(24, 24);
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.InsertGalleryImage("zoom_32x32.png", "images/zoom/zoom_32x32.png", DevExpress.Images.ImageResourceCache.Default.GetImage("images/zoom/zoom_32x32.png"), 0);
            this.imageCollection1.Images.SetKeyName(0, "zoom_32x32.png");
            // 
            // btn_tim
            // 
            this.btn_tim.Appearance.Font = new System.Drawing.Font("Times New Roman", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_tim.Appearance.Options.UseFont = true;
            this.btn_tim.ImageIndex = 0;
            this.btn_tim.ImageList = this.imageCollection1;
            this.btn_tim.Location = new System.Drawing.Point(0, 0);
            this.btn_tim.Name = "btn_tim";
            this.btn_tim.Size = new System.Drawing.Size(77, 29);
            this.btn_tim.TabIndex = 94;
            this.btn_tim.Text = "Tìm";
            this.btn_tim.ToolTip = "Ctrl+F";
            // 
            // UC_Find
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btn_tim);
            this.Name = "UC_Find";
            this.Size = new System.Drawing.Size(79, 29);
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.Utils.ImageCollection imageCollection1;
        public DevExpress.XtraEditors.SimpleButton btn_tim;
    }
}
