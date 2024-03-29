﻿#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_AppTrack
// File Name:           LeadSource.razor.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          01-26-2022 19:30
// Last Updated On:     01-27-2022 20:54
// *****************************************/

#endregion

namespace ProfSvc_AppTrack.Pages.Admin;

public partial class LeadSource
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

    private AdminList SourceRecord
    {
        get;
        set;
    } = new();

    private AdminList SourceRecordClone
    {
        get;
        set;
    } = new();

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

        string _result = await LocalStorageBlazored.GetItemAsStringAsync("autoSource");
        FilterSet(_result);
    }

    protected override async Task OnInitializedAsync()
    {
        await Task.Delay(1);
        await NavManager.RedirectLogin(LocalStorageBlazored);
    }

    private async Task ActionComplete(ActionEventArgs<AdminList> sourceAction)
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

    private void DataHandler() => Count = Grid.CurrentViewData.Count();

    private async Task EditSource(int id)
    {
        Task<double> _index = Grid.GetRowIndexByPrimaryKeyAsync(id);
        await Grid.SelectRowAsync(_index.Result);
        General.SetAdminListDefault("", "", false, false);
        await Task.Delay(1);
        if (id == 0)
        {
            Title = "Add";
            SourceRecordClone.ClearData();
        }
        else
        {
            Title = "Edit";
            SourceRecordClone = SourceRecord.Copy();
        }

        StateHasChanged();
        await AdminDialog.Dialog.ShowAsync();
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

    private void RowSelected(RowSelectEventArgs<AdminList> designation) => SourceRecord = designation.Data;

    private async Task SaveSource()
    {
        await Task.Delay(1);
        string _returnValue = await General.SaveAdminListAsync("Admin_SaveSource", "Source", false, false, SourceRecordClone, Grid, SourceRecord);
        ID = _returnValue.ToInt32();
    }

    private async Task<string> ToggleStatus(int sourceID) => await General.PostToggleAsync("Admin_ToggleSourceStatus", sourceID, "ADMIN", false, Grid);

    public class AdminSourceAdaptor : DataAdaptor
    {
        #region Methods

        public override async Task<object> ReadAsync(DataManagerRequest dm, string key = null) => await General.GetReadAsync("Admin_GetSources", Filter, dm, false);

        #endregion
    }

    public class AdminSourceDropDownAdaptor : DataAdaptor
    {
        #region Methods

        public override async Task<object> ReadAsync(DataManagerRequest dm, string key = null) => await General.GetAutocompleteAsync("Admin_SearchSource", "@Source", dm);

        #endregion
    }
}