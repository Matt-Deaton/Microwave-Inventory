using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace LogicLayerInterfaces
{
	public interface IUserManager
	{
		User AuthenticateUser(string email, string password);

		bool UpdatePassword(int userID, string oldPassword, string newPassword);

        int RetrieveUserIDFromEmail(string email);

        bool FindUser(string email);

        List<User> RetrieveUserListByActive(bool active = true);

        bool EditUser(User oldUser, User newUser);

        bool AddUser(User user);

        bool SetUserActiveState(bool active, int employeeID);

        List<string> RetrieveEmployeeRoles(int employeeID);

        List<string> RetrieveEmployeeRoles();

        bool DeleteUserRole(int employeeID, string role);

        bool AddUserRole(int employeeID, string role);

    }// End interface IUserManager
}
