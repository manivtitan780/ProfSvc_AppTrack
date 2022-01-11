#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_AppTrack
// File Name:           General.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          12-16-2021 19:16
// Last Updated On:     01-04-2022 16:03
// *****************************************/

#endregion

namespace ProfSvc_AppTrack.Code;

public static class General
{
    #region Properties

    public static byte[] Md5PasswordHash(string inputText) => MD5.Create().ComputeHash(new UTF8Encoding().GetBytes(inputText));

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

    public static string Encrypt(object str, bool query = false) => str == null ? "" : Encrypt(str.ToString(), query);

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
        Task<double> _index = grid.GetRowIndexByPrimaryKey(id);
        grid.SelectRowAsync(_index.Result);

        return _responseStream.Result;
    }

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

    public static async Task<object> GetCandidateReadAdaptor(string name, DataManagerRequest dm, bool getStates, int page, int count)
    {
        List<Candidates> _dataSource = new();

        try
        {
            RestClient _restClient = new($"{Start.ApiHost}candidates/GetGridCandidates");
            RestRequest request = new("", Method.Get);
            request.AddQueryParameter("page", page.ToString());
            request.AddQueryParameter("count", count.ToString());
            request.AddQueryParameter("name", name);
            if (getStates)
            {
                request.AddQueryParameter("getStates", getStates.ToString());
            }

            Dictionary<string, object> _restResponse = await _restClient.GetAsync<Dictionary<string, object>>(request);
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
            Candidate.PageCount = Math.Ceiling(_count / count.ToDecimal()).ToInt32();
            Candidate.StartRecord = ((page - 1) * count + 1).ToInt32();
            Candidate.EndRecord = ((page - 1) * count).ToInt32() + _dataSource.Count;

            if (!getStates)
            {
                return dm.RequiresCounts ? new DataResult
                                           {
                                               Result = _dataSource,
                                               Count = _count /*_count*/
                                           } : _dataSource;
            }

            Candidate.States = JsonConvert.DeserializeObject<List<IntValues>>(_restResponse["States"].ToString() ?? string.Empty);
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

    public static async Task<object> GetReadAutocompleteAdaptor(string methodName, string parameterName, DataManagerRequest dm, IHttpClientFactory factory = null)
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
            string _url = Start.ApiHost + "admin/SearchDropDown";

            _url += $"?methodName={methodName}&paramName={parameterName}&filter={dm.Where[0].value}";

            if (factory != null)
            {
                HttpClient _client = factory.CreateClient("app");

                HttpResponseMessage _response = await _client.GetAsync(_url);

                if (!_response.IsSuccessStatusCode)
                {
                    return dm.RequiresCounts ? new DataResult
                                               {
                                                   Result = _dataSource,
                                                   Count = 9 /*_count*/
                                               } : _dataSource;
                }

                string _responseStream = _response.Content.ReadAsStringAsync().Result;
                List<string> _jobOptionsItems = JsonConvert.DeserializeObject<List<string>>(_responseStream);
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
        }
        catch
        {
            return dm.RequiresCounts ? new DataResult
                                       {
                                           Result = _dataSource,
                                           Count = 0
                                       } : _dataSource;
        }

        return dm.RequiresCounts ? new DataResult
                                   {
                                       Result = _dataSource,
                                       Count = 0
                                   } : _dataSource;
    }

    public static async Task<object> GetReadDataAdaptor(string methodName, string filter, IHttpClientFactory clientFactory, DataManagerRequest dm,
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

    public static async Task<object> SetDataManager(string methodName, string connectionString, DataManagerRequest dm, string filterKey = "",
                                                    IHttpClientFactory factory = null)
    {
        List<AdminList> _dataSource = new();
        try
        {
            string _url = $"{Start.ApiHost}admin/GetAdminList?methodName={methodName}&access-control-allow-origin=*";
            if (!filterKey.NullOrWhiteSpace())
            {
                _url += $"&filter={filterKey}";
            }

            if (factory == null)
            {
                return dm.RequiresCounts ? new DataResult
                                           {
                                               Result = _dataSource,
                                               Count = 0
                                           } : _dataSource;
            }

            HttpRequestMessage _request = new(HttpMethod.Get, _url);
            _request.Headers.Add("Accept", "application/vnd.github.v3+json");
            _request.Headers.Add("User-Agent", "ProfSvcTrack-API");

            HttpClient _client = factory.CreateClient("appAdmin");

            HttpResponseMessage _response = await _client.SendAsync(_request);

            if (!_response.IsSuccessStatusCode)
            {
                return dm.RequiresCounts ? new DataResult
                                           {
                                               Result = _dataSource,
                                               Count = 0 /*_count*/
                                           } : _dataSource;
            }

            string _responseStream = await _response.Content.ReadAsStringAsync();
            Dictionary<string, object> _generalItems = JsonConvert.DeserializeObject<Dictionary<string, object>>(_responseStream);
            if (_generalItems == null)
            {
                return dm.RequiresCounts ? new DataResult
                                           {
                                               Result = _dataSource,
                                               Count = 0
                                           } : _dataSource;
            }

            _dataSource = JsonConvert.DeserializeObject<List<AdminList>>((_generalItems["GeneralItems"] as JArray)?.ToString() ?? string.Empty);
            int _count = _generalItems["Count"] as int? ?? 0;

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

    public static void SetAdminListDefault(string type, string methodName, bool isAdd, bool isString, IHttpClientFactory clientFactory)
    {
        AdminListDefault.Type = type;
        AdminListDefault.MethodName = methodName;
        AdminListDefault.IsAdd = isAdd;
        AdminListDefault.IsString = isString;
        AdminListDefault.ClientFactory = clientFactory;
    }

    internal static object SaveAdminList(string v1, string v2, bool v3, bool v4, object designationRecord, SfGrid<AdminList> grid,
                                         IHttpClientFactory clientFactory) => throw new NotImplementedException();

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

    public static T DeserializeObject<T>(object array) => JsonConvert.DeserializeObject<T>((array)?.ToString() ?? string.Empty);


    #endregion
}