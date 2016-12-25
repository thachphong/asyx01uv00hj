using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLNhiemVu.DanhMuc
{
    public partial class FRM_ThamDinh : Form
    {
        public FRM_ThamDinh()
        {
            InitializeComponent();
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }

        private void FRM_ThamDinh_Load(object sender, EventArgs e)
        {
            lblHeadTitle1.setText("Thẩm định");
            lblHeadTitle1.alignCenter(lblHeadTitle1.Parent);
            panelHeader1.alignCenter(panelHeader1.Parent);
            panelHeader2.alignCenter(panelHeader2.Parent);
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            FRM_ThamDinh_Duyet frm = new FRM_ThamDinh_Duyet();
            frm.Show();
        }
    }
}
