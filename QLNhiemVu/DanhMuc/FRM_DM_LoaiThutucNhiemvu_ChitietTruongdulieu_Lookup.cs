﻿using DBAccess;
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
    public partial class FRM_DM_LoaiThutucNhiemvu_ChitietTruongdulieu_Lookup : Form
    {
        FRM_DM_LoaiThutucNhiemvu_ChitietTruongdulieu frm = null;
        DM_LoaiThutucNhiemvu_Truongdulieu_LookupData currentData = null;
        string currentTable = string.Empty;
        public FRM_DM_LoaiThutucNhiemvu_ChitietTruongdulieu_Lookup()
        {
            InitializeComponent();
        }

        private void FRM_DM_LoaiThutucNhiemvu_QuitrinhThamdinh_Load(object sender, EventArgs e)
        {
            frm = (FRM_DM_LoaiThutucNhiemvu_ChitietTruongdulieu)Application.OpenForms["FRM_DM_LoaiThutucNhiemvu_ChitietTruongdulieu"];
            if (frm == null) this.Dispose();

            panelHeader1.alignCenter(panelHeader1.Parent);

            LoadTables();
            LoadFormulas();
            LoadConditionCombines();

            FillValues();
        }

        private void LoadConditionCombines()
        {
            List<string> list = AllDefine.dm_loaithutuc_truongdulieu_lookup_formulas;
            lookUpEdit8.Properties.DataSource = list;
            lookUpEdit8.Properties.BestFitRowCount = list == null ? 0 : list.Count;
            lookUpEdit8.Refresh();

            lookUpEdit10.Properties.DataSource = list;
            lookUpEdit10.Properties.BestFitRowCount = list == null ? 0 : list.Count;
            lookUpEdit10.Refresh();
        }

        private void LoadFormulas()
        {
            List<string> list = AllDefine.dm_loaithutuc_truongdulieu_lookup_conditioncombines;
            lookUpEdit9.Properties.DataSource = list;
            lookUpEdit9.Properties.BestFitRowCount = list == null ? 0 : list.Count;
            lookUpEdit9.Refresh();
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
                currentTable == string.Empty ? null :
                Helpers.ThutucNhiemvu_Truongdulieu.GetListTableColumns(currentTable);

            lookUpEdit2.Properties.DataSource = list;
            lookUpEdit2.Properties.BestFitRowCount = list == null ? 0 : list.Count;
            lookUpEdit2.Refresh();

            lookUpEdit3.Properties.DataSource = list;
            lookUpEdit3.Properties.BestFitRowCount = list == null ? 0 : list.Count;
            lookUpEdit3.Refresh();

            lookUpEdit4.Properties.DataSource = list;
            lookUpEdit4.Properties.BestFitRowCount = list == null ? 0 : list.Count;
            lookUpEdit4.Refresh();

            lookUpEdit5.Properties.DataSource = list;
            lookUpEdit5.Properties.BestFitRowCount = list == null ? 0 : list.Count;
            lookUpEdit5.Refresh();

            lookUpEdit6.Properties.DataSource = list;
            lookUpEdit6.Properties.BestFitRowCount = list == null ? 0 : list.Count;
            lookUpEdit6.Refresh();

            lookUpEdit7.Properties.DataSource = list;
            lookUpEdit7.Properties.BestFitRowCount = list == null ? 0 : list.Count;
            lookUpEdit7.Refresh();

            lookUpEdit11.Properties.DataSource = list;
            lookUpEdit11.Properties.BestFitRowCount = list == null ? 0 : list.Count;
            lookUpEdit11.Refresh();
        }

        private void FillValues()
        {
            currentData = frm.CallBack_GetLookupData();
            if (currentData == null) return;

            lookUpEdit1.EditValue = currentData.Table;
            lookUpEdit1.EditValue = currentData.Table;
            lookUpEdit1.EditValue = currentData.Table;
            lookUpEdit1.EditValue = currentData.Table;
            lookUpEdit1.EditValue = currentData.Table;
            lookUpEdit1.EditValue = currentData.Table;
            lookUpEdit1.EditValue = currentData.Table;
            lookUpEdit1.EditValue = currentData.Table;
            lookUpEdit1.EditValue = currentData.Table;
            lookUpEdit1.EditValue = currentData.Table;
            lookUpEdit1.EditValue = currentData.Table;
            lookUpEdit1.EditValue = currentData.Table;
            lookUpEdit1.EditValue = currentData.Table;
            lookUpEdit1.EditValue = currentData.Table;
        }

        private void btn_capnhat_Click(object sender, EventArgs e)
        {
            if (lookUpEdit7.EditValue == null)
            {
                AllDefine.Show_message("Vui lòng chọn điều kiện lọc dữ liệu!");
                lookUpEdit7.Focus();
                return;
            }
            if (lookUpEdit8.EditValue == null)
            {
                AllDefine.Show_message("Vui lòng chọn điều kiện lọc dữ liệu!");
                lookUpEdit8.Focus();
                return;
            }
            if (textEdit1.Text.Trim() == Guid.Empty.ToString())
            {
                AllDefine.Show_message("Vui lòng chọn điều kiện lọc dữ liệu!");
                textEdit1.Focus();
                return;
            }

            if (lookUpEdit9.EditValue == null)
            {
                if (lookUpEdit11.EditValue == null)
                {
                    AllDefine.Show_message("Vui lòng chọn điều kiện lọc dữ liệu!");
                    lookUpEdit11.Focus();
                    return;
                }
                if (lookUpEdit10.EditValue == null)
                {
                    AllDefine.Show_message("Vui lòng chọn điều kiện lọc dữ liệu!");
                    lookUpEdit10.Focus();
                    return;
                }
                if (textEdit2.Text.Trim() == Guid.Empty.ToString())
                {
                    AllDefine.Show_message("Vui lòng chọn điều kiện lọc dữ liệu!");
                    textEdit2.Focus();
                    return;
                }
            }

            currentData = new DM_LoaiThutucNhiemvu_Truongdulieu_LookupData();
            currentData.ColumnDisplayExtend1 = lookUpEdit5.EditValue == null ? string.Empty : lookUpEdit5.EditValue.ToString();
            currentData.ColumnDisplayExtend2 = lookUpEdit6.EditValue == null ? string.Empty : lookUpEdit6.EditValue.ToString();
            currentData.ColumnDisplayID = lookUpEdit3.EditValue == null ? string.Empty : lookUpEdit3.EditValue.ToString();
            currentData.ColumnDisplayName = lookUpEdit4.EditValue == null ? string.Empty : lookUpEdit4.EditValue.ToString();
            currentData.ColumnSave = lookUpEdit2.EditValue == null ? string.Empty : lookUpEdit2.EditValue.ToString();
            currentData.Condition1 = lookUpEdit7.EditValue == null ? null :
                new DM_LoaiThutucNhiemvu_Truongdulieu_LookupData_Condition()
                {
                    ColumnName = lookUpEdit7.EditValue == null ? string.Empty : lookUpEdit7.EditValue.ToString(),
                    Condition = lookUpEdit8.EditValue == null ? string.Empty : lookUpEdit8.EditValue.ToString(),
                    Value = textEdit1.Text.Trim()
                };
            currentData.Condition2 = (lookUpEdit9.EditValue == null || lookUpEdit11.EditValue == null) ? null :
                new DM_LoaiThutucNhiemvu_Truongdulieu_LookupData_Condition()
                {
                    ColumnName = lookUpEdit11.EditValue == null ? string.Empty : lookUpEdit11.EditValue.ToString(),
                    Condition = lookUpEdit10.EditValue == null ? string.Empty : lookUpEdit10.EditValue.ToString(),
                    Value = textEdit2.Text
                };
            currentData.ConditionCombination = lookUpEdit9.EditValue == null ? string.Empty : lookUpEdit9.EditValue.ToString();
            currentData.Table = lookUpEdit1.EditValue == null ? string.Empty : lookUpEdit1.EditValue.ToString();

            frm.CallBack_UpdateLookupData(currentData, true);
            this.Dispose();
        }

        private void btn_thoat_Click(object sender, EventArgs e)
        {
            frm.CallBack_UpdateLookupData(null, false);
            this.Dispose();
        }

        private void lookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {
            currentTable = lookUpEdit1.EditValue.ToString();
            LoadTableColumns();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            simpleButton1.Visible = false;
            lookUpEdit9.Visible = true;
            lookUpEdit10.Visible = true;
            lookUpEdit11.Visible = true;
            textEdit2.Visible = true;
        }
    }
}