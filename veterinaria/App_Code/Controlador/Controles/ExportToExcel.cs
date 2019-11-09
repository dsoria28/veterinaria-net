using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Web.UI.WebControls;

/// <summary>
/// Descripción breve de ExportToExcel
/// </summary>
public class ExportToExcel
{
    String logo;
    String encabezado;

    //Constructor el cual no recibe ningun parametro, el reporte no tendra logo y titulo
	public ExportToExcel(){}

    //Constructor el cual recibe como parametros el Logo tipo y el titulo del reporte
    public ExportToExcel(String logo,String encabezado)
    {
        this.logo = logo;
        this.encabezado = encabezado;
    }

    public void Call_Export_To_Excel_Data(String nombre,GridView DataGrid)
    {
        String fechaHoraActual = DateTime.Now.ToString("ddMMyyyyhhmmss");
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.Buffer = true;
        HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
        HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + nombre + "_" + fechaHoraActual + ".xls");
        HttpContext.Current.Response.Charset = "UTF-8";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;

        HttpContext.Current.Response.Write(Export_HTML_To_Excel(DataGrid)); //Llamada al procedimiento HTML

        HttpContext.Current.Response.End();
    
    }

    public void Call_Export_To_Excel_Without_Data(String nombre)
    {
        String fechaHoraActual = DateTime.Now.ToString("ddMMyyyyhhmmss");
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.Buffer = true;
        HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
        HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + nombre + "_" + fechaHoraActual + ".xls");
        HttpContext.Current.Response.Charset = "UTF-8";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;

    }


    public void Call_Export_To_Excel(String nombre,String query)
    {
        String fechaHoraActual = DateTime.Now.ToString("ddMMyyyyhhmmss");
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.Buffer = true;
        HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
        HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename="+nombre+"_"+fechaHoraActual+".xls");
        HttpContext.Current.Response.Charset = "UTF-8";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
        
        HttpContext.Current.Response.Write(Export_HTML_To_Excel(query)); //Llamada al procedimiento HTML

        HttpContext.Current.Response.End();
    }

    //Medoto para ejecutar la Exportacion del reporte con query
    public string Export_HTML_To_Excel(String query)
    {
        string ruta;
        string img;
        Page page1 = new Page();
        HtmlForm form1 = new HtmlForm();

        //Se establece conexion con la BD
        Conexion conexion = new Conexion();

        //Se retornan los resultados al objeto
        DataTable dt = conexion.ejecutarConsultaRegistroMultiplesData(query);

        //Create a dummy GridView
        GridView GridView1 = new GridView();
        GridView1.AllowPaging = false;
        GridView1.DataSource = dt;
        GridView1.DataBind();

        GridView1.EnableViewState = false;
        page1.EnableViewState = false;

        page1.Controls.Add(form1);
        form1.Controls.Add(GridView1);

        System.Text.StringBuilder builder1 = new System.Text.StringBuilder();
        System.IO.StringWriter writer1 = new System.IO.StringWriter(builder1);
        HtmlTextWriter writer2 = new HtmlTextWriter(writer1);

        if (logo != null &&  logo != ""){
            ruta = HttpContext.Current.Server.MapPath("../"+logo);
            img = "<img src='" + ruta + "' width='12%' height='9%'>";
        }else {
            ruta = "";
            img = "";
        }
        
        //Se agrega HTML con contenido, solo hasta el BODY
        writer2.Write("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\n"+
        "<html xmlns=\"http://www.w3.org/1999/xhtml\">\n<head>\n<title>Datos</title>\n<meta http-equiv=\"Content-Type\" content=\"text/html; "+
        "charset=iso-8859-1\" />\n<style>\n</style>\n</head>\n<body>\n");
        //Al BODY se le agrega logotipo si es que aplican
        writer2.Write("</br></br></br></br></br></br></br></br></br></br></br></br>"+img);
        //Se agrega titulo del reporte
        writer2.Write("<table><tr><td></td><td></td><td colspan='10'><font face=Arial size=5><center>"+encabezado+"</center></font></td></tr></table><br>");

        page1.DesignerInitialize();
        page1.RenderControl(writer2);
        writer2.Write("\n</body>\n</html>");
        page1.Dispose();
        page1 = null;
        return builder1.ToString();
    }


    //Metodo para ejecutar la exportacion del reporte con GridView
    public string Export_HTML_To_Excel(GridView GridView)
    {
        string ruta;
        string img;
        Page page1 = new Page();
        HtmlForm form1 = new HtmlForm();

        //Se establece conexion con la BD
        //Conexion conexion = new Conexion();

        //Se retornan los resultados al objeto
        //DataTable dt = conexion.ejecutarConsultaRegistroMultiplesData(query);

        //Create a dummy GridView
        //GridView GridView1 = new GridView();
        GridView GridView1 = GridView;

        GridView1.AllowPaging = false;
        //GridView1.DataSource = dt;
        //GridView1.DataBind();

        GridView1.EnableViewState = false;
        page1.EnableViewState = false;

        page1.Controls.Add(form1);
        form1.Controls.Add(GridView1);

        System.Text.StringBuilder builder1 = new System.Text.StringBuilder();
        System.IO.StringWriter writer1 = new System.IO.StringWriter(builder1);
        HtmlTextWriter writer2 = new HtmlTextWriter(writer1);

        if (logo != null && logo != "")
        {
            ruta = HttpContext.Current.Server.MapPath("../images/" + logo);
            img = "<img src='" + ruta + "' width='12%' height='9%'>";
        }
        else
        {
            ruta = "";
            img = "";
        }

        //Se agrega HTML con contenido, solo hasta el BODY
        writer2.Write("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\n" +
        "<html xmlns=\"http://www.w3.org/1999/xhtml\">\n<head>\n<title>Datos</title>\n<meta http-equiv=\"Content-Type\" content=\"text/html; " +
        "charset=iso-8859-1\" />\n<style>\n</style>\n</head>\n<body>\n");
        //Al BODY se le agrega logotipo si es que aplican
        writer2.Write("</br></br></br></br></br></br></br></br></br></br></br></br>" + img);
        //Se agrega titulo del reporte
        writer2.Write("<table><tr><td></td><td></td><td colspan='10'><font face=Arial size=5><center>" + encabezado + "</center></font></td></tr></table><br>");

        page1.DesignerInitialize();
        page1.RenderControl(writer2);
        writer2.Write("\n</body>\n</html>");
        page1.Dispose();
        page1 = null;
        return builder1.ToString();
    }

}