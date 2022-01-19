#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_AppTrack
// File Name:           Candidate.razor.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          01-06-2022 15:41
// Last Updated On:     01-16-2022 19:32
// *****************************************/

#endregion

#region Using

using Syncfusion.Blazor.Spinner;

using ChangeEventArgs = Microsoft.AspNetCore.Components.ChangeEventArgs;
using FileInfo = Syncfusion.Blazor.Inputs.FileInfo;

#endregion

namespace ProfSvc_AppTrack.Pages;

public partial class Candidate
{
    #region Fields

    public CandidateRatingMPC RatingMPC = new();

    private readonly Candidates _candidateContext = new();
    private readonly Dictionary<string, object> _htmlAttributes = new() {{"maxlength", "50"}, {"minlength", "1"}, {"rows", "1"}};
    private readonly Dictionary<string, object> _htmlAttributes1 = new() {{"maxlength", "500"}, {"minlength", "1"}, {"rows", "4"}};

    private readonly List<IntValues> _showRecords =
        new() {new(10, "10 rows"), new(25, "25 rows"), new(50, "50 rows"), new(75, "75 rows"), new(100, "100 rows")};

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

    private bool _dontChangePageDetails = true;

    private CandidateDetails _candidateDetailsObject = new();
    private CandidateDetails _candidateDetailsObjectClone = new();

    private Candidates _target;

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

    private EditContext _statusEditContext;

    private int _currentPage = 1;

    private List<CandidateActivity> _candidateActivityObject = new();
    private List<CandidateEducation> _candidateEducationObject = new();
    private List<CandidateExperience> _candidateExperienceObject = new();
    private List<CandidateMPC> _candidateMPCObject = new();
    private List<CandidateNotes> _candidateNotesObject = new();
    private List<CandidateRating> _candidateRatingObject = new();
    private List<CandidateSkills> _candidateSkillsObject = new();

    private List<ToolbarItemModel> _tools = new()
                                            {
                                                /*new ToolbarItemModel() { Command = ToolbarCommand.Bold },
                                                new ToolbarItemModel() { Command = ToolbarCommand.Italic },
                                                new ToolbarItemModel() { Command = ToolbarCommand.Underline },
                                                new ToolbarItemModel() { Command = ToolbarCommand.StrikeThrough },
                                                new ToolbarItemModel() { Command = ToolbarCommand.FontName },
                                                new ToolbarItemModel() { Command = ToolbarCommand.FontSize },
                                                new ToolbarItemModel() { Command = ToolbarCommand.FontColor },
                                                new ToolbarItemModel() { Command = ToolbarCommand.BackgroundColor },
                                                new ToolbarItemModel() { Command = ToolbarCommand.LowerCase },
                                                new ToolbarItemModel() { Command = ToolbarCommand.UpperCase },
                                                new ToolbarItemModel() { Command = ToolbarCommand.SuperScript },
                                                new ToolbarItemModel() { Command = ToolbarCommand.SubScript },
                                                new ToolbarItemModel() { Command = ToolbarCommand.Separator },
                                                new ToolbarItemModel() { Command = ToolbarCommand.Formats },
                                                new ToolbarItemModel() { Command = ToolbarCommand.Alignments },
                                                new ToolbarItemModel() { Command = ToolbarCommand.ClearFormat },
                                                new ToolbarItemModel() { Command = ToolbarCommand.Separator },
                                                new ToolbarItemModel() { Command = ToolbarCommand.Undo },
                                                new ToolbarItemModel() { Command = ToolbarCommand.Redo }*/
                                            };

    #endregion

    #region Properties

    public static CandidateGrid CandidateGridPersistValues
    {
        get;
        set;
    } = new(1, 25, "");

    public static decimal Count
    {
        get;
        set;
    }

    public static int PageCount
    {
        get;
        set;
    }

    public static int ItemCount
    {
        get;
        set;
    } = 25;

    public static int StartRecord
    {
        get;
        set;
    } = 0;

    public static int EndRecord
    {
        get;
        set;
    } = 0;

