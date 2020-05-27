using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;      
using System.Threading.Tasks;
using DataObjects;
using System.Data.SqlClient;
using System.Data;
using DataAccessInterfaces;

namespace DataAccessLayer
{
	public class UserAccessor : IUserAccessor
	{

		private User SelectUserByEmail(string email)
		{
			User user = null;

			// Start a connection and get a command objcet, stored procedure in 
			// this instance.
			var conn = DBConnection.GetConnection();
			var cmd1 = new SqlCommand("sp_select_user_by_email", conn);
			var cmd2 = new SqlCommand("sp_select_roles_by_userid", conn);

			// Add the parameters to the commmand, and set the values.
			// cmd2 has to wait for value is read in the first try/catch
			cmd1.CommandType = CommandType.StoredProcedure;
			cmd2.CommandType = CommandType.StoredProcedure;
			cmd1.Parameters.Add("@Email", SqlDbType.NVarChar, 250);
			cmd2.Parameters.Add("@EmployeeID", SqlDbType.Int);
			cmd1.Parameters["@Email"].Value = email;

			try
			{
				conn.Open();
				var reader1 = cmd1.ExecuteReader();

				user = new User();

				user.Email = email;
				if (reader1.Read())
				{
					user.EmployeeID = reader1.GetInt32(0);
					user.FirstName = reader1.GetString(1);
					user.LastName = reader1.GetString(2);
					user.PhoneNumber = reader1.GetString(3);
				}
				else
				{
					throw new ApplicationException("User not found");
				}
				reader1.Close();

				cmd2.Parameters["@EmployeeID"].Value = user.EmployeeID;
				var reader2 = cmd2.ExecuteReader();

				List<string> roles = new List<string>();
				while (reader2.Read())
				{
					roles.Add(reader2.GetString(0));
				}
				reader2.Close();

				user.Roles = roles;

			}
			catch (Exception up)
			{
				throw up;
			}
			finally
			{
				conn.Close();
			}

			return user;
		}// End SelectUserByEmail()

        /// <summary>
		/// CREATED BY: Matt Deaton
		/// DATE CREATED: 2020-05-09
        /// 
        /// Method to update the user password, must be done the first time a user logs in to the system. 
        /// 
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
		public bool UpdatePassword(int userID, string oldPassword, string newPassword)
		{
			bool result = false;

			var conn = DBConnection.GetConnection();
			var cmd = new SqlCommand("sp_update_password", conn);
			cmd.CommandType = CommandType.StoredProcedure;

			cmd.Parameters.Add("@EmployeeID", SqlDbType.Int);
			cmd.Parameters.Add("@OldPasswordHash", SqlDbType.NVarChar, 100);
			cmd.Parameters.Add("@NewPasswordHash", SqlDbType.NVarChar, 100);
			cmd.Parameters["@EmployeeID"].Value = userID;
			cmd.Parameters["@OldPasswordHash"].Value = oldPassword;
			cmd.Parameters["@NewPasswordHash"].Value = newPassword;

			try
			{
				conn.Open();
				int rowsAffected = cmd.ExecuteNonQuery();
				result = (rowsAffected == 1);
			}
			catch (Exception up)
			{
				throw up;
			}
			finally
			{
				conn.Close();
			}

			return result;
		}// End UpdatePassword()

        /// <summary>
        /// CREATED BY: Matt Deaton
        /// DATE CREATED: 2020-05-09
        /// 
        /// Method that makes sure the user is Active as well as in the system
        /// based on the credentials of a username and password
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="passwordHash"></param>
        /// <returns></returns>
        public User AuthenticateUser(string username, string passwordHash)
		{
			User user = null;
			int result = 0;

			// Start a connection and get a command objcet, stored procedure in 
			// this instance.
			var conn = DBConnection.GetConnection();
			var cmd = new SqlCommand("sp_authenticate_user", conn);
			cmd.CommandType = CommandType.StoredProcedure;
		
			// Add the parameters to the commmand, and set the values
			cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 250);
			cmd.Parameters.Add("@PasswordHash", SqlDbType.NVarChar, 100);
			cmd.Parameters["@Email"].Value = username;
			cmd.Parameters["@PasswordHash"].Value = passwordHash;

