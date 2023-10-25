using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iText.Kernel.Pdf;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using iText.IO.Image;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Pdfa;

namespace PLMVC.Controllers
{
    public class DulceriaController : Controller
    {

        //Muestra todos los productos disponibles de la dulceria
        public IActionResult Dulceria()
        {
            MLCore.Dulceria dulceria = new MLCore.Dulceria();
            MLCore.Result result = BLCore.Dulceria.GetAll();    
            dulceria.Dulcerias = result.Objects;

            return View(dulceria);
        }

        //Agrega productos al carrito 
        public IActionResult Carrito(int IdDulceria)
        {
            //Valida si existe el producto, esta en false porque se agrega un primer producto
            bool existe = false;
            
            MLCore.Carrito carrito = new MLCore.Carrito();
            carrito.Carritos = new List<object>();
            MLCore.Result result = BLCore.Dulceria.GetById(IdDulceria);

            if (HttpContext.Session.GetString("Carritos") == null)
            {
                if (result.Correct)
                {

                    MLCore.Dulceria dulceria = (MLCore.Dulceria)result.Object;

                    dulceria.Cantidad = 1;
                    carrito.Carritos.Add(dulceria);
                    //Serializar carrito
                    HttpContext.Session.SetString("Carritos", Newtonsoft.Json.JsonConvert.SerializeObject(carrito.Carritos));


                }

            }

            else
            {

                MLCore.Dulceria dulceria = (MLCore.Dulceria)result.Object;
                GetCarrito(carrito);//Recupero el carrito
                foreach (MLCore.Dulceria dulce in carrito.Carritos)
                {
                    if(dulceria.IdDulceria == dulce.IdDulceria)
                    {

                        dulce.Cantidad = dulce.Cantidad + 1;
                        existe = true;
                        break;  
                    }
                    else
                    {

                        existe = false;

                    }

                }

                if(existe = true)
                {
                    HttpContext.Session.SetString("Carritos", Newtonsoft.Json.JsonConvert.SerializeObject(carrito.Carritos));  
                }
                else
                {

                    dulceria.Cantidad = 1;
                    carrito.Carritos.Add(dulceria);
                    HttpContext.Session.SetString("Carritos", Newtonsoft.Json.JsonConvert.SerializeObject(carrito.Carritos));

                }
                

            }

            return RedirectToAction("Dulceria");   


        }

        //Recuperar los productos del carrito
        public MLCore.Carrito GetCarrito(MLCore.Carrito carrito)
        {
            var ventaSesion = Newtonsoft.Json.JsonConvert.DeserializeObject<List<object>>(HttpContext.Session.GetString("Carritos"));

            foreach (var obj in ventaSesion)
            {
                MLCore.Dulceria dulceria = Newtonsoft.Json.JsonConvert.DeserializeObject<MLCore.Dulceria>(obj.ToString());
                carrito.Carritos.Add(dulceria);


            }

            return carrito;

        }


        //Muestra la vista de carrito, todos los productos que hay
        public IActionResult CarritoGetAll()
        {
            MLCore.Carrito carrito = new MLCore.Carrito();

            carrito.Carritos = new List<object>();  
            if(HttpContext.Session.GetString("Carritos") == null)
            {

                return View(carrito);

            }
            else
            {
                GetCarrito(carrito);
                return View(carrito);   

            }


        }



        public IActionResult GenerarPDF()
        {
            MLCore.Carrito carrito = new MLCore.Carrito();
            carrito.Carritos = new List<Object>();
            GetCarrito(carrito);

            //Crear un nuevo documento PDF en una ubicacion temporal
            string rutaTempPDF = Path.GetTempFileName() + ".pdf";

            using (PdfDocument pdfDocument = new PdfDocument(new PdfWriter(rutaTempPDF)))
            {
                using(Document document = new Document(pdfDocument))
                {
                    document.Add(new Paragraph("Resumen de Compra"));
                    //Crear la tabla para mostrar la lista de Objetos
                    Table table = new Table(5); // 5 columnas
                    table.SetWidth(UnitValue.CreatePercentValue(100));//Ancho de la tabla al 100% del documento

                    //Añadir las celdas de encabezado a la tabla
                    table.AddHeaderCell("ID Producto");
                    table.AddHeaderCell("Producto");
                    table.AddHeaderCell("Precio Unitario");
                    table.AddHeaderCell("Cantidad");
                    table.AddHeaderCell("Imagen");

                    foreach (MLCore.Dulceria dulce in carrito.Carritos)
                    {
                        table.AddCell(dulce.IdDulceria.ToString());
                        table.AddCell(dulce.Nombre);
                        table.AddCell(dulce.Precio.ToString());
                        table.AddCell(dulce.Cantidad.ToString());
                        byte[] imageBytes = Convert.FromBase64String(dulce.Imagen);
                        ImageData data = ImageDataFactory.Create(imageBytes);
                        Image image = new Image(data);
                        table.AddCell(image.SetWidth(50).SetHeight(50));

                    }

                    //Añadir la tabla al documento
                    document.Add(table);


                }
            }


            // Leer el archivo PDF como un arreglo de bytes
            byte[] fileBytes = System.IO.File.ReadAllBytes(rutaTempPDF);

            // Eliminar el archivo temporal
            System.IO.File.Delete(rutaTempPDF);
            HttpContext.Session.Remove("Carrito");

            // Descargar el archivo PDF
            return new FileStreamResult(new MemoryStream(fileBytes), "application/pdf")
            {
                FileDownloadName = "ReporteProductos.pdf"
            };
        }


    }
}
