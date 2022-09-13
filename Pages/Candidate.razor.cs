#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_AppTrack
// File Name:           Candidate.razor.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          01-26-2022 19:30
// Last Updated On:     08-12-2022 21:17
// *****************************************/

#endregion

#region Using

using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;

using ActionCompleteEventArgs = Syncfusion.Blazor.Inputs.ActionCompleteEventArgs;
using ChangeEventArgs = Microsoft.AspNetCore.Components.ChangeEventArgs;
//using FileInfo = Syncfusion.Blazor.Inputs.FileInfo;
using SelectEventArgs = Syncfusion.Blazor.Navigations.SelectEventArgs;

#endregion

namespace ProfSvc_AppTrack.Pages;

public partial class Candidate
{
    private const byte RowHeight = 38;

    //private static bool _getStates = true;

    //private static bool _valueChanged = true;

    //private readonly Candidates _candidateContext = new();

    private readonly List<IntValues> _eligibilityCopy = new();
    private readonly Dictionary<string, object> _htmlAttributes = new() {{"maxlength", "50"}, {"minlength", "1"}, {"rows", "1"}};
    private readonly Dictionary<string, object> _htmlAttributes1 = new() {{"maxlength", "500"}, {"minlength", "1"}, {"rows", "4"}};

    private readonly List<KeyValues> _jobOptionsCopy = new();

    private readonly List<IntValues> _showRecords =
        new() {new(10, "10 rows"), new(25, "25 rows"), new(50, "50 rows"), new(75, "75 rows"), new(100, "100 rows")};

    private readonly List<IntValues> _statesCopy = new();

    private readonly List<ToolbarItemModel> _tools1 = new()
                                                      {
                                                          new()
                                                          {
                                                              Command = ToolbarCommand.Bold
                                                          },
                                                          new()
                                                          {
                                                              Command = ToolbarCommand.Italic
                                                          },
                                                          new()
                                                          {
                                                              Command = ToolbarCommand.Underline
                                                          },
                                                          new()
                                                          {
                                                              Command = ToolbarCommand.StrikeThrough
                                                          },
                                                          new()
                                                          {
                                                              Command = ToolbarCommand.LowerCase
                                                          },
                                                          new()
                                                          {
                                                              Command = ToolbarCommand.UpperCase
                                                          },
                                                          new()
                                                          {
                                                              Command = ToolbarCommand.SuperScript
                                                          },
                                                          new()
                                                          {
                                                              Command = ToolbarCommand.SubScript
                                                          },
                                                          new()
                                                          {
                                                              Command = ToolbarCommand.Separator
                                                          },
                                                          new()
                                                          {
                                                              Command = ToolbarCommand.ClearFormat
                                                          },
                                                          new()
                                                          {
                                                              Command = ToolbarCommand.Separator
                                                          },
                                                          new()
                                                          {
                                                              Command = ToolbarCommand.Undo
                                                          },
                                                          new()
                                                          {
                                                              Command = ToolbarCommand.Redo
                                                          }
                                                      };

    private List<CandidateActivity> _candidateActivityObject = new();

    private CandidateDetails _candidateDetailsObject = new();
    private CandidateDetails _candidateDetailsObjectClone = new();
    private List<CandidateDocument> _candidateDocumentsObject = new();
    private List<CandidateEducation> _candidateEducationObject = new();
    private List<CandidateExperience> _candidateExperienceObject = new();
    private List<CandidateMPC> _candidateMPCObject = new();
    private List<CandidateNotes> _candidateNotesObject = new();
    private List<CandidateRating> _candidateRatingObject = new();
    private List<CandidateSkills> _candidateSkillsObject = new();

    private List<KeyValues> _communication;

    private int _currentPage = 1;

    private List<IntValues> _documentTypes = new();

    //private bool _dontChangePageDetails = true;

    private List<IntValues> _eligibility;

    private List<IntValues> _experience;

    private List<KeyValues> _jobOptions;
    private int _selectedTab;

    private List<IntValues> _states;

    private List<StatusCode> _statusCodes;

    //private readonly DialogSettings _dialogParams = new()
    //{
    //	AllowDragging = false, AnimationEffect = DialogEffect.SlideRight, EnableResize = false, Height = "98vh", MinHeight = "98vh",
    //	ShowCloseIcon = false, Width = "480px", XValue = "right"
    //};

    //private readonly List<KeyValues> _statusDropItems = new()
    //{
    //	new KeyValues("CLI", "Client"),
    //	new KeyValues("CND", "Candidate"),
    //	new KeyValues("REQ", "Requisition"),
    //	new KeyValues("SCN", "Candidate Submission"),
    //	new KeyValues("SUB", "Submission"),
    //	new KeyValues("USR", "User"),
    //	new KeyValues("VND", "Vendor")
    //};

    //private EditContext _statusEditContext;

    private Candidates _target;

    private List<KeyValues> _taxTerms;

    private List<Workflow> _workflows;

    //public async Task RecordClickHandler(RecordClickEventArgs<Candidates> args)
    //{
    //    await Grid.ExpandCollapseDetailRowAsync(args.RowData);
    //}

    //private string SkillObj = "";

    public static decimal Count
    {
        get;
        set;
    }

    private AdvancedCandidateSearch DialogSearch
    {
        get;
        set;
    }

    private SubmitCandidate DialogSubmitCandidate
    {
        get;
        set;
    }

    public static int EndRecord
    {
        get;
        set;
    }

    private List<KeyValues> NextSteps
    {
        get;
    } = new();

    public static int PageCount
    {
        get;
        set;
    }

    private int RequisitionID
    {
        get;
        set;
    }

    public static int StartRecord
    {
        get;
        set;
    }

    private SubmitCandidateRequisition SubmitCandidateModel
    {
        get;
        set;
    } = new();

    private ActivityPanel ActivityPanel
    {
        get;
        set;
    }

    private UploadFiles AddedDocument
    {
        get;
        set;
    }

    private MarkupString Address
    {
        get;
        set;
    }

    private AutoCompleteButton AutoCompleteControl
    {
        get;
        set;
    }

    private MarkupString CandidateCommunication
    {
        get;
        set;
    }

    private MarkupString CandidateEligibility
    {
        get;
        set;
    }

    private MarkupString CandidateExperience
    {
        get;
        set;
    }

