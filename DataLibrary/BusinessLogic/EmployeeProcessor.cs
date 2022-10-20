using DataLibrary.DataAccess;
using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.BusinessLogic
{
    // THIS CLASS IS WHAT TIES THE TWO EmployeeModels TOGETHER
    // This BusinessLogic creates the employee and sends the data to SQL thru 'string sql' along with 'data'
    // from EmployeeModel (DataLibrary version).
    // Class is static because we're not instantiating anything or storing data here
    public static class EmployeeProcessor
    {
        // This class is based off of the DataLibrary EmployeeModel, so we're passing in those values.
        // When adding EmployeeModel in this method, be sure to use the one from DataLibrary, NOT MVCApp.
        public static int CreateEmployee(int employeeId, string firstName,
            string lastName, string emailAddress)
        {
            EmployeeModel data = new EmployeeModel
            {
                EmployeeId = employeeId,
                FirstName = firstName,
                LastName = lastName,
                EmailAddress = emailAddress
            };

            // '@' lets us do multiple lines without the '+' and all the rest
            // The '@varNames' are parameterized SQL. '@' indicates that it's a var. It matches the info from 'data' above
            // with the SQL string below. It's taking 'EmployeeModel' and feeding data into 'string sql' to SaveData() in
            // the return statement. Parameterized SQL recognizes what the '@varNames' mean, so no need for single quotes to ID them.
            string sql = @"insert into dbo.Employee (EmployeeId, FirstName, LastName, EmailAddress)
                            values(@EmployeeId, @FirstName, @LastName, @EmailAddress);";

            // I don't need to specify type on params here because 'data' and 'SaveData' are already of type 'EmployeeModel'
            return SqlDataAccess.SaveData(sql, data);
        }

        public static List<EmployeeModel> LoadEmployees()
        {
            // Simple SQL statement: grab everything from an employee's listing in the DB...
            string sql = @"select Id, EmployeeId, FirstName, LastName, EmailAddress
                            from dbo.Employee;";

            // ...and return it back to us. <EmployeeModel> is in these brackets because we're not passing <T> IN somewhere.
            // This 'sql' isn't parameterized because there's no parameters to pass in.
            return SqlDataAccess.LoadData<EmployeeModel>(sql);
        }
    }
}
