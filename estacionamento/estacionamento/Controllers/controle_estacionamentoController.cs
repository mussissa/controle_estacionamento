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
    public class controle_estacionamentoController : Controller
    {
        private Model1 db = new Model1();

        // GET: controle_estacionamento
        public ActionResult Index()
        {
            
            return View(db.controle_estacionamento.ToList());
        }

        // GET: controle_estacionamento/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            controle_estacionamento controle_estacionamento = db.controle_estacionamento.Find(id);
            if (controle_estacionamento == null)
            {
                return HttpNotFound();
            }
            return View(controle_estacionamento);
        }

        // GET: controle_estacionamento/Create
        public ActionResult Create()
        {
            var model = new controle_estacionamento();
            model.horario_chegada = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            return View(model);
        }

        // POST: controle_estacionamento/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "placa,horario_chegada,tempo")] controle_estacionamento controle_estacionamento)
        {
            var tb_controle = db.controle_estacionamento.Find(controle_estacionamento.placa);
            // se a do Carro ja tiver cadastrado, apago ele e crio novamente
            if (tb_controle!= null)
            {
                db.controle_estacionamento.Remove(tb_controle);
                db.SaveChanges();

            }

            string data = controle_estacionamento.horario_chegada;
            IFormatProvider mFomatter = new System.Globalization.CultureInfo("pt-Br");
            DateTime dati = DateTime.Parse(data, mFomatter);

            int data_inteira_inv = Convert.ToInt32(dati.ToString("yyyyMMdd"));

            var tarifas = (from controle in db.vigencia_valor
                           where controle.dt_inicio_int <= data_inteira_inv &&
                           controle.dt_fim_int >= data_inteira_inv 
                           select new {controle.preco, controle.preco_adcional}
                           ).First();

            
            controle_estacionamento.preco = tarifas.preco;
           
         
            controle_estacionamento.valor_pagamento = tarifas.preco * controle_estacionamento.tempo;

            if (ModelState.IsValid)
            {
                db.controle_estacionamento.Add(controle_estacionamento);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            return View(controle_estacionamento);
        }

        // GET: controle_estacionamento/Edit/5
        public ActionResult Edit(string id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            controle_estacionamento controle_estacionamento = db.controle_estacionamento.Find(id);

            if (controle_estacionamento == null)
            {
                return HttpNotFound();
            }
            // se o carro ja foi liberado, nao fazer nada
            if (!controle_estacionamento.liberado)
            {
                controle_estacionamento.horario_saida = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");


                IFormatProvider mFomatter = new System.Globalization.CultureInfo("pt-Br");
                DateTime dat1 = DateTime.Parse(controle_estacionamento.horario_chegada, mFomatter);
                DateTime dat2 = DateTime.Parse(controle_estacionamento.horario_saida, mFomatter);

                int entrada_invert = Convert.ToInt32(dat1.ToString("yyyyMMdd"));

                var tarifas = (from controle in db.vigencia_valor
                               where controle.dt_inicio_int <= entrada_invert &&
                                     controle.dt_fim_int >= entrada_invert
                               select new { controle.preco, controle.preco_adcional }

                               ).First();

                TimeSpan calculo = dat2.Subtract(dat1);


                if (calculo.TotalMinutes <= 30)
                {
                    controle_estacionamento.preco = controle_estacionamento.preco / 2;

                }
                controle_estacionamento.valor_pagamento = controle_estacionamento.preco * controle_estacionamento.tempo;

                //calculando tempo em minutos
                if (calculo.TotalMinutes > controle_estacionamento.tempo * 60)
                {
                    var minutos1 = calculo.TotalMinutes;
                    var minutos2 = controle_estacionamento.tempo * 60;
                    var total = minutos1 - minutos2;
                    //dando a tolerancia de 10 min
                    if (total > 10 && total <= 60)
                    {

                        controle_estacionamento.tempo++;
                        controle_estacionamento.valor_pagamento = controle_estacionamento.valor_pagamento + tarifas.preco_adcional;
                    }
                    else
                        if (total > 60)
                    {
                        int tot = Convert.ToInt32(total / 60);

                        controle_estacionamento.tempo = controle_estacionamento.tempo + tot;
                        controle_estacionamento.valor_pagamento = controle_estacionamento.valor_pagamento + (tarifas.preco_adcional * tot);
                    }

                }

                controle_estacionamento.duracao = calculo.ToString();

            }
            


            return View(controle_estacionamento);
        }

        // POST: controle_estacionamento/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "placa,horario_chegada,horario_saida,duracao,tempo,preco,valor_pagamento")] controle_estacionamento controle_estacionamento)
        {
            if (ModelState.IsValid)
            {
                controle_estacionamento.liberado = true;
                db.Entry(controle_estacionamento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(controle_estacionamento);
        }

        // GET: controle_estacionamento/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            controle_estacionamento controle_estacionamento = db.controle_estacionamento.Find(id);
            if (controle_estacionamento == null)
            {
                return HttpNotFound();
            }
            return View(controle_estacionamento);
        }

        // POST: controle_estacionamento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            controle_estacionamento controle_estacionamento = db.controle_estacionamento.Find(id);
            db.controle_estacionamento.Remove(controle_estacionamento);
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
