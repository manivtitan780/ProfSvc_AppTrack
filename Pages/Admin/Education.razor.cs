#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_AppTrack
// File Name:           Education.razor.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          11-18-2021 19:59
// Last Updated On:     01-04-2022 16:04
// *****************************************/

#endregion

namespace ProfSvc_AppTrack.Pages.Admin;

public partial class Education
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

    private AdminList EducationRecord = new();

    #endregion

    #region Properties

    private AutoCompleteButton AutoCompleteControl
    {
        get;
        set;
    }

    private static bool _valueChanged = true;

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

        string _result = await LocalStorageBlazored.GetItemAsStringAsync("autoEducation");
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

    private async void ActionComplete(ActionEventArgs<AdminList> skillAction)
    {
        if (skillAction.RequestType != Action.Refresh || ID <= 0)
        {
            return;
        }

        double _index = await Grid.GetRowIndexByPrimaryKeyAsync(ID);
        await JsRuntime.InvokeVoidAsync("scroll", _index);
        await Grid.SelectRowAsync(_index);
        ID = -1;
    }

    private void RowSelected(RowSelectEventArgs<AdminList> education)
    {
        EducationRecord = education.Data;
    }

    private async void Cancel()
    {
        await Task.Delay(1);
        await Dialog.HideAsync();
    }

    private void DataHandler(object obj) => Count = Grid.CurrentViewData.Count();

    private async void EditEducation(int id)
    {
        Task<double> _index = Grid.GetRowIndexByPrimaryKey(id);
        await Grid.SelectRowAsync(_index.Result);
        General.SetAdminListDefault("", "", false, false, null);
        await Task.Delay(1);
        if (id == 0)
        {
            Title = "Add";
            EducationRecord = new();
        }
        else
        {
            Title = "Edit";
        }

        await Dialog.ShowAsync();
    }

    private void FilterGrid(ChangeEventArgs<string, KeyValues> education)
    {
        if (!_valueChanged)
        {
            return;
        }

        FilterSet(education.Value);
        Grid.Refresh();
    }

    private void RefreshGrid() => Grid.Refresh();

    private async void SaveEducation(EditContext context)
    {
        await Task.Delay(1);
        await Spinner.ShowAsync();
        string _return = await General.SaveAdminListAsync("Admin_SaveEducation", "Education", false, false, EducationRecord, Grid);
        ID = _return.ToInt32();
        await Task.Delay(1);
        await Spinner.HideAsync();
        await Dialog.HideAsync();
    }

    private async Task<string> ToggleStatus(int educationID) => await General.PostToggleAsync("Admin_ToggleEducationStatus", educationID, "ADMIN", false, Grid);

    #endregion

    #region Nested

    public class AdminEducationAdaptor : DataAdaptor
    {
        #region Methods

        public override Task<object> ReadAsync(DataManagerRequest dm, string key = null) => General.GetReadAsync("Admin_GetEducation", Filter, dm, false);

        #endregion
    }

    public class AdminEducationDropDownAdaptor : DataAdaptor
    {
        #region Methods

        public override Task<object> ReadAsync(DataManagerRequest dm, string key = null) => General.GetAutocompleteAsync("Admin_SearchEducation", "@Education", dm);

        #endregion
    }

    #endregion
}