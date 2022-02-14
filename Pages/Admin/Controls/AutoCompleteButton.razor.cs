#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_AppTrack
// File Name:           AutoCompleteButton.razor.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          01-26-2022 19:30
// Last Updated On:     02-12-2022 19:41
// *****************************************/

#endregion

namespace ProfSvc_AppTrack.Pages.Admin.Controls;

/// <summary>
/// </summary>
public partial class AutoCompleteButton
{
    /// <summary>
    /// </summary>
    public SfAutoComplete<string, KeyValues> Acb { get; set; }

    /// <summary>
    /// </summary>
    [Parameter]
    public EventCallback ButtonClick
    {
        get;
        set;
    }

    [Parameter]
    public string ID
    {
        get;
        set;
    }

    /// <summary>
    /// </summary>
    [Parameter]
    public string PlaceholderText
    {
        get;
        set;
    } = "Select a ...";

    /// <summary>
    /// </summary>
    [Parameter]
    public object Ref
    {
        get;
        set;
    }

    /// <summary>
    /// </summary>
    [Parameter]
    public Type TypeInstance
    {
        get;
        set;
    }

    /// <summary>
    /// </summary>
    public string Value => Acb.Value;

    /// <summary>
    /// </summary>
    [Parameter]
    public EventCallback<ChangeEventArgs<string, KeyValues>> ValueChange
    {
        get;
        set;
    }
}