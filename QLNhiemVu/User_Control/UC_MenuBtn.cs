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
                    btn_in.Enabled = false;
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

        private void UC_MenuBtn_Load(object sender, EventArgs e)
        {
            this.ParentForm.KeyPreview = true;
            this.ParentForm.KeyDown += ParentForm_KeyDown;
        }

        void ParentForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape && btn_boqua.Enabled)
            {
                btn_boqua.PerformClick();
                return;
            }
            else
                if (!e.Control) return;

            switch (e.KeyCode)
            {
                case Keys.N: btn_them.PerformClick(); break;
                case Keys.M: btn_sua.PerformClick(); break;
                case Keys.D: btn_xoa.PerformClick(); break;
                case Keys.S: btn_capnhat.PerformClick(); break;
                case Keys.P: btn_in.PerformClick(); break;
                case Keys.E: btn_thoat.PerformClick(); break;
                default: break;
            }
        }
    }
}
