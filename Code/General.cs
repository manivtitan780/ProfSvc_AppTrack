#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_AppTrack
// File Name:           General.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          01-31-2022 19:26
// Last Updated On:     03-19-2022 16:01
// *****************************************/

#endregion

#region Using

using System.Runtime.InteropServices;

#endregion

namespace ProfSvc_AppTrack.Code;

/// <summary>
/// </summary>
public static class General
{
    /*public static async Task<int> Save<T>(AdminList adminList, string connectionString, string methodName, string parameterName, string userName,
                                          SfGrid<T> grid)
    {
        await using SqlConnection _con = new(connectionString);
        _con.Open();
        int _id = -1;
        try
        {
            await using SqlCommand _command = new(methodName, _con)
                                              {
                                                  CommandType = CommandType.StoredProcedure
                                              };
            _command.AddIntParameter("@ID", adminList.ID.DbNull());
            _command.AddVarcharParameter(parameterName, 100, adminList.Text);
            _command.AddVarcharParameter("@User", 10, userName);
            _command.AddBitParameter("@Enabled", adminList.IsEnabled);
            await using SqlDataReader _reader = await _command.ExecuteReaderAsync();
            if (!_reader.HasRows)
            {
                _id = -1;
            }
            else
            {
                _reader.Read();
                _id = _reader.GetInt32(0);
            }
        }
        catch
        {
            // ignored
        }

        return _id;
    }*/
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
    private static Dictionary<string, object> _restResponse;
    //internal static object SaveAdminList(string v1, string v2, bool v3, bool v4, object designationRecord, SfGrid<AdminList> grid,
    //                                     IHttpClientFactory clientFactory) => throw new NotImplementedException();

    /*
        private static string Encrypt(string str, bool query = false)
        {
            if (str.NullOrWhiteSpace())
            {
                return "";
            }
    
            GenerateBytes(out byte[] _byteKey, out byte[] _vectorByte, query);
            using TripleDes _enc = new(_byteKey, _vectorByte);
    
            return _enc.Encrypt(str);
        }
    */

    /*
        private static void GenerateBytes(out byte[] key, out byte[] vector, bool query = false)
        {
            if (!query)
            {
                key = new byte[]
                      {
                          240, 24, 133, 174, 0, 155, 238, 145, 244, 93, 112, 139, 139, 65, 57, 242, 167, 135, 16, 221, 254, 128, 190, 228
                      };
    
                vector = new byte[]
                         {
                             121, 42, 68, 241, 103, 89, 5, 192
                         };
            }
            else
            {
                key = new byte[]
                      {
                          184, 133, 201, 79, 6, 193, 193, 255, 0, 173, 62, 48, 30, 27, 16, 15
                      };
    
                vector = new byte[]
                         {
                             121, 111, 149, 157, 168, 222, 239, 241
                         };
            }
        }
    */

    /// <summary>
    /// </summary>
    /// <param name="array"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T DeserializeObject<T>(object array) => JsonConvert.DeserializeObject<T>(array?.ToString() ?? string.Empty);

