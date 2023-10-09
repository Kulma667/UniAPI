using System;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft;
using Newtonsoft.Json;
using UniApi.Models;
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
        public string Index([FromForm] Dictionary<string , string> InputData)
        {
            string Id = "";
            string Password = "";
            string Email = "";
            string Name = "";
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
                    return GetUser(Convert.ToInt32(Id));
                case "CreateUser":
                    return CreateUser(Convert.ToInt32(Id), Name, Email, Password, Sex);
                case "ChangePassword":
                    return ChangePassword(Convert.ToInt32(Id), Password);
                default:
                    return ReqAction;
            }
            return "";
        }
        private readonly Repository repo = new Repository();
        public string GetUsers()
        {
            string students = JsonConvert.SerializeObject(repo.GetStudents());
            return students;
        }
        public string GetUser(int Id) 
        {
            string students = JsonConvert.SerializeObject(repo.GetStudent(Id));
            return students;
        }
        public string CreateUser(int Id, string Name, string Email, string Password, string Sex) 
        {
            bool sexual_identity = true;
            if (Sex.Contains("Boy") || Sex.Contains("Girl"))
            {
                if (Sex.Contains("Girl"))
                {
                    sexual_identity = false;
                }
                else if (Sex.Contains("Boy"))
                {
                    sexual_identity = true;
                }   
            if (repo.GetStudents().Count == Id)  
            {
                return JsonConvert.SerializeObject("Error 302: Set Another Id");
            }
            else if (repo.GetStudent(Id).Count == 1)
                {
                    return JsonConvert.SerializeObject("Error 303: This Id already exists"); 
                }
            string students = JsonConvert.SerializeObject(repo.CreateStudent(Id, Name, Email, Password, sexual_identity));
            return $"Succsess! Student Named {Name} created";
            }
            else
            {
                return JsonConvert.SerializeObject("Error -301:Set Sex to Value that equals to = Boy or Girl");
            }
        }
        public string GetPassword(int Id)
        {
            string students = JsonConvert.SerializeObject(repo.GetPass(Id));

            return students;
        }
        public string ChangePassword(int Id , string Password)
        {
            string students = repo.ChangePassword(Id, Password);

            return students;
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
