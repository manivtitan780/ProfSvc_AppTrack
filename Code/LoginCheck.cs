#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_AppTrack
// File Name:           LoginCheck.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          02-13-2022 20:29
// Last Updated On:     02-13-2022 20:29
// *****************************************/

#endregion

namespace ProfSvc_AppTrack.Code;

public class LoginCheck
{
    public async void RedirectLogin(StorageCompression storageCompression, NavigationManager navigationManager)
    {
        LoginCooky _loginCooky = await storageCompression.Get("GridVal");
        if (_loginCooky.UserID.NullOrWhiteSpace())
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
                navigationManager?.NavigateTo($"{navigationManager.BaseUri}Admin/Title", true);
                break;
        }
    }
}