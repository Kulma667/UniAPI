using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UniApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public string Index()
        {
            return "Welcoem to User API";
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
    }
}
