using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Vista_Inicio_home_previo : System.Web.UI.Page
{
    #region Constantes para errores y mensajes
    /// <summary>
    /// Constantes para errores y mensajes
    /// </summary>
    private int iEXITO = 1;
    private int iALERTA = 2;
    private int iERROR = 3;
    #endregion

    #region Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        SessionTimeOut session = new SessionTimeOut();
        //bool sessionE = session.IsSessionTimedOut();
        bool bSessionE = false;
        ///INICIO SESSION
        if (!bSessionE)
        {
            //Se declaran los breadCrumbs
            string[] sDatos = { "Inicio" };
            string[] sUrl = { "" };
            breadCrum.vMigajas(sDatos, sUrl, true);
            //breadCrum.migajas(datos, url);
            ///TRY
            try
            {
                ///recupera el id de Usuario
                string sCveUser = Session["iIdUsuario"].ToString();
                
            }///FIN TRY
            ///INICIO CATCH
            catch (Exception ex)
            {
                ///HAY ERROR EN EJECUCION, REDIRECCIONA A INICIO
                Response.Redirect("../../Login_previo.aspx");
            }///FIN CATCH
        }///FIN IF SESSION
        ///INICIO ELSE SESSION
        else
        {
            ///CIERRA LA SESSION Y REDIRECCIONA A LOGIN
            Session.Clear();
            Session.Abandon();
            Response.Redirect("../../Login_previo.aspx");
        } ///FIN ELSE SESSION
    }
    #endregion
}