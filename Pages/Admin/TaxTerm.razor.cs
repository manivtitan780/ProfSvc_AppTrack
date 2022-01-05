#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_AppTrack
// File Name:           TaxTerm.razor.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          11-18-2021 19:59
// Last Updated On:     01-04-2022 16:08
// *****************************************/

#endregion

namespace ProfSvc_AppTrack.Pages.Admin;

public partial class TaxTerm
{
    #region Fields

    private AdminList TaxTermRecord = new();

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

    private bool VisibleTaxTermInfo
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

    private SfGrid<AdminList> Grid
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

        string _result = await LocalStorageBlazored.GetItemAsStringAsync("autoTaxTerm");
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

    private async void ActionComplete(ActionEventArgs<AdminList> taxTermAction)
    {
        if (taxTermAction.RequestType != Action.Refresh || Code.NullOrWhiteSpace())
        {
            return;
        }

        Task<double> _index = Grid.GetRowIndexByPrimaryKeyAsync(Code);
        await Grid.SelectRowAsync(_index.Result);
        await JsRuntime.InvokeVoidAsync("scroll", _index);
        Code = "";
    }

    private void RowSelected(RowSelectEventArgs<AdminList> designation) => TaxTermRecord = designation.Data;

    private void Cancel() => VisibleTaxTermInfo = false;

    private void DataHandler(object obj) => Count = Grid.CurrentViewData.Count();

    private void EditTaxTerm(string code)
    {
        Task<double> _index = Grid.GetRowIndexByPrimaryKey(code);
        Grid.SelectRowAsync(_index.Result);
        Task.Yield();
        if (code.NullOrWhiteSpace())
        {
            Title = "Add";
            IsAdd = true;
            General.SetAdminListDefault("Tax Term Code", "Admin_CheckTaxTermCode", true, true, _clientFactory);
            TaxTermRecord = new();
        }
        else
        {
            Title = "Edit";
            IsAdd = false;
            General.SetAdminListDefault("", "", false, true, null);
        }

        VisibleTaxTermInfo = true;
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

    private void SaveTaxTerm()
    {
        Task.Yield();
        Code = General.SaveAdminList("Admin_SaveTaxTerm", "TaxTerm", true, true, TaxTermRecord, Grid, _clientFactory);
        VisibleTaxTermInfo = false;
    }

    private void ToggleStatusTaxTerm(string taxTermCode) => General.PostToggle("Admin_ToggleTaxTermStatus", taxTermCode, "ADMIN", true, Grid, _clientFactory);

    #endregion

    #region Nested

    public class AdminTaxTermAdaptor : DataAdaptor
    {
        #region Methods

        /// <summary>Performs data Read operation synchronously.</summary>
        public override Task<object> ReadAsync(DataManagerRequest dm, string key = null) =>
            General.GetReadDataAdaptor("Admin_GetTaxTerms", Filter, _clientFactory, dm);

        #endregion
    }

    public class AdminTaxTermDropDownAdaptor : DataAdaptor
    {
        #region Methods

        public override Task<object> ReadAsync(DataManagerRequest dm, string key = null) =>
            General.GetReadAutocompleteAdaptor("Admin_SearchTaxTerm", "@TaxTerm", dm, _clientFactory);

        #endregion
    }

    #endregion
}