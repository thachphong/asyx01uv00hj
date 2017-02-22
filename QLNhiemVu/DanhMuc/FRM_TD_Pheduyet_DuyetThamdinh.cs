using DBAccess;
using Decided.Libs;
using DevExpress.XtraEditors;
using Newtonsoft.Json;
using QLNhiemVu.FRMModel;
using QLNhiemvu_DBEntities;
using QLNhiemVu_Defines;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLNhiemVu.DanhMuc
{
    public partial class FRM_TD_Pheduyet_DuyetThamdinh : BaseForm_Data
    {
        private static Guid donvisudungID = All.gs_dv_quanly;
        private static string donvisudungName = All.gs_ten_dv_quanly;
        private static string MaForm = "4";

        public TD_ThuchienNhiemvu TD_Nhiemvu = null;
        private static List<TD_Pheduyet_Thamdinh_Duyet> currentList = null;
        private static int currentRowSelected = int.MinValue;
        private static TD_Pheduyet_Thamdinh_Duyet currentDataSelected = null;
        private static string currentState = "NORMAL";
        private static string tepDinhkem = string.Empty;
        private static DM_LoaiThutucNhiemvu_Noidung currentNoidung = null;
        //private static List<TD_Pheduyet_Thamdinh_Duyet_Truongdulieu> tempFields = null;
        private static DM_LoaiThutucNhiemvu currentThutucNhiemvu = null;
        private static byte currentID = 0;

        private static List<TD_Pheduyet_Thamdinh_Duyet_Truongdulieu> tempFields = null;
        private static List<TD_Thamdinh_Duyet_Truongdulieu> tempFields_Thamdinh = null;
        public FRM_TD_Pheduyet_DuyetThamdinh()
        {
            InitializeComponent();
        }

        private void FRM_TD_Pheduyet_DuyetThamdinh_Load(object sender, EventArgs e)
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
            lblHeadTitle1.Text = currentThutucNhiemvu.DM016004;
            label22.Text = currentThutucNhiemvu.DM016004;
            this.Text = currentThutucNhiemvu.DM016004;

            lblHeadTitle1.setText("Phê duyệt Nhiệm vụ từ văn bản có sẵn");
            lblHeadTitle1.alignCenter(lblHeadTitle1.Parent);
            panelHeader1.alignCenter(panelHeader1.Parent);
            panelHeader2.alignCenter(panelHeader2.Parent);

            BindControlEvents();

            currentState = "NORMAL";

            LoadTemplates();

            uC_MenuBtn1.set_status_menu(currentState, currentList == null ? 0 : currentList.Count);
            SetDetailFormEnable(false);
        }

        #region Load templates

        private void LoadTemplates()
        {
            dateEdit1.Properties.Mask.EditMask = All.dateFormat;
            dateEdit2.Properties.Mask.EditMask = All.dateFormat;
            dateEdit3.Properties.Mask.EditMask = All.dateFormat;

            LoadYears();
            LoadKyhieuVB();
            LoadDonviHethong();
            LoadPhanloaiNhiemvu();
            LoadDonviQuanly();
            LoadNoidungThutuc();
            LoadCapbanhanh();
        }

        private void LoadKyhieuVB()
        {
            List<Kyhieuvanban> list = Helpers.Trinhduyet_KyhieuVB.GetList();
            lookUpEdit2.Properties.DataSource = list;
            lookUpEdit2.Properties.DisplayMember = "Kyhieu";
            lookUpEdit2.Properties.ValueMember = "Id";
            lookUpEdit2.Properties.BestFitRowCount = list == null ? 0 : list.Count;
        }

        private void LoadDonviHethong()
        {
            List<TD_Thamdinh_Duyet_Phongban> list = new List<TD_Thamdinh_Duyet_Phongban>() {
                new TD_Thamdinh_Duyet_Phongban() { ID=All.gs_dv_quanly, Name=All.gs_ten_dv_quanly },
            };

            lookUpEdit1.Properties.DataSource = list;
            lookUpEdit1.Properties.DisplayMember = "Name";
            lookUpEdit1.Properties.ValueMember = "ID";
            lookUpEdit1.Properties.BestFitRowCount = list == null ? 0 : list.Count;
        }

        private void LoadCapbanhanh()
        {
            List<TD_Capbanhanh> list = Helpers.Trinhduyet.GetList_Capbanhanh(All.gs_dv_quanly);
            lookUpEdit10.Properties.DataSource = list;
            lookUpEdit10.Properties.DisplayMember = "DM010703";
            lookUpEdit10.Properties.ValueMember = "DM010701";
            lookUpEdit10.Properties.BestFitRowCount = list == null ? 0 : list.Count;
        }

        private void LoadNoidungThutuc()
        {
            List<DM_LoaiThutucNhiemvu_Noidung> list = currentThutucNhiemvu.DsNoidung;

            lookUpEdit13.Properties.DataSource = list;
            lookUpEdit13.Properties.DisplayMember = "DM016104";
            lookUpEdit13.Properties.ValueMember = "DM016101";
            lookUpEdit13.Properties.BestFitRowCount = list == null ? 0 : list.Count;
        }

        private void LoadNguoiky()
        {
            List<TD_Nguoiky> list = null;
            if ((Guid)lookUpEdit10.EditValue != Guid.Empty)
            {
                TD_Capbanhanh obj = (TD_Capbanhanh)lookUpEdit10.GetSelectedDataRow();
                list = obj.DSNguoibanhanh;
            }

            lookUpEdit11.Properties.DataSource = list;
            lookUpEdit11.Properties.DisplayMember = "DM030403";
            lookUpEdit11.Properties.ValueMember = "DM030401";
            lookUpEdit11.Properties.BestFitRowCount = list == null ? 0 : list.Count;
        }

        private void LoadDonviQuanly()
        {
            List<TD_DonviQuanly> list = Helpers.Trinhduyet.GetList_DonviQuanly();
            lookUpEdit10.Properties.DataSource = list;
            lookUpEdit10.Properties.DisplayMember = "DM030105";
            lookUpEdit10.Properties.ValueMember = "DM030101";
            lookUpEdit10.Properties.BestFitRowCount = list == null ? 0 : list.Count;
        }

        private void LoadDanhmucNhiemvu(LookUpEdit control)
        {
            List<TD_ThuchienNhiemvu_DanhmucNhiemvu> list = null;
            if ((Guid)control.EditValue != Guid.Empty)
            {
                TD_ThuchienNhiemvu_PhanloaiNhiemvu obj = (TD_ThuchienNhiemvu_PhanloaiNhiemvu)control.GetSelectedDataRow();
                list = obj.DSDanhmuc;
            }

            LookUpEdit effectControl = control.Name == "lookUpEdit3" ? lookUpEdit4 : lookUpEdit8;
            effectControl.Properties.DataSource = list;
            effectControl.Properties.DisplayMember = "DM020204";
            effectControl.Properties.ValueMember = "DM020201";
            effectControl.Properties.BestFitRowCount = list == null ? 0 : list.Count;
        }

        private void LoadPhanloaiNhiemvu()
        {
            List<TD_ThuchienNhiemvu_PhanloaiNhiemvu> list = Helpers.TrinhduyetThuchienNhiemvu.GetList_PhanloaiNhiemvu();
            lookUpEdit7.Properties.DataSource = list;
            lookUpEdit7.Properties.DisplayMember = "DM014003";
            lookUpEdit7.Properties.ValueMember = "DM014001";
            lookUpEdit7.Properties.BestFitRowCount = list == null ? 0 : list.Count;

            lookUpEdit3.Properties.DataSource = list;
            lookUpEdit3.Properties.DisplayMember = "DM014003";
            lookUpEdit3.Properties.ValueMember = "DM014001";
            lookUpEdit3.Properties.BestFitRowCount = list == null ? 0 : list.Count;
        }

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

            //lookUpEdit5.Properties.DataSource = years;
            //lookUpEdit5.Properties.BestFitRowCount = years.Count;
            //lookUpEdit5.EditValue = DateTime.Now.Year;
        }

        #endregion

        private void BindControlEvents()
        {
            gridColumn10.ColumnEdit.EditValueChanged += ColumnEdit_EditValueChanged;

            uC_MenuBtn1.btn_them.Click += btn_them_Click;
            uC_MenuBtn1.btn_thoat.Click += btn_thoat_Click;
            uC_MenuBtn1.btn_boqua.Click += btn_boqua_Click;
            uC_MenuBtn1.btn_capnhat.Click += btn_capnhat_Click;
            uC_MenuBtn1.btn_sua.Click += btn_sua_Click;
            uC_MenuBtn1.btn_xoa.Click += btn_xoa_Click;
        }

        void btn_xoa_Click(object sender, EventArgs e)
        {
            List<Guid> listChecked = GetIDChecked();
            if (listChecked == null)
            {
                All.Show_message("Vui lòng chọn trước khi xóa!");
                return;
            }

            if (MessageBox.Show("Bạn chắc chắn muốn xóa " + listChecked.Count + " Phê duyệt Nhiệm vụ đang chọn?", "Cảnh báo!", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                APIResponseData result = Helpers.TrinhduyetPheduyetTD.Delete(listChecked);
                if (result == null)
                {
                    All.Show_message("Có lỗi trong quá trình cập nhật dữ liệu!");
                }
                else if (result.ErrorCode == 0)
                {
                    All.Show_message("Xóa thành công " + listChecked.Count + " Phê duyệt Nhiệm vụ đã chọn!");
                    currentState = "NORMAL";
                    LoadList(false);
                }
                else
                {
                    All.Show_message("Error code " + result.ErrorCode + ": " + result.Message);
                }
            }
        }

        private List<Guid> GetIDChecked()
        {
            List<Guid> result = new List<Guid>();

            for (int i = 0; i < gridView1.RowCount; i++)
            {
                bool isChecked = (bool)gridView1.GetRowCellValue(i, gridColumn10);
                if (isChecked)
                    result.Add((Guid)gridView1.GetRowCellValue(i, gridColumn1));
            }

            return result.Count == 0 ? null : result;
        }

        void btn_sua_Click(object sender, EventArgs e)
        {
            currentState = "EDIT";
            uC_MenuBtn1.set_status_menu(currentState, currentList == null ? 0 : currentList.Count);
            AssignDetailFormValue(currentDataSelected);
            SetDetailFormEnable(true);
        }

        void RefreshNewData(TD_Pheduyet_Thamdinh_Duyet obj)
        {
            if (currentList == null) currentList = new List<TD_Pheduyet_Thamdinh_Duyet>();

            TD_Pheduyet_Thamdinh_Duyet current = currentList.FirstOrDefault(o => o.DM017501 == obj.DM017501);
            if (current == null) currentList.Insert(0, obj);
            else current = obj;
        }

        void btn_capnhat_Click(object sender, EventArgs e)
        {
            if (currentState == "NORMAL") return;

            if (!ValidateDetailForm()) return;

            TD_Pheduyet_Thamdinh_Duyet obj = PrepareDetail();

            if (obj == null)
            {
                All.Show_message("Có lỗi trong quá trình cập nhật dữ liệu!");
                return;
            }

            if (currentState == "NEW")
            {
                APIResponseData result = Helpers.TrinhduyetPheduyetTD.Create(obj);
                if (result == null)
                {
                    All.Show_message("Có lỗi trong quá trình cập nhật dữ liệu!");
                }
                else if (result.ErrorCode == 0)
                {
                    All.Show_message("Thêm mới thành công Phê duyệt Nhiệm vụ: " + obj.DM017505);
                    currentState = "NORMAL";
                    RefreshNewData(JsonConvert.DeserializeObject<TD_Pheduyet_Thamdinh_Duyet>(result.Data.ToString()));
                    LoadList();
                }
                else
                {
                    All.Show_message("Error code " + result.ErrorCode + ": " + result.Message);
                }
            }
            else
            {
                APIResponseData result = Helpers.TrinhduyetPheduyetTD.Update(obj);
                if (result == null)
                {
                    All.Show_message("Có lỗi trong quá trình cập nhật dữ liệu!");
                }
                else if (result.ErrorCode == 0)
                {
                    All.Show_message("Cập nhật thành công Phê duyệt Nhiệm vụ: " + obj.DM017505);
                    currentState = "NORMAL";
                    RefreshNewData(JsonConvert.DeserializeObject<TD_Pheduyet_Thamdinh_Duyet>(result.Data.ToString()));
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

        private TD_Pheduyet_Thamdinh_Duyet PrepareDetail()
        {
            try
            {
                TD_Pheduyet_Thamdinh_Duyet obj = new TD_Pheduyet_Thamdinh_Duyet();
                obj.DM017501 = currentState == "NEW" ? Guid.NewGuid() : currentDataSelected.DM017501;
                obj.DM017502 = All.gs_dv_quanly;
                obj.DM017503 = int.Parse(lookUpEdit6.EditValue.ToString());
                obj.DM017504 = Guid.Parse(lookUpEdit8.EditValue.ToString());
                obj.DM017505 = textEdit2.Text;
                obj.DM017506 = textEdit3.Text;
                obj.DM017507 = ((Kyhieuvanban)lookUpEdit2.GetSelectedDataRow()).Kyhieu + '-' + textEdit6.Text;
                obj.DM017508 = dateEdit3.DateTime;
                obj.DM017509 = textEdit4.Text;
                obj.DM017510 = Guid.Parse(lookUpEdit10.EditValue.ToString());
                obj.DM017511 = Guid.Parse(lookUpEdit11.EditValue.ToString());
                obj.DM017512 = Guid.Parse(lookUpEdit13.EditValue.ToString());
                obj.DM017513 = tepDinhkem;
                MemoEdit mmeText = (MemoEdit)xtraScrollableControl1.Controls.Find("mmeText", true).FirstOrDefault();
                obj.DM017514 = mmeText == null ? string.Empty : mmeText.Text;
                obj.DM017515 = currentState == "NEW" ? All.gs_user_id : currentDataSelected.DM017515;
                obj.DM017516 = currentState == "NEW" ? DateTime.Now : currentDataSelected.DM017516;
                obj.DM017517 = All.gs_user_id;
                obj.DM017518 = DateTime.Now;
                obj.DM017519 = currentID;

                if (mmeText == null)//Nhập trường dữ liệu
                {
                    obj.Fields = new List<TD_Pheduyet_Thamdinh_Duyet_Truongdulieu>();
                    foreach (TD_Pheduyet_Thamdinh_Duyet_Truongdulieu f in tempFields)
                    {
                        f.DM017602 = obj.DM017501;
                        string ctrId = "ctrValue_" + f.DM017601.ToString();
                        Control ctrValue = xtraScrollableControl1.Controls.Find(ctrId, true).FirstOrDefault();
                        if (ctrValue == null) continue;

                        switch (f.Kieutruong)
                        {
                            case 1:
                                f.DM017604 = ((TextEdit)ctrValue).Text.Trim();
                                break;
                            case 2:
                                f.DM017604 = ((TextEdit)ctrValue).Text.Trim();
                                break;
                            case 3:
                                f.DM017604 = ((CheckEdit)ctrValue).Checked ? "1" : "0";
                                break;
                            case 4:
                                f.DM017604 = ((DateEdit)ctrValue).DateTime.ToString();
                                break;
                            case 5:
                                TimeEdit ctrTime = (TimeEdit)xtraScrollableControl1.Controls.Find("ctr_" + f.DM017601.ToString() + "_Time", true).FirstOrDefault();
                                DateEdit ctrDate = (DateEdit)xtraScrollableControl1.Controls.Find("ctr_" + f.DM017601.ToString() + "_Date", true).FirstOrDefault();
                                DateTime moment = new DateTime(ctrDate.DateTime.Year, ctrDate.DateTime.Month, ctrDate.DateTime.Day, ctrTime.Time.Hour, ctrTime.Time.Minute, ctrTime.Time.Second);
                                f.DM017604 = moment.ToString();
                                break;
                            case 6:
                                f.DM017604 = ((TimeEdit)ctrValue).Time.ToString();
                                break;
                            case 7:
                                f.DM017604 = ((MemoEdit)ctrValue).Text.Trim();
                                break;
                            case 8:
                                LookUpEdit lue = ((LookUpEdit)ctrValue);
                                f.DM017604 = lue.EditValue == null ? string.Empty : lue.EditValue.ToString();
                                break;
                            case 10:
                                ctrValue = xtraScrollableControl1.Controls.Find("ctr_" + f.DM017601.ToString() + "_Path", true).FirstOrDefault();
                                string filePath = ((TextEdit)ctrValue).Text.Trim();
                                if (File.Exists(filePath))
                                {
                                    string uri = Helpers.CreateRequestUrl_UploadFile();
                                    uri += "&n=td_pheduyetnhiemvutd_tepdinhkem&fn=" + DateTime.Now.Ticks.ToString();
                                    string response = WebUtils.Request_UploadFile(uri, filePath);
                                    APIResponseData result = JsonConvert.DeserializeObject<APIResponseData>(response);
                                    if (result.ErrorCode != 0)
                                    {
                                        All.Show_message("Error code " + result.ErrorCode + ": Lỗi không thể upload tệp!");
                                        return null;
                                    }
                                    f.DM017604 = result.Data.ToString();
                                }
                                break;
                            default: break;
                        }

                        obj.Fields.Add(f);
                    }
                }

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
            if (lookUpEdit6.EditValue == null)
            {
                All.Show_message("Vui lòng chọn Năm!");
                lookUpEdit6.Focus();
                return false;
            }

            if (lookUpEdit1.EditValue.ToString() == Guid.Empty.ToString())
            {
                All.Show_message("Vui lòng chọn Đơn vị hệ thống!");
                lookUpEdit1.Focus();
                return false;
            }

            if (lookUpEdit7.EditValue.ToString() == Guid.Empty.ToString())
            {
                All.Show_message("Vui lòng chọn Phân loại Nhiệm vụ!");
                lookUpEdit7.Focus();
                return false;
            }

            if (lookUpEdit8.EditValue.ToString() == Guid.Empty.ToString())
            {
                All.Show_message("Vui lòng chọn Danh mục Nhiệm vụ!");
                lookUpEdit8.Focus();
                return false;
            }

            if (textEdit2.Text.Trim() == string.Empty)
            {
                All.Show_message("Vui lòng nhập Số văn bản!");
                textEdit2.Focus();
                return false;
            }
            if (textEdit3.Text.Trim() == string.Empty)
            {
                All.Show_message("Vui lòng nhập Tên văn bản!");
                textEdit3.Focus();
                return false;
            }
            if (textEdit6.Text.Trim() == string.Empty)
            {
                All.Show_message("Vui lòng nhập Số văn bản duyệt!");
                textEdit6.Focus();
                return false;
            }

            if (dateEdit3.EditValue == null)
            {
                All.Show_message("Vui lòng chọn Ngày!");
                dateEdit3.Focus();
                return false;
            }

            if (textEdit4.Text.Trim() == string.Empty)
            {
                All.Show_message("Vui lòng nhập Trích yếu!");
                textEdit4.Focus();
                return false;
            }

            if (lookUpEdit10.EditValue.ToString() == Guid.Empty.ToString())
            {
                All.Show_message("Vui lòng chọn Cấp ban hành!");
                lookUpEdit10.Focus();
                return false;
            }

            if (lookUpEdit11.EditValue.ToString() == Guid.Empty.ToString())
            {
                All.Show_message("Vui lòng chọn Người ban hành!");
                lookUpEdit11.Focus();
                return false;
            }

            if (lookUpEdit13.EditValue.ToString() == Guid.Empty.ToString())
            {
                All.Show_message("Vui lòng chọn Nội dung Nhiệm vụ!");
                lookUpEdit13.Focus();
                return false;
            }

            if (currentNoidung.DM016105 == '1')
            {
                MemoEdit mmeText = (MemoEdit)xtraScrollableControl1.Controls.Find("mmeText", true).FirstOrDefault();
                if (mmeText == null || mmeText.Text.Trim() == string.Empty)
                {
                    All.Show_message("Vui lòng nhập Nội dung chi tiết!");
                    if (mmeText != null) mmeText.Focus();
                    return false;
                }
            }
            else
            {
                PrepareDetail();
                var list = tempFields.Where(o => o.Batbuocnhap || o.Kieutruong == 9);
                if (list.Count() > 0)
                {
                    foreach (TD_Pheduyet_Thamdinh_Duyet_Truongdulieu obj in list)
                    {
                        if (obj.Kieutruong != 9 && obj.DM017604.Trim() == string.Empty)
                        {
                            All.Show_message("Vui lòng nhập đầy đủ các trường dữ liệu chi tiết!");
                            return false;
                        }

                        if (obj.Kieutruong == 9)
                        {
                            if (obj.Children == null || obj.Children.Count(o => o.Batbuocnhap && o.DM017604.Trim() == string.Empty) > 0)
                            {
                                All.Show_message("Vui lòng nhập đầy đủ các trường dữ liệu chi tiết!");
                                return false;
                            }
                        }
                    }
                }
            }


            return true;
        }

        void btn_boqua_Click(object sender, EventArgs e)
        {
            currentState = "NORMAL";
            uC_MenuBtn1.set_status_menu(currentState, currentList == null ? 0 : currentList.Count);
            AssignDetailFormValue(currentDataSelected);
            SetDetailFormEnable(false);
        }

        void btn_thoat_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        void btn_them_Click(object sender, EventArgs e)
        {
            currentState = "NEW";
            uC_MenuBtn1.set_status_menu(currentState, 0);
            currentID = Helpers.TrinhduyetPheduyetTD.GetCurrentID();
            AssignDetailFormValue(null);
            SetDetailFormEnable(true);
        }

        bool performChecked = true;
        void ColumnEdit_EditValueChanged(object sender, EventArgs e)
        {
            gridView1.PostEditor();

            CheckEdit cbxEdit = (CheckEdit)sender;
            if (!cbxEdit.Checked)
            {
                performChecked = false;
                checkEdit1.Checked = false;
            }
            else
            {
                bool allChecked = true;
                for (int i = 0; i < gridView1.RowCount; i++)
                {
                    allChecked = (bool)gridView1.GetRowCellValue(i, gridColumn10);
                    if (!allChecked) return;
                }

                if (allChecked)
                    checkEdit1.Checked = true;
            }
        }

        private void LoadList(bool refresh = true)
        {
            TD_Pheduyet_Thamdinh_Duyet_Filter filter = new TD_Pheduyet_Thamdinh_Duyet_Filter();
            filter.TuNgay = dateEdit1.EditValue == null ? DateTime.MinValue : dateEdit1.DateTime;
            filter.DenNgay = dateEdit2.EditValue == null ? DateTime.MinValue : dateEdit2.DateTime;
            filter.MaDanhmuc = lookUpEdit4.EditValue == null ? Guid.Empty : Guid.Parse(lookUpEdit4.EditValue.ToString());
            filter.MaPhanloai = lookUpEdit3.EditValue == null ? Guid.Empty : Guid.Parse(lookUpEdit3.EditValue.ToString());
            filter.Sovanban = textEdit1.Text.Trim();

            currentList = !refresh ?
                Helpers.TrinhduyetPheduyetTD.GetList(filter) :
                currentList;

            gridControl1.DataSource = currentList;
            gridControl1.RefreshDataSource();
            gridView1.BestFitColumns();

            uC_MenuBtn1.set_status_menu(currentState, currentList == null ? 0 : currentList.Count);
            SetDetailFormEnable(false);

            TD_Pheduyet_Thamdinh_Duyet current = (TD_Pheduyet_Thamdinh_Duyet)gridView1.GetFocusedRow();
            AssignDetailFormValue(current);
        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            if (performChecked)
            {
                for (int i = 0; i < gridView1.RowCount; i++)
                {
                    gridView1.SetRowCellValue(i, gridColumn10, checkEdit1.Checked);
                }
            }

            performChecked = true;
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            currentRowSelected = e.FocusedRowHandle;
            currentDataSelected = (TD_Pheduyet_Thamdinh_Duyet)gridView1.GetRow(currentRowSelected);
            AssignDetailFormValue(currentDataSelected);
        }

        private void SetDetailFormEnable(bool isEnable)
        {
            foreach (Control ctrl in panelHeader1.Controls)
            {
                Type t = ctrl.GetType();
                if (t.GetProperty("ReadOnly") != null)
                {
                    t.GetProperty("ReadOnly").SetValue(ctrl, !isEnable, null);
                }
                else
                    ctrl.Enabled = isEnable;
            }

            groupControl1.Enabled = !isEnable;
            panelControl1.Enabled = !isEnable;
        }

        private void AssignDetailFormValue(TD_Pheduyet_Thamdinh_Duyet data)//
        {
            currentID = data == null ? currentID : data.DM017519;
            lookUpEdit1.EditValue = data == null ? Guid.Empty : data.DM017502;
            lookUpEdit6.EditValue = data == null ? DateTime.Now.Year : data.DM017503;
            lookUpEdit7.EditValue = data == null ? Guid.Empty : data.MaPhanloaiNhiemvu;
            lookUpEdit8.EditValue = data == null ? Guid.Empty : data.DM017504;
            textEdit2.Text = currentID.ToString("D2");
            textEdit3.Text = data == null ? string.Empty : data.DM017506;
            textEdit6.Text = data == null ? string.Empty : data.DM017507.Split('-')[1];
            dateEdit3.DateTime = data == null ? DateTime.Now : data.DM017508;
            textEdit4.Text = data == null ? string.Empty : data.DM017509;
            lookUpEdit10.EditValue = data == null ? Guid.Empty : data.DM017510;
            lookUpEdit11.EditValue = data == null ? Guid.Empty : data.DM017511;
            lookUpEdit13.EditValue = data == null ? Guid.Empty : data.DM017512;
            textEdit5.Text = data == null ? string.Empty : data.Chucvu;
            lookUpEdit2.EditValue = data == null ? 0 : data.KyhieuId;

            GenerateContents(data);
        }

        private void ShowChildForm_Huongdan()
        {
            FRM_TD_Huongdan frm = new FRM_TD_Huongdan();
            frm.currentList = currentThutucNhiemvu.DsHuongdan;
            frm.Show();
            frm.Focus();
        }

        private void ShowChildForm_Tepdinhkem()
        {
            FRM_TD_PheduyetVB_AttachedFiles frm = new FRM_TD_PheduyetVB_AttachedFiles();
            this.Enabled = false;
            frm.currentState = currentState;
            frm.Show();
            frm.Focus();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            checkEdit1.Checked = false;
            LoadList(false);
        }

        private void lookUpEdit3_EditValueChanged(object sender, EventArgs e)
        {
            LoadDanhmucNhiemvu(lookUpEdit3);
        }

        private void lookUpEdit7_EditValueChanged(object sender, EventArgs e)
        {
            LoadDanhmucNhiemvu(lookUpEdit7);
        }

        private void lookUpEdit10_EditValueChanged(object sender, EventArgs e)
        {
            LoadNguoiky();
        }

        private void lookUpEdit11_EditValueChanged(object sender, EventArgs e)
        {
            if ((Guid)lookUpEdit11.EditValue == Guid.Empty)
                textEdit5.Text = string.Empty;
            else
            {
                TD_Nguoiky nguoiky = (TD_Nguoiky)lookUpEdit11.GetSelectedDataRow();
                textEdit5.Text = nguoiky.Chucvu;
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            ShowChildForm_Huongdan();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            ShowChildForm_Tepdinhkem();
        }

        private void lookUpEdit13_EditValueChanged(object sender, EventArgs e)
        {
            //Nội dung
            currentNoidung = (DM_LoaiThutucNhiemvu_Noidung)lookUpEdit13.GetSelectedDataRow();
            GenerateContents(currentDataSelected);
        }

        private void GenerateContents(TD_Pheduyet_Thamdinh_Duyet objData)
        {
            xtraScrollableControl1.Controls.Clear();
            xtraScrollableControl2.Controls.Clear();
            if (currentNoidung == null) return;

            //if (currentNoidung.DM016105 == '1')
            //{
            //    MemoEdit memoEdit = new MemoEdit();
            //    memoEdit.Name = "mmeText";
            //    memoEdit.Dock = DockStyle.Fill;
            //    if (currentDataSelected != null) memoEdit.Text = currentDataSelected.DM017514;
            //    memoEdit.ReadOnly = currentState == "NORMAL";
            //    xtraScrollableControl1.Controls.Add(memoEdit);
            //}
            //else
            //{
            //    tempFields =
            //        currentDataSelected == null ? Helpers.TrinhduyetPheduyetTD.GetList_Truongdulieu(currentNoidung.DM016101, Guid.Empty) :
            //        currentDataSelected.Fields != null ? currentDataSelected.Fields :
            //        Helpers.TrinhduyetPheduyetTD.GetList_Truongdulieu(currentNoidung.DM016101, currentDataSelected.DM017501);

            //    panelHeader panelHeaderFields = GenerateDynamicFields_PheduyetVB(tempFields, currentState);

            //    if (panelHeaderFields != null)
            //    {
            //        xtraScrollableControl1.Controls.Add(panelHeaderFields);
            //        panelHeaderFields.alignCenter(panelHeaderFields.Parent);
            //    }
            //}

            TD_Thamdinh_Duyet_FilterOne filter = new TD_Thamdinh_Duyet_FilterOne()
            {
                DonviId = Guid.Parse(lookUpEdit1.EditValue.ToString()),
                MaDanhmuc = Guid.Parse(lookUpEdit8.EditValue.ToString()),
                MaNoidung = Guid.Parse(lookUpEdit13.EditValue.ToString()),
                Nam = int.Parse(lookUpEdit6.EditValue.ToString()),
                Nguoibanhanh = Guid.Parse(lookUpEdit11.EditValue.ToString()),
            };

            TD_Thamdinh_Duyet duyet = Helpers.TrinhduyetThamdinh.Get_DuyetThamdinh(filter);

            if (currentNoidung.DM016105 == '1')
            {
                MemoEdit memoEdit = new MemoEdit();
                memoEdit.Name = "mmeText";
                memoEdit.Dock = DockStyle.Fill;
                if (duyet != null) memoEdit.Text = duyet.DM017121;
                memoEdit.ReadOnly = currentState == "NORMAL";
                xtraScrollableControl2.Controls.Add(memoEdit);

                MemoEdit memoEdit_Thamdinh = new MemoEdit();
                memoEdit_Thamdinh.Name = "mmeText_Thamdinh";
                memoEdit_Thamdinh.Dock = DockStyle.Fill;
                if (objData != null) memoEdit_Thamdinh.Text = objData.DM017514;
                //memoEdit_Thamdinh.ReadOnly = !isEditing;
                xtraScrollableControl2.Controls.Add(memoEdit_Thamdinh);
            }
            else
            {
                tempFields_Thamdinh =
                    duyet == null ? Helpers.TrinhduyetThamdinh.GetList_Truongdulieu(currentNoidung.DM016101, Guid.Empty, TD_Nhiemvu.DM016701) :
                    duyet.Fields != null ? duyet.Fields :
                    Helpers.TrinhduyetThamdinh.GetList_Truongdulieu(currentNoidung.DM016101, duyet.DM017101, duyet.DM017109);

                panelHeader panelHeaderFields = GenerateDynamicFields_Thamdinh(tempFields_Thamdinh);

                if (panelHeaderFields != null)
                {
                    xtraScrollableControl1.Controls.Add(panelHeaderFields);
                    panelHeaderFields.alignCenter(panelHeaderFields.Parent);
                }

                //Pheduyet_Thamdinh
                tempFields =
                    objData == null ? Helpers.TrinhduyetPheduyetTD.GetList_Truongdulieu(currentNoidung.DM016101, Guid.Empty) :
                    objData.Fields != null ? objData.Fields :
                    Helpers.TrinhduyetPheduyetTD.GetList_Truongdulieu(currentNoidung.DM016101, objData.DM017501);

                panelHeader panelHeaderFields_Thamdinh = GenerateDynamicFields_PheduyetThamdinh(tempFields, "EDIT");

                if (panelHeaderFields_Thamdinh != null)
                {
                    xtraScrollableControl2.Controls.Add(panelHeaderFields_Thamdinh);
                    panelHeaderFields_Thamdinh.alignCenter(panelHeaderFields_Thamdinh.Parent);
                }
            }

            Refresh();
        }

        public void CallBack_UpdateField(TD_Pheduyet_Thamdinh_Duyet_Truongdulieu field, bool update)
        {
            if (update)
            {
                TD_Pheduyet_Thamdinh_Duyet_Truongdulieu f = tempFields.FirstOrDefault(o => o.DM017601 == field.DM017601);
                f.Children = field.Children;
            }

            this.Enabled = true;
            this.Focus();
        }

        public List<TD_Pheduyet_Thamdinh_Duyet_Tepdinhkem> CallBack_Tepdinhkem_GetCurrentAttacheds()
        {
            return string.IsNullOrEmpty(tepDinhkem) ? null : JsonConvert.DeserializeObject<List<TD_Pheduyet_Thamdinh_Duyet_Tepdinhkem>>(tepDinhkem);
        }

        public void CallBack_UpdateAttacheds(List<TD_Pheduyet_Thamdinh_Duyet_Tepdinhkem> attacheds, bool update)
        {
            if (update)
            {
                tepDinhkem = attacheds == null ? string.Empty : JsonConvert.SerializeObject(attacheds);
            }

            this.Enabled = true;
            this.Focus();
        }
    }
}
