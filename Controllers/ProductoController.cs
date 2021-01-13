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
    public class ProductoController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            Respuesta oRespuesta = new Respuesta();
            try
            {

                using (BlazorCrudContext db = new BlazorCrudContext())
                {
                    var lst = db.Productos.ToList();
                    oRespuesta.Exito = 1;
                    oRespuesta.ProductoData = lst;
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
            Producto producto = db.Productos.Find(Id);
            Respuesta oRespuesta = new Respuesta();
            try
            {
                producto = db.Productos.Find(Id);

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


            return Ok(producto);
        }



        public static KeyValuePair<Respuesta, List<Producto>> GetValores(int param, string criterio)
        {
            BlazorCrudContext db = new BlazorCrudContext();
            Respuesta resp = new Respuesta();


            List<Producto> oProducto = new List<Producto>();

            try
            {
                switch (param)
                {
                    case 0:
                        oProducto = db.Productos.ToList();
                        resp.Exito = 1;
                        break;
                    case 1:
                        oProducto = db.Productos.Where(a => a.ProductoId == Convert.ToInt64(criterio)).ToList();
                        resp.Exito = 1;
                        break;
                    case 2:
                        oProducto = db.Productos.Where(a => a.Nombre.Contains(criterio)).ToList();
                        resp.Exito = 1;
                        break;
                    case 3:
                        oProducto = db.Productos.Where(a => a.Descripcion.Contains(criterio)).ToList();
                        resp.Exito = 1;
                        break;
                    case 4:
                        oProducto = db.Productos.Where(a => a.Cantidad == Convert.ToDecimal(criterio)).ToList();
                        resp.Exito = 1;
                        break;
                    case 5:
                        oProducto = db.Productos.Where(a => a.Precio == Convert.ToDecimal(criterio)).ToList();
                        resp.Exito = 1;
                        break;
                    case 6:
                        oProducto = db.Productos.Where(a => a.Reorden == Convert.ToDecimal(criterio)).ToList();
                        resp.Exito = 1;
                        break;
                    case 7:
                        oProducto = db.Productos.Where(a => a.Itbis == Convert.ToDecimal(criterio)).ToList();
                        resp.Exito = 1;
                        break;

                    default:
                        oProducto = new List<Producto>();
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


            return new KeyValuePair<Respuesta, List<Producto>>(resp, oProducto);
        }

        [HttpGet("{param}/{criterio}")]
        
        public IActionResult GetAvanzado(int param, string criterio)
        {
            var valores = GetValores(param, criterio);
            List<Producto> oProducto = valores.Value;
            Respuesta respuesta = valores.Key;



            return Ok(oProducto);
        }

        [HttpPost]
        public IActionResult Add(ProductoRequest model)
        {
            Respuesta oRespuesta = new Respuesta();
            try
            {

                using (BlazorCrudContext db = new BlazorCrudContext())
                {
                    Producto oProducto = new Producto();
                    oProducto.Nombre = model.Nombre;
                    oProducto.Descripcion = model.Descripcion;
                    //oProducto.FechaCreacrion = model.FechaCreacrion;
                    oProducto.Cantidad = model.Cantidad;
                    oProducto.Precio = model.Precio;
                    oProducto.Reorden = model.Reorden;
                    oProducto.Itbis = model.Itbis;
                    db.Productos.Add(oProducto);
                    db.SaveChanges();
                    oRespuesta.Exito = 1;
                }
            }
            catch (Exception ex)
            {

                //oRespuesta.Mensaje = ex.Message;
                throw;
            }


            return Ok(oRespuesta);
        }

        [HttpPut]
        public IActionResult Edit(ProductoRequest model)
        {
            Respuesta oRespuesta = new Respuesta();
            try
            {

                using (BlazorCrudContext db = new BlazorCrudContext())
                {
                    Producto oProducto = db.Productos.Find(model.ProductoId);
                    if (oProducto != null)
                    {
                        oProducto.Nombre = model.Nombre;
                        oProducto.Descripcion = model.Descripcion;
                        //oProducto.FechaCreacrion = model.FechaCreacrion;
                        oProducto.Cantidad = model.Cantidad;
                        oProducto.Precio = model.Precio;
                        oProducto.Reorden = model.Reorden;
                        oProducto.Itbis = model.Itbis;
                        db.Entry(oProducto).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        db.SaveChanges();
                        oRespuesta.Exito = 1;
                    }
                    else
                    {
                        oRespuesta.Mensaje = "El producto" +
                            " que desea eliminar no existe";
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
                    Producto oProducto = db.Productos.Find(Id);
                    if (oProducto != null)
                    {
                        db.Remove(oProducto);
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

