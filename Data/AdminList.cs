#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_AppTrack
// File Name:           AdminList.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          12-16-2021 19:27
// Last Updated On:     01-04-2022 16:03
// *****************************************/

#endregion

namespace ProfSvc_AppTrack.Data;

/// <summary>
/// </summary>
public class AdminList
{
    #region Constructors

    /// <summary>
    ///     Initializes the Admin List Class.
    /// </summary>
    public AdminList()
    {
        ClearData();
    }

    /// <summary>
    ///     Initializes the Admin List Class.
    /// </summary>
    /// <param name="id">ID of the Entity.</param>
    /// <param name="text">Text of the Entity.</param>
    /// <param name="created">Created Date of the Entity.</param>
    /// <param name="updated">Last Updated Date of the Entity.</param>
    /// <param name="enabled">Text Status for the Entity.</param>
    /// <param name="isEnabled">Status of the Entity.</param>
    /// <param name="methodName">Name of the Stored Procedure to execute.</param>
    /// <param name="isAdd">Is the Record Add or Edit?</param>
    public AdminList(int id, string text, string created, string updated, string enabled = "Active", bool isEnabled = true, string methodName = "",
                     bool isAdd = false)
    {
        ID = id;
        Text = text;
        CreatedDate = created;
        UpdatedDate = updated;
        Enabled = enabled;
        IsEnabled = isEnabled;
        IsAdd = isAdd;
        IsString = false;
        MethodName = methodName;
    }

    /// <summary>
    ///     Initializes the Admin List Class.
    /// </summary>
    /// <param name="code">Code of the Entity.</param>
    /// <param name="text">Text of the Entity.</param>
    /// <param name="created">Created Date of the Entity.</param>
    /// <param name="updated">Last Updated Date of the Entity.</param>
    /// <param name="enabled">Text Status for the Entity.</param>
    /// <param name="isEnabled">Status of the Entity.</param>
    /// <param name="methodName">Name of the Stored Procedure to execute.</param>
    /// <param name="isAdd">Is the Record Add or Edit?</param>
    public AdminList(string code, string text, string created, string updated, string enabled = "Active", bool isEnabled = true, string methodName = "",
                     bool isAdd = false)
    {
        Code = code;
        Text = text;
        CreatedDate = created;
        UpdatedDate = updated;
        Enabled = enabled;
        IsEnabled = isEnabled;
        IsAdd = isAdd;
        IsString = true;
        MethodName = methodName;
    }

    #endregion

    #region Properties

    /// <summary>
    ///     Is the Record Add or Edit?
    /// </summary>
    public bool IsAdd
    {
        get;
        set;
    }

    /// <summary>
    ///     Status of the Entity.
    /// </summary>
    public bool IsEnabled
    {
        get;
        set;
    }

    /// <summary>
    ///     Is the Primary key passed a String or Integer?
    /// </summary>
    public bool IsString
    {
        get;
        set;
    }

    /// <summary>
    ///     ID of the Entity.
    /// </summary>
    public int ID
    {
        get;
        set;
    }

    /// <summary>
    ///     Code of the Entity.
    /// </summary>
    [CodeExistsValidation]
    public string Code
    {
        get;
        set;
    } = "";

    /// <summary>
    ///     Created Date of the Entity.
    /// </summary>
    public string CreatedDate
    {
        get;
        set;
    }

    /// <summary>
    ///     Text Status for the Entity.
    /// </summary>
    public string Enabled
    {
        get;
        set;
    }

    /// <summary>
    ///     Name of the Stored Procedure to execute.
    /// </summary>
    public string MethodName
    {
        get;
        set;
    }

    /// <summary>
    ///     Text of the Entity.
    /// </summary>
    [Required(ErrorMessage = "This field is required."),
     StringLength(100, MinimumLength = 2, ErrorMessage = "Length should be between 2 and 100 characters.")]
    public string Text
    {
        get;
        set;
    }

    /// <summary>
    ///     Last Updated Date of the Entity.
    /// </summary>
    public string UpdatedDate
    {
        get;
        set;
    }

    #endregion

    #region Methods

    /// <summary>
    ///     Creates a shallow copy of the current object.
    /// </summary>
    /// <returns></returns>
    public AdminList Copy() => (AdminList)MemberwiseClone();

    /// <summary>
    ///     Clears this object data.
    /// </summary>
    public void ClearData()
    {
        ID = 0;
        Text = "";
        CreatedDate = "";
        UpdatedDate = "";
        Enabled = "Active";
        IsEnabled = true;
        IsAdd = false;
        IsString = false;
        MethodName = "";
        Code = "";
    }

    #endregion
}