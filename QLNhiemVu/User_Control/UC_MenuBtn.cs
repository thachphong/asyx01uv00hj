using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLNhiemVu.User_Control
{
    public partial class UC_MenuBtn : UserControl
    {
        public UC_MenuBtn()
        {
            InitializeComponent();
        }
        public void set_status_menu(string ps_edit, int pi_row_count)
        {
            if (ps_edit == "NORMAL")
            {
                if (pi_row_count > 0)
                {
                    btn_them.Enabled = true;
                    btn_sua.Enabled = true;
                    btn_xoa.Enabled = true;
                    btn_capnhat.Enabled = false;
                    btn_in.Enabled = true;
                    btn_boqua.Enabled = false;
                    //btn_congnhan.Enabled = true;
                }
                else
                {
                    btn_them.Enabled = true;
                    btn_sua.Enabled = false;
                    btn_xoa.Enabled = false;
                    btn_capnhat.Enabled = false;
                    btn_in.Enabled = true;
                    btn_boqua.Enabled = false;
                   // btn_congnhan.Enabled = false;
                }
            }
            else if (ps_edit == "NEW")
            {

                btn_them.Enabled = false;
                btn_sua.Enabled = false;
                btn_xoa.Enabled = false;
                btn_capnhat.Enabled = true;
                btn_in.Enabled = false;
                btn_boqua.Enabled = true;
                //btn_congnhan.Enabled = false;
            }
            else if (ps_edit == "EDIT")
            {
                btn_them.Enabled = false;
                btn_sua.Enabled = false;
                btn_xoa.Enabled = false;
                btn_capnhat.Enabled = true;
                btn_in.Enabled = false;
                btn_boqua.Enabled = true;
                //btn_congnhan.Enabled = false;
            }
        }
    }
}
