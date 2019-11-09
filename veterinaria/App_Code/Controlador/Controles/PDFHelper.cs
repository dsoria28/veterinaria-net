using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de PDFHelper
/// </summary>
public class PDFHelper : PdfPageEventHelper
{

    // This is the contentbyte object of the writer
    PdfContentByte obj_cb;
    // we will put the final number of pages in a template
    PdfTemplate obj_template;
    // this is the BaseFont we are going to use for the header / footer
    BaseFont obj_bf = null;
    // This keeps track of the creation time
    DateTime obj_time = DateTime.Now;

    public string sRuta { get; set; }


    public override void OnEndPage(PdfWriter obj_writer, Document obj_document)
    {
        base.OnEndPage(obj_writer, obj_document);

        //PdfContentByte cb = obj_writer.DirectContent;

        //Image imgSoc = Image.GetInstance(HttpContext.Current.Server.MapPath("../../Styles/Imagenes/logo_esaner_ro.png"));

        //imgSoc.ScaleToFit(110,110);
        //imgSoc.SetAbsolutePosition(0, 750);

        //ColumnText ct = new ColumnText(cb);
        //ct.AddText(new Chunk(imgSoc, 0, 0));
    }

    public override void OnStartPage(PdfWriter writer, Document document)
    {
        base.OnStartPage(writer, document);
        PdfPTable t;
        PdfPCell c;

        Image imgLogo;

        imgLogo = iTextSharp.text.Image.GetInstance(sRuta);
        imgLogo.ScaleToFit(200, 200);
        t = new PdfPTable(1);
        t.WidthPercentage = 100;

        float[] w = new float[1];
        w[0] = 10;
        t.SetWidths(w);

        c = new PdfPCell(imgLogo);
        c.Border = 0;
        c.VerticalAlignment = Element.ALIGN_TOP;
        c.HorizontalAlignment = Element.ALIGN_LEFT;
        t.AddCell(c);

        document.Add(t);
    }

    public PDFHelper()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }
}