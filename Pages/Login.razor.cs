#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_AppTrack
// File Name:           Login.razor.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          01-26-2022 19:30
// Last Updated On:     02-13-2022 20:49
// *****************************************/

#endregion

#region Using

#endregion

namespace ProfSvc_AppTrack.Pages;

public partial class Login
{
    public readonly Code.Login ModelLogin = new();

    [CascadingParameter(Name = "IPAddress")]
    public string IPAddress
    {
        get;
        set;
    }

    [Inject]
    private ILocalStorageService BlazoredLocalStorage
    {
        get;
        set;
    }

    [Inject]
    private IConfiguration Configuration
    {
        get;
        set;
    }

    private EditContext EditContext
    {
        get;
        set;
    }

    [Inject]
    private ProtectedLocalStorage LocalStorage
    {
        get;
        set;
    }

    [Inject]
    private NavigationManager NavManager { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);
        if (firstRender)
        {
            EditContext?.Validate();
            StorageCompression _compression = new(LocalStorage);
            //new LoginCheck().RedirectLogin(_compression, NavManager);
            LoginCooky _loginCooky = await _compression.Get("GridVal");
            if (!_loginCooky.UserID.NullOrWhiteSpace())
            {
                switch (_loginCooky.RoleID)
                {
                    case "RC":
                    case "RS":
                    case "SM":
                        NavManager?.NavigateTo($"{NavManager.BaseUri}candidate", true);

                        break;
                    case "AD":
                        NavManager?.NavigateTo($"{NavManager.BaseUri}Admin/Title", true);

                        break;
                }
            }
        }
    }

    protected override async Task OnInitializedAsync()
    {
        EditContext = new(ModelLogin);

        await base.OnInitializedAsync();
    }

    private async void CheckLogin(EditContext obj)
    {
        await Task.Delay(1);
        byte[] _password = General.Md5PasswordHash(ModelLogin.Password.Trim());
        RestClient _restClient = new($"{Start.ApiHost}");
        RestRequest _request = new("Login/Login", Method.Post)
                               {
                                   RequestFormat = DataFormat.Json
                               };
        _request.AddQueryParameter("userName", ModelLogin.UserName);
        _request.AddQueryParameter("password", Convert.ToBase64String(_password));
        _request.AddQueryParameter("ipAddress", IPAddress);
        LoginCooky _loginCooky = await _restClient.PostAsync<LoginCooky>(_request);

        if (_loginCooky == null)
        {
            return;
        }

        AESCryptography _aes = new AESCryptography();
        //_tripleDES.Encrypt();
        string _serializedLogin = JsonConvert.SerializeObject(_loginCooky);
        string _encryptedText = _aes.Encrypt(_serializedLogin);
        await BlazoredLocalStorage.SetItemAsStringAsync("DeliciousCookie", _encryptedText);
        //StorageCompression _compression = new(LocalStorage);
        //await _compression.Set("GridVal", _loginCooky);
        //new LoginCheck().RedirectLogin(_compression, NavManager);
        //switch (_loginCooky.RoleID)
        //{
        //    case "RC":
        //    case "RS":
        //    case "SM":
        //        NavManager?.NavigateTo($"{NavManager.BaseUri}candidate", true);

        //        break;
        //    case "AD":
        //        NavManager?.NavigateTo($"{NavManager.BaseUri}Admin/Designation", true);

        //        break;
        //}
    }
}