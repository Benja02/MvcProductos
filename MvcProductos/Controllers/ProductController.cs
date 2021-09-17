using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MvcProductos.Models;

namespace MvcProductos.Controllers
{
    public class ProductController : Controller
    {
        private TallerContext db = new TallerContext();

        // GET: Product
        public ActionResult Index(string descripcion = "", int existencia = 0, double precio = 0)
        {
            var a = db.Productos.Include(p => p.Category);
            if (!string.IsNullOrEmpty(descripcion))
                a = a.Where(p => p.Descripcion.ToUpper().Contains(descripcion.ToUpper()));
            if (existencia > 0)
                a = a.Where(p => p.existencia == existencia);
            if (precio > 0)
                a = a.Where(p => p.precioVenta == precio);

            return View(a.ToList());
            //return View(db.Productos.ToList());
        }

        // GET: Product/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Productos.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            if (db.Categorias.Count() == 0)
                return RedirectToAction("Create", "Categorias");

            ViewBag.IdCategory = new SelectList(db.Categorias, "IdCategory", "Descripcion");
            return View();
            
        }

        // POST: Product/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdProduct,Descripcion,IdCategoria,Costo,precioVenta,existencia,numPedidos")] Product product)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.IdCategory = new SelectList(db.Categorias, "IdCategory", "Descripcion", product.IdCategoria);
                return View(product);
            }
            if (product.existencia < product.numPedidos)
            {
                ModelState.AddModelError("", "No se puede agregar una cantidad de pedidos mayor a la de las existencias");
                ViewBag.IdCategory = new SelectList(db.Categorias, "IdCategory", "Descripcion", product.IdCategoria);
                return View(product);
            }

            product.Descripcion = product.Descripcion.ToUpper();
            db.Productos.Add(product);
            db.SaveChanges();
            return RedirectToAction("Index");


        }

        // GET: Product/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Productos.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdCategory = new SelectList(db.Categorias, "IdCategory", "Descripcion", product.IdCategoria);
            return View(product);
        }

        // POST: Product/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdProduct,Descripcion,IdCategoria,Costo,precioVenta,existencia,numPedidos")] Product product)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.IdCategory = new SelectList(db.Categorias, "IdCategory", "Descripcion", product.IdCategoria);
                return View(product);
            }
            if (product.existencia < product.numPedidos)
            {
                ModelState.AddModelError("", "Error, intenta pedir más de lo que hay en existencias");
                ViewBag.IdCategory = new SelectList(db.Categorias, "IdCategory", "Descripcion", product.IdCategoria);
                return View(product);
            }

            product.Descripcion = product.Descripcion.ToUpper();
            db.Entry(product).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        // GET: Product/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Productos.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Productos.Find(id);
            db.Productos.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
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
