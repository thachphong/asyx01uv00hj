using DBAccess;
using QLNhiemVu.FRMModel;
using QLNhiemvu_DBEntities;
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
    public partial class FRM_TD_Phancong_Xemykien : BaseForm_Data
    {
        public List<TD_Phancong> currentList = null;
        public string currentState = "NORMAL";
        public FRM_TD_Phancong_Xemykien()
        {
            InitializeComponent();
        }

        private void FRM_TD_Phancong_Xemykien_Load(object sender, EventArgs e)
        {
            panelHeader1.alignCenter(panelHeader1.Parent);

            LoadList();
        }

        private void LoadList()
        {
            gridControl1.DataSource = currentList;
            gridControl1.RefreshDataSource();
            gridView1.BestFitColumns();
        }

        private void btn_capnhat_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