			// Open the connection, execute and process the results
			try
			{
				conn.Open();
				result = Convert.ToInt32(cmd.ExecuteScalar());
				if (result == 1)
				{
					user = SelectUserByEmail(username);
				}
				else
				{
					throw new ApplicationException("User not found!");
				}
			}
			catch (Exception up)
			{
				throw up;
			}
			finally
			{
				conn.Close();
			}
			
			return user;
		}// End AuthenticateUser()


        /// <summary>
        /// CREATED BY: Matt Deaton
        /// DATE CREATED: 2020-05-09
        /// 
        /// Method to select users that are active. 
        /// 
        /// </summary>
        /// <param name="active"></param>
        /// <returns></returns>
		public List<User> SelectUsersByActive(bool active = true)
		{
			List<User> users = new List<User>();

			var conn = DBConnection.GetConnection();
			var cmd = new SqlCommand("sp_select_users_by_active");
			cmd.Connection = conn;
			cmd.CommandType = CommandType.StoredProcedure;

			cmd.Parameters.Add("@Active", SqlDbType.Bit);
			cmd.Parameters["@Active"].Value = active;

			try
			{
				conn.Open();
				var reader = cmd.ExecuteReader();

				if (reader.HasRows)
				{
					while (reader.Read())
					{
						var user = new User();
						user.EmployeeID = reader.GetInt32(0);
						user.FirstName = reader.GetString(1);
						user.LastName = reader.GetString(2);
						user.PhoneNumber = reader.GetString(3);
						user.Email = reader.GetString(4);
						user.Active = reader.GetBoolean(5);
						users.Add(user);
					}
				}
				reader.Close();
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				conn.Close();
			}
			return users;
        }// End SelectUsersByActive()

