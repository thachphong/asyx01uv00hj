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
    public partial class FRM_DM_Thutuc_Saochep : Form
    {
        public FRM_DM_Thutuc frm = null;
        public DM_LoaiThutucNhiemvu_Noidung currentSelected = null;
        public FRM_DM_Thutuc_Saochep()
        {
            InitializeComponent();
        }

        private void FRM_DM_Thutuc_Saochep_Load(object sender, EventArgs e)
        {
            frm = (FRM_DM_Thutuc)Application.OpenForms["FRM_DM_Thutuc"];
            if (frm == null) this.Dispose();

            panelHeader1.alignCenter(panelHeader1.Parent);
            simpleButton2.Enabled = false;

            LoadList_LoaiThutuc();
        }

        private void LoadList_LoaiThutuc()
        {
            List<DM_LoaiThutucNhiemvu> all = Helpers.ThutucNhiemvu.GetList();

            m_loai_thutuc_lk.Properties.DataSource = all;
            m_loai_thutuc_lk.Properties.DisplayMember = "DM016004";
            m_loai_thutuc_lk.Properties.ValueMember = "DM016001";
            m_loai_thutuc_lk.Properties.BestFitRowCount = all.Count;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            LoadList_Noidung();
        }

        private void LoadList_Noidung()
        {
            List<DM_LoaiThutucNhiemvu_Noidung> all = null;
            if (m_loai_thutuc_lk.EditValue == null || m_loai_thutuc_lk.EditValue.ToString() == Guid.Empty.ToString())
                all = null;
            else
            {
                all = Helpers.ThutucNhiemvu_Noidung.GetList(Guid.Parse(m_loai_thutuc_lk.EditValue.ToString()));
            }

            gridControl2.DataSource = all;
            gridControl2.RefreshDataSource();

            gridView2.OptionsBehavior.Editable = false;
        }

        private void gridView2_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            if (e.Row == null)
            {
                simpleButton2.Enabled = false;
                currentSelected = null;
            }
            else
            {
                simpleButton2.Enabled = true;
                currentSelected = (DM_LoaiThutucNhiemvu_Noidung)e.Row;
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (currentSelected == null) return;

            frm.UpdateChildForm_Clone(currentSelected);
            this.Dispose();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            frm.UpdateChildForm_Clone(null);
            this.Dispose();
        }
    }
}
