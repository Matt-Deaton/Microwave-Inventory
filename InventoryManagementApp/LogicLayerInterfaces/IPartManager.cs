using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayerInterfaces
{
	public interface IPartManager
	{

		List<Part> RetrieveAllParts();
		
		List<Part> RetrievePartByPartNumber(string partNumber);

		bool MovePart(string partNumber, string oldLocation, string newLocation, int oldQuantity, int newQuantity);

		bool AddPart(Part part);

		bool AddNewPart(Part part);

		List<Part> RetrieveAllPartInformation();

		bool EditPartInformation(Part part);

		PartVM RetrievePartInformationByPartNumber(string partNumber);

		bool DeletePartByPartNumber(string partNumber);
	}
}
