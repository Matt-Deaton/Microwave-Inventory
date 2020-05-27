using DataAccessLayer;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessInterfaces;
using LogicLayerInterfaces;

namespace LogicLayer
{
	/// <summary>
	/// CREATED BY: Matt Deaton
	/// DATE CREATED: 2019-05-09
	/// 
	/// PartManager class, inherits IPartManger
	/// 
	/// </summary>
	public class PartManager : IPartManager
	{
		private IPartAccessor _partAccessor;

		// Default Construtor
		public PartManager()
		{
			_partAccessor = new PartAccessor();
		}

		// Test Constructor
		public PartManager(IPartAccessor partAccessor)
		{
			_partAccessor = partAccessor;
		}

		public bool AddNewPart(Part part)
		{
			bool result = false;

			try
			{
				result = _partAccessor.InsertNewPart(part);
			}
			catch (Exception ex)
			{
				throw new ApplicationException("Part could not be added.", ex);
			}

			return result;
		}// End AddNewPart()

		public bool AddPart(Part part)
		{
			bool result = false;

			try
			{
				result = (_partAccessor.InsertPartToInventory(part) > 0);
			}
			catch (Exception ex)
			{
				throw new ApplicationException("Part Failed to Add", ex);
			}
			return result;
		}// End AddPart()

		public bool EditPartInformation(Part part)
		{
			bool result = false;

			try
			{
				Part oldPart = _partAccessor.SelectPartByPartNumberForMovingInventory(part.PartNumber);
				result = (1 == _partAccessor.UpdatePartInformation(oldPart, part));
			}
			catch (Exception ex)
			{
				throw new ApplicationException("Failed to Update Part Information.", ex);
			}

			return result;
		}// End EditPartInformation()

		public List<Part> RetrievePartByPartNumber(string partNumber)
		{
			List<Part> part = null;
			try
			{
				part = _partAccessor.FindPartByPartNumber(partNumber);
			}
			catch (Exception ex)
			{
				throw new ApplicationException("Part could not be found", ex);
			}

			return part;
		}// End FindPartByPartNumber()

		public bool MovePart(string partNumber, string oldLocation, string newLocation, int oldQuantity, int newQuantity)
		{
			bool result = false;
			try
			{
				string oldLoc = oldLocation;
				string newLoc = newLocation;
				int oldQuant = oldQuantity;
				int newQuant = newQuantity;

				result = _partAccessor.MovePart(partNumber, oldLoc, newLoc, oldQuant, newQuant);
			}
			catch (Exception ex)
			{
				throw new ApplicationException("Move Failed", ex);
			}

			return result;
		}// End MovePart()

		public List<Part> RetrieveAllPartInformation()
		{
			List<Part> partInformation = null;
			try
			{
				partInformation = _partAccessor.SelectAllPartInformation();
			}
			catch (Exception ex)
			{
				throw new ApplicationException("Information could not be found!", ex);
			}
			return partInformation;
		}// End RetrieveAllPartInformation()

		public List<Part> RetrieveAllParts()
		{
			List<Part> parts = null;
			try
			{
				parts = _partAccessor.SelectAllParts();
			}
			catch (Exception ex)
			{
				throw new ApplicationException("Parts could not be found", ex);
			}
			return parts;
		}// End RetrieveParts()

		public PartVM RetrievePartInformationByPartNumber(string partNumber)
		{
			try
			{
				return _partAccessor.SelectPartInformationByPartNumber(partNumber);
			}
			catch (Exception ex)
			{
				throw new ApplicationException("Part could not be found.", ex);
			}
		}// End RetrievePartInformationByPartNumber()

		public bool DeletePartByPartNumber(string partNumber)
		{
			bool result = false;
			try
			{
				result = (_partAccessor.DeletePartByPartNumber(partNumber) > 0);
			}
			catch (Exception ex)
			{
				throw new ApplicationException("Part could not be deleted.", ex);
			}
			return result;
		}// End DeletePartByPartNumber()

	}// End PartManager class

}// LogicLayer namespace
