using DBAccess;
using DevExpress.XtraEditors;
using QLNhiemvu_DBEntities;
using QLNhiemVu_Defines;
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
    public partial class FRM_TD_Pheduyet : Form
    {
        private static Guid donvisudungID = All.gs_dv_quanly;
        private static string donvisudungName = All.gs_ten_dv_quanly;
        private static Guid nguoisudungID = All.gs_user_id;
        private static string nguoisudungName = All.gs_user_name;
        private static string MaForm = "1";
        private static DM_LoaiThutucNhiemvu currentThutucNhiemvu = null;

        private static List<TD_ThuchienNhiemvu> currentList = null;
        private static TD_ThuchienNhiemvu currentSeleted = null;

        public FRM_TD_Pheduyet()
        {
            InitializeComponent();
        }

        private void FRM_TD_Pheduyet_Load(object sender, EventArgs e)
        {
            currentThutucNhiemvu = Helpers.ThutucNhiemvu.Get_ByMaso(MaForm);
            if (currentThutucNhiemvu == null)
            {
                if (MessageBox.Show("Mã Form không đúng, vui lòng kiểm tra lại!", "Thông báo", MessageBoxButtons.OK) == System.Windows.Forms.DialogResult.OK)
                {
                    this.Dispose();
                    return;
                }
            }
            this.Text = currentThutucNhiemvu.DM016004;

            lblHeadTitle1.setText("Phê duyệt Nhiệm vụ từ dữ liệu Thẩm định");
            lblHeadTitle1.alignCenter(lblHeadTitle1.Parent);
            panelHeader1.alignCenter(panelHeader1.Parent);
            panelHeader2.alignCenter(panelHeader2.Parent);

            gridColumn12.ColumnEdit.EditValueChanged += ColumnEdit_EditValueChanged;

            //LoadTemplates();

            LoadList();
        }

        void ColumnEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (((CheckEdit)sender).Checked)
                currentSeleted = (TD_ThuchienNhiemvu)gridView1.GetFocusedRow();
            else
                currentSeleted = null;
        }

        private void LoadList()
        {
            TD_Pheduyet_Thamdinh_Filter filter = new TD_Pheduyet_Thamdinh_Filter();
            filter.PhongbanBophan = All.gs_dv_quanly;

            currentList = Helpers.TrinhduyetThuchienNhiemvu.GetList_For_PheduyetThamdinh(filter);

            gridControl1.DataSource = currentList;
            gridControl1.RefreshDataSource();
            gridView1.BestFitColumns();
        }

        //private void LoadTemplates()
        //{
        //    //dateEdit1.Properties.Mask.EditMask = All.dateFormat;
        //    //dateEdit2.Properties.Mask.EditMask = All.dateFormat;

        //    LoadYears();
        //    LoadPhanloaiNhiemvu();
        //    LoadLoaithutuc();
        //    LoadDonviQuanly();
        //    LoadTrangthaiHoso();
        //}

        //private void LoadYears()
        //{
        //    List<int> years = new List<int>();
        //    for (int i = DateTime.Now.Year - 3; i <= DateTime.Now.Year + 9; i++)
        //    {
        //        years.Add(i);
        //    }

        //    lookUpEdit6.Properties.DataSource = years;
        //    lookUpEdit6.Properties.BestFitRowCount = years.Count;
        //    lookUpEdit6.EditValue = DateTime.Now.Year;
        //}

        //private void LoadLoaithutuc()
        //{
        //    List<DM_LoaiThutucNhiemvu> list = Helpers.ThutucNhiemvu.GetList();
        //    //DsDonviCapduoi = list;//Dành cho test
        //    lookUpEdit1.Properties.DataSource = list;
        //    lookUpEdit1.Properties.DisplayMember = "DM016004";
        //    lookUpEdit1.Properties.ValueMember = "DM016001";
        //    lookUpEdit1.Properties.BestFitRowCount = list == null ? 0 : list.Count;
        //}

        //private void LoadDonviQuanly()
        //{
        //    List<TD_DonviQuanly> list = Helpers.Trinhduyet.GetList_DonviQuanly();
        //    //DsDonviCapduoi = list;//Dành cho test
        //    lookUpEdit7.Properties.DataSource = list;
        //    lookUpEdit7.Properties.DisplayMember = "DM030105";
        //    lookUpEdit7.Properties.ValueMember = "DM030101";
        //    lookUpEdit7.Properties.BestFitRowCount = list == null ? 0 : list.Count;
        //}

        //private void LoadPhanloaiNhiemvu()
        //{
        //    List<TD_ThuchienNhiemvu_PhanloaiNhiemvu> list = Helpers.TrinhduyetThuchienNhiemvu.GetList_PhanloaiNhiemvu();
        //    lookUpEdit2.Properties.DataSource = list;
        //    lookUpEdit2.Properties.DisplayMember = "DM014003";
        //    lookUpEdit2.Properties.ValueMember = "DM014001";
        //    lookUpEdit2.Properties.BestFitRowCount = list == null ? 0 : list.Count;
        //}

        //private void LoadDanhmucNhiemvu()
        //{
        //    List<TD_ThuchienNhiemvu_DanhmucNhiemvu> list = null;
        //    if (lookUpEdit2.EditValue != null)
        //    {
        //        TD_ThuchienNhiemvu_PhanloaiNhiemvu obj = (TD_ThuchienNhiemvu_PhanloaiNhiemvu)lookUpEdit2.GetSelectedDataRow();
        //        list = obj.DSDanhmuc;
        //    }

        //    lookUpEdit8.Properties.DataSource = list;
        //    lookUpEdit8.Properties.DisplayMember = "DM020204";
        //    lookUpEdit8.Properties.ValueMember = "DM020201";
        //    lookUpEdit8.Properties.BestFitRowCount = list == null ? 0 : list.Count;
        //}

        //private void LoadTrangthaiHoso()
        //{
        //    List<TD_TrangthaiHoSo> list = Helpers.Trinhduyet.GetList_TrangthaiHoSo();
        //    lookUpEdit3.Properties.DataSource = list;
        //    lookUpEdit3.Properties.DisplayMember = "DM012403";
        //    lookUpEdit3.Properties.ValueMember = "DM012401";
        //    lookUpEdit3.Properties.BestFitRowCount = list == null ? 0 : list.Count;
        //}

        //private void lookUpEdit2_EditValueChanged(object sender, EventArgs e)
        //{
        //    LoadDanhmucNhiemvu();
        //}

        private void btn_chapnhan_Click(object sender, EventArgs e)
        {
            if (currentSeleted == null)
            {
                All.Show_message("Chưa chọn Nhiệm vụ!");
                return;
            }

            FRM_TD_Thamdinh_Duyet frm = new FRM_TD_Thamdinh_Duyet();
            frm.TD_Nhiemvu = currentSeleted;
            frm.Show();
            frm.Focus();
        }
    }
}