    /// <summary>
    /// </summary>
    /// <param name="methodName"></param>
    /// <param name="parameterName"></param>
    /// <param name="dm"></param>
    /// <returns></returns>
    public static async Task<object> GetAutocompleteAsync(string methodName, string parameterName, DataManagerRequest dm)
    {
        List<KeyValues> _dataSource = new();

        if (dm.Where is not {Count: > 0} || dm.Where[0].value.NullOrWhiteSpace())
        {
            return dm.RequiresCounts ? new DataResult
                                       {
                                           Result = _dataSource,
                                           Count = 0
                                       } : _dataSource;
        }

        try
        {
            await Task.Delay(1);
            RestClient _restClient = new($"{Start.ApiHost}");
            RestRequest _request = new("Admin/SearchDropDown")
                                   {
                                       RequestFormat = DataFormat.Json
                                   };
            _request.AddQueryParameter("methodName", methodName);
            _request.AddQueryParameter("paramName", parameterName);
            _request.AddQueryParameter("filter", dm.Where[0].value.ToString());
            List<string> _jobOptionsItems = await _restClient.GetAsync<List<string>>(_request); //JsonConvert.DeserializeObject<List<string>>(_responseStream);

            int _count = 0;
            if (_jobOptionsItems == null)
            {
                return dm.RequiresCounts ? new DataResult
                                           {
                                               Result = _dataSource,
                                               Count = _count
                                           } : _dataSource;
            }

            _count = _jobOptionsItems.Count;
            _dataSource.AddRange(_jobOptionsItems.Select(item => new KeyValues(item, item)));

            return dm.RequiresCounts ? new DataResult
                                       {
                                           Result = _dataSource,
                                           Count = _count
                                       } : _dataSource;
        }
        catch
        {
            return dm.RequiresCounts ? new DataResult
                                       {
                                           Result = _dataSource,
                                           Count = 0
                                       } : _dataSource;
        }
    }

    /// <summary>
    /// </summary>
    /// <param name="searchModel"></param>
    /// <param name="dm"></param>
    /// <returns></returns>
    public static async Task<object> GetCandidateReadAdaptor(CandidateSearch searchModel, DataManagerRequest dm) //string name, int page, int count)
    {
        List<Candidates> _dataSource = new();

        int _itemCount = searchModel.ItemCount;
        int _page = searchModel.Page;
        try
        {
            RestClient _restClient = new($"{Start.ApiHost}");
            RestRequest _request = new("Candidates/GetGridCandidates")
                                   {
                                       RequestFormat = DataFormat.Json
                                   };
            //_request.AddQueryParameter("page", searchModel.Page.ToString());
            //_request.AddQueryParameter("count", _itemCount.ToString());
            //_request.AddQueryParameter("name", searchModel.Name);
            _request.AddJsonBody(searchModel);

            _restResponse = await _restClient.GetAsync<Dictionary<string, object>>(_request);
            if (_restResponse == null)
            {
                return dm.RequiresCounts ? new DataResult
                                           {
                                               Result = _dataSource,
                                               Count = 0 /*_count*/
                                           } : _dataSource;
            }

            _dataSource = JsonConvert.DeserializeObject<List<Candidates>>(_restResponse["Candidates"].ToString() ?? string.Empty);
            int _count = _restResponse["Count"].ToInt32();
            Candidate.Count = _count;
            Candidate.PageCount = Math.Ceiling(_count / _itemCount.ToDecimal()).ToInt32();
            Candidate.StartRecord = ((_page - 1) * _itemCount + 1).ToInt32();
            Candidate.EndRecord = ((_page - 1) * _itemCount).ToInt32() + _dataSource.Count;

            return dm.RequiresCounts ? new DataResult
                                       {
                                           Result = _dataSource,
                                           Count = _count /*_count*/
                                       } : _dataSource;

            /*Candidate.States = JsonConvert.DeserializeObject<List<IntValues>>(_restResponse["States"].ToString() ?? string.Empty);
            Candidate.Eligibility = JsonConvert.DeserializeObject<List<IntValues>>(_restResponse["Eligibility"].ToString() ?? string.Empty);
            Candidate.Experience = JsonConvert.DeserializeObject<List<IntValues>>(_restResponse["Experience"].ToString() ?? string.Empty);
            Candidate.Communication.AddRange(new[]
                                             {
                                                 new KeyValues("A", "Average"), new KeyValues("X", "Excellent"), new KeyValues("F", "Fair"),
                                                 new KeyValues("G", "Good")
                                             });
            Candidate.TaxTerms = JsonConvert.DeserializeObject<List<KeyValues>>(_restResponse["TaxTerms"].ToString() ?? string.Empty);
            Candidate.JobOptions = JsonConvert.DeserializeObject<List<KeyValues>>(_restResponse["JobOptions"].ToString() ?? string.Empty);
            Candidate.StatusCodes = JsonConvert.DeserializeObject<List<KeyValues>>(_restResponse["StatusCodes"].ToString() ?? string.Empty);

            return dm.RequiresCounts ? new DataResult
                                       {
                                           Result = _dataSource,
                                           Count = _count
                                       } : _dataSource;*/
        }
        catch
        {
            if (_dataSource == null)
            {
                return dm.RequiresCounts ? new DataResult
                                           {
                                               Result = null,
                                               Count = 1
                                           } : null;
            }

            _dataSource.Add(new());

            return dm.RequiresCounts ? new DataResult
                                       {
                                           Result = _dataSource,
                                           Count = 1
                                       } : _dataSource;
        }
    }

