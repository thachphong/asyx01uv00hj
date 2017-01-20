using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DBAccess;
using DevExpress.XtraEditors;
using QLNhiemVu.FRMModel;
using QLNhiemvu_DBEntities;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using Newtonsoft.Json;
using QLNhiemVu_Defines;
namespace QLNhiemVu.DanhMuc
{
    public partial class FRM_DM_ThuTuc_NhiemVu : BaseForm_Data
    {
        private static List<DM_LoaiThutucNhiemvu> currentList = null;
        private static int currentRowSelected = int.MinValue;
        private string currentState = "NORMAL";
        private static List<Guid> currentListFields = null;
        private static DM_LoaiThutucNhiemvu currentDataSelected = null;
        private int currentRowSelected_D1 = 0;
        private int currentRowSelected_D2 = 0;
        private string _status_detail_1 = "NORMAL";
        private string _status_detail_2 = "NORMAL";
        private bool needUpdate = false;

        private DataTable currentDataTable = null;
        private List<DM_LoaiThutucNhiemvu_Noidung> currentNoidungs = null;
        public List<Guid> currentNoidungTruongdulieus = null;
        private static string currentNoidungLookupData = string.Empty;

        private List<DM_LoaiThutucNhiemvu_Truongdulieu> currentTruongdulieus = null;
        public List<Guid> currentTruongdulieu_Children = null;
        public DM_LoaiThutucNhiemvu_Truongdulieu_LookupData currentTruongdulieu_Lookupdata = null;
        public Guid currentNoidungId = Guid.Empty;

        public List<TD_ThuchienNhiemvu_Cothienthi> dsCothienthi = null;
        public FRM_DM_ThuTuc_NhiemVu()
        {
            InitializeComponent();
        }

        private void gridControl3_Click(object sender, EventArgs e)
        {

        }

        private void FRM_DM_ThuTuc_NhiemVu_Load(object sender, EventArgs e)
        {
            lblHeadTitle1.setText("Danh mục thủ tục nhiệm vụ");
            lblHeadTitle1.alignCenter(lblHeadTitle1.Parent);
            panelHeader1.alignCenter(panelHeader1.Parent);
            //panelHeader2.alignCenter(panelHeader2.Parent);
            gridView1.OptionsBehavior.Editable = false;
            //gridView3.AddNewRow();
            BindControlEvents();
            LoadDonvi();
            LoadPhamviThuTuc();
            LoadLoaiThutuc_Trinhduyet();
            LoadList_Master();

            SetDetailFormEnable(false);
            MenuItem_them_d1.Image = imageCollection1.Images[0];
            MenuItem_sua_d1.Image = imageCollection1.Images[1];
            MenuItem_xoa_d1.Image = imageCollection1.Images[2];

            MenuItem_them_d2.Image = imageCollection1.Images[0];
            MenuItem_sua_d2.Image = imageCollection1.Images[1];
            MenuItem_xoa_d2.Image = imageCollection1.Images[2];

            config_detail();

            btn_truongdulieu_kieutruong.Click += btn_truongdulieu_kieutruong_Click;
            btn_truongdulieu_truongcon.Click += btn_truongdulieu_truongcon_Click;
        }

        void btn_noidung_cauhinh_Click(object sender, EventArgs e)
        {
            DataRow data = gridView2.GetFocusedDataRow();
            Guid noidungId = Guid.Parse(data.ItemArray[0].ToString());
            char cachnhap = char.Parse(data.ItemArray[4].ToString());

            currentNoidungTruongdulieus = currentNoidungs.FirstOrDefault(o => o.DM016101 == noidungId).FieldSelecteds;

            if (cachnhap == '2')
            {
                needUpdate = true;
                ShowChildForm_NoidungTruongdulieu();
            }
        }

