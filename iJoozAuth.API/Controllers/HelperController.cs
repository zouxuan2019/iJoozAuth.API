using System;
using System.Security.Cryptography.X509Certificates;
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
    }
}