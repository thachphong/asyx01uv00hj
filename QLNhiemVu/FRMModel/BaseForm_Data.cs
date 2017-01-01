using DBAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLNhiemVu.FRMModel
{
    public class BaseForm_Data : Form
    {
        protected override void OnLoad(EventArgs e)
        {
            if (!Helpers.DBUtilities.CheckConnection())
            {
                AllDefine.Show_message("Hiện không kết nối được hệ thống Cơ sở dữ liệu!");
                this.Dispose();
                return;
            }

            base.OnLoad(e);
        }
    }
}
