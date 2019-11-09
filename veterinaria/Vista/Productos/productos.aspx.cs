using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Vista_Productos_productos : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Se declaran los breadCrumbs
        string[] sDatos = { "Inicio", "Productos" };
        string[] sUrl = { "" };
        breadCrum.migajas(sDatos, sUrl);
    }
}