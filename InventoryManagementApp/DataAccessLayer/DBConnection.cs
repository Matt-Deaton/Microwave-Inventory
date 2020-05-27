using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DataAccessLayer
{
	/// <summary>
	/// DB Conncection class, for accessing the Database in which the data is stored 
	/// for the inventory applicaiton
	/// </summary>
	internal static class DBConnection
	{
		private static string connectionString =
			@"Data Source=LAPTOP-MATT\SQL2017;Initial Catalog=Inventory_DB;Integrated Security=True";

			//@"Data Source=localhost;Initial Catalog=Inventory_DB;Integrated Security=True"

		public static SqlConnection GetConnection()
		{
			var conn = new SqlConnection(connectionString);
			return conn;
		}
	}

}
