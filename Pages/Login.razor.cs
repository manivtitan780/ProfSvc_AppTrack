#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_AppTrack
// File Name:           Login.razor.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          11-18-2021 19:59
// Last Updated On:     01-04-2022 16:11
// *****************************************/

#endregion

#region Using

#nullable enable
using System.Data;

#endregion

namespace ProfSvc_AppTrack.Pages;

public partial class Login
{
    #region Fields

    public Code.Login ModelLogin = new();

    #endregion

    #region Properties

    public EditContext? EditContext
    {
        get;
        set;
    }

    [CascadingParameter(Name = "IPAddress")]
    public string? IpAddress { get; set; }

    [Inject]
    private IConfiguration? Configuration { get; set; }

    [Inject]
    private NavigationManager? NavManager { get; set; }

    [Inject]
    private ProtectedLocalStorage? SessionStorage { get; set; }

    #endregion

    #region Methods

    protected async void CheckLogin(EditContext obj)
    {
        if (SessionStorage == null)
        {
            return;
        }

        byte[] _password = General.Md5PasswordHash(ModelLogin.Password.Trim());
        await using SqlConnection _connection = new(Configuration.GetConnectionString("DBConnect"));
        await using SqlCommand _command = new("ValidateCandidate", _connection)
                                          {
                                              CommandType = CommandType.StoredProcedure
                                          };
        _command.AddVarcharParameter("@User", 10, ModelLogin.UserName);
        _command.AddBinaryParameter("@Password", 16, _password);
        _command.AddVarcharParameter("@IP", 15, IpAddress);
        _connection.Open();
        await using SqlDataReader _reader = await _command.ExecuteReaderAsync();
        if (!_reader.HasRows)
        {
            return;
        }

        _reader.Read();

        StorageCompression _compression = new(SessionStorage);
        //ProtectedBrowserStorageResult<Cooky> _resultValue = await SessionStorage.GetAsync<Cooky>("GridVal");
        Cooky _cooky = await _compression.Get("GridVal");

        _cooky.UserID = ModelLogin.UserName.ToUpper(CultureInfo.InvariantCulture);
        _cooky.RoleID = _reader.GetString(3);
        _cooky.Role = _reader.GetString(5);
        _cooky.Email = _reader.GetString(2);
        _cooky.FirstName = _reader.GetString(0);
        _cooky.LastName = _reader.GetString(1);
        _cooky.Login = _reader.IsDBNull(4) ? DateTime.MinValue : _reader.GetDateTime(4);
        _cooky.LoginIp = _reader.GetNString(6);
        //await SessionStorage.SetAsync("GridVal", _cooky);
        await _compression.Set("GridVal", _cooky);
        switch (_cooky.RoleID)
        {
            case "RC":
            case "RS":
            case "SM":
                NavManager?.NavigateTo($"{NavManager.BaseUri}candidate", true);

                break;
            case "AD":
                NavManager?.NavigateTo($"{NavManager.BaseUri}Admin/Designation", true);

                break;
        }
    }

    protected override void OnAfterRender(bool firstRender)
    {
        base.OnAfterRender(firstRender);
        if (firstRender)
        {
            EditContext?.Validate();
        }
    }

    protected override async void OnInitialized()
    {
        EditContext = new(ModelLogin);
        StorageCompression _compression = new(SessionStorage);
        Cooky _cooky = await _compression.Get("GridVal");
        if (!_cooky.UserID.NullOrWhiteSpace())
        {
            switch (_cooky.RoleID)
            {
                case "RC":
                case "RS":
                case "SM":
                    NavManager?.NavigateTo($"{NavManager.BaseUri}candidate", true);

                    break;
                case "AD":
                    NavManager?.NavigateTo($"{NavManager.BaseUri}Admin/Designation", true);

                    break;
            }
        }

        await base.OnInitializedAsync();
    }

    #endregion
}