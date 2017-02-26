using QLNhiemvu_DBEntities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLNhiemVu_Defines
{
    public class All
    {
        #region Bổ sung

        public static List<TD_ThuchienNhiemvu_Cothienthi> const_static_column_phancong = new List<TD_ThuchienNhiemvu_Cothienthi>() { 
            new TD_ThuchienNhiemvu_Cothienthi()
            { 
                DisplayName = "Người chuyển VB đến",
                IsChecked = false,
                ColumnName = "Nguoitao",
            },
            new TD_ThuchienNhiemvu_Cothienthi()
            { 
                DisplayName = "Ngày chuyển",
                IsChecked = false,
                ColumnName = "DM016707",
            },
            new TD_ThuchienNhiemvu_Cothienthi()
            { 
                DisplayName = "Xem các ý kiến trước",
                IsChecked = false,
                ColumnName = "Xemykien",
            },
            new TD_ThuchienNhiemvu_Cothienthi()
            { 
                
                DisplayName = "Trạng thái",
                IsChecked = false,
                ColumnName = "Trangthai",
            },
        };

        public static List<DM_LoaiThutucNhiemvu_LoaiCapphep> dm_loaithutuc_loaicapphep = new List<DM_LoaiThutucNhiemvu_LoaiCapphep>() { 
            new DM_LoaiThutucNhiemvu_LoaiCapphep(){ ID='1', Description="Tất cả"},
            new DM_LoaiThutucNhiemvu_LoaiCapphep(){ ID='2', Description="Đơn vị sử dụng chương trình cụ thể"},
            new DM_LoaiThutucNhiemvu_LoaiCapphep(){ ID='3', Description="Đơn vị trong hệ thống đều dùng"}
        };
        public static List<DM_Huongdan_LoaiHuongdan> dm_loaithutuc_loaihuongdan = new List<DM_Huongdan_LoaiHuongdan>() { 
            new DM_Huongdan_LoaiHuongdan(){ ID='1', Description="Tệp văn bản"},
            new DM_Huongdan_LoaiHuongdan(){ ID='2', Description="Tệp video"},
            new DM_Huongdan_LoaiHuongdan(){ ID='3', Description="Tệp audio"}
        };
        public static List<DM_LoaiThutucNhiemvu_Noidung_Cachnhap> dm_loaithutuc_noidung_cachnhap = new List<DM_LoaiThutucNhiemvu_Noidung_Cachnhap>() { 
            new DM_LoaiThutucNhiemvu_Noidung_Cachnhap(){ ID='1', Description="Nhập đoạn văn"},
            new DM_LoaiThutucNhiemvu_Noidung_Cachnhap(){ ID='2', Description="Nhập các trường dữ liệu"}
        };
        public static List<DM_LoaiThutucNhiemvu_Truongdulieu_Cachnhap> dm_loaithutuc_truongdulieu_cachnhap = new List<DM_LoaiThutucNhiemvu_Truongdulieu_Cachnhap>() { 
            new DM_LoaiThutucNhiemvu_Truongdulieu_Cachnhap(){ ID="1", Description="Nhập từ bàn phím"},
            new DM_LoaiThutucNhiemvu_Truongdulieu_Cachnhap(){ ID="2", Description="Nhập từ bảng dữ liệu có sẵn, tự tính"}
        };
        public static List<DM_LoaiThutucNhiemvu_Truongdulieu_Kieutruong> dm_loaithutuc_truongdulieu_kieutruong = new List<DM_LoaiThutucNhiemvu_Truongdulieu_Kieutruong>() { 
            new DM_LoaiThutucNhiemvu_Truongdulieu_Kieutruong(){ ID="0", Description="NONE"},
            new DM_LoaiThutucNhiemvu_Truongdulieu_Kieutruong(){ ID="1", Description="Text"},
            new DM_LoaiThutucNhiemvu_Truongdulieu_Kieutruong(){ ID="2", Description="Number"},
            new DM_LoaiThutucNhiemvu_Truongdulieu_Kieutruong(){ ID="3", Description="Yes/No"},
            new DM_LoaiThutucNhiemvu_Truongdulieu_Kieutruong(){ ID="4", Description="Date"},
            new DM_LoaiThutucNhiemvu_Truongdulieu_Kieutruong(){ ID="5", Description="Datetime"},
            new DM_LoaiThutucNhiemvu_Truongdulieu_Kieutruong(){ ID="6", Description="Time"},
            new DM_LoaiThutucNhiemvu_Truongdulieu_Kieutruong(){ ID="7", Description="Memo"},
            new DM_LoaiThutucNhiemvu_Truongdulieu_Kieutruong(){ ID="8", Description="Lookup"},
            new DM_LoaiThutucNhiemvu_Truongdulieu_Kieutruong(){ ID="9", Description="Tab"},
            new DM_LoaiThutucNhiemvu_Truongdulieu_Kieutruong(){ ID="10", Description="Image"},
        };
        public static List<string> dm_loaithutuc_truongdulieu_lookup_formulas = new List<string>() { "=", ">=", "<=", "likeend", "likefirst", "contain" };
        public static List<string> dm_loaithutuc_truongdulieu_lookup_conditioncombines = new List<string>() { "AND", "OR" };
        public static List<TD_Phancong_Phamvi> td_phancong_phamvi = new List<TD_Phancong_Phamvi>() { 
            new TD_Phancong_Phamvi(){ ID='1', Description="Phân công trong lãnh đạo cùng cấp"},
            new TD_Phancong_Phamvi(){ ID='2', Description="Phân công cho đơn vị cấp dưới"},
            new TD_Phancong_Phamvi(){ ID='3', Description="Chuyển chuyên viên thực hiện"}
        };
        public static List<TD_Phancong_Thamquyen> td_phancong_thamquyen = new List<TD_Phancong_Thamquyen>() { 
            new TD_Phancong_Thamquyen(){ ID='1', Description="Báo cáo của văn phòng cơ quan"},
            new TD_Phancong_Thamquyen(){ ID='2', Description="Phân công của thủ trưởng cơ quan"},
            new TD_Phancong_Thamquyen(){ ID='3', Description="Phân công của lãnh đạo phụ trách"},
            new TD_Phancong_Thamquyen(){ ID='4', Description="Phân công của lãnh đạo đơn vị tham mưu"},
            new TD_Phancong_Thamquyen(){ ID='5', Description="Phân công của lãnh đạo phòng ban"}
        };


        #endregion

        public static int gi_Gridcontrol_width = 350;
        public static Size gs_img_size = new Size(145, 164);
        public static Font Font_control = new Font("Times New Roman", (float)12.75);
        public static Guid gs_dv_quanly = Guid.Parse("e5b0f164ffb0e5119be8c8f73300b490");
        public static Guid gs_user_id = Guid.Parse("D22B2275986F97489B0CE55EB5F163FA");
        public static string gs_user_name = "Nguyễn văn XXX";
        public static string gs_ten_dv_quanly = "Cơ quan X";
        public static string dateFormat = "dd/MM/yyyy";
        public static string datetimeFormat = "dd/MM/yyyy HH:mm:ss";
        public static string timeFormat = "HH:mm:ss";
        public static string numberFormat = "###############";//15 so   
        public static string moneyFormat = "###,###,###,##0.00";
        public static string QuanlityFormat = "###,###,###,##0.0";
        public static string PriceFormat = "###,###,###,##0.0000";
        public static string yyyyMMdd = "yyyyMMdd";
        public static Font Font_GridRow = new Font("Tohoma", (float)9.75);

        public static void Show_Exception(string Message)
        {
            MessageBox.Show(Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void Show_message(string message)
        {
            MessageBox.Show(message, "Thông báo");
        }
    }
}
