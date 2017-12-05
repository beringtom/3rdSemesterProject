using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;
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
        public IList<AllPersonData> GetAllPersons()
        {
            const string selectAllPersons = "select * from Person inner join Roles on Person.FK_RolesId = Roles.Roles_Id inner join Team on Person.FK_TeamId =Team.Team_Id inner join login on Person.Person_Id = login.FK_PersonId";

            using (SqlConnection databaseConnection = new SqlConnection(ConnectionString))
            {
                databaseConnection.Open();
                using (SqlCommand selectCommand = new SqlCommand(selectAllPersons, databaseConnection))
                {
                    using (SqlDataReader reader = selectCommand.ExecuteReader())
                    {
                        List<AllPersonData> studentList = new List<AllPersonData>();
                        while (reader.Read())
                        {
                            AllPersonData student = ReadAllPersonData(reader);
                            studentList.Add(student);
                        }
                        return studentList;
                    }
                }
            }
        }
        //henter en person
        public AllPersonData GetOnePersons(string id)
        {
            int idInt = int.Parse(id);

            string selectAllPersons = "select * from Person inner join Roles on Person.FK_RolesId = Roles.Roles_Id inner join Team on Person.FK_TeamId =Team.Team_Id inner join login on Person.Person_Id = login.FK_PersonId where Person_Id =" + idInt;

            using (SqlConnection databaseConnection = new SqlConnection(ConnectionString))
            {
                databaseConnection.Open();
                using (SqlCommand selectCommand = new SqlCommand(selectAllPersons, databaseConnection))
                {
                    using (SqlDataReader reader = selectCommand.ExecuteReader())
                    {
                        AllPersonData student = new AllPersonData();
                        while (reader.Read())
                        {
                            student = ReadAllPersonData(reader);
                        }
                        return student;
                    }
                }
            }
        }
        //tjekker på om felterne i login tabelen er enes med user input
        public Login Getlogin(Login loginUserPaswords)
        {

            string selectlogin = "select* from login where Login_UserName = '"+loginUserPaswords.Login_UserName+"' and Login_Password = '"+loginUserPaswords.Login_Password+"'";
 
            using (SqlConnection databaseConnection = new SqlConnection(ConnectionString))
            {
                databaseConnection.Open();
                using (SqlCommand selectCommand = new SqlCommand(selectlogin, databaseConnection))
                {
                    using (SqlDataReader reader = selectCommand.ExecuteReader())
                    {
                        Login returnLogin = new Login();
                        while (reader.Read())
                        {
                            returnLogin = ReadLogin(reader);
                            
                        }
                        return returnLogin;
                    }
                }
            }

        }

        public void AddTeam(Team t)
        {
            string addteam = $"INSERT INTO Team(Team_Name) VALUES (@tname)";

            using (SqlConnection databaseConnection = new SqlConnection(ConnectionString))
            {
                databaseConnection.Open();
                using (SqlCommand addcommand = new SqlCommand(addteam, databaseConnection))
                {
                    addcommand.Parameters.AddWithValue("@tname", t.Team_Name);
                    addcommand.ExecuteNonQuery();
                }
            }
        }

        public string SensorCheck(SonsorData s)
        {
            Person localPerson = new Person();
            Room localRoom = new Room();

            using (SqlConnection databaseConnection = new SqlConnection(ConnectionString))
            {
                databaseConnection.Open();

                //Henter Person der Bipede
                localPerson = GetPersonByStudentId(s.CardID, databaseConnection);

                //Hener Lokalet
                localRoom = GetRoomByName(s.Room, databaseConnection);

                //Henter alle timereg for person
                List<TimeRegistration> TimeReg = new List<TimeRegistration>();
                TimeReg = GetAllTimeRegForPerson(localPerson.Person_Id, databaseConnection);


                //TimeChecker
                if (TimeReg.Count == 0)
                {
                    AddTimeRegToDB(s.Time, localRoom.Room_Id, localPerson.Person_Id, databaseConnection);
                    return "CHECKIN";
                }
                else
                {
                    foreach (TimeRegistration treg in TimeReg)
                    {
                        if (treg.TimeRegistration_CheckOut == null)
                        {
                            UpdateTimeRegInDB(treg.TimeRegistration_Id, s.Time, databaseConnection);
                            return "CHECKOUT";
                        }
                        else if (treg.TimeRegistration_CheckOut != null)
                        {
                            AddTimeRegToDB(s.Time, localRoom.Room_Id, localPerson.Person_Id, databaseConnection);
                            return "CHECKIN";
                        }
                    }
                }
            }
            return "ERROR";
        }

        #region TimeRegistrationDBStuff

        private List<TimeRegistration> GetAllTimeRegForPerson(int personid, SqlConnection databaseConnection)
        {
            string FindTimeReg = "SELECT * FROM TimeRegistration WHERE FK_RegPersonId = @regperid";
            using (SqlCommand SelectTimeRegCommand = new SqlCommand(FindTimeReg, databaseConnection))
            {
                SelectTimeRegCommand.Parameters.AddWithValue("@regperid", personid);
                using (SqlDataReader reader = SelectTimeRegCommand.ExecuteReader())
                {
                    List<TimeRegistration> returnlist = new List<TimeRegistration>();
                    while (reader.Read())
                    {
                        TimeRegistration t = ReadTimeReg(reader);
                        returnlist.Add(t);
                    }
                    return returnlist;
                }
            }
        }
        private Person GetPersonByStudentId(string cardid, SqlConnection databaseConnection)
        {
            string FindPerson = "SELECT * FROM Person WHERE Person_StudieId = @cardid";
            using (SqlCommand SelectPersonCommand = new SqlCommand(FindPerson, databaseConnection))
            {
                SelectPersonCommand.Parameters.AddWithValue("@cardid", cardid);
                using (SqlDataReader reader = SelectPersonCommand.ExecuteReader())
                {
                    Person p = new Person();
                    while (reader.Read())
                    {
                        p = ReadPerson(reader);
                    }   
                    return p;
                }
            }
        }

        private Room GetRoomByName(string roomname, SqlConnection databaseConnection)
        {
            string FindRoom = "SELECT * FROM Room WHERE Room_Name = @rname";
            using (SqlCommand SelectRommCommand = new SqlCommand(FindRoom, databaseConnection))
            {
                SelectRommCommand.Parameters.AddWithValue("@rname", roomname);
                using (SqlDataReader reader = SelectRommCommand.ExecuteReader())
                {
                    Room r = new Room();
                    while (reader.Read())
                    {
                        r = ReadRoom(reader);
                    }
                    return r;
                }
            }
        }
        private void UpdateTimeRegInDB(int regid, string datentime, SqlConnection databaseConnection)
        {
            string updateTimeRegData = "UPDATE TimeRegistration PUT TimeRegistration_CheckOut = @timeout WHERE TimeRegistration_Id = @regid";
            
            using (SqlCommand updateTimeRegCommand = new SqlCommand(updateTimeRegData, databaseConnection))
            {
                updateTimeRegCommand.Parameters.AddWithValue("@timeout", datentime);
                updateTimeRegCommand.Parameters.AddWithValue("@regid", regid);
                updateTimeRegCommand.ExecuteNonQuery();
            }        }

        private void AddTimeRegToDB(string datentime, int rid, int pid, SqlConnection databaseConnection)
        {
            string insertTimeRegData = "INSERT INTO TimeRegistration(TimeRegistration_CheckIn, FK_RoomId, FK_RegPersonId) VALUES(@timein, @roomid, @personid)";

            using (SqlCommand insertTimeRegCommand = new SqlCommand())
            {
                insertTimeRegCommand.Parameters.AddWithValue("@timein", datentime);
                insertTimeRegCommand.Parameters.AddWithValue("@roomid", rid);
                insertTimeRegCommand.Parameters.AddWithValue("@personid", pid);
                insertTimeRegCommand.ExecuteNonQuery();
            }
            
        }

        #endregion

        public int DeletePerson(string personID)
        {
            string deletecommand = $"DELETE FROM Person WHERE Person_Id = {personID}";
            using (SqlConnection databaseConnection = new SqlConnection(ConnectionString))
            {
                databaseConnection.Open();
                using (SqlCommand selectCommand = new SqlCommand(deletecommand, databaseConnection))
                {
                    DeleteLogin(int.Parse(personID));
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

        public Person EditPerson(string personID, PersonDataToAdd p)
        {
            string updatequery = $"UPDATE Person SET Person_FirstName =@fname, Person_LastName=@lname, Person_Email=@email,FK_RolesId=@fkroleid, FK_TeamId=@fkteamid,Person_StudentId=@studentid WHERE Person_Id = @personid";

            using (SqlConnection databaseConnection = new SqlConnection(ConnectionString))
            {
                databaseConnection.Open();
                using (SqlCommand updatecommand = new SqlCommand(updatequery, databaseConnection))
                {
                    updatecommand.Parameters.AddWithValue("@fname", p.fname);
                    updatecommand.Parameters.AddWithValue("@lname", p.lname);
                    updatecommand.Parameters.AddWithValue("@email", p.email);
                    updatecommand.Parameters.AddWithValue("@fkroleid", p.roles);
                    updatecommand.Parameters.AddWithValue("@fkteamid", p.teamid);
                    updatecommand.Parameters.AddWithValue("@studentid", p.studentid);
                    updatecommand.Parameters.AddWithValue("@personid", personID);

                    EditLogin(p.username, p.password, int.Parse(personID));
                    using (SqlDataReader reader = updatecommand.ExecuteReader())
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

        public int EditLogin(string username, string password, int personid)
        {
            string updatequery = $"UPDATE login SET login_UserName=@uname, login_Password=@pass WHERE FK_PersonId=@personid";

            using (SqlConnection databaseConnection = new SqlConnection(ConnectionString))
            {
                databaseConnection.Open();
                using (SqlCommand updateCommand = new SqlCommand(updatequery, databaseConnection))
                {
                    updateCommand.Parameters.AddWithValue("@uname", username);
                    updateCommand.Parameters.AddWithValue("@pass", password);
                    updateCommand.Parameters.AddWithValue("@personid", personid);

                    int rowsaffected = updateCommand.ExecuteNonQuery();
                    return rowsaffected;
                }
            }
        }







        //opret ny person 

        public void AddPerson(PersonDataToAdd p)
        {
            decimal personId = AddPersonToDB(p.fname, p.lname, p.email, p.roles, p.studentid, p.teamid);
            int personIdInt = decimal.ToInt32(personId);
            AddLoginToDB(p.username, p.password, personIdInt);
        }




        public decimal AddPersonToDB(string fname, string lname, string email, int roles, string studentid, int teamid)
        {
            string CreatePersons = "INSERT INTO Person(Person_FirstName, Person_LastName, Person_Email,FK_RolesId, FK_TeamId,Person_StudentId) VALUES('"+fname+"', '"+lname+"', '"+email+"', "+roles+", "+teamid+", '"+studentid+"')";
            string identitysql = "SELECT IDENT_CURRENT('Person') as PersonId";
            using (SqlConnection databaseConnection = new SqlConnection(ConnectionString))
            {
                databaseConnection.Open();
                using (SqlCommand insertCommand = new SqlCommand(CreatePersons, databaseConnection))
                {
                    insertCommand.ExecuteNonQuery();

                }
                using (SqlCommand selectCommand = new SqlCommand(identitysql, databaseConnection))
                {
                    using (SqlDataReader reader = selectCommand.ExecuteReader())
                    {
                         bool IsId = reader.Read();
                        if (IsId == false)
                            return -1;
                        decimal lastId = reader.GetDecimal(0);
                        return lastId;
                    }
                }
                  
            }
        }

        public int AddLoginToDB(string username, string password, int personId)
        {
            string CreateLogins = $"INSERT INTO login(login_UserName, login_Password, FK_PersonId) VALUES(@username, @password, @personId)";

            using (SqlConnection databaseConnection = new SqlConnection(ConnectionString))
            {
                databaseConnection.Open();
               
                using (SqlCommand insertCommand = new SqlCommand(CreateLogins, databaseConnection))
                {
                    insertCommand.Parameters.AddWithValue("@username", username);
                    insertCommand.Parameters.AddWithValue("@password", password);
                    insertCommand.Parameters.AddWithValue("@personId", personId);
                    int rowsaffected = insertCommand.ExecuteNonQuery();
                    return rowsaffected;
                }
            }
        }

        //henter alle hold
        public IList<Team> GetTeams()
        {
            string selectAllTeams = "select * from Team";

            using (SqlConnection databaseConnection = new SqlConnection(ConnectionString))
            {
                databaseConnection.Open();
                using (SqlCommand selectCommand = new SqlCommand(selectAllTeams, databaseConnection))
                {
                    using (SqlDataReader reader = selectCommand.ExecuteReader())
                    {
                        IList<Team> teams = new List<Team>();
                        while (reader.Read())
                        {
                            Team team = ReadTeam(reader);
                            teams.Add(team);
                        }
                        return teams;
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

        public AllPersonData ReadAllPersonData(IDataRecord reader)
        {
            int rrid = reader.GetInt32(0);
            string rfirstname = reader.GetString(1);
            string rlastname = reader.GetString(2);
            string remail = reader.GetString(3);
            int rfkrolesid = reader.GetInt32(4);
            int rfkteamid = reader.GetInt32(5);
            string rstudentid = reader.GetString(6);
            int rrolesid = reader.GetInt32(7);
            string rrolestype = reader.GetString(8);
            string rrolesname = reader.GetString(9);
            int rteamid = reader.GetInt32(10);
            string rteamname = reader.GetString(11);
            int rloginid = reader.GetInt32(12);
            string rusername = reader.GetString(13);
            string rpassword = reader.GetString(14);
            int rfkpersonid = reader.GetInt32(15);
            AllPersonData person = new AllPersonData()
            {
                rid = rrid,
                firstname = rfirstname,
                lastname = rlastname,
                email = remail,
                fkrolesid = rfkrolesid,
                fkteamid = rfkteamid,
                studentid = rstudentid,
                rolesid = rrolesid,
                rolestype = rrolestype,
                rolesname = rrolesname,
                teamid = rteamid,
                teamname = rteamname,
                loginid = rloginid,
                username = rusername,
                password = rpassword,
                fkpersonid = rfkpersonid
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

        public Room ReadRoom(IDataReader reader)
        {
            int roomid = reader.GetInt32(0);
            string roomname = reader.GetString(1);

            Room room = new Room()
            {
                Room_Id = roomid,
                Room_Name = roomname
            };
            return room;
        }

        public Team ReadTeam(IDataReader reader)
        {
            int tid = reader.GetInt32(0);
            string tname = reader.GetString(1);

            Team team = new Team()
            {
                Team_Id = tid,
                Team_Name = tname
            };
            return team;
        }

        public TimeRegistration ReadTimeReg(IDataReader reader)
        {
            int regid = reader.GetInt32(0);
            DateTime cin = reader.GetDateTime(1);
            DateTime cout = reader.GetDateTime(2);
            int fkrid = reader.GetInt32(3);
            int fkpid = reader.GetInt32(4);

            TimeRegistration timeRegistration = new TimeRegistration()
            {
                TimeRegistration_Id = regid,
                TimeRegistration_CheckIn = cin,
                TimeRegistration_CheckOut = cout,
                FK_RoomId = fkrid,
                FK_RegPersonId = fkpid
            };
            return timeRegistration;
        }


    }


}
