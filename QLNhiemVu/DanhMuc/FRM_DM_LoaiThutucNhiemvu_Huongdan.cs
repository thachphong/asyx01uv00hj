using DBAccess;
using Decided.Libs;
using DevExpress.XtraEditors;
using Newtonsoft.Json;
using QLNhiemVu.FRMModel;
using QLNhiemvu_DBEntities;
using QLNhiemVu_Defines;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLNhiemVu.DanhMuc
{
    public partial class FRM_DM_LoaiThutucNhiemvu_Huongdan : BaseForm_Data
    {
        private static List<DM_Huongdan> currentList = null;
        private static int currentRowSelected = int.MinValue;
        private static DM_Huongdan currentDataSelected = null;
        private static string currentState = "NORMAL";
        private static string filePath = string.Empty;
        private static Guid _loaithutuc_nhiemvu = Guid.Empty;
        private static bool _upload_flg = false;
        public FRM_DM_LoaiThutucNhiemvu_Huongdan(Guid loaithutuc_huongdan)
        {
            InitializeComponent();
            _loaithutuc_nhiemvu = loaithutuc_huongdan;
        }

        private void FRM_DM_Huongdan_Load(object sender, EventArgs e)
        {
            lblHeadTitle1.setText("Danh mục - Hướng dẫn");
            lblHeadTitle1.alignCenter(lblHeadTitle1.Parent);
            panelHeader1.alignCenter(panelHeader1.Parent);
            panelHeader2.alignCenter(panelHeader2.Parent);
            panelHeader3.alignCenter(panelHeader3.Parent);

            BindControlEvents();

            openFileDialog1.Filter = "Text files|*.txt; *.doc; *.docx; *.pdf; *.xls; *.xlsx; *.ppt; *.pptx|Video files|*.avi; *.mwv|Audio files|*.mp3|All files|*.*";
            openFileDialog1.Title = "Chọn tệp nội dung hướng dẫn!";

            currentState = "NORMAL";

            //LoadThutuc();
            Load_ThongBao();
            LoadHuongdan();
            LoadList();
        }

        private void BindControlEvents()
        {
            gridColumn10.ColumnEdit.EditValueChanged += ColumnEdit_EditValueChanged;

            uC_MenuBtn1.btn_them.Click += btn_them_Click;
            uC_MenuBtn1.btn_thoat.Click += btn_thoat_Click;
            uC_MenuBtn1.btn_boqua.Click += btn_boqua_Click;
            uC_MenuBtn1.btn_capnhat.Click += btn_capnhat_Click;
            uC_MenuBtn1.btn_sua.Click += btn_sua_Click;
            uC_MenuBtn1.btn_xoa.Click += btn_xoa_Click;
        }

        void btn_xoa_Click(object sender, EventArgs e)
        {
            List<Guid> listChecked = GetIDChecked();
            if (listChecked == null)
            {
                All.Show_message("Vui lòng chọn trước khi xóa!");
                return;
            }

            if (MessageBox.Show("Bạn chắc chắn muốn xóa " + listChecked.Count + " Hướng dẫn đang chọn?", "Cảnh báo!", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                APIResponseData result = Helpers.ThutucNhiemvu_Huongdan.Delete(listChecked);
                if (result == null)
                {
                    All.Show_message("Có lỗi trong quá trình cập nhật dữ liệu!");
                }
                else if (result.ErrorCode == 0)
                {
                    All.Show_message("Xóa thành công " + listChecked.Count + " Hướng dẫn đã chọn!");
                    currentState = "NORMAL";
                    LoadList();
                }
                else
                {
                    All.Show_message("Error code " + result.ErrorCode + ": " + result.Message);
                }
            }
        }

        private List<Guid> GetIDChecked()
        {
            List<Guid> result = new List<Guid>();

            for (int i = 0; i < gridView1.RowCount; i++)
            {
                bool isChecked = (bool)gridView1.GetRowCellValue(i, gridColumn10);
                if (isChecked)
                    result.Add((Guid)gridView1.GetRowCellValue(i, gridColumn1));
            }

            return result.Count == 0 ? null : result;
        }

        void btn_sua_Click(object sender, EventArgs e)
        {
            currentState = "EDIT";
            uC_MenuBtn1.set_status_menu(currentState, currentList == null ? 0 : currentList.Count);
            AssignDetailFormValue(currentDataSelected);
            SetDetailFormEnable(true);
            _upload_flg = false;
        }

        void btn_capnhat_Click(object sender, EventArgs e)
        {
            if (currentState == "NORMAL") return;

            if (!ValidateDetailForm()) return;

            DM_Huongdan obj = PrepareDetail();

            if (obj == null)
            {
                All.Show_message("Có lỗi trong quá trình cập nhật dữ liệu!");
                return;
            }

            if (currentState == "NEW")
            {
                APIResponseData result = Helpers.ThutucNhiemvu_Huongdan.Create(obj);
                if (result == null)
                {
                    All.Show_message("Có lỗi trong quá trình cập nhật dữ liệu!");
                }
                else if (result.ErrorCode == 0)
                {
                    All.Show_message("Thêm mới thành công Hướng dẫn: " + obj.DM016303);
                    currentState = "NORMAL";
                    LoadList();
                }
                else
                {
                    All.Show_message("Error code " + result.ErrorCode + ": " + result.Message);
                }
            }
            else
            {
                APIResponseData result = Helpers.ThutucNhiemvu_Huongdan.Update(obj);
                if (result == null)
                {
                    All.Show_message("Có lỗi trong quá trình cập nhật dữ liệu!");
                }
                else if (result.ErrorCode == 0)
                {
                    All.Show_message("Cập nhật thành công Hướng dẫn: " + obj.DM016303);
                    currentState = "NORMAL";
                    LoadList();
                }
                else
                {
                    All.Show_message("Error code " + result.ErrorCode + ": " + result.Message);
                    if (result.ErrorCode == -1)
                        LoadList();
                }
            }
        }

        private DM_Huongdan PrepareDetail()
        {
            try
            {
                DM_Huongdan obj = new DM_Huongdan();
                obj.DM016301 = currentState == "NEW" ? Guid.NewGuid() : currentDataSelected.DM016301;
                obj.DM016302 = _loaithutuc_nhiemvu;
                obj.DM016303 = m_ma_huongdan.Text;
                obj.DM016304 = m_ten_huongdan.Text;
                obj.DM016305 = char.Parse(m_loai_huondan.EditValue.ToString());
                obj.DM016306 = currentState == "NEW" ? All.gs_user_id : currentDataSelected.DM016306;
                obj.DM016307 = currentState == "NEW" ? DateTime.Now : currentDataSelected.DM016307;
                obj.DM016308 = All.gs_user_id;
                obj.DM016309 = DateTime.Now;
                obj.DM016311 = (m_maloi_lk.EditValue == null || m_maloi_lk.EditValue.ToString() == Guid.Empty.ToString()) ? Guid.Empty : Guid.Parse(m_maloi_lk.EditValue.ToString());

                if (m_tiep_dinhkem.Text.Trim() != string.Empty && _upload_flg)
                {
                    string uri = Helpers.CreateRequestUrl_UploadFile();
                    uri += "&n=loaithutucnhiemvu_huongdan&fn=" + obj.DM016301.ToString();
                    string response = WebUtils.Request_UploadFile(uri, m_tiep_dinhkem.Text.Trim());
                    APIResponseData result = JsonConvert.DeserializeObject<APIResponseData>(response);
                    if (result.ErrorCode != 0)
                    {
                        All.Show_message("Error code " + result.ErrorCode + ": Lỗi không thể upload tệp!");
                        return null;
                    }

                    obj.DM016310 = result.Data.ToString();
                }
                else
                {
                    obj.DM016310 = currentDataSelected == null ? string.Empty : currentDataSelected.DM016310;
                }

                return obj;
            }
            catch (Exception ex)
            {
                Log.write(ex);
                return null;
            }
        }

        private bool ValidateDetailForm()
        {
            if (m_loai_huondan.EditValue.ToString().Trim() == string.Empty)
            {
                All.Show_message("Vui lòng chọn Loại hướng dẫn!");
                m_loai_huondan.Focus();
                return false;
            }

            if (m_ma_huongdan.Text.Trim() == string.Empty)
            {
                All.Show_message("Vui lòng nhập Mã hướng dẫn!");
                m_ma_huongdan.Focus();
                return false;
            }

            if (m_ten_huongdan.Text.Trim() == string.Empty)
            {
                All.Show_message("Vui lòng nhập Tên hướng dẫn!");
                m_ten_huongdan.Focus();
                return false;
            }

            if (currentState == "NEW" && m_tiep_dinhkem.Text.Trim() == string.Empty)
            {
                All.Show_message("Vui lòng chọn tệp nội dung hướng dẫn!");
                btn_chonfile.PerformClick();
            }

            return true;
        }

        void btn_boqua_Click(object sender, EventArgs e)
        {
            currentState = "NORMAL";
            uC_MenuBtn1.set_status_menu(currentState, currentList == null ? 0 : currentList.Count);
            AssignDetailFormValue(currentDataSelected);
            SetDetailFormEnable(false);
        }

        void btn_thoat_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        void btn_them_Click(object sender, EventArgs e)
        {
            currentState = "NEW";
            uC_MenuBtn1.set_status_menu(currentState, 0);
            AssignDetailFormValue(null);
            SetDetailFormEnable(true);
            _upload_flg = false;
        }

        private void LoadHuongdan()
        {
            m_loai_huondan.Properties.DataSource = All.dm_loaithutuc_loaihuongdan;
            m_loai_huondan.Properties.DisplayMember = "Description";
            m_loai_huondan.Properties.ValueMember = "ID";
            m_loai_huondan.Properties.BestFitRowCount = All.dm_loaithutuc_loaihuongdan.Count;
            m_loai_huondan.Refresh();
        }

        private void LoadThutuc()
        {
            List<DM_LoaiThutucNhiemvu> list = Helpers.ThutucNhiemvu.GetList();
            m_maloi_lk.Properties.DataSource = list;
            m_maloi_lk.Properties.DisplayMember = "DM016004";
            m_maloi_lk.Properties.ValueMember = "DM016001";
            m_maloi_lk.Properties.BestFitRowCount = list.Count;
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

        private void LoadList()
        {
            currentList = Helpers.ThutucNhiemvu_Huongdan.GetList(_loaithutuc_nhiemvu);

            gridControl1.DataSource = currentList;
            gridControl1.RefreshDataSource();
            gridView1.BestFitColumns();

            uC_MenuBtn1.set_status_menu(currentState, currentList == null ? 0 : currentList.Count);
            SetDetailFormEnable(false);

            DM_Huongdan current = (DM_Huongdan)gridView1.GetFocusedRow();
            AssignDetailFormValue(current);
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

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            currentRowSelected = e.FocusedRowHandle;
            currentDataSelected = (DM_Huongdan)gridView1.GetRow(currentRowSelected);
            AssignDetailFormValue(currentDataSelected);
        }

        private void SetDetailFormEnable(bool isEnable)
        {
            m_maloi_lk.ReadOnly = !isEnable;
            m_loai_huondan.ReadOnly = !isEnable;
            m_ma_huongdan.ReadOnly = !isEnable;
            m_ten_huongdan.ReadOnly = !isEnable;

            groupControl1.Enabled = !isEnable;

            if (isEnable)
                m_ma_huongdan.Focus();
        }

        private void AssignDetailFormValue(DM_Huongdan data)
        {
            m_maloi_lk.EditValue = data == null ? Guid.Empty : data.DM016311;
            m_loai_huondan.EditValue = data == null ? ' ' : data.DM016305;
            m_ma_huongdan.Text = data == null ? string.Empty : data.DM016303;
            m_ten_huongdan.Text = data == null ? string.Empty : data.DM016304;
            m_tiep_dinhkem.Text = data == null ? string.Empty : data.DM016310;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                m_tiep_dinhkem.Text = openFileDialog1.FileName;
                string path = openFileDialog1.FileName;
                _upload_flg = true;
            }
        }
        private void Load_ThongBao()
        {

            List<DM_ThongBao> list = Helpers.ThongBao.GetList();
            m_maloi_lk.Properties.DataSource = list;
            m_maloi_lk.Properties.DisplayMember = "SYS03";
            m_maloi_lk.Properties.ValueMember = "SYS01";
            m_maloi_lk.Properties.BestFitRowCount = list.Count;
        }
    }
}