    /// <summary>
    /// </summary>
    /// <param name="methodName"></param>
    /// <param name="filter"></param>
    /// <param name="clientFactory"></param>
    /// <param name="dm"></param>
    /// <param name="isString"></param>
    /// <returns></returns>
    public static async Task<object> GetRead(string methodName, string filter, IHttpClientFactory clientFactory, DataManagerRequest dm,
                                             bool isString = true)
    {
        List<AdminList> _dataSource = new();

        try
        {
            string _url = Start.ApiHost + "admin/GetAdminList?methodName=" + methodName;

            if (!filter.NullOrWhiteSpace())
            {
                _url += $"&filter={HttpUtility.UrlEncode(filter)}";
            }

            _url += $"&isString={isString}";

            HttpClient _client = clientFactory.CreateClient("app");

            HttpResponseMessage _response = await _client.GetAsync(_url);

            if (!_response.IsSuccessStatusCode)
            {
                return dm.RequiresCounts ? new DataResult
                                           {
                                               Result = _dataSource,
                                               Count = 0 /*_count*/
                                           } : _dataSource;
            }

            string _responseStream = await _response.Content.ReadAsStringAsync();
            Dictionary<string, object> _taxTermItems = JsonConvert.DeserializeObject<Dictionary<string, object>>(_responseStream);

            if (_taxTermItems == null)
            {
                return dm.RequiresCounts ? new DataResult
                                           {
                                               Result = _dataSource,
                                               Count = 0
                                           } : _dataSource;
            }

            _dataSource = JsonConvert.DeserializeObject<List<AdminList>>((_taxTermItems["GeneralItems"] as JArray)?.ToString() ?? string.Empty);
            int _count = _taxTermItems["Count"] as int? ?? 0;

            return dm.RequiresCounts ? new DataResult
                                       {
                                           Result = _dataSource,
                                           Count = _count
                                       } : _dataSource;
        }
        catch
        {
            if (_dataSource == null)
            {
                return dm.RequiresCounts ? new DataResult
                                           {
                                               Result = null,
                                               Count = 1
                                           } : null;
            }

            _dataSource.Add(new());

            return dm.RequiresCounts ? new DataResult
                                       {
                                           Result = _dataSource,
                                           Count = 1
                                       } : _dataSource;
        }
    }

    /// <summary>
    /// </summary>
    /// <param name="methodName"></param>
    /// <param name="filter"></param>
    /// <param name="dm"></param>
    /// <param name="isString"></param>
    /// <returns></returns>
    public static async Task<object> GetReadAsync(string methodName, string filter, DataManagerRequest dm, bool isString = true)
    {
        List<AdminList> _dataSource = new();

        try
        {
            await Task.Delay(1);
            RestClient _restClient = new($"{Start.ApiHost}");
            RestRequest _request = new("Admin/GetAdminList")
                                   {
                                       RequestFormat = DataFormat.Json
                                   };
            _request.AddQueryParameter("methodName", methodName);
            _request.AddQueryParameter("filter", HttpUtility.UrlEncode(filter));
            _request.AddQueryParameter("isString", isString.ToString());
            Dictionary<string, object> _response = await _restClient.GetAsync<Dictionary<string, object>>(_request);
            if (_response == null)
            {
                return await Task.FromResult<object>(dm.RequiresCounts ? new DataResult
                                                                         {
                                                                             Result = _dataSource,
                                                                             Count = 0 /*_count*/
                                                                         } : _dataSource);
            }

            _dataSource = DeserializeObject<List<AdminList>>(_response["GeneralItems"]);
            int _count = _response["Count"] as int? ?? 0;

            return await Task.FromResult<object>(dm.RequiresCounts ? new DataResult
                                                                     {
                                                                         Result = _dataSource,
                                                                         Count = _count
                                                                     } : _dataSource);
        }
        catch
        {
            if (_dataSource == null)
            {
                return await Task.FromResult<object>(dm.RequiresCounts ? new DataResult
                                                                         {
                                                                             Result = null,
                                                                             Count = 1
                                                                         } : null);
            }

            _dataSource.Add(new());

            return await Task.FromResult<object>(dm.RequiresCounts ? new DataResult
                                                                     {
                                                                         Result = _dataSource,
                                                                         Count = 1
                                                                     } : _dataSource);
        }
    }

