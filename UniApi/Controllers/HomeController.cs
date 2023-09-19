using System;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft;
using Newtonsoft.Json;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UniApi.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public string Index()
        {
            return "Welcome to User API";
        }

        [HttpPost]
        public string Index([FromBody] Dictionary<string , string> InoputData)
        { 
            return "";
        }
        public string GetUsers()
        {
            return "";
        }
        public string GetUser(string id) 
        {
            return "";
        }
        public string CreateUser(string Id, string Name, string Email, string Password, bool Sex) 
        {
            return "";
        }
        public string ChangePassword(string Id, string Email, string oldPassword) 
        {
            if (oldPassword != "")
            {
                return "Try again";
            }
            return "Succsess";
        }
        public string hasher256(string Password, string Key) 
        {
            string resulthash = ""; 
            Key = "";
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(Password + Key);
                byte[] hashBytes = sha256.ComputeHash(inputBytes);
                string hashString = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
                resulthash = hashString;
            }
            return JsonConvert.SerializeObject(resulthash);
        }
        public string VerifyHash(string hashString) 
        {

            string originalString = ""; //получать праоль из базы и бить ее в хешер для верифая
            return JsonConvert.SerializeObject(originalString);
        }
    }
}
