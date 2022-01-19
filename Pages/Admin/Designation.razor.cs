#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_AppTrack
// File Name:           Designation.razor.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          11-18-2021 19:59
// Last Updated On:     01-04-2022 16:04
// *****************************************/

#endregion

namespace ProfSvc_AppTrack.Pages.Admin;

/// <inheritdoc />
public partial class Designation
{
    #region Fields

    private readonly Dictionary<string, object> _htmlAttributes = new()
                                                                  {
                                                                      {
                                                                          "maxlength", "100"
                                                                      },
                                                                      {
                                                                          "minlength", "1"
                                                                      },
                                                                      {
                                                                          "rows", "1"
                                                                      }
                                                                  };

    private AdminList _designationRecord = new();

    #endregion

    #region Properties

    private AutoCompleteButton AutoCompleteControl
    {
        get;
        set;
    }

    private static bool _valueChanged = true;

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

    private static void ToolTipOpen(TooltipEventArgs args)
    {
        args.Cancel = !args.HasText;
    }

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

    #endregion

    #region Methods

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender)
        {
            _valueChanged = true;

            return;
        }

        string _result = await LocalStorageBlazored.GetItemAsStringAsync("autoTitle");
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

    private async void ActionComplete(ActionEventArgs<AdminList> designationAction)
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

    private async Task CancelAsync()
    {
        await Task.Delay(1);
        await Dialog.HideAsync();
    }

    private void DataHandler()
    {
        Count = Grid.CurrentViewData.Count();
    }

    private async Task EditDesignationAsync(int id)
    {
        Task<double> _index = Grid.GetRowIndexByPrimaryKey(id);
        await Grid.SelectRowAsync(_index.Result);
        General.SetAdminListDefault("", "", false, false, null);
        await Task.Delay(1);
        if (id == 0)
        {
            Title = "Add";
            _designationRecord = new();
        }
        else
        {
            Title = "Edit";
        }

        await Dialog.ShowAsync();
    }

    private void FilterGrid(ChangeEventArgs<string, KeyValues> designation)
    {
        if (!_valueChanged)
        {
            return;
        }

        FilterSet(designation.Value);
        Grid.Refresh();
    }

    private void RefreshGrid()
    {
        Grid.Refresh();
    }

    private void RowSelected(RowSelectEventArgs<AdminList> designation)
    {
        _designationRecord = designation.Data;
    }

    private async Task SaveDesignationAsync(EditContext context)
    {
        await Task.Delay(1);
        await Spinner.ShowAsync();
        ID = General.SaveAdminList("Admin_SaveDesignation", "Designation", false, false, _designationRecord, Grid, _clientFactory).ToInt32();
        await Task.Delay(1);
        await Spinner.HideAsync();
        await Dialog.HideAsync();
    }

    private void ToggleStatusAsync(int designationID)
    {
        General.PostToggle("Admin_ToggleDesignationStatus", designationID, "ADMIN", false, Grid, _clientFactory);
    }

    #endregion

    #region Nested

    public class AdminDesignationAdaptor : DataAdaptor
    {
        #region Methods

        public override Task<object> ReadAsync(DataManagerRequest dm, string key = null) =>
            General.GetReadDataAdaptor("Admin_GetDesignations", Filter, _clientFactory, dm, false);

        #endregion
    }

    public class AdminDesignationDropDownAdaptor : DataAdaptor
    {
        #region Methods

        public override Task<object> ReadAsync(DataManagerRequest dm, string key = null) =>
            General.GetReadAutocompleteAdaptor("Admin_SearchDesignation", "@Designation", dm, _clientFactory);

        #endregion
    }

    #endregion
}