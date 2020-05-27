using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using System.Security.Cryptography;
using DataObjects;
using DataAccessInterfaces;
using LogicLayerInterfaces;


namespace LogicLayer
{
	public class UserManager : IUserManager
	{
		//Code to an interface, not an implemenatation
		private IUserAccessor _userAccessor;

		// Default Construtor
		public UserManager()
		{
			_userAccessor = new UserAccessor();
		}

		// Test Constructor
		public UserManager(IUserAccessor userAccessor)
		{
			_userAccessor = userAccessor;
		}

		public bool UpdatePassword(int userID, string oldPassword, string newPassword)
		{
			bool result = false;
			try
			{
				string oldPass = hashPassword(oldPassword);
				string newPass = hashPassword(newPassword);

				result = _userAccessor.UpdatePassword(userID, oldPass, newPass);
			}
			catch (Exception ex)
			{
				throw new ApplicationException("Update Failed", ex);
			}

			return result;
		}// End UpdatePassword()

		public User AuthenticateUser(string email, string password)
		{
			User user = null;
			try
			{
				var passwordHash = hashPassword(password);
				password = null;

				user = _userAccessor.AuthenticateUser(email, passwordHash);
			}
			catch (Exception ex)
			{
				throw new ApplicationException("Bad Email or Password", ex);
			}

			return user;
		}// End AuthenticateUser()

		private string hashPassword(string source)
		{
			string result = "";
			byte[] data;

			using(SHA256 sha256hash = SHA256.Create())
			{
				data = sha256hash.ComputeHash(Encoding.UTF8.GetBytes(source));
				var s = new StringBuilder();

				for (int i = 0; i < data.Length; i++)
				{
					s.Append(data[i].ToString("x2"));
				}
				result = s.ToString().ToUpper();
			}

			return result;

        }// End hashPassword()

        public bool AddUser(User user)
        {
            bool result = true;
            try
            {
                result = _userAccessor.InsertEmployee(user) > 0;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("User not added", ex);
            }
            return result;
        }

        public bool AddUserRole(int employeeID, string role)
        {
            bool result = false;
            try
            {
                result = (1 == _userAccessor.InsertOrDeleteEmployeeRole(employeeID, role));
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Role not added!", ex);
            }
            return result;
        }

        public bool DeleteUserRole(int employeeID, string role)
        {
            bool result = false;
            try
            {
                result = (1 == _userAccessor.InsertOrDeleteEmployeeRole(employeeID, role, delete: true));
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Role not removed!", ex);
            }
            return result;
        }

        public bool EditUser(User oldUser, User newUser)
        {
            bool result = false;

            try
            {
                result = _userAccessor.UpdateEmployee(oldUser, newUser) == 1;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Update failed", ex);
            }

            return result;
        }

        public List<string> RetrieveEmployeeRoles(int employeeID)
        {
            List<string> roles = null;

            try
            {
                roles = _userAccessor.SelectRolesByEmployeeID(employeeID);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Roles not found", ex);
            }

            return roles;
        }

        public List<string> RetrieveEmployeeRoles()
        {
            List<string> roles = null;

            try
            {
                roles = _userAccessor.SelectAllRoles();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Roles not found", ex);
            }

            return roles;
        }

        public List<User> RetrieveUserListByActive(bool active = true)
        {
            try
            {
                return _userAccessor.SelectUsersByActive(active);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Data not found.", ex);
            }
        }

        public bool SetUserActiveState(bool active, int employeeID)
        {
            bool result = false;
            try
            {
                if (active)
                {
                    result = 1 == _userAccessor.ActivateEmployee(employeeID);
                }
                else
                {
                    result = 1 == _userAccessor.DeactivateEmployee(employeeID);
                }
                if (result == false)
                {
                    throw new ApplicationException("Employee record not updated.");
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Update failed!", ex);
            }
            return result;
        }

        public bool FindUser(string email)
        {
            try
            {
                return _userAccessor.GetUserByEmail(email) != null;
            }
            catch (ApplicationException ax)
            {
                if (ax.Message == "User not found.")
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Database Error", ex);
            }
        }
        
        public int RetrieveUserIDFromEmail(string email)
        {
            try
            {
                return _userAccessor.GetUserByEmail(email).EmployeeID;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Database Error", ex);
            }
        }

    }// End UserManager Class

}// End LogicLayer namespace
