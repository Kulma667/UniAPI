using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using Dapper;
using Newtonsoft.Json;


namespace UniApi.Models
{
    public class Repository
    {
        //Config{}

        public string conn = Startup.ConString;
        //repo на забирание одного юзера, списка юзеров , создание юзера, изменение пароля(прописать процедуры на все жти методы + расспарссить успешные и не успешные ответы)
        //repo.GetUsers
        //repo.GetUser
        //repo.CreateUser
        //repo.ChangePass
        //
        public List<User> GetStudents() 
        {
            List<User> students = new List<User>();
            using (var cnn = new SqlConnection("workstation id=Students.mssql.somee.com;packet size=4096;user id=UIBStudent_SQLLogin_1;pwd=5255t2awd1;data source=Students.mssql.somee.com;persist security info=False;initial catalog=Students"))
            {
                students = cnn.Query<User>("dbo.AllStudents",
                    commandType: System.Data.CommandType.StoredProcedure).ToList();
            }
            return students;
        }
        public List<User> GetStudent(int id) 
        {
            List<User> students = new List<User>();
            using (var cnn = new SqlConnection("workstation id=Students.mssql.somee.com;packet size=4096;user id=UIBStudent_SQLLogin_1;pwd=5255t2awd1;data source=Students.mssql.somee.com;persist security info=False;initial catalog=Students"))
            {
                students =  cnn.Query<User>("dbo.GetStudent",
                    new { id = id },
                    commandType: System.Data.CommandType.StoredProcedure).ToList();
            }
            return students;
        }
        public List<User> CreateStudent(int id, string Name, string Email, string Password, bool Sex)
        {
            List<User> students = new List<User>();
        try 
        { 
            using (var cnn = new SqlConnection("workstation id=Students.mssql.somee.com;packet size=4096;user id=UIBStudent_SQLLogin_1;pwd=5255t2awd1;data source=Students.mssql.somee.com;persist security info=False;initial catalog=Students"))
            {
                students = cnn.Query<User>("dbo.CreateStudents",
                    new { id = id , Name = Name, Email = Email, Password, Sex = Sex},
                    commandType: System.Data.CommandType.StoredProcedure).ToList();
            }
        }catch (Exception ex) { }
            return students;
        }

        public string ChangePassword(int id, string Password)
        {
            List<User> students = new List<User>();
            using (var cnn = new SqlConnection("workstation id=Students.mssql.somee.com;packet size=4096;user id=UIBStudent_SQLLogin_1;pwd=5255t2awd1;data source=Students.mssql.somee.com;persist security info=False;initial catalog=Students"))
            {
                students = cnn.Query<User>("dbo.ChangePass",
                    new { id = id, Password = Password },
                    commandType: System.Data.CommandType.StoredProcedure).ToList();
            }
            return JsonConvert.SerializeObject("Code 01:Password changed");
        }
        public List<User> GetPass(int id)
        {
            List<User> students = new List<User>();
            using (var cnn = new SqlConnection("workstation id=Students.mssql.somee.com;packet size=4096;user id=UIBStudent_SQLLogin_1;pwd=5255t2awd1;data source=Students.mssql.somee.com;persist security info=False;initial catalog=Students"))
            {
                students = cnn.Query<User>("dbo.GetPassword",
                    new { id = id },
                    commandType: System.Data.CommandType.StoredProcedure).ToList();
            }
            return students;
        }
        public List<SuperUsers> Login(string Login, string Password)
        {
            List<SuperUsers> students = new List<SuperUsers>();
            using (var cnn = new SqlConnection("workstation id=Students.mssql.somee.com;packet size=4096;user id=UIBStudent_SQLLogin_1;pwd=5255t2awd1;data source=Students.mssql.somee.com;persist security info=False;initial catalog=Students"))
            {
                students = cnn.Query<SuperUsers>("dbo.GetUser",
                    new { Name=Login , Password },
                    commandType: System.Data.CommandType.StoredProcedure).ToList();
            }
            return students;
        }
        public string CreateToken(string Token, DateTime CreatedOn)
        {
            List<dynamic> createdToken = new List<dynamic>();
            using (var cnn = new SqlConnection("workstation id=Students.mssql.somee.com;packet size=4096;user id=UIBStudent_SQLLogin_1;pwd=5255t2awd1;data source=Students.mssql.somee.com;persist security info=False;initial catalog=Students"))
            {
                createdToken = cnn.Query<dynamic>("dbo.CreateToken",
                    new { Token, CreatedOn},
                    commandType: System.Data.CommandType.StoredProcedure).ToList();
            }
            return JsonConvert.SerializeObject($"Created:\r\n {JsonConvert.SerializeObject(createdToken)}");
        }
        public string DeleteToken(string Token, DateTime CreatedOn)
        {
            List<dynamic> deletedToken = new List<dynamic>();
            using (var cnn = new SqlConnection("workstation id=Students.mssql.somee.com;packet size=4096;user id=UIBStudent_SQLLogin_1;pwd=5255t2awd1;data source=Students.mssql.somee.com;persist security info=False;initial catalog=Students"))
            {
                deletedToken = cnn.Query<dynamic>("dbo.DeleteToken",
                    new { Token, CreatedOn },
                    commandType: System.Data.CommandType.StoredProcedure).ToList();
            }
            return JsonConvert.SerializeObject($"Deleted:\r\n {JsonConvert.SerializeObject(deletedToken)}");
        }
        public string GetToken(string token)
        {
            List<dynamic> result = new List<dynamic>();
            using (var cnn = new SqlConnection("workstation id=Students.mssql.somee.com;packet size=4096;user id=UIBStudent_SQLLogin_1;pwd=5255t2awd1;data source=Students.mssql.somee.com;persist security info=False;initial catalog=Students"))
            {
                result = cnn.Query<dynamic>("dbo.GetToken",
                    new { Token = token },
                    commandType: System.Data.CommandType.StoredProcedure).ToList();
            }
            return JsonConvert.SerializeObject(result);
        }
    }
}
