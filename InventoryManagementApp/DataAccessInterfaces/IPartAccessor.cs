using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessInterfaces
{
	/// <summary>
	/// Data Access Layer Interface for the PartAccessor
	/// </summary>
	public interface IPartAccessor
	{
		List<Part> SelectAllParts();

		List<Part> FindPartByPartNumber(string partNumber);

		bool MovePart(string partNumber, string oldLocation, string newLocation, int oldQuantity, int newQuantity);

		int InsertPartToInventory(Part part);

		bool InsertNewPart(Part part);

		List<Part> SelectAllPartInformation();

		int UpdatePartInformation(Part oldPart, Part newPart);

		PartVM SelectPartByPartNumberForMovingInventory(string partNumber);

		PartVM SelectPartInformationByPartNumber(string partNumber);

		int DeletePartByPartNumber(string partNumber);
	}
}
