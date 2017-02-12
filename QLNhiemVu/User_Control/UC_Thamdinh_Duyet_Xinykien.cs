using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QLNhiemvu_DBEntities;
using DBAccess;

namespace QLNhiemVu.User_Control
{
    public partial class UC_Thamdinh_Duyet_Xinykien :  CB_Thamdinh_Duyet_Chitiet, I_UC_Thamdinh_Duyet_Chitiet
    {
        public UC_Thamdinh_Duyet_Xinykien()
        {
            InitializeComponent();
        }

        private void UC_Thamdinh_Duyet_Xinykien_Load(object sender, EventArgs e)
        {
            panelHeader1.alignCenter(panelHeader1.Parent);

            LoadNguoiky();
        }

        private void LoadNguoiky()
        {
            List<TD_Nguoiky> list = Helpers.Trinhduyet.GetList_Nhansu(Guid.Empty, '0', '0');
            lookUpEdit11.Properties.DataSource = list;
            lookUpEdit11.Properties.DisplayMember = "DM030403";
            lookUpEdit11.Properties.ValueMember = "DM030401";
            lookUpEdit11.Properties.BestFitRowCount = list == null ? 0 : list.Count;
        }

        public void AssignData(TD_Thamdinh_Duyet objData)
        {
            textEdit2.Text = objData == null ? string.Empty : objData.DM017111;
            dateEdit3.DateTime = objData == null ? DateTime.Now : objData.DM017112;
            lookUpEdit11.EditValue = objData == null ? Guid.Empty : objData.DM017113;
            textEdit5.Text = objData == null ? string.Empty : ((TD_Nguoiky)lookUpEdit11.GetSelectedDataRow()).Chucvu;
            textEdit4.Text = objData == null ? string.Empty : objData.DM017114;
            checkEdit1.Checked = objData == null ? false : objData.DM017115 == '1';
            checkEdit1.Checked = objData == null ? false : objData.DM017116 == '1';
        }

        public void RefreshUI()
        {
            panelHeader1.alignCenter(panelHeader1.Parent);
        }

        public void FillData(ref TD_Thamdinh_Duyet objData)
        {
            objData.DM017111 = textEdit2.Text;
            objData.DM017112 = dateEdit3.DateTime;
            objData.DM017113 = Guid.Parse(lookUpEdit11.EditValue.ToString());
            objData.DM017114 = textEdit4.Text;
            objData.DM017115 = checkEdit1.Checked ? '1' : '0';
            objData.DM017116 = checkEdit2.Checked ? '1' : '0';
        }
    }
}
