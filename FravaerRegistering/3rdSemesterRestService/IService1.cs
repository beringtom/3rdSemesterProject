using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace _3rdSemesterRestService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        string GetData(int value);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        // TODO: Add your service operations here

        
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]

    public class Login
    {
        [DataMember] public int Login_Id;
        [DataMember] public string Login_Username;
        [DataMember] public string Login_Password;
    }

    public class Person
    {
        [DataMember] public int Person_Id;
        [DataMember] public string Person_FirstName;
        [DataMember] public string Person_LastName;
        [DataMember] public string Person_Email;
        [DataMember] public string Person_StudentId;
    }

    public class Roles
    {
        [DataMember] public int Roles_Id;
        [DataMember] public int Roles_Type;
        [DataMember] public string Roles_Name;    
    }

    public class TimeRegistration
    {
        [DataMember] public int TimeRegistration_Id;
        [DataMember] public DateTime TimeRegistration_CheckIn;
        [DataMember] public DateTime TimeRegistration_CheckOut;
    }

    public class Room
    {
        [DataMember] public int Room_Id;
        [DataMember] public string Room_Name;
    }

    public class Team
    {
        [DataMember] public int Team_Id;
        [DataMember] public string Team_Name;
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
