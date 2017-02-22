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
    public partial class FRM_TD_ThuchienNhiemvu_Tab : BaseForm_Data
    {
        FRM_TD_ThuchienNhiemvu frm = null;
        FRM_TD_Thamdinh_Duyet frm_Thamdinh = null;
        FRM_TD_PheduyetNhiemvuTuVBCosan frm_PheduyetVB = null;
        FRM_TD_Pheduyet_DuyetThamdinh frm_PheduyetThamdinh = null;

        public TD_ThuchienNhiemvu_Truongdulieu currentField = null;
        public TD_Thamdinh_Duyet_Truongdulieu currentField_Thamdinh = null;
        public TD_Pheduyet_VB_Truongdulieu currentField_PheduyetVB = null;
        public TD_Pheduyet_Thamdinh_Duyet_Truongdulieu currentField_PheduyetThamdinh = null;

        public string currentState = "NORMAL";
        public FRM_TD_ThuchienNhiemvu_Tab()
        {
            InitializeComponent();
        }

        private void FRM_TD_ThuchienNhiemvu_Tab_Load(object sender, EventArgs e)
        {
            frm = (FRM_TD_ThuchienNhiemvu)Application.OpenForms["FRM_TD_ThuchienNhiemvu"];
            frm_Thamdinh = (FRM_TD_Thamdinh_Duyet)Application.OpenForms["FRM_TD_Thamdinh_Duyet"];
            frm_PheduyetVB = (FRM_TD_PheduyetNhiemvuTuVBCosan)Application.OpenForms["FRM_TD_PheduyetNhiemvuTuVBCosan"];
            frm_PheduyetThamdinh = (FRM_TD_Pheduyet_DuyetThamdinh)Application.OpenForms["FRM_TD_Pheduyet_DuyetThamdinh"];

            if (frm == null && frm_Thamdinh == null && frm_PheduyetVB == null && frm_PheduyetThamdinh == null) this.Dispose();

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

            panelHeader panelHeaderFields = null;
            if (currentField != null)
            {
                List<TD_ThuchienNhiemvu_Truongdulieu> fields = currentField.Children;
                panelHeaderFields = GenerateDynamicFields(fields, currentState);
            }
            else if (currentField_Thamdinh != null)
            {
                List<TD_Thamdinh_Duyet_Truongdulieu> fields = currentField_Thamdinh.Children;
                panelHeaderFields = GenerateDynamicFields_Thamdinh(fields, currentState);
            }
            else if (currentField_PheduyetVB != null)
            {
                List<TD_Pheduyet_VB_Truongdulieu> fields = currentField_PheduyetVB.Children;
                panelHeaderFields = GenerateDynamicFields_PheduyetVB(fields, currentState);
            }
            else if (currentField_PheduyetThamdinh != null)
            {
                List<TD_Pheduyet_Thamdinh_Duyet_Truongdulieu> fields = currentField_PheduyetThamdinh.Children;
                panelHeaderFields = GenerateDynamicFields_PheduyetThamdinh(fields, currentState);
            }

            if (panelHeaderFields != null)
            {
                xtraScrollableControl1.Controls.Add(panelHeaderFields);
                panelHeaderFields.alignCenter(panelHeaderFields.Parent);
            }
        }

        private void btn_capnhat_Click(object sender, EventArgs e)
        {
            if (currentField != null)
            {
                #region TD_Nhiemvu

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
                                All.Show_message("Error code " + result.ErrorCode + ": Lỗi không thể upload tệp!");
                                return;
                            }
                            obj.DM016804 = result.Data.ToString();
                            break;
                        default: break;
                    }
                }
                frm.CallBack_UpdateField(currentField, true);

                #endregion
            }
            else if (currentField_Thamdinh != null)
            {
                #region TD_Thamdinh

                foreach (TD_Thamdinh_Duyet_Truongdulieu obj in currentField_Thamdinh.Children)
                {
                    string ctrId = "ctrValue_" + obj.DM017201.ToString() + "_Thamdinh";
                    Control ctrValue = xtraScrollableControl1.Controls.Find(ctrId, true).FirstOrDefault();
                    if (ctrValue == null) continue;

                    switch (obj.Kieutruong)
                    {
                        case 1:
                            obj.DM017204 = ((TextEdit)ctrValue).Text.Trim();
                            break;
                        case 2:
                            obj.DM017204 = ((TextEdit)ctrValue).Text.Trim();
                            break;
                        case 3:
                            obj.DM017204 = ((CheckEdit)ctrValue).Checked ? "1" : "0";
                            break;
                        case 4:
                            obj.DM017204 = ((DateEdit)ctrValue).DateTime.ToString();
                            break;
                        case 5:
                            TimeEdit ctrTime = (TimeEdit)xtraScrollableControl1.Controls.Find("ctr_" + obj.DM017201.ToString() + "_Time_Thamdinh", true).FirstOrDefault();
                            DateEdit ctrDate = (DateEdit)xtraScrollableControl1.Controls.Find("ctr_" + obj.DM017201.ToString() + "_Date_Thamdinh", true).FirstOrDefault();
                            DateTime moment = new DateTime(ctrDate.DateTime.Year, ctrDate.DateTime.Month, ctrDate.DateTime.Day, ctrTime.Time.Hour, ctrTime.Time.Minute, ctrTime.Time.Second);
                            obj.DM017204 = moment.ToString();
                            break;
                        case 6:
                            obj.DM017204 = ((TimeEdit)ctrValue).Time.ToString();
                            break;
                        case 7:
                            obj.DM017204 = ((MemoEdit)ctrValue).Text.Trim();
                            break;
                        case 8:
                            LookUpEdit lue = ((LookUpEdit)ctrValue);
                            obj.DM017204 = lue.EditValue == null ? string.Empty : lue.EditValue.ToString();
                            break;
                        case 10:
                            ctrValue = xtraScrollableControl1.Controls.Find("ctr_" + obj.DM017201.ToString() + "_Path_Thamdinh", true).FirstOrDefault();
                            string filePath = ((TextEdit)ctrValue).Text.Trim();

                            string uri = Helpers.CreateRequestUrl_UploadFile();
                            uri += "&n=td_thamdinh_duyet_tepdinhkem&fn=" + DateTime.Now.Ticks.ToString();
                            string response = WebUtils.Request_UploadFile(uri, filePath);
                            APIResponseData result = JsonConvert.DeserializeObject<APIResponseData>(response);
                            if (result.ErrorCode != 0)
                            {
                                All.Show_message("Error code " + result.ErrorCode + ": Lỗi không thể upload tệp!");
                                return;
                            }
                            obj.DM017204 = result.Data.ToString();
                            break;
                        default: break;
                    }
                }

                frm_Thamdinh.CallBack_UpdateField(currentField_Thamdinh, true);

                #endregion
            }
            else if (currentField_PheduyetVB != null)
            {
                #region TD_Nhiemvu

                foreach (TD_Pheduyet_VB_Truongdulieu obj in currentField_PheduyetVB.Children)
                {
                    string ctrId = "ctrValue_" + obj.DM017401.ToString();
                    Control ctrValue = xtraScrollableControl1.Controls.Find(ctrId, true).FirstOrDefault();
                    if (ctrValue == null) continue;

                    switch (obj.Kieutruong)
                    {
                        case 1:
                            obj.DM017404 = ((TextEdit)ctrValue).Text.Trim();
                            break;
                        case 2:
                            obj.DM017404 = ((TextEdit)ctrValue).Text.Trim();
                            break;
                        case 3:
                            obj.DM017404 = ((CheckEdit)ctrValue).Checked ? "1" : "0";
                            break;
                        case 4:
                            obj.DM017404 = ((DateEdit)ctrValue).DateTime.ToString();
                            break;
                        case 5:
                            TimeEdit ctrTime = (TimeEdit)xtraScrollableControl1.Controls.Find("ctr_" + obj.DM017401.ToString() + "_Time", true).FirstOrDefault();
                            DateEdit ctrDate = (DateEdit)xtraScrollableControl1.Controls.Find("ctr_" + obj.DM017401.ToString() + "_Date", true).FirstOrDefault();
                            DateTime moment = new DateTime(ctrDate.DateTime.Year, ctrDate.DateTime.Month, ctrDate.DateTime.Day, ctrTime.Time.Hour, ctrTime.Time.Minute, ctrTime.Time.Second);
                            obj.DM017404 = moment.ToString();
                            break;
                        case 6:
                            obj.DM017404 = ((TimeEdit)ctrValue).Time.ToString();
                            break;
                        case 7:
                            obj.DM017404 = ((MemoEdit)ctrValue).Text.Trim();
                            break;
                        case 8:
                            LookUpEdit lue = ((LookUpEdit)ctrValue);
                            obj.DM017404 = lue.EditValue == null ? string.Empty : lue.EditValue.ToString();
                            break;
                        case 10:
                            ctrValue = xtraScrollableControl1.Controls.Find("ctr_" + obj.DM017401.ToString() + "_Path", true).FirstOrDefault();
                            string filePath = ((TextEdit)ctrValue).Text.Trim();

                            string uri = Helpers.CreateRequestUrl_UploadFile();
                            uri += "&n=td_pheduyetnhiemvuvb_tepdinhkem&fn=" + DateTime.Now.Ticks.ToString();
                            string response = WebUtils.Request_UploadFile(uri, filePath);
                            APIResponseData result = JsonConvert.DeserializeObject<APIResponseData>(response);
                            if (result.ErrorCode != 0)
                            {
                                All.Show_message("Error code " + result.ErrorCode + ": Lỗi không thể upload tệp!");
                                return;
                            }
                            obj.DM017404 = result.Data.ToString();
                            break;
                        default: break;
                    }
                }
                frm_PheduyetVB.CallBack_UpdateField(currentField_PheduyetVB, true);

                #endregion
            }
            else if (currentField_PheduyetThamdinh != null)
            {
                #region TD_Nhiemvu

                foreach (TD_Pheduyet_Thamdinh_Duyet_Truongdulieu obj in currentField_PheduyetThamdinh.Children)
                {
                    string ctrId = "ctrValue_" + obj.DM017601.ToString() + "_PheduyetThamdinh";
                    Control ctrValue = xtraScrollableControl1.Controls.Find(ctrId, true).FirstOrDefault();
                    if (ctrValue == null) continue;

                    switch (obj.Kieutruong)
                    {
                        case 1:
                            obj.DM017604 = ((TextEdit)ctrValue).Text.Trim();
                            break;
                        case 2:
                            obj.DM017604 = ((TextEdit)ctrValue).Text.Trim();
                            break;
                        case 3:
                            obj.DM017604 = ((CheckEdit)ctrValue).Checked ? "1" : "0";
                            break;
                        case 4:
                            obj.DM017604 = ((DateEdit)ctrValue).DateTime.ToString();
                            break;
                        case 5:
                            TimeEdit ctrTime = (TimeEdit)xtraScrollableControl1.Controls.Find("ctr_" + obj.DM017601.ToString() + "_Time_PheduyetThamdinh", true).FirstOrDefault();
                            DateEdit ctrDate = (DateEdit)xtraScrollableControl1.Controls.Find("ctr_" + obj.DM017601.ToString() + "_Date_PheduyetThamdinh", true).FirstOrDefault();
                            DateTime moment = new DateTime(ctrDate.DateTime.Year, ctrDate.DateTime.Month, ctrDate.DateTime.Day, ctrTime.Time.Hour, ctrTime.Time.Minute, ctrTime.Time.Second);
                            obj.DM017604 = moment.ToString();
                            break;
                        case 6:
                            obj.DM017604 = ((TimeEdit)ctrValue).Time.ToString();
                            break;
                        case 7:
                            obj.DM017604 = ((MemoEdit)ctrValue).Text.Trim();
                            break;
                        case 8:
                            LookUpEdit lue = ((LookUpEdit)ctrValue);
                            obj.DM017604 = lue.EditValue == null ? string.Empty : lue.EditValue.ToString();
                            break;
                        case 10:
                            ctrValue = xtraScrollableControl1.Controls.Find("ctr_" + obj.DM017601.ToString() + "_Path_PheduyetThamdinh", true).FirstOrDefault();
                            string filePath = ((TextEdit)ctrValue).Text.Trim();

                            string uri = Helpers.CreateRequestUrl_UploadFile();
                            uri += "&n=td_pheduyetnhiemvutd_tepdinhkem&fn=" + DateTime.Now.Ticks.ToString();
                            string response = WebUtils.Request_UploadFile(uri, filePath);
                            APIResponseData result = JsonConvert.DeserializeObject<APIResponseData>(response);
                            if (result.ErrorCode != 0)
                            {
                                All.Show_message("Error code " + result.ErrorCode + ": Lỗi không thể upload tệp!");
                                return;
                            }
                            obj.DM017604 = result.Data.ToString();
                            break;
                        default: break;
                    }
                }
                frm_PheduyetThamdinh.CallBack_UpdateField(currentField_PheduyetThamdinh, true);

                #endregion
            }

            this.Dispose();
        }

        private void btn_thoat_Click(object sender, EventArgs e)
        {
            if (frm != null)
                frm.CallBack_UpdateField(null, false);
            else if (frm_Thamdinh != null)
                frm_Thamdinh.CallBack_UpdateField(null, false);
            else if (frm_PheduyetVB != null)
                frm_PheduyetVB.CallBack_UpdateField(null, false);
            else if (frm_PheduyetThamdinh != null)
                frm_PheduyetThamdinh.CallBack_UpdateField(null, false);

            this.Dispose();
        }
    }
}
