using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using PostoAssistenciaFull.Models;

namespace PostoAssistenciaFull.Controllers
{
    [Authorize]
    public class TrabalhadorController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Trabalhador
        public ActionResult Index()
        {
            return View(db.Trabalhadores.ToList());
        }

        // GET: Trabalhador/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trabalhador trabalhador = db.Trabalhadores.Find(id);
            if (trabalhador == null)
            {
                return HttpNotFound();
            }
            return View(trabalhador);
        }

        // GET: Trabalhador/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Trabalhador/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TrabalhadorId,NomeCompleto,Sexo,Idade,DataNascimento,Telefone1,Telefone2,DataAtualizacao")] Trabalhador trabalhador)
        {
            if (ModelState.IsValid)
            {
                trabalhador.TrabalhadorId = Guid.NewGuid();
                db.Trabalhadores.Add(trabalhador);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(trabalhador);
        }

        // GET: Trabalhador/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trabalhador trabalhador = db.Trabalhadores.Find(id);
            if (trabalhador == null)
            {
                return HttpNotFound();
            }
            return View(trabalhador);
        }

        // POST: Trabalhador/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TrabalhadorId,NomeCompleto,Sexo,Idade,DataNascimento,Telefone1,Telefone2,DataAtualizacao")] Trabalhador trabalhador)
        {
            if (ModelState.IsValid)
            {
                db.Entry(trabalhador).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(trabalhador);
        }

        // GET: Trabalhador/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trabalhador trabalhador = db.Trabalhadores.Find(id);
            if (trabalhador == null)
            {
                return HttpNotFound();
            }
            return View(trabalhador);
        }

        // POST: Trabalhador/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Trabalhador trabalhador = db.Trabalhadores.Find(id);
            db.Trabalhadores.Remove(trabalhador);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IEnumerable<Trabalhador> ObterTodos()
        {
            return db.Trabalhadores.ToList();
        }

        [HttpGet]
        public JsonResult ObterTodosSemVinculoComChamadaJSON(Guid chamadaId)
        {
            var todosTrabalhadores = db.Trabalhadores.ToList();

            var trabalhadoresVinculados = db.ChamadaTrabalhadores.ToList().Where(c => c.ChamadaId == chamadaId);

            var trabalhadoresSemVinculo = todosTrabalhadores.Where(tudo => !trabalhadoresVinculados.Any(vinc => vinc.TrabalhadorId == tudo.TrabalhadorId));

            var serializer = new JavaScriptSerializer();

            if (trabalhadoresSemVinculo == null) return null;

            return Json(serializer.Serialize(trabalhadoresSemVinculo), JsonRequestBehavior.AllowGet);
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
