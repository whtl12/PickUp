using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Security.Cryptography;

public class EncryptValue : MonoBehaviour {
    private static string encryptSecretCode()
    {
        // 암호화 코드
        return "Horneless1234Unicorn";
    }

    private static void SetString(string _key, string _value, byte[] _secret)
    {
        // Hide '_key' string.  
        MD5 md5Hash = MD5.Create();
        byte[] hashData = md5Hash.ComputeHash(System.Text.Encoding.UTF8.GetBytes(_key));
        string hashKey = System.Text.Encoding.UTF8.GetString(hashData);

        // Encrypt '_value' into a byte array  
        byte[] bytes = System.Text.Encoding.UTF8.GetBytes(_value);

        // Eecrypt '_value' with 3DES.  
        TripleDES des = new TripleDESCryptoServiceProvider();
        des.Key = _secret;
        des.Mode = CipherMode.ECB;
        ICryptoTransform xform = des.CreateEncryptor();
        byte[] encrypted = xform.TransformFinalBlock(bytes, 0, bytes.Length);

        // Convert encrypted array into a readable string.  
        string encryptedString = Convert.ToBase64String(encrypted);

        // Set the ( key, encrypted value ) pair in regular PlayerPrefs.  
        PlayerPrefs.SetString(hashKey, encryptedString);

        Debug.Log("SetString hashKey: " + hashKey + " Encrypted Data: " + encryptedString);

        PlayerPrefs.Save();
    }

    private static string GetString(string _key, byte[] _secret)
    {
        // Hide '_key' string.  
        MD5 md5Hash = MD5.Create();
        byte[] hashData = md5Hash.ComputeHash(System.Text.Encoding.UTF8.GetBytes(_key));
        string hashKey = System.Text.Encoding.UTF8.GetString(hashData);

        // Retrieve encrypted '_value' and Base64 decode it.  
        string _value = PlayerPrefs.GetString(hashKey);
        byte[] bytes = Convert.FromBase64String(_value);

        // Decrypt '_value' with 3DES.  
        TripleDES des = new TripleDESCryptoServiceProvider();
        des.Key = _secret;
        des.Mode = CipherMode.ECB;
        ICryptoTransform xform = des.CreateDecryptor();
        byte[] decrypted = xform.TransformFinalBlock(bytes, 0, bytes.Length);

        // decrypte_value as a proper string.  
        string decryptedString = System.Text.Encoding.UTF8.GetString(decrypted);

        Debug.Log("GetString hashKey: " + hashKey + " GetData: " + _value + " Decrypted Data: " + decryptedString);
        return decryptedString;
    }
    public static string GetString(string _key)
    {
        MD5 md5Hash = new MD5CryptoServiceProvider();
        byte[] secret = md5Hash.ComputeHash(System.Text.Encoding.UTF8.GetBytes(encryptSecretCode()));

        try
        {
            return GetString(_key, secret);
        }
        catch
        {
            SetString(_key, string.Empty);
            return "0";
        }
    }
    public static float GetFloat(string _key, float _default = 0f)
    {
        MD5 md5Hash = new MD5CryptoServiceProvider();
        byte[] secret = md5Hash.ComputeHash(System.Text.Encoding.UTF8.GetBytes(encryptSecretCode()));

        float _value;
        if (float.TryParse(GetString(_key, secret), out _value))
        {
            return _value;
        }
        else
        {
            SetFloat(_key, _default);
            return _default;
        }
    }

    public static int GetInt(string _key, int _default)
    {
        MD5 md5Hash = new MD5CryptoServiceProvider();
        byte[] secret = md5Hash.ComputeHash(System.Text.Encoding.UTF8.GetBytes(encryptSecretCode()));

        int _value;
        if (int.TryParse(GetString(_key, secret), out _value))
        {
            return _value;
        }
        else
        {
            SetInt(_key, _default);
            return _default;
        }
    }
    public static void SetInt(string _key, int _value)
    {
        MD5 md5Hash = new MD5CryptoServiceProvider();
        byte[] secret = md5Hash.ComputeHash(System.Text.Encoding.UTF8.GetBytes(encryptSecretCode()));

        SetString(_key, _value.ToString(), secret);
    }
    public static void SetFloat(string _key, float _value)
    {
        MD5 md5Hash = new MD5CryptoServiceProvider();
        byte[] secret = md5Hash.ComputeHash(System.Text.Encoding.UTF8.GetBytes(encryptSecretCode()));

        SetString(_key, _value.ToString(), secret);
    }
    public static void SetString(string _key, string _value)
    {
        MD5 md5Hash = new MD5CryptoServiceProvider();
        byte[] secret = md5Hash.ComputeHash(System.Text.Encoding.UTF8.GetBytes(encryptSecretCode()));

        SetString(_key, _value, secret);
    }
}
