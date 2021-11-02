using Holism.Api;
using Holism.Business;
using Holism.Logs.Business;
using Holism.Logs.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Holism.Zoom.UserApi
{
    public class ZoomController : HolismController
    {
        //https://marketplace.zoom.us/docs/sdk/native-sdks/web/signature

        static readonly char[] padding = { '=' };

        [HttpGet]
        public object GenerateToken(string meetingNumber)
        {
            var apiKey = Config.GetSetting("ZoomApiKey");
            var apiSecret = Config.GetSetting("ZoomApiSecret");
            String timestamp = (ToTimestamp(DateTime.UtcNow.ToUniversalTime()) - 30000).ToString();
            var role = "1"; // 1 for host, 0 for participant

            string message = String.Format ("{0}{1}{2}{3}", apiKey, meetingNumber, timestamp, role);
            apiSecret = apiSecret ?? "";
            var encoding = new System.Text.ASCIIEncoding ();
            byte[] keyByte = encoding.GetBytes(apiSecret);
            byte[] messageBytesTest = encoding.GetBytes(message);
            string msgHashPreHmac = System.Convert.ToBase64String(messageBytesTest);
            byte[] messageBytes = encoding.GetBytes(msgHashPreHmac);
            using (var hmacsha256 = new HMACSHA256(keyByte)) 
            {
                byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);
                string msgHash = System.Convert.ToBase64String(hashmessage);
                string token = String.Format("{0}.{1}.{2}.{3}.{4}", apiKey, meetingNumber, timestamp, role, msgHash);
                var tokenBytes = System.Text.Encoding.UTF8.GetBytes(token);
                return new 
                {
                    Signature = System.Convert.ToBase64String(tokenBytes).TrimEnd(padding)
                };
            }
        }

        public static long ToTimestamp (DateTime value) {
            long epoch = (value.Ticks - 621355968000000000) / 10000;
            return epoch;
        }
    }
}