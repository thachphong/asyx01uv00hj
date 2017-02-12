using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QLNhiemvu_DBEntities;
using DBAccess;
using QLNhiemVu.DanhMuc;
using DevExpress.XtraEditors;
using QLNhiemVu.FRMModel;
using System.IO;
using Decided.Libs;
using Newtonsoft.Json;
using QLNhiemVu_Defines;

namespace QLNhiemVu.User_Control
{
    public partial class UC_Thamdinh_Duyet_Thamdinhvatrinh : CB_Thamdinh_Duyet_Chitiet, I_UC_Thamdinh_Duyet_Chitiet
    {
        private static List<TD_Thamdinh_Duyet_Truongdulieu> tempFields = null;
        private static List<TD_ThuchienNhiemvu_Truongdulieu> tempFields_Nhiemvu = null;
        private static bool isEditing = false;
        public UC_Thamdinh_Duyet_Thamdinhvatrinh()
        {
            InitializeComponent();
        }

        private void UC_Thamdinh_Duyet_Thamdinhvatrinh_Load(object sender, EventArgs e)
        {
            panelHeader1.alignCenter(panelHeader1.Parent);
            LoadNguoiky();
        }

        private void LoadNguoiky()
        {
            List<TD_Nguoiky> list = Helpers.Trinhduyet.GetList_Nhansu(Guid.Empty, '0', '0');
            lookUpEdit11.Properties.DataSource = list;
            lookUpEdit11.Properties.DisplayMember = "DM030403";
            lookUpEdit11.Properties.ValueMember = "DM030401";
            lookUpEdit11.Properties.BestFitRowCount = list == null ? 0 : list.Count;
        }

        public void AssignData(TD_Thamdinh_Duyet objData)
        {
            textEdit2.Text = objData == null ? string.Empty : objData.DM017111;
            dateEdit3.DateTime = objData == null ? DateTime.Now : objData.DM017112;
            lookUpEdit11.EditValue = objData == null ? Guid.Empty : objData.DM017113;
            textEdit5.Text = objData == null ? string.Empty : ((List<TD_Nguoiky>)lookUpEdit11.Properties.DataSource).FirstOrDefault(o => o.DM030401 == Guid.Parse(lookUpEdit11.EditValue.ToString())).Chucvu;
            textEdit4.Text = objData == null ? string.Empty : objData.DM017114;
            textEdit7.Text = objData == null ? string.Empty : objData.DM017119;
            textEdit6.Text = objData == null ? string.Empty : objData.DM017118;

            xtraScrollableControl2.Controls.Clear();
            xtraScrollableControl3.Controls.Clear();

            FRM_TD_Thamdinh_Duyet frm = (FRM_TD_Thamdinh_Duyet)this.ParentForm;

            if (frm.TD_Nhiemvu.LoaiNoidungchitiet == 1)
            {
                MemoEdit memoEdit = new MemoEdit();
                memoEdit.Name = "mmeText";
                memoEdit.Dock = DockStyle.Fill;
                if (frm.TD_Nhiemvu != null) memoEdit.Text = frm.TD_Nhiemvu.DM016715;
                memoEdit.ReadOnly = frm.currentState == "NORMAL";
                xtraScrollableControl2.Controls.Add(memoEdit);

                MemoEdit memoEdit_Thamdinh = new MemoEdit();
                memoEdit_Thamdinh.Name = "mmeText_Thamdinh";
                memoEdit_Thamdinh.Dock = DockStyle.Fill;
                if (objData != null) memoEdit_Thamdinh.Text = objData.DM017121;
                //memoEdit_Thamdinh.ReadOnly = !isEditing;
                xtraScrollableControl3.Controls.Add(memoEdit_Thamdinh);
            }
            else
            {
                tempFields_Nhiemvu =
                    frm.TD_Nhiemvu == null ? Helpers.TrinhduyetThuchienNhiemvu.GetList_Truongdulieu(frm.TD_Nhiemvu.DM016713, Guid.Empty) :
                    frm.TD_Nhiemvu.Fields != null ? frm.TD_Nhiemvu.Fields :
                    Helpers.TrinhduyetThuchienNhiemvu.GetList_Truongdulieu(frm.TD_Nhiemvu.DM016713, frm.TD_Nhiemvu.DM016701);

                panelHeader panelHeaderFields = ((BaseForm_Data)this.ParentForm).GenerateDynamicFields(tempFields_Nhiemvu, frm.currentState);

                if (panelHeaderFields != null)
                {
                    xtraScrollableControl2.Controls.Add(panelHeaderFields);
                    panelHeaderFields.alignCenter(panelHeaderFields.Parent);
                }

                //Thamdinh
                tempFields =
                    objData == null ? Helpers.TrinhduyetThamdinh.GetList_Truongdulieu(frm.TD_Nhiemvu.DM016713, Guid.Empty, frm.TD_Nhiemvu.DM016701) :
                    objData.Fields != null ? objData.Fields :
                    Helpers.TrinhduyetThamdinh.GetList_Truongdulieu(frm.TD_Nhiemvu.DM016713, objData.DM017101, frm.TD_Nhiemvu.DM016701);

                panelHeader panelHeaderFields_Thamdinh = ((BaseForm_Data)this.ParentForm).GenerateDynamicFields_Thamdinh(tempFields, "EDIT");

                if (panelHeaderFields_Thamdinh != null)
                {
                    xtraScrollableControl3.Controls.Add(panelHeaderFields_Thamdinh);
                    panelHeaderFields_Thamdinh.alignCenter(panelHeaderFields_Thamdinh.Parent);
                }
            }

            Refresh();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textEdit6.Text = openFileDialog1.FileName;
            }
        }

