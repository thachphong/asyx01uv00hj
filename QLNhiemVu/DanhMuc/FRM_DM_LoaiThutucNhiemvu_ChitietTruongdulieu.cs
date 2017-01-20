using DBAccess;
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLNhiemVu.DanhMuc
{
    public partial class FRM_DM_LoaiThutucNhiemvu_ChitietTruongdulieu : BaseForm_Data
    {
        private static List<DM_LoaiThutucNhiemvu_Truongdulieu> currentList = null;
        private static int currentRowSelected = int.MinValue;
        private static DM_LoaiThutucNhiemvu_Truongdulieu currentDataSelected = null;
        private static string currentState = "NORMAL";
        private static Guid currentID = Guid.Empty;
        private static string currentKieutruong = "0";
        private static string currentCachnhap = "0";
        private static string currentBangdulieu = string.Empty;
        private static string currentDieukiendulieu = string.Empty;
        private static string currentLookupData = string.Empty;

        public FRM_DM_LoaiThutucNhiemvu_ChitietTruongdulieu()
        {
            InitializeComponent();
        }

        private void FRM_DM_LoaiThutucNhiemvu_ChitietTruongdulieu_Load(object sender, EventArgs e)
        {
            lblHeadTitle1.setText("Danh mục - Loại thủ tục nhiệm vụ - Trường dữ liệu");
            lblHeadTitle1.alignCenter(lblHeadTitle1.Parent);
            panelHeader1.alignCenter(panelHeader1.Parent);
            panelHeader2.alignCenter(panelHeader2.Parent);
            panelHeader3.alignCenter(panelHeader3.Parent);

            BindControlEvents();

            currentState = "NORMAL";

            LoadKieutruong();
            LoadList(false);
        }

        private void LoadKieutruong()
        {
            lookUpEdit3.Properties.DataSource = All.dm_loaithutuc_truongdulieu_kieutruong;
            lookUpEdit3.Properties.DisplayMember = "Description";
            lookUpEdit3.Properties.ValueMember = "ID";
            lookUpEdit3.Properties.BestFitRowCount = All.dm_loaithutuc_truongdulieu_kieutruong.Count;
            lookUpEdit3.Refresh();
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

            if (MessageBox.Show("Bạn chắc chắn muốn xóa " + listChecked.Count + " Trường dữ liệu đang chọn?", "Cảnh báo!", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                APIResponseData result = Helpers.ThutucNhiemvu_Truongdulieu.Delete(listChecked);
                if (result == null)
                {
                    All.Show_message("Có lỗi trong quá trình cập nhật dữ liệu!");
                }
                else if (result.ErrorCode == 0)
                {
                    All.Show_message("Xóa thành công " + listChecked.Count + " Trường dữ liệu đã chọn!");
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

        void RefreshNewData(DM_LoaiThutucNhiemvu_Truongdulieu obj)
        {
            if (currentList == null) currentList = new List<DM_LoaiThutucNhiemvu_Truongdulieu>();

            DM_LoaiThutucNhiemvu_Truongdulieu current = currentList.FirstOrDefault(o => o.DM016201 == obj.DM016201);
            if (current == null) currentList.Insert(0, obj);
            else current = obj;
        }

        void btn_capnhat_Click(object sender, EventArgs e)
        {
            if (currentState == "NORMAL") return;

            if (!ValidateDetailForm()) return;

            DM_LoaiThutucNhiemvu_Truongdulieu obj = PrepareDetail();

            if (obj == null)
            {
                All.Show_message("Có lỗi trong quá trình cập nhật dữ liệu!");
                return;
            }

            if (currentState == "NEW")
            {
                APIResponseData result = Helpers.ThutucNhiemvu_Truongdulieu.Create(obj);
                if (result == null)
                {
                    All.Show_message("Có lỗi trong quá trình cập nhật dữ liệu!");
                }
                else if (result.ErrorCode == 0)
                {
                    All.Show_message("Thêm mới thành công Trường dữ liệu: " + obj.DM016205);
                    currentState = "NORMAL";
                    RefreshNewData(JsonConvert.DeserializeObject<DM_LoaiThutucNhiemvu_Truongdulieu>(result.Data.ToString()));
                    LoadList();
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
                    All.Show_message("Cập nhật thành công Trường dữ liệu: " + obj.DM016205);
                    currentState = "NORMAL";
                    RefreshNewData(JsonConvert.DeserializeObject<DM_LoaiThutucNhiemvu_Truongdulieu>(result.Data.ToString()));
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

        private DM_LoaiThutucNhiemvu_Truongdulieu PrepareDetail()
        {
            try
            {
                DM_LoaiThutucNhiemvu_Truongdulieu obj = new DM_LoaiThutucNhiemvu_Truongdulieu();
                obj.DM016201 = currentState == "NEW" ? Guid.NewGuid() : currentDataSelected.DM016201;
                obj.DM016204 = textEdit1.Text;
                obj.DM016205 = textEdit2.Text;
                obj.DM016206 = textEdit3.Text;
                obj.DM016207 = currentKieutruong.Trim();
                obj.DM016208 = int.Parse(textEdit4.Text);
                obj.DM016209 = currentCachnhap.Trim();
                obj.DM016210 = textEdit5.Text;
                obj.DM016213 = textEdit7.Text;
                obj.DM016214 = int.Parse(textEdit9.Text);
                obj.DM016215 = checkEdit2.Checked ? '1' : '0';
                obj.DM016217 = currentState == "NEW" ? All.gs_user_id : currentDataSelected.DM016217;
                obj.DM016218 = currentState == "NEW" ? DateTime.Now : currentDataSelected.DM016218;
                obj.DM016219 = All.gs_user_id;
                obj.DM016220 = DateTime.Now;
                obj.DM016216 = lookUpEdit1.EditValue == null ? Guid.Empty : Guid.Parse(lookUpEdit1.EditValue.ToString());

                return obj;
            }
            catch (Exception ex)
            {
                Log.write(ex);
                return null;
            }
        }

        private bool ValidateDetailForm()
        {//HERE
            if (textEdit1.Text.Trim() == string.Empty)
            {
                All.Show_message("Vui lòng nhập Mã Trường dữ liệu!");
                textEdit1.Focus();
                return false;
            }

            if (textEdit2.Text.Trim() == string.Empty)
            {
                All.Show_message("Vui lòng nhập Tên Trường dữ liệu!");
                textEdit2.Focus();
                return false;
            }

            if (textEdit3.Text.Trim() == string.Empty)
            {
                All.Show_message("Vui lòng nhập Tên Trường hiển thị!");
                textEdit3.Focus();
                return false;
            }

            if (textEdit9.Text.Trim() == string.Empty)
            {
                All.Show_message("Vui lòng nhập Sắp xếp!");
                textEdit9.Focus();
                return false;
            }
            try { int check = int.Parse(textEdit9.Text); }
            catch
            {
                All.Show_message("Vui lòng nhập Sắp xếp kiểu số nguyên!");
                textEdit9.Focus();
                return false;
            }

            if (textEdit4.Text.Trim() == string.Empty)
            {
                All.Show_message("Vui lòng nhập Độ rộng các cột!");
                textEdit4.Focus();
                return false;
            }
            try { int check = int.Parse(textEdit4.Text); }
            catch
            {
                All.Show_message("Vui lòng nhập Độ rộng các cột kiểu số nguyên!");
                textEdit4.Focus();
                return false;
            }

            if (lookUpEdit3.EditValue.ToString() == "2" ||
                lookUpEdit3.EditValue.ToString() == "8")
            {
                if (textEdit5.Text.Trim() == string.Empty)
                {
                    All.Show_message("Vui lòng nhập Điều kiện dữ liệu phù hợp!");
                    textEdit5.Focus();
                    return false;
                }

                if (lookUpEdit3.EditValue.ToString() == "8")
                {
                    try { JsonConvert.DeserializeObject<DM_LoaiThutucNhiemvu_Truongdulieu_LookupData>(textEdit5.Text.Trim()); }
                    catch
                    {
                        All.Show_message("Vui lòng nhập Điều kiện dữ liệu phù hợp!");
                        ShowChildForm_Lookup();
                        return false;
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
            currentList = !refresh ?
                Helpers.ThutucNhiemvu_Truongdulieu.GetList() :
                currentList;

            gridControl1.DataSource = currentList;
            gridControl1.RefreshDataSource();
            gridView1.BestFitColumns();

            lookUpEdit1.Properties.DataSource = currentList;
            lookUpEdit1.Properties.DisplayMember = "DM016205";
            lookUpEdit1.Properties.ValueMember = "DM016201";

            uC_MenuBtn1.set_status_menu(currentState, currentList == null ? 0 : currentList.Count);
            SetDetailFormEnable(false);

            DM_LoaiThutucNhiemvu_Truongdulieu current = (DM_LoaiThutucNhiemvu_Truongdulieu)gridView1.GetFocusedRow();
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
            currentDataSelected = (DM_LoaiThutucNhiemvu_Truongdulieu)gridView1.GetRow(currentRowSelected);
            AssignDetailFormValue(currentDataSelected);
        }

        private void SetDetailFormEnable(bool isEnable)
        {
            textEdit1.ReadOnly = !isEnable;
            textEdit2.ReadOnly = !isEnable;
            textEdit3.ReadOnly = !isEnable;
            textEdit4.ReadOnly = !isEnable;
            textEdit5.ReadOnly = !isEnable;
            textEdit7.ReadOnly = !isEnable;
            textEdit9.ReadOnly = !isEnable;
            lookUpEdit3.ReadOnly = !isEnable;
            checkEdit2.ReadOnly = !isEnable;
            lookUpEdit1.ReadOnly = !isEnable;

            groupControl1.Enabled = !isEnable;
        }

        private void AssignDetailFormValue(DM_LoaiThutucNhiemvu_Truongdulieu data)
        {
            lookUpEdit3.EditValue = data == null ? string.Empty : data.DM016207.Trim();

            textEdit1.Text = data == null ? string.Empty : data.DM016204;
            textEdit2.Text = data == null ? string.Empty : data.DM016205;
            textEdit3.Text = data == null ? string.Empty : data.DM016206;
            textEdit9.Text = data == null ? string.Empty : data.DM016214.ToString();
            textEdit4.Text = data == null ? string.Empty : data.DM016208.ToString();
            textEdit7.Text = data == null ? string.Empty : data.DM016213;

            textEdit5.Text = data == null ? string.Empty : data.DM016210.Trim();

            checkEdit2.Checked = data == null ? false : data.DM016215 == '1';

            lookUpEdit1.EditValue = data == null ? Guid.Empty : data.DM016216;

            currentLookupData = data == null ? string.Empty : data.DM016210;
        }

        private void lookUpEdit3_EditValueChanged(object sender, EventArgs e)
        {
            currentKieutruong = lookUpEdit3.EditValue.ToString();

            if (currentKieutruong == "2")
            {
                label9.Text = "Công thức tính dữ liệu";
                textEdit5.Enabled = true;
                simpleButton1.Visible = false;
            }
            else if (currentKieutruong == "8")
            {
                label9.Text = "Chọn điều kiện dữ liệu";
                textEdit5.Enabled = false;
                simpleButton1.Visible = true;
            }
            else if (currentKieutruong == "9")
            {
                //label9.Text = "Chọn danh sách trường";
                //textEdit5.Enabled = false;
                //simpleButton1.Visible = true;
                label9.Text = "...";
                textEdit5.Enabled = false;
                simpleButton1.Visible = false;
            }
            else
            {
                label9.Text = "...";
                textEdit5.Enabled = false;
                simpleButton1.Visible = false;
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (currentKieutruong == "8")
            {
                ShowChildForm_Lookup();
            }
            else if (currentKieutruong == "9")
            {
                ShowChildForm_Tab();
            }
        }

        private void ShowChildForm_Tab()
        {
            this.Enabled = false;
            FRM_DM_LoaiThutucNhiemvu_ChitietTruongdulieu_Tab frm = new FRM_DM_LoaiThutucNhiemvu_ChitietTruongdulieu_Tab();
            frm.currentState = currentState;
            frm.Show();
            frm.Focus();
        }

        private void ShowChildForm_Lookup()
        {
            this.Enabled = false;
            FRM_DM_LoaiThutucNhiemvu_ChitietTruongdulieu_Lookup frm = new FRM_DM_LoaiThutucNhiemvu_ChitietTruongdulieu_Lookup();
            frm.currentState = currentState;
            frm.Show();
            frm.Focus();
        }

        public DM_LoaiThutucNhiemvu_Truongdulieu_LookupData CallBack_GetLookupData()
        {
            try
            {
                if (string.IsNullOrEmpty(currentLookupData)) return null;

                return JsonConvert.DeserializeObject<DM_LoaiThutucNhiemvu_Truongdulieu_LookupData>(currentLookupData);
            }
            catch (Exception ex)
            {
                Log.write(ex);
                return null;
            }
        }

        public void CallBack_UpdateLookupData(DM_LoaiThutucNhiemvu_Truongdulieu_LookupData data, bool update)
        {
            if (update)
            {
                currentLookupData = JsonConvert.SerializeObject(data);
                textEdit5.Text = currentLookupData;
            }

            this.Enabled = true;
            this.Focus();
        }

        public List<Guid> CallBack_GetTabData()
        {
            try
            {
                if (string.IsNullOrEmpty(currentLookupData)) return null;

                return JsonConvert.DeserializeObject<List<Guid>>(currentLookupData);
            }
            catch (Exception ex)
            {
                Log.write(ex);
                return null;
            }
        }

        public void CallBack_UpdateTabData(List<Guid> data, bool update)
        {
            if (update)
            {
                currentLookupData = JsonConvert.SerializeObject(data);
                textEdit5.Text = currentLookupData;
            }

            this.Enabled = true;
            this.Focus();
        }
    }
}
