#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_AppTrack
// File Name:           JobOptions.razor.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          11-18-2021 19:59
// Last Updated On:     01-04-2022 16:05
// *****************************************/

#endregion

namespace ProfSvc_AppTrack.Pages.Admin;

public partial class JobOptions
{
    #region Fields

    private readonly Dictionary<string, object> HtmlAttributes = new()
                                                                 {
                                                                     {
                                                                         "maxlength",
                                                                         "100"
                                                                     },
                                                                     {
                                                                         "minlength",
                                                                         "1"
                                                                     },
                                                                     {
                                                                         "rows",
                                                                         "1"
                                                                     }
                                                                 };

    private JobOption JobOptionsRecord = new();

    #endregion

    #region Properties

    private AutoCompleteButton AutoCompleteControl
    {
        get;
        set;
    }

    private static bool _valueChanged = true;

    private bool VisibleJobOptionsInfo
    {
        get;
        set;
    }

    private static IHttpClientFactory _clientFactory;

    [Inject]
    private IHttpClientFactory Client
    {
        get => _clientFactory;
        set => _clientFactory = value;
    }

    [Inject]
    private IJSRuntime JsRuntime
    {
        get;
        set;
    }

    [Inject]
    private ILocalStorageService LocalStorageBlazored
    {
        get;
        set;
    }

    private static int Count
    {
        get;
        set;
    }

    private static List<KeyValues> TaxTermKeyValues
    {
        get;
        set;
    }

    [Inject]
    private ProtectedLocalStorage SessionStorage
    {
        get;
        set;
    }

    private SfGrid<JobOption> Grid
    {
        get;
        set;
    }

    private string Code
    {
        get;
        set;
    } = "";

    private static string Filter
    {
        get;
        set;
    }

    private static string Title
    {
        get;
        set;
    } = "Edit";

    private static void FilterSet(string value)
    {
        Filter = !value.NullOrWhiteSpace() && value != "null" ? value : "";

        if (Filter.Length <= 0)
        {
            return;
        }

        if (Filter.StartsWith("\""))
        {
            Filter = Filter[1..];
        }

        if (Filter.EndsWith("\""))
        {
            Filter = Filter[..^1];
        }
    }

    #endregion

    #region Methods

    protected override async void OnAfterRender(bool firstRender)
    {
        if (!firstRender)
        {
            _valueChanged = true;

            return;
        }

        string _result = await LocalStorageBlazored.GetItemAsStringAsync("autoJobOption");
        FilterSet(_result);
    }

    protected override async void OnInitialized()
    {
        StorageCompression _compression = new(SessionStorage);
        Cooky _cooky = await _compression.Get("GridVal");
        if (_cooky.UserID.NullOrWhiteSpace())
        {
            //NavManager?.NavigateTo($"{NavManager.BaseUri}", true);
        }

        await base.OnInitializedAsync();
    }

    private async void ActionComplete(ActionEventArgs<JobOption> jobOptionAction)
    {
        if (jobOptionAction.RequestType != Action.Refresh || Code.NullOrWhiteSpace())
        {
            return;
        }

        double _index = await Grid.GetRowIndexByPrimaryKeyAsync(Code);
        await Grid.SelectRowAsync(_index);
        await JsRuntime.InvokeVoidAsync("scroll", _index);
        Code = "";
    }

    private void RowSelected(RowSelectEventArgs<JobOption> jobOption)
    {
        JobOptionsRecord = jobOption.Data;
    }

    private void Cancel()
    {
        VisibleJobOptionsInfo = false;
    }

    private void DataHandler(object obj) => Count = Grid.CurrentViewData.Count();

    private async void EditJobOption(string code)
    {
        double _index = await Grid.GetRowIndexByPrimaryKey(code);
        await Grid.SelectRowAsync(_index);
        await Task.Yield();
        if (code.NullOrWhiteSpace())
        {
            Title = "Add";
            JobOptionsRecord = new();
        }
        else
        {
            Title = "Edit";
        }

        VisibleJobOptionsInfo = true;
        StateHasChanged();
    }

