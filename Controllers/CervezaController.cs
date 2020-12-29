using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebServiceBlazorCrud.Models;
using WebServiceBlazorCrud.Models.Request;
using WebServiceBlazorCrud.Models.Response;

namespace WebServiceBlazorCrud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CervezaController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            Respuesta<List<Cerveza>> oRespuesta = new Respuesta<List<Cerveza>>();
            try
            {
                
                using (BlazorCrudContext db = new BlazorCrudContext())
                {
                    var lst = db.Cervezas.ToList();
                    oRespuesta.Exito = 1;
                    oRespuesta.Data = lst;
                }
            }
            catch (Exception ex)
            {

                oRespuesta.Mensaje = ex.Message;
            }
            
            
            return Ok(oRespuesta);
        }
        [HttpGet("{Id}")]
        public IActionResult Get(int Id)
        {
            Respuesta<Cerveza> oRespuesta = new Respuesta<Cerveza>();
            try
            {

                using (BlazorCrudContext db = new BlazorCrudContext())
                {
                    var lst = db.Cervezas.Find(Id);
                    oRespuesta.Exito = 1;
                    oRespuesta.Data = lst;
                }
            }
            catch (Exception ex)
            {

                oRespuesta.Mensaje = ex.Message;
            }


            return Ok(oRespuesta);
        }

        [HttpPost]
        public IActionResult Add(CervezaRequest model)
        {
            Respuesta<List<Cerveza>> oRespuesta = new Respuesta<List<Cerveza>>();
            try
            {

                using (BlazorCrudContext db = new BlazorCrudContext())
                {
                    Cerveza oCerveza = new Cerveza();
                    oCerveza.Nombre = model.Nombre;
                    oCerveza.Marca = model.Marca;
                    db.Cervezas.Add(oCerveza);
                    db.SaveChanges();
                    oRespuesta.Exito = 1;
                }
            }
            catch (Exception ex)
            {

                // oRespuesta.Mensaje = ex.Message;
                throw;
            }


            return Ok(oRespuesta);
        }

        [HttpPut]
        public IActionResult Edit(CervezaRequest model)
        {
            Respuesta<List<Cerveza>> oRespuesta = new Respuesta<List<Cerveza>>();
            try
            {

                using (BlazorCrudContext db = new BlazorCrudContext())
                {
                    Cerveza oCerveza = db.Cervezas.Find(model.Id);
                    oCerveza.Nombre = model.Nombre;
                    oCerveza.Marca = model.Marca;
                    db.Entry(oCerveza).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    db.SaveChanges();
                    oRespuesta.Exito = 1;
                }
            }
            catch (Exception ex)
            {

                oRespuesta.Mensaje = ex.Message;
            }


            return Ok(oRespuesta);
        }

        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id)
        {
            Respuesta<List<Cerveza>> oRespuesta = new Respuesta<List<Cerveza>>();
            try
            {

                using (BlazorCrudContext db = new BlazorCrudContext())
                {
                    Cerveza oCerveza = db.Cervezas.Find(Id);
                    db.Remove(oCerveza);
                    db.SaveChanges();
                    oRespuesta.Exito = 1;
                }
            }
            catch (Exception ex)
            {

                throw;
            }


            return Ok(oRespuesta);
        }
    }
}
