#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_AppTrack
// File Name:           LabelMaskedTextBox.razor.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          01-31-2022 19:26
// Last Updated On:     02-19-2022 21:00
// *****************************************/

#endregion

namespace ProfSvc_AppTrack.Pages.Admin.Controls;

/// <summary>
/// </summary>
public partial class LabelMaskedTextBox
{
    private string _value;

    [Parameter]
    public bool CreateDivTag
    {
        get;
        set;
    } = false;

    /// <summary>
    /// </summary>
    [Parameter]
    public bool CreateTooltip
    {
        get;
        set;
    } = true;

    /// <summary>
    /// </summary>
    [Parameter]
    public string CssClass
    {
        get;
        set;
    }

    /// <summary>
    /// </summary>
    [Parameter]
    public string ID
    {
        get;
        set;
    }

    /// <summary>
    /// </summary>
    [Parameter]
    public string Mask
    {
        get;
        set;
    }

    /// <summary>
    /// </summary>
    [Parameter]
    public string Placeholder
    {
        get;
        set;
    }

    /// <summary>
    /// </summary>
    [Parameter]
    public bool Readonly
    {
        get;
        set;
    }

    /// <summary>
    /// </summary>
    [Parameter]
    public Expression<Func<string>> ValidationMessage
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
    public EventCallback<string> ValueChanged
    {
        get;
        set;
    }

    /// <summary>
    /// </summary>
    [Parameter]
    public Expression<Func<string>> ValueExpression
    {
        get;
        set;
    }

    /// <summary>
    /// </summary>
    [Parameter]
    public string Width
    {
        get;
        set;
    } = "100%";

    private static void ToolTipOpen(TooltipEventArgs args) => args.Cancel = !args.HasText;
}