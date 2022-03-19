#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_AppTrack
// File Name:           LabelMultiSelect.razor.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          01-31-2022 19:26
// Last Updated On:     03-17-2022 21:16
// *****************************************/

#endregion

namespace ProfSvc_AppTrack.Pages.Admin.Controls;

public partial class LabelMultiSelect<TItem, TValue>
{
    private TValue _value;

    [Parameter]
    public bool CreateTooltip
    {
        get;
        set;
    } = true;

    [Parameter]
    public IEnumerable<TItem> DataSource
    {
        get;
        set;
    }

    [Parameter]
    public string FilterBarPlaceholder
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
    public string KeyField
    {
        get;
        set;
    }

    [Parameter]
    public VisualMode Mode
    {
        get;
        set;
    } = VisualMode.Box;

    [Parameter]
    public string Placeholder
    {
        get;
        set;
    }

    [Parameter]
    public Type TypeItem
    {
        get;
        set;
    }

    [Parameter]
    public Type TypeValue
    {
        get;
        set;
    }

    [Parameter]
    public bool UseCustomTemplate
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
    public string ValueField
    {
        get;
        set;
    }

    //[Parameter]
    //public Expression<Func<string>> ValueExpression
    //{
    //    get;
    //    set;
    //}

    [Parameter]
    public string Width
    {
        get;
        set;
    } = "100%";

    public RenderFragment ItemTemplate
    {
        get;
        set;
    }

    public RenderFragment ValueTemplate
    {
        get;
        set;
    }

    [Parameter]
    public bool CreateDivTag
    {
        get;
        set;
    }

    private static void ToolTipOpen(TooltipEventArgs args) => args.Cancel = !args.HasText;
}