        private void ShowChildForm_NoidungTruongdulieu()
        {
            this.Enabled = false;
            FRM_DM_LoaiThutucNhiemvu_Noidung_Nhaptruongdulieu frm = new FRM_DM_LoaiThutucNhiemvu_Noidung_Nhaptruongdulieu();
            frm.currentData = currentNoidungTruongdulieus;
            frm.currentState = _status_detail_1;
            frm.Show();
            frm.Focus();
        }
        public void CallBack_UpdateFieldSelecteds(bool update)
        {
            //if (update)
            //{
            //    currentNoidungTruongdulieus = data;
            //}

            this.Enabled = true;
            this.Focus();

            if (update && needUpdate && _status_detail_1 == "EDIT")
            {
                int rowselect = gridView2.FocusedRowHandle;
                string ma_noidung = gridView2.GetRowCellValue(rowselect, "DM016103").ToString();
                string ten_noidung = gridView2.GetRowCellValue(rowselect, "DM016104").ToString();
                string cachnhap = gridView2.GetRowCellValue(rowselect, "DM016105").ToString();
                if ((ma_noidung + ten_noidung + cachnhap).Length == 0)
                {
                    _status_detail_1 = "NORMAL";
                    gridView2.DeleteRow(rowselect);
                    gridView2.OptionsBehavior.Editable = false;
                    return;
                }
                if (ma_noidung.Length == 0)
                {
                    All.Show_message("Bạn phải nhập Mã nội dung!");
                    return;
                }
                if (ten_noidung.Length == 0)
                {
                    All.Show_message("Bạn phải nhập Tên nội dung!");
                    return;
                }
                if (cachnhap.Length == 0)
                {
                    All.Show_message("Bạn phải chọn cách nhập!");
                    return;
                }
                DM_LoaiThutucNhiemvu_Noidung obj = PrepareD1(gridView2);
                GridColumn col1 = gridView2.Columns[1];
                if (obj == null)
                {
                    All.Show_message("Có lỗi trong quá trình cập nhật dữ liệu!");
                    set_enable_groupControl(currentState);
                    gridView2.OptionsBehavior.Editable = false;
                    gridView2.OptionsBehavior.ReadOnly = true;
                    return;
                }

                APIResponseData result = Helpers.ThutucNhiemvu_Noidung.Update(obj);
                if (result == null)
                {
                    All.Show_message("Có lỗi trong quá trình cập nhật dữ liệu!");
                }
                else if (result.ErrorCode == 0)
                {
                    All.Show_message("Cập nhật thành công Nội dung: " + obj.DM016103);
                    _status_detail_1 = "NORMAL";
                    //gridView2.GetFocusedDataRow().ItemArray[15] = currentNoidungTruongdulieus;
                    //currentDataTable.Rows[gridView2.GetFocusedDataSourceRowIndex()].ItemArray[15] = currentNoidungTruongdulieus;
                    //gridControl2.DataSource = currentDataTable;
                    load_detail_1();
                }
                else
                {
                    All.Show_message("Error code " + result.ErrorCode + ": " + result.Message);
                }

                set_enable_groupControl(currentState);
                gridView2.OptionsBehavior.Editable = false;
                gridView2.OptionsBehavior.ReadOnly = true;
            }
        }