    public static List<IntValues> Eligibility
    {
        get;
        set;
    } = new();

    public static List<IntValues> Experience
    {
        get;
        set;
    } = new();

    public static List<IntValues> States
    {
        get;
        set;
    } = new();

    public static List<KeyValues> Communication
    {
        get;
        set;
    } = new();

    public static List<KeyValues> JobOptions
    {
        get;
        set;
    } = new();

    public static List<KeyValues> StatusCodes
    {
        get;
        set;
    } = new();

    public static List<KeyValues> TaxTerms
    {
        get;
        set;
    } = new();

    public static string Name
    {
        get;
        set;
    } = "";

    private AutoCompleteButton AutoCompleteControl
    {
        get;
        set;
    }

    private static bool _getStates = true;

    private static bool _valueChanged = true;

    private bool FirstRender
    {
        get;
        set;
    } = true;

    /*private int Identifier
	{
		get;
		set;
	}*/

    private bool IsAdd
    {
        get;
        set;
    }

    private bool VisibleCandidateInfo
    {
        get;
        set;
    }

    private bool VisibleSkillDialog
    {
        get;
        set;
    } = false;

    private bool VisibleEducationDialog
    {
        get;
        set;
    }

    private bool VisibleExperienceDialog
    {
        get;
        set;
    }

    private bool VisibleNotesDialog
    {
        get;
        set;
    }

    private bool VisibleActivityDialog
    {
        get;
        set;
    }

    private bool VisibleMPCCandidate
    {
        get;
        set;
    }

    private bool VisibleNewCandidate
    {
        get;
        set;
    }

    private bool VisibleRatingCandidate
    {
        get;
        set;
    }

    private bool VisibleProperty
    {
        get;
        set;
    }

    private bool SpinnerVisible
    {
        get;
        set;
    }

    private const byte RowHeight = 38;

    private static decimal CurrentPage
    {
        get;
        set;
    } = 1;

    private double FileSize
    {
        get;
        set;
    }

    private FileInfo FileInformation
    {
        get;
        set;
    }

    private static IHttpClientFactory _clientFactory;

    [Inject]
    private IHttpClientFactory Client
    {
        set => _clientFactory = value;
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

    private int Code
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

    private MarkupString RatingDate
    {
        get;
        set;
    }

    private MarkupString RatingNote
    {
        get;
        set;
    }

    private MarkupString Address
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

    private MarkupString CandidateExperience
    {
        get;
        set;
    }

    private MemoryStream FileData
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

    private SfGrid<CandidateActivity> GridActivity
    {
        get;
        set;
    }

    private SfGrid<CandidateEducation> GridEducation
    {
        get;
        set;
    }

    private SfGrid<CandidateExperience> GridExperience
    {
        get;
        set;
    }

    private SfGrid<CandidateMPC> GridMPC
    {
        get;
        set;
    }

    private SfGrid<CandidateNotes> GridNotes
    {
        get;
        set;
    }

    private SfGrid<CandidateRating> GridRating
    {
        get;
        set;
    }

    private static SfGrid<Candidates> Grid
    {
        get;
        set;
    }

    private SfGrid<CandidateSkills> GridSkill
    {
        get;
        set;
    }

    private SfSpinner Spinner
    {
        get;
        set;
    } = new();

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

    private static string GetState(int id) => States.FirstOrDefault(state => state.Key == id)?.Value.Split('-')[0].Trim();

    private string MimeType
    {
        get;
        set;
    }

    private string FileName
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

    [JSInvokable("DetailCollapse")]
    public void DetailRowCollapse() => _target = null;

    //public async Task RecordClickHandler(RecordClickEventArgs<Candidates> args)
    //{
    //    await Grid.ExpandCollapseDetailRowAsync(args.RowData);
    //}

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender)
        {
            _valueChanged = true;

            return;
        }

        //_memory.Set("Time", DateTime.Now.ToString(CultureInfo.InvariantCulture));
        if (_candidateContext != null)
        {
            _statusEditContext = new(_candidateContext);
        }

