using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLNhiemVu.FRMModel
{
    public partial class FRM_Model1 : Form
    {
        public FRM_Model1()
        {
            InitializeComponent();
        }
        protected string gs_title_from = "Title1";
        protected string ls_status_master = "NORMAL";
        protected string ls_status_detail = "NORMAL";
        protected Guid ls_master_id;
        protected DataTable dt_master;
        protected int master_curentrow = 0;
        protected int detail_currentrow = 0;             
        private void uC_MenuButton1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("đasfss");

        }

        private void them_click(object sender, EventArgs e)
        {
            wf_them();
        }
        private void sua_click(object sender, EventArgs e)
        {
            wf_sua();
        }
        private void xoa_click(object sender, EventArgs e)
        {
            wf_xoa();
        }
        private void capnhat_click(object sender, EventArgs e)
        {
            wf_capnhat();
        }
        private void in_click(object sender, EventArgs e)
        {
            wf_in();
        }
        private void tim_click(object sender, EventArgs e)
        {
            wf_tim();
        }
        private void boqua_click(object sender, EventArgs e)
        {
            wf_boqua();
        }
        private void thoat_click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void congnhan_huy_click(object sender, EventArgs e)
        {
            wf_congnhan_huy();
        }
        public virtual void wf_congnhan_huy()
        { }
        public virtual void wf_them()
        { }
        public virtual void wf_sua()
        { }
        public virtual void wf_xoa()
        { }
        public virtual void wf_tim()
        { }
        public virtual void wf_in()
        { }
        public virtual void wf_boqua()
        { }
        public virtual void wf_capnhat()
        { }
        public virtual void set_quanhegiadinh(string column_name, Guid giatri) { }
        private void Frm_Model1_SizeChanged(object sender, EventArgs e)
        {
            panelHeader1.alignCenter(panelHeader1.Parent);
        }
        private void uC_MenuBtn1_Load_1(object sender, EventArgs e)
        {
            uC_MenuBtn1.btn_them.Click += new EventHandler(them_click);
            uC_MenuBtn1.btn_sua.Click += new EventHandler(sua_click);
            uC_MenuBtn1.btn_xoa.Click += new EventHandler(xoa_click);
            uC_MenuBtn1.btn_capnhat.Click += new EventHandler(capnhat_click);
            uC_MenuBtn1.btn_in.Click += new EventHandler(in_click);
            uC_MenuBtn1.btn_tim.Click += new EventHandler(tim_click);
            uC_MenuBtn1.btn_thoat.Click += new EventHandler(thoat_click);
            uC_MenuBtn1.btn_boqua.Click += new EventHandler(boqua_click);
            //uC_MenuBtn1.btn_congnhan.Click += new EventHandler(congnhan_huy_click);
        }

        private void FRM_Model1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                switch (e.KeyCode)
                {
                    case Keys.N:
                        wf_them();
                        break;
                    case Keys.M:
                        wf_sua();
                        break;
                    case Keys.D:
                        wf_xoa();
                        break;
                    case Keys.S:
                        wf_capnhat();
                        break;
                    case Keys.F:
                        wf_tim();
                        break;
                    case Keys.P:
                        wf_in();
                        break;
                    case Keys.E:
                        this.Close();
                        break;
                }

            }
            else if (e.KeyCode == Keys.Escape)
            {
                wf_boqua();
            }
        }

        private void FRM_Model1_Load_1(object sender, EventArgs e)
        {
            //AllDefine.set_val_forDBConnection();
            lblHeadTitle1.setText(gs_title_from);
            lblHeadTitle1.alignCenter(lblHeadTitle1.Parent);
            panelHeader1.alignCenter(panelHeader1.Parent);
            panelHeader2.alignCenter(panelHeader2.Parent);
            //groupLeft.Width = AllDefine.gi_Gridcontrol_width;
            this.KeyPreview = true; 
        }
    }
}
