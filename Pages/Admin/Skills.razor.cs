#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_AppTrack
// File Name:           Skills.razor.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          01-26-2022 19:30
// Last Updated On:     02-14-2022 16:04
// *****************************************/

#endregion

namespace ProfSvc_AppTrack.Pages.Admin;

public partial class Skills:ComponentBase
{
    private static bool _valueChanged = true;

    private static IHttpClientFactory _clientFactory;

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

    private AdminList SkillRecord = new();

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

    private bool VisibleSkillInfo
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

        string _result = await LocalStorageBlazored.GetItemAsStringAsync("autoSkill");
        FilterSet(_result);
    }

    protected override async Task OnInitializedAsync()
    {
        await Task.Delay(1);
        await NavManager.RedirectLogin(LocalStorageBlazored);
    }

    private async Task ActionComplete(ActionEventArgs<AdminList> skillAction)
    {
        if (skillAction.RequestType != Action.Refresh || ID <= 0)
        {
            return;
        }

        double _index = await Grid.GetRowIndexByPrimaryKeyAsync(ID);
        await Grid.SelectRowAsync(_index);
        await JsRuntime.InvokeVoidAsync("scroll", _index);
        ID = -1;
    }

    private void Cancel()
    {
        VisibleSkillInfo = false;
    }

    private void DataHandler(object obj) => Count = Grid.CurrentViewData.Count();

    private void EditSkill(int id)
    {
        Task<double> _index = Grid.GetRowIndexByPrimaryKey(id);
        Grid.SelectRowAsync(_index.Result);
        General.SetAdminListDefault("", "", false, false);
        Task.Yield();
        if (id == 0)
        {
            Title = "Add";
            SkillRecord = new();
        }
        else
        {
            Title = "Edit";
        }

        VisibleSkillInfo = true;
        StateHasChanged();
    }

    private void FilterGrid(ChangeEventArgs<string, KeyValues> skill)
    {
        if (!_valueChanged)
        {
            return;
        }

        FilterSet(skill.Value);
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

    private void RowSelected(RowSelectEventArgs<AdminList> designation)
    {
        SkillRecord = designation.Data;
    }

    private void SaveSkill()
    {
        Task.Yield();
        ID = General.SaveAdminList("Admin_SaveSkill", "Skill", false, false, SkillRecord, Grid, _clientFactory).ToInt32();
        VisibleSkillInfo = false;
    }

    private void ToggleStatus(int skillID) => General.PostToggle("Admin_ToggleSkillStatus", skillID, "ADMIN", false, Grid, _clientFactory);

    private static void ToolTipOpen(TooltipEventArgs args)
    {
        args.Cancel = !args.HasText;
    }

    public class AdminSkillAdaptor : DataAdaptor
    {
        #region Methods

        /// <summary>Performs data Read operation synchronously.</summary>
        public override Task<object> ReadAsync(DataManagerRequest dm, string key = null) => General.GetRead("Admin_GetSkills", Filter, _clientFactory, dm, false);

        #endregion
    }

    public class AdminSkillDropDownAdaptor : DataAdaptor
    {
        #region Methods

        public override Task<object> ReadAsync(DataManagerRequest dm, string key = null) => General.GetAutocompleteAsync("Admin_SearchSkill", "@Skill", dm);

        #endregion
    }
}