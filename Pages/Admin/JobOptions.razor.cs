#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_AppTrack
// File Name:           JobOptions.razor.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          11-18-2021 19:59
// Last Updated On:     01-04-2022 16:05
// *****************************************/

#endregion

using Syncfusion.Blazor.PivotView;
using System.Reflection;
using System;

namespace ProfSvc_AppTrack.Pages.Admin;

public partial class JobOptions
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

    private JobOption JobOptionsRecord = new();

    #endregion

    #region Properties

    private AutoCompleteButton AutoCompleteControl
    {
        get;
        set;
    }

    private static bool _valueChanged = true;

    //private bool VisibleJobOptionsInfo
    //{
    //    get;
    //    set;
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
    }

    private static List<KeyValues> TaxTermKeyValues
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

    private SfGrid<JobOption> Grid
    {
        get;
        set;
    }

    private string Code
    {
        get;
        set;
    } = "";

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

    protected override async void OnInitialized()
    {
        StorageCompression _compression = new(SessionStorage);
        Cooky _cooky = await _compression.Get("GridVal");
        if (_cooky.UserID.NullOrWhiteSpace())
        {
            //NavManager?.NavigateTo($"{NavManager.BaseUri}", true);
        }

        await base.OnInitializedAsync();
    }

    private async void ActionComplete(ActionEventArgs<JobOption> jobOptionAction)
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

    private void RowSelected(RowSelectEventArgs<JobOption> jobOption)
    {
        JobOptionsRecord = jobOption.Data;
    }

    private async void Cancel()
    {
        await Task.Delay(1);
        await Dialog.HideAsync();
    }

    private void DataHandler(object obj) => Count = Grid.CurrentViewData.Count();

    private async void EditJobOption(string code)
    {
        await Task.Delay(1);
        double _index = await Grid.GetRowIndexByPrimaryKey(code);
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

    private void RefreshGrid() => Grid.Refresh();

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

        Grid.Refresh();

        await Task.Delay(1);
        await Spinner.HideAsync();
        await Dialog.HideAsync();
    }

    private void SearchClicked()
    {
        if (_valueChanged)
        {
            _valueChanged = false;

            return;
        }

        FilterSet(AutoCompleteControl.Value);
    }

    private void ToolTipOpen(TooltipEventArgs args)
    {
        //_context?.Validate();
        args.Cancel = !args.HasText;
    }

    #endregion

    #region Nested

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
                RestRequest _request = new("Admin/GetJobOptions", Method.Get)
                {
                    RequestFormat = DataFormat.Json
                };
                _request.AddQueryParameter("filter", Filter, true);
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

        public async override Task<object> ReadAsync(DataManagerRequest dm, string key = null) => await General.GetAutocompleteAsync("Admin_SearchJobOption", "@JobOption", dm);

        #endregion
    }

    #endregion
}