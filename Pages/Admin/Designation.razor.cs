#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_AppTrack
// File Name:           Designation.razor.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
<<<<<<< HEAD
// Created On:          01-06-2022 15:41
// Last Updated On:     01-25-2022 20:46
=======
// Created On:          01-26-2022 19:30
// Last Updated On:     01-27-2022 19:00
>>>>>>> 7ecd66aa8a5314bda5d8f46c18ac14a331fbbadf
// *****************************************/

#endregion

namespace ProfSvc_AppTrack.Pages.Admin;

/// <inheritdoc />
public partial class Designation
{
<<<<<<< HEAD
    #region Properties
=======
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
>>>>>>> 7ecd66aa8a5314bda5d8f46c18ac14a331fbbadf

    private AdminList DesignationRecord
    {
        get;
        set;
    } = new();

    private AdminList DesignationRecordClone
    {
        get;
        set;
    } = new();

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

<<<<<<< HEAD
    private Dictionary<string, object> HtmlAttributes
    {
        get;
    } = new()
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
=======
    private AdminList DesignationRecord
    {
        get;
        set;
    } = new();

    private AdminList DesignationRecordClone
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
>>>>>>> 7ecd66aa8a5314bda5d8f46c18ac14a331fbbadf

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

<<<<<<< HEAD
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

=======
>>>>>>> 7ecd66aa8a5314bda5d8f46c18ac14a331fbbadf
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

    /*
        private async Task CancelAsync()
        {
            await Task.Delay(1);
        }
<<<<<<< HEAD
    */
=======

        double _index = await Grid.GetRowIndexByPrimaryKeyAsync(ID);
        await Grid.SelectRowAsync(_index);
        await JsRuntime.InvokeVoidAsync("scroll", _index);
        ID = -1;
    }

    private void DataHandler() => Count = Grid.CurrentViewData.Count();
>>>>>>> 7ecd66aa8a5314bda5d8f46c18ac14a331fbbadf

    private async Task EditDesignationAsync(int id)
    {
        Task<double> _index = Grid.GetRowIndexByPrimaryKey(id);
        await Grid.SelectRowAsync(_index.Result);
        General.SetAdminListDefault("", "", false, false, null);
        await Task.Delay(1);
        if (id == 0)
        {
            Title = "Add";
            DesignationRecordClone.ClearData();
        }
        else
        {
            Title = "Edit";
            DesignationRecordClone = DesignationRecord.Copy();
        }

        StateHasChanged();
        await AdminDialog.Dialog.ShowAsync();
    }

    private async Task SaveDesignation(EditContext context)
    {
        await Task.Delay(1);
        string _returnValue = await General.SaveAdminListAsync("Admin_SaveDesignation", "Designation", false, false, DesignationRecordClone, Grid, DesignationRecord);
        ID = _returnValue.ToInt32();
    }

    private async Task<string> ToggleStatusAsync(int designationID) => await General.PostToggleAsync("Admin_ToggleDesignationStatus", designationID, "ADMIN", false, Grid);

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

    private void DataHandler() => Count = Grid.CurrentViewData.Count();

    private void FilterGrid(ChangeEventArgs<string, KeyValues> designation)
    {
        if (!_valueChanged)
        {
            return;
        }

        FilterSet(designation.Value);
        Grid.Refresh();
    }

<<<<<<< HEAD
    private void RefreshGrid() => Grid.Refresh();

    private void RowSelected(RowSelectEventArgs<AdminList> designation) => DesignationRecord = designation.Data;
=======
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

    private void RowSelected(RowSelectEventArgs<AdminList> designation) => DesignationRecord = designation.Data;

    private async Task SaveDesignation(EditContext context)
    {
        await Task.Delay(1);
        string _returnValue = await General.SaveAdminListAsync("Admin_SaveDesignation", "Designation", false, false, DesignationRecordClone, Grid, DesignationRecord);
        ID = _returnValue.ToInt32();
    }

    private async Task<string> ToggleStatus(int designationID) => await General.PostToggleAsync("Admin_ToggleDesignationStatus", designationID, "ADMIN", false, Grid);
>>>>>>> 7ecd66aa8a5314bda5d8f46c18ac14a331fbbadf

    #region Nested type: AdminDesignationAdaptor

    public class AdminDesignationAdaptor : DataAdaptor
    {
        #region Methods

        public override async Task<object> ReadAsync(DataManagerRequest dm, string key = null) => await General.GetReadAsync("Admin_GetDesignations", Filter, dm, false);

        #endregion
    }

    #endregion

    #region Nested type: AdminDesignationDropDownAdaptor

    public class AdminDesignationDropDownAdaptor : DataAdaptor
    {
        #region Methods

        public override async Task<object> ReadAsync(DataManagerRequest dm, string key = null) => await General.GetAutocompleteAsync("Admin_SearchDesignation", "@Designation", dm);

        #endregion
    }

    #endregion
}