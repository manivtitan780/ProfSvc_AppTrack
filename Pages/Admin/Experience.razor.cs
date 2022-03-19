#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_AppTrack
// File Name:           Experience.razor.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          01-26-2022 19:30
// Last Updated On:     01-27-2022 19:04
// *****************************************/

#endregion

namespace ProfSvc_AppTrack.Pages.Admin;

public partial class Experience:ComponentBase
{
    private static bool _valueChanged = true;

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

    private AdminListDialog AdminDialog
    {
        get;
        set;
    }

    private AutoCompleteButton AutoCompleteControl
    {
        get;
        set;
    }

    private static int Count
    {
        get;
        set;
    } = 24;

    private AdminList ExperienceRecord
    {
        get;
        set;
    } = new();

    private AdminList ExperienceRecordClone
    {
        get;
        set;
    } = new();

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

    private int ID
    {
        get;
        set;
    } = -1;

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
        await Task.Delay(1);
        await NavManager.RedirectLogin(LocalStorageBlazored);
    }

    private async Task ActionComplete(ActionEventArgs<AdminList> experienceAction)
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

    //private async Task Cancel()
    //{
    //    await Task.Delay(1);
    //    await Dialog.HideAsync();
    //}

    private void DataHandler(object obj) => Count = Grid.CurrentViewData.Count();

    private async Task EditExperience(int id)
    {
        await Task.Delay(1);
        Task<double> _index = Grid.GetRowIndexByPrimaryKey(id);
        await Grid.SelectRowAsync(_index.Result);
        General.SetAdminListDefault("", "", false, false);
        if (id == 0)
        {
            Title = "Add";
            ExperienceRecordClone.ClearData();
        }
        else
        {
            Title = "Edit";
            ExperienceRecordClone = ExperienceRecord.Copy();
        }

        StateHasChanged();
        await AdminDialog.Dialog.ShowAsync();
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

    private void RowSelected(RowSelectEventArgs<AdminList> experience) => ExperienceRecord = experience.Data;

    private async Task SaveExperience(EditContext context)
    {
        await Task.Delay(1);
        //await Spinner.ShowAsync();
        string _returnValue = await General.SaveAdminListAsync("Admin_SaveExperience", "Experience", false, false, ExperienceRecord, Grid);
        ID = _returnValue.ToInt32();
        //await Task.Delay(1);
        //await Spinner.HideAsync();
        //await Dialog.HideAsync();
    }

    private async Task<string> ToggleStatus(int experienceID) => await General.PostToggleAsync("Admin_ToggleExperienceStatus", experienceID, "ADMIN", false, Grid);

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
}