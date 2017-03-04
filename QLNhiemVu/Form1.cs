using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QLNhiemVu.DanhMuc;
namespace QLNhiemVu
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            FRM_TD_ThuchienNhiemvu frm = new FRM_TD_ThuchienNhiemvu();
            frm.Show();
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            FRM_TD_PheduyetNhiemvuTuVBCosan frm = new FRM_TD_PheduyetNhiemvuTuVBCosan();
            frm.Show();
        }

        private void simpleButton7_Click(object sender, EventArgs e)
        {
            FRM_DM_Thutuc frm = new FRM_DM_Thutuc();
            frm.Show();
        }

        private void simpleButton11_Click(object sender, EventArgs e)
        {
            FRM_TD_Phancong frm = new FRM_TD_Phancong();
            frm.Show();
        }

        private void simpleButton8_Click(object sender, EventArgs e)
        {
            FRM_TD_Thamdinh frm = new FRM_TD_Thamdinh();
            frm.Show();
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            FRM_TD_Pheduyet frm = new FRM_TD_Pheduyet();
            frm.Show();
        }
    }
}