    /// <summary>
    /// </summary>
    /// <param name="searchModel"></param>
    /// <param name="dm"></param>
    /// <param name="getInformation"></param>
    /// <returns></returns>
    public static async Task<object> GetRequisitionReadAdaptor(RequisitionSearch searchModel, DataManagerRequest dm, bool getInformation = false) //string name, int page, int count)
    {
        List<Requisitions> _dataSource = new();

        int _itemCount = searchModel.ItemCount;
        int _page = searchModel.Page;
        try
        {
            RestClient _restClient = new($"{Start.ApiHost}");
            RestRequest _request = new("Requisition/GetGridRequisitions")
                                   {
                                       RequestFormat = DataFormat.Json
                                   };
            //_request.AddQueryParameter("count", _itemCount.ToString());
            //_request.AddQueryParameter("name", searchModel.Name);
            _request.AddJsonBody(searchModel);
            _request.AddQueryParameter("getCompanyInformation", getInformation);

            _restResponse = await _restClient.GetAsync<Dictionary<string, object>>(_request);
            if (_restResponse == null)
            {
                return dm.RequiresCounts ? new DataResult
                                           {
                                               Result = _dataSource,
                                               Count = 0 /*_count*/
                                           } : _dataSource;
            }

            _dataSource = JsonConvert.DeserializeObject<List<Requisitions>>(_restResponse["Requisitions"].ToString() ?? string.Empty);
            int _count = _restResponse["Count"].ToInt32();
            Requisition.Count = _count;
            Requisition.PageCount = Math.Ceiling(_count / _itemCount.ToDecimal()).ToInt32();
            Requisition.StartRecord = ((_page - 1) * _itemCount + 1).ToInt32();
            Requisition.EndRecord = ((_page - 1) * _itemCount).ToInt32() + _dataSource.Count;

            if (!getInformation)
            {
                return dm.RequiresCounts ? new DataResult
                                           {
                                               Result = _dataSource,
                                               Count = _count /*_count*/
                                           } : _dataSource;
            }

            Requisition.Companies = JsonConvert.DeserializeObject<List<Company>>(_restResponse["Companies"].ToString() ?? string.Empty);
            Requisition.CompanyContacts = JsonConvert.DeserializeObject<List<CompanyContact>>(_restResponse["Contacts"].ToString() ?? string.Empty);
            Requisition.Skills = JsonConvert.DeserializeObject<List<IntValues>>(_restResponse["Skills"].ToString() ?? string.Empty);

            return dm.RequiresCounts ? new DataResult
                                       {
                                           Result = _dataSource,
                                           Count = _count /*_count*/
                                       } : _dataSource;
        }
        catch
        {
            if (_dataSource == null)
            {
                return dm.RequiresCounts ? new DataResult
                                           {
                                               Result = null,
                                               Count = 1
                                           } : null;
            }

            _dataSource.Add(new());

            return dm.RequiresCounts ? new DataResult
                                       {
                                           Result = _dataSource,
                                           Count = 1
                                       } : _dataSource;
        }
    }

