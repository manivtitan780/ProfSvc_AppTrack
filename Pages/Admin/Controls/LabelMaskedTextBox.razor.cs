#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_AppTrack
// File Name:           LabelMaskedTextBox.razor.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          01-29-2022 21:31
// Last Updated On:     01-29-2022 21:43
// *****************************************/

#endregion

namespace ProfSvc_AppTrack.Pages.Admin.Controls;

public partial class LabelMaskedTextBox
{
    private string _value;

    [Parameter]
    public bool CreateTooltip
    {
        get;
        set;
    } = true;

    [Parameter]
    public string CssClass
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

    [Parameter]
    public string Mask
    {
        get;
        set;
    }

    [Parameter]
    public string Placeholder
    {
        get;
        set;
    }

    [Parameter]
    public bool Readonly
    {
        get;
        set;
    }

    [Parameter]
    public Expression<Func<string>> ValidationMessage
    {
        get;
        set;
    }

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

    [Parameter]
    public string Width
    {
        get;
        set;
    } = "100%";

    private static void ToolTipOpen(TooltipEventArgs args) => args.Cancel = !args.HasText;
}