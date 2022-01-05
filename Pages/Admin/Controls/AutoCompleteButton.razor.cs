#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_AppTrack
// File Name:           AutoCompleteButton.razor.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          11-18-2021 19:59
// Last Updated On:     01-04-2022 16:03
// *****************************************/

#endregion

#region Using

#endregion

namespace ProfSvc_AppTrack.Pages.Admin.Controls;

public partial class AutoCompleteButton
{
    #region Properties

    [Parameter]
    public EventCallback ButtonClick
    {
        get;
        set;
    }

    [Parameter]
    public EventCallback<ChangeEventArgs<string, KeyValues>> ValueChange
    {
        get;
        set;
    }

    [Parameter]
    public object Ref
    {
        get;
        set;
    }

    public SfAutoComplete<string, KeyValues> Acb { get; set; }

    [Parameter]
    public string ID
    {
        get;
        set;
    }

    [Parameter]
    public string PlaceholderText
    {
        get;
        set;
    } = "Select a ...";

    public string Value => Acb.Value;

    [Parameter]
    public Type TypeInstance
    {
        get;
        set;
    }

    #endregion

    //[Parameter]
    //public string Value
    //{
    //	get;
    //	set;
    //}
}