    /// <summary>
    /// </summary>
    /// <param name="filter"></param>
    /// <param name="clientFactory"></param>
    /// <param name="dm"></param>
    /// <returns></returns>
    public static async Task<object> GetRoleDataAdaptor(string filter, IHttpClientFactory clientFactory, DataManagerRequest dm)
    {
        List<Role> _dataSource = new();

        try
        {
            string _url = Start.ApiHost + "admin/GetRoles";

            if (!filter.NullOrWhiteSpace())
            {
                _url += $"?filter={HttpUtility.UrlEncode(filter)}";
            }

            HttpClient _client = clientFactory.CreateClient("app");

            HttpResponseMessage _response = await _client.GetAsync(_url);

            if (!_response.IsSuccessStatusCode)
            {
                return dm.RequiresCounts ? new DataResult
                                           {
                                               Result = _dataSource,
                                               Count = 0 /*_count*/
                                           } : _dataSource;
            }

            string _responseStream = await _response.Content.ReadAsStringAsync();
            Dictionary<string, object> _roleItems = JsonConvert.DeserializeObject<Dictionary<string, object>>(_responseStream);

            if (_roleItems == null)
            {
                return dm.RequiresCounts ? new DataResult
                                           {
                                               Result = _dataSource,
                                               Count = 0
                                           } : _dataSource;
            }

            _dataSource = JsonConvert.DeserializeObject<List<Role>>((_roleItems["Roles"] as JArray)?.ToString() ?? string.Empty);
            int _count = _roleItems["Count"] as int? ?? 0;

            return dm.RequiresCounts ? new DataResult
                                       {
                                           Result = _dataSource,
                                           Count = _count
                                       } : _dataSource;
        }
        catch
        {
            if (_dataSource == null)
            {
                return dm.RequiresCounts ? new DataResult
                                           {
                                               Result = null,
                                               Count = 1
                                           } : null;
            }

            _dataSource.Add(new());

            return dm.RequiresCounts ? new DataResult
                                       {
                                           Result = _dataSource,
                                           Count = 1
                                       } : _dataSource;
        }
    }

    /// <summary>
    /// </summary>
    /// <param name="filter"></param>
    /// <param name="clientFactory"></param>
    /// <param name="dm"></param>
    /// <returns></returns>
    public static async Task<object> GetStateDataAdaptor(string filter, IHttpClientFactory clientFactory, DataManagerRequest dm)
    {
        List<State> _dataSource = new();

        try
        {
            string _url = Start.ApiHost + "admin/GetStates";

            if (!filter.NullOrWhiteSpace())
            {
                _url += $"?filter={HttpUtility.UrlEncode(filter)}";
            }

            HttpClient _client = clientFactory.CreateClient("app");

            HttpResponseMessage _response = await _client.GetAsync(_url);

            if (!_response.IsSuccessStatusCode)
            {
                return dm.RequiresCounts ? new DataResult
                                           {
                                               Result = _dataSource,
                                               Count = 0 /*_count*/
                                           } : _dataSource;
            }

            string _responseStream = await _response.Content.ReadAsStringAsync();
            Dictionary<string, object> _roleItems = JsonConvert.DeserializeObject<Dictionary<string, object>>(_responseStream);

            if (_roleItems == null)
            {
                return dm.RequiresCounts ? new DataResult
                                           {
                                               Result = _dataSource,
                                               Count = 0
                                           } : _dataSource;
            }

            _dataSource = JsonConvert.DeserializeObject<List<State>>((_roleItems["States"] as JArray)?.ToString() ?? string.Empty);
            int _count = _roleItems["Count"] as int? ?? 0;

            return dm.RequiresCounts ? new DataResult
                                       {
                                           Result = _dataSource,
                                           Count = _count
                                       } : _dataSource;
        }
        catch
        {
            if (_dataSource == null)
            {
                return dm.RequiresCounts ? new DataResult
                                           {
                                               Result = null,
                                               Count = 1
                                           } : null;
            }

            _dataSource.Add(new());

            return dm.RequiresCounts ? new DataResult
                                       {
                                           Result = _dataSource,
                                           Count = 1
                                       } : _dataSource;
        }
    }