        CandidateGridPersistValues = await LocalStorageBlazored.GetItemAsync<CandidateGrid>("CandidateGrid") ?? new(1, 25, "");

        _currentPage = CandidateGridPersistValues.Page;
        PageCount = _currentPage + 1;

        StorageCompression _compression = new(SessionStorage);
        Cooky _cooky = await _compression.Get("GridVal");
        //CandidateGridPersistValues = await _compression.GetCandidateGrid();
        //CurrentPage = 1;
        ItemCount = CandidateGridPersistValues.ItemCount;
        if (_cooky.UserID.NullOrWhiteSpace())
        {
            //NavManager?.NavigateTo($"{NavManager.BaseUri}", true);
        }

        string _result = await LocalStorageBlazored.GetItemAsStringAsync("autoCandidate");

        FilterSet(_result);
    }

    private async Task DataHandler(object obj)
    {
        DotNetObjectReference<Candidate> _dotNetReference = DotNetObjectReference.Create(this); // create dotnet ref
        await _runtime.InvokeAsync<string>("detail", _dotNetReference);
        //  send the dotnet ref to JS side
        FirstRender = false;
        //Count = Count;
        await Grid.SelectRowAsync(0);
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
        await Task.Delay(5);
        await Spinner.ShowAsync();
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

        await Task.Delay(5);
        await Spinner.HideAsync();
    }

    private void GetMPCDate()
    {
        string _mpcDate = "";
        if (_candidateDetailsObject.MpcNotes == "")
        {
            MPCDate = _mpcDate.ToMarkupString();
        }

        CandidateMPC _candidateMPCObjectFirst = _candidateMPCObject.OrderByDescending(x => x.Date).FirstOrDefault();
        if (_candidateMPCObjectFirst != null)
        {
            _mpcDate =
                $"{_candidateMPCObjectFirst.Date.ToString("d", new CultureInfo("en-us"))} [{string.Concat(_candidateMPCObjectFirst.User.Where(char.IsLetter))}]";
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

        CandidateMPC _candidateMPCObjectFirst = _candidateMPCObject.OrderByDescending(x => x.Date).FirstOrDefault();
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

        CandidateRating _candidateRatingObjectFirst = _candidateRatingObject.OrderByDescending(x => x.Date).FirstOrDefault();
        if (_candidateRatingObjectFirst != null)
        {
            _ratingDate =
                $"{_candidateRatingObjectFirst.Date.ToString("d", new CultureInfo("en-us"))} [{string.Concat(_candidateRatingObjectFirst.User.Where(char.IsLetter))}]";
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

        CandidateRating _candidateRatingObjectFirst = _candidateRatingObject.OrderByDescending(x => x.Date).FirstOrDefault();
        if (_candidateRatingObjectFirst != null)
        {
            _ratingNote = _candidateRatingObjectFirst.Comments;
        }

        RatingNote = _ratingNote.ToMarkupString();
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
                _generateAddress = States.FirstOrDefault(state => state.Key == _candidateDetailsObject.StateID)?.Value.Split('-')[0].Trim();
                // CandidateDetailsObject.State;
            }
            else
            {
                _generateAddress += ", " + States.FirstOrDefault(state => state.Key == _candidateDetailsObject.StateID)?.Value.Split('-')[0].Trim();
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
        CandidateEligibility = _candidateDetailsObject.EligibilityID > 0
                                   ? Eligibility.FirstOrDefault(eligibility => eligibility.Key == _candidateDetailsObject.EligibilityID).Value.ToMarkupString()
                                   : "".ToMarkupString();
    }

    private void SetExperience()
    {
        CandidateExperience = _candidateDetailsObject.ExperienceID > 0
                                  ? Experience.FirstOrDefault(experience => experience.Key == _candidateDetailsObject.ExperienceID).Value.ToMarkupString()
                                  : "".ToMarkupString();
    }

    private void SetJobOption()
    {
        string[] _splitJobOptions = _candidateDetailsObject.JobOptions.Split(',');
        string _returnValue = "";
        foreach (string _str in _splitJobOptions)
        {
            if (_str == "")
            {
                continue;
            }

            if (_returnValue != "")
            {
                _returnValue += ", " + JobOptions.FirstOrDefault(jobOption => jobOption.Key == _str)?.Value;
            }
            else
            {
                _returnValue = JobOptions.FirstOrDefault(jobOption => jobOption.Key == _str)?.Value;
            }
        }

        CandidateJobOptions = _returnValue.ToMarkupString();
    }

    private void SetTaxTerm()
    {
        string[] _splitTaxTerm = _candidateDetailsObject.TaxTerm.Split(',');
        string _returnValue = "";
        foreach (string _str in _splitTaxTerm)
        {
            if (_str == "")
            {
                continue;
            }

            if (_returnValue != "")
            {
                _returnValue += ", " + TaxTerms.FirstOrDefault(taxTerm => taxTerm.Key == _str)?.Value;
            }
            else
            {
                _returnValue = TaxTerms.FirstOrDefault(taxTerm => taxTerm.Key == _str)?.Value;
            }
        }

        CandidateTaxTerms = _returnValue.ToMarkupString();
    }

    private void RowDataBound(RowDataBoundEventArgs<Candidates> candidate)
    {
        //VisibleProperty = false;
    }

    private void AddNewCandidate()
    {
        VisibleNewCandidate = true;
    }

    private void CancelCandidate()
    {
        VisibleCandidateInfo = false;
    }

    private void CancelMPC()
    {
        VisibleMPCCandidate = false;
    }

    private async void CancelRating()
    {
        await Task.Delay(1);
        await RatingDialog.RatingDialog.HideAsync();
    }

    private void CancelSkill()
    {
        VisibleSkillDialog = false;
    }

    private void EditCandidate(int id)
    {
        Task<double> _index = Grid.GetRowIndexByPrimaryKey(id);
        Grid.SelectRowAsync(_index.Result);
        Task.Yield();
        if (id == 0)
        {
            Title = "Add";
            IsAdd = true;
            Grid.AddRecord();
        }
        else
        {
            Title = "Edit";
            IsAdd = false;
            _candidateDetailsObjectClone = _candidateDetailsObject.Copy();
            VisibleCandidateInfo = true;
        }
    }

    public SkillPanel SkillPanel
    {
        get;
        set;
    }

    private void EditSkill(int id)
    {
        SelectedSkill = SkillPanel.SelectedRow;
        VisibleSkillDialog = true;
    }

    public CandidateSkills SelectedSkill
    {
        get;
        set;
    } = new();

    public EditSkillDialog EditSkillDialog
    {
        get;
        set;
    }

    private void EditEducation(int id)
    {
        VisibleEducationDialog = true;
    }

    private void EditExperience(int id)
    {
        VisibleExperienceDialog = true;
    }

    private void EditNotes(int id)
    {
        VisibleNotesDialog = true;
    }

    private void EditActivity(int id)
    {
        VisibleActivityDialog = true;
    }

    private async void FilterGrid(ChangeEventArgs<string, KeyValues> candidate)
    {
        if (!_valueChanged)
        {
            return;
        }

        Name = candidate.Value;
        CandidateGridPersistValues.Page = 1;
        await LocalStorageBlazored.SetItemAsync("CandidateGrid", CandidateGridPersistValues);
        //await new StorageCompression(SessionStorage).SetCandidateGrid();
        Grid.Refresh();
    }

    private void RefreshGrid() => Grid.Refresh();

    private async void SaveCandidate()
    {
        SpinnerVisible = true;
        await Task.Delay(1);
        _candidateDetailsObject = _candidateDetailsObjectClone.Copy();

        RestClient _client = new($"{Start.ApiHost}");
        RestRequest _request = new("Candidates/SaveCandidate", Method.Post)
                               {
                                   RequestFormat = DataFormat.Json
                               };
        _request.AddJsonBody(_candidateDetailsObject);

        await _client.PostAsync<int>(_request);

        _target.Name = _candidateDetailsObject.FirstName + " " + _candidateDetailsObject.LastName;
        _target.Phone = $"{_candidateDetailsObject.Phone1.ToInt64(): (###) ###-####}";
        _target.Email = _candidateDetailsObject.Email;
        _target.Location = _candidateDetailsObject.City + ", " + GetState(_candidateDetailsObject.StateID) + ", " + _candidateDetailsObject.ZipCode;
        _target.Updated = DateTime.Today.ToString("d", new CultureInfo("en-US")) + "[ADMIN]";
        _target.Status = "Available";
        SetupAddress();
        SetCommunication();
        SetEligibility();
        SetJobOption();
        SetTaxTerm();
        SetExperience();
        SpinnerVisible = false;
        await Task.Delay(1);
        VisibleCandidateInfo = false;
        StateHasChanged();
    }

    private void StateIDChanged(ChangeEventArgs<int, IntValues> args)
    {
        //IntValues _selectedItem = args.ItemData;
        //CandidateDetailsObject.State = _selectedItem.Value.Split('-')[0].Trim();
    }

    private void ToolTipOpen(TooltipEventArgs args)
    {
        _statusEditContext?.Validate();
        args.Cancel = !args.HasText;
    }

    private async void FirstClick()
    {
        if (_currentPage < 1)
        {
            _currentPage = 1;
        }

        _currentPage = 1;
        CandidateGridPersistValues.Page = _currentPage;
        await LocalStorageBlazored.SetItemAsync("CandidateGrid", CandidateGridPersistValues);
        //_ = new StorageCompression(SessionStorage).SetCandidateGrid();
        Grid.Refresh();
    }

    private async void PreviousClick()
    {
        if (_currentPage < 1)
        {
            _currentPage = 1;
        }

        _currentPage = CandidateGridPersistValues.Page <= 1 ? 1 : CandidateGridPersistValues.Page - 1;
        CandidateGridPersistValues.Page = _currentPage;
        await LocalStorageBlazored.SetItemAsync("CandidateGrid", CandidateGridPersistValues);
        //_ = new StorageCompression(SessionStorage).SetCandidateGrid();
        Grid.Refresh();
    }

    private async void NextClick()
    {
        if (_currentPage < 1)
        {
            _currentPage = 1;
        }

        _currentPage = CandidateGridPersistValues.Page >= PageCount.ToInt32() ? PageCount.ToInt32() : CandidateGridPersistValues.Page + 1;
        CandidateGridPersistValues.Page = _currentPage;
        await LocalStorageBlazored.SetItemAsync("CandidateGrid", CandidateGridPersistValues);
        //_ = new StorageCompression(SessionStorage).SetCandidateGrid();
        Grid.Refresh();
    }

    private async void LastClick()
    {
        if (_currentPage < 1)
        {
            _currentPage = 1;
        }

        _currentPage = PageCount.ToInt32();
        CandidateGridPersistValues.Page = _currentPage;
        await LocalStorageBlazored.SetItemAsync("CandidateGrid", CandidateGridPersistValues);
        //_ = new StorageCompression(SessionStorage).SetCandidateGrid();
        Grid.Refresh();
    }

    private async void PageNumberChanged(ChangeEventArgs obj)
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

        CandidateGridPersistValues.Page = _currentPage;
        await LocalStorageBlazored.SetItemAsync("CandidateGrid", CandidateGridPersistValues);
        //_ = new StorageCompression(SessionStorage).SetCandidateGrid();
        Grid.Refresh();
    }

    private async void SetAlphabet(string alphabet)
    {
        Name = alphabet;
        _currentPage = 1;
        CandidateGridPersistValues.Page = _currentPage;
        await LocalStorageBlazored.SetItemAsync("CandidateGrid", CandidateGridPersistValues);
        //_ = new StorageCompression(SessionStorage).SetCandidateGrid();
        Grid.Refresh();
    }

    private async void ClearFilter()
    {
        Name = "";
        _currentPage = 1;
        CandidateGridPersistValues.Page = _currentPage;
        await LocalStorageBlazored.SetItemAsync("CandidateGrid", CandidateGridPersistValues);
        //_ = new StorageCompression(SessionStorage).SetCandidateGrid();
        Grid.Refresh();
    }

    private async void AllAlphabet()
    {
        Name = "";
        _currentPage = 1;
        CandidateGridPersistValues.Page = _currentPage;
        await LocalStorageBlazored.SetItemAsync("CandidateGrid", CandidateGridPersistValues);
        //_ = new StorageCompression(SessionStorage).SetCandidateGrid();
        Grid.Refresh();
    }

    private void OnFileUpload(UploadChangeEventArgs file)
    {
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
                _request.AddFile("file", FileData.ToArray(), FileName, MimeType);
                //request.AddParameter("file", new ByteArrayContent(FileData.ToArray()));
                _request.AddParameter("filename", FileName, ParameterType.RequestBody);
                _request.AddParameter("filesize", FileSize, ParameterType.RequestBody);
                _request.AddParameter("mime", MimeType, ParameterType.RequestBody);

                _client.PostAsync(_request);
            }
            catch
            {
                //
            }
        }
    }

    private async void ChangeItemCount(ChangeEventArgs<int, IntValues> obj)
    {
        if (!_dontChangePageDetails)
        {
            _currentPage = 1;
            CandidateGridPersistValues.Page = _currentPage;
            ItemCount = obj.Value;

            await LocalStorageBlazored.SetItemAsync("CandidateGrid", CandidateGridPersistValues);
            Grid.Refresh();
        }
        else
        {
            _dontChangePageDetails = false;
            _currentPage = CandidateGridPersistValues.Page;
            await InvokeAsync(StateHasChanged);
        }
    }

    RatingCandidateDialog RatingDialog
    {
        get;
        set;
    }

    private async void EditRating(int candidateID)
    {
        await Task.Delay(1);
        await RatingDialog.RatingDialog.ShowAsync();
    }

    private void EditMPC(int candidateID)
    {
        VisibleMPCCandidate = true;
    }

    private async void SaveRating(EditContext obj)
    {
        try
        {
            RestClient _client = new($"{Start.ApiHost}");
            RestRequest _request = new("Candidates/SaveRating", Method.Post)
                                   {
                                       RequestFormat = DataFormat.Json
                                   };
            _request.AddJsonBody(obj.Model);
            _request.AddQueryParameter("user", "JOLLY");

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
    }

    private async void SaveSkill(EditContext skill)
    {
        await Task.Delay(1);
        await EditSkillDialog.SpinnerSkill.ShowAsync();
        await Task.Delay(1);
        await EditSkillDialog.SpinnerSkill.HideAsync();
    }

    private async void SaveMPC(EditContext obj)
    {
        try
        {
            RestClient _client = new($"{Start.ApiHost}");
            RestRequest _request = new("Candidates/SaveMPC", Method.Post)
                                   {
                                       RequestFormat = DataFormat.Json
                                   };
            _request.AddJsonBody(obj.Model);
            _request.AddQueryParameter("user", "JOLLY");

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
    }

    #endregion

    #region Nested

    public class AdminCandidateAdaptor : DataAdaptor
    {
        #region Methods

        /// <summary>Performs data Read operation synchronously.</summary>
        public override Task<object> ReadAsync(DataManagerRequest dm, string key = null)
        {
            Task<object> _candidateReturn = General.GetCandidateReadAdaptor(Name, dm, _getStates, CandidateGridPersistValues.Page, ItemCount);
            if (_getStates)
            {
                _getStates = false;
            }

            //Count = ((DataResult)_candidateReturn.Result).Count;
            Grid.SelectRowAsync(0);
            return _candidateReturn;
        }

        #endregion
    }

    public class AdminCandidateDropDownAdaptor : DataAdaptor
    {
        #region Methods

        public override Task<object> ReadAsync(DataManagerRequest dm, string key = null) =>
            General.GetReadAutocompleteAdaptor("SearchCandidate", "@Candidate", dm, _clientFactory);

        #endregion
    }

    #endregion
}