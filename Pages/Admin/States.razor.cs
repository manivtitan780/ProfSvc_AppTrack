#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_AppTrack
// File Name:           States.razor.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          01-26-2022 19:30
// Last Updated On:     02-14-2022 16:04
// *****************************************/

#endregion

namespace ProfSvc_AppTrack.Pages.Admin;

public partial class States:ComponentBase
{
    private static bool _valueChanged = true;

    private static IHttpClientFactory _clientFactory;
    private State StateRecord = new();

    private AutoCompleteButton AutoCompleteControl
    {
        get;
        set;
    }

    [Inject]
    private IHttpClientFactory Client
    {
        set => _clientFactory = value;
    }

    private static int Count
    {
        get;
        set;
    } = 24;

    private static string Filter
    {
        get;
        set;
    }

    private SfGrid<State> Grid
    {
        get;
        set;
    }

    private int ID
    {
        get;
        set;
    }

    private bool IsAdd
    {
        get;
        set;
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

    private string Title
    {
        get;
        set;
    } = "Edit";

    private bool VisibleStateInfo
    {
        get;
        set;
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender)
        {
            _valueChanged = true;

            return;
        }

        string _result = await LocalStorageBlazored.GetItemAsStringAsync("autoState");
        FilterSet(_result);
    }

    protected override async Task OnInitializedAsync()
    {
        await Task.Delay(1);
        await NavManager.RedirectLogin(LocalStorageBlazored);
    }

    private async Task ActionComplete(ActionEventArgs<State> designationAction)
    {
        if (designationAction.RequestType != Action.Refresh || ID <= 0)
        {
            return;
        }

        double _index = await Grid.GetRowIndexByPrimaryKeyAsync(ID);
        await Grid.SelectRowAsync(_index);
        await JsRuntime.InvokeVoidAsync("scroll", _index);
        ID = -1;
    }

    private void Cancel() => VisibleStateInfo = false;

    private void DataHandler() => Count = Grid.CurrentViewData.Count();

    private void EditState(int code)
    {
        Task<double> _index = Grid.GetRowIndexByPrimaryKey(code);
        Grid.SelectRowAsync(_index.Result);
        Task.Yield();
        if (code == 0)
        {
            Title = "Add";
            IsAdd = true;
            General.SetAdminListDefault("State ID", "Admin_CheckStateID", true, true);
            StateRecord = new();
        }
        else
        {
            Title = "Edit";
            IsAdd = false;
            General.SetAdminListDefault("", "", false, true);
        }

        VisibleStateInfo = true;
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

    private void RefreshGrid() => Grid.Refresh();

    private void RowSelected(RowSelectEventArgs<State> state) => StateRecord = state.Data;

    private void SaveState(EditContext context)
    {
        Task.Yield();
        string _url = $"{Start.ApiHost}admin/SaveState";

        if (_clientFactory == null)
        {
            return;
        }

        HttpClient _client = _clientFactory.CreateClient("app");

        StringContent _stateContent = new(JsonConvert.SerializeObject(StateRecord), Encoding.UTF8, "application/json");
        using Task<HttpResponseMessage> _response = _client.PostAsync(_url, _stateContent);

        using Task<string> _responseStream = _response.Result.Content.ReadAsStringAsync();

        ID = _responseStream.Result.ToInt32();

        Grid.Refresh();
        VisibleStateInfo = false;
    }

    private static void ToolTipOpen(TooltipEventArgs args) => args.Cancel = !args.HasText;

    public class AdminStateAdaptor : DataAdaptor
    {
        #region Methods

        /// <summary>Performs data Read operation synchronously.</summary>
        public override Task<object> ReadAsync(DataManagerRequest dm, string key = null) => General.GetStateDataAdaptor(Filter, _clientFactory, dm);

        #endregion
    }

    public class AdminStateDropDownAdaptor : DataAdaptor
    {
        #region Methods

        public override Task<object> ReadAsync(DataManagerRequest dm, string key = null) => General.GetAutocompleteAsync("Admin_SearchState", "@State", dm);

        #endregion
    }
}