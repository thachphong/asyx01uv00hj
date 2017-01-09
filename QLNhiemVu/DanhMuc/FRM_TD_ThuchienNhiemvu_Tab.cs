using DBAccess;
using Decided.Libs;
using DevExpress.XtraEditors;
using Newtonsoft.Json;
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
    public partial class FRM_TD_ThuchienNhiemvu_Tab : BaseForm_Data
    {
        FRM_TD_ThuchienNhiemvu frm = null;
        public TD_ThuchienNhiemvu_Truongdulieu currentField = null;
        public string currentState = "NORMAL";
        public FRM_TD_ThuchienNhiemvu_Tab()
        {
            InitializeComponent();
        }

        private void FRM_TD_ThuchienNhiemvu_Tab_Load(object sender, EventArgs e)
        {
            frm = (FRM_TD_ThuchienNhiemvu)Application.OpenForms["FRM_TD_ThuchienNhiemvu"];
            if (frm == null) this.Dispose();

            panelHeader1.alignCenter(panelHeader1.Parent);

            LoadList();

            if (currentState == "NORMAL")
                btn_capnhat.Enabled = false;
            else
                btn_capnhat.Enabled = true;
        }

        private void LoadList()
        {
            xtraScrollableControl1.Controls.Clear();

            List<TD_ThuchienNhiemvu_Truongdulieu> fields = currentField.Children;

            panelHeader panelHeaderFields = GenerateDynamicFields(fields, currentState);

            if (panelHeaderFields != null)
            {
                xtraScrollableControl1.Controls.Add(panelHeaderFields);
                panelHeaderFields.alignCenter(panelHeaderFields.Parent);
            }
        }

        private void btn_capnhat_Click(object sender, EventArgs e)
        {
            foreach (TD_ThuchienNhiemvu_Truongdulieu obj in currentField.Children)
            {
                string ctrId = "ctrValue_" + obj.DM016801.ToString();
                Control ctrValue = xtraScrollableControl1.Controls.Find(ctrId, true).FirstOrDefault();
                if (ctrValue == null) continue;

                switch (obj.Kieutruong)
                {
                    case 1:
                        obj.DM016804 = ((TextEdit)ctrValue).Text.Trim();
                        break;
                    case 2:
                        obj.DM016804 = ((TextEdit)ctrValue).Text.Trim();
                        break;
                    case 3:
                        obj.DM016804 = ((CheckEdit)ctrValue).Checked ? "1" : "0";
                        break;
                    case 4:
                        obj.DM016804 = ((DateEdit)ctrValue).DateTime.ToString();
                        break;
                    case 5:
                        TimeEdit ctrTime = (TimeEdit)xtraScrollableControl1.Controls.Find("ctr_" + obj.DM016801.ToString() + "_Time", true).FirstOrDefault();
                        DateEdit ctrDate = (DateEdit)xtraScrollableControl1.Controls.Find("ctr_" + obj.DM016801.ToString() + "_Date", true).FirstOrDefault();
                        DateTime moment = new DateTime(ctrDate.DateTime.Year, ctrDate.DateTime.Month, ctrDate.DateTime.Day, ctrTime.Time.Hour, ctrTime.Time.Minute, ctrTime.Time.Second);
                        obj.DM016804 = moment.ToString();
                        break;
                    case 6:
                        obj.DM016804 = ((TimeEdit)ctrValue).Time.ToString();
                        break;
                    case 7:
                        obj.DM016804 = ((MemoEdit)ctrValue).Text.Trim();
                        break;
                    case 8:
                        LookUpEdit lue = ((LookUpEdit)ctrValue);
                        obj.DM016804 = lue.EditValue == null ? string.Empty : lue.EditValue.ToString();
                        break;
                    case 10:
                        ctrValue = xtraScrollableControl1.Controls.Find("ctr_" + obj.DM016801.ToString() + "_Path", true).FirstOrDefault();
                        string filePath = ((TextEdit)ctrValue).Text.Trim();

                        string uri = Helpers.CreateRequestUrl_UploadFile();
                        uri += "&n=td_thuchiennhiemvu_tepdinhkem&fn=" + DateTime.Now.Ticks.ToString();
                        string response = WebUtils.Request_UploadFile(uri, filePath);
                        APIResponseData result = JsonConvert.DeserializeObject<APIResponseData>(response);
                        if (result.ErrorCode != 0)
                        {
                            AllDefine.Show_message("Error code " + result.ErrorCode + ": Lỗi không thể upload tệp!");
                            return;
                        }
                        obj.DM016804 = result.Data.ToString();
                        break;
                    default: break;
                }
            }

            frm.CallBack_UpdateField(currentField, true);
            this.Dispose();
        }

        private void btn_thoat_Click(object sender, EventArgs e)
        {
            frm.CallBack_UpdateField(null, false);
            this.Dispose();
        }
    }
}
