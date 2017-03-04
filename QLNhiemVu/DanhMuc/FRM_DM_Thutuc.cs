using DBAccess;
using DevExpress.XtraGrid.Views.Grid;
using Newtonsoft.Json;
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
    public partial class FRM_DM_Thutuc : Form
    {
        public List<DM_LoaiThutucNhiemvu> currentList_LoaiThutucNhiemvu = null;
        public DM_LoaiThutucNhiemvu currentSelected_LoaiThutucNhiemvu = null;
        public string currentState_LoaiThutucNhiemvu = "NORMAL";
        public static List<Guid> currentSelected_LoaiThutucNhiemvu_Truongdulieu = null;
        public List<TD_ThuchienNhiemvu_Cothienthi> currentSelected_LoaiThutucNhiemvu_Cothienthi = null;

        public List<DM_LoaiThutucNhiemvu_Noidung> currentList_Noidung = null;
        public DataTable currentDataTable_Noidung = null;
        public DM_LoaiThutucNhiemvu_Noidung currentSelected_Noidung = null;
        public string currentState_Noidung = "NORMAL";
        public Guid refNoidungID = Guid.Empty;
        public Guid refFromNoidungID = Guid.Empty;

        public List<DM_LoaiThutucNhiemvu_Truongdulieu> currentList_Truongdulieu = null;
        public DataTable currentDataTable_Truongdulieu = null;
        public DM_LoaiThutucNhiemvu_Truongdulieu currentSelected_Truongdulieu = null;
        public string currentState_Truongdulieu = "NORMAL";
        public DM_LoaiThutucNhiemvu_Truongdulieu_LookupData currentSelected_Truongdulieu_LookupData = null;
        public Guid refTruongdulieuID = Guid.Empty;
        public bool needUpdate_Truongdulieu_Lookupdata = false;

        public int ControlEffectWithMouse = 0;

        public FRM_DM_Thutuc()
        {
            InitializeComponent();
        }

        private void FRM_DM_Thutuc_Load(object sender, EventArgs e)
        {
            lblHeadTitle1.setText("Danh mục thủ tục nhiệm vụ");
            lblHeadTitle1.alignCenter(lblHeadTitle1.Parent);
            panelHeader1.alignCenter(panelHeader1.Parent);
            gridView1.OptionsBehavior.Editable = false;

            BindControlsEvents();

            LoadDonvi();
            LoadPhamviThuTuc();
            LoadLoaiThutuc_Trinhduyet();

            SetDetailFormEnable(false);

            LoadList_LoaiThutucNhiemvu();
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

            if (isEnable)
                m_donvi_sdct_lk.Focus();
        }

        private void BindControlsEvents()
        {
            uC_Find1.btn_tim.Click += btn_tim_Click;
            uC_Help1.btn_main.Click += btn_main_Click;

            uC_MenuBtn1.btn_them.Click += btn_them_Click;
            uC_MenuBtn1.btn_thoat.Click += btn_thoat_Click;
            uC_MenuBtn1.btn_boqua.Click += btn_boqua_Click;
            uC_MenuBtn1.btn_capnhat.Click += btn_capnhat_Click;
            uC_MenuBtn1.btn_sua.Click += btn_sua_Click;
            uC_MenuBtn1.btn_xoa.Click += btn_xoa_Click;

            MenuItem_them_d1.Image = imageCollection1.Images[0];
            MenuItem_sua_d1.Image = imageCollection1.Images[1];
            MenuItem_xoa_d1.Image = imageCollection1.Images[2];
            MenuItem_saochep_d1.Image = imageCollection1.Images[3];

            d1_cachnhap_lk.DataSource = All.dm_loaithutuc_noidung_cachnhap;
            d1_cachnhap_lk.DisplayMember = "Description";
            d1_cachnhap_lk.ValueMember = "ID";
            d1_cachnhap_lk.BestFitRowCount = All.dm_loaithutuc_noidung_cachnhap.Count;

            ledTruongdulieu_Kieutruong.DataSource = All.dm_loaithutuc_truongdulieu_kieutruong;
            ledTruongdulieu_Kieutruong.DisplayMember = "Description";
            ledTruongdulieu_Kieutruong.ValueMember = "ID";
            ledTruongdulieu_Kieutruong.BestFitRowCount = All.dm_loaithutuc_truongdulieu_kieutruong.Count;

            btn_truongdulieu_kieutruong.Click += btn_truongdulieu_kieutruong_Click;
            btn_truongdulieu_truongcon.Click += btn_truongdulieu_truongcon_Click;
        }

        void btn_truongdulieu_truongcon_Click(object sender, EventArgs e)
        {
            if (currentState_Truongdulieu != "EDIT") return;

            string kieutruong = gridView3.GetFocusedRowCellValue("DM016207").ToString();
            if (kieutruong != "9") return;

            FRM_DM_ThuTuc_NhiemVu_Truongcon frm = new FRM_DM_ThuTuc_NhiemVu_Truongcon();
            frm.currentState = currentState_Truongdulieu;
            frm.currentNoidung = currentSelected_Noidung;
            frm.truongchaId = (Guid)gridView3.GetFocusedRowCellValue("DM016201");
            frm.Show();
            this.Enabled = false;
        }
        public void CallBack_UpdateTruongcon(bool update)
        {
            this.Enabled = true;
            this.Focus();

            if (update)
            {
                LoadList_Truongdulieu();

                currentState_Truongdulieu = "NORMAL";

                set_enable_groupControl(0);
                gridView3.OptionsBehavior.Editable = false;
                gridView3.OptionsBehavior.ReadOnly = true;
            }
        }

        void btn_truongdulieu_kieutruong_Click(object sender, EventArgs e)
        {
            DataRow data = gridView3.GetFocusedDataRow();
            string kieutruong = data.ItemArray[4].ToString().Trim();

            if (kieutruong == "8")
            {
                needUpdate_Truongdulieu_Lookupdata = true;
                ShowChildForm_Lookup();
            }
        }

        private void ShowChildForm_Lookup()
        {
            this.Enabled = false;
            FRM_DM_LoaiThutucNhiemvu_ChitietTruongdulieu_Lookup frm = new FRM_DM_LoaiThutucNhiemvu_ChitietTruongdulieu_Lookup();
            frm.formType = "1";
            frm.currentData = currentSelected_Truongdulieu_LookupData;
            frm.currentState = currentState_Truongdulieu;
            frm.Show();
            frm.Focus();
        }
        public void CallBack_UpdateLookupData(bool update)
        {
            this.Enabled = true;
            this.Focus();
        }

        void btn_main_Click(object sender, EventArgs e)
        {
            if (currentSelected_LoaiThutucNhiemvu == null) return;

            FRM_DM_LoaiThutucNhiemvu_Huongdan frm = new FRM_DM_LoaiThutucNhiemvu_Huongdan(currentSelected_LoaiThutucNhiemvu.DM016001);
            frm.Show();
            frm.Focus();
        }

        private void AssignDetailFormValue(DM_LoaiThutucNhiemvu data)
        {
            m_donvi_sdct_lk.EditValue = data == null ? Guid.Empty : data.DM016002;
            m_phamvi_thutuc_lk.EditValue = data == null ? ' ' : data.DM016005;
            m_maform_txt.Text = data == null ? string.Empty : data.DM016003;
            m_ten_form_txt.Text = data == null ? string.Empty : data.DM016004;
            m_quytrinh_thamdinh_chk.Checked = data == null ? false : data.DM016010 == '1';
            m_loai_thutuc_lk.EditValue = data == null ? Guid.Empty : data.DM016011;
            //currentListFields = data == null ? null : data.FieldSelecteds;
            //dsCothienthi = data == null ? null : string.IsNullOrEmpty(data.DM016012) ? null : JsonConvert.DeserializeObject<List<TD_ThuchienNhiemvu_Cothienthi>>(data.DM016012);
        }

        private void set_enable_groupControl(int type)
        {
            switch (type)
            {
                case 1:
                    groupControl1.Enabled = false;
                    groupControl2.Enabled = false;
                    groupControl3.Enabled = false;
                    uC_MenuBtn1.Enabled = true;
                    uC_Help1.Enabled = true;
                    break;
                case 2:
                    groupControl1.Enabled = false;
                    groupControl2.Enabled = true;
                    groupControl3.Enabled = false;
                    uC_MenuBtn1.Enabled = false;
                    uC_Help1.Enabled = false;
                    break;
                case 3:
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
                    uC_MenuBtn1.Enabled = true;
                    uC_Help1.Enabled = true;
                    break;

            }
        }

        void btn_them_Click(object sender, EventArgs e)
        {
            currentState_LoaiThutucNhiemvu = "NEW";
            uC_MenuBtn1.set_status_menu(currentState_LoaiThutucNhiemvu, 0);
            AssignDetailFormValue(null);
            SetDetailFormEnable(true);
            set_enable_groupControl(1);
        }
        void btn_thoat_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        void btn_boqua_Click(object sender, EventArgs e)
        {
            currentState_LoaiThutucNhiemvu = "NORMAL";
            uC_MenuBtn1.set_status_menu(currentState_LoaiThutucNhiemvu, currentList_LoaiThutucNhiemvu == null ? 0 : currentList_LoaiThutucNhiemvu.Count);
            AssignDetailFormValue(currentSelected_LoaiThutucNhiemvu);
            SetDetailFormEnable(false);
            set_enable_groupControl(0);
        }
        void btn_capnhat_Click(object sender, EventArgs e)
        {
            if (currentState_LoaiThutucNhiemvu == "NORMAL") return;

            if (!ValidateDetailForm()) return;

            DM_LoaiThutucNhiemvu obj = PrepareDetail_LoaiThutucNhiemvu();

            if (currentState_LoaiThutucNhiemvu == "NEW")
            {
                APIResponseData result = Helpers.ThutucNhiemvu.Create(obj);
                if (result == null)
                {
                    All.Show_message("Có lỗi trong quá trình cập nhật dữ liệu!");
                }
                else if (result.ErrorCode == 0)
                {
                    All.Show_message("Thêm mới thành công Loại thủ tục nhiệm vụ: " + obj.DM016003);
                    currentState_LoaiThutucNhiemvu = "NORMAL";
                    //LoadList();
                    LoadList_LoaiThutucNhiemvu();

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
                    currentState_LoaiThutucNhiemvu = "NORMAL";
                    LoadList_LoaiThutucNhiemvu();
                }
                else
                {
                    All.Show_message("Error code " + result.ErrorCode + ": " + result.Message);
                    if (result.ErrorCode == -1)
                        LoadList_LoaiThutucNhiemvu();
                }
            }

            uC_Help1.btn_main.Enabled = true;
            SetDetailFormEnable(true);
            uC_MenuBtn1.set_status_menu(currentState_LoaiThutucNhiemvu, currentList_LoaiThutucNhiemvu == null ? 0 : currentList_LoaiThutucNhiemvu.Count);
            set_enable_groupControl(0);

        }
        void btn_sua_Click(object sender, EventArgs e)
        {
            currentState_LoaiThutucNhiemvu = "EDIT";
            uC_MenuBtn1.set_status_menu(currentState_LoaiThutucNhiemvu, currentList_LoaiThutucNhiemvu == null ? 0 : currentList_LoaiThutucNhiemvu.Count);
            AssignDetailFormValue(currentSelected_LoaiThutucNhiemvu);
            SetDetailFormEnable(true);
            set_enable_groupControl(1);
        }
        void btn_xoa_Click(object sender, EventArgs e)
        {
            //List<Guid> listChecked = GetIDChecked();
            //if (listChecked == null)
            //{
            //    All.Show_message("Vui lòng chọn trước khi xóa!");
            //    return;
            //}

            if (MessageBox.Show("Bạn chắc chắn muốn xóa Form :\"" + currentSelected_LoaiThutucNhiemvu.DM016003 + "\" ?", "Cảnh báo!", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                APIResponseData result = Helpers.ThutucNhiemvu.Delete(new List<Guid>() { currentSelected_LoaiThutucNhiemvu.DM016001 });
                if (result == null)
                {
                    All.Show_message("Có lỗi trong quá trình cập nhật dữ liệu!");
                }
                else if (result.ErrorCode == 0)
                {
                    All.Show_message("Xóa thành công !");
                    currentState_LoaiThutucNhiemvu = "NORMAL";
                    LoadList_LoaiThutucNhiemvu();
                }
                else
                {
                    All.Show_message("Error code " + result.ErrorCode + ": " + result.Message);
                }
            }
        }

        private DM_LoaiThutucNhiemvu PrepareDetail_LoaiThutucNhiemvu()
        {
            try
            {
                DM_LoaiThutucNhiemvu obj = new DM_LoaiThutucNhiemvu();
                obj.DM016001 = currentState_LoaiThutucNhiemvu == "NEW" ? Guid.NewGuid() : currentSelected_LoaiThutucNhiemvu.DM016001;
                obj.DM016002 = Guid.Parse(m_donvi_sdct_lk.EditValue.ToString());
                obj.DM016003 = m_maform_txt.Text;
                obj.DM016004 = m_ten_form_txt.Text;
                obj.DM016005 = char.Parse(m_phamvi_thutuc_lk.EditValue.ToString());
                obj.DM016006 = currentState_LoaiThutucNhiemvu == "NEW" ? All.gs_user_id : currentSelected_LoaiThutucNhiemvu.DM016006;
                obj.DM016007 = currentState_LoaiThutucNhiemvu == "NEW" ? DateTime.Now : currentSelected_LoaiThutucNhiemvu.DM016007;
                obj.DM016008 = All.gs_user_id;
                obj.DM016009 = DateTime.Now;
                obj.DM016010 = m_quytrinh_thamdinh_chk.Checked ? '1' : '0';
                obj.DM016011 = Guid.Parse(m_loai_thutuc_lk.EditValue.ToString());
                obj.DM016012 = JsonConvert.SerializeObject(currentSelected_LoaiThutucNhiemvu_Cothienthi);
                obj.FieldSelecteds = currentSelected_LoaiThutucNhiemvu_Truongdulieu;

                return obj;
            }
            catch (Exception ex)
            {
                Log.write(ex);
                return null;
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

        void btn_tim_Click(object sender, EventArgs e)
        {
            LoadList_LoaiThutucNhiemvu();
        }

        private void LoadList_LoaiThutucNhiemvu()
        {
            DM_LoaiThutucNhiemvu_Filter filter = new DM_LoaiThutucNhiemvu_Filter();
            filter.Ten = textEdit5.Text.Trim();
            filter.Phamvisudung = lookUpEdit6.EditValue == null ? '0' : char.Parse(lookUpEdit6.EditValue.ToString());
            filter.Loai = lookUpEdit3.EditValue == null ? Guid.Empty : Guid.Parse(lookUpEdit3.EditValue.ToString());

            currentList_LoaiThutucNhiemvu = Helpers.ThutucNhiemvu.GetList(filter);

            gridControl1.DataSource = currentList_LoaiThutucNhiemvu;
            gridControl1.RefreshDataSource();
            gridView1.BestFitColumns();

            uC_MenuBtn1.set_status_menu(currentState_LoaiThutucNhiemvu, currentList_LoaiThutucNhiemvu == null ? 0 : currentList_LoaiThutucNhiemvu.Count);

            currentSelected_LoaiThutucNhiemvu = (DM_LoaiThutucNhiemvu)gridView1.GetFocusedRow();
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

            //L_loaithutuc_lk.DataSource = All.dm_loaithutuc_loaicapphep;
            //L_loaithutuc_lk.DisplayMember = "Description";
            //L_loaithutuc_lk.ValueMember = "ID";
            //L_loaithutuc_lk.BestFitRowCount = All.dm_loaithutuc_loaicapphep.Count;

            lookUpEdit6.Properties.DataSource = All.dm_loaithutuc_loaicapphep;
            lookUpEdit6.Properties.DisplayMember = "Description";
            lookUpEdit6.Properties.ValueMember = "ID";
            lookUpEdit6.Properties.BestFitRowCount = All.dm_loaithutuc_loaicapphep.Count;
        }
        private void LoadLoaiThutuc_Trinhduyet()
        {
            List<DM_LoaiThutucTrinhduyet> list = Helpers.LoaiThutucTrinhDuyet.GetList();
            m_loai_thutuc_lk.Properties.DataSource = list;
            m_loai_thutuc_lk.Properties.DisplayMember = "DM014203";
            m_loai_thutuc_lk.Properties.ValueMember = "DM014201";
            m_loai_thutuc_lk.Properties.BestFitRowCount = list == null ? 0 : list.Count;
            m_loai_thutuc_lk.Refresh();

            lookUpEdit3.Properties.DataSource = list;
            lookUpEdit3.Properties.DisplayMember = "DM014203";
            lookUpEdit3.Properties.ValueMember = "DM014201";
            lookUpEdit3.Properties.BestFitRowCount = list == null ? 0 : list.Count;
            lookUpEdit3.Refresh();
        }

        private void FRM_DM_Thutuc_SizeChanged(object sender, EventArgs e)
        {
            splitContainerControl2.IsSplitterFixed = true;
            splitContainerControl2.SplitterPosition = splitContainerControl2.Height / 2;
        }

        private void gridView1_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            currentSelected_LoaiThutucNhiemvu = (DM_LoaiThutucNhiemvu)gridView1.GetFocusedRow();

            AssignDetailFormValue(currentSelected_LoaiThutucNhiemvu);
            LoadList_Noidung();
        }

        private void LoadList_Noidung()
        {
            if (currentSelected_LoaiThutucNhiemvu == null) return;
            currentList_Noidung = Helpers.ThutucNhiemvu_Noidung.GetList(currentSelected_LoaiThutucNhiemvu.DM016001);
            currentDataTable_Noidung = UF_Function.ToDataTable(currentList_Noidung);
            gridControl2.DataSource = currentDataTable_Noidung;
            gridControl2.RefreshDataSource();

            gridView2.OptionsBehavior.Editable = false;
            refNoidungID = Guid.Empty;
            refFromNoidungID = Guid.Empty;
        }

        private void MenuItem_them_d1_Click(object sender, EventArgs e)
        {
            StartAddNewDetail();
        }

        bool isStartingAddNew = false;
        private void StartAddNewDetail()
        {
            string status = ControlEffectWithMouse == 2 ? currentState_Noidung : ControlEffectWithMouse == 3 ? currentState_Truongdulieu : string.Empty;
            if (string.IsNullOrEmpty(status)) return;

            if (status == "NORMAL")
            {
                GridView gridView = ControlEffectWithMouse == 2 ? gridView2 : ControlEffectWithMouse == 3 ? gridView3 : null;
                if (gridView == null) return;

                gridView.AddNewRow();
                if (ControlEffectWithMouse == 2)
                    currentState_Noidung = "NEW";
                else if (ControlEffectWithMouse == 3)
                    currentState_Truongdulieu = "NEW";

                isStartingAddNew = true;
                gridView.OptionsBehavior.Editable = true;
                gridView.OptionsBehavior.ReadOnly = false;
                gridView.ShowInplaceEditForm();
                gridView.Focus();

                set_enable_groupControl(ControlEffectWithMouse);

                gridColumn5.OptionsColumn.AllowEdit = true;
                gridColumn10.OptionsColumn.AllowEdit = true;
                gridColumn11.OptionsColumn.AllowEdit = true;
                gridColumn12.OptionsColumn.AllowEdit = true;
                gridColumn13.OptionsColumn.AllowEdit = true;
                gridColumn9.OptionsColumn.AllowEdit = true;
                gridColumn16.OptionsColumn.AllowEdit = true;
                gridColumn17.OptionsColumn.AllowEdit = true;
            }
        }

        private void MenuItem_sua_d1_Click(object sender, EventArgs e)
        {
            string status = ControlEffectWithMouse == 2 ? currentState_Noidung : ControlEffectWithMouse == 3 ? currentState_Truongdulieu : string.Empty;
            if (string.IsNullOrEmpty(status)) return;

            if (status == "NORMAL")
            {
                GridView gridView = ControlEffectWithMouse == 2 ? gridView2 : ControlEffectWithMouse == 3 ? gridView3 : null;
                if (gridView == null) return;

                if (ControlEffectWithMouse == 2)
                    currentState_Noidung = "EDIT";
                else if (ControlEffectWithMouse == 3)
                    currentState_Truongdulieu = "EDIT";

                gridView.ShowInplaceEditForm();
                gridView.OptionsBehavior.Editable = true;
                gridView.OptionsBehavior.ReadOnly = false;
                gridView.Focus();

                set_enable_groupControl(ControlEffectWithMouse);
            }
        }

        private void MenuItem_xoa_d1_Click(object sender, EventArgs e)
        {
            string status = ControlEffectWithMouse == 2 ? currentState_Noidung : ControlEffectWithMouse == 3 ? currentState_Truongdulieu : string.Empty;
            if (string.IsNullOrEmpty(status)) return;

            GridView gridView = ControlEffectWithMouse == 2 ? gridView2 : ControlEffectWithMouse == 3 ? gridView3 : null;
            if (gridView == null) return;

            if (status == "NEW")
            {
                if (ControlEffectWithMouse == 2)
                    currentState_Noidung = "NORMAL";
                else if (ControlEffectWithMouse == 3)
                    currentState_Truongdulieu = "NORMAL";

                gridView.DeleteRow(gridView.FocusedRowHandle);
                gridView.OptionsBehavior.Editable = false;
            }
            else
            {
                string columnName = ControlEffectWithMouse == 2 ? "DM016101" : string.Empty;

                Guid id_delete = Guid.Parse(gridView.GetRowCellValue(gridView.FocusedRowHandle, columnName).ToString());
                if (MessageBox.Show("Bạn chắc chắn muốn xóa?", "Cảnh báo!", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    APIResponseData result =
                        ControlEffectWithMouse == 2 ?
                        Helpers.ThutucNhiemvu_Noidung.Delete(new List<Guid>() { id_delete }) :
                        null;

                    if (result == null)
                    {
                        All.Show_message("Có lỗi trong quá trình cập nhật dữ liệu!");
                    }
                    else if (result.ErrorCode == 0)
                    {
                        if (ControlEffectWithMouse == 2)
                            currentState_Noidung = "NORMAL";
                        else if (ControlEffectWithMouse == 3)
                            currentState_Truongdulieu = "NORMAL";

                        gridView.DeleteRow(gridView.FocusedRowHandle);
                        gridView.OptionsBehavior.Editable = false;
                        All.Show_message("Xóa thành công!");

                        if (ControlEffectWithMouse == 2)
                            LoadList_Noidung();
                        else if (ControlEffectWithMouse == 3)
                            return;
                    }
                    else
                    {
                        All.Show_message("Error code " + result.ErrorCode + ": " + result.Message);
                    }
                }
            }
        }

        private void MenuItem_saochep_d1_Click(object sender, EventArgs e)
        {
            if (currentState_LoaiThutucNhiemvu != "NORMAL" || currentState_Noidung != "NORMAL" || currentState_Truongdulieu != "NORMAL") return;

            ShowChildForm_Clone();
        }

        private void ShowChildForm_Clone()
        {
            this.Enabled = false;
            FRM_DM_Thutuc_Saochep frm = new FRM_DM_Thutuc_Saochep();
            frm.Show();
            frm.Focus();
        }

        public void UpdateChildForm_Clone(DM_LoaiThutucNhiemvu_Noidung obj)
        {
            this.Enabled = true;

            if (obj != null)
            {
                if (currentState_Noidung == "NORMAL")
                {
                    gridView2.AddNewRow();
                    currentState_Noidung = "CLONE";

                    isStartingAddNew = true;
                    gridView2.OptionsBehavior.Editable = true;
                    gridView2.OptionsBehavior.ReadOnly = false;
                    gridView2.ShowInplaceEditForm();
                    gridView2.Focus();

                    gridView2.SetFocusedRowCellValue("DM016104", obj.DM016104);
                    gridView2.SetFocusedRowCellValue("DM016105", obj.DM016105);

                    set_enable_groupControl(ControlEffectWithMouse);

                    gridColumn5.OptionsColumn.AllowEdit = false;
                    refNoidungID = obj.DM016101;
                }
            }
        }

        private void gridControl2_MouseDown(object sender, MouseEventArgs e)
        {
            ControlEffectWithMouse = 2;
        }

        private void gridControl3_MouseDown(object sender, MouseEventArgs e)
        {
            ControlEffectWithMouse = 3;
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (currentState_Noidung != "NORMAL" || currentState_Truongdulieu != "NORMAL")
                e.Cancel = true;
            else
            {
                if (ControlEffectWithMouse == 2)
                {
                    if (currentSelected_LoaiThutucNhiemvu == null) e.Cancel = true;
                }
                else if (ControlEffectWithMouse == 3)
                {
                    if ((currentSelected_Noidung == null) || currentSelected_Noidung.DM016105 != '2') e.Cancel = true;
                }

                if (ControlEffectWithMouse == 2)
                    MenuItem_saochep_d1.Visible = true;
                else
                    MenuItem_saochep_d1.Visible = false;
            }
        }

        private void gridView2_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            if (isStartingAddNew)
            {
                isStartingAddNew = false;
                return;
            }

            if (currentState_Noidung != "NORMAL")
            {
                currentState_Noidung = "NORMAL";
                set_enable_groupControl(0);
                gridView2.OptionsBehavior.Editable = false;
            }

            if (e.RowHandle != -1)
            {
                Guid id = Guid.Parse(gridView2.GetFocusedRowCellValue("DM016101").ToString());
                currentSelected_Noidung = currentSelected_LoaiThutucNhiemvu.DsNoidung.FirstOrDefault(o => o.DM016101 == id);

                if (currentSelected_Noidung.DM016105 != '2') groupControl3.Enabled = false;
                else groupControl3.Enabled = true;

                if (currentSelected_Noidung.DM016111 == Guid.Empty)
                    gridColumn5.OptionsColumn.AllowEdit = true;
                else
                    gridColumn5.OptionsColumn.AllowEdit = false;
            }
            else
                currentSelected_Noidung = null;

            LoadList_Truongdulieu();
        }

        private void LoadList_Truongdulieu()
        {
            if (currentSelected_Noidung == null)
                currentDataTable_Truongdulieu = null;
            else
            {
                currentList_Truongdulieu = Helpers.ThutucNhiemvu_Truongdulieu.GetList_Root(currentSelected_Noidung.DM016101);
                currentDataTable_Truongdulieu = UF_Function.ToDataTable(currentList_Truongdulieu);
            }

            gridControl3.DataSource = currentDataTable_Truongdulieu;
            gridView3.BestFitColumns();
            gridView3.OptionsDetail.ShowDetailTabs = false;
            gridControl3.RefreshDataSource();

            gridView3.OptionsBehavior.Editable = false;

            refTruongdulieuID = Guid.Empty;
        }

        private void gridView2_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            if (currentState_Noidung == "NORMAL") return;

            string ma_noidung = gridView2.GetFocusedRowCellValue("DM016103").ToString();
            string ten_noidung = gridView2.GetFocusedRowCellValue("DM016104").ToString();
            string cach_nhap = gridView2.GetFocusedRowCellValue("DM016105").ToString();
            if ((ma_noidung + ten_noidung + cach_nhap).Length == 0)
            {
                currentState_Noidung = "NORMAL";
                gridView2.DeleteRow(gridView2.FocusedRowHandle);
                gridView2.OptionsBehavior.Editable = false;
                gridView2.OptionsBehavior.ReadOnly = true;
                set_enable_groupControl(0);
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
            if (cach_nhap.Length == 0)
            {
                All.Show_message("Bạn phải chọn cách nhập!");
                return;
            }

            DM_LoaiThutucNhiemvu_Noidung obj = PrepareDetail_Noidung();
            if (obj == null)
            {
                All.Show_message("Có lỗi trong quá trình cập nhật dữ liệu!");
                set_enable_groupControl(0);
                gridView2.OptionsBehavior.Editable = false;
                gridView2.OptionsBehavior.ReadOnly = true;
                return;
            }

            if (currentState_Noidung == "NEW" || currentState_Noidung == "CLONE")
            {
                APIResponseData result = Helpers.ThutucNhiemvu_Noidung.Create(obj);
                if (result == null)
                {
                    All.Show_message("Có lỗi trong quá trình cập nhật dữ liệu!");
                }
                else if (result.ErrorCode == 0)
                {
                    All.Show_message("Thêm mới thành công Nội dung: " + obj.DM016103);
                    currentState_Noidung = "NORMAL";
                    LoadList_LoaiThutucNhiemvu();
                    
                    set_enable_groupControl(0);
                    gridView2.OptionsBehavior.Editable = false;
                    gridView2.OptionsBehavior.ReadOnly = true;
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
                    currentState_Noidung = "NORMAL";
                    LoadList_LoaiThutucNhiemvu();

                    set_enable_groupControl(0);
                    gridView2.OptionsBehavior.Editable = false;
                    gridView2.OptionsBehavior.ReadOnly = true;
                }
                else
                {
                    All.Show_message("Error code " + result.ErrorCode + ": " + result.Message);
                }
            }
        }

        private DM_LoaiThutucNhiemvu_Noidung PrepareDetail_Noidung()
        {
            try
            {
                DM_LoaiThutucNhiemvu_Noidung obj = new DM_LoaiThutucNhiemvu_Noidung();
                obj.DM016101 = (currentState_Noidung == "NEW" || currentState_Noidung == "CLONE") ? Guid.NewGuid() : Guid.Parse(gridView2.GetFocusedRowCellValue("DM016101").ToString());
                obj.DM016102 = currentSelected_LoaiThutucNhiemvu.DM016001;
                obj.DM016103 = gridView2.GetFocusedRowCellValue("DM016103").ToString();
                obj.DM016104 = gridView2.GetFocusedRowCellValue("DM016104").ToString();
                obj.DM016105 = (char)gridView2.GetFocusedRowCellValue("DM016105");
                obj.DM016106 = All.gs_user_id;
                obj.DM016107 = DateTime.Now;
                obj.DM016108 = All.gs_user_id;
                obj.DM016109 = DateTime.Now;
                obj.DM016110 = string.Empty;
                obj.DM016111 = currentState_Noidung == "NEW" ? Guid.Empty : refNoidungID;

                return obj;
            }
            catch (Exception ex)
            {
                Log.write(ex);
                return null;
            }
        }

        private void gridView3_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            if (isStartingAddNew)
            {
                isStartingAddNew = false;
                return;
            }

            if (currentState_Truongdulieu != "NORMAL")
            {
                currentState_Truongdulieu = "NORMAL";
                set_enable_groupControl(0);
                gridView3.OptionsBehavior.Editable = false;
            }

            currentSelected_Truongdulieu_LookupData = null;

            if (e.RowHandle < 0)
            {
                return;
            }

            currentSelected_Truongdulieu = null;
            DataRow dr = gridView3.GetFocusedDataRow();

            if (dr != null)
            {
                Guid id = Guid.Parse(gridView3.GetFocusedRowCellValue("DM016201").ToString());
                currentSelected_Truongdulieu = currentSelected_Noidung.DsTruongdulieu.FirstOrDefault(o => o.DM016201 == id);

                #region Check clone

                gridColumn10.OptionsColumn.AllowEdit = currentSelected_Truongdulieu.DM016221 == Guid.Empty;
                gridColumn11.OptionsColumn.AllowEdit = currentSelected_Truongdulieu.DM016221 == Guid.Empty;
                gridColumn12.OptionsColumn.AllowEdit = currentSelected_Truongdulieu.DM016221 == Guid.Empty;
                gridColumn13.OptionsColumn.AllowEdit = currentSelected_Truongdulieu.DM016221 == Guid.Empty;
                gridColumn9.OptionsColumn.AllowEdit = currentSelected_Truongdulieu.DM016221 == Guid.Empty;
                gridColumn16.OptionsColumn.AllowEdit = currentSelected_Truongdulieu.DM016221 == Guid.Empty;
                gridColumn17.OptionsColumn.AllowEdit = currentSelected_Truongdulieu.DM016221 == Guid.Empty;

                #endregion

                string kieutruong = dr.ItemArray[4].ToString().Trim();
                string data = dr.ItemArray[7].ToString().Trim();

                if (string.IsNullOrEmpty(data))
                {
                    currentSelected_Truongdulieu_LookupData = null;
                }
                else
                {
                    if (kieutruong == "8")
                        currentSelected_Truongdulieu_LookupData = JsonConvert.DeserializeObject<DM_LoaiThutucNhiemvu_Truongdulieu_LookupData>(data);
                }
            }
        }

        private void gridView3_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            if (currentState_Truongdulieu == "NORMAL") return;

            string maso = gridView3.GetFocusedRowCellValue("DM016204").ToString();
            string ten = gridView3.GetFocusedRowCellValue("DM016205").ToString();
            string tenhienthi = gridView3.GetFocusedRowCellValue("DM016206").ToString();
            string kieutruong = gridView3.GetFocusedRowCellValue("DM016207").ToString();
            string dorong = gridView3.GetFocusedRowCellValue("DM016208").ToString();
            string congthuctinh = gridView3.GetFocusedRowCellValue("DM016213").ToString();
            string sapxep = gridView3.GetFocusedRowCellValue("DM016214").ToString();

            if ((maso + ten + tenhienthi + kieutruong + dorong + sapxep).Length == 0)
            {
                currentState_Truongdulieu = "NORMAL";
                gridView3.DeleteRow(gridView3.FocusedRowHandle);
                gridView3.OptionsBehavior.Editable = false;
                gridView3.OptionsBehavior.ReadOnly = true;
                set_enable_groupControl(0);
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
            if (kieutruong == "8" && currentSelected_Truongdulieu_LookupData == null)
            {
                All.Show_message("Bạn phải chọn Điều kiện dữ liệu!");
                return;
            }
            if (dorong.Length == 0)
            {
                All.Show_message("Bạn phải nhập Độ rộng!");
                return;
            }
            if (sapxep.Length == 0)
            {
                All.Show_message("Bạn phải nhập Sắp xếp!");
                return;
            }

            DM_LoaiThutucNhiemvu_Truongdulieu obj = PrepareDetail_Truongdulieu();
            if (obj == null)
            {
                All.Show_message("Có lỗi trong quá trình cập nhật dữ liệu!");
                set_enable_groupControl(0);
                gridView3.OptionsBehavior.Editable = false;
                gridView3.OptionsBehavior.ReadOnly = true;
                return;
            }

            if (currentState_Truongdulieu == "NEW")
            {
                APIResponseData result = Helpers.ThutucNhiemvu_Truongdulieu.Create(obj);
                if (result == null)
                {
                    All.Show_message("Có lỗi trong quá trình cập nhật dữ liệu!");
                }
                else if (result.ErrorCode == 0)
                {
                    All.Show_message("Thêm mới thành công Trường dữ liệu: " + obj.DM016204);
                    currentState_Truongdulieu = "NORMAL";
                    LoadList_LoaiThutucNhiemvu();

                    set_enable_groupControl(0);
                    gridView3.OptionsBehavior.Editable = false;
                    gridView3.OptionsBehavior.ReadOnly = true;
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
                    currentState_Truongdulieu = "NORMAL";
                    LoadList_LoaiThutucNhiemvu();

                    set_enable_groupControl(0);
                    gridView3.OptionsBehavior.Editable = false;
                    gridView3.OptionsBehavior.ReadOnly = true;
                }
                else
                {
                    All.Show_message("Error code " + result.ErrorCode + ": " + result.Message);
                }
            }            
        }

        private DM_LoaiThutucNhiemvu_Truongdulieu PrepareDetail_Truongdulieu()
        {
            try
            {
                DM_LoaiThutucNhiemvu_Truongdulieu obj = new DM_LoaiThutucNhiemvu_Truongdulieu();
                obj.DM016201 = currentState_Truongdulieu == "NEW" ? Guid.NewGuid() : Guid.Parse(gridView3.GetFocusedRowCellValue("DM016201").ToString());
                obj.DM016204 = gridView3.GetFocusedRowCellValue("DM016204").ToString();
                obj.DM016205 = gridView3.GetFocusedRowCellValue("DM016205").ToString();
                obj.DM016206 = gridView3.GetFocusedRowCellValue("DM016206").ToString();
                obj.DM016207 = gridView3.GetFocusedRowCellValue("DM016207").ToString().Trim();
                obj.DM016208 = int.Parse(gridView3.GetFocusedRowCellValue("DM016208").ToString());
                obj.DM016209 = string.Empty;
                obj.DM016210 = obj.DM016207 == "8" ? JsonConvert.SerializeObject(currentSelected_Truongdulieu_LookupData) : string.Empty;
                obj.DM016213 = gridView3.GetFocusedRowCellValue("DM016213").ToString();
                obj.DM016214 = int.Parse(gridView3.GetFocusedRowCellValue("DM016214").ToString());
                char batbuocnhap = gridView3.GetFocusedRowCellValue("DM016215") == DBNull.Value ? '0' : char.Parse(gridView3.GetFocusedRowCellValue("DM016215").ToString().Trim());
                obj.DM016215 = batbuocnhap;
                obj.DM016217 = All.gs_user_id;
                obj.DM016218 = DateTime.Now;
                obj.DM016219 = All.gs_user_id;
                obj.DM016220 = DateTime.Now;
                obj.DM016216 = gridView3.GetFocusedRowCellValue("DM016216") == DBNull.Value ? Guid.Empty : Guid.Parse(gridView3.GetFocusedRowCellValue("DM016216").ToString());
                obj.NoidungId = currentSelected_Noidung.DM016101;
                obj.DM016221 = refTruongdulieuID;

                return obj;
            }
            catch (Exception ex)
            {
                Log.write(ex);
                return null;
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            FRM_DM_ThuTuc_NhiemVu_Cothienthi frm = new FRM_DM_ThuTuc_NhiemVu_Cothienthi();

            if (currentSelected_LoaiThutucNhiemvu_Cothienthi == null)
                currentSelected_LoaiThutucNhiemvu_Cothienthi = Helpers.TrinhduyetThuchienNhiemvu.GenerateListColumns();

            frm.currentList = currentSelected_LoaiThutucNhiemvu_Cothienthi;
            frm.currentState = currentState_LoaiThutucNhiemvu;
            frm.Show();
            frm.Focus();
        }

        private void gridView2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (currentState_Noidung != "NORMAL")
                {
                    gridView2.UpdateCurrentRow();
                    set_enable_groupControl(0);
                    gridView3.OptionsBehavior.Editable = false;
                    gridView3.OptionsBehavior.ReadOnly = true;
                }
            }
        }

        private void gridControl2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (currentState_Noidung != "NORMAL")
                {
                    gridView2.UpdateCurrentRow();
                    set_enable_groupControl(0);
                    gridView3.OptionsBehavior.Editable = false;
                    gridView3.OptionsBehavior.ReadOnly = true;
                }
            }
        }
    }
}
