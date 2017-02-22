using DBAccess;
using Decided.Libs;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using Newtonsoft.Json;
using QLNhiemVu.FRMModel;
using QLNhiemvu_DBEntities;
using QLNhiemVu_Defines;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLNhiemVu.DanhMuc
{
    public partial class FRM_TD_Phancong_DonvinhanVB : BaseForm_Data
    {
        public List<TD_DonviQuanly> currentList = null;
        public List<TD_DonviQuanly> currentListSelected = null;

        private Guid chutriID = Guid.Empty;
        private FRM_TD_Phancong frm = null;
        public FRM_TD_Phancong_DonvinhanVB()
        {
            InitializeComponent();
        }

        private void FRM_TD_Phancong_DonvinhanVB_Load(object sender, EventArgs e)
        {
            frm = (FRM_TD_Phancong)Application.OpenForms["FRM_TD_Phancong"];
            if (frm == null) this.Dispose();

            lblHeadTitle1.setText("Danh sách đơn vị nhận phân công");
            lblHeadTitle1.alignCenter(lblHeadTitle1.Parent);
            panelHeader1.alignCenter(panelHeader1.Parent);
            panelHeader2.alignCenter(panelHeader2.Parent);

            BindControlEvents();

            LoadList();
        }

        private void BindControlEvents()
        {
            gridColumn10.ColumnEdit.EditValueChanged += ColumnEdit_EditValueChanged;
            repositoryItemCheckEdit2.EditValueChanged += repositoryItemCheckEdit2_EditValueChanged;
        }

        void repositoryItemCheckEdit2_EditValueChanged(object sender, EventArgs e)
        {
            gridView1.PostEditor();
            if (((CheckEdit)sender).Checked)
            {
                chutriID = ((TD_DonviQuanly)gridView1.GetFocusedRow()).DM030101;

                for (int i = 0; i < currentList.Count; i++)
                {
                    currentList[i].IsLeader = currentList[i].DM030101 == chutriID;
                }

                LoadList();
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
            if (currentList != null && currentListSelected != null)
            {
                foreach (TD_DonviQuanly obj in currentList)
                {
                    TD_DonviQuanly selected = currentListSelected.FirstOrDefault(o => o.DM030101 == obj.DM030101);
                    if (selected != null)
                        obj.IsChecked = true;
                }
            }

            gridControl1.DataSource = currentList;
            gridControl1.RefreshDataSource();
            gridView1.BestFitColumns();

            currentListSelected = null;
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
        }

        private void btn_capnhat_Click(object sender, EventArgs e)
        {
            //Xử lý data
            currentListSelected = new List<TD_DonviQuanly>();
            for (int i = 0; i < gridView1.RowCount; i++)
            {
                TD_DonviQuanly obj = (TD_DonviQuanly)gridView1.GetRow(i);
                if (obj.IsChecked)
                {
                    //if (obj.DM030101 == chutriID)
                    //    obj.IsLeader = true;

                    currentListSelected.Add(obj);
                }
            }

            frm.DsDonvinhanVB = currentListSelected.Count == 0 ? null : currentListSelected;
            frm.CallBack_UpdateDsDonvinhanVB();
        }

        private void btn_thoat_Click_1(object sender, EventArgs e)
        {
            frm.CallBack_UpdateDsDonvinhanVB();
        }
    }
}
