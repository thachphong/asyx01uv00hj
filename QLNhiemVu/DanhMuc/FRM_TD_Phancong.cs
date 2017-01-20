using DBAccess;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
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
    public partial class FRM_TD_Phancong : Form
    {
        private static Guid donvisudungID = All.gs_dv_quanly;
        private static string donvisudungName = All.gs_ten_dv_quanly;
        private static Guid nguoisudungID = All.gs_user_id;
        private static string nguoisudungName = All.gs_user_name;
        private static string MaForm = "1";
        private static List<TD_DonviQuanly> DsDonviCapduoi = null;

        private static List<TD_ThuchienNhiemvu> currentList = null;
        private static int currentRowSelected = int.MinValue;
        private static TD_ThuchienNhiemvu currentNhiemvu = null;
        private static TD_Phancong currentPhancong = null;
        private static string currentState = "NORMAL";

        private static DM_LoaiThutucNhiemvu currentThutucNhiemvu = null;

        public FRM_TD_Phancong()
        {
            InitializeComponent();
        }

        private void FRM_TD_Phancong_Load(object sender, EventArgs e)
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

            lblHeadTitle1.setText("Trình duyệt Thực hiện Phân công");
            lblHeadTitle1.alignCenter(lblHeadTitle1.Parent);
            panelHeader1.alignCenter(panelHeader1.Parent);
            panelHeader2.alignCenter(panelHeader2.Parent);
            panelHeader3.alignCenter(panelHeader2.Parent);

            label23.Text = donvisudungName;
            label1.Text = nguoisudungName;

            BindControlEvents();

            currentState = "NORMAL";

            LoadTemplates();

            uC_MenuBtn1.set_status_menu(currentState, currentList == null ? 0 : currentList.Count);
            SetDetailFormEnable(false);
            AssignDetailFormValue(null);
        }

        #region Load templates

        private void LoadTemplates()
        {
            dateEdit1.Properties.Mask.EditMask = All.dateFormat;
            dateEdit2.Properties.Mask.EditMask = All.dateFormat;

            LoadYears();
            LoadPhamviphancong();
            LoadThamquyenphancong();
            LoadNguoinhanVanban();
            LoadPhancong();
            LoadDonviQuanly();
            LoadTrangthaiHoso();

            GenerateGridTemplate();
        }

        private void GenerateGridTemplate()
        {
            if (currentThutucNhiemvu == null || currentThutucNhiemvu.DsCothienthi == null || currentThutucNhiemvu.DsCothienthi.Count == 0) return;

            gridView1.Columns.Clear();

            foreach (TD_ThuchienNhiemvu_Cothienthi cot in currentThutucNhiemvu.DsCothienthi)
            {
                if (!cot.IsChecked) continue;

                GridColumn colDyn = new GridColumn()
                {
                    Caption = cot.DisplayName,
                    FieldName = cot.ColumnName,
                    UnboundType = DevExpress.Data.UnboundColumnType.Object,
                    Visible = true,
                };
                colDyn.OptionsColumn.AllowEdit = false;
                colDyn.OptionsColumn.AllowFocus = false;
                gridView1.Columns.Add(colDyn);
            }

            GridColumn col = new GridColumn()
            {
                Caption = "Xem ý kiến",
                Width = 100,
                ColumnEdit = new RepositoryItemButtonEdit()
                {
                    TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor,
                },
                Visible = true,
            };
            col.OptionsColumn.FixedWidth = true;
            col.OptionsColumn.AllowEdit = true;
            col.OptionsColumn.AllowFocus = true;
            col.ColumnEdit.Click += ColumnEdit_Click;
            gridView1.Columns.Add(col);

            gridView1.RefreshEditor(false);
        }

        private void LoadThamquyenphancong()
        {
            lookUpEdit5.Properties.DataSource = All.td_phancong_thamquyen;
            lookUpEdit5.Properties.DisplayMember = "Description";
            lookUpEdit5.Properties.ValueMember = "ID";
            lookUpEdit5.Properties.BestFitRowCount = All.td_phancong_thamquyen == null ? 0 : All.td_phancong_thamquyen.Count;
        }

        private void LoadPhamviphancong()
        {
            lookUpEdit7.Properties.DataSource = All.td_phancong_phamvi;
            lookUpEdit7.Properties.DisplayMember = "Description";
            lookUpEdit7.Properties.ValueMember = "ID";
            lookUpEdit7.Properties.BestFitRowCount = All.td_phancong_phamvi == null ? 0 : All.td_phancong_phamvi.Count;

            lookUpEdit14.Properties.DataSource = All.td_phancong_phamvi;
            lookUpEdit14.Properties.DisplayMember = "Description";
            lookUpEdit14.Properties.ValueMember = "ID";
            lookUpEdit14.Properties.BestFitRowCount = All.td_phancong_phamvi == null ? 0 : All.td_phancong_phamvi.Count;
        }

        private void LoadTrangthaiHoso()
        {
            List<TD_TrangthaiHoSo> list = Helpers.Trinhduyet.GetList_TrangthaiHoSo();
            lookUpEdit1.Properties.DataSource = list;
            lookUpEdit1.Properties.DisplayMember = "DM012403";
            lookUpEdit1.Properties.ValueMember = "DM012401";
            lookUpEdit1.Properties.BestFitRowCount = list == null ? 0 : list.Count;
        }

        //private void LoadNoidungThutuc()
        //{
        //    List<DM_LoaiThutucNhiemvu_Noidung> list = currentThutucNhiemvu.DsNoidung;

        //    lookUpEdit13.Properties.DataSource = list;
        //    lookUpEdit13.Properties.DisplayMember = "DM016104";
        //    lookUpEdit13.Properties.ValueMember = "DM016101";
        //    lookUpEdit13.Properties.BestFitRowCount = list == null ? 0 : list.Count;
        //}

        private void LoadPhancong()
        {
            //char phamviId = '0';
            //char thamquyenId = '0';

            //if (lookUpEdit7.EditValue != null)
            //    phamviId = char.Parse(lookUpEdit7.EditValue.ToString());
            //if (lookUpEdit5.EditValue != null)
            //    thamquyenId = char.Parse(lookUpEdit5.EditValue.ToString());

            List<TD_Nguoiky> list = Helpers.Trinhduyet.GetList_Nhansu(nguoisudungID, '0', '0');

            lookUpEdit4.Properties.DataSource = list;
            lookUpEdit4.Properties.DisplayMember = "DM030403";
            lookUpEdit4.Properties.ValueMember = "DM030401";
            lookUpEdit4.Properties.BestFitRowCount = list == null ? 0 : list.Count;
        }

        private void LoadNguoinhanVanban()
        {
            char phamviId = '0';
            char thamquyenId = '0';

            if (lookUpEdit7.EditValue != null)
                phamviId = char.Parse(lookUpEdit7.EditValue.ToString());
            if (lookUpEdit5.EditValue != null)
                thamquyenId = char.Parse(lookUpEdit5.EditValue.ToString());

            List<TD_Nguoiky> list = Helpers.Trinhduyet.GetList_Nhansu(nguoisudungID, phamviId, thamquyenId);

            lookUpEdit3.Properties.DataSource = list;
            lookUpEdit3.Properties.DisplayMember = "DM030403";
            lookUpEdit3.Properties.ValueMember = "DM030401";
            lookUpEdit3.Properties.BestFitRowCount = list == null ? 0 : list.Count;
        }

        private void LoadDonviQuanly()
        {
            List<TD_DonviQuanly> list = Helpers.Trinhduyet.GetList_DonviQuanly();
            DsDonviCapduoi = list;//Dành cho test
            lookUpEdit2.Properties.DataSource = list;
            lookUpEdit2.Properties.DisplayMember = "DM030105";
            lookUpEdit2.Properties.ValueMember = "DM030101";
            lookUpEdit2.Properties.BestFitRowCount = list == null ? 0 : list.Count;
        }

        //private void LoadDanhmucNhiemvu(LookUpEdit control)
        //{
        //    List<TD_ThuchienNhiemvu_DanhmucNhiemvu> list = null;
        //    if ((Guid)control.EditValue != Guid.Empty)
        //    {
        //        TD_ThuchienNhiemvu_PhanloaiNhiemvu obj = (TD_ThuchienNhiemvu_PhanloaiNhiemvu)control.GetSelectedDataRow();
        //        list = obj.DSDanhmuc;
        //    }

        //    LookUpEdit effectControl = control.Name == "lookUpEdit3" ? lookUpEdit4 : lookUpEdit8;
        //    effectControl.Properties.DataSource = list;
        //    effectControl.Properties.DisplayMember = "DM020204";
        //    effectControl.Properties.ValueMember = "DM020201";
        //    effectControl.Properties.BestFitRowCount = list == null ? 0 : list.Count;
        //}

        //private void LoadPhanloaiNhiemvu()
        //{
        //    List<TD_ThuchienNhiemvu_PhanloaiNhiemvu> list = Helpers.TrinhduyetThuchienNhiemvu.GetList_PhanloaiNhiemvu();
        //    lookUpEdit7.Properties.DataSource = list;
        //    lookUpEdit7.Properties.DisplayMember = "DM014003";
        //    lookUpEdit7.Properties.ValueMember = "DM014001";
        //    lookUpEdit7.Properties.BestFitRowCount = list == null ? 0 : list.Count;

        //    lookUpEdit3.Properties.DataSource = list;
        //    lookUpEdit3.Properties.DisplayMember = "DM014003";
        //    lookUpEdit3.Properties.ValueMember = "DM014001";
        //    lookUpEdit3.Properties.BestFitRowCount = list == null ? 0 : list.Count;
        //}

        private void LoadYears()
        {
            List<int> years = new List<int>();
            for (int i = DateTime.Now.Year - 3; i <= DateTime.Now.Year + 9; i++)
            {
                years.Add(i);
            }

            lookUpEdit6.Properties.DataSource = years;
            lookUpEdit6.Properties.BestFitRowCount = years.Count;
            lookUpEdit6.EditValue = DateTime.Now.Year;
        }

        #endregion

        private void BindControlEvents()
        {
            //gridColumn10.ColumnEdit.EditValueChanged += ColumnEdit_EditValueChanged;
            //gridColumn30.ColumnEdit.Click += ColumnEdit_Click;

            uC_MenuBtn1.btn_them.Click += btn_them_Click;
            uC_MenuBtn1.btn_thoat.Click += btn_thoat_Click;
            uC_MenuBtn1.btn_boqua.Click += btn_boqua_Click;
            uC_MenuBtn1.btn_capnhat.Click += btn_capnhat_Click;
            uC_MenuBtn1.btn_sua.Click += btn_sua_Click;
            uC_MenuBtn1.btn_xoa.Click += btn_xoa_Click;
        }

        void ColumnEdit_Click(object sender, EventArgs e)
        {
            TD_ThuchienNhiemvu obj = (TD_ThuchienNhiemvu)gridView1.GetFocusedRow();
            if (obj.DsPhancong == null || obj.DsPhancong.Count == 0)
            {
                All.Show_message("Nhiệm vụ này chưa có Phân công!");
                return;
            }

            FRM_TD_Phancong_Xemykien frm = new FRM_TD_Phancong_Xemykien();
            frm.currentList = obj.DsPhancong;
            frm.Show();
            frm.Focus();
        }

        void btn_xoa_Click(object sender, EventArgs e)
        {
            ////List<Guid> listChecked = GetIDChecked();
            //if (listChecked == null)
            //{
            //    All.Show_message("Vui lòng chọn trước khi xóa!");
            //    return;
            //}

            //if (MessageBox.Show("Bạn chắc chắn muốn xóa " + listChecked.Count + " Phân công đang chọn?", "Cảnh báo!", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            //{
            //    APIResponseData result = Helpers.TrinhDuyetPhancong.Delete(listChecked);
            //    if (result == null)
            //    {
            //        All.Show_message("Có lỗi trong quá trình cập nhật dữ liệu!");
            //    }
            //    else if (result.ErrorCode == 0)
            //    {
            //        All.Show_message("Xóa thành công " + listChecked.Count + " Phân công đã chọn!");
            //        currentState = "NORMAL";
            //        LoadList(false);
            //    }
            //    else
            //    {
            //        All.Show_message("Error code " + result.ErrorCode + ": " + result.Message);
            //    }
            //}
        }

        //private List<Guid> GetIDChecked()
        //{
        //    List<Guid> result = new List<Guid>();

        //    for (int i = 0; i < gridView1.RowCount; i++)
        //    {
        //        bool isChecked = (bool)gridView1.GetRowCellValue(i, gridColumn10);
        //        if (isChecked)
        //            result.Add((Guid)gridView1.GetRowCellValue(i, gridColumn1));
        //    }

        //    return result.Count == 0 ? null : result;
        //}

        void btn_sua_Click(object sender, EventArgs e)
        {
            currentState = "EDIT";
            uC_MenuBtn1.set_status_menu(currentState, currentList == null ? 0 : currentList.Count);
            AssignDetailFormValue(currentNhiemvu);
            SetDetailFormEnable(true);
        }

        void RefreshNewData(TD_Phancong obj)
        {
            TD_ThuchienNhiemvu current = currentList.FirstOrDefault(o => o.DM016701 == obj.DM017002);
            if (current == null) return;

            TD_Phancong pc = current.DsPhancong.FirstOrDefault(o => o.DM017001 == obj.DM017001);
            if (pc == null) current.DsPhancong.Insert(0, obj);
            else pc = obj;
        }

        void btn_capnhat_Click(object sender, EventArgs e)
        {
            if (currentState == "NORMAL") return;

            if (!ValidateDetailForm()) return;

            TD_Phancong obj = PrepareDetail();

            if (obj == null)
            {
                All.Show_message("Có lỗi trong quá trình cập nhật dữ liệu!");
                return;
            }

            if (currentState == "NEW")
            {
                APIResponseData result = Helpers.TrinhDuyetPhancong.Create(obj);
                if (result == null)
                {
                    All.Show_message("Có lỗi trong quá trình cập nhật dữ liệu!");
                }
                else if (result.ErrorCode == 0)
                {
                    All.Show_message("Thêm mới thành công Phân công: " + obj.DM017001);
                    currentState = "NORMAL";
                    RefreshNewData(JsonConvert.DeserializeObject<TD_Phancong>(result.Data.ToString()));
                    LoadList();
                }
                else
                {
                    All.Show_message("Error code " + result.ErrorCode + ": " + result.Message);
                }
            }
            else
            {
                APIResponseData result = Helpers.TrinhDuyetPhancong.Update(obj);
                if (result == null)
                {
                    All.Show_message("Có lỗi trong quá trình cập nhật dữ liệu!");
                }
                else if (result.ErrorCode == 0)
                {
                    All.Show_message("Cập nhật thành công Phân công: " + obj.DM017001);
                    currentState = "NORMAL";
                    RefreshNewData(JsonConvert.DeserializeObject<TD_Phancong>(result.Data.ToString()));
                    LoadList();
                }
                else
                {
                    All.Show_message("Error code " + result.ErrorCode + ": " + result.Message);
                    if (result.ErrorCode == -1)
                        LoadList();
                }
            }
        }

        private TD_Phancong PrepareDetail()
        {
            try
            {
                TD_Phancong obj = new TD_Phancong();
                obj.DM017001 = currentState == "NEW" ? Guid.NewGuid() : currentPhancong.DM017001;
                obj.DM017002 = currentNhiemvu.DM016701;
                obj.DM017003 = char.Parse(lookUpEdit14.EditValue.ToString());

                if (obj.DM017003 == '2')
                    obj.DM017004 = Guid.Parse(lookUpEdit10.EditValue.ToString());
                else
                    obj.DM017004 = ((TD_Phancong_DoituongNhanVB)lookUpEdit10.GetSelectedDataRow()).MaDonvi;

                if (obj.DM017003 == '2')
                    obj.DM017005 = Guid.Empty;
                else
                    obj.DM017005 = Guid.Parse(lookUpEdit10.EditValue.ToString());

                obj.DM017006 = dateEdit1.DateTime;
                obj.DM017007 = dateEdit2.DateTime;
                obj.DM017008 = memoEdit1.Text;
                obj.DM017009 = currentState == "NEW" ? All.gs_user_id : currentPhancong.DM017009;
                obj.DM017010 = currentState == "NEW" ? DateTime.Now : currentPhancong.DM017010;
                obj.DM017011 = All.gs_user_id;
                obj.DM017012 = DateTime.Now;

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
            //if (lookUpEdit6.EditValue == null)
            //{
            //    All.Show_message("Vui lòng chọn Năm!");
            //    lookUpEdit6.Focus();
            //    return false;
            //}

            //if (lookUpEdit1.EditValue.ToString() == Guid.Empty.ToString())
            //{
            //    All.Show_message("Vui lòng chọn Trạng thái!");
            //    lookUpEdit1.Focus();
            //    return false;
            //}

            //if (lookUpEdit7.EditValue.ToString() == Guid.Empty.ToString())
            //{
            //    All.Show_message("Vui lòng chọn Phân loại Phân công!");
            //    lookUpEdit7.Focus();
            //    return false;
            //}

            //if (lookUpEdit8.EditValue.ToString() == Guid.Empty.ToString())
            //{
            //    All.Show_message("Vui lòng chọn Danh mục Phân công!");
            //    lookUpEdit8.Focus();
            //    return false;
            //}

            //if (textEdit2.Text.Trim() == string.Empty)
            //{
            //    All.Show_message("Vui lòng nhập Số văn bản!");
            //    textEdit2.Focus();
            //    return false;
            //}
            //if (textEdit3.Text.Trim() == string.Empty)
            //{
            //    All.Show_message("Vui lòng nhập Số văn bản!");
            //    textEdit3.Focus();
            //    return false;
            //}

            //if (dateEdit3.EditValue == null)
            //{
            //    All.Show_message("Vui lòng chọn Ngày!");
            //    dateEdit3.Focus();
            //    return false;
            //}

            //if (textEdit4.Text.Trim() == string.Empty)
            //{
            //    All.Show_message("Vui lòng nhập Trích yếu!");
            //    textEdit4.Focus();
            //    return false;
            //}

            //if (lookUpEdit10.EditValue.ToString() == Guid.Empty.ToString())
            //{
            //    All.Show_message("Vui lòng chọn Cơ quan nhận!");
            //    lookUpEdit10.Focus();
            //    return false;
            //}

            //if (lookUpEdit11.EditValue.ToString() == Guid.Empty.ToString())
            //{
            //    All.Show_message("Vui lòng chọn Người ký!");
            //    lookUpEdit11.Focus();
            //    return false;
            //}

            //if (lookUpEdit13.EditValue.ToString() == Guid.Empty.ToString())
            //{
            //    All.Show_message("Vui lòng chọn Nội dung Phân công!");
            //    lookUpEdit13.Focus();
            //    return false;
            //}

            //if (currentNoidung.DM016105 == '1')
            //{
            //    MemoEdit mmeText = (MemoEdit)xtraScrollableControl1.Controls.Find("mmeText", true).FirstOrDefault();
            //    if (mmeText == null || mmeText.Text.Trim() == string.Empty)
            //    {
            //        All.Show_message("Vui lòng nhập Nội dung chi tiết!");
            //        if (mmeText != null) mmeText.Focus();
            //        return false;
            //    }
            //}
            //else
            //{
            //    var list = tempFields.Where(o => o.Batbuocnhap);
            //    if (list.Count() > 0)
            //    {
            //        foreach (TD_ThuchienNhiemvu_Truongdulieu obj in list)
            //        {
            //            if (obj.Kieutruong != 9 && obj.DM016804.Trim() == string.Empty)
            //            {
            //                All.Show_message("Vui lòng nhập đầy đủ các trường dữ liệu chi tiết!");
            //                return false;
            //            }

            //            if (obj.Kieutruong == 9)
            //            {
            //                if (obj.Children == null || obj.Children.Count(o => o.DM016804.Trim() == string.Empty) > 0)
            //                {
            //                    All.Show_message("Vui lòng nhập đầy đủ các trường dữ liệu chi tiết!");
            //                    return false;
            //                }
            //            }
            //        }
            //    }
            //}


            return true;
        }

        void btn_boqua_Click(object sender, EventArgs e)
        {
            currentState = "NORMAL";
            uC_MenuBtn1.set_status_menu(currentState, currentList == null ? 0 : currentList.Count);
            AssignDetailFormValue(currentNhiemvu);
            SetDetailFormEnable(false);
        }

        void btn_thoat_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        void btn_them_Click(object sender, EventArgs e)
        {
            //if (currentNhiemvu == null)
            //{
            //    return;
            //}

            currentState = "NEW";
            uC_MenuBtn1.set_status_menu(currentState, 0);
            AssignDetailFormValue(null);
            SetDetailFormEnable(true);
        }

        //bool performChecked = true;
        //void ColumnEdit_EditValueChanged(object sender, EventArgs e)
        //{
        //    gridView1.PostEditor();

        //    CheckEdit cbxEdit = (CheckEdit)sender;
        //    if (!cbxEdit.Checked)
        //    {
        //        performChecked = false;
        //        checkEdit1.Checked = false;
        //    }
        //    else
        //    {
        //        bool allChecked = true;
        //        for (int i = 0; i < gridView1.RowCount; i++)
        //        {
        //            allChecked = (bool)gridView1.GetRowCellValue(i, gridColumn10);
        //            if (!allChecked) return;
        //        }

        //        if (allChecked)
        //            checkEdit1.Checked = true;
        //    }
        //}

        private void LoadList(bool refresh = true)
        {
            TD_Phancong_Filter filter = new TD_Phancong_Filter();
            filter.Nam = lookUpEdit6.EditValue == null ? 0 : int.Parse(lookUpEdit6.EditValue.ToString());
            filter.Donviphancong = lookUpEdit2.EditValue == null ? Guid.Empty : Guid.Parse(lookUpEdit2.EditValue.ToString());
            filter.Nguoinhanvanbanden = lookUpEdit3.EditValue == null ? Guid.Empty : Guid.Parse(lookUpEdit3.EditValue.ToString());
            filter.Nguoiphancong = lookUpEdit4.EditValue == null ? Guid.Empty : Guid.Parse(lookUpEdit4.EditValue.ToString());
            filter.Phamviphancong = lookUpEdit7.EditValue == null ? '0' : char.Parse(lookUpEdit7.EditValue.ToString());
            filter.Thamquyenphancong = lookUpEdit5.EditValue == null ? '0' : char.Parse(lookUpEdit5.EditValue.ToString());
            filter.Trangthai = lookUpEdit1.EditValue == null ? Guid.Empty : Guid.Parse(lookUpEdit1.EditValue.ToString());

            currentList = !refresh ?
                Helpers.TrinhduyetThuchienNhiemvu.GetList_For_Phancong(filter) :
                currentList;

            gridControl1.DataSource = currentList;
            gridControl1.RefreshDataSource();
            gridView1.BestFitColumns();

            uC_MenuBtn1.set_status_menu(currentState, currentList == null ? 0 : currentList.Count);
            SetDetailFormEnable(false);

            currentNhiemvu = (TD_ThuchienNhiemvu)gridView1.GetFocusedRow();
            AssignDetailFormValue(currentNhiemvu);
        }

        //private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (performChecked)
        //    {
        //        for (int i = 0; i < gridView1.RowCount; i++)
        //        {
        //            gridView1.SetRowCellValue(i, gridColumn10, checkEdit1.Checked);
        //        }
        //    }

        //    performChecked = true;
        //}

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            currentRowSelected = e.FocusedRowHandle;
            currentNhiemvu = (TD_ThuchienNhiemvu)gridView1.GetRow(currentRowSelected);
            AssignDetailFormValue(currentNhiemvu);
        }

        private void SetDetailFormEnable(bool isEnable)
        {
            foreach (Control ctrl in panelHeader1.Controls)
            {
                Type t = ctrl.GetType();
                if (t.GetProperty("ReadOnly") != null)
                {
                    t.GetProperty("ReadOnly").SetValue(ctrl, isEnable, null);
                }
                else
                    ctrl.Enabled = !isEnable;
            }

            groupControl1.Enabled = !isEnable;
            groupControl2.Enabled = isEnable;
        }

        private void AssignDetailFormValue(TD_ThuchienNhiemvu data)
        {
            TD_Phancong pc = null;
            if (data != null && data.DsPhancong != null)
            {
                pc = data.DsPhancong.OrderByDescending(o => o.DM017006).FirstOrDefault();
            }

            lookUpEdit14.EditValue = pc == null ? '0' : pc.DM017003;
            dateEdit1.DateTime = pc == null ? DateTime.Now : pc.DM017006;
            lookUpEdit10.EditValue = pc == null ? Guid.Empty : pc.DM017005;
            lookUpEdit1.EditValue = pc == null ? Guid.Empty : data.DM016720;
            dateEdit2.DateTime = pc == null ? DateTime.Now : pc.DM017007;
            memoEdit1.Text = pc == null ? string.Empty : pc.DM017008;

            if (currentNhiemvu == null)
                uC_MenuBtn1.btn_them.Enabled = false;
            else if (currentState == "NORMAL")
                uC_MenuBtn1.btn_them.Enabled = true;
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            ShowChildForm_Huongdan();
        }

        private void ShowChildForm_Huongdan()
        {
            FRM_TD_Huongdan frm = new FRM_TD_Huongdan();
            frm.currentList = currentThutucNhiemvu.DsHuongdan;
            frm.Show();
            frm.Focus();
        }

        private void lookUpEdit7_EditValueChanged(object sender, EventArgs e)
        {
            LoadNguoinhanVanban();
        }

        private void lookUpEdit5_EditValueChanged(object sender, EventArgs e)
        {
            LoadNguoinhanVanban();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            LoadList(false);
        }

        private void lookUpEdit14_EditValueChanged(object sender, EventArgs e)
        {
            List<TD_Phancong_DoituongNhanVB> list = LoadDoituongNhanVB();
            lookUpEdit10.Properties.DataSource = list;
            lookUpEdit10.Properties.DisplayMember = "Description";
            lookUpEdit10.Properties.ValueMember = "ID";
            lookUpEdit10.Properties.BestFitRowCount = list == null ? 0 : list.Count;

            if (lookUpEdit14.EditValue.ToString() == "1") //Lãnh đạo cùng cấp
            {
                label12.Text = "Người nhận tiếp theo";
            }
            else if (lookUpEdit14.EditValue.ToString() == "2") //Đơn vị cấp dưới
            {
                label12.Text = "Đơn vị nhận";
            }
            else //Chuyển chuyên viên
            {
                label12.Text = "Chuyên viên nhận";
            }
        }

        private List<TD_Phancong_DoituongNhanVB> LoadDoituongNhanVB()
        {
            char phamviId = char.Parse(lookUpEdit14.EditValue.ToString());

            List<TD_Phancong_DoituongNhanVB> result = new List<TD_Phancong_DoituongNhanVB>();

            if (phamviId == '2')
            {
                foreach (TD_DonviQuanly dv in DsDonviCapduoi)
                    result.Add(new TD_Phancong_DoituongNhanVB(dv));
            }
            else
            {
                List<TD_Nguoiky> dsNK = Helpers.Trinhduyet.GetList_Nhansu(nguoisudungID, phamviId, '0');
                foreach (TD_Nguoiky nk in dsNK)
                    result.Add(new TD_Phancong_DoituongNhanVB(nk));
            }

            return result.Count == 0 ? null : result;
        }
    }
}
