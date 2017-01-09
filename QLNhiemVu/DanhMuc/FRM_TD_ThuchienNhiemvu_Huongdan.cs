using DBAccess;
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
    public partial class FRM_TD_ThuchienNhiemvu_Huongdan : BaseForm_Data
    {
        FRM_TD_ThuchienNhiemvu frm = null;
        List<DM_Huongdan> currentList = null;
        public string currentState = "NORMAL";
        public FRM_TD_ThuchienNhiemvu_Huongdan()
        {
            InitializeComponent();
        }

        private void FRM_TD_ThuchienNhiemvu_Huongdan_Load(object sender, EventArgs e)
        {
            frm = (FRM_TD_ThuchienNhiemvu)Application.OpenForms["FRM_TD_ThuchienNhiemvu"];
            if (frm == null) this.Dispose();

            panelHeader1.alignCenter(panelHeader1.Parent);
            gridColumn10.ColumnEdit.Click += ColumnEdit_Click;

            LoadList();
            //CheckSelecteds();

            //if (currentState == "NORMAL")
            //    btn_capnhat.Enabled = false;
            //else
            //    btn_capnhat.Enabled = true;
        }

        void ColumnEdit_Click(object sender, EventArgs e)
        {
            DM_Huongdan obj = (DM_Huongdan)gridView1.GetFocusedRow();
            System.Diagnostics.Process.Start((DefineValues.API_Host + obj.DM016310).Replace("//", "/"));
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
            currentList = frm.CallBack_Huongdan_GetCurrentSelecteds();

            gridControl1.DataSource = currentList;
            gridControl1.RefreshDataSource();
            gridView1.BestFitColumns();
        }

        private void btn_capnhat_Click(object sender, EventArgs e)
        {
            //frm.CallBack_Huongdan_Update();
            this.Dispose();
        }

        //private void btn_thoat_Click(object sender, EventArgs e)
        //{
        //    frm.CallBack_Thamdinh(null, false);
        //    this.Dispose();
        //}
    }
}
