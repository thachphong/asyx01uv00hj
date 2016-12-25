using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLNhiemVu.FRMModel
{
    public partial class FRM_Model2 : Form
    {
        public FRM_Model2()
        {
            InitializeComponent();
        }
        protected string gs_title_from = "Title1";
        //protected string ls_status_master = "NORMAL";
        //protected string ls_status_detail = "NORMAL";
        //protected Guid ls_master_id;
        //protected DataTable dt_master;
        //protected int master_curentrow = 0;
        //protected int detail_currentrow = 0;

        private void FRM_Model2_Load(object sender, EventArgs e)
        {
            //AllDefine.set_val_forDBConnection();
                lblHeadTitle1.setText(gs_title_from);
                lblHeadTitle1.alignCenter(lblHeadTitle1.Parent);
                panelHeader1.alignCenter(panelHeader1.Parent);
                int[] y = {30};

                panelHeader2.alignCenter(panelHeader2.Parent,y);
            //    //groupLeft.Width = AllDefine.gi_Gridcontrol_width;
            //    this.KeyPreview = true;
        }
       
    }
}
