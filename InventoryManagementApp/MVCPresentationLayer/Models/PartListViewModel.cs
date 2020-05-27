using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataObjects;
using System.Web.Mvc;
using System.Text;

namespace MVCPresentationLayer.Models
{
	public class PartListViewModel
	{
		public List<Part> Part { get; set; }
	}
}