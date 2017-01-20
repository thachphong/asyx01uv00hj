using DBAccess;
using Decided.Libs;
using DevExpress.XtraEditors;
using Newtonsoft.Json;
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
    public partial class FRM_TD_ThuchienNhiemvu_AttachedFiles : BaseForm_Data
    {
        FRM_TD_ThuchienNhiemvu frm = null;
        List<TD_ThuchienNhiemvu_Tepdinhkem> currentList = null;
        public string currentState = "NORMAL";
        public FRM_TD_ThuchienNhiemvu_AttachedFiles()
        {
            InitializeComponent();
        }

        private void FRM_TD_ThuchienNhiemvu_AttachedFiles_Load(object sender, EventArgs e)
        {
            frm = (FRM_TD_ThuchienNhiemvu)Application.OpenForms["FRM_TD_ThuchienNhiemvu"];
            if (frm == null) this.Dispose();

            panelHeader1.alignCenter(panelHeader1.Parent);
            gridColumn10.ColumnEdit.EditValueChanged += ColumnEdit_EditValueChanged;

            currentList = frm.CallBack_Tepdinhkem_GetCurrentAttacheds();
            LoadList();
            //CheckSelecteds();

            if (currentState == "NORMAL")
            {
                btn_capnhat.Enabled = false;
                btn_chontep.Enabled = false;
            }
            else
            {
                btn_capnhat.Enabled = true;
                btn_chontep.Enabled = true;
            }
        }

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
            //List<Guid> selecteds = frm.CallBack_GetTabData();
            //if (selecteds == null) return;

            //for (int i = 0; i < gridView1.RowCount; i++)
            //{
            //    DM_LoaiThutucNhiemvu_Truongdulieu obj = (DM_LoaiThutucNhiemvu_Truongdulieu)gridView1.GetRow(i);
            //    if (selecteds.Contains(obj.DM016201))
            //        gridView1.SetRowCellValue(i, gridColumn10, true);
            //}
        }

        private void LoadList()
        {
            gridControl1.DataSource = currentList;
            gridControl1.RefreshDataSource();
            gridView1.BestFitColumns();
        }

        private void btn_capnhat_Click(object sender, EventArgs e)
        {
            currentList = new List<TD_ThuchienNhiemvu_Tepdinhkem>();
            for (int i = 0; i < gridView1.RowCount; i++)
            {
                bool isChecked = (bool)gridView1.GetRowCellValue(i, gridColumn10);
                if (isChecked) currentList.Add(new TD_ThuchienNhiemvu_Tepdinhkem()
                {
                    Filename = gridView1.GetRowCellValue(i, gridColumn2).ToString(),
                    Id = Guid.Parse(gridView1.GetRowCellValue(i, gridColumn1).ToString()),
                    IsChecked = true,
                    Path = gridView1.GetRowCellValue(i, gridColumn3).ToString()
                });
            }

            frm.CallBack_UpdateAttacheds(currentList.Count > 0 ? currentList : null, true);
            this.Dispose();
        }

        private void btn_thoat_Click(object sender, EventArgs e)
        {
            frm.CallBack_UpdateAttacheds(null, false);
            this.Dispose();
        }

        private void btn_chontep_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (currentList == null) currentList = new List<TD_ThuchienNhiemvu_Tepdinhkem>();

                foreach (string filePath in openFileDialog1.FileNames)
                {
                    string fileName = DateTime.Now.Ticks.ToString();
                    string uri = Helpers.CreateRequestUrl_UploadFile();
                    uri += "&n=td_thuchiennhiemvu_tepdinhkem&fn=" + fileName;
                    string response = WebUtils.Request_UploadFile(uri, filePath);
                    APIResponseData result = JsonConvert.DeserializeObject<APIResponseData>(response);
                    if (result.ErrorCode != 0)
                    {
                        All.Show_message("Error code " + result.ErrorCode + ": Lỗi không thể upload tệp!");
                        return;
                    }
                    else
                    {
                        currentList.Add(new TD_ThuchienNhiemvu_Tepdinhkem()
                        {
                            Filename = fileName,
                            Id = Guid.NewGuid(),
                            IsChecked = true,
                            Path = result.Data.ToString()
                        });
                    }
                }

                LoadList();
            }
        }

        bool performChecked = true;

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
