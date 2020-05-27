using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LogicLayer;
using LogicLayerInterfaces;
using DataObjects;

namespace MVCPresentationLayer.Controllers
{
    public class InventoryController : Controller
    {
        private IPartManager _partManager = null;

        public InventoryController()
        {
            _partManager = new PartManager();
        }
        // GET: Inventory
        public ActionResult Index()
        {
            ViewBag.Title = "Current Parts Inventory";
            var parts = _partManager.RetrieveAllPartInformation();
            return View(parts);
        }

        // GET: Inventory/Details/5
        public ActionResult Details(string partNumber)
        {
            ViewBag.Title = "Part Information";
            var part = _partManager.RetrievePartInformationByPartNumber(partNumber);

            return View(part);
        }

        
        // GET: Inventory/Create
        [Authorize(Roles = "Buyer")]
        public ActionResult Create()
        {
            ViewBag.Title = "Add New Part to System";
            return View();
        }

        // POST: Inventory/Create
        [HttpPost]
        [Authorize(Roles = "Buyer")]
        public ActionResult Create(Part newPart)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _partManager.AddNewPart(newPart);
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    return View();
                }
            }
            return View();
        }

        // GET: Inventory/Edit/5
        [Authorize(Roles = "Manger, Buyer")]
        public ActionResult Edit(string partNumber)
        {
            PartVM part = _partManager.RetrievePartInformationByPartNumber(partNumber);
            ViewBag.Title = "Edit Part Information";

            return View(part);
        }

        // POST: Inventory/Edit/5
        [HttpPost]
        [Authorize(Roles = "Manger, Buyer")]
        public ActionResult Edit(string partNumber, Part part)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    part.PartNumber = partNumber;
                    _partManager.EditPartInformation(part);
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    return View();
                }
            }
            return View();
        }

        // GET: Inventory/Delete/5
        [Authorize(Roles = "Buyer")]
        public ActionResult Delete(string partNumber)
        {
            if (partNumber == null)
            {
                return RedirectToAction("Index");
            }
            PartVM part = _partManager.RetrievePartInformationByPartNumber(partNumber);

            return View(part);
        }

        // POST: Inventory/Delete/5
        [HttpPost]
        [Authorize(Roles = "Buyer")]
        public ActionResult Delete(string partNumber, FormCollection collection)
        {
            try
            {
                if (_partManager.DeletePartByPartNumber(partNumber))
                {
                    return RedirectToAction("Index");
                }
                return View();
            }
            catch
            {
                return View();
            }
        }
    }
}
