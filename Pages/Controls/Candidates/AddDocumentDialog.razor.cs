#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_AppTrack
// File Name:           AddDocumentDialog.razor.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          02-25-2022 19:07
// Last Updated On:     02-25-2022 19:08
// *****************************************/

#endregion

namespace ProfSvc_AppTrack.Pages.Controls.Candidates;

public partial class AddDocumentDialog
{
    public SfDialog Dialog
    {
        get;
        set;
    }

    public SfSpinner Spinner
    {
        get;
        set;
    }

    [Parameter]
    public CandidateDocument ModelDocumentObject
    {
        get;
        set;
    }

    /// <summary>
    /// </summary>
    [Parameter]
    public EventCallback<MouseEventArgs> Cancel
    {
        get;
        set;
    }

    /// <summary>
    /// </summary>
    [Parameter]
    public EventCallback<EditContext> SaveDocument
    {
        get;
        set;
    }


    private async Task CancelDocumentDialog(MouseEventArgs arg)
    {
        await Task.Delay(1);
    }

    private async Task SaveDocumentDialog(EditContext arg)
    {
        await Task.Delay(1);
    }

    private async Task OnFileUpload(UploadChangeEventArgs arg)
    {
        await Task.Delay(1);

    }
}