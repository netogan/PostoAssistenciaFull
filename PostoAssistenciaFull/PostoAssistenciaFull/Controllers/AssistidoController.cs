using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using PostoAssistenciaFull.Models;

namespace PostoAssistenciaFull.Controllers
{
    [Authorize]
    public class AssistidoController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Assistido
        public ActionResult Index()
        {
            return View(db.Assistidos.ToList().OrderBy(x => x.NomeCompleto));
        }

        // GET: Assistido/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var assistido = db.Assistidos.Find(id);

            if (assistido == null)
                return HttpNotFound();

            var urlFoto = $"{HttpRuntime.AppDomainAppPath}\\fotos\\{assistido.AssistidoId}.jpg";

            if (System.IO.File.Exists(urlFoto))
            {
                using (var image = Image.FromFile(urlFoto))
                {
                    using (MemoryStream m = new MemoryStream())
                    {
                        image.Save(m, image.RawFormat);
                        byte[] imageBytes = m.ToArray();

                        assistido.FotoBase64 = Convert.ToBase64String(imageBytes);
                    }
                }
            }

            return View(assistido);
        }

        // GET: Assistido/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Assistido/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AssistidoId,NomeCompleto,Sexo,Idade,DataNascimento,GrauParentesco,Telefone1,Telefone2,NumeracaoCalcado,NumeracaoRoupaSuperior,NumeracaoRoupaInferior,Observacao,Principal,DependenteId,EnderecoId")] Assistido assistido)
        {
            if (ModelState.IsValid)
            {
                assistido.AssistidoId = Guid.NewGuid();
                assistido.DataAtualizacao = DateTime.Now;
                db.Assistidos.Add(assistido);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(assistido);
        }

        // GET: Assistido/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var assistido = db.Assistidos.Find(id);

            if (assistido == null)
                return HttpNotFound();

            var urlFoto = $"{HttpRuntime.AppDomainAppPath}\\fotos\\{assistido.AssistidoId}.jpg";

            if (System.IO.File.Exists(urlFoto))
            {
                using (var image = Image.FromFile(urlFoto))
                {
                    using (MemoryStream m = new MemoryStream())
                    {
                        image.Save(m, image.RawFormat);
                        byte[] imageBytes = m.ToArray();

                        assistido.FotoBase64 = Convert.ToBase64String(imageBytes);
                    }
                }
            }

            return View(assistido);
        }

        // POST: Assistido/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AssistidoId,NomeCompleto,Sexo,Idade,DataNascimento,GrauParentesco,Telefone1,Telefone2,NumeracaoCalcado,NumeracaoRoupaSuperior,NumeracaoRoupaInferior,Observacao,Principal,DependenteId,EnderecoId")] Assistido assistido)
        {
            if (ModelState.IsValid)
            {
                db.Entry(assistido).State = EntityState.Modified;
                assistido.DataAtualizacao = DateTime.Now;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(assistido);
        }

        // GET: Assistido/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assistido assistido = db.Assistidos.Find(id);
            if (assistido == null)
            {
                return HttpNotFound();
            }
            return View(assistido);
        }

        // POST: Assistido/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Assistido assistido = db.Assistidos.Find(id);
            db.Assistidos.Remove(assistido);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IEnumerable<Assistido> ObterTodos()
        {
            return db.Assistidos.ToList();
        }

        [HttpGet]
        public JsonResult ObterPorNome(string nome)
        {
            var listaAssistidos = db.Assistidos.ToList();

            var serializer = new JavaScriptSerializer();

            if (nome == null) return Json(serializer.Serialize(listaAssistidos), JsonRequestBehavior.AllowGet);

            var filtro = listaAssistidos.Where(x => 
                            Utils.CaracterEspecial.RemoverAcentos(x.NomeCompleto).ToLower().Contains(Utils.CaracterEspecial.RemoverAcentos(nome)));

            if (filtro == null) return null;

            return Json(serializer.Serialize(filtro), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ObterPorId(Guid id)
        {
            if(id == null) return Json(new EmptyResult(), JsonRequestBehavior.AllowGet);

            var assistido = db.Assistidos.Find(id);

            if (assistido == null) return Json(new EmptyResult(), JsonRequestBehavior.AllowGet);

            var serializer = new JavaScriptSerializer();

            return Json(serializer.Serialize(assistido), JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpPost]
        [Route("IncluirFoto")]
        public string IncluirFoto(Guid assistidoId)
        {
            string base64String = string.Empty;

            if (HttpContext.Request.Files.AllKeys.Any())
            {
                var foto = HttpContext.Request.Files["foto"];

                var diretorioFoto = $"{HttpRuntime.AppDomainAppPath}\\fotos\\{assistidoId}.jpg";

                ImageResizer.ImageJob i = new ImageResizer.ImageJob(foto, diretorioFoto, new ImageResizer.ResizeSettings("width=300;height=300;format=jpg;mode=max"));

                i.CreateParentDirectory = true; 
                i.Build();

                using (var image = Image.FromFile(diretorioFoto))
                {
                    using (MemoryStream m = new MemoryStream())
                    {
                        image.Save(m, image.RawFormat);
                        byte[] imageBytes = m.ToArray();

                        base64String = Convert.ToBase64String(imageBytes);
                    }
                }
            }

            return base64String;
        }
    }
}