    /// <summary>
    /// </summary>
    /// <param name="filter"></param>
    /// <param name="clientFactory"></param>
    /// <param name="dm"></param>
    /// <returns></returns>
    public static async Task<object> GetStatusCodeReadAdaptor(string filter, IHttpClientFactory clientFactory, DataManagerRequest dm)
    {
        List<StatusCode> _dataSource = new();

        try
        {
            string _url = Start.ApiHost + "admin/GetStatusCodes";

            if (!filter.NullOrWhiteSpace())
            {
                _url += $"?filter={HttpUtility.UrlEncode(filter)}";
            }

            HttpClient _client = clientFactory.CreateClient("app");

            HttpResponseMessage _response = await _client.GetAsync(_url);

            if (!_response.IsSuccessStatusCode)
            {
                return dm.RequiresCounts ? new DataResult
                                           {
                                               Result = _dataSource,
                                               Count = 0 /*_count*/
                                           } : _dataSource;
            }

            string _responseStream = await _response.Content.ReadAsStringAsync();
            Dictionary<string, object> _statusCodeItems = JsonConvert.DeserializeObject<Dictionary<string, object>>(_responseStream);

            if (_statusCodeItems == null)
            {
                return dm.RequiresCounts ? new DataResult
                                           {
                                               Result = _dataSource,
                                               Count = 0
                                           } : _dataSource;
            }

            _dataSource = JsonConvert.DeserializeObject<List<StatusCode>>((_statusCodeItems["StatusCodes"] as JArray)?.ToString() ?? string.Empty);
            int _count = _statusCodeItems["Count"] as int? ?? 0;

            return dm.RequiresCounts ? new DataResult
                                       {
                                           Result = _dataSource,
                                           Count = _count
                                       } : _dataSource;
        }
        catch
        {
            if (_dataSource == null)
            {
                return dm.RequiresCounts ? new DataResult
                                           {
                                               Result = null,
                                               Count = 1
                                           } : null;
            }

            _dataSource.Add(new());

            return dm.RequiresCounts ? new DataResult
                                       {
                                           Result = _dataSource,
                                           Count = 1
                                       } : _dataSource;
        }
    }

    /// <summary>
    /// </summary>
    /// <param name="inputText"></param>
    /// <returns></returns>
    public static byte[] Md5PasswordHash(string inputText) => MD5.Create().ComputeHash(new UTF8Encoding().GetBytes(inputText));

    /*
        public static Color FromHex(string hex)
        {
            if (hex.StartsWith("#"))
            {
                hex = hex[1..];
            }
    
            if (hex.Length != 6)
            {
                return Color.Empty;
            }
    
            Color _color = Color.Empty;
    
            try
            {
                int _red = int.Parse(hex[..2], NumberStyles.HexNumber);
                int _green = int.Parse(hex.Substring(2, 2), NumberStyles.HexNumber);
                int _blue = int.Parse(hex.Substring(4, 2), NumberStyles.HexNumber);
    
                return Color.FromArgb(_red, _green, _blue);
            }
            catch
            {
                return _color;
            }
        }
    */

    /*
        public static string Decrypt(string str, bool query = false)
        {
            if (str.NullOrWhiteSpace())
            {
                return "";
            }
    
            GenerateBytes(out byte[] _byteKey, out byte[] _vectorByte, query);
            using TripleDes _enc = new(_byteKey, _vectorByte);
    
            return _enc.Decrypt(str).Trim();
        }
    */

    /*
        public static string Edit<T>(int id, SfGrid<T> grid, ref bool isAdd)
        {
            Task<double> _index = grid.GetRowIndexByPrimaryKey(id);
            grid.SelectRowAsync(_index.Result);
            string _title;
            if (id == 0)
            {
                _title = "Add";
                isAdd = true;
                Task.Yield();
                grid.AddRecordAsync();
            }
            else
            {
                _title = "Edit";
                isAdd = false;
                grid.StartEditAsync();
            }
    
            return _title;
        }
    */

