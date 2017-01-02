using DBAccess;
using DevExpress.XtraEditors;
using QLNhiemVu.FRMModel;
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
    public partial class FRM_DM_LoaiThutucNhiemvu : BaseForm_Data
    {
        private static List<DM_LoaiThutucNhiemvu> currentList = null;
        private static int currentRowSelected = int.MinValue;
        private static DM_LoaiThutucNhiemvu currentDataSelected = null;
        private static string currentState = "NORMAL";
        private static List<Guid> currentListFields = null;
        public FRM_DM_LoaiThutucNhiemvu()
        {
            InitializeComponent();
        }

        private void FRM_LoaiThutucNhiemvu_Load(object sender, EventArgs e)
        {
            lblHeadTitle1.setText("Danh mục - Loại Thủ tục/Nhiệm vụ");
            lblHeadTitle1.alignCenter(lblHeadTitle1.Parent);
            panelHeader1.alignCenter(panelHeader1.Parent);
            panelHeader2.alignCenter(panelHeader2.Parent);
            panelHeader3.alignCenter(panelHeader3.Parent);

            BindControlEvents();

            LoadDonvi();
            LoadLoaiCapphep();
            LoadList();
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

            if (MessageBox.Show("Bạn chắc chắn muốn xóa " + listChecked.Count + " Loại thủ tục đang chọn?", "Cảnh báo!", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                APIResponseData result = Helpers.ThutucNhiemvu.Delete(listChecked);
                if (result == null)
                {
                    AllDefine.Show_message("Có lỗi trong quá trình cập nhật dữ liệu!");
                }
                else if (result.ErrorCode == 0)
                {
                    AllDefine.Show_message("Xóa thành công " + listChecked.Count + " Loại thủ tục nhiệm vụ đã chọn!");
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

            DM_LoaiThutucNhiemvu obj = PrepareDetail();

            if (obj == null)
            {
                AllDefine.Show_message("Có lỗi trong quá trình cập nhật dữ liệu!");
                return;
            }

            if (currentState == "NEW")
            {
                APIResponseData result = Helpers.ThutucNhiemvu.Create(obj);
                if (result == null)
                {
                    AllDefine.Show_message("Có lỗi trong quá trình cập nhật dữ liệu!");
                }
                else if (result.ErrorCode == 0)
                {
                    AllDefine.Show_message("Thêm mới thành công Loại thủ tục nhiệm vụ: " + obj.DM016003);
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
                APIResponseData result = Helpers.ThutucNhiemvu.Update(obj);
                if (result == null)
                {
                    AllDefine.Show_message("Có lỗi trong quá trình cập nhật dữ liệu!");
                }
                else if (result.ErrorCode == 0)
                {
                    AllDefine.Show_message("Cập nhật thành công Loại thủ tục nhiệm vụ: " + obj.DM016003);
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

        private DM_LoaiThutucNhiemvu PrepareDetail()
        {
            try
            {
                DM_LoaiThutucNhiemvu obj = new DM_LoaiThutucNhiemvu();
                obj.DM016001 = currentState == "NEW" ? Guid.NewGuid() : currentDataSelected.DM016001;
                obj.DM016002 = Guid.Parse(lookUpEdit1.EditValue.ToString());
                obj.DM016003 = textEdit1.Text;
                obj.DM016004 = textEdit2.Text;
                obj.DM016005 = char.Parse(lookUpEdit2.EditValue.ToString());
                obj.DM016006 = currentState == "NEW" ? AllDefine.gs_user_id : currentDataSelected.DM016006;
                obj.DM016007 = currentState == "NEW" ? DateTime.Now : currentDataSelected.DM016007;
                obj.DM016008 = AllDefine.gs_user_id;
                obj.DM016009 = DateTime.Now;
                obj.DM016010 = checkEdit2.Checked ? '1' : '0';
                obj.FieldSelecteds = currentListFields;

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
            if (lookUpEdit1.EditValue.ToString() == Guid.Empty.ToString())
            {
                AllDefine.Show_message("Vui lòng chọn Đơn vị sử dụng chương trình!");
                lookUpEdit1.Focus();
                return false;
            }

            if (lookUpEdit2.EditValue.ToString().Trim() == string.Empty)
            {
                AllDefine.Show_message("Vui lòng chọn Loại cấp phép!");
                lookUpEdit2.Focus();
                return false;
            }

            if (textEdit1.Text.Trim() == string.Empty)
            {
                AllDefine.Show_message("Vui lòng nhập Mã form!");
                textEdit1.Focus();
                return false;
            }

            if (textEdit2.Text.Trim() == string.Empty)
            {
                AllDefine.Show_message("Vui lòng nhập Tên form!");
                textEdit2.Focus();
                return false;
            }

            if (checkEdit1.Checked && currentListFields == null)
            {
                AllDefine.Show_message("Vui lòng nhập Chọn trường dữ liệu thẩm định!");
                ShowChildForm();
                return false;
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

        private void LoadLoaiCapphep()
        {
            lookUpEdit2.Properties.DataSource = AllDefine.dm_loaithutuc_loaicapphep;
            lookUpEdit2.Properties.DisplayMember = "Description";
            lookUpEdit2.Properties.ValueMember = "ID";
            lookUpEdit2.Properties.BestFitRowCount = AllDefine.dm_loaithutuc_loaicapphep.Count;
            lookUpEdit2.Refresh();
        }

        private void LoadDonvi()
        {
            List<DM_Donvi> list = new List<DM_Donvi>() { new DM_Donvi() { ID = AllDefine.gs_dv_quanly, Name = AllDefine.gs_ten_dv_quanly } };
            lookUpEdit1.Properties.DataSource = list;
            lookUpEdit1.Properties.DisplayMember = "Name";
            lookUpEdit1.Properties.ValueMember = "ID";
            lookUpEdit1.Properties.BestFitRowCount = list.Count;
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
            currentList = Helpers.ThutucNhiemvu.GetList();

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
            currentDataSelected = (DM_LoaiThutucNhiemvu)gridView1.GetRow(currentRowSelected);
            AssignDetailFormValue(currentDataSelected);
        }

        private void SetDetailFormEnable(bool isEnable)
        {
            lookUpEdit1.ReadOnly = !isEnable;
            lookUpEdit2.ReadOnly = !isEnable;
            textEdit1.ReadOnly = !isEnable;
            textEdit2.ReadOnly = !isEnable;
            checkEdit2.ReadOnly = !isEnable;

            groupControl1.Enabled = !isEnable;

            if (isEnable)
                lookUpEdit1.Focus();
        }

        private void AssignDetailFormValue(DM_LoaiThutucNhiemvu data)
        {
            lookUpEdit1.EditValue = data == null ? Guid.Empty : data.DM016002;
            lookUpEdit2.EditValue = data == null ? ' ' : data.DM016005;
            textEdit1.Text = data == null ? string.Empty : data.DM016003;
            textEdit2.Text = data == null ? string.Empty : data.DM016004;
            checkEdit2.Checked = data == null ? false : data.DM016010 == '1';
            currentListFields = data == null ? null : data.FieldSelecteds;
        }

        private void ShowChildForm()
        {
            this.Enabled = false;
            FRM_DM_LoaiThutucNhiemvu_QuitrinhThamdinh frm = new FRM_DM_LoaiThutucNhiemvu_QuitrinhThamdinh();
            frm.Show();
            frm.Focus();
        }

        private void checkEdit2_CheckedChanged(object sender, EventArgs e)
        {
            if (currentState == "NORMAL") return;

            if (checkEdit2.Checked)
            {
                simpleButton1.Visible = true;
            }
            else
            {
                simpleButton1.Visible = false;
            }
        }

        public void CallBack_Thamdinh(List<Guid> fieldSelecteds, bool update)
        {
            if (update)
                currentListFields = fieldSelecteds;

            this.Enabled = true;
            this.Focus();
        }

        public List<Guid> CallBack_GetCurrentFieldSelected()
        {
            return currentListFields;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            ShowChildForm();
        }
    }
}
