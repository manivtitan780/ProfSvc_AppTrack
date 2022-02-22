#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_AppTrack
// File Name:           LoginCheck.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          02-13-2022 20:29
// Last Updated On:     02-14-2022 15:54
// *****************************************/

#endregion

namespace ProfSvc_AppTrack.Code;

public static class LoginCheck
{
    /// <summary>
    /// Redirect the User to Role-appropriate Page, from Login Page, if already signed in.
    /// </summary>
    /// <param name="navigationManager">The Navigation Manager object that is injected into the page.</param>
    /// <param name="blazoredStorage">Blazored.LocalStorage object that is injected into the page.</param>
    /// <returns>A Task.</returns>
    public static async Task RedirectLogin(this NavigationManager navigationManager, ILocalStorageService blazoredStorage)
    {
        string _cookyString = await blazoredStorage.GetItemAsync<string>("DeliciousCookie");
        LoginCooky _loginCooky = null;
        if (_cookyString.NullOrWhiteSpace())
        {
            return;
        }

        AESCryptography _aes = new();
        string _deserializedText = _aes.Decrypt(_cookyString);
        _loginCooky = JsonConvert.DeserializeObject<LoginCooky>(_deserializedText);

        if (_loginCooky == null || _loginCooky.UserID.NullOrWhiteSpace())
        {
            return;
        }

        switch (_loginCooky.RoleID)
        {
            case "RC":
            case "RS":
            case "SM":
                navigationManager.NavigateTo($"{navigationManager.BaseUri}candidate", true);
                break;
            case "AD":
                navigationManager.NavigateTo($"{navigationManager.BaseUri}Admin/Title", true);
                break;
        }
    }

    /// <summary>
    /// Redirect the User to Login Page, from Inner Pages, if not signed in.
    /// </summary>
    /// <param name="navigationManager">The Navigation Manager object that is injected into the page.</param>
    /// <param name="blazoredStorage">Blazored.LocalStorage object that is injected into the page.</param>
    /// <returns>A Task.</returns>
    public static async Task<LoginCooky> RedirectInner(this NavigationManager navigationManager, ILocalStorageService blazoredStorage)
    {
        string _cookyString = await blazoredStorage.GetItemAsync<string>("DeliciousCookie");
        LoginCooky _loginCooky = null;
        if (!_cookyString.NullOrWhiteSpace())
        {
            AESCryptography _aes = new();
            string _deserializedText = _aes.Decrypt(_cookyString);
            _loginCooky = JsonConvert.DeserializeObject<LoginCooky>(_deserializedText);
        }

        if (_loginCooky != null && !_loginCooky.UserID.NullOrWhiteSpace())
        {
            return _loginCooky;
        }

        navigationManager.NavigateTo($"{navigationManager.BaseUri}", true);
        return null;
    }
}