    //    private List<ToolbarItemModel> _tools = new()
    //                                            {
    //                                                /*new ToolbarItemModel() { Command = ToolbarCommand.Bold },
    //                                                new ToolbarItemModel() { Command = ToolbarCommand.Italic },
    //                                                new ToolbarItemModel() { Command = ToolbarCommand.Underline },
    //                                                new ToolbarItemModel() { Command = ToolbarCommand.StrikeThrough },
    //                                                new ToolbarItemModel() { Command = ToolbarCommand.FontName },
    //                                                new ToolbarItemModel() { Command = ToolbarCommand.FontSize },
    //                                                new ToolbarItemModel() { Command = ToolbarCommand.FontColor },
    //                                                new ToolbarItemModel() { Command = ToolbarCommand.BackgroundColor },
    //                                                new ToolbarItemModel() { Command = ToolbarCommand.LowerCase },
    //                                                new ToolbarItemModel() { Command = ToolbarCommand.UpperCase },
    //                                                new ToolbarItemModel() { Command = ToolbarCommand.SuperScript },
    //                                                new ToolbarItemModel() { Command = ToolbarCommand.SubScript },
    //                                                new ToolbarItemModel() { Command = ToolbarCommand.Separator },
    //                                                new ToolbarItemModel() { Command = ToolbarCommand.Formats },
    //                                                new ToolbarItemModel() { Command = ToolbarCommand.Alignments },
    //                                                new ToolbarItemModel() { Command = ToolbarCommand.ClearFormat },
    //                                                new ToolbarItemModel() { Command = ToolbarCommand.Separator },
    //                                                new ToolbarItemModel() { Command = ToolbarCommand.Undo },
    //                                                new ToolbarItemModel() { Command = ToolbarCommand.Redo }*/
    //                                            };

/*
    private static CandidateGrid CandidateGridPersistValues
    {
        get;
        set;
    } = new(1, 25, "");
*/

    private MarkupString CandidateJobOptions
    {
        get;
        set;
    }

    private MarkupString CandidateTaxTerms
    {
        get;
        set;
    }

    /*private int Code
    {
        get;
        set;
    }

    private static decimal CurrentPage
    {
        get;
        set;
    } = 1;*/

    private EditActivityDialog DialogActivity
    {
        get;
        set;
    }

    private AddDocumentDialog DialogDocument
    {
        get;
        set;
    }

    private EditCandidateDialog DialogEditCandidate
    {
        get;
        set;
    }

    private EditEducationDialog DialogEducation
    {
        get;
        set;
    }

    private EditExperienceDialog DialogExperience
    {
        get;
        set;
    }

    private MPCCandidateDialog DialogMPC
    {
        get;
        set;
    }

    private EditNotesDialog DialogNotes
    {
        get;
        set;
    }

    private RatingCandidateDialog DialogRating
    {
        get;
        set;
    }

    private EditSkillDialog DialogSkill
    {
        get;
        set;
    }

    private DownloadsPanel DownloadsPanel
    {
        get;
        set;
    }

    private EducationPanel EducationPanel
    {
        get;
        set;
    }

    [Inject]
    private IWebHostEnvironment Environment
    {
        get;
        set;
    }

    private ExperiencePanel ExperiencePanel
    {
        get;
        set;
    }

    private MemoryStream FileData
    {
        get;
        set;
    }

    //private FileInfo FileInformation
    //{
    //    get;
    //    set;
    //}

    private string FileName
    {
        get;
        set;
    }

    private double FileSize
    {
        get;
        set;
    }

    private static string Filter
    {
        get;
        set;
    }

    private bool FirstRender
    {
        get;
        set;
    } = true;

    private static SfGrid<Candidates> Grid
    {
        get;
        set;
    }

    //private SfGrid<CandidateActivity> GridActivity
    //{
    //    get;
    //    set;
    //}

    //private SfGrid<CandidateEducation> GridEducation
    //{
    //    get;
    //    set;
    //}

    //private SfGrid<CandidateExperience> GridExperience
    //{
    //    get;
    //    set;
    //}

    //private SfGrid<CandidateMPC> GridMPC
    //{
    //    get;
    //    set;
    //}

    //private SfGrid<CandidateNotes> GridNotes
    //{
    //    get;
    //    set;
    //}

    //private SfGrid<CandidateRating> GridRating
    //{
    //    get;
    //    set;
    //}

    //private SfGrid<CandidateSkills> GridSkill
    //{
    //    get;
    //    set;
    //}

    /*private int Identifier
	{
		get;
		set;
	}*/

    //private bool IsAdd
    //{
    //    get;
    //    set;
    //}

    //private static int ItemCount
    //{
    //    get;
    //    set;
    //} = 25;

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

    private LoginCooky LoginCookyUser
    {
        get;
        set;
    }

    private string MimeType
    {
        get;
        set;
    }

    private MarkupString MPCDate
    {
        get;
        set;
    }

    private MarkupString MPCNote
    {
        get;
        set;
    }

    private static string Name
    {
        get;
        set;
    } = "";

    [Inject]
    private NavigationManager NavManager
    {
        get;
        set;
    }

    private CandidateDocument NewDocument
    {
        get;
    } = new();

    private NotesPanel NotesPanel
    {
        get;
        set;
    }

    private MarkupString RatingDate
    {
        get;
        set;
    }

    private CandidateRatingMPC RatingMPC
    {
        get;
        set;
    } = new();

    private MarkupString RatingNote
    {
        get;
        set;
    }

    private static CandidateSearch SearchModel
    {
        get;
        set;
    } = new();

    private CandidateActivity SelectedActivity
    {
        get;
        set;
    } = new();

    private CandidateDocument SelectedDownload
    {
        get;
        set;
    } = new();

    private CandidateEducation SelectedEducation
    {
        get;
        set;
    } = new();

    private CandidateExperience SelectedExperience
    {
        get;
        set;
    } = new();

    private CandidateNotes SelectedNotes
    {
        get;
        set;
    } = new();

    private CandidateSkills SelectedSkill
    {
        get;
        set;
    } = new();

    //private int NumberOfLines
    //{
    //	get;
    //	set;
    //}

    [Inject]
    private ProtectedLocalStorage SessionStorage
    {
        get;
        set;
    }

    private SkillPanel SkillPanel
    {
        get;
        set;
    }

    private SortDirection SortDirectionProperty
    {
        get;
    } = SortDirection.Ascending;

    private string SortField
    {
        get;
    } = "Updated";

    private SfSpinner Spinner
    {
        get;
        set;
    } = new();

    //private bool SpinnerVisible
    //{
    //    get;
    //    set;
    //}

    private static string Title
    {
        get;
        set;
    } = "Edit";

/*
    private bool VisibleActivityDialog
    {
        get;
        set;
    }
*/

    private bool VisibleCandidateInfo
    {
        get;
        set;
    }

    /*private bool VisibleEducationDialog
    {
        get;
        set;
    }

    private bool VisibleExperienceDialog
    {
        get;
        set;
    }*/

    /*private bool VisibleMPCCandidate
    {
        get;
        set;
    }*/

    private bool VisibleNewCandidate
    {
        get;
        set;
    }

    /*private bool VisibleNotesDialog
    {
        get;
        set;
    }

    private bool VisibleProperty
    {
        get;
        set;
    }

    private bool VisibleRatingCandidate
    {
        get;
        set;
    }*/

    private bool VisibleSkillDialog
    {
        get;
        set;
    }

    [JSInvokable("DetailCollapse")]
    public void DetailRowCollapse() => _target = null;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await Task.Delay(1);
        if (!firstRender)
        {
            //_valueChanged = true;

            return;
        }

        //if (_candidateContext != null)
        //{
            //_statusEditContext = new(_candidateContext);
        //}

