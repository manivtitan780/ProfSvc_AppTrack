#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_AppTrack
// File Name:           LabelNumeric.razor.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          01-31-2022 19:26
// Last Updated On:     02-01-2022 16:21
// *****************************************/

#endregion

namespace ProfSvc_AppTrack.Pages.Admin.Controls;

public partial class LabelNumeric<TValue>
{
    private TValue _value;

    [Parameter]
    public bool CreateDivTag
    {
        get;
        set;
    } = true;

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
    public string Currency
    {
        get;
        set;
    } = "USD";

    [Parameter]
    public int? Decimals
    {
        get;
        set;
    } = 0;

    [Parameter]
    public string Format
    {
        get;
        set;
    } = "n";

    [Parameter]
    public string ID
    {
        get;
        set;
    }

    [Parameter]
    public TValue Max
    {
        get;
        set;
    }

    [Parameter]
    public TValue Min
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
    public Expression<Func<TValue>> ValidationMessage
    {
        get;
        set;
    }

    [Parameter]
    public TValue Value
    {
        get => _value;
        set
        {
            if (EqualityComparer<TValue>.Default.Equals(value, _value))
            {
                return;
            }

            _value = value;
            ValueChanged.InvokeAsync(value);
        }
    }

    [Parameter]
    public EventCallback<TValue> ValueChanged
    {
        get;
        set;
    }

    [Parameter]
    public Expression<Func<TValue>> ValueExpression
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