using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LogicLayer;
using LogicLayerInterfaces;
using DataObjects;
using MVCPresentationLayer.Models;
using Microsoft.AspNet.Identity.Owin;

namespace MVCPresentationLayer.Controllers
{
    public class RecievingController : Controller
    {
        private IPartManager _partManager = null;

        public RecievingController()
        {
            _partManager = new PartManager();
        }

        public RecievingController(IPartManager partManager)
        {
            _partManager = partManager;
        }
        // GET: Inventory
        public ActionResult Index()
        {
            ViewBag.Title = "Current Parts Inventory";
            var parts = from p in _partManager.RetrieveAllParts()
                        select p;
            PartListViewModel partListView = new PartListViewModel
            {
                Part = parts.ToList()
            };

            return View(partListView);
        }

        // GET: Recieving/Recieve
        [Authorize(Roles = "Recieving Clerk")]
        public ActionResult Recieve()
        {
            ViewBag.Title = "Recieve Parts";
            return View();
        }

        // POST: Recieving/Recieve
        [HttpPost]
        [Authorize(Roles = "Recieving Clerk")]
        public ActionResult Recieve(Part part)
        {
            // Could not get this to work with the Model State, I think it is because a couple of my values were null. PartName and Description
            // are already in the database and I could not figure out how to populate them. 
            //if (ModelState.IsValid)
            //{
                try
                {
                    _partManager.AddPart(part);
                    TempData["successMessage"] = string.Format("Part Added to System.");
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    TempData["errorMessage"] = string.Format("Part Not Added to System.");
                    return View();
                }
            //}
            //TempData["errorMessage"] = string.Format("Model State NOT Valid.");
            //return View();
            //return View();
        }

    }
}