        _currentPage = SearchModel.Page;
        PageCount = _currentPage + 1;
        SearchModel.User = LoginCookyUser == null || LoginCookyUser.UserID.NullOrWhiteSpace() ? "JOLLY" : LoginCookyUser.UserID.ToUpperInvariant();
    }

    protected override async Task OnInitializedAsync()
    {
        await Task.Delay(1);
        Uri _uri = NavManager.ToAbsoluteUri(NavManager.Uri);
        if (QueryHelpers.ParseQuery(_uri.Query).TryGetValue("requisition", out StringValues _tempRequisitionID))
        {
            RequisitionID = _tempRequisitionID.ToInt32();
        }

        LoginCookyUser = await NavManager.RedirectInner(LocalStorageBlazored);
        IMemoryCache _memoryCache = Start.MemCache;
        while (_states == null)
        {
            _memoryCache.TryGetValue("States", out _states);
        }

        _statesCopy.Clear();
        _statesCopy.Add(new(0, "All"));
        _statesCopy.AddRange(_states);

        while (_eligibility == null)
        {
            _memoryCache.TryGetValue("Eligibility", out _eligibility);
        }

        _eligibilityCopy.Clear();
        _eligibilityCopy.Add(new(0, "All"));
        _eligibilityCopy.AddRange(_eligibility);

        _memoryCache.TryGetValue("Experience", out _experience);
        _memoryCache.TryGetValue("TaxTerms", out _taxTerms);
        while (_jobOptions == null)
        {
            _memoryCache.TryGetValue("JobOptions", out _jobOptions);
        }

        _jobOptionsCopy.Clear();
        _jobOptionsCopy.Add(new("%", "All"));
        _jobOptionsCopy.AddRange(_jobOptions);

        _memoryCache.TryGetValue("StatusCodes", out _statusCodes);
        _memoryCache.TryGetValue("Workflow", out _workflows);
        _memoryCache.TryGetValue("Communication", out _communication);
        _memoryCache.TryGetValue("DocumentTypes", out _documentTypes);

        string _cookyString = await LocalStorageBlazored.GetItemAsync<string>("CandidateGrid");
        if (!_cookyString.NullOrWhiteSpace())
        {
            SearchModel = JsonConvert.DeserializeObject<CandidateSearch>(_cookyString);
        }
        else
        {
            await LocalStorageBlazored.SetItemAsync("CandidateGrid", SearchModel);
        }

        await base.OnInitializedAsync();
    }

    private async Task AddDocument(MouseEventArgs arg)
    {
        await Task.Delay(1);

        NewDocument.ClearData();

        await DialogDocument.Dialog.ShowAsync();
    }

    /*private static void RowDataBound(RowDataBoundEventArgs<Candidates> candidate)
    {
        //VisibleProperty = false;
    }*/

    private void AddNewCandidate()
    {
        VisibleNewCandidate = true;
    }

    private async Task AdvancedSearch()
    {
        await Task.Delay(1);
        await DialogSearch.Dialog.ShowAsync();
    }

    private void AfterDocument(ActionCompleteEventArgs arg)
    {
        DialogDocument.DialogFooter.SaveButton.Disabled = false;
        DialogDocument.DialogFooter.CancelButton.Disabled = false;
    }

    private async Task AllAlphabet()
    {
        SearchModel.Name = "";
        _currentPage = 1;
        SearchModel.Page = _currentPage;
        await LocalStorageBlazored.SetItemAsync("CandidateGrid", SearchModel);
        //_ = new StorageCompression(SessionStorage).SetCandidateGrid();
        await Grid.Refresh();
    }

    private void BeforeDocument(BeforeUploadEventArgs arg)
    {
        DialogDocument.DialogFooter.SaveButton.Disabled = true;
        DialogDocument.DialogFooter.CancelButton.Disabled = true;
    }

    private void CancelCandidate()
    {
        VisibleCandidateInfo = false;
    }

    private async Task CancelMPC()
    {
        await Task.Delay(1);
        await DialogMPC.Dialog.HideAsync();
        //VisibleMPCCandidate = false;
    }

    private async Task CancelRating()
    {
        await Task.Delay(1);
        await DialogRating.Dialog.HideAsync();
    }

    private void CancelSkill()
    {
        VisibleSkillDialog = false;
    }

    private async Task ChangeItemCount(ChangeEventArgs<int, IntValues> obj)
    {
        _currentPage = 1;
        SearchModel.Page = _currentPage;
        SearchModel.ItemCount = obj.Value;

        await LocalStorageBlazored.SetItemAsync("CandidateGrid", SearchModel);
        await Grid.Refresh();
        StateHasChanged();
    }

    private async Task ClearFilter()
    {
        Name = "";
        _currentPage = 1;
        SearchModel.Page = _currentPage;
        int _currentPageItemCount = SearchModel.ItemCount;
        SearchModel.ClearData();
        SearchModel.ItemCount = _currentPageItemCount;
        SearchModel.User = LoginCookyUser == null || LoginCookyUser.UserID.NullOrWhiteSpace() ? "JOLLY" : LoginCookyUser.UserID.ToUpperInvariant();
        await LocalStorageBlazored.SetItemAsync("CandidateGrid", SearchModel);
        await Grid.Refresh();
    }

    private async Task DataHandler(object obj)
    {
        DotNetObjectReference<Candidate> _dotNetReference = DotNetObjectReference.Create(this); // create dotnet ref
        await Runtime.InvokeAsync<string>("detail", _dotNetReference);
        //  send the dotnet ref to JS side
        FirstRender = false;
        //Count = Count;
        if (Grid.TotalItemCount > 0)
        {
            await Grid.SelectRowAsync(0);
        }
    }

    private async Task DeleteDocument(int arg)
    {
        await Task.Delay(1);
        try
        {
            RestClient _client = new($"{Start.ApiHost}");
            RestRequest _request = new("Candidates/DeleteCandidateDocument", Method.Post)
                                   {
                                       RequestFormat = DataFormat.Json
                                   };
            _request.AddQueryParameter("documentID", arg.ToString());
            _request.AddQueryParameter("user", LoginCookyUser == null || LoginCookyUser.UserID.NullOrWhiteSpace() ? "JOLLY" : LoginCookyUser.UserID.ToUpperInvariant());

            Dictionary<string, object> _response = await _client.PostAsync<Dictionary<string, object>>(_request);
            if (_response == null)
            {
                return;
            }

            _candidateDocumentsObject = General.DeserializeObject<List<CandidateDocument>>(_response["Document"]);
        }
        catch
        {
            //
        }

        await Task.Delay(1);
    }

    private async Task DeleteEducation(int id)
    {
        await Task.Delay(1);
        try
        {
            RestClient _client = new($"{Start.ApiHost}");
            RestRequest _request = new("Candidates/DeleteEducation", Method.Post)
                                   {
                                       RequestFormat = DataFormat.Json
                                   };
            _request.AddQueryParameter("id", id.ToString());
            _request.AddQueryParameter("candidateID", _target.ID.ToString());
            _request.AddQueryParameter("user", LoginCookyUser == null || LoginCookyUser.UserID.NullOrWhiteSpace() ? "JOLLY" : LoginCookyUser.UserID.ToUpperInvariant());

            Dictionary<string, object> _response = await _client.PostAsync<Dictionary<string, object>>(_request);
            if (_response == null)
            {
                return;
            }

            _candidateEducationObject = General.DeserializeObject<List<CandidateEducation>>(_response["Education"]);
        }
        catch
        {
            //
        }

        await Task.Delay(1);
    }

    private async Task DeleteExperience(int id)
    {
        await Task.Delay(1);
        try
        {
            using RestClient _client = new($"{Start.ApiHost}");
            RestRequest _request = new("Candidates/DeleteExperience", Method.Post)
                                   {
                                       RequestFormat = DataFormat.Json
                                   };
            _request.AddQueryParameter("id", id.ToString());
            _request.AddQueryParameter("candidateID", _target.ID.ToString());
            _request.AddQueryParameter("user", LoginCookyUser == null || LoginCookyUser.UserID.NullOrWhiteSpace() ? "JOLLY" : LoginCookyUser.UserID.ToUpperInvariant());

            Dictionary<string, object> _response = await _client.PostAsync<Dictionary<string, object>>(_request);
            if (_response == null)
            {
                return;
            }

            _candidateExperienceObject = General.DeserializeObject<List<CandidateExperience>>(_response["Experience"]);
        }
        catch
        {
            //
        }

        await Task.Delay(1);
    }

    private async Task DeleteNotes(int id)
    {
        await Task.Delay(1);
        try
        {
            using RestClient _client = new($"{Start.ApiHost}");
            RestRequest _request = new("Candidates/DeleteNotes", Method.Post)
                                   {
                                       RequestFormat = DataFormat.Json
                                   };
            _request.AddQueryParameter("id", id.ToString());
            _request.AddQueryParameter("candidateID", _target.ID.ToString());
            _request.AddQueryParameter("user", LoginCookyUser == null || LoginCookyUser.UserID.NullOrWhiteSpace() ? "JOLLY" : LoginCookyUser.UserID.ToUpperInvariant());

            Dictionary<string, object> _response = await _client.PostAsync<Dictionary<string, object>>(_request);
            if (_response == null)
            {
                return;
            }

            _candidateNotesObject = General.DeserializeObject<List<CandidateNotes>>(_response["Notes"]);
        }
        catch
        {
            //
        }

        await Task.Delay(1);
    }

    private async Task DeleteSkill(int id)
    {
        await Task.Delay(1);
        try
        {
            using RestClient _client = new($"{Start.ApiHost}");
            RestRequest _request = new("Candidates/DeleteSkill", Method.Post)
                                   {
                                       RequestFormat = DataFormat.Json
                                   };
            _request.AddQueryParameter("id", id.ToString());
            _request.AddQueryParameter("user", LoginCookyUser == null || LoginCookyUser.UserID.NullOrWhiteSpace() ? "JOLLY" : LoginCookyUser.UserID.ToUpperInvariant());

            Dictionary<string, object> _response = await _client.PostAsync<Dictionary<string, object>>(_request);
            if (_response == null)
            {
                return;
            }

            _candidateSkillsObject = General.DeserializeObject<List<CandidateSkills>>(_response["Skill"]);
        }
        catch
        {
            //
        }

        await Task.Delay(1);
    }

    private async Task DetailDataBind(DetailDataBoundEventArgs<Candidates> candidate)
    {
        //VisibleProperty = true;

        if (_target != null)
        {
            if (_target != candidate.Data) // return when target is equal to args.data
            {
                await Grid.ExpandCollapseDetailRowAsync(_target);
            }
        }

        _target = candidate.Data;

        //_candidateDetailsObject.ClearData();
        await Task.Delay(1);
        await Spinner.ShowAsync();
        //await Task.Delay(1000);
        RestClient _restClient = new($"{Start.ApiHost}");
        RestRequest request = new("Candidates/GetCandidateDetails");
        request.AddQueryParameter("candidateID", _target.ID);

        Dictionary<string, object> _restResponse = await _restClient.GetAsync<Dictionary<string, object>>(request);

        if (_restResponse != null)
        {
            _candidateDetailsObject = JsonConvert.DeserializeObject<CandidateDetails>(_restResponse["Candidate"]?.ToString() ?? string.Empty);
            _candidateSkillsObject = General.DeserializeObject<List<CandidateSkills>>(_restResponse["Skills"]);
            _candidateEducationObject = General.DeserializeObject<List<CandidateEducation>>(_restResponse["Education"]);
            _candidateExperienceObject = General.DeserializeObject<List<CandidateExperience>>(_restResponse["Experience"]);
            _candidateActivityObject = General.DeserializeObject<List<CandidateActivity>>(_restResponse["Activity"]);
            _candidateNotesObject = General.DeserializeObject<List<CandidateNotes>>(_restResponse["Notes"]);
            _candidateRatingObject = General.DeserializeObject<List<CandidateRating>>(_restResponse["Rating"]);
            _candidateMPCObject = General.DeserializeObject<List<CandidateMPC>>(_restResponse["MPC"]);
            _candidateDocumentsObject = General.DeserializeObject<List<CandidateDocument>>(_restResponse["Document"]);
            RatingMPC = JsonConvert.DeserializeObject<CandidateRatingMPC>(_restResponse["RatingMPC"]?.ToString() ?? string.Empty);
            GetMPCDate();
            GetMPCNote();
            GetRatingDate();
            GetRatingNote();
            SetupAddress();
            SetCommunication();
            SetEligibility();
            SetJobOption();
            SetTaxTerm();
            SetExperience();
        }

        _selectedTab = _candidateActivityObject.Count > 0 ? 7 : 0;

        await Task.Delay(1);
        await Spinner.HideAsync();
    }

    private async Task DownloadDocument(int arg)
    {
        await Task.Delay(1);
        SelectedDownload = DownloadsPanel.SelectedRow;
        string _queryString = (SelectedDownload.InternalFileName + "^" + _target.ID + "^" + SelectedDownload.Location + "^0").ToBase64String();
        //NavManager.NavigateTo(NavManager.BaseUri + "Download/" + _queryString);
        await JsRuntime.InvokeVoidAsync("open", $"{NavManager.BaseUri}Download/{_queryString}", "_blank");
        /*string _filePath = Path.Combine(Environment.WebRootPath, "Uploads", "Candidate", _target.ID.ToString(), SelectedDownload.InternalFileName).ToBase64String();
        byte[] _fileBytes = await File.ReadAllBytesAsync(_filePath);
        return File(_fileBytes, "application/force-download", _decodedStringArray[2]);*/
    }

    private async Task EditActivity(int id)
    {
        await Task.Delay(1);

        SelectedActivity = ActivityPanel.SelectedRow;
        NextSteps.Clear();
        NextSteps.Add(new("No Change", ""));
        try
        {
            foreach (string[] _next in _workflows.Where(flow => flow.Step == SelectedActivity.StatusCode).Select(flow => flow.Next.Split(',')))
            {
                foreach (string _nextString in _next)
                {
                    foreach (StatusCode _status in _statusCodes.Where(status => status.Code == _nextString && status.AppliesToCode == "SCN"))
                    {
                        NextSteps.Add(new(_status.Status, _nextString));
                        break;
                    }
                }

                break;
            }
        }
        catch
        {
            //
        }

        await DialogActivity.Dialog.ShowAsync();
    }

    private async Task EditCandidate()
    {
        //double _index = await Grid.GetRowIndexByPrimaryKey(_target.ID);
        //await Grid.SelectRowAsync(_index);
        await Task.Delay(1);
        await Spinner.ShowAsync();
        if (_target.ID == 0)
        {
            Title = "Add";
            //IsAdd = true;
            _candidateDetailsObjectClone.ClearData();
        }
        else
        {
            Title = "Edit";
            //IsAdd = false;
            _candidateDetailsObjectClone = _candidateDetailsObject.Copy();
        }

        StateHasChanged();
        await DialogEditCandidate.Dialog.ShowAsync();
        await Spinner.HideAsync();
    }

    private async Task EditEducation(int id)
    {
        await Task.Delay(1);
        if (id == 0)
        {
            SelectedEducation.ClearData();
        }
        else
        {
            SelectedEducation = EducationPanel.SelectedRow;
        }

        await DialogEducation.Dialog.ShowAsync();
    }

    private async Task EditExperience(int id)
    {
        await Task.Delay(1);
        if (id == 0)
        {
            SelectedExperience.ClearData();
        }
        else
        {
            SelectedExperience = ExperiencePanel.SelectedRow;
        }

        await DialogExperience.Dialog.ShowAsync();
    }

    private async Task EditMPC()
    {
        await Task.Delay(1);
        StateHasChanged();
        await DialogMPC.Dialog.ShowAsync();
        //VisibleMPCCandidate = true;
    }

    private async Task EditNotes(int id)
    {
        await Task.Delay(1);
        if (id == 0)
        {
            SelectedNotes.ClearData();
        }
        else
        {
            SelectedNotes = NotesPanel.SelectedRow;
        }

        await DialogNotes.Dialog.ShowAsync();
    }

    private async Task EditRating()
    {
        await Task.Delay(1);
        await DialogRating.Dialog.ShowAsync();
    }

    private async Task EditSkill(int id)
    {
        if (id == 0)
        {
            SelectedSkill.ClearData();
        }
        else
        {
            SelectedSkill = SkillPanel.SelectedRow;
        }

        await DialogSkill.Dialog.ShowAsync();
    }

    private async Task FilterGrid(ChangeEventArgs<string, KeyValues> candidate)
    {
        SearchModel.Name = candidate.Value ?? "";
        _currentPage = 1;
        SearchModel.Page = _currentPage;
        candidate.IsInteracted = true;
        await LocalStorageBlazored.SetItemAsync("CandidateGrid", SearchModel);
        //_ = new StorageCompression(SessionStorage).SetCandidateGrid();
        await Grid.Refresh();
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

    private async Task FirstClick()
    {
        if (_currentPage < 1)
        {
            _currentPage = 1;
        }

        _currentPage = 1;
        SearchModel.Page = _currentPage;
        await LocalStorageBlazored.SetItemAsync("CandidateGrid", SearchModel);
        //_ = new StorageCompression(SessionStorage).SetCandidateGrid();
        await Grid.Refresh();
    }

    private void GetMPCDate()
    {
        string _mpcDate = "";
        if (_candidateDetailsObject.MpcNotes == "")
        {
            MPCDate = _mpcDate.ToMarkupString();
        }

        CandidateMPC _candidateMPCObjectFirst = _candidateMPCObject.MaxBy(x => x.Date);
        if (_candidateMPCObjectFirst != null)
        {
            _mpcDate =
                $"{_candidateMPCObjectFirst.Date.CultureDate()} [{string.Concat(_candidateMPCObjectFirst.User.Where(char.IsLetter))}]";
        }

        MPCDate = _mpcDate.ToMarkupString();
    }

    private void GetMPCNote()
    {
        string _mpcNote = "";
        if (_candidateDetailsObject.MpcNotes == "")
        {
            MPCNote = _mpcNote.ToMarkupString();
        }

        CandidateMPC _candidateMPCObjectFirst = _candidateMPCObject.MaxBy(x => x.Date);
        if (_candidateMPCObjectFirst != null)
        {
            _mpcNote = _candidateMPCObjectFirst.Comments;
        }

        MPCNote = _mpcNote.ToMarkupString();
    }

    private void GetRatingDate()
    {
        string _ratingDate = "";
        if (_candidateDetailsObject.RateNotes == "")
        {
            RatingDate = _ratingDate.ToMarkupString();
        }

        CandidateRating _candidateRatingObjectFirst = _candidateRatingObject.MaxBy(x => x.Date);
        if (_candidateRatingObjectFirst != null)
        {
            _ratingDate =
                $"{_candidateRatingObjectFirst.Date.CultureDate()} [{string.Concat(_candidateRatingObjectFirst.User.Where(char.IsLetter))}]";
        }

        RatingDate = _ratingDate.ToMarkupString();
    }

    private void GetRatingNote()
    {
        string _ratingNote = "";
        if (_candidateDetailsObject.RateNotes == "")
        {
            RatingNote = _ratingNote.ToMarkupString();
        }

        CandidateRating _candidateRatingObjectFirst = _candidateRatingObject.MaxBy(x => x.Date);
        if (_candidateRatingObjectFirst != null)
        {
            _ratingNote = _candidateRatingObjectFirst.Comments;
        }

        RatingNote = _ratingNote.ToMarkupString();
    }

    private string GetState(int id) => _states.FirstOrDefault(state => state.Key == id)?.Value.Split('-')[1].Replace("[", "").Replace("]", "").Trim();

    private async Task LastClick()
    {
        if (_currentPage < 1)
        {
            _currentPage = 1;
        }

        _currentPage = PageCount.ToInt32();
        SearchModel.Page = _currentPage;
        await LocalStorageBlazored.SetItemAsync("CandidateGrid", SearchModel);
        //_ = new StorageCompression(SessionStorage).SetCandidateGrid();
        await Grid.Refresh();
    }

    private async Task NextClick()
    {
        if (_currentPage < 1)
        {
            _currentPage = 1;
        }

        _currentPage = SearchModel.Page >= PageCount.ToInt32() ? PageCount.ToInt32() : SearchModel.Page + 1;
        SearchModel.Page = _currentPage;
        await LocalStorageBlazored.SetItemAsync("CandidateGrid", SearchModel);
        //_ = new StorageCompression(SessionStorage).SetCandidateGrid();
        await Grid.Refresh();
    }

    private async Task OnActionBegin(ActionEventArgs<Candidates> args)
    {
        await Task.Delay(1);
        if (args.RequestType == Action.Sorting)
        {
            SearchModel.SortField = args.ColumnName switch
                                    {
                                        "Name" => 2,
                                        "Location" => 5,
                                        "Status" => 8,
                                        _ => 1
                                    };
            SearchModel.SortDirection = args.Direction == SortDirection.Ascending ? (byte)1 : (byte)0;
            await LocalStorageBlazored.SetItemAsync("CandidateGrid", SearchModel);
            await Grid.Refresh();
        }
    }

    private async Task OnActionComplete(ActionEventArgs<Candidates> arg)
    {
        await Task.Delay(1);
    }

    private async Task OnFileUpload(UploadChangeEventArgs file)
    {
        await Task.Delay(1);
        foreach (UploadFiles _file in file.Files)
        {
            MimeType = _file.FileInfo.MimeContentType;
            FileName = _file.FileInfo.Name;
            FileSize = _file.FileInfo.Size;
            FileData = _file.Stream;

            //List<AdminList> _dataSource = new();
            try
            {
                //string _url = ;

                RestClient _client = new($"{Start.ApiHost}");
                RestRequest _request = new("Candidates/ParseResume", Method.Post)
                                       {
                                           AlwaysMultipartFormData = true
                                       };
                //request.AddParameter("file", new ByteArrayContent(FileData.ToArray()));
                _request.AddParameter("fileData",
                                      $"{FileName}^{FileSize}^{MimeType}^{(LoginCookyUser == null || LoginCookyUser.UserID.NullOrWhiteSpace() ? "JOLLY" : LoginCookyUser.UserID.ToUpperInvariant())}",
                                      ParameterType.RequestBody);
                _request.AddFile("file", FileData.ToArray(), FileName, MimeType);

                await _client.PostAsync(_request);
                await Grid.Refresh();
            }
            catch
            {
                //
            }
        }
    }

    private async Task PageNumberChanged(ChangeEventArgs obj)
    {
        decimal _currentValue = obj.Value.ToDecimal();
        if (_currentValue < 1)
        {
            _currentPage = 1;
        }
        else if (_currentValue > PageCount)
        {
            _currentPage = PageCount.ToInt32();
        }

        SearchModel.Page = _currentPage;
        await LocalStorageBlazored.SetItemAsync("CandidateGrid", SearchModel);
        //_ = new StorageCompression(SessionStorage).SetCandidateGrid();
        await Grid.Refresh();
    }

    private async Task PreviousClick()
    {
        if (_currentPage < 1)
        {
            _currentPage = 1;
        }

        _currentPage = SearchModel.Page <= 1 ? 1 : SearchModel.Page - 1;
        SearchModel.Page = _currentPage;
        await LocalStorageBlazored.SetItemAsync("CandidateGrid", SearchModel);
        //_ = new StorageCompression(SessionStorage).SetCandidateGrid();
        await Grid.Refresh();
    }

    private static void RefreshGrid() => Grid.Refresh();

    private async Task SaveActivity(EditContext activity)
    {
        await Task.Delay(1);

        try
        {
            RestClient _client = new($"{Start.ApiHost}");
            RestRequest _request = new("Candidates/SaveCandidateActivity", Method.Post)
                                   {
                                       RequestFormat = DataFormat.Json
                                   };
            _request.AddJsonBody(activity.Model);
            _request.AddQueryParameter("user", LoginCookyUser == null || LoginCookyUser.UserID.NullOrWhiteSpace() ? "JOLLY" : LoginCookyUser.UserID.ToUpperInvariant());
            _request.AddQueryParameter("candidateID", _target.ID);

            Dictionary<string, object> _response = await _client.PostAsync<Dictionary<string, object>>(_request);
            if (_response == null)
            {
                return;
            }

            _candidateActivityObject = General.DeserializeObject<List<CandidateActivity>>(_response["Activity"]);
        }
        catch
        {
            //
        }

        await Task.Delay(1);
    }

    private async Task SaveCandidate()
    {
        //SpinnerVisible = true;
        await Task.Delay(1);
        DialogEditCandidate.Footer.CancelButton.Disabled = true;
        DialogEditCandidate.Footer.SaveButton.Disabled = true;

        RestClient _client = new($"{Start.ApiHost}");
        RestRequest _request = new("Candidates/SaveCandidate", Method.Post)
                               {
                                   RequestFormat = DataFormat.Json
                               };
        _request.AddJsonBody(_candidateDetailsObject);

        await _client.PostAsync<int>(_request);

        _candidateDetailsObject = _candidateDetailsObjectClone.Copy();
        _target.Name = _candidateDetailsObject.FirstName + " " + _candidateDetailsObject.LastName;
        _target.Phone = _candidateDetailsObject.Phone1.FormatPhoneNumber();
        _target.Email = _candidateDetailsObject.Email;
        _target.Location = _candidateDetailsObject.City + ", " + GetState(_candidateDetailsObject.StateID) + ", " + _candidateDetailsObject.ZipCode;
        _target.Updated = DateTime.Today.CultureDate() + "[ADMIN]";
        _target.Status = "Available";
        SetupAddress();
        SetCommunication();
        SetEligibility();
        SetJobOption();
        SetTaxTerm();
        SetExperience();
        //SpinnerVisible = false;
        DialogEditCandidate.Footer.CancelButton.Disabled = false;
        DialogEditCandidate.Footer.SaveButton.Disabled = false;
        await Task.Delay(1);
        //VisibleCandidateInfo = false;
        StateHasChanged();
    }

    private async Task SaveDocument(EditContext document)
    {
        await Task.Delay(1);
        try
        {
            if (document.Model is CandidateDocument _document)
            {
                RestClient _client = new($"{Start.ApiHost}");
                RestRequest _request = new("Candidates/UploadDocument", Method.Post)
                                       {
                                           AlwaysMultipartFormData = true
                                       };
                _request.AddFile("file", AddedDocument.Stream.ToArray(), AddedDocument.FileInfo.Name, AddedDocument.FileInfo.MimeContentType);
                //request.AddParameter("file", new ByteArrayContent(FileData.ToArray()));
                _request.AddParameter("name", _document.Name, ParameterType.GetOrPost);
                _request.AddParameter("notes", _document.Notes, ParameterType.GetOrPost);
                _request.AddParameter("candidateID", _target.ID.ToString(), ParameterType.GetOrPost);
                _request.AddParameter("user", LoginCookyUser == null || LoginCookyUser.UserID.NullOrWhiteSpace() ? "JOLLY" : LoginCookyUser.UserID.ToUpperInvariant(), ParameterType.GetOrPost);
                _request.AddParameter("path", Start.UploadsPath, ParameterType.GetOrPost);
                _request.AddParameter("type", _document.DocumentTypeID.ToString(), ParameterType.GetOrPost);
                Dictionary<string, object> _response = await _client.PostAsync<Dictionary<string, object>>(_request);
                if (_response == null)
                {
                    return;
                }

                _candidateDocumentsObject = General.DeserializeObject<List<CandidateDocument>>(_response["Document"]);
            }
        }
        catch
        {
            //
        }

        await Task.Delay(1);
    }

    private async Task SaveEducation(EditContext education)
    {
        await Task.Delay(1);

        try
        {
            if (education.Model is CandidateEducation _education)
            {
                RestClient _client = new($"{Start.ApiHost}");
                RestRequest _request = new("Candidates/SaveEducation", Method.Post)
                                       {
                                           RequestFormat = DataFormat.Json
                                       };
                _request.AddJsonBody(_education);
                _request.AddQueryParameter("user", LoginCookyUser == null || LoginCookyUser.UserID.NullOrWhiteSpace() ? "JOLLY" : LoginCookyUser.UserID.ToUpperInvariant());
                _request.AddQueryParameter("candidateID", _target.ID);

                Dictionary<string, object> _response = await _client.PostAsync<Dictionary<string, object>>(_request);
                if (_response == null)
                {
                    return;
                }

                _candidateEducationObject = General.DeserializeObject<List<CandidateEducation>>(_response["Education"]);
            }
        }
        catch
        {
            //
        }

        await Task.Delay(1);
    }

    private async Task SaveExperience(EditContext experience)
    {
        await Task.Delay(1);

        try
        {
            RestClient _client = new($"{Start.ApiHost}");
            RestRequest _request = new("Candidates/SaveExperience", Method.Post)
                                   {
                                       RequestFormat = DataFormat.Json
                                   };
            _request.AddJsonBody(experience.Model);
            _request.AddQueryParameter("user", LoginCookyUser == null || LoginCookyUser.UserID.NullOrWhiteSpace() ? "JOLLY" : LoginCookyUser.UserID.ToUpperInvariant());
            _request.AddQueryParameter("candidateID", _target.ID);

            Dictionary<string, object> _response = await _client.PostAsync<Dictionary<string, object>>(_request);
            if (_response == null)
            {
                return;
            }

            _candidateExperienceObject = General.DeserializeObject<List<CandidateExperience>>(_response["Experience"]);
        }
        catch
        {
            //
        }

        await Task.Delay(1);
    }

    private async Task SaveMPC(EditContext editContext)
    {
        await Task.Delay(1);
        try
        {
            RestClient _client = new($"{Start.ApiHost}");
            RestRequest _request = new("Candidates/SaveMPC", Method.Post)
                                   {
                                       RequestFormat = DataFormat.Json
                                   };
            _request.AddJsonBody(editContext.Model);
            _request.AddQueryParameter("user", LoginCookyUser == null || LoginCookyUser.UserID.NullOrWhiteSpace() ? "JOLLY" : LoginCookyUser.UserID.ToUpperInvariant());

            Dictionary<string, object> _response = await _client.PostAsync<Dictionary<string, object>>(_request);
            if (_response != null)
            {
                _candidateMPCObject = General.DeserializeObject<List<CandidateMPC>>(_response["MPCList"]);
                RatingMPC = JsonConvert.DeserializeObject<CandidateRatingMPC>(_response["FirstMPC"]?.ToString() ?? string.Empty);
                GetMPCDate();
                GetMPCNote();
            }
        }
        catch
        {
            //
        }

        await Task.Delay(1);
    }

    private async Task SaveNotes(EditContext notes)
    {
        await Task.Delay(1);

        try
        {
            RestClient _client = new($"{Start.ApiHost}");
            RestRequest _request = new("Candidates/SaveNotes", Method.Post)
                                   {
                                       RequestFormat = DataFormat.Json
                                   };
            _request.AddJsonBody(notes.Model);
            _request.AddQueryParameter("user", LoginCookyUser == null || LoginCookyUser.UserID.NullOrWhiteSpace() ? "JOLLY" : LoginCookyUser.UserID.ToUpperInvariant());
            _request.AddQueryParameter("candidateID", _target.ID);

            Dictionary<string, object> _response = await _client.PostAsync<Dictionary<string, object>>(_request);
            if (_response == null)
            {
                return;
            }

            _candidateNotesObject = General.DeserializeObject<List<CandidateNotes>>(_response["Notes"]);
        }
        catch
        {
            //
        }

        await Task.Delay(1);
    }

    private async Task SaveRating(EditContext editContext)
    {
        await Task.Delay(1);
        try
        {
            RestClient _client = new($"{Start.ApiHost}");
            RestRequest _request = new("Candidates/SaveRating", Method.Post)
                                   {
                                       RequestFormat = DataFormat.Json
                                   };
            _request.AddJsonBody(editContext.Model);
            _request.AddQueryParameter("user", LoginCookyUser == null || LoginCookyUser.UserID.NullOrWhiteSpace() ? "JOLLY" : LoginCookyUser.UserID.ToUpperInvariant());

            Dictionary<string, object> _response = await _client.PostAsync<Dictionary<string, object>>(_request);
            if (_response != null)
            {
                _candidateRatingObject = General.DeserializeObject<List<CandidateRating>>(_response["RatingList"]);
                RatingMPC = JsonConvert.DeserializeObject<CandidateRatingMPC>(_response["FirstRating"]?.ToString() ?? string.Empty);
                _candidateDetailsObject.RateCandidate = RatingMPC.Rating;
                GetRatingDate();
                GetRatingNote();
            }
        }
        catch
        {
            //
        }

        await Task.Delay(1);
    }

    private async Task SaveSkill(EditContext skill)
    {
        await Task.Delay(1);

        try
        {
            RestClient _client = new($"{Start.ApiHost}");
            RestRequest _request = new("Candidates/SaveSkill", Method.Post)
                                   {
                                       RequestFormat = DataFormat.Json
                                   };
            _request.AddJsonBody(skill.Model);
            _request.AddQueryParameter("user", LoginCookyUser.UserID.ToUpperInvariant());
            _request.AddQueryParameter("candidateID", _target.ID);

            Dictionary<string, object> _response = await _client.PostAsync<Dictionary<string, object>>(_request);
            if (_response == null)
            {
                return;
            }

            _candidateSkillsObject = General.DeserializeObject<List<CandidateSkills>>(_response["Skills"]);
        }
        catch
        {
            //
        }

        await Task.Delay(1);
    }

    private async Task SearchCandidate(EditContext arg)
    {
        await Task.Delay(1);
        await LocalStorageBlazored.SetItemAsync("CandidateGrid", arg.Model as CandidateSearch);
        await Grid.Refresh();
    }

    private async Task SetAlphabet(string alphabet)
    {
        SearchModel.Name = alphabet;
        _currentPage = 1;
        SearchModel.Page = _currentPage;
        await LocalStorageBlazored.SetItemAsync("CandidateGrid", SearchModel);
        //_ = new StorageCompression(SessionStorage).SetCandidateGrid();
        await Grid.Refresh();
    }

    private void SetCommunication()
    {
        string _returnValue = _candidateDetailsObject.Communication switch
                              {
                                  "G" => "Good",
                                  "A" => "Average",
                                  "X" => "Excellent",
                                  _ => "Fair"
                              };

        CandidateCommunication = _returnValue.ToMarkupString();
    }

    private void SetEligibility()
    {
        if (_eligibility is {Count: > 0})
        {
            CandidateEligibility = _candidateDetailsObject.EligibilityID > 0
                                       ? _eligibility.FirstOrDefault(eligibility => eligibility.Key == _candidateDetailsObject.EligibilityID)!.Value.ToMarkupString()
                                       : "".ToMarkupString();
        }
    }

    private void SetExperience()
    {
        if (_experience is {Count: > 0})
        {
            CandidateExperience = _candidateDetailsObject.ExperienceID > 0
                                      ? _experience.FirstOrDefault(experience => experience.Key == _candidateDetailsObject.ExperienceID).Value.ToMarkupString()
                                      : "".ToMarkupString();
        }
    }

    private void SetJobOption()
    {
        string _returnValue = "";
        if (_jobOptions is {Count: > 0})
        {
            string[] _splitJobOptions = _candidateDetailsObject.JobOptions.Split(',');
            foreach (string _str in _splitJobOptions)
            {
                if (_str == "")
                {
                    continue;
                }

                if (_returnValue != "")
                {
                    _returnValue += ", " + _jobOptions.FirstOrDefault(jobOption => jobOption.Key == _str)?.Value;
                }
                else
                {
                    _returnValue = _jobOptions.FirstOrDefault(jobOption => jobOption.Key == _str)?.Value;
                }
            }
        }

        CandidateJobOptions = _returnValue.ToMarkupString();
    }

    private void SetTaxTerm()
    {
        string _returnValue = "";
        /*IMemoryCache _memoryCache = Start.MemCache;
        _memoryCache.TryGetValue("TaxTerms", out List<KeyValues> _taxTerms);*/

        if (_taxTerms is {Count: > 0})
        {
            string[] _splitTaxTerm = _candidateDetailsObject.TaxTerm.Split(',');
            foreach (string _str in _splitTaxTerm)
            {
                if (_str == "")
                {
                    continue;
                }

                if (_returnValue != "")
                {
                    _returnValue += ", " + _taxTerms.FirstOrDefault(taxTerm => taxTerm.Key == _str)?.Value;
                }
                else
                {
                    _returnValue = _taxTerms.FirstOrDefault(taxTerm => taxTerm.Key == _str)?.Value;
                }
            }
        }

        CandidateTaxTerms = _returnValue.ToMarkupString();
    }

    private void SetupAddress()
    {
        //NumberOfLines = 1;
        string _generateAddress = _candidateDetailsObject.Address1;

        if (_generateAddress == "")
        {
            _generateAddress = _candidateDetailsObject.Address2;
        }
        else
        {
            _generateAddress += _candidateDetailsObject.Address2 == "" ? "" : "<br/>" + _candidateDetailsObject.Address2;
        }

        if (_generateAddress == "")
        {
            _generateAddress = _candidateDetailsObject.City;
        }
        else
        {
            _generateAddress += _candidateDetailsObject.City == "" ? "" : "<br/>" + _candidateDetailsObject.City;
        }

        if (_candidateDetailsObject.StateID > 0)
        {
            if (_generateAddress == "")
            {
                _generateAddress = _states.FirstOrDefault(state => state.Key == _candidateDetailsObject.StateID)?.Value?.Split('-')[0].Trim();
                // CandidateDetailsObject.State;
            }
            else
            {
                try //Because sometimes the default values are not getting set. It's so random that it can't be debugged. And it never fails during debugging session.
                {
                    _generateAddress += ", " + _states.FirstOrDefault(state => state.Key == _candidateDetailsObject.StateID)?.Value?.Split('-')[0].Trim();
                }
                catch
                {
                    //
                }
                //+ CandidateDetailsObject.State;
            }
        }

        if (_candidateDetailsObject.ZipCode != "")
        {
            if (_generateAddress == "")
            {
                _generateAddress = _candidateDetailsObject.ZipCode;
            }
            else
            {
                _generateAddress += ", " + _candidateDetailsObject.ZipCode;
            }
        }

        //NumberOfLines = _generateAddress.Split("<br/>").Length;

        Address = _generateAddress.ToMarkupString();
    }

    private async Task SubmitCandidateToRequisition(EditContext arg)
    {
        await Task.Delay(1);

        try
        {
            RestClient _client = new($"{Start.ApiHost}");
            RestRequest _request = new("Candidates/SubmitCandidateRequisition", Method.Post)
                                   {
                                       RequestFormat = DataFormat.Json
                                   };
            _request.AddQueryParameter("requisitionID", RequisitionID);
            _request.AddQueryParameter("candidateID", _target.ID);
            _request.AddQueryParameter("notes", SubmitCandidateModel.Text);
            _request.AddQueryParameter("user", LoginCookyUser == null || LoginCookyUser.UserID.NullOrWhiteSpace() ? "JOLLY" : LoginCookyUser.UserID.ToUpperInvariant());

            Dictionary<string, object> _response = await _client.PostAsync<Dictionary<string, object>>(_request);
            if (_response == null)
            {
                return;
            }

            _candidateActivityObject = General.DeserializeObject<List<CandidateActivity>>(_response["Activity"]);
        }
        catch
        {
            //
        }

        await Task.Delay(1);
    }

    private async Task SubmitSelectedCandidate(MouseEventArgs arg)
    {
        await Task.Delay(1);

        SubmitCandidateModel.ClearData();
        await DialogSubmitCandidate.Dialog.ShowAsync();
    }

    private async Task TabSelected(SelectEventArgs args)
    {
        await Task.Delay(1);
        _selectedTab = args.SelectedIndex;
    }

    private async Task UndoActivity(int activityID)
    {
        await Task.Delay(1);

        try
        {
            RestClient _client = new($"{Start.ApiHost}");
            RestRequest _request = new("Candidates/UndoCandidateActivity", Method.Post)
                                   {
                                       RequestFormat = DataFormat.Json
                                   };
            _request.AddQueryParameter("submissionID", activityID);
            _request.AddQueryParameter("user", LoginCookyUser == null || LoginCookyUser.UserID.NullOrWhiteSpace() ? "JOLLY" : LoginCookyUser.UserID.ToUpperInvariant());

            Dictionary<string, object> _response = await _client.PostAsync<Dictionary<string, object>>(_request);
            if (_response == null)
            {
                return;
            }

            _candidateActivityObject = General.DeserializeObject<List<CandidateActivity>>(_response["Activity"]);
        }
        catch
        {
            //
        }

        await Task.Delay(1);
    }

    private async Task UploadDocument(UploadChangeEventArgs file)
    {
        await Task.Delay(1);
        foreach (UploadFiles _file in file.Files)
        {
            AddedDocument = _file;
        }
    }

    public class AdminCandidateAdaptor : DataAdaptor
    {
        #region Methods

        /// <summary>Performs data Read operation synchronously.</summary>
        public override Task<object> ReadAsync(DataManagerRequest dm, string key = null)
        {
            Task<object> _candidateReturn = General.GetCandidateReadAdaptor(SearchModel, dm);
            //Count = ((DataResult)_candidateReturn.Result).Count;
            Grid.SelectRowAsync(0);
            return _candidateReturn;
        }

        #endregion
    }

    public class AdminCandidateDropDownAdaptor : DataAdaptor
    {
        #region Methods

        public override Task<object> ReadAsync(DataManagerRequest dm, string key = null) => General.GetAutocompleteAsync("SearchCandidate", "@Candidate", dm);

        #endregion
    }
}