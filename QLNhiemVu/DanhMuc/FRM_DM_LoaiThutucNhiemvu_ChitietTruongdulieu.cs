using DBAccess;
using DevExpress.XtraEditors;
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
    public partial class FRM_DM_LoaiThutucNhiemvu_ChitietTruongdulieu : Form
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

            LoadKieutruong();
            LoadCachnhap();
            LoadTables();
            LoadList();
        }

        private void LoadTables()
        {
            List<string> list = Helpers.ThutucNhiemvu_Truongdulieu.GetListTables();
            lookUpEdit1.Properties.DataSource = list;
            lookUpEdit1.Properties.BestFitRowCount = list == null ? 0 : list.Count;
            lookUpEdit1.Refresh();
        }

        private void LoadTableColumns()
        {
            List<string> list =
                currentBangdulieu == string.Empty ? null :
                Helpers.ThutucNhiemvu_Truongdulieu.GetListTableColumns(currentBangdulieu);

            lookUpEdit2.Properties.DataSource = list;
            lookUpEdit2.Properties.BestFitRowCount = list == null ? 0 : list.Count;
            lookUpEdit2.Refresh();
        }

        private void LoadCachnhap()
        {
            lookUpEdit4.Properties.DataSource = AllDefine.dm_loaithutuc_truongdulieu_cachnhap;
            lookUpEdit4.Properties.DisplayMember = "Description";
            lookUpEdit4.Properties.ValueMember = "ID";
            lookUpEdit4.Properties.BestFitRowCount = AllDefine.dm_loaithutuc_truongdulieu_cachnhap.Count;
            lookUpEdit4.Refresh();
        }

        private void LoadKieutruong()
        {
            lookUpEdit3.Properties.DataSource = AllDefine.dm_loaithutuc_truongdulieu_kieutruong;
            lookUpEdit3.Properties.DisplayMember = "Description";
            lookUpEdit3.Properties.ValueMember = "ID";
            lookUpEdit3.Properties.BestFitRowCount = AllDefine.dm_loaithutuc_truongdulieu_kieutruong.Count;
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
                AllDefine.Show_message("Vui lòng chọn trước khi xóa!");
                return;
            }

            if (MessageBox.Show("Bạn chắc chắn muốn xóa " + listChecked.Count + " Trường dữ liệu đang chọn?", "Cảnh báo!", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                APIResponseData result = Helpers.ThutucNhiemvu_Truongdulieu.Delete(listChecked);
                if (result == null)
                {
                    AllDefine.Show_message("Có lỗi trong quá trình cập nhật dữ liệu!");
                }
                else if (result.ErrorCode == 0)
                {
                    AllDefine.Show_message("Xóa thành công " + listChecked.Count + " Trường dữ liệu đã chọn!");
                    currentState = "NORMAL";
                    LoadList();
                }
                else
                {
                    AllDefine.Show_message("Error code " + result.ErrorCode + ": " + result.Message);
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

        void btn_capnhat_Click(object sender, EventArgs e)
        {
            if (currentState == "NORMAL") return;

            if (!ValidateDetailForm()) return;

            DM_LoaiThutucNhiemvu_Truongdulieu obj = PrepareDetail();

            if (obj == null)
            {
                AllDefine.Show_message("Có lỗi trong quá trình cập nhật dữ liệu!");
                return;
            }

            if (currentState == "NEW")
            {
                APIResponseData result = Helpers.ThutucNhiemvu_Truongdulieu.Create(obj);
                if (result == null)
                {
                    AllDefine.Show_message("Có lỗi trong quá trình cập nhật dữ liệu!");
                }
                else if (result.ErrorCode == 0)
                {
                    AllDefine.Show_message("Thêm mới thành công Trường dữ liệu: " + obj.DM016205);
                    currentState = "NORMAL";
                    LoadList();
                }
                else
                {
                    AllDefine.Show_message("Error code " + result.ErrorCode + ": " + result.Message);
                }
            }
            else
            {
                APIResponseData result = Helpers.ThutucNhiemvu_Truongdulieu.Update(obj);
                if (result == null)
                {
                    AllDefine.Show_message("Có lỗi trong quá trình cập nhật dữ liệu!");
                }
                else if (result.ErrorCode == 0)
                {
                    AllDefine.Show_message("Cập nhật thành công Trường dữ liệu: " + obj.DM016205);
                    currentState = "NORMAL";
                    LoadList();
                }
                else
                {
                    AllDefine.Show_message("Error code " + result.ErrorCode + ": " + result.Message);
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
                //10+11: chỉ khi 07 = 8
                obj.DM016210 = obj.DM016207 == "8" ? lookUpEdit1.EditValue.ToString() : string.Empty;
                obj.DM016211 = obj.DM016207 == "8" ? lookUpEdit2.EditValue.ToString() : string.Empty;
                obj.DM016212 = textEdit8.Text;
                obj.DM016213 = textEdit7.Text;
                obj.DM016214 = int.Parse(textEdit9.Text);
                obj.DM016215 = checkEdit2.Checked ? '1' : '0';
                obj.DM016217 = currentState == "NEW" ? AllDefine.gs_user_id : currentDataSelected.DM016217;
                obj.DM016218 = currentState == "NEW" ? DateTime.Now : currentDataSelected.DM016218;
                obj.DM016219 = AllDefine.gs_user_id;
                obj.DM016220 = DateTime.Now;

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
                AllDefine.Show_message("Vui lòng nhập Mã Trường dữ liệu!");
                textEdit1.Focus();
                return false;
            }

            if (textEdit2.Text.Trim() == string.Empty)
            {
                AllDefine.Show_message("Vui lòng nhập Tên Trường dữ liệu!");
                textEdit2.Focus();
                return false;
            }

            if (textEdit3.Text.Trim() == string.Empty)
            {
                AllDefine.Show_message("Vui lòng nhập Tên Trường hiển thị!");
                textEdit3.Focus();
                return false;
            }

            if (textEdit9.Text.Trim() == string.Empty)
            {
                AllDefine.Show_message("Vui lòng nhập Sắp xếp!");
                textEdit9.Focus();
                return false;
            }
            try { int check = int.Parse(textEdit9.Text); }
            catch
            {
                AllDefine.Show_message("Vui lòng nhập Sắp xếp kiểu số nguyên!");
                textEdit9.Focus();
                return false;
            }

            if (textEdit4.Text.Trim() == string.Empty)
            {
                AllDefine.Show_message("Vui lòng nhập Độ rộng các cột!");
                textEdit4.Focus();
                return false;
            }
            try { int check = int.Parse(textEdit4.Text); }
            catch
            {
                AllDefine.Show_message("Vui lòng nhập Độ rộng các cột kiểu số nguyên!");
                textEdit4.Focus();
                return false;
            }

            if (lookUpEdit3.EditValue.ToString() == "8")
            {
                if (lookUpEdit4.EditValue.ToString() == string.Empty)
                {
                    AllDefine.Show_message("Vui lòng nhập chọn Cách nhập!");
                    lookUpEdit4.Focus();
                    return false;
                }
                else
                {
                    if (lookUpEdit4.EditValue.ToString() == "2")
                    {
                        if (lookUpEdit1.EditValue.ToString() == string.Empty)
                        {
                            AllDefine.Show_message("Vui lòng nhập chọn Bảng dữ liệu!");
                            lookUpEdit1.Focus();
                            return false;
                        }

                        if (lookUpEdit2.EditValue.ToString() == string.Empty)
                        {
                            AllDefine.Show_message("Vui lòng nhập chọn Điều kiện dữ liệu!");
                            lookUpEdit2.Focus();
                            return false;
                        }
                    }
                }

                if (textEdit8.Text.Trim() == string.Empty)
                {
                    AllDefine.Show_message("Vui lòng nhập Công thức tính dữ liệu!");
                    textEdit8.Focus();
                    return false;
                }

                if (textEdit7.Text.Trim() == string.Empty)
                {
                    AllDefine.Show_message("Vui lòng nhập Cộng cột dữ liệu!");
                    textEdit7.Focus();
                    return false;
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

        private void LoadList()
        {
            currentList = Helpers.ThutucNhiemvu_Truongdulieu.GetList();

            gridControl1.DataSource = currentList;
            gridControl1.RefreshDataSource();
            gridView1.BestFitColumns();

            uC_MenuBtn1.set_status_menu(currentState, currentList == null ? 0 : currentList.Count);
            SetDetailFormEnable(false);
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
            groupControl2.Enabled = isEnable;
            groupControl1.Enabled = !isEnable;
        }

        private void AssignDetailFormValue(DM_LoaiThutucNhiemvu_Truongdulieu data)
        {
            lookUpEdit3.EditValue = data == null ? string.Empty : data.DM016207.Trim();
            lookUpEdit4.EditValue = data == null ? string.Empty : data.DM016209.Trim();

            textEdit1.Text = data == null ? string.Empty : data.DM016204;
            textEdit2.Text = data == null ? string.Empty : data.DM016205;
            textEdit3.Text = data == null ? string.Empty : data.DM016206;
            textEdit9.Text = data == null ? string.Empty : data.DM016214.ToString();
            textEdit4.Text = data == null ? string.Empty : data.DM016208.ToString();
            textEdit8.Text = data == null ? string.Empty : data.DM016212;
            textEdit7.Text = data == null ? string.Empty : data.DM016213;

            lookUpEdit1.EditValue = data == null ? string.Empty : data.DM016210.Trim();
            lookUpEdit2.EditValue = data == null ? string.Empty : data.DM016211.Trim();

            checkEdit2.Checked = data == null ? false : data.DM016215 == '1';
        }

        private void lookUpEdit3_EditValueChanged(object sender, EventArgs e)
        {
            currentKieutruong = lookUpEdit3.EditValue.ToString();
            //currentCachnhap = lookUpEdit4.EditValue.ToString();

            if (currentKieutruong == "8")
            {
                lookUpEdit4.Enabled = true;
                textEdit8.Enabled = true;
                textEdit7.Enabled = true;

                if (currentCachnhap == "2")
                {
                    lookUpEdit1.Enabled = true;
                    lookUpEdit2.Enabled = true;
                }
                else
                {
                    lookUpEdit1.Enabled = false;
                    lookUpEdit2.Enabled = false;
                }
            }
            else
            {
                lookUpEdit4.Enabled = false;
                textEdit8.Enabled = false;
                textEdit7.Enabled = false;
                lookUpEdit1.Enabled = false;
                lookUpEdit2.Enabled = false;
            }
        }

        private void lookUpEdit4_EditValueChanged(object sender, EventArgs e)
        {
            currentCachnhap = lookUpEdit4.EditValue.ToString();

            if (lookUpEdit4.EditValue.ToString() == "2")
            {
                lookUpEdit1.Enabled = true;
                lookUpEdit2.Enabled = true;
            }
            else
            {
                lookUpEdit1.Enabled = false;
                lookUpEdit2.Enabled = false;
            }
        }

        private void lookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {
            currentBangdulieu = lookUpEdit1.EditValue.ToString();
            LoadTableColumns();
        }
    }
}
