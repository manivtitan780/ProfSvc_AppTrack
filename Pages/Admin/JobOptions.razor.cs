#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_AppTrack
// File Name:           JobOptions.razor.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          01-26-2022 19:30
// Last Updated On:     01-27-2022 19:30
// *****************************************/

#endregion

namespace ProfSvc_AppTrack.Pages.Admin;

public partial class JobOptions
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

    private JobOption JobOptionsRecord = new();

    private AutoCompleteButton AutoCompleteControl
    {
        get;
        set;
    }

    private string Code
    {
        get;
        set;
    } = "";

    private static int Count
    {
        get;
        set;
    }

    private SfDialog Dialog
    {
        get;
        set;
    }

    private static string Filter
    {
        get;
        set;
    }

    private SfGrid<JobOption> Grid
    {
        get;
        set;
    }

    //private bool VisibleJobOptionsInfo
    //{
    //    get;
    //    set;
    //}

    [Inject]
    private NavigationManager NavManager
    {
        get;
        set;
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

    [Inject]
    private ProtectedLocalStorage SessionStorage
    {
        get;
        set;
    }

    private SfSpinner Spinner
    {
        get;
        set;
    }

    private static List<KeyValues> TaxTermKeyValues
    {
        get;
        set;
    }

    private static string Title
    {
        get;
        set;
    } = "Edit";

    protected override async void OnAfterRender(bool firstRender)
    {
        if (!firstRender)
        {
            _valueChanged = true;

            return;
        }

        string _result = await LocalStorageBlazored.GetItemAsStringAsync("autoJobOption");
        FilterSet(_result);
    }

    protected override async Task OnInitializedAsync()
    {
        await Task.Delay(1);
        await NavManager.RedirectLogin(LocalStorageBlazored);
    }

    private async Task ActionComplete(ActionEventArgs<JobOption> jobOptionAction)
    {
        if (jobOptionAction.RequestType != Action.Refresh || Code.NullOrWhiteSpace())
        {
            return;
        }

        double _index = await Grid.GetRowIndexByPrimaryKeyAsync(Code);
        await Grid.SelectRowAsync(_index);
        await JsRuntime.InvokeVoidAsync("scroll", _index);
        Code = "";
    }

    private async Task Cancel()
    {
        await Task.Delay(1);
        await Dialog.HideAsync();
    }

    private void DataHandler(object obj) => Count = Grid.CurrentViewData.Count();

    private async Task EditJobOption(string code)
    {
        await Task.Delay(1);
        double _index = await Grid.GetRowIndexByPrimaryKeyAsync(code);
        await Grid.SelectRowAsync(_index);
        if (code.NullOrWhiteSpace())
        {
            Title = "Add";
            JobOptionsRecord = new();
        }
        else
        {
            Title = "Edit";
        }

        await Dialog.ShowAsync();
    }

    private void FilterGrid(ChangeEventArgs<string, KeyValues> jobOption)
    {
        if (!_valueChanged)
        {
            return;
        }

        FilterSet(jobOption.Value);
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

    private void RowSelected(RowSelectEventArgs<JobOption> jobOption) => JobOptionsRecord = jobOption.Data;

    private async Task SaveJobOption(EditContext context)
    {
        await Task.Delay(1);
        await Spinner.ShowAsync();
        RestClient _restClient = new($"{Start.ApiHost}");
        RestRequest _request = new("Admin/SaveJobOptions", Method.Post)
                               {
                                   RequestFormat = DataFormat.Json
                               };
        _request.AddJsonBody(JobOptionsRecord);
        string _response = await _restClient.PostAsync<string>(_request);
        Code = _response;

        await Grid.Refresh();

        await Task.Delay(1);
        await Spinner.HideAsync();
        await Dialog.HideAsync();
    }

    /*
        private void SearchClicked()
        {
            if (_valueChanged)
            {
                _valueChanged = false;
    
                return;
            }
    
            FilterSet(AutoCompleteControl.Value);
        }
    */

    private static void ToolTipOpen(TooltipEventArgs args)
    {
        //_context?.Validate();
        args.Cancel = !args.HasText;
    }

    public class AdminJobOptionAdaptor : DataAdaptor
    {
        #region Methods

        /// <summary>Performs data Read operation synchronously.</summary>
        public override async Task<object> ReadAsync(DataManagerRequest dm, string key = null)
        {
            List<JobOption> _dataSource = new();

            try
            {
                await Task.Delay(1);
                RestClient _restClient = new($"{Start.ApiHost}");
                RestRequest _request = new("Admin/GetJobOptions")
                                       {
                                           RequestFormat = DataFormat.Json
                                       };
                _request.AddQueryParameter("filter", Filter);
                Dictionary<string, object> _jobOptionsItems = await _restClient.GetAsync<Dictionary<string, object>>(_request);
                int _count = 0;
                TaxTermKeyValues = new();
                if (_jobOptionsItems == null)
                {
                    return dm.RequiresCounts ? new DataResult
                                               {
                                                   Result = _dataSource,
                                                   Count = 0
                                               } : _dataSource;
                }

                _dataSource = General.DeserializeObject<List<JobOption>>(_jobOptionsItems["JobOptions"]);
                _count = _jobOptionsItems["Count"] as int? ?? 0;
                TaxTermKeyValues = General.DeserializeObject<List<KeyValues>>(_jobOptionsItems["TaxTerms"]);

                return dm.RequiresCounts ? new DataResult
                                           {
                                               Result = _dataSource,
                                               Count = _count
                                           } : _dataSource;
            }
            catch
            {
                if (_dataSource == null)
                {
                    return dm.RequiresCounts ? new DataResult
                                               {
                                                   Result = null,
                                                   Count = 1
                                               } : null;
                }

                _dataSource.Add(new("", ""));

                _valueChanged = false;

                return dm.RequiresCounts ? new DataResult
                                           {
                                               Result = _dataSource,
                                               Count = 1
                                           } : _dataSource;
            }
        }

        #endregion
    }

    public class AdminJobOptionDropDownAdaptor : DataAdaptor
    {
        #region Methods

        public override async Task<object> ReadAsync(DataManagerRequest dm, string key = null) => await General.GetAutocompleteAsync("Admin_SearchJobOption", "@JobOption", dm);

        #endregion
    }
}