    /*
        public static string Encrypt(object str, bool query = false) => str == null ? "" : Encrypt(str.ToString(), query);
    */

    /// <summary>
    /// </summary>
    /// <param name="methodName"></param>
    /// <param name="id"></param>
    /// <param name="userName"></param>
    /// <param name="isString"></param>
    /// <param name="grid"></param>
    /// <param name="clientFactory"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static string PostToggle<T>(string methodName, object id, string userName, bool isString, SfGrid<T> grid, IHttpClientFactory clientFactory = null)
    {
        string _url = $"{Start.ApiHost}admin/ToggleAdminList?methodName={methodName}&id={id}&userName={userName}&idIsString={isString}";

        if (clientFactory == null)
        {
            return "";
        }

        HttpClient _client = clientFactory.CreateClient("app");

        StringContent _adminContent = new(userName, Encoding.UTF8, "application/json");
        using Task<HttpResponseMessage> _response = _client.PostAsync(_url, _adminContent);

        Task<string> _responseStream = _response.Result.Content.ReadAsStringAsync();

        grid.Refresh();
        Task<double> _index = grid.GetRowIndexByPrimaryKeyAsync(id);
        grid.SelectRowAsync(_index.Result);

        return _responseStream.Result;
    }

    /// <summary>
    /// </summary>
    /// <param name="methodName"></param>
    /// <param name="id"></param>
    /// <param name="userName"></param>
    /// <param name="isString"></param>
    /// <param name="grid"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static async Task<string> PostToggleAsync<T>(string methodName, object id, string userName, bool isString, SfGrid<T> grid)
    {
        await Task.Yield();
        RestClient _restClient = new($"{Start.ApiHost}");
        RestRequest _request = new("Admin/ToggleAdminList", Method.Post);
        _request.AddQueryParameter("methodName", methodName);
        _request.AddQueryParameter("id", id.ToString());
        _request.AddQueryParameter("username", userName);
        _request.AddQueryParameter("idIsString", isString.ToString());
        string _response = await _restClient.PostAsync<string>(_request);

        await grid.Refresh();
        double _index = await grid.GetRowIndexByPrimaryKeyAsync(id);
        await grid.SelectRowAsync(_index);

        return await Task.FromResult(_response);
    }

