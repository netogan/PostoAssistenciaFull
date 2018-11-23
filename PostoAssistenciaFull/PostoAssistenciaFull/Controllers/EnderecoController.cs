using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using PostoAssistenciaFull.Integracoes.Postmon.Models;
using PostoAssistenciaFull.Models;

namespace PostoAssistenciaFull.Controllers
{
    [Authorize]
    public class EnderecoController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Endereco
        public ActionResult Index()
        {
            return View(db.Enderecos.ToList());
        }

        // GET: Endereco/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Endereco endereco = db.Enderecos.Find(id);
            if (endereco == null)
            {
                return HttpNotFound();
            }
            return View(endereco);
        }

        // GET: Endereco/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Endereco/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EnderecoId,Uf,Cidade,Bairro,Logradouro,Complemento,Cep")] Endereco endereco)
        {
            if (ModelState.IsValid)
            {
                endereco.EnderecoId = Guid.NewGuid();
                db.Enderecos.Add(endereco);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(endereco);
        }

        // GET: Endereco/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Endereco endereco = db.Enderecos.Find(id);
            if (endereco == null)
            {
                return HttpNotFound();
            }
            return View(endereco);
        }

        // POST: Endereco/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EnderecoId,Uf,Cidade,Bairro,Logradouro,Complemento,Cep")] Endereco endereco)
        {
            if (ModelState.IsValid)
            {
                db.Entry(endereco).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(endereco);
        }

        // GET: Endereco/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Endereco endereco = db.Enderecos.Find(id);
            if (endereco == null)
            {
                return HttpNotFound();
            }
            return View(endereco);
        }

        // POST: Endereco/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Endereco endereco = db.Enderecos.Find(id);
            db.Enderecos.Remove(endereco);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public JsonResult ObterPorNome(string logradouro)
        {
            var listaEnderecos = db.Enderecos.ToList();

            var filtro = listaEnderecos.Where(x =>
                            Utils.CaracterEspecial.RemoverAcentos(x.Logradouro).ToLower().Contains(Utils.CaracterEspecial.RemoverAcentos(logradouro)));

            if (filtro == null) return null;

            var serializer = new JavaScriptSerializer();

            return Json(serializer.Serialize(filtro), JsonRequestBehavior.AllowGet);
        }

        //[HttpGet]
        //public JsonResult ObterTodos()
        //{
        //    var listaEnderecos = db.Enderecos.ToList();

        //    if (listaEnderecos == null) return null;

        //    var serializer = new JavaScriptSerializer();

        //    return Json(serializer.Serialize(listaEnderecos), JsonRequestBehavior.AllowGet);
        //}

        [HttpGet]
        public JsonResult ObterPorLogradouro(string logradouro)
        {
            var listaEnderecos = db.Enderecos.ToList();

            var serializer = new JavaScriptSerializer();

            if (logradouro == null) return Json(serializer.Serialize(listaEnderecos), JsonRequestBehavior.AllowGet);

            var filtro = listaEnderecos.Where(x =>
                            Utils.CaracterEspecial.RemoverAcentos(x.Logradouro).ToLower().Contains(Utils.CaracterEspecial.RemoverAcentos(logradouro)));

            if (filtro == null) return null;

            return Json(serializer.Serialize(filtro), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public IEnumerable<Endereco> ObterTodos()
        {
            var enderecos = db.Enderecos.ToList();

            var assistidos = db.Assistidos.ToList();

            var enderecosLivres = enderecos.Where(e => !assistidos.Any(a => e.EnderecoId == a.EnderecoId));

            return enderecosLivres;
        }

        [HttpGet]
        public JsonResult ConsultarCep(string cep)
        {
            var postmon = new Integracoes.Postmon.Query();

            var response = postmon.ConsultarCep(cep);

            if (!response.EhValido) return Json(new EmptyResult(), JsonRequestBehavior.AllowGet);

            var serializer = new JavaScriptSerializer();

            return Json(serializer.Serialize(response), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ObterPorId(Guid id)
        {
            if (id == null) return Json(new EmptyResult(), JsonRequestBehavior.AllowGet);

            var endereco = db.Enderecos.Find(id);

            if (endereco == null) return Json(new EmptyResult(), JsonRequestBehavior.AllowGet);

            var serializer = new JavaScriptSerializer();

            return Json(serializer.Serialize(endereco), JsonRequestBehavior.AllowGet);
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
