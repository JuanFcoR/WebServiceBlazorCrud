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
    public class CervezaController1 : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            Respuesta1 oRespuesta = new Respuesta1();
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
            Cerveza cerveza = new Cerveza();
            Respuesta1 oRespuesta = new Respuesta1();
            try
            {

                using (BlazorCrudContext db = new BlazorCrudContext())
                {
                    cerveza = oRespuesta.Data.Where(c => c.Id == Id).FirstOrDefault();

                    oRespuesta.Exito = 1;

                }
            }
            catch (Exception ex)
            {

                oRespuesta.Mensaje = ex.Message;
            }


            return Ok(cerveza);
        }

        [HttpPost]
        public IActionResult Add(CervezaRequest model)
        {
            Respuesta1 oRespuesta = new Respuesta1();
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
            Respuesta1 oRespuesta = new Respuesta1();
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
            Respuesta1 oRespuesta = new Respuesta1();
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
