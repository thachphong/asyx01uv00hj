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
            FRM_TDTHNV frm = new FRM_TDTHNV();
            frm.Show();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            FRM_PC_CQ frm = new FRM_PC_CQ();
            frm.Show();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            FRM_PC_PB frm = new FRM_PC_PB();
            frm.Show();
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            FRM_ThamDinh frm = new FRM_ThamDinh();
            frm.Show();
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            FRM_CN_TVB frm = new FRM_CN_TVB();
            frm.Show();
        }

        private void simpleButton7_Click(object sender, EventArgs e)
        {
            FRM_DM_LoaiThutucNhiemvu frm = new FRM_DM_LoaiThutucNhiemvu();
            frm.Show();
        }

        private void simpleButton8_Click(object sender, EventArgs e)
        {
            FRM_DM_LoaiThutucNhiemvu_Huongdan frm = new FRM_DM_LoaiThutucNhiemvu_Huongdan();
            frm.Show();
        }

        private void simpleButton9_Click(object sender, EventArgs e)
        {
            FRM_DM_LoaiThutucNhiemvu_Noidung frm = new FRM_DM_LoaiThutucNhiemvu_Noidung();
            frm.Show();
        }

        private void simpleButton10_Click(object sender, EventArgs e)
        {
            FRM_DM_LoaiThutucNhiemvu_ChitietTruongdulieu frm = new FRM_DM_LoaiThutucNhiemvu_ChitietTruongdulieu();
            frm.Show();
        }
    }
}
