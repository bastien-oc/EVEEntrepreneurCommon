using System.Collections;
using System.Collections.Generic;
using System;
using System.Security.Cryptography;
using System.Text;

public static class SignatureHash {
    const string salt = "18E0891455C3373A"; //your salt here

    public static string CalculateHash(string input)
    {
        MD5 md5 = MD5.Create();

        byte[] inputBytes = Encoding.ASCII.GetBytes(salt+input);

        byte[] hash = md5.ComputeHash(inputBytes);

        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < hash.Length; i++)
            sb.Append(hash[i].ToString("X2"));
        
        return sb.ToString().ToUpper();
    }
}
