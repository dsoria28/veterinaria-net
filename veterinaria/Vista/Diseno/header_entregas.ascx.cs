using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Vista_Diseno_header_entregas : System.Web.UI.UserControl
{
    /// <summary>
    /// PAGE LOAD
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        ///INSTNACIA A CLASE DE SESSION
        SessionTimeOut session = new SessionTimeOut();
        ///EJECUTA METODO PARA VERIFICAR SI NO ESTA CADUCADA LA SESSION
        bool bSessionE = session.IsSessionTimedOut();
        ///VERIFICA SI NO ESTA CADUDADA SESSION
        if (!bSessionE)
        {
            ///INICIO TRY
            try
            {
                ///CONSTRUYE MENU
                string sIdUsuario = Session["iIdUsuario"].ToString();
                atxtIdUsuarioNotificaciones.Value = sIdUsuario;
                ///hulMenuUsuario.InnerHtml = menuUsuario(Session["iIdUsuario"].ToString());
                ///Variable para desencryptar id de usuario
                Security secNomUser = new Security(Session["NombreUsuario"].ToString());
                ///MUESTRA NOMBRE DE USUARIO
                alblNombreUsuario.Text = secNomUser.DesEncriptar();
            }///FIN TRY
             ///INICIO CATCH
            catch (Exception ex)
            {

            }///FIN CATCH

        }/////FIN VERIFICA NO ESTA CADUCADA LA SESSION
         ///INICIO EL SE ESTA CADUCA SESSION
        else
        {
            ///INICIO TRY
            try
            {
                ///LIMPIA VARIABLES
                Session.Clear();
                Session.Abandon();
                ///REDIRECCIONA A LOGIN
                Response.Redirect("../../LoginID.aspx", false);
            }///FIN TRY
             ///INICIO CATCH
            catch (Exception ex)
            {

            }///FIN CATCH

        }///FIN ELSE ESTA CADUCADA SESSION
    }


    /// <summary>
    /// Método para Cerrara sesión del sistema
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        try
        {

            if (Session["iIdUsuario"] != null) Session.Remove("iIdUsuario");
            if (Session["iTipoUsuario"] != null) Session.Remove("iTipoUsuario");
            if (Session["NombreUsuario"] != null) Session.Remove("NombreUsuario");
            if (Session["iAccesoTarjeta"] != null) Session.Remove("iAccesoTarjeta");

            Session.Abandon();
            Session.Clear();
            Session.Abandon();
            Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));
            Session.RemoveAll();
            Response.Redirect("../../LoginID.aspx", false);
        }
        catch (Exception ex)
        {
            Console.Write(ex.Message);
        }

    }
}