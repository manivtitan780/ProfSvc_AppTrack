#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_AppTrack
// File Name:           TaxTerm.razor.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          01-26-2022 19:30
// Last Updated On:     02-14-2022 16:05
// *****************************************/

#endregion

namespace ProfSvc_AppTrack.Pages.Admin;

public partial class TaxTerm
{
    private static bool _valueChanged = true;

    private static IHttpClientFactory _clientFactory;
    private AdminList TaxTermRecord = new();

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

    private string Code
    {
        get;
        set;
    } = "";

    private static int Count
    {
        get;
        set;
    }

    private static string Filter
    {
        get;
        set;
    }

    private SfGrid<AdminList> Grid
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

    private static string Title
    {
        get;
        set;
    } = "Edit";

    private bool VisibleTaxTermInfo
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

        string _result = await LocalStorageBlazored.GetItemAsStringAsync("autoTaxTerm");
        FilterSet(_result);
    }

    protected override async Task OnInitializedAsync()
    {
        await Task.Delay(1);
        await NavManager.RedirectLogin(LocalStorageBlazored);
    }

    private async Task ActionComplete(ActionEventArgs<AdminList> taxTermAction)
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
            General.SetAdminListDefault("Tax Term Code", "Admin_CheckTaxTermCode", true, true);
            TaxTermRecord = new();
        }
        else
        {
            Title = "Edit";
            IsAdd = false;
            General.SetAdminListDefault("", "", false, true);
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

    private void RowSelected(RowSelectEventArgs<AdminList> designation) => TaxTermRecord = designation.Data;

    private void SaveTaxTerm()
    {
        Task.Yield();
        Code = General.SaveAdminList("Admin_SaveTaxTerm", "TaxTerm", true, true, TaxTermRecord, Grid, _clientFactory);
        VisibleTaxTermInfo = false;
    }

    private void ToggleStatusTaxTerm(string taxTermCode) => General.PostToggle("Admin_ToggleTaxTermStatus", taxTermCode, "ADMIN", true, Grid, _clientFactory);

    private static void ToolTipOpen(TooltipEventArgs args) => args.Cancel = !args.HasText;

    public class AdminTaxTermAdaptor : DataAdaptor
    {
        #region Methods

        /// <summary>Performs data Read operation synchronously.</summary>
        public override Task<object> ReadAsync(DataManagerRequest dm, string key = null) => General.GetRead("Admin_GetTaxTerms", Filter, _clientFactory, dm);

        #endregion
    }

    public class AdminTaxTermDropDownAdaptor : DataAdaptor
    {
        #region Methods

        public override Task<object> ReadAsync(DataManagerRequest dm, string key = null) => General.GetAutocompleteAsync("Admin_SearchTaxTerm", "@TaxTerm", dm);

        #endregion
    }
}