using System;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Components.Forms;
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
        public string Index([FromBody] Dictionary<string , string> InputData)
        {
            string Id = "";
            string Password = "";
            string Email = "";
            string Name = "";
            string SName= "";
            string OName = "";
            string Sex = "";
            string ReqAction = "";
 
            foreach (var parameter in InputData)
            {
                switch (parameter.Key.ToLower())
                {
                    case "reqaction":
                        ReqAction = parameter.Value;
                        break;
                    case "id":
                        Id = parameter.Value;
                        break;
                    case "password":
                        Password = parameter.Value;
                        break;
                    case "email":
                        Email = parameter.Value;
                        break;
                    case "name":
                        Name = parameter.Value;
                        break;
                    case "sname":
                        SName = parameter.Value;
                        break;
                    case "oname":
                        OName = parameter.Value;
                        break;
                    case "sex":
                        Sex = parameter.Value;
                        break;
                }
            }
            switch (ReqAction)
            {
                case "GetUsers":
                    return GetUsers();
                case "GetUser":
                    return GetUser(Id);
                case "CreateUser":
                    return CreateUser(Id, Name, SName, OName, Email, Password, Sex);
                case "ChangePassword":
                    return ChangePassword(Id, Email);
                default:
                    return ReqAction;
            }
            return "";
        }
        public string GetUsers()
        {

            //repo.Лист юзеров 
            return "";
        }
        public string GetUser(string Id) 
        {
            //repo.Один юзер(id)
            return "";
        }
        public string CreateUser(string Id, string Name,string SName, string OName, string Email, string Password, string Sex) 
        {
            //repo.CreateUsera(Id, Name, Email, Password, Sex)
            return "";
        }
        public string ChangePassword(string Id, string Email) 
        {
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