        private void LoadList_Master()
        {
            currentList = Helpers.ThutucNhiemvu.GetList();

            gridControl1.DataSource = currentList;
            gridControl1.RefreshDataSource();
            gridView1.BestFitColumns();

            uC_MenuBtn1.set_status_menu(currentState, currentList == null ? 0 : currentList.Count);
            //SetDetailFormEnable(false);

            DM_LoaiThutucNhiemvu current = (DM_LoaiThutucNhiemvu)gridView1.GetFocusedRow();
            //AssignDetailFormValue(current);
        }
        private void LoadDonvi()
        {
            List<DM_Donvi> list = new List<DM_Donvi>() { new DM_Donvi() { ID = All.gs_dv_quanly, Name = All.gs_ten_dv_quanly } };
            m_donvi_sdct_lk.Properties.DataSource = list;
            m_donvi_sdct_lk.Properties.DisplayMember = "Name";
            m_donvi_sdct_lk.Properties.ValueMember = "ID";
            m_donvi_sdct_lk.Properties.BestFitRowCount = list.Count;
        }
        private void LoadPhamviThuTuc()
        {
            m_phamvi_thutuc_lk.Properties.DataSource = All.dm_loaithutuc_loaicapphep;
            m_phamvi_thutuc_lk.Properties.DisplayMember = "Description";
            m_phamvi_thutuc_lk.Properties.ValueMember = "ID";
            m_phamvi_thutuc_lk.Properties.BestFitRowCount = All.dm_loaithutuc_loaicapphep.Count;
            m_phamvi_thutuc_lk.Refresh();
            L_loaithutuc_lk.DataSource = All.dm_loaithutuc_loaicapphep;
            L_loaithutuc_lk.DisplayMember = "Description";
            L_loaithutuc_lk.ValueMember = "ID";
            L_loaithutuc_lk.BestFitRowCount = All.dm_loaithutuc_loaicapphep.Count;

        }
        private void LoadLoaiThutuc_Trinhduyet()
        {
            List<DM_LoaiThutucTrinhduyet> list = Helpers.LoaiThutucTrinhDuyet.GetList();
            m_loai_thutuc_lk.Properties.DataSource = list;
            m_loai_thutuc_lk.Properties.DisplayMember = "DM014203";
            m_loai_thutuc_lk.Properties.ValueMember = "DM014201";
            m_loai_thutuc_lk.Properties.BestFitRowCount = list == null ? 0 : list.Count;
            m_loai_thutuc_lk.Refresh();

        }
        private void SetDetailFormEnable(bool isEnable)
        {
            m_donvi_sdct_lk.ReadOnly = !isEnable;
            m_loai_thutuc_lk.ReadOnly = !isEnable;
            m_maform_txt.ReadOnly = !isEnable;
            m_ten_form_txt.ReadOnly = !isEnable;
            m_phamvi_thutuc_lk.ReadOnly = !isEnable;
            m_quytrinh_thamdinh_chk.ReadOnly = !isEnable;
            m_loai_thutuc_lk.ReadOnly = !isEnable;

            uC_Help1.Enabled = !isEnable;
            //groupControl1.Enabled = !isEnable;

            if (isEnable)
                m_donvi_sdct_lk.Focus();
        }
        private void BindControlEvents()
        {
            //gridColumn10.ColumnEdit.EditValueChanged += ColumnEdit_EditValueChanged;

            uC_MenuBtn1.btn_them.Click += btn_them_Click;
            uC_MenuBtn1.btn_thoat.Click += btn_thoat_Click;
            uC_MenuBtn1.btn_boqua.Click += btn_boqua_Click;
            uC_MenuBtn1.btn_capnhat.Click += btn_capnhat_Click;
            uC_MenuBtn1.btn_sua.Click += btn_sua_Click;
            uC_MenuBtn1.btn_xoa.Click += btn_xoa_Click;
            uC_Help1.btn_main.Click += btn_huongdan_Click;
        }
        void btn_them_Click(object sender, EventArgs e)
        {
            currentState = "NEW";
            uC_MenuBtn1.set_status_menu(currentState, 0);
            AssignDetailFormValue(null);
            SetDetailFormEnable(true);
            set_enable_groupControl("M");
        }
        void btn_thoat_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        void btn_boqua_Click(object sender, EventArgs e)
        {
            currentState = "NORMAL";
            uC_MenuBtn1.set_status_menu(currentState, currentList == null ? 0 : currentList.Count);
            AssignDetailFormValue(currentDataSelected);
            SetDetailFormEnable(false);
            set_enable_groupControl("A");
        }
        void btn_capnhat_Click(object sender, EventArgs e)
        {
            if (currentState == "NORMAL") return;

            if (!ValidateDetailForm()) return;

            DM_LoaiThutucNhiemvu obj = PrepareDetail();

            //if (obj == null)
            //{
            //    All.Show_message("Có lỗi trong quá trình cập nhật dữ liệu!");
            //    return;
            //}

            if (currentState == "NEW")
            {
                APIResponseData result = Helpers.ThutucNhiemvu.Create(obj);
                if (result == null)
                {
                    All.Show_message("Có lỗi trong quá trình cập nhật dữ liệu!");
                }
                else if (result.ErrorCode == 0)
                {
                    All.Show_message("Thêm mới thành công Loại thủ tục nhiệm vụ: " + obj.DM016003);
                    currentState = "NORMAL";
                    //LoadList();
                    LoadList_Master();

                }
                else
                {
                    All.Show_message("Error code " + result.ErrorCode + ": " + result.Message);
                }
            }
            else
            {
                APIResponseData result = Helpers.ThutucNhiemvu.Update(obj);
                if (result == null)
                {
                    All.Show_message("Có lỗi trong quá trình cập nhật dữ liệu!");
                }
                else if (result.ErrorCode == 0)
                {
                    All.Show_message("Cập nhật thành công Loại thủ tục nhiệm vụ: " + obj.DM016003);
                    currentState = "NORMAL";
                    LoadList_Master();
                }
                else
                {
                    All.Show_message("Error code " + result.ErrorCode + ": " + result.Message);
                    if (result.ErrorCode == -1)
                        LoadList_Master();
                }
            }
            SetDetailFormEnable(true);
            uC_MenuBtn1.set_status_menu(currentState, currentList == null ? 0 : currentList.Count);
            set_enable_groupControl("A");

        }
        void btn_sua_Click(object sender, EventArgs e)
        {
            currentState = "EDIT";
            uC_MenuBtn1.set_status_menu(currentState, currentList == null ? 0 : currentList.Count);
            AssignDetailFormValue(currentDataSelected);
            SetDetailFormEnable(true);
            set_enable_groupControl("M");
        }
        void btn_xoa_Click(object sender, EventArgs e)
        {
            //List<Guid> listChecked = GetIDChecked();
            //if (listChecked == null)
            //{
            //    All.Show_message("Vui lòng chọn trước khi xóa!");
            //    return;
            //}

            if (MessageBox.Show("Bạn chắc chắn muốn xóa Form :\"" + currentDataSelected.DM016003 + "\" ?", "Cảnh báo!", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                APIResponseData result = Helpers.ThutucNhiemvu.Delete(new List<Guid>() { currentDataSelected.DM016001 });
                if (result == null)
                {
                    All.Show_message("Có lỗi trong quá trình cập nhật dữ liệu!");
                }
                else if (result.ErrorCode == 0)
                {
                    All.Show_message("Xóa thành công !");
                    currentState = "NORMAL";
                    LoadList_Master();
                }
                else
                {
                    All.Show_message("Error code " + result.ErrorCode + ": " + result.Message);
                }
            }
        }
        private void AssignDetailFormValue(DM_LoaiThutucNhiemvu data)
        {
            m_donvi_sdct_lk.EditValue = data == null ? Guid.Empty : data.DM016002;
            m_phamvi_thutuc_lk.EditValue = data == null ? ' ' : data.DM016005;
            m_maform_txt.Text = data == null ? string.Empty : data.DM016003;
            m_ten_form_txt.Text = data == null ? string.Empty : data.DM016004;
            m_quytrinh_thamdinh_chk.Checked = data == null ? false : data.DM016010 == '1';
            m_loai_thutuc_lk.EditValue = data == null ? Guid.Empty : data.DM016011;
            currentListFields = data == null ? null : data.FieldSelecteds;
            dsCothienthi = data == null ? null : string.IsNullOrEmpty(data.DM016012) ? null : JsonConvert.DeserializeObject<List<TD_ThuchienNhiemvu_Cothienthi>>(data.DM016012);
        }
        private void set_enable_groupControl(string status)
        {
            switch (status)
            {
                case "M":
                    groupControl1.Enabled = false;
                    groupControl2.Enabled = false;
                    groupControl3.Enabled = false;
                    uC_MenuBtn1.Enabled = true;
                    break;
                case "D1":
                    groupControl1.Enabled = false;
                    groupControl2.Enabled = true;
                    groupControl3.Enabled = false;
                    uC_MenuBtn1.Enabled = false;
                    uC_Help1.Enabled = false;
                    break;
                case "D2":
                    groupControl1.Enabled = false;
                    groupControl2.Enabled = false;
                    groupControl3.Enabled = true;
                    uC_MenuBtn1.Enabled = false;
                    uC_Help1.Enabled = false;
                    break;
                default:
                    groupControl1.Enabled = true;
                    groupControl2.Enabled = true;
                    groupControl3.Enabled = true;
                    break;

            }
        }
        private bool ValidateDetailForm()
        {
            if (m_donvi_sdct_lk.EditValue.ToString() == Guid.Empty.ToString())
            {
                All.Show_message("Vui lòng chọn Đơn vị sử dụng chương trình!");
                m_donvi_sdct_lk.Focus();
                return false;
            }

            if (m_phamvi_thutuc_lk.EditValue.ToString().Trim() == string.Empty)
            {
                All.Show_message("Vui lòng chọn phạm vi sử dụng thủ tục!");
                m_phamvi_thutuc_lk.Focus();
                return false;
            }
            if (m_loai_thutuc_lk.EditValue.ToString().Trim() == Guid.Empty.ToString())
            {
                All.Show_message("Vui lòng chọn Loại thủ tục!");
                m_loai_thutuc_lk.Focus();
                return false;
            }
            if (m_maform_txt.Text.Trim() == string.Empty)
            {
                All.Show_message("Vui lòng nhập Mã form!");
                m_maform_txt.Focus();
                return false;
            }

            if (m_ten_form_txt.Text.Trim() == string.Empty)
            {
                All.Show_message("Vui lòng nhập Tên form!");
                m_ten_form_txt.Focus();
                return false;
            }

            //if (checkEdit1.Checked && currentListFields == null)
            //{
            //    All.Show_message("Vui lòng nhập Chọn trường dữ liệu thẩm định!");
            //    ShowChildForm();
            //    return false;

            //}

            return true;
        }
        private DM_LoaiThutucNhiemvu PrepareDetail()
        {
            try
            {
                DM_LoaiThutucNhiemvu obj = new DM_LoaiThutucNhiemvu();
                obj.DM016001 = currentState == "NEW" ? Guid.NewGuid() : currentDataSelected.DM016001;
                obj.DM016002 = Guid.Parse(m_donvi_sdct_lk.EditValue.ToString());
                obj.DM016003 = m_maform_txt.Text;
                obj.DM016004 = m_ten_form_txt.Text;
                obj.DM016005 = char.Parse(m_phamvi_thutuc_lk.EditValue.ToString());
                obj.DM016006 = currentState == "NEW" ? All.gs_user_id : currentDataSelected.DM016006;
                obj.DM016007 = currentState == "NEW" ? DateTime.Now : currentDataSelected.DM016007;
                obj.DM016008 = All.gs_user_id;
                obj.DM016009 = DateTime.Now;
                obj.DM016010 = m_quytrinh_thamdinh_chk.Checked ? '1' : '0';
                obj.DM016011 = Guid.Parse(m_loai_thutuc_lk.EditValue.ToString());
                obj.DM016012 = JsonConvert.SerializeObject(dsCothienthi);
                obj.FieldSelecteds = currentListFields;

                return obj;
            }
            catch (Exception ex)
            {
                Log.write(ex);
                return null;
            }
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            currentRowSelected = e.FocusedRowHandle;
            currentDataSelected = (DM_LoaiThutucNhiemvu)gridView1.GetRow(currentRowSelected);
            //dsCothienthi = string.IsNullOrEmpty(currentDataSelected.DM016012) ? null : JsonConvert.DeserializeObject<List<TD_ThuchienNhiemvu_Cothienthi>>(currentDataSelected.DM016012);
            //if (dsCothienthi == null)
            //    dsCothienthi = Helpers.TrinhduyetThuchienNhiemvu.GenerateListColumns();

            AssignDetailFormValue(currentDataSelected);
            load_detail_1();
        }

        private void uC_Help1_Load(object sender, EventArgs e)
        {

        }

        private void uC_Help1_Click(object sender, EventArgs e)
        {

            //FRM_DM_LoaiThutucNhiemvu_Huongdan frm = new FRM_DM_LoaiThutucNhiemvu_Huongdan();
            //frm.Show();
        }
        public void btn_huongdan_Click(object sender, EventArgs e)
        {
            FRM_TD_Huongdan frm = new FRM_TD_Huongdan();
            frm.currentList = currentDataSelected.DsHuongdan;
            frm.Show();
            frm.Focus();
        }
        #region detail_1
        private void config_detail()
        {
            d1_cachnhap_lk.DataSource = All.dm_loaithutuc_noidung_cachnhap;
            d1_cachnhap_lk.DisplayMember = "Description";
            d1_cachnhap_lk.ValueMember = "ID";
            d1_cachnhap_lk.BestFitRowCount = All.dm_loaithutuc_noidung_cachnhap.Count;
            gridView2.OptionsBehavior.Editable = false;

            ledTruongdulieu_Kieutruong.DataSource = All.dm_loaithutuc_truongdulieu_kieutruong;
            ledTruongdulieu_Kieutruong.DisplayMember = "Description";
            ledTruongdulieu_Kieutruong.ValueMember = "ID";
            ledTruongdulieu_Kieutruong.BestFitRowCount = All.dm_loaithutuc_truongdulieu_kieutruong.Count;
            gridView3.OptionsBehavior.Editable = false;
        }
        private void load_detail_1()
        {
            if (currentDataSelected == null) return;
            currentNoidungs = Helpers.ThutucNhiemvu_Noidung.GetList(currentDataSelected.DM016001);
            currentDataTable = UF_Function.ToDataTable(currentNoidungs);
            gridControl2.DataSource = currentDataTable;
            gridView2.OptionsDetail.ShowDetailTabs = false;
        }

        private void load_list_3()
        {
            currentTruongdulieus = Helpers.ThutucNhiemvu_Truongdulieu.GetList_Root(currentNoidungId);
            DataTable dt = UF_Function.ToDataTable(currentTruongdulieus);
            gridControl3.DataSource = dt;
            gridView3.BestFitColumns();
            gridView3.OptionsDetail.ShowDetailTabs = false;
        }

        private void MenuItem_them_d1_Click(object sender, EventArgs e)
        {
            if (_status_detail_1 == "NORMAL")
            {
                set_enable_groupControl("D1");
                add_newDetail_D1();
            }

        }
        private void MenuItem_sua_d1_Click(object sender, EventArgs e)
        {
            if (_status_detail_1 == "NORMAL")
            {
                set_enable_groupControl("D1");

                currentRowSelected_D1 = gridView2.RowCount - 1;
                _status_detail_1 = "EDIT";
                gridView2.ShowInplaceEditForm();
                gridView2.OptionsBehavior.Editable = true;
                gridView2.OptionsBehavior.ReadOnly = false;
                gridView2.Focus();
            }
        }
        private void MenuItem_xoa_d1_Click(object sender, EventArgs e)
        {
            if (_status_detail_1 == "NEW")
            {
                gridView2.DeleteRow(gridView2.FocusedRowHandle);
                _status_detail_1 = "NORMAL";
                gridView2.OptionsBehavior.Editable = false;
            }
            else
            {
                Guid id_delete = Guid.Parse(gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "DM016101").ToString());
                if (MessageBox.Show("Bạn chắc chắn muốn xóa Nội dung đang chọn?", "Cảnh báo!", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    APIResponseData result = Helpers.ThutucNhiemvu_Noidung.Delete(new List<Guid>() { id_delete });
                    if (result == null)
                    {
                        All.Show_message("Có lỗi trong quá trình cập nhật dữ liệu!");
                    }
                    else if (result.ErrorCode == 0)
                    {
                        _status_detail_1 = "NORMAL";
                        gridView2.DeleteRow(gridView2.FocusedRowHandle);
                        gridView2.OptionsBehavior.Editable = false;
                        All.Show_message("Xóa thành công Nội dung đã chọn!");
                        load_detail_1();
                    }
                    else
                    {
                        All.Show_message("Error code " + result.ErrorCode + ": " + result.Message);
                    }
                }
            }
        }
        #endregion
        private void add_newDetail_D1()
        {
            if (_status_detail_1 == "NORMAL")
            {
                gridView2.AddNewRow();
                currentRowSelected_D1 = gridView2.RowCount - 1;
                _status_detail_1 = "NEW";
                gridView2.ShowInplaceEditForm();
                gridView2.OptionsBehavior.Editable = true;
                gridView2.OptionsBehavior.ReadOnly = false;
                gridView2.Focus();
            }
        }
        private void gridView2_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            GridView view = sender as GridView;
            if (_status_detail_1 == "NORMAL") return;
            int rowselect = e.RowHandle;
            string ma_noidung = view.GetRowCellValue(rowselect, "DM016103").ToString();
            string ten_noidung = view.GetRowCellValue(rowselect, "DM016104").ToString();
            string cachnhap = view.GetRowCellValue(rowselect, "DM016105").ToString();
            if ((ma_noidung + ten_noidung + cachnhap).Length == 0)
            {
                _status_detail_1 = "NORMAL";
                gridView2.DeleteRow(gridView2.FocusedRowHandle);
                gridView2.OptionsBehavior.Editable = false;
                return;
            }
            if (ma_noidung.Length == 0)
            {
                All.Show_message("Bạn phải nhập Mã nội dung!");
                return;
            }
            if (ten_noidung.Length == 0)
            {
                All.Show_message("Bạn phải nhập Tên nội dung!");
                return;
            }
            if (cachnhap.Length == 0)
            {
                All.Show_message("Bạn phải chọn cách nhập!");
                return;
            }
            DM_LoaiThutucNhiemvu_Noidung obj = PrepareD1(view);
            GridColumn col1 = view.Columns[1];
            if (obj == null)
            {
                All.Show_message("Có lỗi trong quá trình cập nhật dữ liệu!");
                set_enable_groupControl(currentState);
                gridView2.OptionsBehavior.Editable = false;
                gridView2.OptionsBehavior.ReadOnly = true;
                return;
            }

            if (_status_detail_1 == "NEW")
            {
                APIResponseData result = Helpers.ThutucNhiemvu_Noidung.Create(obj);
                if (result == null)
                {
                    All.Show_message("Có lỗi trong quá trình cập nhật dữ liệu!");
                }
                else if (result.ErrorCode == 0)
                {
                    All.Show_message("Thêm mới thành công Nội dung: " + obj.DM016103);
                    _status_detail_1 = "NORMAL";
                    load_detail_1();
                }
                else
                {
                    All.Show_message("Error code " + result.ErrorCode + ": " + result.Message);
                }
            }
            else
            {
                APIResponseData result = Helpers.ThutucNhiemvu_Noidung.Update(obj);
                if (result == null)
                {
                    All.Show_message("Có lỗi trong quá trình cập nhật dữ liệu!");
                }
                else if (result.ErrorCode == 0)
                {
                    All.Show_message("Cập nhật thành công Nội dung: " + obj.DM016103);
                    _status_detail_1 = "NORMAL";
                    load_detail_1();
                }
                else
                {
                    All.Show_message("Error code " + result.ErrorCode + ": " + result.Message);
                    //if (result.ErrorCode == -1)
                    // LoadList();
                }
            }

            set_enable_groupControl(currentState);
            gridView2.OptionsBehavior.Editable = false;
            gridView2.OptionsBehavior.ReadOnly = true;
        }
        private DM_LoaiThutucNhiemvu_Noidung PrepareD1(GridView view)
        {
            try
            {
                int rowselect = view.FocusedRowHandle;
                DM_LoaiThutucNhiemvu_Noidung obj = new DM_LoaiThutucNhiemvu_Noidung();
                obj.DM016101 = _status_detail_1 == "NEW" ? Guid.NewGuid() : Guid.Parse(view.GetRowCellValue(rowselect, "DM016101").ToString());
                obj.DM016102 = currentDataSelected.DM016001;
                obj.DM016103 = view.GetRowCellValue(rowselect, "DM016103").ToString();
                obj.DM016104 = view.GetRowCellValue(rowselect, "DM016104").ToString();
                obj.DM016105 = (char)view.GetRowCellValue(rowselect, "DM016105");
                obj.DM016106 = All.gs_user_id;
                obj.DM016107 = DateTime.Now;
                obj.DM016108 = All.gs_user_id;
                obj.DM016109 = DateTime.Now;
                obj.DM016110 = string.Empty;
                obj.FieldSelecteds = currentNoidungTruongdulieus;

                return obj;
            }
            catch (Exception ex)
            {
                Log.write(ex);
                return null;
            }
        }

