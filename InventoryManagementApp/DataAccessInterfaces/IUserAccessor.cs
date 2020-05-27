using DataObjects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessInterfaces
{
    /// <summary>
    /// Data Access Layer Interface for the UserAccessor
    /// </summary>
	public interface IUserAccessor
	{
		User AuthenticateUser(string username, string passwordHash);

		bool UpdatePassword(int userID, string oldPassword, string newPassword);

        List<User> SelectUsersByActive(bool active = true);

        int UpdateEmployee(User oldUser, User newUser);

        int InsertEmployee(User user);

        int ActivateEmployee(int employeeID);

        int DeactivateEmployee(int employeeID);

        List<string> SelectAllRoles();

        List<string> SelectRolesByEmployeeID(int employeeID);

        int InsertOrDeleteEmployeeRole(int employeeID, string role, bool delete = false);

        User GetUserByEmail(string email);
    }
}
