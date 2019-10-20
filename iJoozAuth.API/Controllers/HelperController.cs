using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace iJoozAuth.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class HelperController
    {
        [HttpGet("pfxBase64/{pfxName}/{password}")]
        public string getBase64Pfx([FromRoute] string pfxName, [FromRoute] string password)
        {
            var cert = new X509Certificate2(pfxName, password, X509KeyStorageFlags.Exportable);
            byte[] certBytes = cert.Export(X509ContentType.Pkcs12, password);
            var certString = Convert.ToBase64String(certBytes);
            return certString;
        }

        [HttpGet("test")]
        public string fomoSignatureTest()
        {
            PaymentAttributes fomo = new PaymentAttributes()
            {
                Price = "1",
                Description = "a"
            };
            string host = "https://localhost:8100/menu/e-wallet/topup";

            // set predefine values
            fomo.ReturnUrl = host + "/payment/return";
            fomo.CallbackUrl = host + "/payment/callback";
            fomo.Transaction = "aaaa";
            fomo.Nonce = "bbbb";
            fomo.Type = "sale";
            fomo.Timeout = "1800";
            fomo.CurrencyCode = "sgd";
            FomoPaymentProcess(fomo);
            return fomo.Signature;
        }

        private void FomoPaymentProcess(PaymentAttributes fomo)
        {
            // To be modified for actual payment gateway
            fomo.Url = "https://gateway.fomopay.com/sandbox/pgw/v1";
            fomo.Merchant = "test";
            string apiKey = "00000000";

            string param = this.Param(fomo, false);
            string queryString = param + "&shared_key=" + apiKey;
            Console.WriteLine(queryString);
            fomo.Signature = ComputeSha256Hash(queryString);
        }

        public string Param(PaymentAttributes fomo, bool withSignature)
        {
            SortedDictionary<string, string> param = new SortedDictionary<string, string>();
            param.Add("merchant", fomo.Merchant);
            param.Add("price", fomo.Price);
            param.Add("description", fomo.Description);
            param.Add("transaction", fomo.Transaction);
            param.Add("return_url", fomo.ReturnUrl);
            param.Add("callback_url", fomo.CallbackUrl);
            param.Add("currency_code", fomo.CurrencyCode);
            param.Add("type", fomo.Type);
            param.Add("timeout", fomo.Timeout);
            param.Add("nonce", fomo.Nonce);

            if (withSignature)
                param.Add("signature", fomo.Signature);

            StringBuilder strArray = new StringBuilder();
            foreach (KeyValuePair<string, string> temp in param)
            {
                strArray.Append(temp.Key + "=" + temp.Value + "&");
            }

            int nLen = strArray.Length;
            strArray.Remove(nLen - 1, 1);

            return strArray.ToString();
        }

        public static string ComputeSha256Hash(string rawData)
        {
            var crypt = new System.Security.Cryptography.SHA256Managed();
            var hash = new System.Text.StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(rawData));
            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }

            return hash.ToString().ToLower();
        }

        public class PaymentAttributes
        {
            public string Merchant { get; set; }

            public string Price { get; set; }

            public string Description { get; set; }

            public string Transaction { get; set; }

            public string ReturnUrl { get; set; }

            public string CallbackUrl { get; set; }

            public string CurrencyCode { get; set; }

            public string Type { get; set; }

            public string Timeout { get; set; }

            public string Nonce { get; set; }

            public string Signature { get; set; }

            public string Url { get; set; }

            public string PaymentResult { get; set; }

            public string Upstream { get; set; }

            public string PaymentId { get; set; }
        }
    }
}