    private void FilterGrid(ChangeEventArgs<string, KeyValues> jobOption)
    {
        if (!_valueChanged)
        {
            return;
        }

        FilterSet(jobOption.Value);
        Grid.Refresh();
    }

    private void RefreshGrid() => Grid.Refresh();

    private async void SaveJobOption(EditContext context)
    {
        await Task.Yield();
        string _url = Start.ApiHost + "admin/SaveJobOptions";
        //JobOption _job = jobOptionAction.Data;

        HttpClient _client = _clientFactory.CreateClient("app");

        StringContent _jobOptionContent = new(JsonConvert.SerializeObject(JobOptionsRecord), Encoding.UTF8, "application/json");
        using HttpResponseMessage _response = await _client.PostAsync(_url, _jobOptionContent);

        string _responseStream = await _response.Content.ReadAsStringAsync();
        Code = _responseStream;

        Grid.Refresh();
        VisibleJobOptionsInfo = false;
    }

    private void SearchClicked()
    {
        if (_valueChanged)
        {
            _valueChanged = false;

            return;
        }

        FilterSet(AutoCompleteControl.Value);
    }

    private void ToolTipOpen(TooltipEventArgs args)
    {
        //_context?.Validate();
        args.Cancel = !args.HasText;
    }

    #endregion

    #region Nested

    public class AdminJobOptionAdaptor : DataAdaptor
    {
        #region Methods

        /// <summary>Performs data Read operation synchronously.</summary>
        public override async Task<object> ReadAsync(DataManagerRequest dm, string key = null)
        {
            List<JobOption> _dataSource = new();

            try
            {
                string _url = Start.ApiHost + "admin/GetJobOptions";

                if (!Filter.NullOrWhiteSpace())
                {
                    _url += "?filter=" + Filter;
                }

                HttpClient _client = _clientFactory.CreateClient("app");

                HttpResponseMessage _response = await _client.GetAsync(_url);

                if (!_response.IsSuccessStatusCode)
                {
                    return dm.RequiresCounts ? new DataResult
                                               {
                                                   Result = _dataSource, Count = 0 /*_count*/
                                               } : _dataSource;
                }

                string _responseStream = await _response.Content.ReadAsStringAsync();
                Dictionary<string, object> _jobOptionsItems = JsonConvert.DeserializeObject<Dictionary<string, object>>(_responseStream);
                int _count = 0;
                TaxTermKeyValues = new();
                if (_jobOptionsItems == null)
                {
                    return dm.RequiresCounts ? new DataResult
                                               {
                                                   Result = _dataSource, Count = 0
                                               } : _dataSource;
                }

                _dataSource = JsonConvert.DeserializeObject<List<JobOption>>((_jobOptionsItems["JobOptions"] as JArray)?.ToString() ?? string.Empty);
                _count = _jobOptionsItems["Count"] as int? ?? 0;
                TaxTermKeyValues = JsonConvert.DeserializeObject<List<KeyValues>>((_jobOptionsItems["TaxTerms"] as JArray)?.ToString() ?? string.Empty);

                return dm.RequiresCounts ? new DataResult
                                           {
                                               Result = _dataSource, Count = _count
                                           } : _dataSource;
            }
            catch
            {
                if (_dataSource == null)
                {
                    return dm.RequiresCounts ? new DataResult
                                               {
                                                   Result = null, Count = 1
                                               } : null;
                }

                _dataSource.Add(new("", ""));

                _valueChanged = false;

                return dm.RequiresCounts ? new DataResult
                                           {
                                               Result = _dataSource, Count = 1
                                           } : _dataSource;
            }
        }

        #endregion
    }

    public class AdminJobOptionDropDownAdaptor : DataAdaptor
    {
        #region Methods

        public override Task<object> ReadAsync(DataManagerRequest dm, string key = null) =>
            General.GetReadAutocompleteAdaptor("SearchJobOptions", "", dm, _clientFactory);

        #endregion
    }

    #endregion
}