using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Linq;
using WeBMetas.Models;
using WeBMetas.Models.Contexto;

namespace WeBMetas.Controllers
{
    public class CadastroMetasController : Controller
    {
        private readonly Contexto _contexto;

        public CadastroMetasController(Contexto contexto)
        {
            _contexto = contexto;
        }

        public IActionResult Index()
        {
            var lista = _contexto.CadastroMetas.ToList();
            ViewBag.Metas = lista;
            return View(lista);
        }

        [HttpGet]
        public IActionResult Detalhe(int Id)
        {
            var meta = _contexto.CadastroMetas.Find(Id);

            var vp = Convert.ToDecimal(meta.Meta);
            var vt = Convert.ToDecimal(meta.QtdGuardarPorMes);

            var meses = (vp / vt).ToString("N1");

            var teste = meses.Split(',');
            if (Convert.ToDecimal(teste[1]) >= 3)
            {
                Convert.ToString(teste[1]).Split(',', ' ');
                var res = Convert.ToDecimal(teste[0]) + 1;
                ViewBag.QtdMeses = res;
            }
            else
            {
                ViewBag.QtdMeses = teste[0];
            }
            return View(meta);
        }

        [HttpGet]
        public IActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registrar(CadastroMetasModel metas, IFormCollection form)
        {
            //CultureInfo cult = new CultureInfo("pt-BR");
            //string dta = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss", cult);

            //gastos.Data_Gasto = dta;

            ViewBag.ImgLink = metas.LinkImagemMeta;
            TempData["NomeMeta"] = metas.NomeMeta;

            if (ModelState.IsValid)
            {

                _contexto.Add(metas);
                _contexto.SaveChanges();
            }

            return RedirectToAction("Registrar");
        }

        [HttpGet]
        public IActionResult Editar(int Id)
        {
            var produto = _contexto.CadastroMetas.Find(Id);
            return View();
        }

        [HttpPost]
        public IActionResult Editar(CadastroMetasModel metas)
        {
            if (ModelState.IsValid)
            {
                _contexto.CadastroMetas.Update(metas);
                _contexto.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(metas);
        }

        [HttpGet]
        public IActionResult Remover(int Id)
        {
            var meta = _contexto.CadastroMetas.Find(Id);
            return View(meta);
        }

        [HttpPost]
        public IActionResult Remover(CadastroMetasModel metas)
        {
            var meta = _contexto.CadastroMetas.Find(metas.Id);
            if (meta != null)
            {
                _contexto.Remove(meta);
                _contexto.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(meta);
        }
    }
}
