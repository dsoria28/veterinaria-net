using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;



//librerias
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using System.Threading;
using System.Diagnostics;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using System.IO;
using iTextSharp;
using iTextSharp.text.html.simpleparser;

/// <summary>
/// Descripción breve de CrearPDF
/// </summary>
public class CrearPDF
{

    public void PDFFormato1(string FilePath, int numPaginas, string[] cuerpoHTML,string nomDoc,string title)
    {
        try
        {
            //Se Crea El Documento tamaño Carta
            Document document = new Document(PageSize.LETTER,100,100,100,100);

            //Se Obtiene la ruta del servidor
            string ruta = HttpContext.Current.Server.MapPath("~/"); ;
            //Indicamos donde se va a guardar eldocumento
            PdfWriter.GetInstance(document, new FileStream(ruta + "\\\\"+nomDoc+".pdf", FileMode.Create));
            
            //se abre el documento
            document.Open();
            document.AddTitle(title);




            for (int i = 0; i < numPaginas; i++)
            {
                string HTML = "";
                // Crea una imagen 
                iTextSharp.text.Image pdfImage;
                iTextSharp.text.Image pdflogo;
                iTextSharp.text.Image pdfInfo;

                //Se obtiene la ruta de la imagen
                pdfImage = iTextSharp.text.Image.GetInstance(ruta + "/images/marca.png");
                pdflogo = iTextSharp.text.Image.GetInstance(ruta + "/images/logo.png");
                pdfInfo = iTextSharp.text.Image.GetInstance(ruta + "/images/Info.png");

                //se pone el tamaño
                pdfImage.ScaleToFit(200, 790);
                pdflogo.ScaleToFit(200, 790);
                pdfInfo.ScaleToFit(400, 790);


                //se Indica la posicion
                pdfImage.Alignment = iTextSharp.text.Image.UNDERLYING;

                pdfImage.SetAbsolutePosition(0, 1);

                pdflogo.Alignment = iTextSharp.text.Image.UNDERLYING;

                pdflogo.SetAbsolutePosition(400, 685);

                pdfInfo.Alignment = iTextSharp.text.Image.UNDERLYING;

                pdfInfo.SetAbsolutePosition(200, 3);
                //Se agrega la imagen al documento
                document.Add(pdfImage);
                document.Add(pdflogo);
                document.Add(pdfInfo);


                //se crea un objeto para estilos
                iTextSharp.text.html.simpleparser.StyleSheet styles = new iTextSharp.text.html.simpleparser.StyleSheet();

                //obleto tipo html
                //iTextSharp.text.html.simpleparser.HTMLWorker hw = new iTextSharp.text.html.simpleparser.HTMLWorker(document,styles);

                styles.LoadTagStyle("p", "color", "red");
                styles.LoadStyle("redBigText", "size", "20pt");
                styles.LoadStyle("redBigText", "color", "RED");

                //obleto tipo html
                iTextSharp.text.html.simpleparser.HTMLWorker hw = new iTextSharp.text.html.simpleparser.HTMLWorker(document);
                if (cuerpoHTML.Count() > i)
                {
                    
                    HTML = cuerpoHTML[i];   
                }else
                {
                    HTML = "";
                    
                }

              /*  Paragraph p = new Paragraph();
                p.IndentationLeft = 100;
                HTML.HorizontalAlignment = Element.ALIGN_LEFT;
                p.Add(outerTable);
                document.Add(p);*/
                //hw.Parse(new StringReader(HTML),styles);
                //var parsedHtmlElements = HTMLWorker.ParseToList(new StringReader(HTML), styles);
                hw.Parse(new StringReader(HTML));
               // HTMLWorker.ParseToList(new StringReader(HTML), styles);
                document.NewPage();

            }


            //se Cierra el document0
            document.Close();

            //se muestra el documento
            mostrarPDF(nomDoc+".pdf", ruta);
        }
        catch(Exception ex)
        {

            Console.Write(ex.Message);
        }

    }

