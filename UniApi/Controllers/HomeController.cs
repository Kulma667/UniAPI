using System;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
            string token = "";
 
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
                    case "token":
                        token = parameter.Value;
                        break;
                }
            }
            switch (ReqAction)
            {
                case "GetUsers":
                    return GetUsers(token);
                case "GetUser":
                    return GetUser(Convert.ToInt32(Id));
                case "CreateUser":
                    return CreateUser(Convert.ToInt32(Id), Name, Email, Password, Sex, token);
                case "ChangePassword":
                    return ChangePassword(Convert.ToInt32(Id), Password, token);
                default:
                    return "Undefinded action";
            }
        }
        private readonly Repository repo = new Repository();
        public string GetUsers(string token)
        {
            string result = VerifyToken(token);
            if (JsonConvert.DeserializeObject<dynamic>(result) == "OK")
            {
                string students = JsonConvert.SerializeObject(repo.GetStudents());
                return students;
            }
            return result;
        }
            public string GetUser(int Id) 
        {
            string students = JsonConvert.SerializeObject(repo.GetStudent(Id));
            return students;
        }
        public string CreateUser(int Id, string Name, string Email, string Password, string Sex, string token) 
        {
            string result = VerifyToken(token);
            if (JsonConvert.DeserializeObject<dynamic>(result) == "OK")
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
            else 
            {
                return result;
            }
        }
        public string GetPassword(int Id, string token)
        {
            string result = VerifyToken(token);
            if (JsonConvert.DeserializeObject<dynamic>(result) == "OK")
            {
                string students = JsonConvert.SerializeObject(repo.GetPass(Id));
                return students;
            }
            return JsonConvert.SerializeObject("Error -201: Invalid Token");
        }
        public string ChangePassword(int Id , string Password, string token)
        {
            string result = VerifyToken(token);
            if (JsonConvert.DeserializeObject<dynamic>(result) == "OK")
            {
                string students = repo.ChangePassword(Id, Password);
                return students;
            }
            return result;
        }
        public string Token(string Login,string Password) 
        {
            if (repo.Login(Login, Password).Count == 1)
            {
                string result = "";
                result = StringToBase64(Login + "," + Password);
                repo.CreateToken(result, DateTime.Now);
                return JsonConvert.SerializeObject(result);
            }
            else 
            {
                return "Неверный логин или пароль";
            }
        }
        static string StringToBase64(string originalString)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(originalString);
            return Convert.ToBase64String(bytes);
        }
        public string VerifyToken(string token) 
        {
            byte[] bytes = Convert.FromBase64String(token);
            string originalString = Encoding.UTF8.GetString(bytes);
            string[] data = originalString.Split(",");
            if (repo.Login(data[0], data[1]).Count == 1)
            {
                return JsonConvert.SerializeObject("OK");
            }
            return JsonConvert.SerializeObject("Error:403, LOL u are wrong");
        }
        //public string Login(string Login, string Password) 
        //{
        //    if (repo.Login(Login , Password).Count == 1)
        //    {
        //        return "";
        //    }
        //    return "";
        //}
        public void TokenLife() 
        {
            
        }
    }
}
