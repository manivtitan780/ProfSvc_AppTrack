#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_AppTrack
// File Name:           LabelDropDown.razor.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          01-31-2022 15:34
// Last Updated On:     01-31-2022 15:43
// *****************************************/

#endregion

using ChangeEventArgs = Microsoft.AspNetCore.Components.ChangeEventArgs;

namespace ProfSvc_AppTrack.Pages.Admin.Controls;

public partial class LabelDropDown<TValue, TItem>
{
    private TValue _value;

    [Parameter]
    public IEnumerable<TItem> DataSource
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
    public string TextField
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

    [Parameter]
    public EventCallback<ChangeEventArgs<TValue, TItem>> DropDownValueChange
    {
        get;
        set;
    }
}