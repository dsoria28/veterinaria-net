using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Vista_Diseno_brearCrumbs_previo : System.Web.UI.UserControl
{
   
    #region Variable
    private string sResultado = "";
    #endregion
   
   
    #region Inicio migajas
    /// <summary>
    /// Método para dibujar brearCrumbs de previo
    /// </summary>
    /// <param name="datosB"></param>
    /// <param name="url"></param>
    public void vMigajas(string[] sDatos, string[] sUrl, bool iAplicaHome)
    {

        //se declaran variables
        int iTotal = 0;
        string sRes = "<div class='container'>"+
                        "<div class='row'>" +
                            "<div class='btn-group btn-breadcrumb' style='padding-bottom: 2%;'>";
        /*============================*/

        //se valida si aplica home
        if (iAplicaHome == true)
        {
            sRes += "<a href='../../Vista/Inicio/home_previo.aspx' class='btn btn-default'><i class='glyphicon glyphicon-home txt-Azul'></i></a>";
        }

        //Inicio <strong>│</strong> Contenidos <strong>│</strong> SEGURIDAD EN LA CADENA DE SUMINISTRO
        //se valida su el 
        if (sDatos.Length > 0)
        {
            iTotal = sDatos.Length;
            for (int i = 0; i < sDatos.Length; i++)
            {
                ///VERIFICA SI ES EL ULTIMO REGISTRO PARA ASIGNAR CLASE ACTIVA
                if (i == (iTotal - 1))
                {
                    sRes += "<a href='#' class='btn btn-default text-Bread'><b class='txt-Azul'>" + sDatos[i] + "&nbsp;</b><i class='fa fa-check-circle icon_green'></i></a>";
                }///ELSE DE QUE ES UNA SECUENCIA ANTERIOR
                else
                {
                    sRes += "<a href='#' class='btn btn-default'>" + sDatos[i] + "</a>";
                }

            }

        }
        //se cierra div
        sRes += "</div>" +
                    "</div>"+
                        "</div>";

        sResultado = sRes;
    }
    #endregion
  
   
    #region PAGE LOAD
    protected void Page_Load(object sender, EventArgs e)
    {
        //se pintan breadcrumbs
        lblBreadCrumbs.Text = sResultado; 
    }
    #endregion
   

    


}