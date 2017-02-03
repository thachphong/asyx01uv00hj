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
    public partial class FRM_TD_ThuchienNhiemvu : BaseForm_Data
    {
        private static Guid donvisudungID = All.gs_dv_quanly;
        private static string donvisudungName = All.gs_ten_dv_quanly;
        private static string MaForm = "1";

        private static List<TD_ThuchienNhiemvu> currentList = null;
        private static int currentRowSelected = int.MinValue;
        private static TD_ThuchienNhiemvu currentDataSelected = null;
        private static string currentState = "NORMAL";
        private static string tepDinhkem = string.Empty;
        private static DM_LoaiThutucNhiemvu_Noidung currentNoidung = null;
        private static List<TD_ThuchienNhiemvu_Truongdulieu> tempFields = null;
        private static DM_LoaiThutucNhiemvu currentThutucNhiemvu = null;
        public FRM_TD_ThuchienNhiemvu()
        {
            InitializeComponent();
        }

        private void FRM_TD_ThuchienNhiemvu_Load(object sender, EventArgs e)
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

            lblHeadTitle1.setText("Trình duyệt Thực hiện nhiệm vụ");
            lblHeadTitle1.alignCenter(lblHeadTitle1.Parent);
            panelHeader1.alignCenter(panelHeader1.Parent);
            panelHeader2.alignCenter(panelHeader2.Parent);

            label23.Text = donvisudungName;

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
            LoadPhanloaiNhiemvu();
            LoadDonviQuanly();
            LoadNoidungThutuc();
            LoadTrangthaiHoso();
        }

        private void LoadTrangthaiHoso()
        {
            List<TD_TrangthaiHoSo> list = Helpers.Trinhduyet.GetList_TrangthaiHoSo();
            lookUpEdit1.Properties.DataSource = list;
            lookUpEdit1.Properties.DisplayMember = "DM012403";
            lookUpEdit1.Properties.ValueMember = "DM012401";
            lookUpEdit1.Properties.BestFitRowCount = list == null ? 0 : list.Count;
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
                TD_DonviQuanly obj = (TD_DonviQuanly)lookUpEdit10.GetSelectedDataRow();
                list = obj.DSNguoiky;
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

            lookUpEdit5.Properties.DataSource = years;
            lookUpEdit5.Properties.BestFitRowCount = years.Count;
            lookUpEdit5.EditValue = DateTime.Now.Year;
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

            if (MessageBox.Show("Bạn chắc chắn muốn xóa " + listChecked.Count + " Nhiệm vụ đang chọn?", "Cảnh báo!", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                APIResponseData result = Helpers.TrinhduyetThuchienNhiemvu.Delete(listChecked);
                if (result == null)
                {
                    All.Show_message("Có lỗi trong quá trình cập nhật dữ liệu!");
                }
                else if (result.ErrorCode == 0)
                {
                    All.Show_message("Xóa thành công " + listChecked.Count + " Nhiệm vụ đã chọn!");
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

        void RefreshNewData(TD_ThuchienNhiemvu obj)
        {
            if (currentList == null) currentList = new List<TD_ThuchienNhiemvu>();

            TD_ThuchienNhiemvu current = currentList.FirstOrDefault(o => o.DM016701 == obj.DM016701);
            if (current == null) currentList.Insert(0, obj);
            else current = obj;
        }

        void btn_capnhat_Click(object sender, EventArgs e)
        {
            if (currentState == "NORMAL") return;

            if (!ValidateDetailForm()) return;

            TD_ThuchienNhiemvu obj = PrepareDetail();

            if (obj == null)
            {
                All.Show_message("Có lỗi trong quá trình cập nhật dữ liệu!");
                return;
            }

            if (currentState == "NEW")
            {
                APIResponseData result = Helpers.TrinhduyetThuchienNhiemvu.Create(obj);
                if (result == null)
                {
                    All.Show_message("Có lỗi trong quá trình cập nhật dữ liệu!");
                }
                else if (result.ErrorCode == 0)
                {
                    All.Show_message("Thêm mới thành công Trình duyệt Thực hiện nhiệm vụ: " + obj.DM016706);
                    currentState = "NORMAL";
                    RefreshNewData(JsonConvert.DeserializeObject<TD_ThuchienNhiemvu>(result.Data.ToString()));
                    LoadList();
                }
                else
                {
                    All.Show_message("Error code " + result.ErrorCode + ": " + result.Message);
                }
            }
            else
            {
                APIResponseData result = Helpers.TrinhduyetThuchienNhiemvu.Update(obj);
                if (result == null)
                {
                    All.Show_message("Có lỗi trong quá trình cập nhật dữ liệu!");
                }
                else if (result.ErrorCode == 0)
                {
                    All.Show_message("Cập nhật thành công Nhiệm vụ: " + obj.DM016706);
                    currentState = "NORMAL";
                    RefreshNewData(JsonConvert.DeserializeObject<TD_ThuchienNhiemvu>(result.Data.ToString()));
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

        private TD_ThuchienNhiemvu PrepareDetail()
        {
            try
            {
                TD_ThuchienNhiemvu obj = new TD_ThuchienNhiemvu();
                obj.DM016701 = currentState == "NEW" ? Guid.NewGuid() : currentDataSelected.DM016701;
                obj.DM016702 = All.gs_dv_quanly;
                obj.DM016703 = int.Parse(lookUpEdit6.EditValue.ToString());
                obj.DM016705 = Guid.Parse(lookUpEdit8.EditValue.ToString());
                obj.DM016706 = textEdit2.Text + "-" + textEdit3.Text;
                obj.DM016707 = dateEdit3.DateTime;
                obj.DM016708 = textEdit4.Text;
                obj.DM016710 = Guid.Parse(lookUpEdit11.EditValue.ToString());
                obj.DM016711 = memoEdit1.Text;
                obj.DM016713 = Guid.Parse(lookUpEdit13.EditValue.ToString());
                obj.DM016714 = tepDinhkem; //Tệp đính kèm
                MemoEdit mmeText = (MemoEdit)xtraScrollableControl1.Controls.Find("mmeText", true).FirstOrDefault();
                obj.DM016715 = mmeText == null ? string.Empty : mmeText.Text;
                obj.DM016716 = currentState == "NEW" ? All.gs_user_id : currentDataSelected.DM016716;
                obj.DM016717 = currentState == "NEW" ? DateTime.Now : currentDataSelected.DM016717;
                obj.DM016718 = All.gs_user_id;
                obj.DM016719 = DateTime.Now;
                obj.DM016720 = Guid.Parse(lookUpEdit1.EditValue.ToString());

                if (mmeText == null)//Nhập trường dữ liệu
                {
                    obj.Fields = new List<TD_ThuchienNhiemvu_Truongdulieu>();
                    foreach (TD_ThuchienNhiemvu_Truongdulieu f in tempFields)
                    {
                        f.DM016802 = obj.DM016701;
                        string ctrId = "ctrValue_" + f.DM016801.ToString();
                        Control ctrValue = xtraScrollableControl1.Controls.Find(ctrId, true).FirstOrDefault();
                        if (ctrValue == null) continue;

                        switch (f.Kieutruong)
                        {
                            case 1:
                                f.DM016804 = ((TextEdit)ctrValue).Text.Trim();
                                break;
                            case 2:
                                f.DM016804 = ((TextEdit)ctrValue).Text.Trim();
                                break;
                            case 3:
                                f.DM016804 = ((CheckEdit)ctrValue).Checked ? "1" : "0";
                                break;
                            case 4:
                                f.DM016804 = ((DateEdit)ctrValue).DateTime.ToString();
                                break;
                            case 5:
                                TimeEdit ctrTime = (TimeEdit)xtraScrollableControl1.Controls.Find("ctr_" + f.DM016801.ToString() + "_Time", true).FirstOrDefault();
                                DateEdit ctrDate = (DateEdit)xtraScrollableControl1.Controls.Find("ctr_" + f.DM016801.ToString() + "_Date", true).FirstOrDefault();
                                DateTime moment = new DateTime(ctrDate.DateTime.Year, ctrDate.DateTime.Month, ctrDate.DateTime.Day, ctrTime.Time.Hour, ctrTime.Time.Minute, ctrTime.Time.Second);
                                f.DM016804 = moment.ToString();
                                break;
                            case 6:
                                f.DM016804 = ((TimeEdit)ctrValue).Time.ToString();
                                break;
                            case 7:
                                f.DM016804 = ((MemoEdit)ctrValue).Text.Trim();
                                break;
                            case 8:
                                LookUpEdit lue = ((LookUpEdit)ctrValue);
                                f.DM016804 = lue.EditValue == null ? string.Empty : lue.EditValue.ToString();
                                break;
                            case 10:
                                ctrValue = xtraScrollableControl1.Controls.Find("ctr_" + f.DM016801.ToString() + "_Path", true).FirstOrDefault();
                                string filePath = ((TextEdit)ctrValue).Text.Trim();
                                if (File.Exists(filePath))
                                {
                                    string uri = Helpers.CreateRequestUrl_UploadFile();
                                    uri += "&n=td_thuchiennhiemvu_tepdinhkem&fn=" + DateTime.Now.Ticks.ToString();
                                    string response = WebUtils.Request_UploadFile(uri, filePath);
                                    APIResponseData result = JsonConvert.DeserializeObject<APIResponseData>(response);
                                    if (result.ErrorCode != 0)
                                    {
                                        All.Show_message("Error code " + result.ErrorCode + ": Lỗi không thể upload tệp!");
                                        return null;
                                    }
                                    f.DM016804 = result.Data.ToString();
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
                All.Show_message("Vui lòng chọn Trạng thái!");
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
                All.Show_message("Vui lòng nhập Số văn bản!");
                textEdit3.Focus();
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
                All.Show_message("Vui lòng chọn Cơ quan nhận!");
                lookUpEdit10.Focus();
                return false;
            }

            if (lookUpEdit11.EditValue.ToString() == Guid.Empty.ToString())
            {
                All.Show_message("Vui lòng chọn Người ký!");
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
                var list = tempFields.Where(o => o.Batbuocnhap);
                if (list.Count() > 0)
                {
                    foreach (TD_ThuchienNhiemvu_Truongdulieu obj in list)
                    {
                        if (obj.Kieutruong != 9 && obj.DM016804.Trim() == string.Empty)
                        {
                            All.Show_message("Vui lòng nhập đầy đủ các trường dữ liệu chi tiết!");
                            return false;
                        }

                        if (obj.Kieutruong == 9)
                        {
                            if (obj.Children == null || obj.Children.Count(o => o.Batbuocnhap && o.DM016804.Trim() == string.Empty) > 0)
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
            TD_ThuchienNhiemvu_Filter filter = new TD_ThuchienNhiemvu_Filter();
            filter.Nam = lookUpEdit5.EditValue == null ? 0 : int.Parse(lookUpEdit5.EditValue.ToString());
            filter.TuNgay = dateEdit1.EditValue == null ? DateTime.MinValue : dateEdit1.DateTime;
            filter.DenNgay = dateEdit2.EditValue == null ? DateTime.MinValue : dateEdit2.DateTime;
            filter.MaDanhmuc = lookUpEdit4.EditValue == null ? Guid.Empty : Guid.Parse(lookUpEdit4.EditValue.ToString());
            filter.MaPhanloai = lookUpEdit3.EditValue == null ? Guid.Empty : Guid.Parse(lookUpEdit3.EditValue.ToString());
            filter.Sovanban = textEdit1.Text.Trim();

            currentList = !refresh ?
                Helpers.TrinhduyetThuchienNhiemvu.GetList(filter) :
                currentList;

            gridControl1.DataSource = currentList;
            gridControl1.RefreshDataSource();
            gridView1.BestFitColumns();

            uC_MenuBtn1.set_status_menu(currentState, currentList == null ? 0 : currentList.Count);
            SetDetailFormEnable(false);

            TD_ThuchienNhiemvu current = (TD_ThuchienNhiemvu)gridView1.GetFocusedRow();
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
            currentDataSelected = (TD_ThuchienNhiemvu)gridView1.GetRow(currentRowSelected);
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

        private void AssignDetailFormValue(TD_ThuchienNhiemvu data)
        {
            lookUpEdit6.EditValue = data == null ? DateTime.Now.Year : data.DM016703;
            lookUpEdit7.EditValue = data == null ? Guid.Empty : data.MaPhanloaiNhiemvu;
            lookUpEdit8.EditValue = data == null ? Guid.Empty : data.DM016705;
            lookUpEdit1.EditValue = data == null ? Guid.Empty : data.DM016720;
            textEdit4.Text = data == null ? string.Empty : data.DM016708;

            if (data != null)
            {
                string[] sovanban = data.DM016706.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
                textEdit2.Text = sovanban[0];
                textEdit3.Text = sovanban[1];

                dateEdit3.EditValue = data.DM016707;
            }
            else
            {
                textEdit2.Text = string.Empty;
                textEdit3.Text = string.Empty;

                dateEdit3.EditValue = null;
            }

            lookUpEdit10.EditValue = data == null ? Guid.Empty : data.MaCoquannhan;
            lookUpEdit11.EditValue = data == null ? Guid.Empty : data.DM016710;
            memoEdit1.Text = data == null ? string.Empty : data.DM016711;
            lookUpEdit13.EditValue = data == null ? Guid.Empty : data.DM016713;

            GenerateContents();
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
            FRM_TD_ThuchienNhiemvu_AttachedFiles frm = new FRM_TD_ThuchienNhiemvu_AttachedFiles();
            this.Enabled = false;
            frm.currentState = currentState;
            frm.Show();
            frm.Focus();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            checkEdit1.Checked = false;
            LoadList();
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
            GenerateContents();
        }

        private void GenerateContents()
        {
            xtraScrollableControl1.Controls.Clear();
            if (currentNoidung == null) return;

            if (currentNoidung.DM016105 == '1')
            {
                MemoEdit memoEdit = new MemoEdit();
                memoEdit.Name = "mmeText";
                memoEdit.Dock = DockStyle.Fill;
                if (currentDataSelected != null) memoEdit.Text = currentDataSelected.DM016715;
                memoEdit.ReadOnly = currentState == "NORMAL";
                xtraScrollableControl1.Controls.Add(memoEdit);
            }
            else
            {
                tempFields =
                    currentDataSelected == null ? Helpers.TrinhduyetThuchienNhiemvu.GetList_Truongdulieu(currentNoidung.DM016101, Guid.Empty) :
                    currentDataSelected.Fields != null ? currentDataSelected.Fields :
                    Helpers.TrinhduyetThuchienNhiemvu.GetList_Truongdulieu(currentNoidung.DM016101, currentDataSelected.DM016701);

                panelHeader panelHeaderFields = GenerateDynamicFields(tempFields, currentState);

                if (panelHeaderFields != null)
                {
                    xtraScrollableControl1.Controls.Add(panelHeaderFields);
                    panelHeaderFields.alignCenter(panelHeaderFields.Parent);
                }
            }

            Refresh();
        }

        public void CallBack_UpdateField(TD_ThuchienNhiemvu_Truongdulieu field, bool update)
        {
            if (update)
            {
                TD_ThuchienNhiemvu_Truongdulieu f = tempFields.FirstOrDefault(o => o.DM016801 == field.DM016801);
                f.Children = field.Children;
            }

            this.Enabled = true;
            this.Focus();
        }

        public List<TD_ThuchienNhiemvu_Tepdinhkem> CallBack_Tepdinhkem_GetCurrentAttacheds()
        {
            return string.IsNullOrEmpty(tepDinhkem) ? null : JsonConvert.DeserializeObject<List<TD_ThuchienNhiemvu_Tepdinhkem>>(tepDinhkem);
        }

        public void CallBack_UpdateAttacheds(List<TD_ThuchienNhiemvu_Tepdinhkem> attacheds, bool update)
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
