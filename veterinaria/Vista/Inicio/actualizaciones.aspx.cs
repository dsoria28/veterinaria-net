using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Vista_Inicio_actualizaciones : System.Web.UI.Page
{
    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {

        SessionTimeOut obj_session = new SessionTimeOut();
        bool bSessionE = obj_session.IsSessionTimedOut();
        ///INICIO SESSION
        if (!bSessionE)
        {
            //Se declaran los breadCrumbs
            string[] sDatos = { "Actualizaciones" };
            string[] sUrl = { "" };
            breadCrum.migajas(sDatos, sUrl);

            ///TRY
            try
            {
                ///recupera el id de Usuario
                string sCveUser = Session["iIdUsuario"].ToString();

                ///Objeto a Clientes
                Actualizacion obj_actualizacion = new Actualizacion();
                obj_actualizacion.getContenidoEncabezado(obj_actualizacion);

                alblListaActualizacion.Text = obj_actualizacion.sContenido;
            }///FIN TRY
            ///INICIO CATCH
            catch (Exception ex)
            {
                ///HAY ERROR EN EJECUCION, REDIRECCIONA A INICIO
                Response.Redirect("../Inicio/Inicio.aspx");
            }///FIN CATCH
        }///FIN IF SESSION
        ///INICIO ELSE SESSION
        else
        {
            ///CIERRA LA SESSION Y REDIRECCIONA A LOGIN
            Session.Clear();
            Session.Abandon();
            Response.Redirect("../../Login.aspx");
        } ///FIN ELSE SESSION
    }
    #endregion

    #region fn_generar_PDF
    /// <summary>
    /// Método para generar la vista del pdf
    /// </summary>
    /// <param name="sIdNotificacion"></param>
    /// <returns></returns>
    [WebMethod]
    public static Actualizacion fn_generar_PDF(string sIdNotificacion)
    {
        ///Instancia a la clase Actualizacion
        Actualizacion obj_actualizacion = new Actualizacion();
        ///Instancia a la clase Security para desencriptar el id
        Security obj_idNotificacion = new Security(sIdNotificacion);
        ///Se asignan valores
        obj_actualizacion.giIdNotificacion = int.Parse(obj_idNotificacion.DesEncriptar());
        ///Se llama al método recuperar datos
        obj_actualizacion.recuperaDatosManual(obj_actualizacion);
        ///Se retorna el objeto
        return obj_actualizacion;
    }
    #endregion

    #region fn_recupera_datos_actualizacion
    /// <summary>
    /// Método para recuperar los datos de la actualización
    /// </summary>
    /// <param name="sIdNotificacion"></param>
    /// <returns></returns>
    [WebMethod]
    public static Actualizacion fn_recupera_datos_actualizacion(string sIdNotificacion)
    {
        ///Instancia a la clase Actualizacion
        Actualizacion obj_actualizacion = new Actualizacion();
        ///Instancia a la clase Security para desencriptar el id
        Security obj_idNotificacion = new Security(sIdNotificacion);
        ///Se asignan valores
        obj_actualizacion.giIdNotificacion = int.Parse(obj_idNotificacion.DesEncriptar());
        ///Se llama al método recuperar datos
        obj_actualizacion.recuperaDatosActualizacion(obj_actualizacion);
        ///Se retorna el objeto
        return obj_actualizacion;
    }
    #endregion
}