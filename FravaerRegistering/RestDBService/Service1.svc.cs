using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace RestDBService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        private const string ConnectionString =
                "Server=tcp:3rdsemesterprojectserver.database.windows.net,1433;Initial Catalog=3rdSemesterProjectDB;Persist Security Info=False;User ID=team4;Password=Noot1234;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
            ;
        //henter alle personer
        public IList<Person> GetAllPersons()
        {
            const string selectAllPersons = "select * from Person inner join Roles on Person.Person_Id = Roles.Roles_Id inner join Team on Person.Person_Id =Team.Team_Id";

            using (SqlConnection databaseConnection = new SqlConnection(ConnectionString))
            {
                databaseConnection.Open();
                using (SqlCommand selectCommand = new SqlCommand(selectAllPersons, databaseConnection))
                {
                    using (SqlDataReader reader = selectCommand.ExecuteReader())
                    {
                        List<Person> studentList = new List<Person>();
                        while (reader.Read())
                        {
                            Person student = ReadPerson(reader);
                            studentList.Add(student);
                        }
                        return studentList;
                    }
                }
            }
        }
        //henter en person
        public IList<Person> GetOnePersons(string id)
        {
            int idInt = int.Parse(id);

            string selectAllPersons = "select * from Person inner join Roles on Person.Person_Id = Roles.Roles_Id inner join Team on Person.Person_Id =Team.Team_Id where Person_Id ="+ idInt;

            using (SqlConnection databaseConnection = new SqlConnection(ConnectionString))
            {
                databaseConnection.Open();
                using (SqlCommand selectCommand = new SqlCommand(selectAllPersons, databaseConnection))
                {
                    using (SqlDataReader reader = selectCommand.ExecuteReader())
                    {
                        List<Person> studentList = new List<Person>();
                        while (reader.Read())
                        {
                            Person student = ReadPerson(reader);
                            studentList.Add(student);
                        }
                        return studentList;
                    }
                }
            }
        }
        //tjekker på om felterne i login tabelen er enes med user input
        public IList<Login> Getlogin(Login loginUserPaswords)
        {
           

            string selectlogin = "select* from login where Login_UserName = '"+loginUserPaswords.Login_UserName+"' and Login_Password = '"+loginUserPaswords.Login_Password+"'";
 
            using (SqlConnection databaseConnection = new SqlConnection(ConnectionString))
            {
                databaseConnection.Open();
                using (SqlCommand selectCommand = new SqlCommand(selectlogin, databaseConnection))
                {
                    using (SqlDataReader reader = selectCommand.ExecuteReader())
                    {
                        List<Login> LoginList = new List<Login>();
                        while (reader.Read())
                        {
                            Login login = ReadLogin(reader);
                            LoginList.Add(login);
                            
                        }
                        //prøvet at sende logins personId over til finction GetOnePerns for at skrive den ud, men kan ikke få det til at virke
                        //foreach (var i in LoginList)
                        //{
                        //   string personId = i.FK_PersonId.ToString();
                        //    GetOnePersons(personId);
                        //}
                        return LoginList;
                    }
                }
            }

        }

        public Person ReadPerson(IDataRecord reader)
        {
            int id = reader.GetInt32(0);
            string firstname = reader.GetString(1);
            string lastname = reader.GetString(2);
            string email = reader.GetString(3);
            string rolesname = reader.GetString(9);
            string teamname = reader.GetString(11);
            string studentid = reader.GetString(6);
            Person person = new Person()
            {
                Person_Id = id,
                Person_FirstName = firstname,
                Person_LastName = lastname,
                Person_Email = email,
                Roles_Name = rolesname,
                Team_Name = teamname,
                Person_StudentId = studentid
            };
            return person;
        }

        public Login ReadLogin(IDataReader reader)
        {
            int id = reader.GetInt32(0);
            string username = reader.GetString(1);
            string password = reader.GetString(2);
            int personid = reader.GetInt32(3);

            Login login = new Login()
            {
                Login_Id = id,
                Login_UserName = username,
                Login_Password = password,
                FK_PersonId = personid
            };
            return login;

        }

        
    }
}