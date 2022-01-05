#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_AppTrack
// File Name:           LeadSource.razor.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          11-18-2021 19:59
// Last Updated On:     01-04-2022 16:06
// *****************************************/

#endregion

namespace ProfSvc_AppTrack.Pages.Admin;

public partial class LeadSource
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

    private AdminList SourceRecord = new();

    #endregion

    #region Properties

    private AutoCompleteButton AutoCompleteControl
    {
        get;
        set;
    }

    private static bool _valueChanged = true;

    private bool VisibleCandidateInfo
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
        args.Cancel = !args.HasText;
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender)
        {
            _valueChanged = true;

            return;
        }

        string _result = await LocalStorageBlazored.GetItemAsStringAsync("autoSource");
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

    //private void ActionBegin(ActionEventArgs<AdminList> sourceAction)
    //{
    //	if (sourceAction.RequestType != Action.Save)
    //	{
    //		return;
    //	}

    //	ID = General.SaveAdminList("Admin_SaveSource", "Source", false, false, sourceAction.Data, Grid, _clientFactory).ToInt32();
    //}

    private async void ActionComplete(ActionEventArgs<AdminList> sourceAction)
    {
        if (sourceAction.RequestType != Action.Refresh || ID <= 0)
        {
            return;
        }

        double _index = await Grid.GetRowIndexByPrimaryKeyAsync(ID);
        await Grid.SelectRowAsync(_index);
        await JsRuntime.InvokeVoidAsync("scroll", _index);
        ID = -1;
    }

    private void RowSelected(RowSelectEventArgs<AdminList> designation)
    {
        SourceRecord = designation.Data;
    }

    private void Cancel()
    {
        VisibleCandidateInfo = false;
    }

    private void DataHandler() => Count = Grid.CurrentViewData.Count();

    private void EditSource(int id)
    {
        Task<double> _index = Grid.GetRowIndexByPrimaryKey(id);
        Grid.SelectRowAsync(_index.Result);
        General.SetAdminListDefault("", "", false, false, null);
        Task.Yield();
        if (id == 0)
        {
            Title = "Add";
            SourceRecord = new();
        }
        else
        {
            Title = "Edit";
            Grid.StartEditAsync();
        }

        VisibleCandidateInfo = true;
        StateHasChanged();
    }

    private void FilterGrid(ChangeEventArgs<string, KeyValues> source)
    {
        if (!_valueChanged)
        {
            return;
        }

        FilterSet(source.Value);
        Grid.Refresh();
    }

    private void RefreshGrid() => Grid.Refresh();

    private void SaveSource()
    {
        Task.Yield();
        ID = General.SaveAdminList("Admin_SaveSource", "Source", false, false, SourceRecord, Grid, _clientFactory).ToInt32();
        VisibleCandidateInfo = false;
    }

    private void ToggleStatusAsync(int sourceID) => General.PostToggle("Admin_ToggleSourceStatus", sourceID, "ADMIN", false, Grid, _clientFactory);

    #endregion

    #region Nested

    public class AdminSourceAdaptor : DataAdaptor
    {
        #region Methods

        public override Task<object> ReadAsync(DataManagerRequest dm, string key = null) =>
            General.GetReadDataAdaptor("Admin_GetSources", Filter, _clientFactory, dm, false);

        #endregion
    }

    public class AdminSourceDropDownAdaptor : DataAdaptor
    {
        #region Methods

        public override Task<object> ReadAsync(DataManagerRequest dm, string key = null) =>
            General.GetReadAutocompleteAdaptor("Admin_SearchSource", "@Source", dm, _clientFactory);

        #endregion
    }

    #endregion
}