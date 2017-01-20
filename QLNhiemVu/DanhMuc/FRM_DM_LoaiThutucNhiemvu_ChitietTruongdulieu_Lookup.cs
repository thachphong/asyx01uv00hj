using DBAccess;
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
    public partial class FRM_DM_LoaiThutucNhiemvu_ChitietTruongdulieu_Lookup : BaseForm_Data
    {
        public DM_LoaiThutucNhiemvu_Truongdulieu_LookupData currentData = null;
        string currentTable = string.Empty;
        public string currentState = "NORMAL";
        public string formType = string.Empty;

        public FRM_DM_LoaiThutucNhiemvu_ChitietTruongdulieu_Lookup()
        {
            InitializeComponent();
        }

        private void FRM_DM_LoaiThutucNhiemvu_QuitrinhThamdinh_Load(object sender, EventArgs e)
        {
            //frm = (FRM_DM_ThuTuc_NhiemVu)Application.OpenForms["FRM_DM_ThuTuc_NhiemVu"];
            //if (frm == null) this.Dispose();

            panelHeader1.alignCenter(panelHeader1.Parent);

            LoadTables();
            LoadFormulas();
            LoadConditionCombines();

            FillValues();

            if (currentState == "NORMAL")
                btn_capnhat.Enabled = false;
            else
                btn_capnhat.Enabled = true;
        }

        private void LoadConditionCombines()
        {
            List<string> list = All.dm_loaithutuc_truongdulieu_lookup_formulas;
            lookUpEdit8.Properties.DataSource = list;
            lookUpEdit8.Properties.BestFitRowCount = list == null ? 0 : list.Count;
            lookUpEdit8.Refresh();

            lookUpEdit10.Properties.DataSource = list;
            lookUpEdit10.Properties.BestFitRowCount = list == null ? 0 : list.Count;
            lookUpEdit10.Refresh();
        }

        private void LoadFormulas()
        {
            List<string> list = All.dm_loaithutuc_truongdulieu_lookup_conditioncombines;
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
            //currentData = frm.CallBack_GetLookupData();
            if (currentData == null) return;

            lookUpEdit1.EditValue = currentData.Table;
            lookUpEdit2.EditValue = currentData.ColumnSave;
            lookUpEdit3.EditValue = currentData.ColumnDisplayID;
            lookUpEdit4.EditValue = currentData.ColumnDisplayName;
            lookUpEdit5.EditValue = currentData.ColumnDisplayExtend1;
            lookUpEdit6.EditValue = currentData.ColumnDisplayExtend2;
            lookUpEdit7.EditValue = currentData.Condition1 == null ? string.Empty : currentData.Condition1.ColumnName;
            lookUpEdit8.EditValue = currentData.Condition1 == null ? string.Empty : currentData.Condition1.Condition;
            textEdit1.Text = currentData.Condition1 == null ? string.Empty : currentData.Condition1.Value;
            lookUpEdit11.EditValue = currentData.Condition2 == null ? string.Empty : currentData.Condition2.ColumnName;
            lookUpEdit10.EditValue = currentData.Condition2 == null ? string.Empty : currentData.Condition2.Condition;
            textEdit2.Text = currentData.Condition2 == null ? string.Empty : currentData.Condition2.Value;
            lookUpEdit9.EditValue = currentData.ConditionCombination;

            if (currentData.ConditionCombination.Trim() != string.Empty)
            {
                simpleButton1.Visible = false;
                lookUpEdit11.Visible = true;
                lookUpEdit10.Visible = true;
                lookUpEdit9.Visible = true;
                textEdit2.Visible = true;
            }
            else
            {
                simpleButton1.Visible = true;
                lookUpEdit11.Visible = false;
                lookUpEdit10.Visible = false;
                lookUpEdit9.Visible = false;
                textEdit2.Visible = false;
            }
        }

        private void btn_capnhat_Click(object sender, EventArgs e)
        {
            if (lookUpEdit7.EditValue == null)
            {
                All.Show_message("Vui lòng chọn điều kiện lọc dữ liệu!");
                lookUpEdit7.Focus();
                return;
            }
            if (lookUpEdit8.EditValue == null)
            {
                All.Show_message("Vui lòng chọn điều kiện lọc dữ liệu!");
                lookUpEdit8.Focus();
                return;
            }
            if (textEdit1.Text.Trim() == Guid.Empty.ToString())
            {
                All.Show_message("Vui lòng chọn điều kiện lọc dữ liệu!");
                textEdit1.Focus();
                return;
            }

            if (lookUpEdit9.EditValue != null)
            {
                if (lookUpEdit11.EditValue == null || lookUpEdit11.EditValue.ToString().Trim() == string.Empty)
                {
                    All.Show_message("Vui lòng chọn điều kiện lọc dữ liệu!");
                    lookUpEdit11.Focus();
                    return;
                }
                if (lookUpEdit10.EditValue == null || lookUpEdit10.EditValue.ToString().Trim() == string.Empty)
                {
                    All.Show_message("Vui lòng chọn điều kiện lọc dữ liệu!");
                    lookUpEdit10.Focus();
                    return;
                }
                if (textEdit2.Text.Trim() == string.Empty)
                {
                    All.Show_message("Vui lòng chọn điều kiện lọc dữ liệu!");
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

            if (formType == "1")
            {
                FRM_DM_ThuTuc_NhiemVu frm = (FRM_DM_ThuTuc_NhiemVu)Application.OpenForms["FRM_DM_ThuTuc_NhiemVu"];
                frm.currentTruongdulieu_Lookupdata = currentData;
                this.Close();
                frm.CallBack_UpdateLookupData(true);
            }
            else
            {
                FRM_DM_ThuTuc_NhiemVu_Truongcon frm = (FRM_DM_ThuTuc_NhiemVu_Truongcon)Application.OpenForms["FRM_DM_ThuTuc_NhiemVu_Truongcon"];
                frm.currentTruongdulieu_Lookupdata = currentData;
                this.Close();
                frm.CallBack_UpdateLookupData(true);
            }

            this.Dispose();
        }

        private void btn_thoat_Click(object sender, EventArgs e)
        {
            this.Close();

            if (formType == "1")
            {
                FRM_DM_ThuTuc_NhiemVu frm = (FRM_DM_ThuTuc_NhiemVu)Application.OpenForms["FRM_DM_ThuTuc_NhiemVu"];
                frm.CallBack_UpdateLookupData(false);
            }
            else
            {
                FRM_DM_ThuTuc_NhiemVu_Truongcon frm = (FRM_DM_ThuTuc_NhiemVu_Truongcon)Application.OpenForms["FRM_DM_ThuTuc_NhiemVu_Truongcon"];
                frm.CallBack_UpdateLookupData(false);
            }

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
