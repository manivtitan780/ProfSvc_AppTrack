#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_AppTrack
// File Name:           Extensions.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          01-26-2022 19:30
// Last Updated On:     02-12-2022 19:38
// *****************************************/

#endregion

namespace ProfSvc_AppTrack.Code;

public static partial class Extensions
{
    /// <summary>
    /// </summary>
    /// <param name="jsRuntime"></param>
    /// <param name="message"></param>
    /// <returns></returns>
    public static async ValueTask<bool> Confirm(this IJSRuntime jsRuntime, string message) => await jsRuntime.InvokeAsync<bool>("confirm", message);

    /// <summary>
    ///     Set Value to DBNull.Value if Integer Value is 0.
    /// </summary>
    /// <param name="i"> Integer whose property has to be checked. </param>
    /// <returns> Integer Value or DBNull.Value </returns>
    public static object DBNull(this double? i) => i == null || i == 0 ? System.DBNull.Value : i;

    /// <summary>
    ///     Set Value to DBNull.Value if Integer Value is 0.
    /// </summary>
    /// <param name="i"> Integer whose property has to be checked. </param>
    /// <returns> Integer Value or DBNull.Value </returns>
    public static object DBNull(this int i) => i == 0 ? System.DBNull.Value : i;

    /// <summary>
    ///     Set Value to DBNull.Value if String Value is Empty or Optionally "0".
    /// </summary>
    /// <param name="s"> String whose property has to be checked. </param>
    /// <param name="isZero"> If true additionally checks if String Value equals "0". </param>
    /// <returns> String Value or DBNull.Value </returns>
    public static object DBNull(this string s, bool isZero = false) => isZero ? string.IsNullOrWhiteSpace(s) || s.Trim() == "0" ? System.DBNull.Value : s.Trim() :
                                                                       string.IsNullOrWhiteSpace(s) ? System.DBNull.Value : s.Trim();

    /// <summary>
    ///     Converts a HTML-encoded string to a normal string.
    /// </summary>
    /// <param name="s"> The string to decode. </param>
    /// <returns> String </returns>
    public static string HtmlDecode(this string s) => HttpUtility.HtmlDecode(s);

    /// <summary>
    ///     Converts a string to a HTML-encoded string.
    /// </summary>
    /// <param name="s"> The string to encode. </param>
    /// <returns> String containing HTML encoded text </returns>
    public static string HtmlEncode(this string s) => HttpUtility.HtmlEncode(s);

    /// <summary>
    ///     Checks if the string is null or blank.
    /// </summary>
    /// <param name="s"> String </param>
    /// <returns> Boolean </returns>
    public static bool NullOrWhiteSpace(this string s) => string.IsNullOrWhiteSpace(s);

    /// <summary>
    ///     Checks if the object is null or blank.
    /// </summary>
    /// <param name="o"> Object </param>
    /// <returns> Boolean </returns>
    public static bool NullOrWhiteSpace(this object o) => o == null || o.ToString().NullOrWhiteSpace();

    /*/// <summary>
    ///     Set Value to DBNull.Value if Double Value is null or Optionally "0".
    /// </summary>
    /// <param name="s"> String whose property has to be checked. </param>
    /// <param name="isZero"> If true additionally checks if String Value equals "0". </param>
    /// <returns> String Value or DBNull.Value </returns>
    public static object DBNull(this double? s, bool isZero = false) =>
        isZero ? s == null || s == 0 ? (object) System.DBNull.Value : s : s ?? (object) System.DBNull.Value;*/

    /// <summary>
    /// </summary>
    /// <param name="p"></param>
    /// <param name="negative"></param>
    /// <returns></returns>
    public static int RandomNumber(this Page p, bool negative = true) => !negative ? RandomNumberGenerator.GetInt32(0, 5000) : RandomNumberGenerator.GetInt32(-5000, 5000);

    /// <summary>
    ///     Converts a string containing 0 or 1 to Boolean
    /// </summary>
    /// <param name="s">string which needs to be converted</param>
    /// <returns>Boolean</returns>
    public static bool StringToBoolean(this string s) => s == "1";

    /// <summary>
    /// Convert a string to Base64 converted string.
    /// </summary>
    /// <param name="s">The string to convert.</param>
    /// <returns>Base64 converted string.</returns>
    public static string ToBase64String(this string s)
    {
        byte[] _bytes = Encoding.UTF8.GetBytes(s);
        return Convert.ToBase64String(_bytes);
    }

    /// <summary>
    /// Convert a Base64 string to plain string.
    /// </summary>
    /// <param name="s">Base64 string.</param>
    /// <returns>The raw string.</returns>
    public static string FromBase64String(this string s)
    {
        byte[] _bytes = Convert.FromBase64String(s);
        return Encoding.UTF8.GetString(_bytes);
    }

    /// <summary>
    /// </summary>
    /// <param name="s"></param>
    /// <param name="byteArray"></param>
    /// <returns></returns>
    public static string ToBase64ByteArray(this Stream s, out byte[] byteArray)
    {
        string _base64String;
        byte[] _binaryData;

        try
        {
            using BinaryReader _reader = new(s);
            _binaryData = _reader.ReadBytes((int)s.Length);
        }
        catch (Exception _exp)
        {
            byteArray = null;

            return _exp.Message;
        }

        try
        {
            _base64String = Convert.ToBase64String(_binaryData, 0, _binaryData.Length);
        }
        catch (Exception _exp)
        {
            byteArray = null;

            return _exp.Message;
        }

        try
        {
            byteArray = Convert.FromBase64String(_base64String);
        }
        catch (Exception _exp)
        {
            byteArray = null;

            return _exp.Message;
        }

        return ""; //no error message on return
    }

    /// <summary>
    ///     Decodes a string from URL Safe string.
    /// </summary>
    /// <param name="s"> String </param>
    /// <returns> string </returns>
    public static string UrlDecode(this string s) => HttpUtility.UrlDecode(s);

    /// <summary>
    ///     Encodes a string to URL Safe string.
    /// </summary>
    /// <param name="s"> String </param>
    /// <returns> string </returns>
    public static string UrlEncode(this string s) => HttpUtility.UrlEncode(s);
}