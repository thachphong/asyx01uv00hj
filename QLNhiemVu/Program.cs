using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
namespace QLNhiemVu
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            string folder_name = @"log";
            bool folderExists = Directory.Exists(folder_name);
            if (!folderExists)
                Directory.CreateDirectory(folder_name);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
    class AllDefine
    {
        public static int gi_Gridcontrol_width = 350;
        public static Size gs_img_size = new Size(145, 164);        
        public static Font Font_control = new Font("Times New Roman", (float)12.75);        
        public static Guid gs_dv_quanly = Guid.Parse("e5b0f164ffb0e5119be8c8f73300b490");       
        public static Guid gs_user_id = Guid.Parse("D22B2275986F97489B0CE55EB5F163FA");
        public static string gs_user_name = "Nguyễn văn XXX";
        public static string gs_ten_dv_quanly = "Cơ quan X";        
        public static string dateFormat = "dd/MM/yyyy";
        public static string datetimeFormat = "dd/MM/yyyy HH:mm:ss";
        public static string timeFormat = "HH:mm:ss";
        public static string numberFormat = "###############";//15 so   
        public static string moneyFormat = "###,###,###,##0.00";
        public static string QuanlityFormat = "###,###,###,##0.0";
        public static string PriceFormat = "###,###,###,##0.0000";
        public static string yyyyMMdd = "yyyyMMdd";
        public static Font Font_GridRow = new Font("Tohoma", (float)9.75);        
        public static void Show_Exception(string Message)
        {
            MessageBox.Show(Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public static void Show_message(string message)
        {            
            MessageBox.Show(message, "Thông báo");
        }
    }
}
