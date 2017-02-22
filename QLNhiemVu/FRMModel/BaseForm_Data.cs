using DBAccess;
using DevExpress.XtraEditors;
using Newtonsoft.Json;
using QLNhiemVu.DanhMuc;
using QLNhiemvu_DBEntities;
using QLNhiemVu_Defines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLNhiemVu.FRMModel
{
    public class BaseForm_Data : Form
    {
        protected override void OnLoad(EventArgs e)
        {
            //if (!Helpers.DBUtilities.CheckConnection())
            //{
            //    All.Show_message("Hiện không kết nối được hệ thống Cơ sở dữ liệu!");
            //    this.Dispose();
            //    return;
            //}

            base.OnLoad(e);
        }

        protected override void OnShown(EventArgs e)
        {
            if (!Helpers.DBUtilities.CheckConnection())
            {
                All.Show_message("Hiện không kết nối được hệ thống Cơ sở dữ liệu!");
                this.Dispose();
                return;
            }

            base.OnShown(e);
        }

        public panelHeader GenerateDynamicFields_Thamdinh(List<TD_Thamdinh_Duyet_Truongdulieu> fields, string currentState = "NORMAL")
        {
            try
            {
                if (fields == null || fields.Count == 0) return null;

                panelHeader panelHeaderFields = new panelHeader()
                {
                    Name = "panelHeaderFields_Thamdinh",
                    Width = 600,
                };
                TableLayoutPanel tableFields = new TableLayoutPanel()
                {
                    Name = "tableFields_Thamdinh",
                    Dock = DockStyle.Fill,
                    ColumnCount = 2,
                    GrowStyle = TableLayoutPanelGrowStyle.AddRows,
                    RowCount = 0,
                    CellBorderStyle = TableLayoutPanelCellBorderStyle.None
                };

                tableFields.ColumnStyles.Add(new ColumnStyle() { SizeType = SizeType.Absolute, Width = 250 });
                tableFields.ColumnStyles.Add(new ColumnStyle() { SizeType = SizeType.Absolute, Width = 350 });
                System.Drawing.Size controlValueSize_Full = new System.Drawing.Size(344, 26);
                RowStyle rowStyle = new RowStyle(SizeType.Absolute, 32);

                foreach (TD_Thamdinh_Duyet_Truongdulieu field in fields)
                {
                    tableFields.RowCount++;
                    tableFields.RowStyles.Add(new RowStyle(rowStyle.SizeType, rowStyle.Height));

                    Label lblTitle = new Label()
                    {
                        Text = field.Tenhienthi,
                        Font = All.Font_control,
                        Dock = DockStyle.Fill,
                        Padding = new Padding(0, 7, 0, 0)
                    };
                    tableFields.Controls.Add(lblTitle, 0, tableFields.RowCount - 1);

                    Control ctrValue = null;
                    switch (field.Kieutruong)
                    {
                        case 1:
                            ctrValue = new TextEdit() { Font = All.Font_control, Size = controlValueSize_Full };
                            ((TextEdit)ctrValue).ReadOnly = currentState == "NORMAL";
                            if (!string.IsNullOrEmpty(field.DM017204)) ((TextEdit)ctrValue).Text = field.DM017204;
                            break;
                        case 2:
                            ctrValue = new TextEdit() { Font = All.Font_control, Size = controlValueSize_Full };
                            ((TextEdit)ctrValue).ReadOnly = currentState == "NORMAL";
                            if (!string.IsNullOrEmpty(field.DM017204)) ((TextEdit)ctrValue).Text = field.DM017204;
                            break;
                        case 3:
                            ctrValue = new CheckEdit() { Font = All.Font_control, Text = string.Empty, Margin = new Padding(2, 7, 0, 0) };
                            ((CheckEdit)ctrValue).ReadOnly = currentState == "NORMAL";
                            if (!string.IsNullOrEmpty(field.DM017204)) ((CheckEdit)ctrValue).Checked = field.DM017204 == "1";
                            break;
                        case 4:
                            ctrValue = new DateEdit() { Font = All.Font_control, Size = new System.Drawing.Size(100, controlValueSize_Full.Height) };
                            ((DateEdit)ctrValue).Properties.Mask.EditMask = "dd/MM/yyyy";
                            ((DateEdit)ctrValue).Properties.Mask.UseMaskAsDisplayFormat = true;
                            ((DateEdit)ctrValue).ReadOnly = currentState == "NORMAL";
                            if (!string.IsNullOrEmpty(field.DM017204)) ((DateEdit)ctrValue).DateTime = DateTime.Parse(field.DM017204);
                            break;
                        case 5:
                            ctrValue = new TableLayoutPanel()
                            {
                                Dock = DockStyle.Fill,
                                ColumnCount = 2,
                                RowCount = 1,
                                CellBorderStyle = TableLayoutPanelCellBorderStyle.None,
                                Padding = new Padding(0),
                                Margin = new Padding(0)
                            };
                            ((TableLayoutPanel)ctrValue).ColumnStyles.Add(new ColumnStyle() { SizeType = SizeType.Absolute, Width = 103 });
                            ((TableLayoutPanel)ctrValue).ColumnStyles.Add(new ColumnStyle() { SizeType = SizeType.Absolute, Width = 247 });

                            DateTime datetimeValue = !string.IsNullOrEmpty(field.DM017204) ? DateTime.Parse(field.DM017204) : DateTime.MinValue;

                            TimeEdit ctrValue_Time = new TimeEdit() { Name = "ctr_" + field.DM017201.ToString() + "_Time_Thamdinh", Font = All.Font_control, Size = new System.Drawing.Size(100, controlValueSize_Full.Height), Margin = new Padding(3, 0, 0, 0) };
                            ctrValue_Time.Properties.Mask.EditMask = "HH:mm:ss";
                            ctrValue_Time.Properties.Mask.UseMaskAsDisplayFormat = true;
                            ctrValue_Time.ReadOnly = currentState == "NORMAL";
                            if (datetimeValue != DateTime.MinValue)
                                ctrValue_Time.Time = datetimeValue;
                            ((TableLayoutPanel)ctrValue).Controls.Add(ctrValue_Time, 0, 0);

                            DateEdit ctrValue_Date = new DateEdit() { Name = "ctr_" + field.DM017201.ToString() + "_Date_Thamdinh", Font = All.Font_control, Size = new System.Drawing.Size(100, controlValueSize_Full.Height), Margin = new Padding(3, 0, 0, 0) };
                            ctrValue_Date.Properties.Mask.EditMask = "dd/MM/yyyy";
                            ctrValue_Date.Properties.Mask.UseMaskAsDisplayFormat = true;
                            ctrValue_Date.ReadOnly = currentState == "NORMAL";
                            if (datetimeValue != DateTime.MinValue)
                                ctrValue_Date.DateTime = datetimeValue;
                            ((TableLayoutPanel)ctrValue).Controls.Add(ctrValue_Date, 1, 0);
                            break;
                        case 6:
                            ctrValue = new TimeEdit() { Font = All.Font_control, Size = new System.Drawing.Size(100, controlValueSize_Full.Height) };
                            ((TimeEdit)ctrValue).Properties.Mask.EditMask = "HH:mm:ss";
                            ((TimeEdit)ctrValue).Properties.Mask.UseMaskAsDisplayFormat = true;
                            ((TimeEdit)ctrValue).ReadOnly = currentState == "NORMAL";
                            if (!string.IsNullOrEmpty(field.DM017204)) ((TimeEdit)ctrValue).Time = DateTime.Parse(field.DM017204);
                            break;
                        case 7:
                            ctrValue = new MemoEdit() { Font = All.Font_control, Size = new System.Drawing.Size(controlValueSize_Full.Width, controlValueSize_Full.Height * 3) };
                            tableFields.RowStyles[tableFields.RowCount - 1].Height = ctrValue.Size.Height + 6;
                            ((MemoEdit)ctrValue).ReadOnly = currentState == "NORMAL";
                            if (!string.IsNullOrEmpty(field.DM017204)) ((MemoEdit)ctrValue).Text = field.DM017204;
                            break;
                        case 8:
                            ctrValue = new LookUpEdit() { Font = All.Font_control, Size = controlValueSize_Full };
                            LookUpEdit lueValue = (LookUpEdit)ctrValue;
                            lueValue.Properties.Appearance.Font = All.Font_control;
                            lueValue.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(32, 31, 53);
                            lueValue.Properties.Appearance.Options.UseFont = true;
                            lueValue.Properties.Appearance.Options.UseForeColor = true;

                            lueValue.Properties.AppearanceDropDown.Font = All.Font_control;
                            lueValue.Properties.AppearanceDropDown.Options.UseFont = true;

                            lueValue.Properties.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Combo;

                            lueValue.Properties.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoComplete;
                            lueValue.Properties.ShowHeader = false;
                            lueValue.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;

                            lueValue.Properties.NullText = string.Empty;

                            DM_LoaiThutucNhiemvu_Truongdulieu_LookupData dieukiendulieu = JsonConvert.DeserializeObject<DM_LoaiThutucNhiemvu_Truongdulieu_LookupData>(field.DieukienDulieu);
                            lueValue.Properties.DataSource = field.LookupData;
                            lueValue.Properties.ValueMember = dieukiendulieu.ColumnSave;
                            lueValue.Properties.DisplayMember = dieukiendulieu.ColumnDisplayName;
                            lueValue.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo()
                            {
                                FieldName = dieukiendulieu.ColumnDisplayID
                            });
                            lueValue.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo()
                            {
                                FieldName = dieukiendulieu.ColumnDisplayName
                            });
                            if (!string.IsNullOrEmpty(dieukiendulieu.ColumnDisplayExtend1))
                                lueValue.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo()
                                {
                                    FieldName = dieukiendulieu.ColumnDisplayExtend1
                                });
                            if (!string.IsNullOrEmpty(dieukiendulieu.ColumnDisplayExtend2))
                                lueValue.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo()
                                {
                                    FieldName = dieukiendulieu.ColumnDisplayExtend2
                                });

                            lueValue.ReadOnly = currentState == "NORMAL";
                            if (!string.IsNullOrEmpty(field.DM017204)) lueValue.EditValue = field.DM017204;
                            break;
                        case 9:
                            ctrValue = new SimpleButton() { Font = All.Font_control, Text = "Nhập dữ liệu", Size = new System.Drawing.Size(100, controlValueSize_Full.Height) };
                            ctrValue.Click += delegate(object sender, EventArgs e)
                            { btnNhapdulieu_Thamdinh_Click(sender, e, field, currentState); };
                            break;
                        case 10:
                            ctrValue = new TableLayoutPanel()
                            {
                                Dock = DockStyle.Fill,
                                ColumnCount = 2,
                                RowCount = 1,
                                CellBorderStyle = TableLayoutPanelCellBorderStyle.None,
                                Padding = new Padding(0),
                                Margin = new Padding(0)
                            };
                            ((TableLayoutPanel)ctrValue).ColumnStyles.Add(new ColumnStyle() { SizeType = SizeType.Absolute, Width = 300 });
                            ((TableLayoutPanel)ctrValue).ColumnStyles.Add(new ColumnStyle() { SizeType = SizeType.Absolute, Width = 50 });

                            TextEdit ctrValue_Path = new TextEdit() { Name = "ctr_" + field.DM017201.ToString() + "_Path_Thamdinh", ReadOnly = true, Font = All.Font_control, Size = new System.Drawing.Size(294, 26), Margin = new Padding(3, 0, 0, 0) };
                            if (!string.IsNullOrEmpty(field.DM017204)) ((TextEdit)ctrValue_Path).Text = field.DM017204;
                            ((TextEdit)ctrValue_Path).ReadOnly = currentState == "NORMAL";
                            ((TableLayoutPanel)ctrValue).Controls.Add(ctrValue_Path, 0, 0);

                            SimpleButton ctrValue_Button = new SimpleButton() { Name = "ctr_" + field.DM017201.ToString() + "_Button_Thamdinh", Text = "...", Font = All.Font_control, Size = new System.Drawing.Size(44, 26), Margin = new Padding(3, 0, 0, 0) };
                            ctrValue_Button.Click += btnChooseFile_Click;
                            ((SimpleButton)ctrValue_Button).Enabled = currentState != "NORMAL";
                            ((TableLayoutPanel)ctrValue).Controls.Add(ctrValue_Button, 1, 0);
                            break;
                        default: break;
                    }

                    if (ctrValue != null)
                    {
                        ctrValue.Name = "ctrValue_" + field.DM017201.ToString() + "_Thamdinh";
                        tableFields.Controls.Add(ctrValue, 1, tableFields.RowCount - 1);
                    }
                }

                panelHeaderFields.Controls.Add(tableFields);
                panelHeaderFields.Height = (int)tableFields.RowStyles.Cast<RowStyle>().Sum(o => o.Height);

                return panelHeaderFields;
            }
            catch (Exception ex)
            {
                Log.write(ex);
                return null;
            }
        }

        public panelHeader GenerateDynamicFields_PheduyetThamdinh(List<TD_Pheduyet_Thamdinh_Duyet_Truongdulieu> fields, string currentState = "NORMAL")
        {
            try
            {
                if (fields == null || fields.Count == 0) return null;

                panelHeader panelHeaderFields = new panelHeader()
                {
                    Name = "panelHeaderFields_Pheduyet",
                    Width = 600,
                };
                TableLayoutPanel tableFields = new TableLayoutPanel()
                {
                    Name = "tableFields_Pheduyet",
                    Dock = DockStyle.Fill,
                    ColumnCount = 2,
                    GrowStyle = TableLayoutPanelGrowStyle.AddRows,
                    RowCount = 0,
                    CellBorderStyle = TableLayoutPanelCellBorderStyle.None
                };

                tableFields.ColumnStyles.Add(new ColumnStyle() { SizeType = SizeType.Absolute, Width = 250 });
                tableFields.ColumnStyles.Add(new ColumnStyle() { SizeType = SizeType.Absolute, Width = 350 });
                System.Drawing.Size controlValueSize_Full = new System.Drawing.Size(344, 26);
                RowStyle rowStyle = new RowStyle(SizeType.Absolute, 32);

                foreach (TD_Pheduyet_Thamdinh_Duyet_Truongdulieu field in fields)
                {
                    tableFields.RowCount++;
                    tableFields.RowStyles.Add(new RowStyle(rowStyle.SizeType, rowStyle.Height));

                    Label lblTitle = new Label()
                    {
                        Text = field.Tenhienthi,
                        Font = All.Font_control,
                        Dock = DockStyle.Fill,
                        Padding = new Padding(0, 7, 0, 0)
                    };
                    tableFields.Controls.Add(lblTitle, 0, tableFields.RowCount - 1);

                    Control ctrValue = null;
                    switch (field.Kieutruong)
                    {
                        case 1:
                            ctrValue = new TextEdit() { Font = All.Font_control, Size = controlValueSize_Full };
                            ((TextEdit)ctrValue).ReadOnly = currentState == "NORMAL";
                            if (!string.IsNullOrEmpty(field.DM017604)) ((TextEdit)ctrValue).Text = field.DM017604;
                            break;
                        case 2:
                            ctrValue = new TextEdit() { Font = All.Font_control, Size = controlValueSize_Full };
                            ((TextEdit)ctrValue).ReadOnly = currentState == "NORMAL";
                            if (!string.IsNullOrEmpty(field.DM017604)) ((TextEdit)ctrValue).Text = field.DM017604;
                            break;
                        case 3:
                            ctrValue = new CheckEdit() { Font = All.Font_control, Text = string.Empty, Margin = new Padding(2, 7, 0, 0) };
                            ((CheckEdit)ctrValue).ReadOnly = currentState == "NORMAL";
                            if (!string.IsNullOrEmpty(field.DM017604)) ((CheckEdit)ctrValue).Checked = field.DM017604 == "1";
                            break;
                        case 4:
                            ctrValue = new DateEdit() { Font = All.Font_control, Size = new System.Drawing.Size(100, controlValueSize_Full.Height) };
                            ((DateEdit)ctrValue).Properties.Mask.EditMask = "dd/MM/yyyy";
                            ((DateEdit)ctrValue).Properties.Mask.UseMaskAsDisplayFormat = true;
                            ((DateEdit)ctrValue).ReadOnly = currentState == "NORMAL";
                            if (!string.IsNullOrEmpty(field.DM017604)) ((DateEdit)ctrValue).DateTime = DateTime.Parse(field.DM017604);
                            break;
                        case 5:
                            ctrValue = new TableLayoutPanel()
                            {
                                Dock = DockStyle.Fill,
                                ColumnCount = 2,
                                RowCount = 1,
                                CellBorderStyle = TableLayoutPanelCellBorderStyle.None,
                                Padding = new Padding(0),
                                Margin = new Padding(0)
                            };
                            ((TableLayoutPanel)ctrValue).ColumnStyles.Add(new ColumnStyle() { SizeType = SizeType.Absolute, Width = 103 });
                            ((TableLayoutPanel)ctrValue).ColumnStyles.Add(new ColumnStyle() { SizeType = SizeType.Absolute, Width = 247 });

                            DateTime datetimeValue = !string.IsNullOrEmpty(field.DM017604) ? DateTime.Parse(field.DM017604) : DateTime.MinValue;

                            TimeEdit ctrValue_Time = new TimeEdit() { Name = "ctr_" + field.DM017601.ToString() + "_Time_Pheduyet", Font = All.Font_control, Size = new System.Drawing.Size(100, controlValueSize_Full.Height), Margin = new Padding(3, 0, 0, 0) };
                            ctrValue_Time.Properties.Mask.EditMask = "HH:mm:ss";
                            ctrValue_Time.Properties.Mask.UseMaskAsDisplayFormat = true;
                            ctrValue_Time.ReadOnly = currentState == "NORMAL";
                            if (datetimeValue != DateTime.MinValue)
                                ctrValue_Time.Time = datetimeValue;
                            ((TableLayoutPanel)ctrValue).Controls.Add(ctrValue_Time, 0, 0);

                            DateEdit ctrValue_Date = new DateEdit() { Name = "ctr_" + field.DM017601.ToString() + "_Date_Pheduyet", Font = All.Font_control, Size = new System.Drawing.Size(100, controlValueSize_Full.Height), Margin = new Padding(3, 0, 0, 0) };
                            ctrValue_Date.Properties.Mask.EditMask = "dd/MM/yyyy";
                            ctrValue_Date.Properties.Mask.UseMaskAsDisplayFormat = true;
                            ctrValue_Date.ReadOnly = currentState == "NORMAL";
                            if (datetimeValue != DateTime.MinValue)
                                ctrValue_Date.DateTime = datetimeValue;
                            ((TableLayoutPanel)ctrValue).Controls.Add(ctrValue_Date, 1, 0);
                            break;
                        case 6:
                            ctrValue = new TimeEdit() { Font = All.Font_control, Size = new System.Drawing.Size(100, controlValueSize_Full.Height) };
                            ((TimeEdit)ctrValue).Properties.Mask.EditMask = "HH:mm:ss";
                            ((TimeEdit)ctrValue).Properties.Mask.UseMaskAsDisplayFormat = true;
                            ((TimeEdit)ctrValue).ReadOnly = currentState == "NORMAL";
                            if (!string.IsNullOrEmpty(field.DM017604)) ((TimeEdit)ctrValue).Time = DateTime.Parse(field.DM017604);
                            break;
                        case 7:
                            ctrValue = new MemoEdit() { Font = All.Font_control, Size = new System.Drawing.Size(controlValueSize_Full.Width, controlValueSize_Full.Height * 3) };
                            tableFields.RowStyles[tableFields.RowCount - 1].Height = ctrValue.Size.Height + 6;
                            ((MemoEdit)ctrValue).ReadOnly = currentState == "NORMAL";
                            if (!string.IsNullOrEmpty(field.DM017604)) ((MemoEdit)ctrValue).Text = field.DM017604;
                            break;
                        case 8:
                            ctrValue = new LookUpEdit() { Font = All.Font_control, Size = controlValueSize_Full };
                            LookUpEdit lueValue = (LookUpEdit)ctrValue;
                            lueValue.Properties.Appearance.Font = All.Font_control;
                            lueValue.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(32, 31, 53);
                            lueValue.Properties.Appearance.Options.UseFont = true;
                            lueValue.Properties.Appearance.Options.UseForeColor = true;

                            lueValue.Properties.AppearanceDropDown.Font = All.Font_control;
                            lueValue.Properties.AppearanceDropDown.Options.UseFont = true;

                            lueValue.Properties.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Combo;

                            lueValue.Properties.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoComplete;
                            lueValue.Properties.ShowHeader = false;
                            lueValue.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;

                            lueValue.Properties.NullText = string.Empty;

                            DM_LoaiThutucNhiemvu_Truongdulieu_LookupData dieukiendulieu = JsonConvert.DeserializeObject<DM_LoaiThutucNhiemvu_Truongdulieu_LookupData>(field.DieukienDulieu);
                            lueValue.Properties.DataSource = field.LookupData;
                            lueValue.Properties.ValueMember = dieukiendulieu.ColumnSave;
                            lueValue.Properties.DisplayMember = dieukiendulieu.ColumnDisplayName;
                            lueValue.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo()
                            {
                                FieldName = dieukiendulieu.ColumnDisplayID
                            });
                            lueValue.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo()
                            {
                                FieldName = dieukiendulieu.ColumnDisplayName
                            });
                            if (!string.IsNullOrEmpty(dieukiendulieu.ColumnDisplayExtend1))
                                lueValue.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo()
                                {
                                    FieldName = dieukiendulieu.ColumnDisplayExtend1
                                });
                            if (!string.IsNullOrEmpty(dieukiendulieu.ColumnDisplayExtend2))
                                lueValue.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo()
                                {
                                    FieldName = dieukiendulieu.ColumnDisplayExtend2
                                });

                            lueValue.ReadOnly = currentState == "NORMAL";
                            if (!string.IsNullOrEmpty(field.DM017604)) lueValue.EditValue = field.DM017604;
                            break;
                        case 9:
                            ctrValue = new SimpleButton() { Font = All.Font_control, Text = "Nhập dữ liệu", Size = new System.Drawing.Size(100, controlValueSize_Full.Height) };
                            ctrValue.Click += delegate(object sender, EventArgs e)
                            { btnNhapdulieu_PheduyetThamdinh_Click(sender, e, field, currentState); };
                            break;
                        case 10:
                            ctrValue = new TableLayoutPanel()
                            {
                                Dock = DockStyle.Fill,
                                ColumnCount = 2,
                                RowCount = 1,
                                CellBorderStyle = TableLayoutPanelCellBorderStyle.None,
                                Padding = new Padding(0),
                                Margin = new Padding(0)
                            };
                            ((TableLayoutPanel)ctrValue).ColumnStyles.Add(new ColumnStyle() { SizeType = SizeType.Absolute, Width = 300 });
                            ((TableLayoutPanel)ctrValue).ColumnStyles.Add(new ColumnStyle() { SizeType = SizeType.Absolute, Width = 50 });

                            TextEdit ctrValue_Path = new TextEdit() { Name = "ctr_" + field.DM017601.ToString() + "_Path_Pheduyet", ReadOnly = true, Font = All.Font_control, Size = new System.Drawing.Size(294, 26), Margin = new Padding(3, 0, 0, 0) };
                            if (!string.IsNullOrEmpty(field.DM017604)) ((TextEdit)ctrValue_Path).Text = field.DM017604;
                            ((TextEdit)ctrValue_Path).ReadOnly = currentState == "NORMAL";
                            ((TableLayoutPanel)ctrValue).Controls.Add(ctrValue_Path, 0, 0);

                            SimpleButton ctrValue_Button = new SimpleButton() { Name = "ctr_" + field.DM017601.ToString() + "_Button_Pheduyet", Text = "...", Font = All.Font_control, Size = new System.Drawing.Size(44, 26), Margin = new Padding(3, 0, 0, 0) };
                            ctrValue_Button.Click += btnChooseFile_Click;
                            ((SimpleButton)ctrValue_Button).Enabled = currentState != "NORMAL";
                            ((TableLayoutPanel)ctrValue).Controls.Add(ctrValue_Button, 1, 0);
                            break;
                        default: break;
                    }

                    if (ctrValue != null)
                    {
                        ctrValue.Name = "ctrValue_" + field.DM017601.ToString() + "_Pheduyet";
                        tableFields.Controls.Add(ctrValue, 1, tableFields.RowCount - 1);
                    }
                }

                panelHeaderFields.Controls.Add(tableFields);
                panelHeaderFields.Height = (int)tableFields.RowStyles.Cast<RowStyle>().Sum(o => o.Height);

                return panelHeaderFields;
            }
            catch (Exception ex)
            {
                Log.write(ex);
                return null;
            }
        }

        public panelHeader GenerateDynamicFields(List<TD_ThuchienNhiemvu_Truongdulieu> fields, string currentState = "NORMAL")
        {
            try
            {
                if (fields == null || fields.Count == 0) return null;

                panelHeader panelHeaderFields = new panelHeader()
                {
                    Name = "panelHeaderFields",
                    Width = 600,
                };
                TableLayoutPanel tableFields = new TableLayoutPanel()
                {
                    Name = "tableFields",
                    Dock = DockStyle.Fill,
                    ColumnCount = 2,
                    GrowStyle = TableLayoutPanelGrowStyle.AddRows,
                    RowCount = 0,
                    CellBorderStyle = TableLayoutPanelCellBorderStyle.None
                };

                tableFields.ColumnStyles.Add(new ColumnStyle() { SizeType = SizeType.Absolute, Width = 250 });
                tableFields.ColumnStyles.Add(new ColumnStyle() { SizeType = SizeType.Absolute, Width = 350 });
                System.Drawing.Size controlValueSize_Full = new System.Drawing.Size(344, 26);
                RowStyle rowStyle = new RowStyle(SizeType.Absolute, 32);

                foreach (TD_ThuchienNhiemvu_Truongdulieu field in fields)
                {
                    tableFields.RowCount++;
                    tableFields.RowStyles.Add(new RowStyle(rowStyle.SizeType, rowStyle.Height));

                    Label lblTitle = new Label()
                    {
                        Text = field.Tenhienthi,
                        Font = All.Font_control,
                        Dock = DockStyle.Fill,
                        Padding = new Padding(0, 7, 0, 0)
                    };
                    tableFields.Controls.Add(lblTitle, 0, tableFields.RowCount - 1);

                    Control ctrValue = null;
                    switch (field.Kieutruong)
                    {
                        case 1:
                            ctrValue = new TextEdit() { Font = All.Font_control, Size = controlValueSize_Full };
                            ((TextEdit)ctrValue).ReadOnly = currentState == "NORMAL";
                            if (!string.IsNullOrEmpty(field.DM016804)) ((TextEdit)ctrValue).Text = field.DM016804;
                            break;
                        case 2:
                            ctrValue = new TextEdit() { Font = All.Font_control, Size = controlValueSize_Full };
                            ((TextEdit)ctrValue).ReadOnly = currentState == "NORMAL";
                            if (!string.IsNullOrEmpty(field.DM016804)) ((TextEdit)ctrValue).Text = field.DM016804;
                            break;
                        case 3:
                            ctrValue = new CheckEdit() { Font = All.Font_control, Text = string.Empty, Margin = new Padding(2, 7, 0, 0) };
                            ((CheckEdit)ctrValue).ReadOnly = currentState == "NORMAL";
                            if (!string.IsNullOrEmpty(field.DM016804)) ((CheckEdit)ctrValue).Checked = field.DM016804 == "1";
                            break;
                        case 4:
                            ctrValue = new DateEdit() { Font = All.Font_control, Size = new System.Drawing.Size(100, controlValueSize_Full.Height) };
                            ((DateEdit)ctrValue).Properties.Mask.EditMask = "dd/MM/yyyy";
                            ((DateEdit)ctrValue).Properties.Mask.UseMaskAsDisplayFormat = true;
                            ((DateEdit)ctrValue).ReadOnly = currentState == "NORMAL";
                            if (!string.IsNullOrEmpty(field.DM016804)) ((DateEdit)ctrValue).DateTime = DateTime.Parse(field.DM016804);
                            break;
                        case 5:
                            ctrValue = new TableLayoutPanel()
                            {
                                Dock = DockStyle.Fill,
                                ColumnCount = 2,
                                RowCount = 1,
                                CellBorderStyle = TableLayoutPanelCellBorderStyle.None,
                                Padding = new Padding(0),
                                Margin = new Padding(0)
                            };
                            ((TableLayoutPanel)ctrValue).ColumnStyles.Add(new ColumnStyle() { SizeType = SizeType.Absolute, Width = 103 });
                            ((TableLayoutPanel)ctrValue).ColumnStyles.Add(new ColumnStyle() { SizeType = SizeType.Absolute, Width = 247 });

                            DateTime datetimeValue = !string.IsNullOrEmpty(field.DM016804) ? DateTime.Parse(field.DM016804) : DateTime.MinValue;

                            TimeEdit ctrValue_Time = new TimeEdit() { Name = "ctr_" + field.DM016801.ToString() + "_Time", Font = All.Font_control, Size = new System.Drawing.Size(100, controlValueSize_Full.Height), Margin = new Padding(3, 0, 0, 0) };
                            ctrValue_Time.Properties.Mask.EditMask = "HH:mm:ss";
                            ctrValue_Time.Properties.Mask.UseMaskAsDisplayFormat = true;
                            ctrValue_Time.ReadOnly = currentState == "NORMAL";
                            if (datetimeValue != DateTime.MinValue)
                                ctrValue_Time.Time = datetimeValue;
                            ((TableLayoutPanel)ctrValue).Controls.Add(ctrValue_Time, 0, 0);

                            DateEdit ctrValue_Date = new DateEdit() { Name = "ctr_" + field.DM016801.ToString() + "_Date", Font = All.Font_control, Size = new System.Drawing.Size(100, controlValueSize_Full.Height), Margin = new Padding(3, 0, 0, 0) };
                            ctrValue_Date.Properties.Mask.EditMask = "dd/MM/yyyy";
                            ctrValue_Date.Properties.Mask.UseMaskAsDisplayFormat = true;
                            ctrValue_Date.ReadOnly = currentState == "NORMAL";
                            if (datetimeValue != DateTime.MinValue)
                                ctrValue_Date.DateTime = datetimeValue;
                            ((TableLayoutPanel)ctrValue).Controls.Add(ctrValue_Date, 1, 0);
                            break;
                        case 6:
                            ctrValue = new TimeEdit() { Font = All.Font_control, Size = new System.Drawing.Size(100, controlValueSize_Full.Height) };
                            ((TimeEdit)ctrValue).Properties.Mask.EditMask = "HH:mm:ss";
                            ((TimeEdit)ctrValue).Properties.Mask.UseMaskAsDisplayFormat = true;
                            ((TimeEdit)ctrValue).ReadOnly = currentState == "NORMAL";
                            if (!string.IsNullOrEmpty(field.DM016804)) ((TimeEdit)ctrValue).Time = DateTime.Parse(field.DM016804);
                            break;
                        case 7:
                            ctrValue = new MemoEdit() { Font = All.Font_control, Size = new System.Drawing.Size(controlValueSize_Full.Width, controlValueSize_Full.Height * 3) };
                            tableFields.RowStyles[tableFields.RowCount - 1].Height = ctrValue.Size.Height + 6;
                            ((MemoEdit)ctrValue).ReadOnly = currentState == "NORMAL";
                            if (!string.IsNullOrEmpty(field.DM016804)) ((MemoEdit)ctrValue).Text = field.DM016804;
                            break;
                        case 8:
                            ctrValue = new LookUpEdit() { Font = All.Font_control, Size = controlValueSize_Full };
                            LookUpEdit lueValue = (LookUpEdit)ctrValue;
                            lueValue.Properties.Appearance.Font = All.Font_control;
                            lueValue.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(32, 31, 53);
                            lueValue.Properties.Appearance.Options.UseFont = true;
                            lueValue.Properties.Appearance.Options.UseForeColor = true;

                            lueValue.Properties.AppearanceDropDown.Font = All.Font_control;
                            lueValue.Properties.AppearanceDropDown.Options.UseFont = true;

                            lueValue.Properties.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Combo;

                            lueValue.Properties.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoComplete;
                            lueValue.Properties.ShowHeader = false;
                            lueValue.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;

                            lueValue.Properties.NullText = string.Empty;

                            DM_LoaiThutucNhiemvu_Truongdulieu_LookupData dieukiendulieu = JsonConvert.DeserializeObject<DM_LoaiThutucNhiemvu_Truongdulieu_LookupData>(field.DieukienDulieu);
                            lueValue.Properties.DataSource = field.LookupData;
                            lueValue.Properties.ValueMember = dieukiendulieu.ColumnSave;
                            lueValue.Properties.DisplayMember = dieukiendulieu.ColumnDisplayName;
                            lueValue.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo()
                            {
                                FieldName = dieukiendulieu.ColumnDisplayID
                            });
                            lueValue.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo()
                            {
                                FieldName = dieukiendulieu.ColumnDisplayName
                            });
                            if (!string.IsNullOrEmpty(dieukiendulieu.ColumnDisplayExtend1))
                                lueValue.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo()
                                {
                                    FieldName = dieukiendulieu.ColumnDisplayExtend1
                                });
                            if (!string.IsNullOrEmpty(dieukiendulieu.ColumnDisplayExtend2))
                                lueValue.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo()
                                {
                                    FieldName = dieukiendulieu.ColumnDisplayExtend2
                                });

                            lueValue.ReadOnly = currentState == "NORMAL";
                            if (!string.IsNullOrEmpty(field.DM016804)) lueValue.EditValue = field.DM016804;
                            break;
                        case 9:
                            ctrValue = new SimpleButton() { Font = All.Font_control, Text = "Nhập dữ liệu", Size = new System.Drawing.Size(100, controlValueSize_Full.Height) };
                            ctrValue.Click += delegate(object sender, EventArgs e)
                            { btnNhapdulieu_Click(sender, e, field, currentState); };
                            break;
                        case 10:
                            ctrValue = new TableLayoutPanel()
                            {
                                Dock = DockStyle.Fill,
                                ColumnCount = 2,
                                RowCount = 1,
                                CellBorderStyle = TableLayoutPanelCellBorderStyle.None,
                                Padding = new Padding(0),
                                Margin = new Padding(0)
                            };
                            ((TableLayoutPanel)ctrValue).ColumnStyles.Add(new ColumnStyle() { SizeType = SizeType.Absolute, Width = 300 });
                            ((TableLayoutPanel)ctrValue).ColumnStyles.Add(new ColumnStyle() { SizeType = SizeType.Absolute, Width = 50 });

                            TextEdit ctrValue_Path = new TextEdit() { Name = "ctr_" + field.DM016801.ToString() + "_Path", ReadOnly = true, Font = All.Font_control, Size = new System.Drawing.Size(294, 26), Margin = new Padding(3, 0, 0, 0) };
                            if (!string.IsNullOrEmpty(field.DM016804)) ((TextEdit)ctrValue_Path).Text = field.DM016804;
                            ((TextEdit)ctrValue_Path).ReadOnly = currentState == "NORMAL";
                            ((TableLayoutPanel)ctrValue).Controls.Add(ctrValue_Path, 0, 0);

                            SimpleButton ctrValue_Button = new SimpleButton() { Name = "ctr_" + field.DM016801.ToString() + "_Button", Text = "...", Font = All.Font_control, Size = new System.Drawing.Size(44, 26), Margin = new Padding(3, 0, 0, 0) };
                            ctrValue_Button.Click += btnChooseFile_Click;
                            ((SimpleButton)ctrValue_Button).Enabled = currentState != "NORMAL";
                            ((TableLayoutPanel)ctrValue).Controls.Add(ctrValue_Button, 1, 0);
                            break;
                        default: break;
                    }

                    if (ctrValue != null)
                    {
                        ctrValue.Name = "ctrValue_" + field.DM016801.ToString();
                        tableFields.Controls.Add(ctrValue, 1, tableFields.RowCount - 1);
                    }
                }

                panelHeaderFields.Controls.Add(tableFields);
                panelHeaderFields.Height = (int)tableFields.RowStyles.Cast<RowStyle>().Sum(o => o.Height);

                return panelHeaderFields;
            }
            catch (Exception ex)
            {
                Log.write(ex);
                return null;
            }
        }

        public panelHeader GenerateDynamicFields_PheduyetVB(List<TD_Pheduyet_VB_Truongdulieu> fields, string currentState = "NORMAL")
        {
            try
            {
                if (fields == null || fields.Count == 0) return null;

                panelHeader panelHeaderFields = new panelHeader()
                {
                    Name = "panelHeaderFields",
                    Width = 600,
                };
                TableLayoutPanel tableFields = new TableLayoutPanel()
                {
                    Name = "tableFields",
                    Dock = DockStyle.Fill,
                    ColumnCount = 2,
                    GrowStyle = TableLayoutPanelGrowStyle.AddRows,
                    RowCount = 0,
                    CellBorderStyle = TableLayoutPanelCellBorderStyle.None
                };

                tableFields.ColumnStyles.Add(new ColumnStyle() { SizeType = SizeType.Absolute, Width = 250 });
                tableFields.ColumnStyles.Add(new ColumnStyle() { SizeType = SizeType.Absolute, Width = 350 });
                System.Drawing.Size controlValueSize_Full = new System.Drawing.Size(344, 26);
                RowStyle rowStyle = new RowStyle(SizeType.Absolute, 32);

                foreach (TD_Pheduyet_VB_Truongdulieu field in fields)
                {
                    tableFields.RowCount++;
                    tableFields.RowStyles.Add(new RowStyle(rowStyle.SizeType, rowStyle.Height));

                    Label lblTitle = new Label()
                    {
                        Text = field.Tenhienthi,
                        Font = All.Font_control,
                        Dock = DockStyle.Fill,
                        Padding = new Padding(0, 7, 0, 0)
                    };
                    tableFields.Controls.Add(lblTitle, 0, tableFields.RowCount - 1);

                    Control ctrValue = null;
                    switch (field.Kieutruong)
                    {
                        case 1:
                            ctrValue = new TextEdit() { Font = All.Font_control, Size = controlValueSize_Full };
                            ((TextEdit)ctrValue).ReadOnly = currentState == "NORMAL";
                            if (!string.IsNullOrEmpty(field.DM017404)) ((TextEdit)ctrValue).Text = field.DM017404;
                            break;
                        case 2:
                            ctrValue = new TextEdit() { Font = All.Font_control, Size = controlValueSize_Full };
                            ((TextEdit)ctrValue).ReadOnly = currentState == "NORMAL";
                            if (!string.IsNullOrEmpty(field.DM017404)) ((TextEdit)ctrValue).Text = field.DM017404;
                            break;
                        case 3:
                            ctrValue = new CheckEdit() { Font = All.Font_control, Text = string.Empty, Margin = new Padding(2, 7, 0, 0) };
                            ((CheckEdit)ctrValue).ReadOnly = currentState == "NORMAL";
                            if (!string.IsNullOrEmpty(field.DM017404)) ((CheckEdit)ctrValue).Checked = field.DM017404 == "1";
                            break;
                        case 4:
                            ctrValue = new DateEdit() { Font = All.Font_control, Size = new System.Drawing.Size(100, controlValueSize_Full.Height) };
                            ((DateEdit)ctrValue).Properties.Mask.EditMask = "dd/MM/yyyy";
                            ((DateEdit)ctrValue).Properties.Mask.UseMaskAsDisplayFormat = true;
                            ((DateEdit)ctrValue).ReadOnly = currentState == "NORMAL";
                            if (!string.IsNullOrEmpty(field.DM017404)) ((DateEdit)ctrValue).DateTime = DateTime.Parse(field.DM017404);
                            break;
                        case 5:
                            ctrValue = new TableLayoutPanel()
                            {
                                Dock = DockStyle.Fill,
                                ColumnCount = 2,
                                RowCount = 1,
                                CellBorderStyle = TableLayoutPanelCellBorderStyle.None,
                                Padding = new Padding(0),
                                Margin = new Padding(0)
                            };
                            ((TableLayoutPanel)ctrValue).ColumnStyles.Add(new ColumnStyle() { SizeType = SizeType.Absolute, Width = 103 });
                            ((TableLayoutPanel)ctrValue).ColumnStyles.Add(new ColumnStyle() { SizeType = SizeType.Absolute, Width = 247 });

                            DateTime datetimeValue = !string.IsNullOrEmpty(field.DM017404) ? DateTime.Parse(field.DM017404) : DateTime.MinValue;

                            TimeEdit ctrValue_Time = new TimeEdit() { Name = "ctr_" + field.DM017401.ToString() + "_Time", Font = All.Font_control, Size = new System.Drawing.Size(100, controlValueSize_Full.Height), Margin = new Padding(3, 0, 0, 0) };
                            ctrValue_Time.Properties.Mask.EditMask = "HH:mm:ss";
                            ctrValue_Time.Properties.Mask.UseMaskAsDisplayFormat = true;
                            ctrValue_Time.ReadOnly = currentState == "NORMAL";
                            if (datetimeValue != DateTime.MinValue)
                                ctrValue_Time.Time = datetimeValue;
                            ((TableLayoutPanel)ctrValue).Controls.Add(ctrValue_Time, 0, 0);

                            DateEdit ctrValue_Date = new DateEdit() { Name = "ctr_" + field.DM017401.ToString() + "_Date", Font = All.Font_control, Size = new System.Drawing.Size(100, controlValueSize_Full.Height), Margin = new Padding(3, 0, 0, 0) };
                            ctrValue_Date.Properties.Mask.EditMask = "dd/MM/yyyy";
                            ctrValue_Date.Properties.Mask.UseMaskAsDisplayFormat = true;
                            ctrValue_Date.ReadOnly = currentState == "NORMAL";
                            if (datetimeValue != DateTime.MinValue)
                                ctrValue_Date.DateTime = datetimeValue;
                            ((TableLayoutPanel)ctrValue).Controls.Add(ctrValue_Date, 1, 0);
                            break;
                        case 6:
                            ctrValue = new TimeEdit() { Font = All.Font_control, Size = new System.Drawing.Size(100, controlValueSize_Full.Height) };
                            ((TimeEdit)ctrValue).Properties.Mask.EditMask = "HH:mm:ss";
                            ((TimeEdit)ctrValue).Properties.Mask.UseMaskAsDisplayFormat = true;
                            ((TimeEdit)ctrValue).ReadOnly = currentState == "NORMAL";
                            if (!string.IsNullOrEmpty(field.DM017404)) ((TimeEdit)ctrValue).Time = DateTime.Parse(field.DM017404);
                            break;
                        case 7:
                            ctrValue = new MemoEdit() { Font = All.Font_control, Size = new System.Drawing.Size(controlValueSize_Full.Width, controlValueSize_Full.Height * 3) };
                            tableFields.RowStyles[tableFields.RowCount - 1].Height = ctrValue.Size.Height + 6;
                            ((MemoEdit)ctrValue).ReadOnly = currentState == "NORMAL";
                            if (!string.IsNullOrEmpty(field.DM017404)) ((MemoEdit)ctrValue).Text = field.DM017404;
                            break;
                        case 8:
                            ctrValue = new LookUpEdit() { Font = All.Font_control, Size = controlValueSize_Full };
                            LookUpEdit lueValue = (LookUpEdit)ctrValue;
                            lueValue.Properties.Appearance.Font = All.Font_control;
                            lueValue.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(32, 31, 53);
                            lueValue.Properties.Appearance.Options.UseFont = true;
                            lueValue.Properties.Appearance.Options.UseForeColor = true;

                            lueValue.Properties.AppearanceDropDown.Font = All.Font_control;
                            lueValue.Properties.AppearanceDropDown.Options.UseFont = true;

                            lueValue.Properties.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Combo;

                            lueValue.Properties.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoComplete;
                            lueValue.Properties.ShowHeader = false;
                            lueValue.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;

                            lueValue.Properties.NullText = string.Empty;

                            DM_LoaiThutucNhiemvu_Truongdulieu_LookupData dieukiendulieu = JsonConvert.DeserializeObject<DM_LoaiThutucNhiemvu_Truongdulieu_LookupData>(field.DieukienDulieu);
                            lueValue.Properties.DataSource = field.LookupData;
                            lueValue.Properties.ValueMember = dieukiendulieu.ColumnSave;
                            lueValue.Properties.DisplayMember = dieukiendulieu.ColumnDisplayName;
                            lueValue.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo()
                            {
                                FieldName = dieukiendulieu.ColumnDisplayID
                            });
                            lueValue.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo()
                            {
                                FieldName = dieukiendulieu.ColumnDisplayName
                            });
                            if (!string.IsNullOrEmpty(dieukiendulieu.ColumnDisplayExtend1))
                                lueValue.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo()
                                {
                                    FieldName = dieukiendulieu.ColumnDisplayExtend1
                                });
                            if (!string.IsNullOrEmpty(dieukiendulieu.ColumnDisplayExtend2))
                                lueValue.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo()
                                {
                                    FieldName = dieukiendulieu.ColumnDisplayExtend2
                                });

                            lueValue.ReadOnly = currentState == "NORMAL";
                            if (!string.IsNullOrEmpty(field.DM017404)) lueValue.EditValue = field.DM017404;
                            break;
                        case 9:
                            ctrValue = new SimpleButton() { Font = All.Font_control, Text = "Nhập dữ liệu", Size = new System.Drawing.Size(100, controlValueSize_Full.Height) };
                            ctrValue.Click += delegate(object sender, EventArgs e)
                            { btnNhapdulieu_PheduyetVB_Click(sender, e, field, currentState); };
                            break;
                        case 10:
                            ctrValue = new TableLayoutPanel()
                            {
                                Dock = DockStyle.Fill,
                                ColumnCount = 2,
                                RowCount = 1,
                                CellBorderStyle = TableLayoutPanelCellBorderStyle.None,
                                Padding = new Padding(0),
                                Margin = new Padding(0)
                            };
                            ((TableLayoutPanel)ctrValue).ColumnStyles.Add(new ColumnStyle() { SizeType = SizeType.Absolute, Width = 300 });
                            ((TableLayoutPanel)ctrValue).ColumnStyles.Add(new ColumnStyle() { SizeType = SizeType.Absolute, Width = 50 });

                            TextEdit ctrValue_Path = new TextEdit() { Name = "ctr_" + field.DM017401.ToString() + "_Path", ReadOnly = true, Font = All.Font_control, Size = new System.Drawing.Size(294, 26), Margin = new Padding(3, 0, 0, 0) };
                            if (!string.IsNullOrEmpty(field.DM017404)) ((TextEdit)ctrValue_Path).Text = field.DM017404;
                            ((TextEdit)ctrValue_Path).ReadOnly = currentState == "NORMAL";
                            ((TableLayoutPanel)ctrValue).Controls.Add(ctrValue_Path, 0, 0);

                            SimpleButton ctrValue_Button = new SimpleButton() { Name = "ctr_" + field.DM017401.ToString() + "_Button", Text = "...", Font = All.Font_control, Size = new System.Drawing.Size(44, 26), Margin = new Padding(3, 0, 0, 0) };
                            ctrValue_Button.Click += btnChooseFile_Click;
                            ((SimpleButton)ctrValue_Button).Enabled = currentState != "NORMAL";
                            ((TableLayoutPanel)ctrValue).Controls.Add(ctrValue_Button, 1, 0);
                            break;
                        default: break;
                    }

                    if (ctrValue != null)
                    {
                        ctrValue.Name = "ctrValue_" + field.DM017401.ToString();
                        tableFields.Controls.Add(ctrValue, 1, tableFields.RowCount - 1);
                    }
                }

                panelHeaderFields.Controls.Add(tableFields);
                panelHeaderFields.Height = (int)tableFields.RowStyles.Cast<RowStyle>().Sum(o => o.Height);

                return panelHeaderFields;
            }
            catch (Exception ex)
            {
                Log.write(ex);
                return null;
            }
        }

        void btnNhapdulieu_Click(object sender, EventArgs e, TD_ThuchienNhiemvu_Truongdulieu obj, string currentState)
        {
            FRM_TD_ThuchienNhiemvu_Tab frm = new FRM_TD_ThuchienNhiemvu_Tab();
            frm.currentField = obj;
            frm.currentField_Thamdinh = null;
            frm.currentState = currentState;
            this.FindForm().Enabled = false;
            frm.Show();
            frm.Focus();
        }

        void btnNhapdulieu_Thamdinh_Click(object sender, EventArgs e, TD_Thamdinh_Duyet_Truongdulieu obj, string currentState)
        {
            FRM_TD_ThuchienNhiemvu_Tab frm = new FRM_TD_ThuchienNhiemvu_Tab();
            frm.currentField = null;
            frm.currentField_Thamdinh = obj;
            frm.currentState = currentState;
            this.FindForm().Enabled = false;
            frm.Show();
            frm.Focus();
        }

        void btnNhapdulieu_PheduyetThamdinh_Click(object sender, EventArgs e, TD_Pheduyet_Thamdinh_Duyet_Truongdulieu obj, string currentState)
        {
            FRM_TD_ThuchienNhiemvu_Tab frm = new FRM_TD_ThuchienNhiemvu_Tab();
            frm.currentField = null;
            frm.currentField_Thamdinh = null;
            frm.currentField_PheduyetThamdinh = obj;
            frm.currentState = currentState;
            this.FindForm().Enabled = false;
            frm.Show();
            frm.Focus();
        }

        void btnNhapdulieu_PheduyetVB_Click(object sender, EventArgs e, TD_Pheduyet_VB_Truongdulieu obj, string currentState)
        {
            FRM_TD_ThuchienNhiemvu_Tab frm = new FRM_TD_ThuchienNhiemvu_Tab();
            frm.currentField = null;
            frm.currentField_Thamdinh = null;
            frm.currentField_PheduyetVB = obj;

            frm.currentState = currentState;
            this.FindForm().Enabled = false;
            frm.Show();
            frm.Focus();
        }

        void btnChooseFile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    SimpleButton btnChooseFile = (SimpleButton)sender;
                    TableLayoutPanel tableParent = (TableLayoutPanel)btnChooseFile.Parent;
                    TextEdit txtPath = (TextEdit)tableParent.GetControlFromPosition(0, 0);
                    txtPath.Text = openFileDialog.FileName;
                }
            }
        }
    }
}