        /// <summary>
        /// CREATED BY: Matt Deaton
        /// DATE CREATED: 2020-05-09
        /// 
        /// Method to update an employee
        /// 
        /// </summary>
        /// <param name="oldUser"></param>
        /// <param name="newUser"></param>
        /// <returns></returns>
        public int UpdateEmployee(User oldUser, User newUser)
        {
            int rows = 0;

            var conn = DBConnection.GetConnection();

            var cmd = new SqlCommand("sp_update_employee", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@EmployeeID", oldUser.EmployeeID);

            cmd.Parameters.AddWithValue("@NewFirstName", newUser.FirstName);
            cmd.Parameters.AddWithValue("@NewLastName", newUser.LastName);
            cmd.Parameters.AddWithValue("@NewPhoneNumber", newUser.PhoneNumber);
            cmd.Parameters.AddWithValue("@NewEmail", newUser.Email);

            cmd.Parameters.AddWithValue("@OldFirstName", oldUser.FirstName);
            cmd.Parameters.AddWithValue("@OldLastName", oldUser.LastName);
            cmd.Parameters.AddWithValue("@OldPhoneNumber", oldUser.PhoneNumber);
            cmd.Parameters.AddWithValue("@OldEmail", oldUser.Email);

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return rows;
        }// End UpdateEmployee

        /// <summary>
        /// CREATED BY: Matt Deaton
        /// DATE CREATED: 2020-05-09
        /// 
        /// Method to insert an employee
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public int InsertEmployee(User user)
        {
            int employeeID = 0;

            var conn = DBConnection.GetConnection();

            var cmd = new SqlCommand("sp_insert_employee", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
            cmd.Parameters.AddWithValue("@LastName", user.LastName);
            cmd.Parameters.AddWithValue("@PhoneNumber", user.PhoneNumber);
            cmd.Parameters.AddWithValue("@Email", user.Email);

            try
            {
                conn.Open();
                employeeID = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return employeeID;
        }// End InsertEmployee

        /// <summary>
        /// CREATED BY: Matt Deaton
        /// DATE CREATED: 2020-05-09
        /// 
        /// Method to activate an employee
        /// 
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        public int ActivateEmployee(int employeeID)
        {
            int rows = 0;

            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_reactivate_employee", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EmployeeID", employeeID);

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return rows;
        }// End ActivateEmployee

        /// <summary>
        /// CREATED BY: Matt Deaton
        /// DATE CREATED: 2020-05-09
        /// 
        /// Method to Deactive Employee
        /// 
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        public int DeactivateEmployee(int employeeID)
        {
            int rows = 0;

            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_deactivate_employee", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EmployeeID", employeeID);

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return rows;
        }// End DeactivateEmployee

        /// <summary>
        /// CREATED BY: Matt Deaton
        /// DATE CREATED: 2020-05-09
        /// 
        /// Method to select all the employee roles.
        /// 
        /// </summary>
        /// <returns></returns>
        public List<string> SelectAllRoles()
        {
            List<string> roles = new List<string>();

            // connection
            var conn = DBConnection.GetConnection();

            // command objects
            var cmd = new SqlCommand("sp_select_all_roles");
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                // open connection
                conn.Open();

                // execute the first command

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string role = reader.GetString(0);
                    roles.Add(role);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return roles;
        }// End SelectAllRoles

        /// <summary>
        /// CREATED BY: Matt Deaton
        /// DATE CREATED: 2020-05-09
        /// 
        /// Method to select roles by employee ID
        /// 
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        public List<string> SelectRolesByEmployeeID(int employeeID)
        {
            List<string> roles = new List<string>();

            // connection
            var conn = DBConnection.GetConnection();

            // command objects
            var cmd = new SqlCommand("sp_select_roles_by_employeeID");
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd.Parameters.Add("@EmployeeID", SqlDbType.Int);
            cmd.Parameters["@EmployeeID"].Value = employeeID;

            try
            {
                // open connection
                conn.Open();

                // execute the first command

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string role = reader.GetString(0);
                    roles.Add(role);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return roles;
        }// End SelectRolesByEmployeeID

        /// <summary>
        /// CREATED BY: Matt Deaton
        /// DATE CREATED: 2020-05-09
        /// 
        /// Method to Insert/Delete an employee role.
        /// 
        /// </summary>
        /// <param name="employeeID"></param>
        /// <param name="role"></param>
        /// <param name="delete"></param>
        /// <returns></returns>
        public int InsertOrDeleteEmployeeRole(int employeeID, string role, bool delete = false)
        {
            int rows = 0;

            string cmdText = delete ? "sp_delete_employee_role" : "sp_insert_employee_role";

            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@EmployeeID", employeeID);
            cmd.Parameters.AddWithValue("@RoleID", role);

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return rows;
        }// End InsertOrDeleteEmployeeRole()

        /// <summary>
        /// CREATED BY: Matt Deaton
        /// DATE CREATED: 2020-05-09
        /// 
        /// Method to get a user by their email. 
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public User GetUserByEmail(string email)
        {
            User user = null;

            // connection
            var conn = DBConnection.GetConnection();

            // command objects (2)
            var cmd1 = new SqlCommand("sp_select_user_by_email");
            var cmd2 = new SqlCommand("sp_select_roles_by_userID");

            cmd1.Connection = conn;
            cmd2.Connection = conn;

            cmd1.CommandType = CommandType.StoredProcedure;
            cmd2.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd1.Parameters.Add("@Email", SqlDbType.NVarChar, 250);
            cmd1.Parameters["@Email"].Value = email;

            cmd2.Parameters.Add("@EmployeeID", SqlDbType.Int);
            // we cannot set the value of this parameter yet

            try
            {
                // open connection
                conn.Open();

                // execute the first command
                var reader1 = cmd1.ExecuteReader();

                if (reader1.Read())
                {
                    user = new User();

                    user.EmployeeID = reader1.GetInt32(0);
                    user.FirstName = reader1.GetString(1);
                    user.LastName = reader1.GetString(2);
                    user.PhoneNumber = reader1.GetString(3);
                    user.Email = email;
                }
                else
                {
                    throw new ApplicationException("User not found.");
                }
                reader1.Close(); // this is no longer needed

                cmd2.Parameters["@EmployeeID"].Value = user.EmployeeID;
                var reader2 = cmd2.ExecuteReader();

                List<string> roles = new List<string>();
                while (reader2.Read())
                {
                    string role = reader2.GetString(0);
                    roles.Add(role);
                }
                user.Roles = roles;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return user;
        }// End GetUserByEmail()
    }// End UserAccessor Class

}// End DataAccessLayer namespace