        private DM_LoaiThutucNhiemvu_Truongdulieu PrepareD2(GridView view)
        {
            try
            {
                int rowselect = view.FocusedRowHandle;
                DM_LoaiThutucNhiemvu_Truongdulieu obj = new DM_LoaiThutucNhiemvu_Truongdulieu();
                obj.DM016201 = _status_detail_2 == "NEW" ? Guid.NewGuid() : Guid.Parse(view.GetRowCellValue(rowselect, "DM016201").ToString());
                obj.DM016204 = view.GetRowCellValue(rowselect, "DM016204").ToString();
                obj.DM016205 = view.GetRowCellValue(rowselect, "DM016205").ToString();
                obj.DM016206 = view.GetRowCellValue(rowselect, "DM016206").ToString();
                obj.DM016207 = view.GetRowCellValue(rowselect, "DM016207").ToString().Trim();
                obj.DM016208 = int.Parse(view.GetRowCellValue(rowselect, "DM016208").ToString());
                obj.DM016209 = string.Empty;
                obj.DM016210 = obj.DM016207 == "8" ? JsonConvert.SerializeObject(currentTruongdulieu_Lookupdata) : string.Empty;
                obj.DM016213 = view.GetRowCellValue(rowselect, "DM016213").ToString();
                obj.DM016214 = int.Parse(view.GetRowCellValue(rowselect, "DM016214").ToString());
                char batbuocnhap = view.GetRowCellValue(rowselect, "DM016215") == DBNull.Value ? '0' : char.Parse(view.GetRowCellValue(rowselect, "DM016215").ToString().Trim());
                obj.DM016215 = batbuocnhap;
                obj.DM016217 = All.gs_user_id;
                obj.DM016218 = DateTime.Now;
                obj.DM016219 = All.gs_user_id;
                obj.DM016220 = DateTime.Now;
                obj.DM016216 = view.GetRowCellValue(rowselect, "DM016216") == DBNull.Value ? Guid.Empty : Guid.Parse(view.GetRowCellValue(rowselect, "DM016216").ToString());
                obj.NoidungId = currentNoidungId;

                return obj;
            }
            catch (Exception ex)
            {
                Log.write(ex);
                return null;
            }
        }

