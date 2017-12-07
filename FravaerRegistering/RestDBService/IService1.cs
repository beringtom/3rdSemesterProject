﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace RestDBService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json, UriTemplate = "Person/")]
        IList<AllPersonData> GetAllPersons();

        [OperationContract]
        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json, UriTemplate = "Person/{id}")]
        AllPersonData GetOnePersons(string id);

        [OperationContract]
        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json, UriTemplate = "Person/Team/{id}")]
        IList<AllPersonData> GetAllPersonsInTeam(string id);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "Login/")]
        Login Getlogin(Login loginUserPaswords);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "Person/")]
        void AddPerson(PersonDataToAdd p);


        [OperationContract]
        [WebInvoke(Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "Person/{id}")]
        Person EditPerson(string id, PersonDataToAdd p);

        [OperationContract]
        [WebInvoke(Method = "DELETE", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "Person/{id}")]
        int DeletePerson(string id);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "Team/")]
        void AddTeam(Team t);

        [OperationContract]
        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json,ResponseFormat = WebMessageFormat.Json, UriTemplate = "Teams/")]
        IList<Team> GetTeams();


        //[OperationContract]
        //Person ReadPerson(IDataRecord reader);
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
             UriTemplate = "Sensor/")]
        string SensorCheck(SonsorData s);

        [OperationContract]
        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
             UriTemplate = "Schedule/{id}")]
        IList<Schedule> GetScheduleForTeam(string id);

    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]

    public class Login
    {
        [DataMember]
        public int Login_Id;
        [DataMember]
        public string Login_UserName;
        [DataMember]
        public string Login_Password;
        [DataMember]
        public int FK_PersonId;
    }

    public class Person
    {
        [DataMember]
        public int Person_Id;
        [DataMember]
        public string Person_FirstName;
        [DataMember]
        public string Person_LastName;
        [DataMember]
        public string Person_Email;
        [DataMember]
        public int FK_RolesId;
        [DataMember]
        public int FK_TeamId;
        [DataMember]
        public string Roles_Name;
        [DataMember]
        public string Team_Name;
        [DataMember]
        public string Person_StudentId;
    }

    public class Roles
    {
        [DataMember]
        public int Roles_Id;
        [DataMember]
        public int Roles_Type;
        [DataMember]
        public string Roles_Name;
    }

    public class TimeRegistration
    {
        [DataMember]
        public int TimeRegistration_Id;
        [DataMember]
        public DateTime TimeRegistration_CheckIn;
        [DataMember]
        public DateTime TimeRegistration_CheckOut;
        [DataMember]
        public int FK_RoomId;
        [DataMember]
        public int FK_RegPersonId;
    }

    public class Room
    {
        [DataMember]
        public int Room_Id;
        [DataMember]
        public string Room_Name;
    }

    public class Team
    {
        [DataMember]
        public int Team_Id;
        [DataMember]
        public string Team_Name;
    }

    public class PersonDataToAdd
    {
         [DataMember]
         public string fname;
         [DataMember]
         public string lname;
         [DataMember]
         public string email;
         [DataMember]
         public string username;
         [DataMember]
         public string password;
         [DataMember]
         public int roles;
         [DataMember]
         public string studentid;
         [DataMember]
         public int teamid;
     }

public class AllPersonData
    {
        public int rid;
        [DataMember]
        public string firstname;
        [DataMember]
        public string lastname;
        [DataMember]
        public string email;
        [DataMember]
        public int fkrolesid;
        [DataMember]
        public int fkteamid;
        [DataMember]
        public string studentid;
        [DataMember]
        public int rolesid;
        [DataMember]
        public string rolestype;
        [DataMember]
        public string rolesname;
        [DataMember]
        public int teamid;
        [DataMember]
        public string teamname;
        [DataMember]
        public int loginid;
        [DataMember]
        public string username;
        [DataMember]
        public string password;
        [DataMember]
        public int fkpersonid;
    }

    public class SonsorData
    {
        [DataMember]
        public string Room;
        [DataMember]
        public string Time;
        [DataMember]
        public string CardID;

    }

    public class Schedule
    {
        [DataMember]
        public int ScheduleId;
        [DataMember]
        public int FKScheduleRoomId;
        [DataMember]
        public int FKScheduleTeamId;
        [DataMember]
        public string ScheduleTimefrom;
        [DataMember]
        public string ScheduleTimeTo;
        [DataMember]
        public string ScheduleLecture;
    }
    
}