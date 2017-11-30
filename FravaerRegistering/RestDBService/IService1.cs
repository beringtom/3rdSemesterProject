using System;
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
        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json, UriTemplate = "Person")]
        IList<Person> GetAllPersons();

        [OperationContract]
        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json, UriTemplate = "Person/{id}")]
        IList<Person> GetOnePersons(string id);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "Login")]
        IList<Login> Getlogin(Login loginUserPaswords);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "Person")]
        IList<Person> AddPersons(Person addPerson);
        
        //[OperationContract]
        //Person ReadPerson(IDataRecord reader);
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

    // Schedule bliver sandsynligvis slettet igen, hvis den er unødvendig.
    // Der skal mere research af Outlook til før vi ved det.

    //public class Lecture
    //{
    //    [DataMember] public int Lecture_Id;
    //    [DataMember] public DateTime Lecture_StartTime;
    //    [DataMember] public DateTime Lecture_EndTime;
    //}

    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}