        private void gridView2_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            currentRowSelected_D1 = e.FocusedRowHandle;
            if (currentRowSelected_D1 < 0)
            {
                currentNoidungTruongdulieus = null;
                return;
            }

            currentNoidungId = (Guid)gridView2.GetFocusedDataRow().ItemArray[0];
            if (gridView2.GetFocusedDataRow().ItemArray[15] == DBNull.Value)
                currentTruongdulieus = null;
            else
                currentTruongdulieus = (List<DM_LoaiThutucNhiemvu_Truongdulieu>)gridView2.GetFocusedDataRow().ItemArray[16];

            load_list_3();
            //}
        }

        private void gridView2_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            //MessageBox.Show("loi");
        }

        private void MenuItem_them_d2_Click(object sender, EventArgs e)
        {
            if (_status_detail_2 == "NORMAL")
            {
                DM_LoaiThutucNhiemvu_Noidung noidung = currentNoidungs == null ? null : currentNoidungs.FirstOrDefault(o => o.DM016101 == currentNoidungId);
                if (noidung == null || noidung.DM016105 != '2') return;

                set_enable_groupControl("D2");
                add_newDetail_D2();
            }
        }

        private void add_newDetail_D2()
        {
            if (_status_detail_2 == "NORMAL")
            {
                gridView3.AddNewRow();
                //currentRowSelected_D1 = gridView2.RowCount - 1;
                _status_detail_2 = "NEW";
                gridView3.ShowInplaceEditForm();
                gridView3.OptionsBehavior.Editable = true;
                gridView3.OptionsBehavior.ReadOnly = false;
                gridView3.Focus();
            }
        }

        private void MenuItem_sua_d2_Click(object sender, EventArgs e)
        {
            if (_status_detail_2 == "NORMAL")
            {
                DM_LoaiThutucNhiemvu_Noidung noidung = currentNoidungs == null ? null : currentNoidungs.FirstOrDefault(o => o.DM016101 == currentNoidungId);
                if (noidung == null || noidung.DM016105 != '2') return;

                set_enable_groupControl("D2");

                //currentRowSelected_D1 = gridView2.RowCount - 1;
                _status_detail_2 = "EDIT";
                gridView3.ShowInplaceEditForm();
                gridView3.OptionsBehavior.Editable = true;
                gridView3.OptionsBehavior.ReadOnly = false;
                gridView3.Focus();
            }
        }

        private void MenuItem_xoa_d2_Click(object sender, EventArgs e)
        {
            if (_status_detail_2 == "NEW")
            {
                gridView3.DeleteRow(gridView3.FocusedRowHandle);
                _status_detail_2 = "NORMAL";
                gridView3.OptionsBehavior.Editable = false;
            }
            else
            {
                DM_LoaiThutucNhiemvu_Noidung noidung = currentNoidungs == null ? null : currentNoidungs.FirstOrDefault(o => o.DM016101 == currentNoidungId);
                if (noidung == null || noidung.DM016105 != '2') return;

                if (gridView3.FocusedRowHandle < 0) return;

                Guid id_delete = Guid.Parse(gridView3.GetRowCellValue(gridView3.FocusedRowHandle, "DM016201").ToString());
                if (MessageBox.Show("Bạn chắc chắn muốn xóa Trường dữ liệu đang chọn?", "Cảnh báo!", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    APIResponseData result = Helpers.ThutucNhiemvu_Truongdulieu.Delete(new List<Guid>() { id_delete });
                    if (result == null)
                    {
                        All.Show_message("Có lỗi trong quá trình cập nhật dữ liệu!");
                    }
                    else if (result.ErrorCode == 0)
                    {
                        _status_detail_2 = "NORMAL";
                        gridView3.DeleteRow(gridView3.FocusedRowHandle);
                        gridView3.OptionsBehavior.Editable = false;
                        All.Show_message("Xóa thành công Trường dữ liệu đã chọn!");
                        //LoadList();
                    }
                    else
                    {
                        All.Show_message("Error code " + result.ErrorCode + ": " + result.Message);
                    }
                }
            }
        }

        void btn_truongdulieu_kieutruong_Click(object sender, EventArgs e)
        {
            DataRow data = gridView3.GetFocusedDataRow();
            //Guid id = Guid.Parse(data.ItemArray[0].ToString());
            string kieutruong = data.ItemArray[4].ToString().Trim();

            if (kieutruong == "8")
            {
                needUpdate = true;
                ShowChildForm_Lookup();
            }
            //else if (kieutruong == "9")
            //{
            //    needUpdate = true;
            //    ShowChildForm_Tab();
            //}
        }

        void btn_truongdulieu_truongcon_Click(object sender, EventArgs e)
        {
            if (_status_detail_2 != "EDIT") return;

            int rowselect = gridView3.FocusedRowHandle;
            string kieutruong = gridView3.GetRowCellValue(rowselect, "DM016207").ToString();
            if (kieutruong != "9") return;

            List<DM_LoaiThutucNhiemvu_Truongdulieu> list = gridView3.GetRowCellValue(rowselect, "DsTruongcon") == DBNull.Value ? null : (List<DM_LoaiThutucNhiemvu_Truongdulieu>)gridView3.GetRowCellValue(rowselect, "DsTruongcon");

            FRM_DM_ThuTuc_NhiemVu_Truongcon frm = new FRM_DM_ThuTuc_NhiemVu_Truongcon();
            frm.currentNoidung = currentNoidungs.FirstOrDefault(o => o.DM016101 == currentNoidungId);
            frm.currentNoidungId = currentNoidungId;
            frm.currentList = list;
            frm.Show();
            this.Enabled = false;
        }
        public void CallBack_UpdateTruongcon(bool update)
        {
            this.Enabled = true;
            this.Focus();

            if (update)
            {
                load_list_3();

                _status_detail_2 = "NORMAL";
                set_enable_groupControl(_status_detail_2);
                gridView3.OptionsBehavior.Editable = false;
                gridView3.OptionsBehavior.ReadOnly = true;
            }
        }

        private void ShowChildForm_Lookup()
        {
            this.Enabled = false;
            FRM_DM_LoaiThutucNhiemvu_ChitietTruongdulieu_Lookup frm = new FRM_DM_LoaiThutucNhiemvu_ChitietTruongdulieu_Lookup();
            frm.currentData = currentTruongdulieu_Lookupdata;
            frm.currentState = _status_detail_2;
            frm.Show();
            frm.Focus();
        }
        public void CallBack_UpdateLookupData(bool update)
        {
            this.Enabled = true;
            this.Focus();
        }

        private void gridView3_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            currentRowSelected_D2 = e.FocusedRowHandle;
            currentTruongdulieu_Lookupdata = null;

            if (currentRowSelected_D2 < 0)
            {
                return;
            }

            string kieutruong = gridView3.GetFocusedDataRow().ItemArray[4].ToString().Trim();
            string data = gridView3.GetFocusedDataRow().ItemArray[7].ToString().Trim();

            if (string.IsNullOrEmpty(data))
            {
                currentTruongdulieu_Lookupdata = null;
            }
            else
            {
                if (kieutruong == "8")
                    currentTruongdulieu_Lookupdata = JsonConvert.DeserializeObject<DM_LoaiThutucNhiemvu_Truongdulieu_LookupData>(data);
                //else if (kieutruong == "9")
                //    currentTruongdulieu_Children = JsonConvert.DeserializeObject<List<Guid>>(data);
            }
        }

        private void gridView3_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            GridView view = sender as GridView;
            if (_status_detail_2 == "NORMAL") return;

            int rowselect = e.RowHandle;
            string maso = view.GetRowCellValue(rowselect, "DM016204").ToString();
            string ten = view.GetRowCellValue(rowselect, "DM016205").ToString();
            string tenhienthi = view.GetRowCellValue(rowselect, "DM016206").ToString();
            string kieutruong = view.GetRowCellValue(rowselect, "DM016207").ToString();
            string dorong = view.GetRowCellValue(rowselect, "DM016208").ToString();
            string congthuctinh = view.GetRowCellValue(rowselect, "DM016213").ToString();
            string sapxep = view.GetRowCellValue(rowselect, "DM016214").ToString();

            if ((maso + ten + tenhienthi + kieutruong + dorong + congthuctinh + sapxep).Length == 0)
            {
                _status_detail_2 = "NORMAL";
                gridView3.DeleteRow(gridView3.FocusedRowHandle);
                gridView3.OptionsBehavior.Editable = false;
                return;
            }

            if (maso.Length == 0)
            {
                All.Show_message("Bạn phải nhập Mã số!");
                return;
            }
            if (ten.Length == 0)
            {
                All.Show_message("Bạn phải nhập Tên trường!");
                return;
            }
            if (tenhienthi.Length == 0)
            {
                All.Show_message("Bạn phải Tên hiển thị!");
                return;
            }
            if (kieutruong.Length == 0)
            {
                All.Show_message("Bạn phải chọn Kiểu trường!");
                return;
            }
            if (dorong.Length == 0)
            {
                All.Show_message("Bạn phải nhập Độ rộng!");
                return;
            }
            if (congthuctinh.Length == 0)
            {
                All.Show_message("Bạn phải nhập Công thức tính!");
                return;
            }
            if (sapxep.Length == 0)
            {
                All.Show_message("Bạn phải nhập Sắp xếp!");
                return;
            }

            DM_LoaiThutucNhiemvu_Truongdulieu obj = PrepareD2(view);
            GridColumn col1 = view.Columns[1];
            if (obj == null)
            {
                All.Show_message("Có lỗi trong quá trình cập nhật dữ liệu!");
                set_enable_groupControl(_status_detail_2);
                gridView3.OptionsBehavior.Editable = false;
                gridView3.OptionsBehavior.ReadOnly = true;
                return;
            }

            if (_status_detail_2 == "NEW")
            {
                APIResponseData result = Helpers.ThutucNhiemvu_Truongdulieu.Create(obj);
                if (result == null)
                {
                    All.Show_message("Có lỗi trong quá trình cập nhật dữ liệu!");
                }
                else if (result.ErrorCode == 0)
                {
                    All.Show_message("Thêm mới thành công Trường dữ liệu: " + obj.DM016204);
                    _status_detail_2 = "NORMAL";
                    load_list_3();
                }
                else
                {
                    All.Show_message("Error code " + result.ErrorCode + ": " + result.Message);
                }
            }
            else
            {
                APIResponseData result = Helpers.ThutucNhiemvu_Truongdulieu.Update(obj);
                if (result == null)
                {
                    All.Show_message("Có lỗi trong quá trình cập nhật dữ liệu!");
                }
                else if (result.ErrorCode == 0)
                {
                    All.Show_message("Cập nhật thành công Trường dữ liệu: " + obj.DM016204);
                    _status_detail_2 = "NORMAL";
                    load_list_3();
                }
                else
                {
                    All.Show_message("Error code " + result.ErrorCode + ": " + result.Message);
                }
            }

            set_enable_groupControl(_status_detail_2);
            gridView3.OptionsBehavior.Editable = false;
            gridView3.OptionsBehavior.ReadOnly = true;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            FRM_DM_ThuTuc_NhiemVu_Cothienthi frm = new FRM_DM_ThuTuc_NhiemVu_Cothienthi();

            if (dsCothienthi == null)
                dsCothienthi = Helpers.TrinhduyetThuchienNhiemvu.GenerateListColumns();
            frm.currentList = dsCothienthi;

            frm.currentState = currentState;
            frm.Show();
            frm.Focus();
        }
    }

}
