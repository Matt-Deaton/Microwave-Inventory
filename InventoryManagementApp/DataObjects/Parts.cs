using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DataObjects
{
	public class Part
	{
		[Required(ErrorMessage = "Please supply a Part Number")]
		[MaxLength(10, ErrorMessage = "The part number can not be longer than 10 characters")]
		[Display(Name = "Part Number")]
		public string PartNumber { get; set; }

		public string Location { get; set; }

		[Required(ErrorMessage = "Please supply a Part Name")]
		[MaxLength(30, ErrorMessage = "The part name can not be longer than 30 characters")]
		[Display(Name = "Part Name")]
		public string PartName { get; set; }

		public int Quantity { get; set; }

		[Required(ErrorMessage = "Please the Cost of the Part")]
		public decimal Cost { get; set; }

		[Required(ErrorMessage = "Please supply a Description of the Part")]
		[MaxLength(200, ErrorMessage = "The part number can not be longer than 200 characters")]
		public string Description { get; set; }

		public List<string> Parts { get; set; }
		
	}// End Part class

}// End DataObjects namespace
