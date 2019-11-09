using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Vista_Diseno_header_prev : System.Web.UI.UserControl
{
    #region Page Load
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
                hulMenu.InnerHtml = menuUsuario(Session["iIdUsuario"].ToString());
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
                Response.Redirect("../../Login_previo.aspx", false);
            }///FIN TRY
            ///INICIO CATCH
            catch (Exception ex)
            {

            }///FIN CATCH

        }///FIN ELSE ESTA CADUCADA SESSION

    }
    #endregion


    #region Método para cerrar session
    /// <summary>
    /// Método para cerrar session
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

            Session.Abandon();
            Session.Clear();
            Session.Abandon();
            Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));
            Session.RemoveAll();
            Response.Redirect("../../LogIn_previo.aspx", false);
        }
        catch (Exception ex)
        {
            Console.Write(ex.Message);
        }

    }
    #endregion


    #region Método para construir el menú
    /// <summary>
    /// Método para construir el menú
    /// </summary>
    /// <param name="sIdUser"></param>
    /// <returns></returns>
    public string menuUsuario(string sIdUser)
    {
        ///Instancia a objeto para desencryptar id de usuario
        Security encUser = new Security(sIdUser.ToString());

        string sQuery = "select cc_m.iIdSubMenu,cc_m.sNombreMenu,sUrl, " +
        "(select COUNT(iIdUsuario) from tr_SubMenu_Usuarios where iIdUsuario="+encUser.DesEncriptar()+" and iIdSubMenu=cc_m.iIdSubMenu) as iAplicaPrevio, " +
        "(select COUNT(iIdUsuario) from tb_ActividadesPendientesUsuario where iIdUsuario="+encUser.DesEncriptar()+" and iIdTipoActividad=1 and iIdEstatus in(1)) as iTotal "+
        "from cc_MenuPrevio cc_m "+
        "where cc_m.iEstatus=1";

        Conexion con = new Conexion();
        List<string> lstDatos = new List<string>();
        ///Inicializa variable a retornar
        string sResultado = "";
        string sActive = "";

        lstDatos = con.ejecutarConsultaRegistroMultiples(sQuery);
        ///VERIFICA SI TIENE REGISTROS
        if (lstDatos.Count > 1)
        {
            ///SE COMIENZA A CONSTRUIR ELEMENTOS
            sResultado += "<li><a href='../Inicio/home_previo.aspx'>Inicio</a></li>";
            ///RECORRE CICLO
            for (int i = 1; i < lstDatos.Count; i = i + 5)
            {
                //sActive = i == 1 ? "class='active'" : "";
                ///Verifica si se tiene acceso al Menu
                if (lstDatos[i + 3].Equals("1"))
                {
                    ///RECUPERA ID DE SUBMENU PARA ACCESO
                    Security encCon = new Security(lstDatos[i + 0]);

                    sResultado += "<li><a href='" + lstDatos[i + 2] + "?sTypeCtrlAc=" + encCon.Encriptar() + "'>" + lstDatos[i + 1] + " (" + lstDatos[i + 4] + ")</a></li>";
                }
                
            }///FIN RECORRE CICLO
            /// DIV FINAL
            sResultado += "<div class='clearfix'></div>";
        }///FIN VERIFICA TIENE REGISTROS


        ///RETORNA RESULTADO
        return sResultado;

    }
    #endregion
}