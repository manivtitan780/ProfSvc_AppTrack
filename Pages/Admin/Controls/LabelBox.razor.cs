#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_AppTrack
// File Name:           LabelBox.razor.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          01-31-2022 19:26
// Last Updated On:     02-12-2022 19:47
// *****************************************/

#endregion

namespace ProfSvc_AppTrack.Pages.Admin.Controls;

/// <summary>
/// </summary>
public partial class LabelBox
{
    private string _value;

    /// <summary>
    /// </summary>
    [Parameter]
    public EventCallback<string> BindValueChanged
    {
        get;
        set;
    }

    //[Parameter]
    //public object Context
    //{
    //	get;
    //	set;
    //}

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
    public int MaxLength
    {
        get;
        set;
    }

    /// <summary>
    /// </summary>
    [Parameter]
    public int MinLength
    {
        get;
        set;
    }

    /// <summary>
    /// </summary>
    [Parameter]
    public bool Multiline
    {
        get;
        set;
    }

    /// <summary>
    /// </summary>
    [Parameter]
    public EventCallback<TooltipEventArgs> OnOpen
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
    public RenderFragment ValidationTemplate
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
            if (_value == value)
            {
                return;
            }

            _value = value;
            BindValueChanged.InvokeAsync(value);
        }
    }

    /// <inheritdoc />
    protected override async void OnInitialized()
    {
        //if (Context != null)
        //{
        //	_context = new EditContext(Context);
        //}

        await Task.Yield();
        await base.OnInitializedAsync();
    }

    private static void ToolTipOpen(TooltipEventArgs args)
    {
        //_context?.Validate();
        args.Cancel = !args.HasText;
    }
}