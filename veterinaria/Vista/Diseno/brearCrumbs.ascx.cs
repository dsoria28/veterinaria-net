using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class Plantillas_brearCrumbs : System.Web.UI.UserControl
{
    #region Variable
    private string sResultado = "";
    #endregion


    #region Migajas
    public void migajas(string[] sDatosB,string[] sUrl)
    {
        int iTotal = 0;
        string sRes = "<ol class='breadcrumb'>";

        //Inicio <strong>│</strong> Contenidos <strong>│</strong> SEGURIDAD EN LA CADENA DE SUMINISTRO

        if (sDatosB.Length > 0)
        {
            iTotal = sDatosB.Length;
            for (int i = 0; i < sDatosB.Length; i++)
            {
                ///VERIFICA SI ES EL ULTIMO REGISTRO PARA ASIGNAR CLASE ACTIVA
                if (i == (iTotal - 1))
                {
                    sRes += "<li class='Active'>" + sDatosB[i] + "</li>";
                }///ELSE DE QUE ES UNA SECUENCIA ANTERIOR
                else {
                    sRes += "<li><a href='#'>" + sDatosB[i] + "</a></li>";
                }
                
            }

        }
        sRes += "</ol>";

        sResultado = sRes;
    }
    #endregion


    #region Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        cb1.Text = sResultado;
    }
    #endregion
}
