﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.WebAPI
{
    public class Sha1Converter
    {
        // Convert byte to string from SHA1
        public string ByteArrayToString(byte[] inputArray)
        {
            StringBuilder output = new StringBuilder("");
            for (int i = 0; i < inputArray.Length; i++)
            {
                output.Append(inputArray[i].ToString("X2"));
            }

            return output.ToString();
        }

        // SHA1 encryption returning string
        public string SHA1Encrypt(string phrase)
        {
            UTF8Encoding encoder = new UTF8Encoding();
            SHA1Managed sha1hasher = new SHA1Managed();
            byte[] hashedDataBytes = sha1hasher.ComputeHash(encoder.GetBytes(phrase));

            return ByteArrayToString(hashedDataBytes);
        }
    }
}
