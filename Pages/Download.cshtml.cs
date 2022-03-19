#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_AppTrack
// File Name:           Download.cshtml.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          02-23-2022 19:23
// Last Updated On:     02-23-2022 21:49
// *****************************************/

#endregion

#region Using

using Microsoft.AspNetCore.Mvc;

#endregion

namespace ProfSvc_AppTrack.Pages;

public class DownloadModel : PageModel
{
    public DownloadModel(IWebHostEnvironment environment) => _environment = environment;

    private IWebHostEnvironment _environment;

    /// <summary>
    /// Downloads a File.
    /// </summary>
    /// <param name="file">Base64 encoded string containing Internal File Name, Candidate or Requisition or Company ID, Final Download File Name, and type of ID separated by a carat(^)</param>
    /// <returns></returns>
    public IActionResult OnGet(string file)
    {
        if (file.NullOrWhiteSpace())
        {
            return new OkResult();
        }

        string[] _decodedStringArray = file.FromBase64String().Split('^');
        if (_decodedStringArray.Length != 4)
        {
            return new OkResult();
        }

        string _type = _decodedStringArray[3] switch
                       {
                           "1" => "Requisition",
                           "2" => "Company",
                           _ => "Candidate"
                       };

        string _filePath = Path.Combine(Start.UploadsPath, "Uploads", _type, _decodedStringArray[1], _decodedStringArray[0]);
        byte[] _fileBytes = System.IO.File.ReadAllBytes(_filePath);
        return File(_fileBytes, "application/force-download", _decodedStringArray[2]);
    }
}