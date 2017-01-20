namespace QLNhiemVu.User_Control
{
    partial class UC_Help
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UC_Help));
            this.btn_main = new DevExpress.XtraEditors.SimpleButton();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_main
            // 
            this.btn_main.Appearance.Font = new System.Drawing.Font("Times New Roman", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_main.Appearance.ForeColor = System.Drawing.Color.Green;
            this.btn_main.Appearance.Options.UseFont = true;
            this.btn_main.Appearance.Options.UseForeColor = true;
            this.btn_main.ImageIndex = 0;
            this.btn_main.ImageList = this.imageCollection1;
            this.btn_main.Location = new System.Drawing.Point(0, 0);
            this.btn_main.Name = "btn_main";
            this.btn_main.Size = new System.Drawing.Size(136, 32);
            this.btn_main.TabIndex = 17;
            this.btn_main.Text = "Hướng dẫn";
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageSize = new System.Drawing.Size(24, 24);
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.InsertGalleryImage("knowledgebasearticle_32x32.png", "office2013/support/knowledgebasearticle_32x32.png", DevExpress.Images.ImageResourceCache.Default.GetImage("office2013/support/knowledgebasearticle_32x32.png"), 0);
            this.imageCollection1.Images.SetKeyName(0, "knowledgebasearticle_32x32.png");
            // 
            // UC_Help
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btn_main);
            this.Name = "UC_Help";
            this.Size = new System.Drawing.Size(136, 32);
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.Utils.ImageCollection imageCollection1;
        public DevExpress.XtraEditors.SimpleButton btn_main;
    }
}