    public void PDFFormato1Imagen(string FilePath, int numPaginas, string[] cuerpoHTML, string nomDoc, string title,string nomImagen,string rutaUsu,float widthImage,float heigthImage,float positionX,float positionY,int numPageImagen)
    {
        try
        {
            //Se Crea El Documento tamaño Carta
            Document document = new Document(PageSize.LETTER, 100, 90, 100, 0);

            //Se Obtiene la ruta del servidor
            string ruta = HttpContext.Current.Server.MapPath("~/"); ;
            //Indicamos donde se va a guardar eldocumento
            PdfWriter.GetInstance(document, new FileStream(ruta + "\\\\" + nomDoc + ".pdf", FileMode.Create));

            //se abre el documento
            document.Open();
            document.AddTitle(title);




            for (int i = 0; i < numPaginas; i++)
            {
                string HTML = "";
                // Crea una imagen 
                iTextSharp.text.Image pdfImage;
                iTextSharp.text.Image pdflogo;
                iTextSharp.text.Image pdfInfo;

                iTextSharp.text.Image pdfImagenUsu;  

                //Se obtiene la ruta de la imagen
                pdfImage = iTextSharp.text.Image.GetInstance(ruta + "Vista/images/marca.png");
                pdflogo = iTextSharp.text.Image.GetInstance(ruta + "Vista/images/logo.png");
                pdfInfo = iTextSharp.text.Image.GetInstance(ruta + "Vista/images/Info.png");

                pdfImagenUsu = iTextSharp.text.Image.GetInstance(rutaUsu);

                //se pone el tamaño
                pdfImage.ScaleToFit(140, 800);
                pdflogo.ScaleToFit(170, 480);
                pdfInfo.ScaleToFit(400, 790);

                pdfImagenUsu.ScaleToFit(widthImage, heigthImage);

                //se Indica la posicion
                pdfImage.Alignment = iTextSharp.text.Image.UNDERLYING;

                pdfImage.SetAbsolutePosition(-1, -5);

                pdflogo.Alignment = iTextSharp.text.Image.UNDERLYING;

                pdflogo.SetAbsolutePosition(390, 670);

                pdfInfo.Alignment = iTextSharp.text.Image.UNDERLYING;

                pdfInfo.SetAbsolutePosition(200, 3);


                pdfImagenUsu.Alignment = iTextSharp.text.Image.UNDERLYING;

                pdfImagenUsu.SetAbsolutePosition(positionX, positionY);
                //Se agrega la imagen al documento
                document.Add(pdfImage);
                document.Add(pdflogo);
                document.Add(pdfInfo);

                if (i == (numPageImagen-1))
                {
                    document.Add(pdfImagenUsu);
                }

                //se crea un objeto para estilos
                iTextSharp.text.html.simpleparser.StyleSheet styles = new iTextSharp.text.html.simpleparser.StyleSheet();

                //obleto tipo html
                //iTextSharp.text.html.simpleparser.HTMLWorker hw = new iTextSharp.text.html.simpleparser.HTMLWorker(document,styles);

                styles.LoadTagStyle("p", "color", "red");
                styles.LoadStyle("redBigText", "size", "20pt");
                styles.LoadStyle("redBigText", "color", "RED");

                //obleto tipo html
                iTextSharp.text.html.simpleparser.HTMLWorker hw = new iTextSharp.text.html.simpleparser.HTMLWorker(document);
                if (cuerpoHTML.Count() > i)
                {

                    HTML = cuerpoHTML[i];
                }
                else
                {
                    HTML = "";

                }

                /*  Paragraph p = new Paragraph();
                  p.IndentationLeft = 100;
                  HTML.HorizontalAlignment = Element.ALIGN_LEFT;
                  p.Add(outerTable);
                  document.Add(p);*/
                //hw.Parse(new StringReader(HTML),styles);
                //var parsedHtmlElements = HTMLWorker.ParseToList(new StringReader(HTML), styles);
                hw.Parse(new StringReader(HTML));
                // HTMLWorker.ParseToList(new StringReader(HTML), styles);
                document.NewPage();

            }


            //se Cierra el document0
            document.Close();

            //se muestra el documento
            mostrarPDF(nomDoc + ".pdf", ruta);
        }
        catch (Exception ex)
        {

            Console.Write(ex.Message);
        }

    }

    private void mostrarPDF(string s, string r)
    {
       


        HttpContext.Current.Response.ClearContent();
        HttpContext.Current.Response.ClearHeaders();
        HttpContext.Current.Response.AddHeader("Content-Disposition", "inline;filename=" + s);
        HttpContext.Current.Response.ContentType = "application/pdf";
        HttpContext.Current.Response.WriteFile(r + s);
        HttpContext.Current.Response.Flush();
        HttpContext.Current.Response.Clear();

       // System.IO.File.Delete(r+s);

    }
	public CrearPDF()
	{
		//
		// TODO: Agregar aquí la lógica del constructor
		//
	}
}