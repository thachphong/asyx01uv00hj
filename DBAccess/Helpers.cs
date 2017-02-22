using Newtonsoft.Json;
using QLNhiemVu;
using QLNhiemvu_DBEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBAccess
{
    public class Helpers
    {
        #region Utilities

        public static string CreateRequestUrl(string module)
        {
            return DefineValues.API_Host + module + "?u=" + DefineValues.API_Authentication_User + "&p=" + DefineValues.API_Authentication_Password;
        }

        public static string CreateRequestUrl_UploadFile()
        {
            return DefineValues.API_Host + "Functions/ResourceManager.aspx" + "?u=" + DefineValues.API_Authentication_User + "&p=" + DefineValues.API_Authentication_Password;
        }

        public static APIResponseData ConvertFromString(string data)
        {
            try
            {
                return JsonConvert.DeserializeObject<APIResponseData>(data);
            }
            catch (Exception ex)
            {
                Log.write(ex);
                return null;
            }
        }

        public class DBUtilities
        {
            public static bool CheckConnection()
            {
                try
                {
                    string url = CreateRequestUrl("dbutilities");
                    APIRequestData requestData = new APIRequestData()
                    {
                        Action = "checkdbconnection",
                        Data = null
                    };
                    string response = Decided.Libs.WebUtils.Request_POST(url, JsonConvert.SerializeObject(requestData));

                    if (string.IsNullOrEmpty(response)) return false;

                    APIResponseData result = ConvertFromString(response);
                    if (result == null || result.ErrorCode != 0) return false;

                    return result.ErrorCode == 0;
                }
                catch (Exception ex)
                {
                    Log.write(ex);
                    return false;
                }
            }
        }

        #endregion

        #region ThutucNhiemvu

        public class ThutucNhiemvu
        {
            public static DM_LoaiThutucNhiemvu Get_ByMaso(string maso)
            {
                try
                {
                    string url = CreateRequestUrl("loaithutuc");
                    APIRequestData requestData = new APIRequestData()
                    {
                        Action = "get_bymaso",
                        Data = maso
                    };
                    string response = Decided.Libs.WebUtils.Request_POST(url, JsonConvert.SerializeObject(requestData));

                    if (string.IsNullOrEmpty(response)) return null;

                    APIResponseData result = ConvertFromString(response);
                    if (result == null || result.ErrorCode != 0) return null;

                    return result.Data == null ? null : JsonConvert.DeserializeObject<DM_LoaiThutucNhiemvu>(result.Data.ToString());
                }
                catch (Exception ex)
                {
                    Log.write(ex);
                    return null;
                }
            }
            public static List<DM_LoaiThutucNhiemvu> GetList(DM_LoaiThutucNhiemvu_Filter filter = null)
            {
                try
                {
                    string url = CreateRequestUrl("loaithutuc");
                    APIRequestData requestData = new APIRequestData()
                    {
                        Action = "getlist",
                        Data = JsonConvert.SerializeObject(filter)
                    };
                    string response = Decided.Libs.WebUtils.Request_POST(url, JsonConvert.SerializeObject(requestData));

                    if (string.IsNullOrEmpty(response)) return null;

                    APIResponseData result = ConvertFromString(response);
                    if (result == null || result.ErrorCode != 0) return null;

                    return result.Data == null ? null : JsonConvert.DeserializeObject<List<DM_LoaiThutucNhiemvu>>(result.Data.ToString());
                }
                catch (Exception ex)
                {
                    Log.write(ex);
                    return null;
                }
            }

            public static APIResponseData Create(DM_LoaiThutucNhiemvu obj)
            {
                try
                {
                    string url = CreateRequestUrl("loaithutuc");
                    APIRequestData requestData = new APIRequestData()
                    {
                        Action = "create",
                        Data = JsonConvert.SerializeObject(obj)
                    };
                    string response = Decided.Libs.WebUtils.Request_POST(url, JsonConvert.SerializeObject(requestData));

                    if (string.IsNullOrEmpty(response)) return null;

                    return ConvertFromString(response);
                }
                catch (Exception ex)
                {
                    Log.write(ex);
                    return null;
                }
            }

            public static APIResponseData Update(DM_LoaiThutucNhiemvu obj)
            {
                try
                {
                    string url = CreateRequestUrl("loaithutuc");
                    APIRequestData requestData = new APIRequestData()
                    {
                        Action = "update",
                        Data = JsonConvert.SerializeObject(obj)
                    };
                    string response = Decided.Libs.WebUtils.Request_POST(url, JsonConvert.SerializeObject(requestData));

                    if (string.IsNullOrEmpty(response)) return null;

                    return ConvertFromString(response);
                }
                catch (Exception ex)
                {
                    Log.write(ex);
                    return null;
                }
            }

            public static APIResponseData Delete(List<Guid> list)
            {
                try
                {
                    string url = CreateRequestUrl("loaithutuc");
                    APIRequestData requestData = new APIRequestData()
                    {
                        Action = "delete",
                        Data = JsonConvert.SerializeObject(list)
                    };
                    string response = Decided.Libs.WebUtils.Request_POST(url, JsonConvert.SerializeObject(requestData));

                    if (string.IsNullOrEmpty(response)) return null;

                    return ConvertFromString(response);
                }
                catch (Exception ex)
                {
                    Log.write(ex);
                    return null;
                }
            }
        }

        #endregion

        #region ThutucNhiemvu_Huongdan

        public class ThutucNhiemvu_Huongdan
        {
            public static List<DM_Huongdan> GetList(Guid thutucId)
            {
                try
                {
                    string url = CreateRequestUrl("loaithutuc_huongdan");
                    APIRequestData requestData = new APIRequestData()
                    {
                        Action = "getlist"
                    };
                    if (thutucId != Guid.Empty)
                        requestData.Data = thutucId;

                    string response = Decided.Libs.WebUtils.Request_POST(url, JsonConvert.SerializeObject(requestData));

                    if (string.IsNullOrEmpty(response)) return null;

                    APIResponseData result = ConvertFromString(response);
                    if (result == null || result.ErrorCode != 0) return null;

                    return result.Data == null ? null : JsonConvert.DeserializeObject<List<DM_Huongdan>>(result.Data.ToString());
                }
                catch (Exception ex)
                {
                    Log.write(ex);
                    return null;
                }
            }

            public static APIResponseData Create(DM_Huongdan obj)
            {
                try
                {
                    string url = CreateRequestUrl("loaithutuc_huongdan");
                    APIRequestData requestData = new APIRequestData()
                    {
                        Action = "create",
                        Data = JsonConvert.SerializeObject(obj)
                    };
                    string response = Decided.Libs.WebUtils.Request_POST(url, JsonConvert.SerializeObject(requestData));

                    if (string.IsNullOrEmpty(response)) return null;

                    return ConvertFromString(response);
                }
                catch (Exception ex)
                {
                    Log.write(ex);
                    return null;
                }
            }

            public static APIResponseData Update(DM_Huongdan obj)
            {
                try
                {
                    string url = CreateRequestUrl("loaithutuc_huongdan");
                    APIRequestData requestData = new APIRequestData()
                    {
                        Action = "update",
                        Data = JsonConvert.SerializeObject(obj)
                    };
                    string response = Decided.Libs.WebUtils.Request_POST(url, JsonConvert.SerializeObject(requestData));

                    if (string.IsNullOrEmpty(response)) return null;

                    return ConvertFromString(response);
                }
                catch (Exception ex)
                {
                    Log.write(ex);
                    return null;
                }
            }

            public static APIResponseData Delete(List<Guid> list)
            {
                try
                {
                    string url = CreateRequestUrl("loaithutuc_huongdan");
                    APIRequestData requestData = new APIRequestData()
                    {
                        Action = "delete",
                        Data = JsonConvert.SerializeObject(list)
                    };
                    string response = Decided.Libs.WebUtils.Request_POST(url, JsonConvert.SerializeObject(requestData));

                    if (string.IsNullOrEmpty(response)) return null;

                    return ConvertFromString(response);
                }
                catch (Exception ex)
                {
                    Log.write(ex);
                    return null;
                }
            }
        }

        #endregion

        #region ThutucNhiemvu_Noidung

        public class ThutucNhiemvu_Noidung
        {
            public static List<DM_LoaiThutucNhiemvu_Noidung> GetList(Guid thutucID)
            {
                try
                {
                    string url = CreateRequestUrl("loaithutuc_noidung");
                    APIRequestData requestData = new APIRequestData()
                    {
                        Action = "getlist",
                        Data = thutucID.ToString()
                    };
                    string response = Decided.Libs.WebUtils.Request_POST(url, JsonConvert.SerializeObject(requestData));

                    if (string.IsNullOrEmpty(response)) return null;

                    APIResponseData result = ConvertFromString(response);
                    if (result == null || result.ErrorCode != 0) return null;

                    return result.Data == null ? null : JsonConvert.DeserializeObject<List<DM_LoaiThutucNhiemvu_Noidung>>(result.Data.ToString());
                }
                catch (Exception ex)
                {
                    Log.write(ex);
                    return null;
                }
            }

            public static APIResponseData Create(DM_LoaiThutucNhiemvu_Noidung obj)
            {
                try
                {
                    string url = CreateRequestUrl("loaithutuc_noidung");
                    APIRequestData requestData = new APIRequestData()
                    {
                        Action = "create",
                        Data = JsonConvert.SerializeObject(obj)
                    };
                    string response = Decided.Libs.WebUtils.Request_POST(url, JsonConvert.SerializeObject(requestData));

                    if (string.IsNullOrEmpty(response)) return null;

                    return ConvertFromString(response);
                }
                catch (Exception ex)
                {
                    Log.write(ex);
                    return null;
                }
            }

            public static APIResponseData Update(DM_LoaiThutucNhiemvu_Noidung obj)
            {
                try
                {
                    string url = CreateRequestUrl("loaithutuc_noidung");
                    APIRequestData requestData = new APIRequestData()
                    {
                        Action = "update",
                        Data = JsonConvert.SerializeObject(obj)
                    };
                    string response = Decided.Libs.WebUtils.Request_POST(url, JsonConvert.SerializeObject(requestData));

                    if (string.IsNullOrEmpty(response)) return null;

                    return ConvertFromString(response);
                }
                catch (Exception ex)
                {
                    Log.write(ex);
                    return null;
                }
            }

            public static APIResponseData Delete(List<Guid> list)
            {
                try
                {
                    string url = CreateRequestUrl("loaithutuc_noidung");
                    APIRequestData requestData = new APIRequestData()
                    {
                        Action = "delete",
                        Data = JsonConvert.SerializeObject(list)
                    };
                    string response = Decided.Libs.WebUtils.Request_POST(url, JsonConvert.SerializeObject(requestData));

                    if (string.IsNullOrEmpty(response)) return null;

                    return ConvertFromString(response);
                }
                catch (Exception ex)
                {
                    Log.write(ex);
                    return null;
                }
            }
        }

        #endregion

        #region ThutucNhiemvu_Truongdulieu

        public class ThutucNhiemvu_Truongdulieu
        {
            public static List<DM_LoaiThutucNhiemvu_Truongdulieu> GetList(Guid parentId)
            {
                try
                {
                    string url = CreateRequestUrl("loaithutuc_truongdulieu");
                    APIRequestData requestData = new APIRequestData()
                    {
                        Action = "getlist_children",
                        Data = parentId.ToString()
                    };
                    string response = Decided.Libs.WebUtils.Request_POST(url, JsonConvert.SerializeObject(requestData));

                    if (string.IsNullOrEmpty(response)) return null;

                    APIResponseData result = ConvertFromString(response);
                    if (result == null || result.ErrorCode != 0) return null;

                    return result.Data == null ? null : JsonConvert.DeserializeObject<List<DM_LoaiThutucNhiemvu_Truongdulieu>>(result.Data.ToString());
                }
                catch (Exception ex)
                {
                    Log.write(ex);
                    return null;
                }
            }

            public static List<DM_LoaiThutucNhiemvu_Truongdulieu> GetList_Root(Guid noidungId)
            {
                try
                {
                    string url = CreateRequestUrl("loaithutuc_truongdulieu");
                    APIRequestData requestData = new APIRequestData()
                    {
                        Action = "getlist_root",
                        Data = noidungId.ToString()
                    };
                    string response = Decided.Libs.WebUtils.Request_POST(url, JsonConvert.SerializeObject(requestData));

                    if (string.IsNullOrEmpty(response)) return null;

                    APIResponseData result = ConvertFromString(response);
                    if (result == null || result.ErrorCode != 0) return null;

                    return result.Data == null ? null : JsonConvert.DeserializeObject<List<DM_LoaiThutucNhiemvu_Truongdulieu>>(result.Data.ToString());
                }
                catch (Exception ex)
                {
                    Log.write(ex);
                    return null;
                }
            }

            public static List<DM_LoaiThutucNhiemvu_Truongdulieu> GetList()
            {
                try
                {
                    string url = CreateRequestUrl("loaithutuc_truongdulieu");
                    APIRequestData requestData = new APIRequestData()
                    {
                        Action = "getlist",
                        Data = null
                    };
                    string response = Decided.Libs.WebUtils.Request_POST(url, JsonConvert.SerializeObject(requestData));

                    if (string.IsNullOrEmpty(response)) return null;

                    APIResponseData result = ConvertFromString(response);
                    if (result == null || result.ErrorCode != 0) return null;

                    return result.Data == null ? null : JsonConvert.DeserializeObject<List<DM_LoaiThutucNhiemvu_Truongdulieu>>(result.Data.ToString());
                }
                catch (Exception ex)
                {
                    Log.write(ex);
                    return null;
                }
            }

            public static List<DM_LoaiThutucNhiemvu_Truongdulieu> GetListCanChildren()
            {
                try
                {
                    string url = CreateRequestUrl("loaithutuc_truongdulieu");
                    APIRequestData requestData = new APIRequestData()
                    {
                        Action = "getlistcanchildren",
                        Data = null
                    };
                    string response = Decided.Libs.WebUtils.Request_POST(url, JsonConvert.SerializeObject(requestData));

                    if (string.IsNullOrEmpty(response)) return null;

                    APIResponseData result = ConvertFromString(response);
                    if (result == null || result.ErrorCode != 0) return null;

                    return result.Data == null ? null : JsonConvert.DeserializeObject<List<DM_LoaiThutucNhiemvu_Truongdulieu>>(result.Data.ToString());
                }
                catch (Exception ex)
                {
                    Log.write(ex);
                    return null;
                }
            }

            public static List<string> GetListTables()
            {
                try
                {
                    string url = CreateRequestUrl("loaithutuc_truongdulieu");
                    APIRequestData requestData = new APIRequestData()
                    {
                        Action = "getlisttables",
                        Data = null
                    };
                    string response = Decided.Libs.WebUtils.Request_POST(url, JsonConvert.SerializeObject(requestData));

                    if (string.IsNullOrEmpty(response)) return null;

                    APIResponseData result = ConvertFromString(response);
                    if (result == null || result.ErrorCode != 0) return null;

                    return result.Data == null ? null : JsonConvert.DeserializeObject<List<string>>(result.Data.ToString());
                }
                catch (Exception ex)
                {
                    Log.write(ex);
                    return null;
                }
            }

            public static List<string> GetListTableColumns(string tableName)
            {
                try
                {
                    string url = CreateRequestUrl("loaithutuc_truongdulieu");
                    APIRequestData requestData = new APIRequestData()
                    {
                        Action = "getlisttablecolumns",
                        Data = tableName
                    };
                    string response = Decided.Libs.WebUtils.Request_POST(url, JsonConvert.SerializeObject(requestData));

                    if (string.IsNullOrEmpty(response)) return null;

                    APIResponseData result = ConvertFromString(response);
                    if (result == null || result.ErrorCode != 0) return null;

                    return result.Data == null ? null : JsonConvert.DeserializeObject<List<string>>(result.Data.ToString());
                }
                catch (Exception ex)
                {
                    Log.write(ex);
                    return null;
                }
            }

            public static APIResponseData Create(DM_LoaiThutucNhiemvu_Truongdulieu obj)
            {
                try
                {
                    string url = CreateRequestUrl("loaithutuc_truongdulieu");
                    APIRequestData requestData = new APIRequestData()
                    {
                        Action = "create",
                        Data = JsonConvert.SerializeObject(obj)
                    };
                    string response = Decided.Libs.WebUtils.Request_POST(url, JsonConvert.SerializeObject(requestData));

                    if (string.IsNullOrEmpty(response)) return null;

                    return ConvertFromString(response);
                }
                catch (Exception ex)
                {
                    Log.write(ex);
                    return null;
                }
            }

            public static APIResponseData Update(DM_LoaiThutucNhiemvu_Truongdulieu obj)
            {
                try
                {
                    string url = CreateRequestUrl("loaithutuc_truongdulieu");
                    APIRequestData requestData = new APIRequestData()
                    {
                        Action = "update",
                        Data = JsonConvert.SerializeObject(obj)
                    };
                    string response = Decided.Libs.WebUtils.Request_POST(url, JsonConvert.SerializeObject(requestData));

                    if (string.IsNullOrEmpty(response)) return null;

                    return ConvertFromString(response);
                }
                catch (Exception ex)
                {
                    Log.write(ex);
                    return null;
                }
            }

            public static APIResponseData Delete(List<Guid> list)
            {
                try
                {
                    string url = CreateRequestUrl("loaithutuc_truongdulieu");
                    APIRequestData requestData = new APIRequestData()
                    {
                        Action = "delete",
                        Data = JsonConvert.SerializeObject(list)
                    };
                    string response = Decided.Libs.WebUtils.Request_POST(url, JsonConvert.SerializeObject(requestData));

                    if (string.IsNullOrEmpty(response)) return null;

                    return ConvertFromString(response);
                }
                catch (Exception ex)
                {
                    Log.write(ex);
                    return null;
                }
            }
        }

        #endregion

        #region Trinhduyet

        public class Trinhduyet
        {
            public static List<TD_Capbanhanh> GetList_Capbanhanh(Guid donviID)
            {
                try
                {
                    string url = CreateRequestUrl("trinhduyet_thuchiennhiemvu");
                    APIRequestData requestData = new APIRequestData()
                    {
                        Action = "getlist_capbanhanh",
                        Data = donviID.ToString()
                    };
                    string response = Decided.Libs.WebUtils.Request_POST(url, JsonConvert.SerializeObject(requestData));

                    if (string.IsNullOrEmpty(response)) return null;

                    APIResponseData result = ConvertFromString(response);
                    if (result == null || result.ErrorCode != 0) return null;

                    return result.Data == null ? null : JsonConvert.DeserializeObject<List<TD_Capbanhanh>>(result.Data.ToString());
                }
                catch (Exception ex)
                {
                    Log.write(ex);
                    return null;
                }
            }
            public static List<TD_TrangthaiHoSo> GetList_TrangthaiHoSo()
            {
                try
                {
                    string url = CreateRequestUrl("trinhduyet_thuchiennhiemvu");
                    APIRequestData requestData = new APIRequestData()
                    {
                        Action = "getlist_trangthaihoso",
                        Data = null
                    };
                    string response = Decided.Libs.WebUtils.Request_POST(url, JsonConvert.SerializeObject(requestData));

                    if (string.IsNullOrEmpty(response)) return null;

                    APIResponseData result = ConvertFromString(response);
                    if (result == null || result.ErrorCode != 0) return null;

                    return result.Data == null ? null : JsonConvert.DeserializeObject<List<TD_TrangthaiHoSo>>(result.Data.ToString());
                }
                catch (Exception ex)
                {
                    Log.write(ex);
                    return null;
                }
            }

            public static List<TD_DonviQuanly> GetList_DonviQuanly()
            {
                try
                {
                    string url = CreateRequestUrl("trinhduyet_thuchiennhiemvu");
                    APIRequestData requestData = new APIRequestData()
                    {
                        Action = "getlist_donviquanly",
                        Data = null
                    };
                    string response = Decided.Libs.WebUtils.Request_POST(url, JsonConvert.SerializeObject(requestData));

                    if (string.IsNullOrEmpty(response)) return null;

                    APIResponseData result = ConvertFromString(response);
                    if (result == null || result.ErrorCode != 0) return null;

                    return result.Data == null ? null : JsonConvert.DeserializeObject<List<TD_DonviQuanly>>(result.Data.ToString());
                }
                catch (Exception ex)
                {
                    Log.write(ex);
                    return null;
                }
            }

            public static List<TD_Nguoiky> GetList_Nhansu(Guid ngSudungId, char phamviId, char thamquyenId)
            {
                try
                {
                    string url = CreateRequestUrl("trinhduyet_thuchiennhiemvu");
                    APIRequestData requestData = new APIRequestData()
                    {
                        Action = "getlist_nhansu",
                        Data = JsonConvert.SerializeObject(new List<string>(){
                            ngSudungId.ToString(),
                            phamviId.ToString(),
                            thamquyenId.ToString()
                        })
                    };
                    string response = Decided.Libs.WebUtils.Request_POST(url, JsonConvert.SerializeObject(requestData));

                    if (string.IsNullOrEmpty(response)) return null;

                    APIResponseData result = ConvertFromString(response);
                    if (result == null || result.ErrorCode != 0) return null;

                    return result.Data == null ? null : JsonConvert.DeserializeObject<List<TD_Nguoiky>>(result.Data.ToString());
                }
                catch (Exception ex)
                {
                    Log.write(ex);
                    return null;
                }
            }
        }

        #endregion

        #region Trinhduyet_ThuchienNhiemvu

        public class TrinhduyetThuchienNhiemvu
        {
            public static List<TD_ThuchienNhiemvu_PhanloaiNhiemvu> GetList_PhanloaiNhiemvu()
            {
                try
                {
                    string url = CreateRequestUrl("trinhduyet_thuchiennhiemvu");
                    APIRequestData requestData = new APIRequestData()
                    {
                        Action = "getlist_phanloainhiemvu",
                        Data = null
                    };
                    string response = Decided.Libs.WebUtils.Request_POST(url, JsonConvert.SerializeObject(requestData));

                    if (string.IsNullOrEmpty(response)) return null;

                    APIResponseData result = ConvertFromString(response);
                    if (result == null || result.ErrorCode != 0) return null;

                    return result.Data == null ? null : JsonConvert.DeserializeObject<List<TD_ThuchienNhiemvu_PhanloaiNhiemvu>>(result.Data.ToString());
                }
                catch (Exception ex)
                {
                    Log.write(ex);
                    return null;
                }
            }

            public static List<TD_ThuchienNhiemvu_Truongdulieu> GetList_Truongdulieu(Guid noidungId, Guid tdnhiemvuId)
            {
                try
                {
                    string url = CreateRequestUrl("trinhduyet_thuchiennhiemvu");
                    APIRequestData requestData = new APIRequestData()
                    {
                        Action = "getlist_truongdulieu",
                        Data = JsonConvert.SerializeObject(new List<Guid>() { noidungId, tdnhiemvuId })
                    };
                    string response = Decided.Libs.WebUtils.Request_POST(url, JsonConvert.SerializeObject(requestData));

                    if (string.IsNullOrEmpty(response)) return null;

                    APIResponseData result = ConvertFromString(response);
                    if (result == null || result.ErrorCode != 0) return null;

                    return result.Data == null ? null : JsonConvert.DeserializeObject<List<TD_ThuchienNhiemvu_Truongdulieu>>(result.Data.ToString());
                }
                catch (Exception ex)
                {
                    Log.write(ex);
                    return null;
                }
            }

            public static APIResponseData Create(TD_ThuchienNhiemvu obj)
            {
                try
                {
                    string url = CreateRequestUrl("trinhduyet_thuchiennhiemvu");
                    APIRequestData requestData = new APIRequestData()
                    {
                        Action = "create",
                        Data = JsonConvert.SerializeObject(obj)
                    };
                    string response = Decided.Libs.WebUtils.Request_POST(url, JsonConvert.SerializeObject(requestData));

                    if (string.IsNullOrEmpty(response)) return null;

                    return ConvertFromString(response);
                }
                catch (Exception ex)
                {
                    Log.write(ex);
                    return null;
                }
            }

            public static APIResponseData Update(TD_ThuchienNhiemvu obj)
            {
                try
                {
                    string url = CreateRequestUrl("trinhduyet_thuchiennhiemvu");
                    APIRequestData requestData = new APIRequestData()
                    {
                        Action = "update",
                        Data = JsonConvert.SerializeObject(obj)
                    };
                    string response = Decided.Libs.WebUtils.Request_POST(url, JsonConvert.SerializeObject(requestData));

                    if (string.IsNullOrEmpty(response)) return null;

                    return ConvertFromString(response);
                }
                catch (Exception ex)
                {
                    Log.write(ex);
                    return null;
                }
            }

            public static APIResponseData Delete(List<Guid> list)
            {
                try
                {
                    string url = CreateRequestUrl("trinhduyet_thuchiennhiemvu");
                    APIRequestData requestData = new APIRequestData()
                    {
                        Action = "delete",
                        Data = JsonConvert.SerializeObject(list)
                    };
                    string response = Decided.Libs.WebUtils.Request_POST(url, JsonConvert.SerializeObject(requestData));

                    if (string.IsNullOrEmpty(response)) return null;

                    return ConvertFromString(response);
                }
                catch (Exception ex)
                {
                    Log.write(ex);
                    return null;
                }
            }

            public static List<TD_ThuchienNhiemvu> GetList(TD_ThuchienNhiemvu_Filter filter)
            {
                try
                {
                    string url = CreateRequestUrl("trinhduyet_thuchiennhiemvu");
                    APIRequestData requestData = new APIRequestData()
                    {
                        Action = "getlist",
                        Data = JsonConvert.SerializeObject(filter)
                    };
                    string response = Decided.Libs.WebUtils.Request_POST(url, JsonConvert.SerializeObject(requestData));

                    if (string.IsNullOrEmpty(response)) return null;

                    APIResponseData result = ConvertFromString(response);
                    if (result == null || result.ErrorCode != 0) return null;

                    return result.Data == null ? null : JsonConvert.DeserializeObject<List<TD_ThuchienNhiemvu>>(result.Data.ToString());
                }
                catch (Exception ex)
                {
                    Log.write(ex);
                    return null;
                }
            }

            public static List<TD_ThuchienNhiemvu> GetList_For_Phancong(TD_Phancong_Filter filter)
            {
                try
                {
                    string url = CreateRequestUrl("trinhduyet_thuchiennhiemvu");
                    APIRequestData requestData = new APIRequestData()
                    {
                        Action = "getlist_for_phancong",
                        Data = JsonConvert.SerializeObject(filter)
                    };
                    string response = Decided.Libs.WebUtils.Request_POST(url, JsonConvert.SerializeObject(requestData));

                    if (string.IsNullOrEmpty(response)) return null;

                    APIResponseData result = ConvertFromString(response);
                    if (result == null || result.ErrorCode != 0) return null;

                    return result.Data == null ? null : JsonConvert.DeserializeObject<List<TD_ThuchienNhiemvu>>(result.Data.ToString());
                }
                catch (Exception ex)
                {
                    Log.write(ex);
                    return null;
                }
            }

            public static List<TD_ThuchienNhiemvu> GetList_For_Thamdinh(TD_Thamdinh_Filter filter)
            {
                try
                {
                    string url = CreateRequestUrl("trinhduyet_thuchiennhiemvu");
                    APIRequestData requestData = new APIRequestData()
                    {
                        Action = "getlist_for_thamdinh",
                        Data = JsonConvert.SerializeObject(filter)
                    };
                    string response = Decided.Libs.WebUtils.Request_POST(url, JsonConvert.SerializeObject(requestData));

                    if (string.IsNullOrEmpty(response)) return null;

                    APIResponseData result = ConvertFromString(response);
                    if (result == null || result.ErrorCode != 0) return null;

                    return result.Data == null ? null : JsonConvert.DeserializeObject<List<TD_ThuchienNhiemvu>>(result.Data.ToString());
                }
                catch (Exception ex)
                {
                    Log.write(ex);
                    return null;
                }
            }

            public static List<TD_ThuchienNhiemvu> GetList_For_PheduyetThamdinh(TD_Pheduyet_Thamdinh_Filter filter)
            {
                try
                {
                    string url = CreateRequestUrl("trinhduyet_thuchiennhiemvu");
                    APIRequestData requestData = new APIRequestData()
                    {
                        Action = "getlist_for_pheduyetthamdinh",
                        Data = JsonConvert.SerializeObject(filter)
                    };
                    string response = Decided.Libs.WebUtils.Request_POST(url, JsonConvert.SerializeObject(requestData));

                    if (string.IsNullOrEmpty(response)) return null;

                    APIResponseData result = ConvertFromString(response);
                    if (result == null || result.ErrorCode != 0) return null;

                    return result.Data == null ? null : JsonConvert.DeserializeObject<List<TD_ThuchienNhiemvu>>(result.Data.ToString());
                }
                catch (Exception ex)
                {
                    Log.write(ex);
                    return null;
                }
            }

            public static List<TD_ThuchienNhiemvu_Cothienthi> GenerateListColumns()
            {
                try
                {
                    PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(TD_ThuchienNhiemvu));
                    List<TD_ThuchienNhiemvu_Cothienthi> result = new List<TD_ThuchienNhiemvu_Cothienthi>();
                    for (int i = 0; i < props.Count; i++)
                    {
                        PropertyDescriptor prop = props[i];
                        result.Add(
                            new TD_ThuchienNhiemvu_Cothienthi()
                            {
                                IsChecked = false,
                                ColumnName = prop.Name,
                                DisplayName = string.Empty
                            });
                    }

                    return result.Count == 0 ? null : result;
                }
                catch
                {
                    return null;
                }
            }
        }

        #endregion

        #region Trinhduyet_Phancong

        public class TrinhDuyetPhancong
        {
            public static APIResponseData Create(TD_Phancong obj)
            {
                try
                {
                    string url = CreateRequestUrl("trinhduyet_phancong");
                    APIRequestData requestData = new APIRequestData()
                    {
                        Action = "create",
                        Data = JsonConvert.SerializeObject(obj)
                    };
                    string response = Decided.Libs.WebUtils.Request_POST(url, JsonConvert.SerializeObject(requestData));

                    if (string.IsNullOrEmpty(response)) return null;

                    return ConvertFromString(response);
                }
                catch (Exception ex)
                {
                    Log.write(ex);
                    return null;
                }
            }

            public static APIResponseData Update(TD_Phancong obj)
            {
                try
                {
                    string url = CreateRequestUrl("trinhduyet_phancong");
                    APIRequestData requestData = new APIRequestData()
                    {
                        Action = "update",
                        Data = JsonConvert.SerializeObject(obj)
                    };
                    string response = Decided.Libs.WebUtils.Request_POST(url, JsonConvert.SerializeObject(requestData));

                    if (string.IsNullOrEmpty(response)) return null;

                    return ConvertFromString(response);
                }
                catch (Exception ex)
                {
                    Log.write(ex);
                    return null;
                }
            }

            public static APIResponseData Delete(List<Guid> list)
            {
                try
                {
                    string url = CreateRequestUrl("trinhduyet_phancong");
                    APIRequestData requestData = new APIRequestData()
                    {
                        Action = "delete",
                        Data = JsonConvert.SerializeObject(list)
                    };
                    string response = Decided.Libs.WebUtils.Request_POST(url, JsonConvert.SerializeObject(requestData));

                    if (string.IsNullOrEmpty(response)) return null;

                    return ConvertFromString(response);
                }
                catch (Exception ex)
                {
                    Log.write(ex);
                    return null;
                }
            }
        }

        #endregion

        #region LoaiThutucTrinhDuyet
        public class LoaiThutucTrinhDuyet
        {
            public static List<DM_LoaiThutucTrinhduyet> GetList()
            {
                try
                {
                    string url = CreateRequestUrl("loaithutuctrinhduyet");
                    Log.debug(url);
                    APIRequestData requestData = new APIRequestData()
                    {
                        Action = "getlist",
                        Data = null
                    };
                    string response = Decided.Libs.WebUtils.Request_POST(url, JsonConvert.SerializeObject(requestData));

                    if (string.IsNullOrEmpty(response)) return null;

                    APIResponseData result = ConvertFromString(response);
                    if (result == null || result.ErrorCode != 0) return null;

                    return result.Data == null ? null : JsonConvert.DeserializeObject<List<DM_LoaiThutucTrinhduyet>>(result.Data.ToString());
                }
                catch (Exception ex)
                {
                    Log.write(ex);
                    return null;
                }
            }
        }
        #endregion

        #region Thongbao
        public class ThongBao
        {
            public static List<DM_ThongBao> GetList()
            {
                try
                {
                    string url = CreateRequestUrl("thongbao");
                    Log.debug(url);
                    APIRequestData requestData = new APIRequestData()
                    {
                        Action = "getlist",
                        Data = null
                    };
                    string response = Decided.Libs.WebUtils.Request_POST(url, JsonConvert.SerializeObject(requestData));

                    if (string.IsNullOrEmpty(response)) return null;

                    APIResponseData result = ConvertFromString(response);
                    if (result == null || result.ErrorCode != 0) return null;

                    return result.Data == null ? null : JsonConvert.DeserializeObject<List<DM_ThongBao>>(result.Data.ToString());
                }
                catch (Exception ex)
                {
                    Log.write(ex);
                    return null;
                }
            }
        }
        #endregion

        #region Trinhduyet_Thamdinh

        public class TrinhduyetThamdinh
        {
            public static TD_Thamdinh_Duyet Get_DuyetThamdinh(TD_Thamdinh_Duyet_FilterOne filter)
            {
                try
                {
                    string url = CreateRequestUrl("trinhduyet_thamdinh");
                    APIRequestData requestData = new APIRequestData()
                    {
                        Action = "get_duyetthamdinh",
                        Data = JsonConvert.SerializeObject(filter)
                    };
                    string response = Decided.Libs.WebUtils.Request_POST(url, JsonConvert.SerializeObject(requestData));

                    if (string.IsNullOrEmpty(response)) return null;

                    APIResponseData result = ConvertFromString(response);
                    if (result == null || result.ErrorCode != 0) return null;

                    return result.Data == null ? null : JsonConvert.DeserializeObject<TD_Thamdinh_Duyet>(result.Data.ToString());
                }
                catch (Exception ex)
                {
                    Log.write(ex);
                    return null;
                }
            }
            public static List<TD_Thamdinh_Duyet_Truongdulieu> GetList_Truongdulieu(Guid noidungId, Guid tdDuyetId, Guid tdNhiemvuId)
            {
                try
                {
                    string url = CreateRequestUrl("trinhduyet_thamdinh");
                    APIRequestData requestData = new APIRequestData()
                    {
                        Action = "getlist_truongdulieu",
                        Data = JsonConvert.SerializeObject(new List<Guid>() { noidungId, tdDuyetId, tdNhiemvuId })
                    };
                    string response = Decided.Libs.WebUtils.Request_POST(url, JsonConvert.SerializeObject(requestData));

                    if (string.IsNullOrEmpty(response)) return null;

                    APIResponseData result = ConvertFromString(response);
                    if (result == null || result.ErrorCode != 0) return null;

                    return result.Data == null ? null : JsonConvert.DeserializeObject<List<TD_Thamdinh_Duyet_Truongdulieu>>(result.Data.ToString());
                }
                catch (Exception ex)
                {
                    Log.write(ex);
                    return null;
                }
            }

            public static List<TD_Thamdinh_Duyet> GetList_Duyet(TD_Thamdinh_Duyet_Filter filter)
            {
                try
                {
                    string url = CreateRequestUrl("trinhduyet_thamdinh");
                    APIRequestData requestData = new APIRequestData()
                    {
                        Action = "getlist_duyet",
                        Data = JsonConvert.SerializeObject(filter)
                    };
                    string response = Decided.Libs.WebUtils.Request_POST(url, JsonConvert.SerializeObject(requestData));

                    if (string.IsNullOrEmpty(response)) return null;

                    APIResponseData result = ConvertFromString(response);
                    if (result == null || result.ErrorCode != 0) return null;

                    return result.Data == null ? null : JsonConvert.DeserializeObject<List<TD_Thamdinh_Duyet>>(result.Data.ToString());
                }
                catch (Exception ex)
                {
                    Log.write(ex);
                    return null;
                }
            }

            //public static List<TD_Thamdinh_Duyet_Truongdulieu> GetList_Duyet_Truongdulieu(Guid noidungId, Guid tdnhiemvuId)
            //{
            //    try
            //    {
            //        string url = CreateRequestUrl("trinhduyet_thuchiennhiemvu");
            //        APIRequestData requestData = new APIRequestData()
            //        {
            //            Action = "getlist_truongdulieu",
            //            Data = JsonConvert.SerializeObject(new List<Guid>() { noidungId, tdnhiemvuId })
            //        };
            //        string response = Decided.Libs.WebUtils.Request_POST(url, JsonConvert.SerializeObject(requestData));

            //        if (string.IsNullOrEmpty(response)) return null;

            //        APIResponseData result = ConvertFromString(response);
            //        if (result == null || result.ErrorCode != 0) return null;

            //        return result.Data == null ? null : JsonConvert.DeserializeObject<List<TD_ThuchienNhiemvu_Truongdulieu>>(result.Data.ToString());
            //    }
            //    catch (Exception ex)
            //    {
            //        Log.write(ex);
            //        return null;
            //    }
            //}

            public static APIResponseData Create_Duyet(TD_Thamdinh_Duyet obj)
            {
                try
                {
                    string url = CreateRequestUrl("trinhduyet_thamdinh");
                    APIRequestData requestData = new APIRequestData()
                    {
                        Action = "create_duyet",
                        Data = JsonConvert.SerializeObject(obj)
                    };
                    string response = Decided.Libs.WebUtils.Request_POST(url, JsonConvert.SerializeObject(requestData));

                    if (string.IsNullOrEmpty(response)) return null;

                    return ConvertFromString(response);
                }
                catch (Exception ex)
                {
                    Log.write(ex);
                    return null;
                }
            }

            public static APIResponseData Update_Duyet(TD_Thamdinh_Duyet obj)
            {
                try
                {
                    string url = CreateRequestUrl("trinhduyet_thamdinh");
                    APIRequestData requestData = new APIRequestData()
                    {
                        Action = "update_duyet",
                        Data = JsonConvert.SerializeObject(obj)
                    };
                    string response = Decided.Libs.WebUtils.Request_POST(url, JsonConvert.SerializeObject(requestData));

                    if (string.IsNullOrEmpty(response)) return null;

                    return ConvertFromString(response);
                }
                catch (Exception ex)
                {
                    Log.write(ex);
                    return null;
                }
            }

            public static APIResponseData Delete_Duyet(List<Guid> list)
            {
                try
                {
                    string url = CreateRequestUrl("trinhduyet_thamdinh");
                    APIRequestData requestData = new APIRequestData()
                    {
                        Action = "delete_duyet",
                        Data = JsonConvert.SerializeObject(list)
                    };
                    string response = Decided.Libs.WebUtils.Request_POST(url, JsonConvert.SerializeObject(requestData));

                    if (string.IsNullOrEmpty(response)) return null;

                    return ConvertFromString(response);
                }
                catch (Exception ex)
                {
                    Log.write(ex);
                    return null;
                }
            }
        }

        #endregion

        #region Trinhduyet_Pheduyet

        public class TrinhduyetPheduyetTD
        {
            public static byte GetCurrentID()
            {
                try
                {
                    string url = CreateRequestUrl("trinhduyet_pheduyet_vb");
                    APIRequestData requestData = new APIRequestData()
                    {
                        Action = "get_currentid_thamdinh",
                        Data = null
                    };
                    string response = Decided.Libs.WebUtils.Request_POST(url, JsonConvert.SerializeObject(requestData));

                    if (string.IsNullOrEmpty(response)) return 0;

                    APIResponseData result = ConvertFromString(response);
                    if (result == null || result.ErrorCode != 0) return 0;

                    return result.Data == null ? (byte)0 : byte.Parse(result.Data.ToString());
                }
                catch (Exception ex)
                {
                    Log.write(ex);
                    return 0;
                }
            }

            public static List<TD_Pheduyet_Thamdinh_Duyet_Truongdulieu> GetList_Truongdulieu(Guid noidungId, Guid pheduyetId)
            {
                try
                {
                    string url = CreateRequestUrl("trinhduyet_pheduyet_vb");
                    APIRequestData requestData = new APIRequestData()
                    {
                        Action = "getlist_truongdulieu_thamdinh",
                        Data = JsonConvert.SerializeObject(new List<Guid>() { noidungId, pheduyetId })
                    };
                    string response = Decided.Libs.WebUtils.Request_POST(url, JsonConvert.SerializeObject(requestData));

                    if (string.IsNullOrEmpty(response)) return null;

                    APIResponseData result = ConvertFromString(response);
                    if (result == null || result.ErrorCode != 0) return null;

                    return result.Data == null ? null : JsonConvert.DeserializeObject<List<TD_Pheduyet_Thamdinh_Duyet_Truongdulieu>>(result.Data.ToString());
                }
                catch (Exception ex)
                {
                    Log.write(ex);
                    return null;
                }
            }

            public static List<TD_Pheduyet_Thamdinh_Duyet> GetList(TD_Pheduyet_Thamdinh_Duyet_Filter filter)
            {
                try
                {
                    string url = CreateRequestUrl("trinhduyet_pheduyet_vb");
                    APIRequestData requestData = new APIRequestData()
                    {
                        Action = "getlist_thamdinh",
                        Data = JsonConvert.SerializeObject(filter)
                    };
                    string response = Decided.Libs.WebUtils.Request_POST(url, JsonConvert.SerializeObject(requestData));

                    if (string.IsNullOrEmpty(response)) return null;

                    APIResponseData result = ConvertFromString(response);
                    if (result == null || result.ErrorCode != 0) return null;

                    return result.Data == null ? null : JsonConvert.DeserializeObject<List<TD_Pheduyet_Thamdinh_Duyet>>(result.Data.ToString());
                }
                catch (Exception ex)
                {
                    Log.write(ex);
                    return null;
                }
            }

            public static APIResponseData Create(TD_Pheduyet_Thamdinh_Duyet obj)
            {
                try
                {
                    string url = CreateRequestUrl("trinhduyet_pheduyet_vb");
                    APIRequestData requestData = new APIRequestData()
                    {
                        Action = "create_thamdinh",
                        Data = JsonConvert.SerializeObject(obj)
                    };
                    string response = Decided.Libs.WebUtils.Request_POST(url, JsonConvert.SerializeObject(requestData));

                    if (string.IsNullOrEmpty(response)) return null;

                    return ConvertFromString(response);
                }
                catch (Exception ex)
                {
                    Log.write(ex);
                    return null;
                }
            }

            public static APIResponseData Update(TD_Pheduyet_Thamdinh_Duyet obj)
            {
                try
                {
                    string url = CreateRequestUrl("trinhduyet_pheduyet_vb");
                    APIRequestData requestData = new APIRequestData()
                    {
                        Action = "update_thamdinh",
                        Data = JsonConvert.SerializeObject(obj)
                    };
                    string response = Decided.Libs.WebUtils.Request_POST(url, JsonConvert.SerializeObject(requestData));

                    if (string.IsNullOrEmpty(response)) return null;

                    return ConvertFromString(response);
                }
                catch (Exception ex)
                {
                    Log.write(ex);
                    return null;
                }
            }

            public static APIResponseData Delete(List<Guid> list)
            {
                try
                {
                    string url = CreateRequestUrl("trinhduyet_pheduyet_vb");
                    APIRequestData requestData = new APIRequestData()
                    {
                        Action = "delete_thamdinh",
                        Data = JsonConvert.SerializeObject(list)
                    };
                    string response = Decided.Libs.WebUtils.Request_POST(url, JsonConvert.SerializeObject(requestData));

                    if (string.IsNullOrEmpty(response)) return null;

                    return ConvertFromString(response);
                }
                catch (Exception ex)
                {
                    Log.write(ex);
                    return null;
                }
            }
        }

        public class TrinhduyetPheduyetVB
        {
            public static byte GetCurrentID()
            {
                try
                {
                    string url = CreateRequestUrl("trinhduyet_pheduyet_vb");
                    APIRequestData requestData = new APIRequestData()
                    {
                        Action = "get_currentid",
                        Data = null
                    };
                    string response = Decided.Libs.WebUtils.Request_POST(url, JsonConvert.SerializeObject(requestData));

                    if (string.IsNullOrEmpty(response)) return 0;

                    APIResponseData result = ConvertFromString(response);
                    if (result == null || result.ErrorCode != 0) return 0;

                    return result.Data == null ? (byte)0 : byte.Parse(result.Data.ToString());
                }
                catch (Exception ex)
                {
                    Log.write(ex);
                    return 0;
                }
            }

            public static List<TD_Pheduyet_VB_Truongdulieu> GetList_Truongdulieu(Guid noidungId, Guid pheduyetId)
            {
                try
                {
                    string url = CreateRequestUrl("trinhduyet_pheduyet_vb");
                    APIRequestData requestData = new APIRequestData()
                    {
                        Action = "getlist_truongdulieu",
                        Data = JsonConvert.SerializeObject(new List<Guid>() { noidungId, pheduyetId })
                    };
                    string response = Decided.Libs.WebUtils.Request_POST(url, JsonConvert.SerializeObject(requestData));

                    if (string.IsNullOrEmpty(response)) return null;

                    APIResponseData result = ConvertFromString(response);
                    if (result == null || result.ErrorCode != 0) return null;

                    return result.Data == null ? null : JsonConvert.DeserializeObject<List<TD_Pheduyet_VB_Truongdulieu>>(result.Data.ToString());
                }
                catch (Exception ex)
                {
                    Log.write(ex);
                    return null;
                }
            }

            public static List<TD_Pheduyet_VB> GetList(TD_Pheduyet_VB_Filter filter)
            {
                try
                {
                    string url = CreateRequestUrl("trinhduyet_pheduyet_vb");
                    APIRequestData requestData = new APIRequestData()
                    {
                        Action = "getlist",
                        Data = JsonConvert.SerializeObject(filter)
                    };
                    string response = Decided.Libs.WebUtils.Request_POST(url, JsonConvert.SerializeObject(requestData));

                    if (string.IsNullOrEmpty(response)) return null;

                    APIResponseData result = ConvertFromString(response);
                    if (result == null || result.ErrorCode != 0) return null;

                    return result.Data == null ? null : JsonConvert.DeserializeObject<List<TD_Pheduyet_VB>>(result.Data.ToString());
                }
                catch (Exception ex)
                {
                    Log.write(ex);
                    return null;
                }
            }

            public static APIResponseData Create(TD_Pheduyet_VB obj)
            {
                try
                {
                    string url = CreateRequestUrl("trinhduyet_pheduyet_vb");
                    APIRequestData requestData = new APIRequestData()
                    {
                        Action = "create",
                        Data = JsonConvert.SerializeObject(obj)
                    };
                    string response = Decided.Libs.WebUtils.Request_POST(url, JsonConvert.SerializeObject(requestData));

                    if (string.IsNullOrEmpty(response)) return null;

                    return ConvertFromString(response);
                }
                catch (Exception ex)
                {
                    Log.write(ex);
                    return null;
                }
            }

            public static APIResponseData Update(TD_Pheduyet_VB obj)
            {
                try
                {
                    string url = CreateRequestUrl("trinhduyet_pheduyet_vb");
                    APIRequestData requestData = new APIRequestData()
                    {
                        Action = "update",
                        Data = JsonConvert.SerializeObject(obj)
                    };
                    string response = Decided.Libs.WebUtils.Request_POST(url, JsonConvert.SerializeObject(requestData));

                    if (string.IsNullOrEmpty(response)) return null;

                    return ConvertFromString(response);
                }
                catch (Exception ex)
                {
                    Log.write(ex);
                    return null;
                }
            }

            public static APIResponseData Delete(List<Guid> list)
            {
                try
                {
                    string url = CreateRequestUrl("trinhduyet_pheduyet_vb");
                    APIRequestData requestData = new APIRequestData()
                    {
                        Action = "delete",
                        Data = JsonConvert.SerializeObject(list)
                    };
                    string response = Decided.Libs.WebUtils.Request_POST(url, JsonConvert.SerializeObject(requestData));

                    if (string.IsNullOrEmpty(response)) return null;

                    return ConvertFromString(response);
                }
                catch (Exception ex)
                {
                    Log.write(ex);
                    return null;
                }
            }
        }

        #endregion

        #region KyhieuVB

        public class Trinhduyet_KyhieuVB
        {
            public static List<Kyhieuvanban> GetList()
            {
                try
                {
                    string url = CreateRequestUrl("trinhduyet_thuchiennhiemvu");
                    APIRequestData requestData = new APIRequestData()
                    {
                        Action = "getlist_kyhieuvb",
                        Data = null
                    };
                    string response = Decided.Libs.WebUtils.Request_POST(url, JsonConvert.SerializeObject(requestData));

                    if (string.IsNullOrEmpty(response)) return null;

                    APIResponseData result = ConvertFromString(response);
                    if (result == null || result.ErrorCode != 0) return null;

                    return result.Data == null ? null : JsonConvert.DeserializeObject<List<Kyhieuvanban>>(result.Data.ToString());
                }
                catch (Exception ex)
                {
                    Log.write(ex);
                    return null;
                }
            }
        }

        #endregion
    }
}
