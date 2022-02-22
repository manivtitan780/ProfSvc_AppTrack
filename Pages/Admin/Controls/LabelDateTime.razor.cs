#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_AppTrack
// File Name:           LabelDateTime.razor.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          02-19-2022 15:10
// Last Updated On:     02-19-2022 15:33
// *****************************************/

#endregion

using FocusEventArgs = Microsoft.AspNetCore.Components.Web.FocusEventArgs;

namespace ProfSvc_AppTrack.Pages.Admin.Controls;

/// <summary>
/// </summary>
public partial class LabelDateTime
{
    private DateTime _value;

    /// <summary>
    /// </summary>
    [Parameter]
    public bool CreateDivTag
    {
        get;
        set;
    } = true;

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
    public SfDateTimePicker<DateTime> DateTimeBox
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
    public DateTime Max
    {
        get;
        set;
    } = DateTime.MaxValue;

    /// <summary>
    /// </summary>
    [Parameter]
    public DateTime Min
    {
        get;
        set;
    } = DateTime.MinValue;

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
    public DateTime Value
    {
        get => _value;
        set
        {
            if (EqualityComparer<DateTime?>.Default.Equals(value, _value))
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
    public EventCallback<DateTime> ValueChanged
    {
        get;
        set;
    }

    /// <summary>
    /// </summary>
    [Parameter]
    public Expression<Func<DateTime>> ValueExpression
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
    } = "30%";

    /// <summary>
    /// </summary>
    /// <param name="args"></param>
    private void ToolTipOpen(TooltipEventArgs args) => args.Cancel = !args.HasText;

    private async Task DateTimePickerFocus(FocusEventArgs args)
    {
        await Task.Delay(1);
        await DateTimeBox.ShowDatePopupAsync();
    }
}