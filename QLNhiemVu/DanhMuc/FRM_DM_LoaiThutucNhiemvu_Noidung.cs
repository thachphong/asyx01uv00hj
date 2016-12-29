﻿using DBAccess;
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
    public partial class FRM_DM_LoaiThutucNhiemvu_Noidung : Form
    {
        private static List<DM_LoaiThutucNhiemvu_Noidung> currentList = null;
        private static int currentRowSelected = int.MinValue;
        private static DM_LoaiThutucNhiemvu_Noidung currentDataSelected = null;
        private static string currentState = "NORMAL";
        private static Guid currentThutucID = Guid.Empty;
        public FRM_DM_LoaiThutucNhiemvu_Noidung()
        {
            InitializeComponent();
        }

        private void FRM_DM_Huongdan_Load(object sender, EventArgs e)
        {
            lblHeadTitle1.setText("Danh mục - Loại thủ tục nhiệm vụ - Nội dung chi tiết");
            lblHeadTitle1.alignCenter(lblHeadTitle1.Parent);
            panelHeader1.alignCenter(panelHeader1.Parent);
            panelHeader2.alignCenter(panelHeader2.Parent);
            panelHeader3.alignCenter(panelHeader3.Parent);

            BindControlEvents();

            LoadThutuc();
            LoadCachnhap();
            //LoadList();
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

            if (MessageBox.Show("Bạn chắc chắn muốn xóa " + listChecked.Count + " Nội dung đang chọn?", "Cảnh báo!", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                APIResponseData result = Helpers.ThutucNhiemvu_Noidung.Delete(listChecked);
                if (result == null)
                {
                    AllDefine.Show_message("Có lỗi trong quá trình cập nhật dữ liệu!");
                }
                else if (result.ErrorCode == 0)
                {
                    AllDefine.Show_message("Xóa thành công " + listChecked.Count + " Nội dung đã chọn!");
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

            DM_LoaiThutucNhiemvu_Noidung obj = PrepareDetail();

            if (obj == null)
            {
                AllDefine.Show_message("Có lỗi trong quá trình cập nhật dữ liệu!");
                return;
            }

            if (currentState == "NEW")
            {
                APIResponseData result = Helpers.ThutucNhiemvu_Noidung.Create(obj);
                if (result == null)
                {
                    AllDefine.Show_message("Có lỗi trong quá trình cập nhật dữ liệu!");
                }
                else if (result.ErrorCode == 0)
                {
                    AllDefine.Show_message("Thêm mới thành công Nội dung: " + obj.DM016103);
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
                APIResponseData result = Helpers.ThutucNhiemvu_Noidung.Update(obj);
                if (result == null)
                {
                    AllDefine.Show_message("Có lỗi trong quá trình cập nhật dữ liệu!");
                }
                else if (result.ErrorCode == 0)
                {
                    AllDefine.Show_message("Cập nhật thành công Nội dung: " + obj.DM016103);
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

        private DM_LoaiThutucNhiemvu_Noidung PrepareDetail()
        {
            try
            {
                DM_LoaiThutucNhiemvu_Noidung obj = new DM_LoaiThutucNhiemvu_Noidung();
                obj.DM016101 = currentState == "NEW" ? Guid.NewGuid() : currentDataSelected.DM016101;
                obj.DM016102 = currentThutucID;
                obj.DM016103 = textEdit1.Text;
                obj.DM016104 = textEdit2.Text;
                obj.DM016105 = char.Parse(lookUpEdit2.EditValue.ToString());
                obj.DM016106 = currentState == "NEW" ? AllDefine.gs_user_id : currentDataSelected.DM016106;
                obj.DM016107 = currentState == "NEW" ? DateTime.Now : currentDataSelected.DM016107;
                obj.DM016108 = AllDefine.gs_user_id;
                obj.DM016109 = DateTime.Now;
                obj.DM016110 = memoEdit1.Text;

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
            //if (lookUpEdit1.EditValue.ToString() == Guid.Empty.ToString())
            //{
            //    AllDefine.Show_message("Vui lòng chọn Loại thủ tục nhiệm vụ!");
            //    lookUpEdit1.Focus();
            //    return false;
            //}

            if (lookUpEdit2.EditValue.ToString().Trim() == string.Empty)
            {
                AllDefine.Show_message("Vui lòng chọn Cách nhập!");
                lookUpEdit2.Focus();
                return false;
            }

            if (textEdit1.Text.Trim() == string.Empty)
            {
                AllDefine.Show_message("Vui lòng nhập Mã nội dung!");
                textEdit1.Focus();
                return false;
            }

            if (textEdit2.Text.Trim() == string.Empty)
            {
                AllDefine.Show_message("Vui lòng nhập Tên nội dung!");
                textEdit2.Focus();
                return false;
            }

            if (lookUpEdit2.EditValue.ToString() == "1")
            {
                if (memoEdit1.Text.Trim() == string.Empty)
                {
                    AllDefine.Show_message("Vui lòng nhập Đoạn văn!");
                    memoEdit1.Focus();
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

        private void LoadCachnhap()
        {
            lookUpEdit2.Properties.DataSource = AllDefine.dm_loaithutuc_noidung_cachnhap;
            lookUpEdit2.Properties.DisplayMember = "Description";
            lookUpEdit2.Properties.ValueMember = "ID";
            lookUpEdit2.Properties.BestFitRowCount = AllDefine.dm_loaithutuc_noidung_cachnhap.Count;
            lookUpEdit2.Refresh();
        }

        private void LoadThutuc()
        {
            List<DM_LoaiThutucNhiemvu> list = Helpers.ThutucNhiemvu.GetList();
            lookUpEdit1.Properties.DataSource = list;
            lookUpEdit1.Properties.DisplayMember = "DM016004";
            lookUpEdit1.Properties.ValueMember = "DM016001";
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
            currentList = Helpers.ThutucNhiemvu_Noidung.GetList(currentThutucID);

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
            currentDataSelected = (DM_LoaiThutucNhiemvu_Noidung)gridView1.GetRow(currentRowSelected);
            AssignDetailFormValue(currentDataSelected);
        }

        private void SetDetailFormEnable(bool isEnable)
        {
            groupControl2.Enabled = isEnable;
            groupControl1.Enabled = !isEnable;
            lookUpEdit1.Enabled = !isEnable;

            if (isEnable)
                lookUpEdit2.Focus();
        }

        private void AssignDetailFormValue(DM_LoaiThutucNhiemvu_Noidung data)
        {
            lookUpEdit1.EditValue = data == null ? Guid.Empty : data.DM016102;
            lookUpEdit2.EditValue = data == null ? ' ' : data.DM016105;
            textEdit1.Text = data == null ? string.Empty : data.DM016103;
            textEdit2.Text = data == null ? string.Empty : data.DM016104;
        }

        private void lookUpEdit2_EditValueChanged(object sender, EventArgs e)
        {
            groupBox1.Visible = lookUpEdit2.EditValue.ToString() == "1";
        }

        private void lookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {
            currentThutucID = Guid.Parse(lookUpEdit1.EditValue.ToString());
            if (currentThutucID != Guid.Empty)
                LoadList();
        }
    }
}