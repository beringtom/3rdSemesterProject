using System;
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

        public IList<Person> GetAllPersons()
        {
            const string selectAllPersons = "select * from Person";

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

        public Person ReadPerson(IDataRecord reader)
        {
            int id = reader.GetInt32(0);
            string firstname = reader.GetString(1);
            string lastname = reader.GetString(2);
            string email = reader.GetString(3);
            string studentid = reader.GetString(4);
            Person person = new Person()
            {
                Person_Id = id,
                Person_FirstName = firstname,
                Person_LastName = lastname,
                Person_Email = email,
                Person_StudentId = studentid
            };
            return person;
        }
    }
}