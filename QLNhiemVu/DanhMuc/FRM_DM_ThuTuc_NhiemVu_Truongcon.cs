using DBAccess;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
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
    public partial class FRM_DM_ThuTuc_NhiemVu_Truongcon : BaseForm_Data
    {
        FRM_DM_ThuTuc_NhiemVu frm = null;
        public Guid currentNoidungId = Guid.Empty;
        public List<DM_LoaiThutucNhiemvu_Truongdulieu> currentList = null;
        private int currentRowSelected_D2 = 0;
        private string _status_detail_2 = "NORMAL";
        private DataTable currentDataTable = null;
        public DM_LoaiThutucNhiemvu_Noidung currentNoidung = null;
        private static string currentNoidungLookupData = string.Empty;
        public DM_LoaiThutucNhiemvu_Truongdulieu_LookupData currentTruongdulieu_Lookupdata = null;

        public FRM_DM_ThuTuc_NhiemVu_Truongcon()
        {
            InitializeComponent();
        }

        private void FRM_DM_ThuTuc_NhiemVu_Truongcon_Load(object sender, EventArgs e)
        {
            frm = (FRM_DM_ThuTuc_NhiemVu)Application.OpenForms["FRM_DM_ThuTuc_NhiemVu"];
            if (frm == null) this.Dispose();

            panelHeader1.alignCenter(panelHeader1.Parent);

            LoadList();

            gridView3.OptionsBehavior.Editable = false;
            LoadGridData();

            btn_truongdulieu_kieutruong.Click += btn_truongdulieu_kieutruong_Click;
        }

        void btn_truongdulieu_kieutruong_Click(object sender, EventArgs e)
        {
            DataRow data = gridView3.GetFocusedDataRow();
            //Guid id = Guid.Parse(data.ItemArray[0].ToString());
            string kieutruong = data.ItemArray[4].ToString().Trim();

            if (kieutruong == "8")
            {
                ShowChildForm_Lookup();
            }
        }
        private void ShowChildForm_Lookup()
        {
            this.Enabled = false;
            FRM_DM_LoaiThutucNhiemvu_ChitietTruongdulieu_Lookup frm = new FRM_DM_LoaiThutucNhiemvu_ChitietTruongdulieu_Lookup();
            frm.currentData = currentTruongdulieu_Lookupdata;
            frm.currentState = _status_detail_2;
            frm.Show();
            frm.Focus();
        }
        public void CallBack_UpdateLookupData(bool update)
        {
            this.Enabled = true;
            this.Focus();
        }

        private void LoadGridData()
        {
            ledTruongdulieu_Kieutruong.DataSource = All.dm_loaithutuc_truongdulieu_kieutruong;
            ledTruongdulieu_Kieutruong.DisplayMember = "Description";
            ledTruongdulieu_Kieutruong.ValueMember = "ID";
            ledTruongdulieu_Kieutruong.BestFitRowCount = All.dm_loaithutuc_truongdulieu_kieutruong.Count;
        }

        private void LoadList()
        {
            DataTable dt = UF_Function.ToDataTable(currentList);
            gridControl3.DataSource = dt;
            gridControl3.RefreshDataSource();
            gridView3.BestFitColumns();
        }

        private void btn_capnhat_Click(object sender, EventArgs e)
        {
            frm.CallBack_UpdateTruongcon(true);
            this.Dispose();
        }

        private void btn_thoat_Click(object sender, EventArgs e)
        {
            frm.CallBack_UpdateTruongcon(false);
            this.Dispose();
        }

        private void gridView3_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            currentRowSelected_D2 = e.FocusedRowHandle;
            //currentTruongdulieu_Children = null;
            currentTruongdulieu_Lookupdata = null;

            if (currentRowSelected_D2 < 0)
            {
                return;
            }

            string kieutruong = gridView3.GetFocusedDataRow().ItemArray[4].ToString().Trim();
            string data = gridView3.GetFocusedDataRow().ItemArray[7].ToString().Trim();

            if (string.IsNullOrEmpty(data))
            {
                //currentTruongdulieu_Children = null;
                currentTruongdulieu_Lookupdata = null;
            }
            else
            {
                if (kieutruong == "8")
                    currentTruongdulieu_Lookupdata = JsonConvert.DeserializeObject<DM_LoaiThutucNhiemvu_Truongdulieu_LookupData>(data);
                //else if (kieutruong == "9")
                //    currentTruongdulieu_Children = JsonConvert.DeserializeObject<List<Guid>>(data);
            }
        }

        private void gridView3_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            GridView view = sender as GridView;
            if (_status_detail_2 == "NORMAL") return;

            int rowselect = e.RowHandle;
            string maso = view.GetRowCellValue(rowselect, "DM016204").ToString();
            string ten = view.GetRowCellValue(rowselect, "DM016205").ToString();
            string tenhienthi = view.GetRowCellValue(rowselect, "DM016206").ToString();
            string kieutruong = view.GetRowCellValue(rowselect, "DM016207").ToString();
            string dorong = view.GetRowCellValue(rowselect, "DM016208").ToString();
            string congthuctinh = view.GetRowCellValue(rowselect, "DM016213").ToString();
            string sapxep = view.GetRowCellValue(rowselect, "DM016214").ToString();

            if ((maso + ten + tenhienthi + kieutruong + dorong + congthuctinh + sapxep).Length == 0)
            {
                _status_detail_2 = "NORMAL";
                gridView3.DeleteRow(gridView3.FocusedRowHandle);
                gridView3.OptionsBehavior.Editable = false;
                return;
            }

            if (maso.Length == 0)
            {
                All.Show_message("Bạn phải nhập Mã số!");
                return;
            }
            if (ten.Length == 0)
            {
                All.Show_message("Bạn phải nhập Tên trường!");
                return;
            }
            if (tenhienthi.Length == 0)
            {
                All.Show_message("Bạn phải Tên hiển thị!");
                return;
            }
            if (kieutruong.Length == 0)
            {
                All.Show_message("Bạn phải chọn Kiểu trường!");
                return;
            }
            if (dorong.Length == 0)
            {
                All.Show_message("Bạn phải nhập Độ rộng!");
                return;
            }
            if (congthuctinh.Length == 0)
            {
                All.Show_message("Bạn phải nhập Công thức tính!");
                return;
            }
            if (sapxep.Length == 0)
            {
                All.Show_message("Bạn phải nhập Sắp xếp!");
                return;
            }

            DM_LoaiThutucNhiemvu_Truongdulieu obj = PrepareD2(view);
            GridColumn col1 = view.Columns[1];
            if (obj == null)
            {
                All.Show_message("Có lỗi trong quá trình cập nhật dữ liệu!");
                //set_enable_groupControl(_status_detail_2);
                gridView3.OptionsBehavior.Editable = false;
                gridView3.OptionsBehavior.ReadOnly = true;
                return;
            }

            if (_status_detail_2 == "NEW")
            {
                APIResponseData result = Helpers.ThutucNhiemvu_Truongdulieu.Create(obj);
                if (result == null)
                {
                    All.Show_message("Có lỗi trong quá trình cập nhật dữ liệu!");
                }
                else if (result.ErrorCode == 0)
                {
                    All.Show_message("Thêm mới thành công Trường dữ liệu: " + obj.DM016204);
                    _status_detail_2 = "NORMAL";
                    LoadList();
                }
                else
                {
                    All.Show_message("Error code " + result.ErrorCode + ": " + result.Message);
                }
            }
            else
            {
                APIResponseData result = Helpers.ThutucNhiemvu_Truongdulieu.Update(obj);
                if (result == null)
                {
                    All.Show_message("Có lỗi trong quá trình cập nhật dữ liệu!");
                }
                else if (result.ErrorCode == 0)
                {
                    All.Show_message("Cập nhật thành công Trường dữ liệu: " + obj.DM016204);
                    _status_detail_2 = "NORMAL";
                    LoadList();
                }
                else
                {
                    All.Show_message("Error code " + result.ErrorCode + ": " + result.Message);
                }
            }

            gridView3.OptionsBehavior.Editable = false;
            gridView3.OptionsBehavior.ReadOnly = true;
        }

        private DM_LoaiThutucNhiemvu_Truongdulieu PrepareD2(GridView view)
        {
            try
            {
                int rowselect = view.FocusedRowHandle;
                DM_LoaiThutucNhiemvu_Truongdulieu obj = new DM_LoaiThutucNhiemvu_Truongdulieu();
                obj.DM016201 = _status_detail_2 == "NEW" ? Guid.NewGuid() : Guid.Parse(view.GetRowCellValue(rowselect, "DM016201").ToString());
                obj.DM016204 = view.GetRowCellValue(rowselect, "DM016204").ToString();
                obj.DM016205 = view.GetRowCellValue(rowselect, "DM016205").ToString();
                obj.DM016206 = view.GetRowCellValue(rowselect, "DM016206").ToString();
                obj.DM016207 = view.GetRowCellValue(rowselect, "DM016207").ToString().Trim();
                obj.DM016208 = int.Parse(view.GetRowCellValue(rowselect, "DM016208").ToString());
                obj.DM016209 = string.Empty;
                obj.DM016210 = obj.DM016207 == "8" ? JsonConvert.SerializeObject(currentTruongdulieu_Lookupdata) : string.Empty;
                obj.DM016213 = view.GetRowCellValue(rowselect, "DM016213").ToString();
                obj.DM016214 = int.Parse(view.GetRowCellValue(rowselect, "DM016214").ToString());
                char batbuocnhap = view.GetRowCellValue(rowselect, "DM016215") == DBNull.Value ? '0' : char.Parse(view.GetRowCellValue(rowselect, "DM016215").ToString().Trim());
                obj.DM016215 = batbuocnhap;
                obj.DM016217 = All.gs_user_id;
                obj.DM016218 = DateTime.Now;
                obj.DM016219 = All.gs_user_id;
                obj.DM016220 = DateTime.Now;
                obj.DM016216 = view.GetRowCellValue(rowselect, "DM016216") == DBNull.Value ? Guid.Empty : Guid.Parse(view.GetRowCellValue(rowselect, "DM016216").ToString());
                obj.NoidungId = currentNoidungId;

                return obj;
            }
            catch (Exception ex)
            {
                Log.write(ex);
                return null;
            }
        }

        private void MenuItem_them_d2_Click(object sender, EventArgs e)
        {
            if (_status_detail_2 == "NORMAL")
            {
                add_newDetail_D2();
            }
        }

        private void add_newDetail_D2()
        {
            if (_status_detail_2 == "NORMAL")
            {
                gridView3.AddNewRow();
                //currentRowSelected_D1 = gridView2.RowCount - 1;
                _status_detail_2 = "NEW";
                gridView3.ShowInplaceEditForm();
                gridView3.OptionsBehavior.Editable = true;
                gridView3.OptionsBehavior.ReadOnly = false;
                gridView3.Focus();
            }
        }

        private void MenuItem_sua_d2_Click(object sender, EventArgs e)
        {
            if (_status_detail_2 == "NORMAL")
            {
                _status_detail_2 = "EDIT";
                gridView3.ShowInplaceEditForm();
                gridView3.OptionsBehavior.Editable = true;
                gridView3.OptionsBehavior.ReadOnly = false;
                gridView3.Focus();
            }
        }

        private void MenuItem_xoa_d2_Click(object sender, EventArgs e)
        {
            if (_status_detail_2 == "NEW")
            {
                gridView3.DeleteRow(gridView3.FocusedRowHandle);
                _status_detail_2 = "NORMAL";
                gridView3.OptionsBehavior.Editable = false;
            }
            else
            {
                if (gridView3.FocusedRowHandle < 0) return;

                Guid id_delete = Guid.Parse(gridView3.GetRowCellValue(gridView3.FocusedRowHandle, "DM016201").ToString());
                if (MessageBox.Show("Bạn chắc chắn muốn xóa Trường dữ liệu đang chọn?", "Cảnh báo!", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    APIResponseData result = Helpers.ThutucNhiemvu_Truongdulieu.Delete(new List<Guid>() { id_delete });
                    if (result == null)
                    {
                        All.Show_message("Có lỗi trong quá trình cập nhật dữ liệu!");
                    }
                    else if (result.ErrorCode == 0)
                    {
                        _status_detail_2 = "NORMAL";
                        gridView3.DeleteRow(gridView3.FocusedRowHandle);
                        gridView3.OptionsBehavior.Editable = false;
                        All.Show_message("Xóa thành công Trường dữ liệu đã chọn!");
                        //LoadList();
                    }
                    else
                    {
                        All.Show_message("Error code " + result.ErrorCode + ": " + result.Message);
                    }
                }
            }
        }
    }
}
