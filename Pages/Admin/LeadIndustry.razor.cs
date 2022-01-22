#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_AppTrack
// File Name:           LeadIndustry.razor.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          11-18-2021 19:59
// Last Updated On:     01-04-2022 16:05
// *****************************************/

#endregion

namespace ProfSvc_AppTrack.Pages.Admin;

public partial class LeadIndustry
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

    private AdminList LeadIndustryRecord = new();

    #endregion

    #region Properties

    private AutoCompleteButton AutoCompleteControl
    {
        get;
        set;
    }

    private static bool _valueChanged = true;

    //private bool VisibleLeadIndustryInfo
    //{
    //    get;
    //    set;
    //}

    private SfDialog Dialog
    {
        get;
        set;
    }

    private SfSpinner Spinner
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

    private int ID
    {
        get;
        set;
    } = -1;

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

    public void ToolTipOpen(TooltipEventArgs args)
    {
        //_adminContext?.Validate();
        args.Cancel = !args.HasText;
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender)
        {
            _valueChanged = true;

            return;
        }

        string _result = await LocalStorageBlazored.GetItemAsStringAsync("autoIndustry");
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

    private async void ActionComplete(ActionEventArgs<AdminList> industryAction)
    {
        if (industryAction.RequestType != Action.Refresh || ID <= 0)
        {
            return;
        }

        double _index = await Grid.GetRowIndexByPrimaryKeyAsync(ID);
        await Grid.SelectRowAsync(_index);
        await JsRuntime.InvokeVoidAsync("scroll", _index);
        ID = -1;
    }

    private void RowSelected(RowSelectEventArgs<AdminList> leadIndustry)
    {
        LeadIndustryRecord = leadIndustry.Data;
    }

    private async void Cancel()
    {
        await Task.Delay(1);
        await Dialog.HideAsync();
        //VisibleLeadIndustryInfo = false;
    }

    private void DataHandler() => Count = Grid.CurrentViewData.Count();

    private async void EditIndustry(int id)
    {
        await Task.Delay(1);
        double _index = await Grid.GetRowIndexByPrimaryKey(id);
        await Grid.SelectRowAsync(_index);
        General.SetAdminListDefault("", "", false, false, null);
        if (id == 0)
        {
            Title = "Add";
            LeadIndustryRecord = new();
        }
        else
        {
            Title = "Edit";
        }

        await Dialog.ShowAsync();
        //VisibleLeadIndustryInfo = true;
        //StateHasChanged();
    }

    private void FilterGrid(ChangeEventArgs<string, KeyValues> industry)
    {
        if (!_valueChanged)
        {
            return;
        }

        FilterSet(industry.Value);
        Grid.Refresh();
    }

    private void RefreshGrid() => Grid.Refresh();

    private async void SaveIndustry(EditContext context)
    {
        await Task.Delay(1);
        await Spinner.ShowAsync();
        ID = General.SaveAdminList("Admin_SaveIndustry", "Industry", false, false, LeadIndustryRecord, Grid, _clientFactory).ToInt32();
        await Task.Delay(1);
        await Spinner.HideAsync();
        await Dialog.HideAsync();
        //VisibleLeadIndustryInfo = false;
    }

    private async Task ToggleStatusAsync(int industryID) => await General.PostToggleAsync("Admin_ToggleIndustryStatus", industryID, "ADMIN", false, Grid);

    #endregion

    #region Nested

    public class AdminIndustryAdaptor : DataAdaptor
    {
        #region Methods

        public override Task<object> ReadAsync(DataManagerRequest dm, string key = null) =>
            General.GetReadDataAdaptor("Admin_GetIndustries", Filter, _clientFactory, dm, false);

        #endregion
    }

    public class AdminIndustryDropDownAdaptor : DataAdaptor
    {
        #region Methods

        public override Task<object> ReadAsync(DataManagerRequest dm, string key = null) =>
            General.GetReadAutocompleteAdaptor("Admin_SearchIndustry", "@Industry", dm, _clientFactory);

        #endregion
    }

    #endregion
}