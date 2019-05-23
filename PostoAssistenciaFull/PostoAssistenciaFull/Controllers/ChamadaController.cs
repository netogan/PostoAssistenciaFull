using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PostoAssistenciaFull.Models;

namespace PostoAssistenciaFull.Controllers
{
    [Authorize]
    public class ChamadaController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Chamada
        public ActionResult Index()
        {
            return View(db.Chamadas.ToList());
        }

        // GET: Chamada/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chamada chamada = db.Chamadas.Find(id);
            if (chamada == null)
            {
                return HttpNotFound();
            }
            return View(chamada);
        }

        // GET: Chamada/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Chamada/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ChamadaId,DataChamada,DataCriacao,Observação")] Chamada chamada)
        {
            if (ModelState.IsValid)
            {
                chamada.ChamadaId = Guid.NewGuid();
                chamada.DataCriacao = DateTime.Now;
                db.Chamadas.Add(chamada);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(chamada);
        }

        // GET: Chamada/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chamada chamada = db.Chamadas.Find(id);
            if (chamada == null)
            {
                return HttpNotFound();
            }
            return View(chamada);
        }

        // POST: Chamada/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ChamadaId,DataChamada,DataCriacao,Observação")] Chamada chamada)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chamada).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(chamada);
        }

        // GET: Chamada/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chamada chamada = db.Chamadas.Find(id);
            if (chamada == null)
            {
                return HttpNotFound();
            }
            return View(chamada);
        }

        // POST: Chamada/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Chamada chamada = db.Chamadas.Find(id);
            db.Chamadas.Remove(chamada);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost, ActionName("AssociarTrabalhador")]
        [ValidateAntiForgeryToken]
        public ActionResult AssociarTrabalhador(Guid chamadaId)
        {
            var chamadaTrabalhador = new ChamadaTrabalhador()
            {
                ChamadaTrabalhadorId = Guid.NewGuid()

            };

            db.ChamadaTrabalhadores.Add(chamadaTrabalhador);
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
