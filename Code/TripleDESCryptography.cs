#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_AppTrack
// File Name:           TripleDESCryptography.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          01-26-2022 19:30
// Last Updated On:     02-13-2022 20:57
// *****************************************/

#endregion

namespace ProfSvc_AppTrack.Code;

/// <summary>
///     Summary description for TripleDES
/// </summary>
public class TripleDESCryptography
{
    /// <summary>
    /// </summary>
    /// <param name="key"></param>
    /// <param name="iv"></param>
    public TripleDESCryptography(byte[] key, byte[] iv)
    {
        //_tripleDes1.KeySize = 16;
        _tripleDes1.Key = key;
        _tripleDes1.IV = iv;
    }

    public TripleDESCryptography()
    {
        //_tripleDes1.KeySize = 16;
        //_tripleDes1.BlockSize = 64;
        //_tripleDes1.Mode = CipherMode.CFB;
        //_tripleDes1.FeedbackSize = 64;
        _tripleDes1.Key = Encoding.ASCII.GetBytes("12345678901234567890123456789012");
        _tripleDes1.IV = Encoding.ASCII.GetBytes("1111111111222222");
    }

    private readonly UTF8Encoding _encoder = new();

    private readonly Aes _tripleDes1 = System.Security.Cryptography.Aes.Create();

    private readonly byte[] _tripleKey;

    private readonly byte[] _vectorByte;

    /// <summary>
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public byte[] Decrypt(byte[] input) => Transform(input, _tripleDes1.CreateDecryptor());

    /// <summary>
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public string Decrypt(string text)
    {
        byte[] _input = Convert.FromBase64String(text);
        byte[] _bytes = Transform(_input, _tripleDes1.CreateDecryptor(_tripleKey, _vectorByte));

        return _encoder.GetString(_bytes);
    }

    /// <summary>
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public byte[] Encrypt(byte[] input) => Transform(input, _tripleDes1.CreateEncryptor());

    /// <summary>
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public string Encrypt(string text)
    {
        byte[] _bytes = _encoder.GetBytes(text);
        
        return Convert.ToBase64String(Transform(_bytes, _tripleDes1.CreateEncryptor()));
    }

    private static byte[] Transform(byte[] input, ICryptoTransform cryptoTransform)
    {
        using MemoryStream _stream2 = new();
        using CryptoStream _stream = new(_stream2, cryptoTransform, CryptoStreamMode.Write);
        _stream.Write(input, 0, input.Length);
        _stream.FlushFinalBlock();
        _stream2.Position = 0L;
        byte[] _buffer = new byte[(int)(_stream2.Length - 1L) + 1];
        _ = _stream2.Read(_buffer, 0, _buffer.Length);

        return _buffer;
    }
}