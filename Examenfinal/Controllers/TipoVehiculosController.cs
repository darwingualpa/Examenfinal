using Datos.Model;
using Datos.ModelosNuevos;
using Datos.Viewmodel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Examenfinal.Controllers
{
    public class TipoVehiculosController : Controller
    {
        private readonly EjercicioEvaluacionContext _context;
        public TipoVehiculosController(EjercicioEvaluacionContext context)
        {
            _context = context;


        }
        public void Combox()
        {

            ViewData["CodigoVehiculo"] = new SelectList(_context.Vehiculos.Select(D => new ViewModeltiposdevehiculo
            {
                Codigo = D.Codigo,
                Nombres = $"{D.Nombre}",
                Estado=D.Estado,

            }).Where(F => F.Estado==1).ToList(), "Codigo", "Nombres");

        }


        // GET: TipoVehiculosController
        public ActionResult Index()
        {


            List<ViewModeltiposdevehiculo> ltstipovehiculos = _context.TipoVehiculos.Select(D => new ViewModeltiposdevehiculo
            {
                Codigo = D.Codigo,
                Descripcion =D.Descripcion,
                Nombres = $"{D.Nombre}",
                
                Estado = D.Estado,

            }).Where(F => F.Estado == 1).ToList();


            //List<TipoVehiculo> ltstipovehiculos = _context.TipoVehiculos.ToList();
            return View(ltstipovehiculos);
        }

        // GET: TipoVehiculosController/Details/5
        public ActionResult Details(int id)
        {
            TipoVehiculo tipovehiculo = _context.TipoVehiculos.Where(D => D.CodigoVehiculo == id).FirstOrDefault();

            return View(tipovehiculo);
        }

        // GET: VehiculosController/Create
        public ActionResult Create()
        {
            Combox();
            return View();
        }

        // POST: VehiculosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TipoVehiculo tipovehiculo)
        {
            try
            {
                tipovehiculo.Estado = 1;
                _context.Add(tipovehiculo);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                Combox();
                return View(tipovehiculo);
            }
        }

        // GET: VehiculosController/Edit/5
        public ActionResult Edit(int id)
        {
            Combox();
            TipoVehiculo tipovehiculo = _context.TipoVehiculos.Where(D => D.CodigoVehiculo == id).FirstOrDefault();
            return View();
        }

        // POST: VehiculosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, TipoVehiculo tipovehiculo)
        {
            if (id != tipovehiculo.CodigoVehiculo)
            {
                return RedirectToAction("Index");
            }
            try
            {
                _context.Update(tipovehiculo);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                Combox();
                return View(tipovehiculo);
            }
        }



        // POST: VehiculosController/Delete/5

        public ActionResult Activar(int id)
        {
            Vehiculo vehiculo = _context.Vehiculos.Where(D => D.Codigo == id).FirstOrDefault();
            vehiculo.Estado = 1;
            _context.Update(vehiculo);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Desactivar(int id)
        {
            TipoVehiculo tipovehiculo = _context.TipoVehiculos.Where(D => D.CodigoVehiculo == id).FirstOrDefault();
            tipovehiculo.Estado = 0;
            _context.Update(tipovehiculo);
            _context.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}
