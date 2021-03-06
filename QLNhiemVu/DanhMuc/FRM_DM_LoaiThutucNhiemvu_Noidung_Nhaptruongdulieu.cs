﻿using DBAccess;
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
    public partial class FRM_DM_LoaiThutucNhiemvu_Noidung_Nhaptruongdulieu : BaseForm_Data
    {
        FRM_DM_ThuTuc_NhiemVu frm = null;
        List<DM_LoaiThutucNhiemvu_Truongdulieu> currentList = null;
        public List<Guid> currentData = null;
        public string currentState = "NORMAL";

        public FRM_DM_LoaiThutucNhiemvu_Noidung_Nhaptruongdulieu()
        {
            InitializeComponent();
        }

        private void FRM_DM_LoaiThutucNhiemvu_Noidung_Nhaptruongdulieu_Load(object sender, EventArgs e)
        {
            frm = (FRM_DM_ThuTuc_NhiemVu)Application.OpenForms["FRM_DM_ThuTuc_NhiemVu"];
            if (frm == null) this.Dispose();

            panelHeader1.alignCenter(panelHeader1.Parent);

            LoadList();
            CheckSelecteds();

            if (currentState == "NORMAL")
                btn_capnhat.Enabled = false;
            else
                btn_capnhat.Enabled = true;

            gridColumn10.ColumnEdit.EditValueChanged += ColumnEdit_EditValueChanged;
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

        private void CheckSelecteds()
        {
            if (currentData == null) return;

            for (int i = 0; i < gridView1.RowCount; i++)
            {
                DM_LoaiThutucNhiemvu_Truongdulieu obj = (DM_LoaiThutucNhiemvu_Truongdulieu)gridView1.GetRow(i);
                if (currentData.Contains(obj.DM016201))
                    gridView1.SetRowCellValue(i, gridColumn10, true);
            }
        }

        private void LoadList()
        {
            currentList = Helpers.ThutucNhiemvu_Truongdulieu.GetList();

            gridControl1.DataSource = currentList;
            gridControl1.RefreshDataSource();
            gridView1.BestFitColumns();
        }

        private void btn_capnhat_Click(object sender, EventArgs e)
        {
            List<Guid> selecteds = new List<Guid>();

            for (int i = 0; i < gridView1.RowCount; i++)
            {
                DM_LoaiThutucNhiemvu_Truongdulieu obj = (DM_LoaiThutucNhiemvu_Truongdulieu)gridView1.GetRow(i);
                if ((bool)gridView1.GetRowCellValue(i, gridColumn10))
                    selecteds.Add(obj.DM016201);
            }

            frm.currentNoidungTruongdulieus = selecteds.Count == 0 ? null : selecteds;
            this.Close();
            frm.CallBack_UpdateFieldSelecteds(true);
            this.Dispose();
        }

        private void btn_thoat_Click(object sender, EventArgs e)
        {
            this.Close();
            frm.CallBack_UpdateFieldSelecteds(false);
            this.Dispose();
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
    }
}
