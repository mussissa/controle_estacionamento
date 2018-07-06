using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using estacionamento.Models.model1;

namespace estacionamento.Controllers
{
    public class vigencia_valorController : Controller
    {
        private Model1 db = new Model1();

        // GET: vigencia_valor
        public ActionResult Index()
        {
            return View(db.vigencia_valor.ToList());
        }

        // GET: vigencia_valor/Details/5
        public ActionResult Details(string dt_inicio, string dt_fim)
        {
            if (dt_inicio == null && dt_fim == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            vigencia_valor vigencia_valor = db.vigencia_valor.Find( dt_inicio ,dt_fim);
            if (vigencia_valor == null)
            {
                return HttpNotFound();
            }
            return View(vigencia_valor);
        }

        // GET: vigencia_valor/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: vigencia_valor/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "dt_inicio,dt_fim,preco,preco_adcional")] vigencia_valor vigencia_valor)
        {
            //so é sera adicionado se a data final for maior ou igual a data inicial
            DateTime dati = Convert.ToDateTime(vigencia_valor.dt_inicio);

            int data_inteira = Convert.ToInt32(dati.ToString("yyyy/MM/dd").Replace("/", ""));

            vigencia_valor.dt_inicio_int = data_inteira;

            dati = Convert.ToDateTime(vigencia_valor.dt_fim);

            data_inteira = Convert.ToInt32(dati.ToString("yyyy/MM/dd").Replace("/", ""));

            vigencia_valor.dt_fim_int = data_inteira;

            if(vigencia_valor.dt_fim_int >= vigencia_valor.dt_inicio_int)
            {
                if (ModelState.IsValid)
                {
                    db.vigencia_valor.Add(vigencia_valor);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

            }
            
            return View(vigencia_valor);
        }

        // GET: vigencia_valor/Edit/5
        public ActionResult Edit(string dt_inicio, string dt_fim)
        {
            if (dt_inicio == null && dt_fim == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            vigencia_valor vigencia_valor = db.vigencia_valor.Find(dt_inicio ,dt_fim);
            if (vigencia_valor == null)
            {
                return HttpNotFound();
            }
            return View(vigencia_valor);
        }

        // POST: vigencia_valor/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "dt_inicio,dt_fim,preco,preco_adcional")] vigencia_valor vigencia_valor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vigencia_valor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vigencia_valor);
        }

        // GET: vigencia_valor/Delete/5
        public ActionResult Delete(string dt_inicio, string dt_fim)
        {
            if (dt_inicio == null && dt_fim == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            vigencia_valor vigencia_valor = db.vigencia_valor.Find(dt_inicio, dt_fim);
            if (vigencia_valor == null)
            {
                return HttpNotFound();
            }
            return View(vigencia_valor);
        }

        // POST: vigencia_valor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string dt_inicio, string dt_fim)
        {
            vigencia_valor vigencia_valor = db.vigencia_valor.Find(dt_inicio, dt_fim);
            db.vigencia_valor.Remove(vigencia_valor);
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
