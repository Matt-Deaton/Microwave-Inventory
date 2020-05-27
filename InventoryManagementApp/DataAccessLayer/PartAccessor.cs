using DataObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessInterfaces;

namespace DataAccessLayer
{
	/// <summary>
	/// PartAccessor class, inherits from the IPartAccessor
	/// </summary>
	public class PartAccessor : IPartAccessor
	{
		/// <summary>
		/// CREATED BY: Matt Deaton
		/// DATE CREATED: 2019-11-01
		/// 
		/// Method to find a part based on the partNumber
		/// 
		/// </summary>
		/// <param name="partNumber"></param>
		/// <returns></returns>
		public List<Part> FindPartByPartNumber(string partNumber)
		{
			List<Part> parts = new List<Part>();

			var conn = DBConnection.GetConnection();
			var cmd = new SqlCommand("sp_select_by_part_number", conn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@PartNumber", SqlDbType.NVarChar, 10);
			cmd.Parameters["@PartNumber"].Value = partNumber;

			try
			{
				conn.Open();
				var reader = cmd.ExecuteReader();
				if (reader.HasRows)
				{
					while (reader.Read())
					{
						var part = new Part();
						part.PartNumber = reader.GetString(0);
						part.Quantity = reader.GetInt32(1);
						part.PartName = reader.GetString(2);
						part.Location = reader.GetString(3);

						parts.Add(part);
					}
				}

				reader.Close();
			}
			catch (Exception up)
			{
				throw up;
			}
			finally
			{
				conn.Close();
			}

			return parts;

		}// End FindPartByPartNumber()

		/// <summary>
		/// CREATED BY: Matt Deaton
		/// DATE CREATED: 2019-11-01
		/// 
		/// Method to Move a Part in the system. 
		/// 
		/// </summary>
		/// <param name="partNumber"></param>
		/// <param name="oldLocation"></param>
		/// <param name="newLocation"></param>
		/// <param name="oldQuantity"></param>
		/// <param name="newQuantity"></param>
		/// <returns></returns>
		public bool MovePart(string partNumber, string oldLocation, string newLocation, int oldQuantity, int newQuantity)
		{
			bool result = false;

			var conn = DBConnection.GetConnection();
			var cmd = new SqlCommand("sp_move_parts", conn);
			cmd.CommandType = CommandType.StoredProcedure;

			cmd.Parameters.Add("@PartNumber", SqlDbType.NVarChar, 10);
			cmd.Parameters.Add("@OldQuantity", SqlDbType.Int);
			cmd.Parameters.Add("@OldLocationNumber", SqlDbType.NVarChar, 4);
			cmd.Parameters.Add("@NewQuantity", SqlDbType.Int);
			cmd.Parameters.Add("@NewLocationNumber", SqlDbType.NVarChar, 4);

			cmd.Parameters["@PartNumber"].Value = partNumber;
			cmd.Parameters["@OldQuantity"].Value = oldQuantity;
			cmd.Parameters["@OldLocationNumber"].Value = oldLocation;
			cmd.Parameters["@newQuantity"].Value = newQuantity;
			cmd.Parameters["@NewLocationNumber"].Value = newLocation;

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
		}// End MovePart()

		/// <summary>
		/// CREATED BY: Matt Deaton
		/// DATE CREATED: 2019-11-01
		/// 
		/// Method to Select All Parts, to be used for material handlers and recieving clerks.
		/// 
		/// </summary>
		/// <returns></returns>
		public List<Part> SelectAllParts()
		{
			List<Part> parts = new List<Part>();

			var conn = DBConnection.GetConnection();
			var cmd = new SqlCommand("sp_select_parts", conn);
			cmd.CommandType = CommandType.StoredProcedure;
			try
			{
				conn.Open();
				var reader = cmd.ExecuteReader();
				if (reader.HasRows)
				{
					while (reader.Read())
					{
						var part = new Part();
						part.PartNumber = reader.GetString(0);
						part.Quantity = reader.GetInt32(1);
						part.PartName = reader.GetString(2);
						part.Location = reader.GetString(3);

						parts.Add(part);
					}
				}

				reader.Close();
			}
			catch (Exception up)
			{
				throw up;
			}
			finally
			{
				conn.Close();
			}

			return parts;

		}// End SelectAllParts()

		/// <summary>
		/// CREATED BY: Matt Deaton
		/// DATE CREATED: 2019-11-01
		/// 
		/// Method to Insert a New Part into the System. To be used by managers and buyers.
		/// 
		/// </summary>
		/// <param name="part"></param>
		/// <returns></returns>
		public bool InsertNewPart(Part part)
		{
			bool newPart = false;

			var conn = DBConnection.GetConnection();
			var cmd = new SqlCommand("sp_insert_new_part", conn);
			cmd.CommandType = CommandType.StoredProcedure;

			cmd.Parameters.AddWithValue("@PartNumber", part.PartNumber);
			cmd.Parameters.AddWithValue("@PartName", part.PartName);
			cmd.Parameters.AddWithValue("@Cost", part.Cost);
			cmd.Parameters.AddWithValue("@Description", part.Description);

			try
			{
				conn.Open();
				int rows = cmd.ExecuteNonQuery();

				newPart = (rows == 1);
			}
			catch (Exception up)
			{
				throw up;
			}
			finally
			{
				conn.Close();
			}

			return newPart;
		}// End InsertNewPart()

		/// <summary>
		/// CREATED BY: Matt Deaton
		/// DATE CREATED: 2019-11-01
		/// 
		/// Method to display all the part information. To be used by managers and buyers.
		/// 
		/// </summary>
		/// <returns></returns>
		public List<Part> SelectAllPartInformation()
		{

			List<Part> parts = new List<Part>();

			var conn = DBConnection.GetConnection();
			var cmd = new SqlCommand("sp_select_all_part_information", conn);
			cmd.CommandType = CommandType.StoredProcedure;
			try
			{
				conn.Open();
				var reader = cmd.ExecuteReader();
				if (reader.HasRows)
				{
					while (reader.Read())
					{
						var part = new Part();
						part.PartNumber = reader.GetString(0);
						part.PartName = reader.GetString(1);
						part.Cost = reader.GetDecimal(2);
						part.Description = reader.GetString(3);

						parts.Add(part);
					}
				}

				reader.Close();
			}
			catch (Exception up)
			{
				throw up;
			}
			finally
			{
				conn.Close();
			}

			return parts;
		}// SelectAllPartInformation()

		/// <summary>
		/// CREATED BY: Matt Deaton
		/// DATE CREATED: 2019-11-01
		/// 
		/// Method to recieve a part and add it into inventory. To be used by recieving clerks. 
		/// 
		/// </summary>
		/// <param name="part"></param>
		/// <returns></returns>
		public int InsertPartToInventory(Part part)
		{
			int rows = 0;

			var conn = DBConnection.GetConnection();
			var cmd = new SqlCommand("sp_recieve_part", conn);
			cmd.CommandType = CommandType.StoredProcedure;

			cmd.Parameters.AddWithValue("@PartNumber", part.PartNumber);
			cmd.Parameters.AddWithValue("@Quantity", part.Quantity);
			cmd.Parameters.AddWithValue("@LocationNumber", part.Location);

			try
			{
				conn.Open();
				rows = cmd.ExecuteNonQuery();
			}
			catch (Exception up)
			{
				throw up;
			}
			finally
			{
				conn.Close();
			}
			return rows;
		}// End InsertPartToInventory()

		/// <summary>
		/// CREATED BY: Matt Deaton
		/// DATE CREATED: 2019-11-01
		/// 
		/// Method to update part information. To be used by manager or buyer. 
		/// 
		/// </summary>
		/// <param name="oldPart"></param>
		/// <param name="newPart"></param>
		/// <returns></returns>
		public int UpdatePartInformation(Part oldPart, Part newPart)
		{
			int rows = 0;

			var conn = DBConnection.GetConnection();
			var cmd = new SqlCommand("sp_update_part_informtion", conn);
			cmd.CommandType = CommandType.StoredProcedure;

			cmd.Parameters.AddWithValue("@PartNumber", oldPart.PartNumber);

			cmd.Parameters.AddWithValue("@NewPartName", newPart.PartName);
			cmd.Parameters.AddWithValue("@NewCost", newPart.Cost);
			cmd.Parameters.AddWithValue("@NewDescription", newPart.Description);

			cmd.Parameters.AddWithValue("@OldPartName", oldPart.PartName);
			cmd.Parameters.AddWithValue("@OldCost", oldPart.Cost);
			cmd.Parameters.AddWithValue("@OldDescription", oldPart.Description);

			try
			{
				conn.Open();
				rows = cmd.ExecuteNonQuery();
				if (rows == 0)
				{
					throw new ApplicationException("Part not found.");
				}
			}
			catch (Exception up)
			{
				throw up; ;
			}
			finally
			{
				conn.Close();
			}
			return rows;
		}// End UpdatePartInformation()

		/// <summary>
		/// CREATED BY: Matt Deaton
		/// DATE CREATED: 2020-05-09
		/// 
		/// Method to Select a Part to Move in Inventory.
		/// 
		/// </summary>
		/// <param name="partNumber"></param>
		/// <returns></returns>
		public PartVM SelectPartByPartNumberForMovingInventory(string partNumber)
		{
			PartVM part = null;

			var conn = DBConnection.GetConnection();
			var cmd = new SqlCommand("sp_select_by_part_number", conn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@PartNumber", partNumber);

			try
			{
				conn.Open();
				var reader = cmd.ExecuteReader();
				if (reader.HasRows)
				{
					while (reader.Read())
					{
						part = new PartVM()
						{
							PartNumber = reader.GetString(0),
							Quantity = reader.GetInt32(1),
							PartName = reader.GetString(2),
							Location = reader.GetString(3),
							Description = reader.GetString(4),
							Cost = reader.GetDecimal(5)
						};
					}
				}

				reader.Close();
			}
			catch (Exception up)
			{
				throw up;
			}
			finally
			{
				conn.Close();
			}
			return part;
		}// End SelectPartByPartNumberForMovingInventory()

		/// <summary>
		/// CREATED BY: Matt Deaton
		/// DATE CREATED: 2020-05-09
		/// 
		/// Method to Select Part information by the part number. 
		/// 
		/// </summary>
		/// <param name="partNumber"></param>
		/// <returns></returns>
		public PartVM SelectPartInformationByPartNumber(string partNumber)
		{
			PartVM part = null;

			var conn = DBConnection.GetConnection();
			var cmd = new SqlCommand("sp_select_part_information_by_part_number", conn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@PartNumber", partNumber);

			try
			{
				conn.Open();
				var reader = cmd.ExecuteReader();

				if (reader.HasRows)
				{
					reader.Read();

					part = new PartVM()
					{
						PartNumber = reader.GetString(0),
						PartName = reader.GetString(1),
						Description = reader.GetString(2),
						Cost = reader.GetDecimal(3)
					};
				}

				reader.Close();
			}
			catch (Exception up)
			{
				throw up;
			}
			finally
			{
				conn.Close();
			}
			return part;
		}// End SelectPartInformationByPartNumber()

		/// <summary>
		/// CREATED BY: Matt Deaton
		/// DATE CREATED: 2020-05-09
		/// 
		/// Method to delete a part, by part number. 
		/// 
		/// </summary>
		/// <param name="partNumber"></param>
		/// <returns></returns>
		public int DeletePartByPartNumber(string partNumber)
		{
			int result = 0;

			var conn = DBConnection.GetConnection();
			var cmd = new SqlCommand("sp_delete_part_by_part_number", conn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@PartNumber", partNumber);

			try
			{
				conn.Open();
				result = cmd.ExecuteNonQuery();
			}
			catch (Exception)
			{
				throw;
			}
			finally
			{
				conn.Close();
			}

			return result;
		}// End DeletePartByPartNumber()

	}// End PartAccessor class

}// DataAccessLayer namespace
