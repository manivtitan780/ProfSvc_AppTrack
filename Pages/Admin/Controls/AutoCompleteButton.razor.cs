#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_AppTrack
// File Name:           AutoCompleteButton.razor.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          01-26-2022 19:30
// Last Updated On:     03-14-2022 21:07
// *****************************************/

#endregion

namespace ProfSvc_AppTrack.Pages.Admin.Controls;

/// <summary>
/// </summary>
public partial class AutoCompleteButton
{
    private string _value;

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
    public bool EnablePersistence
    {
        get;
        set;
    } = true;

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
    [Parameter]
    public string Value
    {
        get => _value;
        set
        {
            if (EqualityComparer<string>.Default.Equals(value, _value))
            {
                return;
            }

            _value = value;
            ValueChanged.InvokeAsync(value);
        }
    }

    /// <summary>
    /// </summary>
    [Parameter]
    public EventCallback<ChangeEventArgs<string, KeyValues>> ValueChange
    {
        get;
        set;
    }

    [Parameter]
    public EventCallback<string> ValueChanged
    {
        get;
        set;
    }

    [Parameter]
    public Expression<Func<string>> ValueExpression
    {
        get;
        set;
    }
}