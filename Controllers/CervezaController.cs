using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
            Respuesta oRespuesta = new Respuesta();
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
            BlazorCrudContext db = new BlazorCrudContext();
            Cerveza cerveza = db.Cervezas.Find(Id);
            Respuesta oRespuesta = new Respuesta();
            try
            {

                
                
                cerveza = db.Cervezas.Find(Id);

                oRespuesta.Exito = 1;

                
            }
            catch (Exception ex)
            {

                oRespuesta.Mensaje = ex.Message;
            }
            finally
            {
                db.Dispose();
            }


            return Ok(cerveza);
        }

        public static List<Cerveza> BusquedaAvanzada(int param, string criterio,Respuesta resp)
        {
            BlazorCrudContext db = new BlazorCrudContext();
            
            List<Cerveza> oCerveza = new List<Cerveza>();

            try
            {
                switch (param)
                {
                    case 0:
                        oCerveza = db.Cervezas.ToList();
                        resp.Exito = 1;
                        break;
                    case 1:
                        oCerveza = db.Cervezas.Where(a => a.Id == Convert.ToInt64(criterio)).ToList();
                        resp.Exito = 1;
                        break;
                    case 2:
                        oCerveza = db.Cervezas.Where(a => a.Nombre.Contains(criterio)).ToList();
                        resp.Exito = 1;
                        break;
                    case 3:
                        oCerveza = db.Cervezas.Where(a => a.Marca.Contains(criterio)).ToList();
                        resp.Exito = 1;
                        break;

                    default:
                        oCerveza = new List<Cerveza>();
                        resp.Exito = 0;
                        resp.Mensaje = "Este parametro no existe";
                        break;
                }



            }
            catch (Exception ex)
            {
                resp.Mensaje = ex.Message;
              
            }
            finally
            {
                db.Dispose();
            }


            return oCerveza;
        }

        public static KeyValuePair<Respuesta, List<Cerveza>> GetValores(int param, string criterio)
        {
            BlazorCrudContext db = new BlazorCrudContext();
            Respuesta resp = new Respuesta();
            

            List<Cerveza> oCerveza = new List<Cerveza>();

            try
            {
                switch (param)
                {
                    case 0:
                        oCerveza = db.Cervezas.ToList();
                        resp.Exito = 1;
                        break;
                    case 1:
                        oCerveza = db.Cervezas.Where(a => a.Id == Convert.ToInt64(criterio)).ToList();
                        resp.Exito = 1;
                        break;
                    case 2:
                        oCerveza = db.Cervezas.Where(a => a.Nombre.Contains(criterio)).ToList();
                        resp.Exito = 1;
                        break;
                    case 3:
                        oCerveza = db.Cervezas.Where(a => a.Marca.Contains(criterio)).ToList();
                        resp.Exito = 1;
                        break;

                    default:
                        oCerveza = new List<Cerveza>();
                        resp.Exito = 0;
                        resp.Mensaje = "Este parametro no existe";
                        break;
                }



            }
            catch (Exception ex)
            {
                resp.Mensaje = ex.Message;

            }
            finally
            {
                db.Dispose();
            }


            return new KeyValuePair<Respuesta, List<Cerveza>>(resp, oCerveza);
        }

        [HttpGet("{param}/{criterio}")]
        public IActionResult GetAvanzado(int param, string criterio)
        {
            var valores = GetValores(param, criterio);
            List<Cerveza> oCerveza = valores.Value;
            Respuesta respuesta = valores.Key;

            

            return Ok(oCerveza);
        }

        [HttpPost]
        public IActionResult Add(CervezaRequest model)
        {
            Respuesta oRespuesta = new Respuesta();
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

                 oRespuesta.Mensaje = ex.Message;
                //throw;
            }


            return Ok(oRespuesta);
        }

        [HttpPut]
        public IActionResult Edit(CervezaRequest model)
        {
            Respuesta oRespuesta = new Respuesta();
            try
            {

                using (BlazorCrudContext db = new BlazorCrudContext())
                {
                    Cerveza oCerveza = db.Cervezas.Find(model.Id);
                    if(oCerveza!=null)
                    {
                        oCerveza.Nombre = model.Nombre;
                        oCerveza.Marca = model.Marca;
                        db.Entry(oCerveza).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        db.SaveChanges();
                        oRespuesta.Exito = 1;
                    }
                    else
                    {
                        oRespuesta.Mensaje = "La cerveza que desea eliminar no existe";
                        oRespuesta.Exito = 0;
                    }
                    
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
            Respuesta oRespuesta = new Respuesta();
            try
            {

                using (BlazorCrudContext db = new BlazorCrudContext())
                {
                    Cerveza oCerveza = db.Cervezas.Find(Id);
                    if(oCerveza!=null)
                    {
                        db.Remove(oCerveza);
                        db.SaveChanges();
                        oRespuesta.Exito = 1;
                    }
                    else
                    {
                        oRespuesta.Mensaje = "El elemento que busca no pudo ser encontrado";
                        oRespuesta.Exito = 0;
                    }
                    
                }
            }

            catch (Exception ex)
            {

                oRespuesta.Mensaje = ex.Message;
            }


            return Ok(oRespuesta);
        }
    }
}
