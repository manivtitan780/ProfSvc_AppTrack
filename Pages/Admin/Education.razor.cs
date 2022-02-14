#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_AppTrack
// File Name:           Education.razor.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          01-31-2022 19:26
// Last Updated On:     01-31-2022 19:39
// *****************************************/

#endregion

namespace ProfSvc_AppTrack.Pages.Admin;

public partial class Education
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

    private AdminList EducationRecord
    {
        get;
        set;
    } = new();

    private AdminList EducationRecordClone
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

    private string Title
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

        string _result = await LocalStorageBlazored.GetItemAsStringAsync("autoEducation");
        FilterSet(_result);
    }

    protected override async Task OnInitializedAsync()
    {
        StorageCompression _compression = new(SessionStorage);
        LoginCooky _loginCooky = await _compression.Get("GridVal");
        if (_loginCooky.UserID.NullOrWhiteSpace())
        {
            //NavManager?.NavigateTo($"{NavManager.BaseUri}", true);
        }
    }

    private async Task ActionComplete(ActionEventArgs<AdminList> skillAction)
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

    /*
    private async Task Cancel()
    {
        await Task.Delay(1);
    }
*/
    /*
        private async Task Cancel()
        {
            await Task.Delay(1);
            await Dialog.HideAsync();
        }
    */

    private void DataHandler(object obj) => Count = Grid.CurrentViewData.Count();

    private async Task EditEducation(int id)
    {
        Task<double> _index = Grid.GetRowIndexByPrimaryKey(id);
        await Grid.SelectRowAsync(_index.Result);
        General.SetAdminListDefault("", "", false, false);
        await Task.Delay(1);
        if (id == 0)
        {
            Title = "Add";
            EducationRecordClone.ClearData();
        }
        else
        {
            Title = "Edit";
            EducationRecordClone = EducationRecord.Copy();
        }

        StateHasChanged();
        await AdminDialog.Dialog.ShowAsync();
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

    private void RowSelected(RowSelectEventArgs<AdminList> education) => EducationRecord = education.Data;

    private async Task SaveEducation(EditContext context)
    {
        await Task.Delay(1);
        string _return = await General.SaveAdminListAsync("Admin_SaveEducation", "Education", false, false, EducationRecordClone, Grid, EducationRecord);
        ID = _return.ToInt32();
    }

    private async Task<string> ToggleStatus(int educationID) => await General.PostToggleAsync("Admin_ToggleEducationStatus", educationID, "ADMIN", false, Grid);

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
}