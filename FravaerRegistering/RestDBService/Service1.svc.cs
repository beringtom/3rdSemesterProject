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
                        return LoginList;
                    }
                }
            }

        }


        public int DeletePerson(int personID)
        {
            string deletecommand = $"DELETE FROM Person WHERE Person_Id = {personID}";
            using (SqlConnection databaseConnection = new SqlConnection(ConnectionString))
            {
                databaseConnection.Open();
                using (SqlCommand selectCommand = new SqlCommand(deletecommand, databaseConnection))
                {
                    int rowsaffected = selectCommand.ExecuteNonQuery();

                    return rowsaffected;
                }
            }
        }

        public int DeleteLogin(int personID)
        {
            string deletecommand = $"DELETE FROM login WHERE FK_PersonId = {personID}";
            using (SqlConnection databaseConnection = new SqlConnection(ConnectionString))
            {
                databaseConnection.Open();
                using (SqlCommand selectCommand = new SqlCommand(deletecommand, databaseConnection))
                {
                    int rowsaffected = selectCommand.ExecuteNonQuery();

                    return rowsaffected;
                }
            }
        }

        public Person EditPerson(int personID, string fname, string lname, string email, string username,
            string password, int roles, int studentid, int teamid)
        {
            string updatecommand = $"UPDATE Person SET Person_FirstName ={fname}, Person_LastName={lname}, Person_Email={email},FK_RolesId={roles}, FK_TeamId={teamid},Person_StudentId={studentid} WHERE Person_Id = {personID}";

            using (SqlConnection databaseConnection = new SqlConnection(ConnectionString))
            {
                databaseConnection.Open();
                using (SqlCommand selectCommand = new SqlCommand(updatecommand, databaseConnection))
                {
                    using (SqlDataReader reader = selectCommand.ExecuteReader())
                    {
                        Person student = new Person();
                        while (reader.Read())
                        {
                            student = ReadPerson(reader);
                        }
                        EditLogin(username, password, personID);
                        return student;
                    }
                }
            }
        }

        public int EditLogin(string username, string password, int personid)
        {
            string updatecommand = $"UPDATE login SET login_UserName={username}, login_Password={password} WHERE FK_PersonId={personid}";

            using (SqlConnection databaseConnection = new SqlConnection(ConnectionString))
            {
                databaseConnection.Open();
                using (SqlCommand selectCommand = new SqlCommand(updatecommand, databaseConnection))
                {
                    int rowsaffected = selectCommand.ExecuteNonQuery();
                    return rowsaffected;
                }
            }
        }







        //opret ny person 

        public Person AddPerson(string fname, string lname, string email, string username, string password, int roles, int studentid, int teamid)
        {
            Person x = AddPersonToDB(fname, lname, email, roles, studentid, teamid);
            AddLoginToDB(username, password, x.Person_Id);
            return x;
        }




        public Person AddPersonToDB(string fname, string lname, string email, int roles, int studentid, int teamid)
        {
            

            string CreatePersons = "INSERT INTO Person(Person_FirstName, Person_LastName, Person_Email,FK_RolesId, FK_TeamId,Person_StudentId) VALUES('"+fname+"', '"+lname+"', '"+email+"', "+roles+", "+teamid+", '"+studentid+"')";

            using (SqlConnection databaseConnection = new SqlConnection(ConnectionString))
            {
                databaseConnection.Open();
                using (SqlCommand selectCommand = new SqlCommand(CreatePersons, databaseConnection))
                {
                    using (SqlDataReader reader = selectCommand.ExecuteReader())
                    {
                        Person student = new Person();
                        while (reader.Read())
                        {
                            student = ReadPerson(reader);
                        }
                        return student;
                    }
                }
            }
        }

        public int AddLoginToDB(string username, string password, int personid)
        {
            string CreateLogins = $"INSERT INTO login(login_UserName, login_Password, FK_PersonId) VALUES{username}, {password}, {personid}";

            using (SqlConnection databaseConnection = new SqlConnection(ConnectionString))
            {
                databaseConnection.Open();
                using (SqlCommand selectCommand = new SqlCommand(CreateLogins, databaseConnection))
                {
                    int rowsaffected = selectCommand.ExecuteNonQuery();
                    return rowsaffected;
                }
            }
        }

        public Person ReadPerson(IDataRecord reader)
        {
            int id = reader.GetInt32(0);
            string firstname = reader.GetString(1);
            string lastname = reader.GetString(2);
            string email = reader.GetString(3);
            int fkrolesid = reader.GetInt32(4);
            int fkteamid = reader.GetInt32(5);
            string rolesname = reader.GetString(9);
            string teamname = reader.GetString(11);
            string studentid = reader.GetString(6);
            Person person = new Person()
            {
                Person_Id = id,
                Person_FirstName = firstname,
                Person_LastName = lastname,
                Person_Email = email,
                FK_RolesId = fkrolesid,
                FK_TeamId = fkteamid,
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