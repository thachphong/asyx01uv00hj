using DBAccess;
using DevExpress.XtraEditors;
using Newtonsoft.Json;
using QLNhiemVu.FRMModel;
using QLNhiemVu.User_Control;
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
    public partial class FRM_TD_Thamdinh_Duyet : BaseForm_Data
    {
        public TD_ThuchienNhiemvu TD_Nhiemvu = null;

        private static Guid donvisudungID = All.gs_dv_quanly;
        private static string donvisudungName = All.gs_ten_dv_quanly;
        private static Guid nguoisudungID = All.gs_user_id;
        private static string nguoisudungName = All.gs_user_name;
        //private static string MaForm = "1";
        //private static DM_LoaiThutucNhiemvu currentThutucNhiemvu = null;
        public string currentState = "NORMAL";
        private static List<TD_Thamdinh_Duyet> currentList = null;
        private static TD_Thamdinh_Duyet currentDataSelected = null;
        private static int currentRowSelected = -1;
        private static Control iControl = null;
        private static List<TD_Thamdinh_Duyet_Truongdulieu> tempFields = null;
        public FRM_TD_Thamdinh_Duyet()
        {
            InitializeComponent();
        }

        private void FRM_TD_Thamdinh_Duyet_Load(object sender, EventArgs e)
        {
            if (TD_Nhiemvu == null)
            {
                this.Dispose();
                return;
            }

            label8.Text = donvisudungName;
            lblHeadTitle1.setText("Phê duyệt");
            lblHeadTitle1.alignCenter(lblHeadTitle1.Parent);
            panelHeader1.alignCenter(panelHeader1.Parent);
            panelHeader2.alignCenter(panelHeader2.Parent);

            BindControlEvents();

            currentState = "NORMAL";

            LoadTemplates();
            LoadList(false);

            uC_MenuBtn1.set_status_menu(currentState, currentList == null ? 0 : currentList.Count);
            SetDetailFormEnable(false);
        }

        private void SetDetailFormEnable(bool isEnable)
        {
            //foreach (Control ctrl in panelHeader1.Controls)
            //{
            //    Type t = ctrl.GetType();
            //    if (t.GetProperty("ReadOnly") != null)
            //    {
            //        t.GetProperty("ReadOnly").SetValue(ctrl, !isEnable, null);
            //    }
            //    else
            //        ctrl.Enabled = isEnable;
            //}

            groupControl1.Enabled = !isEnable;
            panelControl1.Enabled = !isEnable;

            groupControl2.Enabled = isEnable;
            panelControl2.Enabled = isEnable;
        }

        #region Load templates

        private void LoadTemplates()
        {
            dateEdit1.Properties.Mask.EditMask = All.dateFormat;
            dateEdit2.Properties.Mask.EditMask = All.dateFormat;
            dateEdit3.Properties.Mask.EditMask = All.dateFormat;

            LoadYears();
            LoadPhongbanbophan();
            LoadPhanloaiNhiemvu();
            LoadDonviQuanly();
            LoadThutucNhiemvu();
            //LoadTrangthaiHoso();
        }

        private void LoadPhongbanbophan()
        {
            List<DM_LoaiThutucNhiemvu> list = Helpers.ThutucNhiemvu.GetList();

            lookUpEdit10.Properties.DataSource = list;
            lookUpEdit10.Properties.DisplayMember = "DM016004";
            lookUpEdit10.Properties.ValueMember = "DM016001";
            lookUpEdit10.Properties.BestFitRowCount = list == null ? 0 : list.Count;
        }

        private void LoadThutucNhiemvu()
        {
            List<TD_Thamdinh_Duyet_Phongban> list = new List<TD_Thamdinh_Duyet_Phongban>() {
                new TD_Thamdinh_Duyet_Phongban(){ ID=All.gs_dv_quanly, Name=All.gs_ten_dv_quanly},
            };

            lookUpEdit1.Properties.DataSource = list;
            lookUpEdit1.Properties.DisplayMember = "Name";
            lookUpEdit1.Properties.ValueMember = "ID";
            lookUpEdit1.Properties.BestFitRowCount = list == null ? 0 : list.Count;
        }

        //private void LoadNguoiky()
        //{
        //    List<TD_Nguoiky> list = null;
        //    if ((Guid)lookUpEdit10.EditValue != Guid.Empty)
        //    {
        //        TD_DonviQuanly obj = (TD_DonviQuanly)lookUpEdit10.GetSelectedDataRow();
        //        list = obj.DSNguoiky;
        //    }

        //    lookUpEdit11.Properties.DataSource = list;
        //    lookUpEdit11.Properties.DisplayMember = "DM030403";
        //    lookUpEdit11.Properties.ValueMember = "DM030401";
        //    lookUpEdit11.Properties.BestFitRowCount = list == null ? 0 : list.Count;
        //}

        private void LoadDonviQuanly()
        {
            List<TD_DonviQuanly> list = Helpers.Trinhduyet.GetList_DonviQuanly();
            lookUpEdit1.Properties.DataSource = list;
            lookUpEdit1.Properties.DisplayMember = "DM030105";
            lookUpEdit1.Properties.ValueMember = "DM030101";
            lookUpEdit1.Properties.BestFitRowCount = list == null ? 0 : list.Count;
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

        private void LoadUC(TD_Thamdinh_Duyet obj)
        {
            groupControl2.Controls.Clear();

            iControl = null;
            if (checkEdit2.Checked)
                iControl = new UC_Thamdinh_Duyet_Xinykien();
            if (checkEdit3.Checked)
                iControl = new UC_Thamdinh_Duyet_Tralaihoso();
            if (checkEdit4.Checked)
                iControl = new UC_Thamdinh_Duyet_Yeucaubosung();
            if (checkEdit5.Checked)
                iControl = new UC_Thamdinh_Duyet_Thamdinhvatrinh();

            if (iControl != null)
            {
                iControl.Dock = DockStyle.Fill;
                groupControl2.Controls.Add(iControl);
                ((I_UC_Thamdinh_Duyet_Chitiet)iControl).RefreshUI();

                ((CB_Thamdinh_Duyet_Chitiet)iControl).TD_Nhiemvu = TD_Nhiemvu;
                ((I_UC_Thamdinh_Duyet_Chitiet)iControl).AssignData(obj);
            }

            Refresh();
        }

        private void checkEdit2_CheckedChanged(object sender, EventArgs e)
        {
            LoadUC(currentDataSelected);
        }

        private void checkEdit3_CheckedChanged(object sender, EventArgs e)
        {
            LoadUC(currentDataSelected);
        }

        private void checkEdit4_CheckedChanged(object sender, EventArgs e)
        {
            LoadUC(currentDataSelected);
        }

        private void checkEdit5_CheckedChanged(object sender, EventArgs e)
        {
            LoadUC(currentDataSelected);
        }

        private void lookUpEdit7_EditValueChanged(object sender, EventArgs e)
        {
            LoadDanhmucNhiemvu((LookUpEdit)sender);
        }

        private void lookUpEdit3_EditValueChanged(object sender, EventArgs e)
        {
            LoadDanhmucNhiemvu((LookUpEdit)sender);
        }

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

            if (MessageBox.Show("Bạn chắc chắn muốn xóa " + listChecked.Count + " Duyệt thẩm định đang chọn?", "Cảnh báo!", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                APIResponseData result = Helpers.TrinhduyetThamdinh.Delete_Duyet(listChecked);
                if (result == null)
                {
                    All.Show_message("Có lỗi trong quá trình cập nhật dữ liệu!");
                }
                else if (result.ErrorCode == 0)
                {
                    All.Show_message("Xóa thành công " + listChecked.Count + " Duyệt thẩm định đã chọn!");
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

        void RefreshNewData(TD_Thamdinh_Duyet obj)
        {
            if (currentList == null) currentList = new List<TD_Thamdinh_Duyet>();

            TD_Thamdinh_Duyet current = currentList.FirstOrDefault(o => o.DM017101 == obj.DM017101);
            if (current == null) currentList.Insert(0, obj);
            else current = obj;
        }

        void btn_capnhat_Click(object sender, EventArgs e)
        {
            if (currentState == "NORMAL") return;

            //if (!ValidateDetailForm()) return;

            TD_Thamdinh_Duyet obj = PrepareDetail();

            if (obj == null)
            {
                All.Show_message("Có lỗi trong quá trình cập nhật dữ liệu!");
                return;
            }

            if (currentState == "NEW")
            {
                APIResponseData result = Helpers.TrinhduyetThamdinh.Create_Duyet(obj);
                if (result == null)
                {
                    All.Show_message("Có lỗi trong quá trình cập nhật dữ liệu!");
                }
                else if (result.ErrorCode == 0)
                {
                    All.Show_message("Thêm mới thành công Duyệt thẩm định: " + obj.DM017105);
                    currentState = "NORMAL";
                    RefreshNewData(JsonConvert.DeserializeObject<TD_Thamdinh_Duyet>(result.Data.ToString()));
                    LoadList();
                }
                else
                {
                    All.Show_message("Error code " + result.ErrorCode + ": " + result.Message);
                }
            }
            else
            {
                APIResponseData result = Helpers.TrinhduyetThamdinh.Update_Duyet(obj);
                if (result == null)
                {
                    All.Show_message("Có lỗi trong quá trình cập nhật dữ liệu!");
                }
                else if (result.ErrorCode == 0)
                {
                    All.Show_message("Cập nhật thành công Duyệt thẩm định: " + obj.DM017105);
                    currentState = "NORMAL";
                    RefreshNewData(JsonConvert.DeserializeObject<TD_Thamdinh_Duyet>(result.Data.ToString()));
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

        private TD_Thamdinh_Duyet PrepareDetail()
        {
            try
            {
                TD_Thamdinh_Duyet obj = new TD_Thamdinh_Duyet();
                obj.DM017101 = currentState == "NEW" ? Guid.NewGuid() : currentDataSelected.DM017101;
                obj.DM017102 = All.gs_dv_quanly;
                obj.DM017103 = int.Parse(lookUpEdit6.EditValue.ToString());
                obj.DM017104 = Guid.Parse(lookUpEdit8.EditValue.ToString());
                obj.DM017105 = textEdit2.Text;
                obj.DM017106 = dateEdit3.DateTime;
                obj.DM017107 = textEdit5.Text;
                obj.DM017108 = All.gs_user_id;
                obj.DM017109 = TD_Nhiemvu.DM016701;
                obj.DM017110 = checkEdit2.Checked ? '1' : checkEdit3.Checked ? '2' : checkEdit4.Checked ? '3' : '4';
                //obj.DM017111 = memoEdit1.Text;
                //obj.DM017112 = memoEdit1.Text;
                //obj.DM017113 = Guid.Parse(lookUpEdit13.EditValue.ToString());
                //obj.DM017114 = tepDinhkem; //Tệp đính kèm
                //MemoEdit mmeText = (MemoEdit)xtraScrollableControl1.Controls.Find("mmeText", true).FirstOrDefault();
                //obj.DM017115 = mmeText == null ? string.Empty : mmeText.Text;
                //obj.DM017116 = currentState == "NEW" ? All.gs_user_id : currentDataSelected.DM017116;
                //obj.DM017117 = currentState == "NEW" ? DateTime.Now : currentDataSelected.DM017117;
                //obj.DM017118 = All.gs_user_id;
                //obj.DM017119 = DateTime.Now;
                obj.DM017120 = Guid.Parse(lookUpEdit10.EditValue.ToString());

                ((I_UC_Thamdinh_Duyet_Chitiet)iControl).FillData(ref obj);

                //if (mmeText == null)//Nhập trường dữ liệu
                //{
                //    obj.Fields = new List<TD_ThuchienNhiemvu_Truongdulieu>();
                //    foreach (TD_ThuchienNhiemvu_Truongdulieu f in tempFields)
                //    {
                //        f.DM016802 = obj.DM016701;
                //        string ctrId = "ctrValue_" + f.DM016801.ToString();
                //        Control ctrValue = xtraScrollableControl1.Controls.Find(ctrId, true).FirstOrDefault();
                //        if (ctrValue == null) continue;

                //        switch (f.Kieutruong)
                //        {
                //            case 1:
                //                f.DM016804 = ((TextEdit)ctrValue).Text.Trim();
                //                break;
                //            case 2:
                //                f.DM016804 = ((TextEdit)ctrValue).Text.Trim();
                //                break;
                //            case 3:
                //                f.DM016804 = ((CheckEdit)ctrValue).Checked ? "1" : "0";
                //                break;
                //            case 4:
                //                f.DM016804 = ((DateEdit)ctrValue).DateTime.ToString();
                //                break;
                //            case 5:
                //                TimeEdit ctrTime = (TimeEdit)xtraScrollableControl1.Controls.Find("ctr_" + f.DM016801.ToString() + "_Time", true).FirstOrDefault();
                //                DateEdit ctrDate = (DateEdit)xtraScrollableControl1.Controls.Find("ctr_" + f.DM016801.ToString() + "_Date", true).FirstOrDefault();
                //                DateTime moment = new DateTime(ctrDate.DateTime.Year, ctrDate.DateTime.Month, ctrDate.DateTime.Day, ctrTime.Time.Hour, ctrTime.Time.Minute, ctrTime.Time.Second);
                //                f.DM016804 = moment.ToString();
                //                break;
                //            case 6:
                //                f.DM016804 = ((TimeEdit)ctrValue).Time.ToString();
                //                break;
                //            case 7:
                //                f.DM016804 = ((MemoEdit)ctrValue).Text.Trim();
                //                break;
                //            case 8:
                //                LookUpEdit lue = ((LookUpEdit)ctrValue);
                //                f.DM016804 = lue.EditValue == null ? string.Empty : lue.EditValue.ToString();
                //                break;
                //            case 10:
                //                ctrValue = xtraScrollableControl1.Controls.Find("ctr_" + f.DM016801.ToString() + "_Path", true).FirstOrDefault();
                //                string filePath = ((TextEdit)ctrValue).Text.Trim();
                //                if (File.Exists(filePath))
                //                {
                //                    string uri = Helpers.CreateRequestUrl_UploadFile();
                //                    uri += "&n=td_thuchiennhiemvu_tepdinhkem&fn=" + DateTime.Now.Ticks.ToString();
                //                    string response = WebUtils.Request_UploadFile(uri, filePath);
                //                    APIResponseData result = JsonConvert.DeserializeObject<APIResponseData>(response);
                //                    if (result.ErrorCode != 0)
                //                    {
                //                        All.Show_message("Error code " + result.ErrorCode + ": Lỗi không thể upload tệp!");
                //                        return null;
                //                    }
                //                    f.DM016804 = result.Data.ToString();
                //                }
                //                break;
                //            default: break;
                //        }

                //        obj.Fields.Add(f);
                //    }
                //}

                return obj;
            }
            catch (Exception ex)
            {
                Log.write(ex);
                return null;
            }
        }

        //private bool ValidateDetailForm()
        //{
        //    if (lookUpEdit6.EditValue == null)
        //    {
        //        All.Show_message("Vui lòng chọn Năm!");
        //        lookUpEdit6.Focus();
        //        return false;
        //    }

        //    if (lookUpEdit1.EditValue.ToString() == Guid.Empty.ToString())
        //    {
        //        All.Show_message("Vui lòng chọn Trạng thái!");
        //        lookUpEdit1.Focus();
        //        return false;
        //    }

        //    if (lookUpEdit7.EditValue.ToString() == Guid.Empty.ToString())
        //    {
        //        All.Show_message("Vui lòng chọn Phân loại Nhiệm vụ!");
        //        lookUpEdit7.Focus();
        //        return false;
        //    }

        //    if (lookUpEdit8.EditValue.ToString() == Guid.Empty.ToString())
        //    {
        //        All.Show_message("Vui lòng chọn Danh mục Nhiệm vụ!");
        //        lookUpEdit8.Focus();
        //        return false;
        //    }

        //    if (textEdit2.Text.Trim() == string.Empty)
        //    {
        //        All.Show_message("Vui lòng nhập Số văn bản!");
        //        textEdit2.Focus();
        //        return false;
        //    }
        //    if (textEdit3.Text.Trim() == string.Empty)
        //    {
        //        All.Show_message("Vui lòng nhập Số văn bản!");
        //        textEdit3.Focus();
        //        return false;
        //    }

        //    if (dateEdit3.EditValue == null)
        //    {
        //        All.Show_message("Vui lòng chọn Ngày!");
        //        dateEdit3.Focus();
        //        return false;
        //    }

        //    if (textEdit4.Text.Trim() == string.Empty)
        //    {
        //        All.Show_message("Vui lòng nhập Trích yếu!");
        //        textEdit4.Focus();
        //        return false;
        //    }

        //    if (lookUpEdit10.EditValue.ToString() == Guid.Empty.ToString())
        //    {
        //        All.Show_message("Vui lòng chọn Cơ quan nhận!");
        //        lookUpEdit10.Focus();
        //        return false;
        //    }

        //    if (lookUpEdit11.EditValue.ToString() == Guid.Empty.ToString())
        //    {
        //        All.Show_message("Vui lòng chọn Người ký!");
        //        lookUpEdit11.Focus();
        //        return false;
        //    }

        //    if (lookUpEdit13.EditValue.ToString() == Guid.Empty.ToString())
        //    {
        //        All.Show_message("Vui lòng chọn Nội dung Nhiệm vụ!");
        //        lookUpEdit13.Focus();
        //        return false;
        //    }

        //    if (currentNoidung.DM016105 == '1')
        //    {
        //        MemoEdit mmeText = (MemoEdit)xtraScrollableControl1.Controls.Find("mmeText", true).FirstOrDefault();
        //        if (mmeText == null || mmeText.Text.Trim() == string.Empty)
        //        {
        //            All.Show_message("Vui lòng nhập Nội dung chi tiết!");
        //            if (mmeText != null) mmeText.Focus();
        //            return false;
        //        }
        //    }
        //    else
        //    {
        //        PrepareDetail();
        //        var list = tempFields.Where(o => o.Batbuocnhap || o.Kieutruong == 9);
        //        if (list.Count() > 0)
        //        {
        //            foreach (TD_ThuchienNhiemvu_Truongdulieu obj in list)
        //            {
        //                if (obj.Kieutruong != 9 && obj.DM016804.Trim() == string.Empty)
        //                {
        //                    All.Show_message("Vui lòng nhập đầy đủ các trường dữ liệu chi tiết!");
        //                    return false;
        //                }

        //                if (obj.Kieutruong == 9)
        //                {
        //                    if (obj.Children == null || obj.Children.Count(o => o.Batbuocnhap && o.DM016804.Trim() == string.Empty) > 0)
        //                    {
        //                        All.Show_message("Vui lòng nhập đầy đủ các trường dữ liệu chi tiết!");
        //                        return false;
        //                    }
        //                }
        //            }
        //        }
        //    }


        //    return true;
        //}

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
            TD_Thamdinh_Duyet_Filter filter = new TD_Thamdinh_Duyet_Filter();
            filter.DuyetID = TD_Nhiemvu.DM016701;
            filter.Nam = lookUpEdit5.EditValue == null ? 0 : int.Parse(lookUpEdit5.EditValue.ToString());
            filter.TuNgay = dateEdit1.EditValue == null ? DateTime.MinValue : dateEdit1.DateTime;
            filter.DenNgay = dateEdit2.EditValue == null ? DateTime.MinValue : dateEdit2.DateTime;
            filter.MaDanhmuc = lookUpEdit4.EditValue == null ? Guid.Empty : Guid.Parse(lookUpEdit4.EditValue.ToString());
            filter.MaPhanloai = lookUpEdit3.EditValue == null ? Guid.Empty : Guid.Parse(lookUpEdit3.EditValue.ToString());
            filter.Sovanban = textEdit1.Text.Trim();

            currentList = !refresh ?
                Helpers.TrinhduyetThamdinh.GetList_Duyet(filter) :
                currentList;

            gridControl1.DataSource = currentList;
            gridControl1.RefreshDataSource();
            gridView1.BestFitColumns();

            uC_MenuBtn1.set_status_menu(currentState, currentList == null ? 0 : currentList.Count);
            SetDetailFormEnable(false);

            TD_Thamdinh_Duyet current = (TD_Thamdinh_Duyet)gridView1.GetFocusedRow();
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
            currentDataSelected = (TD_Thamdinh_Duyet)gridView1.GetRow(currentRowSelected);
            AssignDetailFormValue(currentDataSelected);
        }

        private void AssignDetailFormValue(TD_Thamdinh_Duyet data)
        {
            lookUpEdit6.EditValue = data == null ? DateTime.Now.Year : data.DM017103;
            lookUpEdit7.EditValue = data == null ? Guid.Empty : data.MaPhanloaiNhiemvu;
            lookUpEdit8.EditValue = data == null ? Guid.Empty : data.DM017104;
            lookUpEdit10.EditValue = data == null ? Guid.Empty : data.DM017120;
            lookUpEdit1.EditValue = All.gs_dv_quanly;
            textEdit4.Text = All.gs_user_name;
            textEdit2.Text = data == null ? string.Empty : data.DM017105;
            dateEdit3.DateTime = data == null ? DateTime.Now : data.DM017106;
            textEdit5.Text = data == null ? string.Empty : data.DM017107;

            int type = 1;
            if (data != null)
                type = int.Parse(data.DM017110.ToString());
            switch (type)
            {
                case 1:
                    checkEdit2.Checked = true;
                    break;
                case 2:
                    checkEdit3.Checked = true;
                    break;
                case 3:
                    checkEdit4.Checked = true;
                    break;
                case 4:
                    checkEdit5.Checked = true;
                    break;
                default:
                    checkEdit2.Checked = true;
                    break;
            }
            LoadUC(data);
        }

        public void CallBack_UpdateField(TD_Thamdinh_Duyet_Truongdulieu field, bool update)
        {
            if (update)
            {
                //TD_Thamdinh_Duyet_Truongdulieu f = tempFields.FirstOrDefault(o => o.DM017201 == field.DM017201);
                //f.Children = field.Children;

                ((UC_Thamdinh_Duyet_Thamdinhvatrinh)iControl).CallBack_UpdateField(field, update);
            }

            this.Enabled = true;
            this.Focus();
        }
    }
}
