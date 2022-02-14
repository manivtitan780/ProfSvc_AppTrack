#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_AppTrack
// File Name:           StatusCodes.razor.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          11-18-2021 19:59
// Last Updated On:     01-04-2022 16:08
// *****************************************/

#endregion

namespace ProfSvc_AppTrack.Pages.Admin;

public partial class StatusCodes
{
    #region Fields

    private readonly List<KeyValues> _statusDropItems = new()
                                                        {
                                                            new("CLI", "Client"), new("CND", "Candidate"), new("REQ", "Requisition"),
                                                            new("SCN", "Candidate Submission"), new("SUB", "Submission"), new("USR", "User"),
                                                            new("VND", "Vendor")
                                                        };

    private StatusCode StatusCodeRecord = new();

    #endregion

    #region Properties

    private AutoCompleteButton AutoCompleteControl
    {
        get;
        set;
    }

    private static bool _valueChanged = true;

    private bool IsAdd
    {
        get;
        set;
    }

    private bool VisibleStatusCodeInfo
    {
        get;
        set;
    }

    private static IHttpClientFactory _clientFactory;

    [Inject]
    private IHttpClientFactory Client
    {
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

    private int ID
    {
        get;
        set;
    }

    private static int Count
    {
        get;
        set;
    }

    [Inject]
    private NavigationManager NavManager
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

    private SfGrid<StatusCode> Grid
    {
        get;
        set;
    }

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

    private static void ToolTipOpen(TooltipEventArgs args) => args.Cancel = !args.HasText;

    #endregion

    #region Methods

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender)
        {
            _valueChanged = true;

            return;
        }

        string _result = await LocalStorageBlazored.GetItemAsStringAsync("autoStatusCode");
        FilterSet(_result);
    }

    protected override async Task OnInitializedAsync()
    {
        StorageCompression _compression = new(SessionStorage);
        LoginCooky _loginCooky = await _compression.Get("GridVal");
        if (_loginCooky.UserID.NullOrWhiteSpace())
        {
            //NavManager?.NavigateTo($"{NavManager.BaseUri}", true);
        }
    }

    private async Task ActionComplete(ActionEventArgs<StatusCode> taxTermAction)
    {
        if (taxTermAction.RequestType != Action.Refresh || ID <= 0)
        {
            return;
        }

        Task<double> _index = Grid.GetRowIndexByPrimaryKeyAsync(ID);
        await Grid.SelectRowAsync(_index.Result);
        await JsRuntime.InvokeVoidAsync("scroll", _index.Result);
        ID = 0;
    }

    private void RowSelected(RowSelectEventArgs<StatusCode> designation) => StatusCodeRecord = designation.Data;

    private void Cancel() => VisibleStatusCodeInfo = false;

    private void DataHandler(object obj) => Count = Grid.CurrentViewData.Count();

    private void EditStatusCode(int id)
    {
        Task<double> _index = Grid.GetRowIndexByPrimaryKey(id);
        Grid.SelectRowAsync(_index.Result);
        Task.Yield();
        if (id == 0)
        {
            Title = "Add";
            IsAdd = true;
            StatusCodeRecord = new();
        }
        else
        {
            Title = "Edit";
            IsAdd = false;
        }

        VisibleStatusCodeInfo = true;
        StateHasChanged();
    }

    private void FilterGrid(ChangeEventArgs<string, KeyValues> taxTerm)
    {
        if (!_valueChanged)
        {
            return;
        }

        FilterSet(taxTerm.Value);
        Grid.Refresh();
    }

    private void RefreshGrid() => Grid.Refresh();

    private void SaveStatusCode()
    {
        Task.Yield();
        string _url = $"{Start.ApiHost}admin/SaveStatusCode";

        if (_clientFactory == null)
        {
            return;
        }

        HttpClient _client = _clientFactory.CreateClient("app");

        StringContent _statusContent = new(JsonConvert.SerializeObject(StatusCodeRecord), Encoding.UTF8, "application/json");
        using Task<HttpResponseMessage> _response = _client.PostAsync(_url, _statusContent);

        using Task<string> _responseStream = _response.Result.Content.ReadAsStringAsync();

        ID = _responseStream.Result.ToInt32();

        Grid.Refresh();
        VisibleStatusCodeInfo = false;
    }

    #endregion

    #region Nested

    public class AdminStatusCodeAdaptor : DataAdaptor
    {
        #region Methods

        /// <summary>Performs data Read operation synchronously.</summary>
        public override Task<object> ReadAsync(DataManagerRequest dm, string key = null) => General.GetStatusCodeReadAdaptor(Filter, _clientFactory, dm);

        #endregion
    }

    public class AdminStatusCodeDropDownAdaptor : DataAdaptor
    {
        #region Methods

        public override Task<object> ReadAsync(DataManagerRequest dm, string key = null) => General.GetAutocompleteAsync("Admin_SearchStatusCode", "@StatusCode", dm);

        #endregion
    }

    #endregion
}