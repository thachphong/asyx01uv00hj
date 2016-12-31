using DBAccess;
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
    public partial class FRM_DM_LoaiThutucNhiemvu_QuitrinhThamdinh : Form
    {
        FRM_DM_LoaiThutucNhiemvu frm = null;
        List<DM_LoaiThutucNhiemvu_Truongdulieu> currentList = null;
        public FRM_DM_LoaiThutucNhiemvu_QuitrinhThamdinh()
        {
            InitializeComponent();
        }

        private void FRM_DM_LoaiThutucNhiemvu_QuitrinhThamdinh_Load(object sender, EventArgs e)
        {
            frm = (FRM_DM_LoaiThutucNhiemvu)Application.OpenForms["FRM_DM_LoaiThutucNhiemvu"];
            if (frm == null) this.Dispose();

            panelHeader1.alignCenter(panelHeader1.Parent);

            LoadList();
            CheckSelecteds();
        }

        private void CheckSelecteds()
        {
            List<Guid> selecteds = frm.CallBack_GetCurrentFieldSelected();
            if (selecteds == null) return;

            for (int i = 0; i < gridView1.RowCount; i++)
            {
                DM_LoaiThutucNhiemvu_Truongdulieu obj = (DM_LoaiThutucNhiemvu_Truongdulieu)gridView1.GetRow(i);
                if (selecteds.Contains(obj.DM016201))
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

            frm.CallBack_Thamdinh(selecteds.Count == 0 ? null : selecteds, true);
            this.Dispose();
        }

        private void btn_thoat_Click(object sender, EventArgs e)
        {
            frm.CallBack_Thamdinh(null, false);
            this.Dispose();
        }
    }
}
