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
    public partial class FRM_DM_ThuTuc_NhiemVu_Cothienthi : BaseForm_Data
    {
        FRM_DM_ThuTuc_NhiemVu frm = null;
        public List<TD_ThuchienNhiemvu_Cothienthi> currentList = null;
        public string currentState = "NORMAL";
        public FRM_DM_ThuTuc_NhiemVu_Cothienthi()
        {
            InitializeComponent();
        }

        private void FRM_DM_ThuTuc_NhiemVu_Cothienthi_Load(object sender, EventArgs e)
        {
            frm = (FRM_DM_ThuTuc_NhiemVu)Application.OpenForms["FRM_DM_ThuTuc_NhiemVu"];
            if (frm == null) this.Dispose();

            panelHeader1.alignCenter(panelHeader1.Parent);
            gridColumn10.ColumnEdit.EditValueChanged += ColumnEdit_EditValueChanged;

            gridView1.OptionsBehavior.Editable = true;

            LoadList();
            //CheckSelecteds();

            if (currentState == "NORMAL")
                btn_capnhat.Enabled = false;
            else
                btn_capnhat.Enabled = true;
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

        //private void CheckSelecteds()
        //{
        //    List<DM_Huongdan> selecteds = frm.CallBack_Huongdan_GetCurrentSelecteds();
        //    if (selecteds == null) return;

        //    for (int i = 0; i < gridView1.RowCount; i++)
        //    {
        //        DM_LoaiThutucNhiemvu_Truongdulieu obj = (DM_LoaiThutucNhiemvu_Truongdulieu)gridView1.GetRow(i);
        //        if (selecteds.Contains(obj.DM016201))
        //            gridView1.SetRowCellValue(i, gridColumn10, true);
        //    }
        //}

        private void LoadList()
        {
            gridControl1.DataSource = currentList;
            gridControl1.RefreshDataSource();
            gridView1.BestFitColumns();
        }

        private void btn_capnhat_Click(object sender, EventArgs e)
        {
            List<TD_ThuchienNhiemvu_Cothienthi> dsCot = new List<TD_ThuchienNhiemvu_Cothienthi>();
            for (int i = 0; i < gridView1.RowCount; i++)
            {
                bool isChecked = (bool)gridView1.GetRowCellValue(i, gridColumn10);
                string columnName = gridView1.GetRowCellValue(i, gridColumn1).ToString();
                string displayName = gridView1.GetRowCellValue(i, gridColumn2).ToString();

                dsCot.Add(new TD_ThuchienNhiemvu_Cothienthi()
                {
                    ColumnName = columnName,
                    DisplayName = displayName,
                    IsChecked = isChecked
                });
            }

            frm.dsCothienthi = dsCot;
            frm.Enabled = true;
            frm.Focus();
            this.Dispose();
        }

        private void checkEdit1_EditValueChanged(object sender, EventArgs e)
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

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            frm.Enabled = true;
            frm.Focus();
            this.Dispose();
        }
    }
}
