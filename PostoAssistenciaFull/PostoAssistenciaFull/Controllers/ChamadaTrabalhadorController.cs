using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using PostoAssistenciaFull.Models;
using PostoAssistenciaFull.ViewModel.ChamadaTrabalhador;

namespace PostoAssistenciaFull.Controllers
{
    [Authorize]
    public class ChamadaTrabalhadorController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ChamadaTrabalhador
        public ActionResult Index()
        {
            var chamadaTrabalhadores = db.ChamadaTrabalhadores.Include(c => c.Chamada).Include(c => c.Trabalhador);
            return View(chamadaTrabalhadores.ToList());
        }

        // GET: ChamadaTrabalhador/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChamadaTrabalhador chamadaTrabalhador = db.ChamadaTrabalhadores.Find(id);
            if (chamadaTrabalhador == null)
            {
                return HttpNotFound();
            }
            return View(chamadaTrabalhador);
        }

        // GET: ChamadaTrabalhador/Create
        public ActionResult Create()
        {
            ViewBag.ChamadaId = new SelectList(db.Chamadas, "ChamadaId", "Observação");
            ViewBag.TrabalhadorId = new SelectList(db.Trabalhadores, "TrabalhadorId", "NomeCompleto");
            return View();
        }

        // POST: ChamadaTrabalhador/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ChamadaTrabalhadorId,ChamadaId,TrabalhadorId,Observação")] ChamadaTrabalhador chamadaTrabalhador)
        {
            if (ModelState.IsValid)
            {
                chamadaTrabalhador.ChamadaTrabalhadorId = Guid.NewGuid();
                db.ChamadaTrabalhadores.Add(chamadaTrabalhador);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ChamadaId = new SelectList(db.Chamadas, "ChamadaId", "Observação", chamadaTrabalhador.ChamadaId);
            ViewBag.TrabalhadorId = new SelectList(db.Trabalhadores, "TrabalhadorId", "NomeCompleto", chamadaTrabalhador.TrabalhadorId);
            return View(chamadaTrabalhador);
        }

        // GET: ChamadaTrabalhador/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChamadaTrabalhador chamadaTrabalhador = db.ChamadaTrabalhadores.Find(id);
            if (chamadaTrabalhador == null)
            {
                return HttpNotFound();
            }
            ViewBag.ChamadaId = new SelectList(db.Chamadas, "ChamadaId", "Observação", chamadaTrabalhador.ChamadaId);
            ViewBag.TrabalhadorId = new SelectList(db.Trabalhadores, "TrabalhadorId", "NomeCompleto", chamadaTrabalhador.TrabalhadorId);
            return View(chamadaTrabalhador);
        }

        // POST: ChamadaTrabalhador/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ChamadaTrabalhadorId,ChamadaId,TrabalhadorId,Observação")] ChamadaTrabalhador chamadaTrabalhador)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chamadaTrabalhador).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ChamadaId = new SelectList(db.Chamadas, "ChamadaId", "Observação", chamadaTrabalhador.ChamadaId);
            ViewBag.TrabalhadorId = new SelectList(db.Trabalhadores, "TrabalhadorId", "NomeCompleto", chamadaTrabalhador.TrabalhadorId);
            return View(chamadaTrabalhador);
        }

        // GET: ChamadaTrabalhador/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChamadaTrabalhador chamadaTrabalhador = db.ChamadaTrabalhadores.Find(id);
            if (chamadaTrabalhador == null)
            {
                return HttpNotFound();
            }
            return View(chamadaTrabalhador);
        }

        // POST: ChamadaTrabalhador/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            ChamadaTrabalhador chamadaTrabalhador = db.ChamadaTrabalhadores.Find(id);
            db.ChamadaTrabalhadores.Remove(chamadaTrabalhador);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost, ActionName("CriarChamadaParaTrabalhadores")]
        public JsonResult CriarChamadaParaTrabalhadores(ViewModelChamadaTrabalhador viewChamadaTrabalhador)
        {
            foreach(var item in viewChamadaTrabalhador.Trabalhadores)
            {
                var novaChamadaTrabalhador = new ChamadaTrabalhador()
                {
                    ChamadaTrabalhadorId = Guid.NewGuid(),
                    ChamadaId = viewChamadaTrabalhador.ChamadaId,
                    TrabalhadorId = item
                };

                db.ChamadaTrabalhadores.Add(novaChamadaTrabalhador);
                db.SaveChanges();
            }

            return new JsonResult();
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
