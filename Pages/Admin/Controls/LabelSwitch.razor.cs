#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_AppTrack
// File Name:           LabelSwitch.razor.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          11-18-2021 19:59
// Last Updated On:     01-04-2022 16:04
// *****************************************/

#endregion

#region Using

#endregion

namespace ProfSvc_AppTrack.Pages.Admin.Controls;

public partial class LabelSwitch:ComponentBase
{
    #region Fields

    private bool _value;

    #endregion

    #region Properties

    [Parameter]
    public bool BindValue
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

    [Parameter]
    public EventCallback<bool> BindValueChanged
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
    public string OffLabel
    {
        get;
        set;
    }

    [Parameter]
    public string OnLabel
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

    #endregion

    #region Methods

    protected override async void OnInitialized()
    {
        await base.OnInitializedAsync();
    }

    #endregion
}