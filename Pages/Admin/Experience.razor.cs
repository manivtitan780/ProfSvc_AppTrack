#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_AppTrack
// File Name:           Experience.razor.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          11-18-2021 19:59
// Last Updated On:     01-04-2022 16:05
// *****************************************/

#endregion

namespace ProfSvc_AppTrack.Pages.Admin;

public partial class Experience
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

    private AdminList ExperienceRecord = new();

    #endregion

    #region Properties

    private AutoCompleteButton AutoCompleteControl
    {
        get;
        set;
    }

    private static bool _valueChanged = true;

    //private static IHttpClientFactory _clientFactory;

    //[Inject]
    //private IHttpClientFactory Client
    //{
    //    set => _clientFactory = value;
    //}

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

    private static string Title
    {
        get;
        set;
    } = "Edit";

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

        string _result = await LocalStorageBlazored.GetItemAsStringAsync("autoExperience");
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

    private async void ActionComplete(ActionEventArgs<AdminList> experienceAction)
    {
        if (experienceAction.RequestType != Action.Refresh || ID <= 0)
        {
            return;
        }

        double _index = await Grid.GetRowIndexByPrimaryKeyAsync(ID);
        await JsRuntime.InvokeVoidAsync("scroll", _index);
        await Grid.SelectRowAsync(_index);
        ID = -1;
    }

    private void RowSelected(RowSelectEventArgs<AdminList> experience)
    {
        ExperienceRecord = experience.Data;
    }

    private async void Cancel()
    {
        await Task.Delay(1);
        await Dialog.HideAsync();
    }

    private void DataHandler(object obj) => Count = Grid.CurrentViewData.Count();

    private async void EditExperience(int id)
    {
        await Task.Delay(1);
        Task<double> _index = Grid.GetRowIndexByPrimaryKey(id);
        await Grid.SelectRowAsync(_index.Result);
        General.SetAdminListDefault("", "", false, false, null);
        if (id == 0)
        {
            Title = "Add";
            ExperienceRecord = new();
        }
        else
        {
            Title = "Edit";
        }
        await Dialog.ShowAsync();
    }

    private void FilterGrid(ChangeEventArgs<string, KeyValues> experience)
    {
        if (!_valueChanged)
        {
            return;
        }

        FilterSet(experience.Value);
        Grid.Refresh();
    }

    private void RefreshGrid() => Grid.Refresh();

    private async void SaveExperience(EditContext context)
    {
        await Task.Delay(1);
        await Spinner.ShowAsync();
        string _returnValue = await General.SaveAdminListAsync("Admin_SaveExperience", "Experience", false, false, ExperienceRecord, Grid);
        ID = _returnValue.ToInt32();
        await Task.Delay(1);
        await Spinner.HideAsync();
        await Dialog.HideAsync();
    }

    private async Task<string> ToggleStatus(int experienceID) => await General.PostToggleAsync("Admin_ToggleExperienceStatus", experienceID, "ADMIN", false, Grid);

    #endregion

    #region Nested

    public class AdminExperienceAdaptor : DataAdaptor
    {
        #region Methods

        public override Task<object> ReadAsync(DataManagerRequest dm, string key = null) => General.GetReadAsync("Admin_GetExperience", Filter, dm, false);

        #endregion
    }

    public class AdminExperienceDropDownAdaptor : DataAdaptor
    {
        #region Methods

        public override Task<object> ReadAsync(DataManagerRequest dm, string key = null) => General.GetAutocompleteAsync("Admin_SearchExperience", "@Experience", dm);

        #endregion
    }

    #endregion
}