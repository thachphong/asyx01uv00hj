using Newtonsoft.Json;
using QLNhiemVu;
using QLNhiemvu_DBEntities;
using System;
using System.Collections.Generic;
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
            return DefineValues.API_Host + module +"?u=" + DefineValues.API_Authentication_User + "&p=" + DefineValues.API_Authentication_Password;
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
            public static List<DM_LoaiThutucNhiemvu> GetList()
            {
                try
                {
                    string url = CreateRequestUrl("loaithutuc");
                    APIRequestData requestData = new APIRequestData()
                    {
                        Action = "getlist",
                        Data = null
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
            public static List<DM_Huongdan> GetList()
            {
                try
                {
                    string url = CreateRequestUrl("loaithutuc_huongdan");
                    APIRequestData requestData = new APIRequestData()
                    {
                        Action = "getlist",
                        Data = null
                    };
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
    }
}
