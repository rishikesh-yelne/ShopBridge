using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShopBridge.Models;
using System.IO;

namespace ShopBridge.Controllers
{
    public class ItemController : Controller
    {
        private ShopBridgeDBContext db = new ShopBridgeDBContext();

        // GET: Item
        public ActionResult Index()
        {
            return View(db.itemTbls.ToList());
        }

        // GET: Item/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            itemTbl itemTbl = db.itemTbls.Find(id);
            if (itemTbl == null)
            {
                return HttpNotFound();
            }
            return View(itemTbl);
        }

        public PartialViewResult CreatePartial()
        {
            return PartialView("_CreateItem");
        }

        public PartialViewResult ViewItems()
        {
            return PartialView("_ViewItems", db.itemTbls.ToList());
        }

        // POST: Item/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "itemID,itemName,itemDescription,itemPrice")] itemTbl itemTbl)
        {
            if (ModelState.IsValid)
            {
                if (Request.Files.Count > 0)
                {
                    try
                    {
                        using (var binaryReader = new BinaryReader(Request.Files["ItemImage"].InputStream))
                        {
                            itemTbl.itemImage = binaryReader.ReadBytes(Request.Files["ItemImage"].ContentLength);
                        }
                        db.itemTbls.Add(itemTbl);
                        db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("ItemImage", "Item not saved, please try again");
                        Console.WriteLine(ex.Message);
                    }
                }
                else
                {
                    ModelState.AddModelError("ItemImage", "Image not selected");
                    //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
            }
            return PartialView("_CreateItem", itemTbl);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteItem(int id)
        {
            try
            {
                itemTbl itemTbl = db.itemTbls.Find(id);
                db.itemTbls.Remove(itemTbl);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return PartialView("_ViewItems", db.itemTbls.ToList());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