        private void xtraScrollableControl1_DockChanged(object sender, EventArgs e)
        {
            panelHeader1.alignCenter(panelHeader1.Parent);
        }


        public void RefreshUI()
        {
            panelHeader1.alignCenter(panelHeader1.Parent);
        }

        public void CallBack_UpdateField(TD_Thamdinh_Duyet_Truongdulieu field, bool update)
        {
            if (update)
            {
                TD_Thamdinh_Duyet_Truongdulieu f = tempFields.FirstOrDefault(o => o.DM017201 == field.DM017201);
                f.Children = field.Children;
            }

            this.Enabled = true;
            this.Focus();
        }

        public void FillData(ref TD_Thamdinh_Duyet objData)
        {
            objData.DM017111 = textEdit2.Text;
            objData.DM017112 = dateEdit3.DateTime;
            objData.DM017113 = Guid.Parse(lookUpEdit11.EditValue.ToString());
            objData.DM017114 = textEdit4.Text;
            objData.DM017119 = textEdit7.Text;
            objData.DM017118 = textEdit6.Text;

            MemoEdit mmeText = (MemoEdit)xtraScrollableControl3.Controls.Find("mmeText_Thamdinh", true).FirstOrDefault();
            if (mmeText == null)//Nhập trường dữ liệu
            {
                objData.Fields = new List<TD_Thamdinh_Duyet_Truongdulieu>();
                foreach (TD_Thamdinh_Duyet_Truongdulieu f in tempFields)
                {
                    f.DM017202 = objData.DM017101;
                    string ctrId = "ctrValue_" + f.DM017201.ToString() + "_Thamdinh";
                    Control ctrValue = xtraScrollableControl1.Controls.Find(ctrId, true).FirstOrDefault();
                    if (ctrValue == null) continue;

                    switch (f.Kieutruong)
                    {
                        case 1:
                            f.DM017204 = ((TextEdit)ctrValue).Text.Trim();
                            break;
                        case 2:
                            f.DM017204 = ((TextEdit)ctrValue).Text.Trim();
                            break;
                        case 3:
                            f.DM017204 = ((CheckEdit)ctrValue).Checked ? "1" : "0";
                            break;
                        case 4:
                            f.DM017204 = ((DateEdit)ctrValue).DateTime.ToString();
                            break;
                        case 5:
                            TimeEdit ctrTime = (TimeEdit)xtraScrollableControl1.Controls.Find("ctr_" + f.DM017201.ToString() + "_Time_Thamdinh", true).FirstOrDefault();
                            DateEdit ctrDate = (DateEdit)xtraScrollableControl1.Controls.Find("ctr_" + f.DM017201.ToString() + "_Date_Thamdinh", true).FirstOrDefault();
                            DateTime moment = new DateTime(ctrDate.DateTime.Year, ctrDate.DateTime.Month, ctrDate.DateTime.Day, ctrTime.Time.Hour, ctrTime.Time.Minute, ctrTime.Time.Second);
                            f.DM017204 = moment.ToString();
                            break;
                        case 6:
                            f.DM017204 = ((TimeEdit)ctrValue).Time.ToString();
                            break;
                        case 7:
                            f.DM017204 = ((MemoEdit)ctrValue).Text.Trim();
                            break;
                        case 8:
                            LookUpEdit lue = ((LookUpEdit)ctrValue);
                            f.DM017204 = lue.EditValue == null ? string.Empty : lue.EditValue.ToString();
                            break;
                        case 10:
                            ctrValue = xtraScrollableControl1.Controls.Find("ctr_" + f.DM017201.ToString() + "_Path_Thamdinh", true).FirstOrDefault();
                            string filePath = ((TextEdit)ctrValue).Text.Trim();
                            if (File.Exists(filePath))
                            {
                                string uri = Helpers.CreateRequestUrl_UploadFile();
                                uri += "&n=td_thamdinh_duyet_tepdinhkem&fn=" + DateTime.Now.Ticks.ToString();
                                string response = WebUtils.Request_UploadFile(uri, filePath);
                                APIResponseData result = JsonConvert.DeserializeObject<APIResponseData>(response);
                                if (result.ErrorCode != 0)
                                {
                                    All.Show_message("Error code " + result.ErrorCode + ": Lỗi không thể upload tệp!");
                                    return;
                                }
                                f.DM017204 = result.Data.ToString();
                            }
                            break;
                        default: break;
                    }

                    objData.Fields.Add(f);
                }
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            if (!isEditing)
                simpleButton3.Text = "Lưu";
            else
                simpleButton3.Text = "Sửa";

            groupControl1.Enabled = !isEditing;
            isEditing = !isEditing;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            simpleButton3.Enabled = false;
            groupControl3.Visible = true;
        }
    }
}