    /// <summary>
    /// </summary>
    /// <param name="methodName"></param>
    /// <param name="parameterName"></param>
    /// <param name="containDescription"></param>
    /// <param name="isString"></param>
    /// <param name="adminList"></param>
    /// <param name="grid"></param>
    /// <param name="clientFactory"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static string SaveAdminList<T>(string methodName, string parameterName, bool containDescription, bool isString, AdminList adminList, SfGrid<T> grid,
                                          IHttpClientFactory clientFactory = null)
    {
        string _url =
            $"{Start.ApiHost}admin/SaveAdminList?methodName={methodName}&parameterName={parameterName}&containDescription={containDescription}&isString={isString}";

        if (clientFactory == null)
        {
            return "";
        }

        //return "";

        HttpClient _client = clientFactory.CreateClient("app");

        StringContent _adminContent = new(JsonConvert.SerializeObject(adminList), Encoding.UTF8, "application/json");
        using Task<HttpResponseMessage> _response = _client.PostAsync(_url, _adminContent);

        using Task<string> _responseStream = _response.Result.Content.ReadAsStringAsync();

        grid.Refresh();

        return _responseStream.Result;
    }

    /// <summary>
    /// </summary>
    /// <param name="methodName"></param>
    /// <param name="parameterName"></param>
    /// <param name="containDescription"></param>
    /// <param name="isString"></param>
    /// <param name="adminList"></param>
    /// <param name="grid"></param>
    /// <param name="mainAdminList"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static async Task<string> SaveAdminListAsync<T>(string methodName, string parameterName, bool containDescription, bool isString, AdminList adminList, SfGrid<T> grid,
                                                           AdminList mainAdminList = null)
    {
        await Task.Yield();
        RestClient _restClient = new($"{Start.ApiHost}");
        RestRequest _request = new("Admin/SaveAdminList", Method.Post)
                               {
                                   RequestFormat = DataFormat.Json
                               };
        _request.AddQueryParameter("methodName", methodName);
        _request.AddQueryParameter("parameterName", parameterName);
        _request.AddQueryParameter("containsDescription", containDescription);
        //_request.AddQueryParameter("username", userName);
        _request.AddQueryParameter("isString", isString.ToString());
        _request.AddJsonBody(adminList);
        string _response = await _restClient.PostAsync<string>(_request);

        if (mainAdminList != null)
        {
            mainAdminList = adminList.Copy();
        }

        await grid.Refresh();

        return await Task.FromResult(_response);
    }

    //    public static async Task<object> SetDataManager(string methodName, string connectionString, DataManagerRequest dm, string filterKey = "",
    //                                                    IHttpClientFactory factory = null)
    //    {
    //        List<AdminList> _dataSource = new();
    //        try
    //        {
    //            string _url = $"{Start.ApiHost}admin/GetAdminList?methodName={methodName}&access-control-allow-origin=*";
    //            if (!filterKey.NullOrWhiteSpace())
    //            {
    //                _url += $"&filter={filterKey}";
    //            }
    //
    //            if (factory == null)
    //            {
    //                return dm.RequiresCounts ? new DataResult
    //                {
    //                    Result = _dataSource,
    //                    Count = 0
    //                } : _dataSource;
    //            }
    //
    //            HttpRequestMessage _request = new(HttpMethod.Get, _url);
    //            _request.Headers.Add("Accept", "application/vnd.github.v3+json");
    //            _request.Headers.Add("User-Agent", "ProfSvcTrack-API");
    //
    //            HttpClient _client = factory.CreateClient("appAdmin");
    //
    //            HttpResponseMessage _response = await _client.SendAsync(_request);
    //
    //            if (!_response.IsSuccessStatusCode)
    //            {
    //                return dm.RequiresCounts ? new DataResult
    //                {
    //                    Result = _dataSource,
    //                    Count = 0 /*_count*/
    //                } : _dataSource;
    //            }
    //
    //            string _responseStream = await _response.Content.ReadAsStringAsync();
    //            Dictionary<string, object> _generalItems = JsonConvert.DeserializeObject<Dictionary<string, object>>(_responseStream);
    //            if (_generalItems == null)
    //            {
    //                return dm.RequiresCounts ? new DataResult
    //                {
    //                    Result = _dataSource,
    //                    Count = 0
    //                } : _dataSource;
    //            }
    //
    //            _dataSource = JsonConvert.DeserializeObject<List<AdminList>>((_generalItems["GeneralItems"] as JArray)?.ToString() ?? string.Empty);
    //            int _count = _generalItems["Count"] as int? ?? 0;
    //
    //            return dm.RequiresCounts ? new DataResult
    //            {
    //                Result = _dataSource,
    //                Count = _count
    //            } : _dataSource;
    //        }
    //        catch
    //        {
    //            return dm.RequiresCounts ? new DataResult
    //            {
    //                Result = _dataSource,
    //                Count = 0
    //            } : _dataSource;
    //        }
    //    }

    /// <summary>
    /// </summary>
    /// <param name="type"></param>
    /// <param name="methodName"></param>
    /// <param name="isAdd"></param>
    /// <param name="isString"></param>
    public static void SetAdminListDefault(string type, string methodName, bool isAdd, bool isString) //, IHttpClientFactory clientFactory
    {
        AdminListDefault.Type = type;
        AdminListDefault.MethodName = methodName;
        AdminListDefault.IsAdd = isAdd;
        AdminListDefault.IsString = isString;
        //AdminListDefault.ClientFactory = clientFactory;
    }
}