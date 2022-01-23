#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_AppTrack
// File Name:           Roles.razor.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          11-18-2021 19:59
// Last Updated On:     01-04-2022 16:06
// *****************************************/

#endregion

namespace ProfSvc_AppTrack.Pages.Admin;

public partial class Roles
{
    #region Fields

    private Role RoleRecord = new();

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

    private bool VisibleRoleInfo
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

    private static int Count
    {
        get;
        set;
    } = 24;

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

    private SfGrid<Role> Grid
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

    private string Title
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

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender)
        {
            _valueChanged = true;

            return;
        }

        string _result = await LocalStorageBlazored.GetItemAsStringAsync("autoRole");
        FilterSet(_result);
    }

    protected override async Task OnInitializedAsync()
    {
        StorageCompression _compression = new(SessionStorage);
        Cooky _cooky = await _compression.Get("GridVal");
        if (_cooky.UserID.NullOrWhiteSpace())
        {
            //NavManager?.NavigateTo($"{NavManager.BaseUri}", true);
        }
    }

    private void ActionBegin(ActionEventArgs<Role> roleAction)
    {
    }

    private async void ActionComplete(ActionEventArgs<Role> designationAction)
    {
        if (designationAction.RequestType != Action.Refresh || Code.NullOrWhiteSpace())
        {
            return;
        }

        double _index = await Grid.GetRowIndexByPrimaryKeyAsync(Code);
        await Grid.SelectRowAsync(_index);
        await JsRuntime.InvokeVoidAsync("scroll", _index);
        Code = "";
    }

    private void RowSelected(RowSelectEventArgs<Role> role)
    {
        RoleRecord = role.Data;
    }

    private void Cancel()
    {
        VisibleRoleInfo = false;
    }

    private void DataHandler() => Count = Grid.CurrentViewData.Count();

    private void EditRole(string code)
    {
        Task<double> _index = Grid.GetRowIndexByPrimaryKey(code);
        Grid.SelectRowAsync(_index.Result);
        Task.Yield();
        if (code.NullOrWhiteSpace())
        {
            Title = "Add";
            IsAdd = true;
            General.SetAdminListDefault("Role ID", "Admin_CheckRoleID", true, true, _clientFactory);
            RoleRecord = new();
        }
        else
        {
            Title = "Edit";
            IsAdd = false;
            General.SetAdminListDefault("", "", false, true, null);
        }

        VisibleRoleInfo = true;
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

    private void SaveRole()
    {
        Task.Yield();
        string _url = $"{Start.ApiHost}admin/SaveRole";

        if (_clientFactory == null)
        {
            return;
        }

        HttpClient _client = _clientFactory.CreateClient("app");

        StringContent _roleContent = new(JsonConvert.SerializeObject(RoleRecord), Encoding.UTF8, "application/json");
        using Task<HttpResponseMessage> _response = _client.PostAsync(_url, _roleContent);

        using Task<string> _responseStream = _response.Result.Content.ReadAsStringAsync();

        Code = _responseStream.Result;

        Grid.Refresh();
        VisibleRoleInfo = false;
    }

    private void ToolTipOpen(TooltipEventArgs args)
    {
        args.Cancel = !args.HasText;
    }

    #endregion

    #region Nested

    public class AdminRoleAdaptor : DataAdaptor
    {
        #region Methods

        /// <summary>Performs data Read operation synchronously.</summary>
        public override Task<object> ReadAsync(DataManagerRequest dm, string key = null) => General.GetRoleDataAdaptor(Filter, _clientFactory, dm);

        #endregion
    }

    public class AdminRoleDropDownAdaptor : DataAdaptor
    {
        #region Methods

        public override Task<object> ReadAsync(DataManagerRequest dm, string key = null) => General.GetReadAutocompleteAdaptorAsync("Admin_SearchRole", "@Role", dm);

        #endregion
    }

    #endregion
}