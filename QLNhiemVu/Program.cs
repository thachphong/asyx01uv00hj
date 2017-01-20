using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using QLNhiemvu_DBEntities;

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
}
