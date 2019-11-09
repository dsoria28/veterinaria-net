using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Data.SqlClient;
using System.Configuration;

public partial class Vista_Inicio_Inicio : System.Web.UI.Page
{

    #region Page Load
    protected void Page_Load(object sender, EventArgs e)
    {

        //Se declaran los breadCrumbs
        string[] sDatos = { "Inicio" };
        string[] sUrl = { "" };
        breadCrum.migajas(sDatos, sUrl);
        /*SessionTimeOut session = new SessionTimeOut();
        //bool sessionE = session.IsSessionTimedOut();
        bool bSessionE = false;
        ///INICIO SESSION
        //if (!bSessionE)
        //{
            try
            {
                ///recupera el id de Usuario
                //string sCveUser = Session["iIdUsuario"].ToString();
                /////recupera id de Submenu
                //string sIdSubMenu = Request.QueryString["sTypeCtrlAc"];
                /////recupera id de Submenu
                //string sIdSubMenuPrevio = "MQA0AA==";
                /////INSTANCIA A CLASE PERMISOS
                //Permisos obj_permiso = new Permisos(sCveUser, sIdSubMenu);
                /////INSTANCIA A CLASE PERMISOS
                //Permisos obj_permiso_previo = new Permisos(sCveUser, sIdSubMenuPrevio);
                /////variable para almacenar el tipo de acceso
                //int iTipoAcceso = obj_permiso.getValidaAction(obj_permiso);
                /////variable para almacenar el tipo de acceso
                //int iTipoAccesoPrevio = obj_permiso_previo.getValidaAction(obj_permiso_previo);
                /////TRY
                /////Recupera id de usuario
                //ahddUser.Value = sCveUser;
                //ahddTypeCtrl.Value = sIdSubMenu;
                /////Declara objeto para encryptar id de acceso
                //Security secIdAction = new Security(iTipoAcceso.ToString());
                /////Declara objeto para encryptar id de acceso
                //Security secIdActionPrevio = new Security(iTipoAccesoPrevio.ToString());
                /////Asigna action para realizar
                //ahdAction.Value = secIdAction.Encriptar();
                /////Asigna action para realizar
                //ahdActionPrevio.Value = secIdActionPrevio.Encriptar();
                ////Lenar combobox patallas frecuentes
                //fn_llenar_combobox_pantallas_frecuentes();
                ////Se llena combobox
                //fn_llenar_combobox_pantallas_inicio();
                /////
                //fn_mostrar_dialog_aviso(sCveUser);
            }///FIN TRY
            ///INICIO CATCH
            catch (Exception ex)
            {
                ///HAY ERROR EN EJECUCION, REDIRECCIONA A INICIO
                Response.Redirect("../../Login.aspx");
            }///FIN CATCH
        //}///FIN IF SESSION
        ///INICIO ELSE SESSION
        /*else
        {
            ///CIERRA LA SESSION Y REDIRECCIONA A LOGIN
            Session.Clear();
            Session.Abandon();
            Response.Redirect("../../Login.aspx");
        } ///FIN ELSE SESSION*/
    }
    #endregion


    #region Método para guardar la pantalla de inicio
    /// <summary>
    /// Método para guardar la pantalla de inicio
    /// </summary>
    /// <param name="iIdPantallaInicio"></param>
    /// <param name="sIdUsuario"></param>
    /// <returns></returns>
    [WebMethod]
    public static Inicio fn_guardar_pantalla_inicio(int iIdPantallaInicio, string sIdUsuario) 
    {
        //Se desencipta el id del usuario
        Security sec_idUsuario = new Security(sIdUsuario);
        int iIdUsuario = int.Parse(sec_idUsuario.DesEncriptar());
        //Se crea la instancia de la clase inicio
        Inicio obj_inicio = new Inicio();
        //Se pasan los parametros que se usarán
        obj_inicio.iIdUsuario = iIdUsuario;
        obj_inicio.iIdPantallaInicio = iIdPantallaInicio;
        //Se ejecuta el método para guardar la pantalla de inicio
        obj_inicio.fn_guardar_pantalla_inicio(obj_inicio);
        return obj_inicio;
    }
    #endregion

    /*
    #region Método para obtener la vista de la pantalla de inicio del usuario
    /// <summary>
    /// Método para obtener la vista de la pantalla de inicio del usuario
    /// </summary>
    /// <param name="sIdUsuario"></param>
    /// <returns></returns>
    [WebMethod]
    public static Inicio fn_mostrar_pantalla_inicio(string sIdUsuario) 
    {
        //Se desencipta el id del usuario
        Security sec_idUsuario = new Security(sIdUsuario);
        int iIdUsuario = int.Parse(sec_idUsuario.DesEncriptar());
        //Se crea la instancia de la clase inicio
        Inicio obj_inicio = new Inicio();
        //Se pasan los parametros que se usarán
        obj_inicio.iIdUsuario = iIdUsuario;
        //Se ejecuta el método para guardar la pantalla de inicio
        obj_inicio.fn_mostrar_pantalla_inicio(obj_inicio);
        return obj_inicio;
    }
    #*/


    #region Método para obtener la vista de Pantallas Frecuentes
    /// <summary>
    /// Método para obtener la vista de Pantallas Frecuentes
    /// </summary>
    /// <param name="sIdUsuario"></param>
    /// <returns></returns>
    [WebMethod]
    public static Inicio fn_vista_pantallas_frecuentes(string sIdUsuario)
    {
        //Se desencipta el id del usuario
        Security sec_idUsuario = new Security(sIdUsuario);
        int iIdUsuario = int.Parse(sec_idUsuario.DesEncriptar());
        //Se crea la instancia de la clase inicio
        Inicio obj_inicio = new Inicio();
        //Se pasan los parametros que se usarán
        obj_inicio.iIdUsuario = iIdUsuario;
        //Se ejecuta el método para guardar la pantalla de inicio
        obj_inicio.fn_vista_pantallas_frecuentes(obj_inicio);
        return obj_inicio;
    }
    #endregion

   
    #region Función para generar la lista de pantallas frecuentes
    /// <summary>
    /// Función para generar la lista de pantallas frecuentes
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public static Inicio fn_generar_lista_pantallas_frecuentes()
    {
        //Se instancia la clase administración de pantallas frecuentes
        Inicio obj_inicio = new Inicio();
        obj_inicio.fn_generar_lista_pantallas_frecuentes(obj_inicio);
        return obj_inicio;
    }
    #endregion

    
    #region Función para validar el agregar la pantalla frecuente
    /// <summary>
    /// Función para validar el agregar la pantalla frecuente
    /// </summary>
    /// <param name="iIdPantallaFrecuente"></param>
    /// <param name="sIdUsuario"></param>
    /// <returns></returns>
    [WebMethod]
    public static Inicio fn_validar_agregar_pantalla_frecuente(int iIdPantallaFrecuente, string sIdUsuario)
    {
        //Se desencipta el id del usuario
        Security sec_idUsuario = new Security(sIdUsuario);
        int iIdUsuario = int.Parse(sec_idUsuario.DesEncriptar());
        //Se instancia la clase administración de pantallas frecuentes
        Inicio obj_inicio = new Inicio();
        obj_inicio.iIdPantallaFrecuente = iIdPantallaFrecuente;
        obj_inicio.iIdUsuario = iIdUsuario;
        obj_inicio.fn_validar_agregar_pantalla_frecuente(obj_inicio);
        return obj_inicio;
    }
    #endregion

    
    #region Función para agregar o eliminar las pantallas frecuentes
    /// <summary>
    /// Función para agregar o eliminar las pantallas frecuentes
    /// </summary>
    /// <param name="iIdPantallaFrecuente"></param>
    /// <param name="sIdUsuario"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    [WebMethod]
    public static Inicio fn_guardar_eliminar_pantalla_frecuente(int iIdPantallaFrecuente, string sIdUsuario, int type)
    {
        //Se desencipta el id del usuario
        Security sec_idUsuario = new Security(sIdUsuario);
        int iIdUsuario = int.Parse(sec_idUsuario.DesEncriptar());
        //Se instancia la clase administración de pantallas frecuentes
        Inicio obj_inicio = new Inicio();
        obj_inicio.iIdPantallaFrecuente = iIdPantallaFrecuente;
        obj_inicio.iIdUsuario = iIdUsuario;
        obj_inicio.type = type;
        Security secIdUser = new Security(HttpContext.Current.Session["iIdUsuario"].ToString());
        obj_inicio.iIdUsuarioAccion = int.Parse(secIdUser.DesEncriptar());
        obj_inicio.fn_guardar_eliminar_pantalla_frecuente(obj_inicio);
        return obj_inicio;
    }
    #endregion

    /*
    #region Función para llenar combo pantallas
    /// <summary>
    /// Función para llenar combo pantallas
    /// </summary>
    public void fn_llenar_combobox_pantallas_frecuentes() {
        int iIdUsuarioSesion;
        Security sec_idUsuario = new Security(Session["iIdUsuario"].ToString());
        iIdUsuarioSesion = int.Parse(sec_idUsuario.DesEncriptar());
        SqlConnection sqlCon = new SqlConnection(ConfigurationManager.ConnectionStrings["DB_SOPConnectionString"].ConnectionString.ToString());
        SqlCommand sqlCom = new SqlCommand("SELECT CONVERT(VARCHAR(10),iIdSubMenu), sNombreSubMenu FROM ct_SubMenu WHERE iEstado=1 and iIdSubMenu in (SELECT iIdSubMenu FROM tr_SubMenu_Usuarios WHERE iTipoAcceso in (1,2) and iIdUsuario=" + iIdUsuarioSesion.ToString() + ")", sqlCon);
        SqlDataReader sqlDr = null;
        try
        {
            sqlCon.Open();
            sqlDr = sqlCom.ExecuteReader();
            this.addlPantallasFrecuentes.Items.Clear();
            while (sqlDr.Read())
            {
                this.addlPantallasFrecuentes.Items.Add(new ListItem(Convert.ToString(sqlDr[1]).Trim(), Convert.ToString(sqlDr[0]).Trim()));
            }
            this.addlPantallasFrecuentes.Items.Add(new ListItem("", String.Empty));
            this.addlPantallasFrecuentes.SelectedIndex = this.addlPantallasFrecuentes.Items.Count - 1;
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }
        finally
        {
            sqlCon.Close();
            sqlDr.Dispose();
        }
    }
    #endregion
    */

    #region Función para llenar combo pantallas inicio
    /// <summary>
    /// Función para llenar combo pantallas inicio
    /// </summary>
    public void fn_llenar_combobox_pantallas_inicio() {
        int iIdUsuarioSesion;
        Security sec_idUsuario = new Security(Session["iIdUsuario"].ToString());
        iIdUsuarioSesion = int.Parse(sec_idUsuario.DesEncriptar());
        //Se crea instancia a clase
        Inicio obj_inicio = new Inicio();
        obj_inicio.iIdUsuario = iIdUsuarioSesion;
        obj_inicio.fn_obtener_tipo_usuario(obj_inicio);
        SqlConnection sqlCon = new SqlConnection(ConfigurationManager.ConnectionStrings["DB_SOPConnectionString"].ConnectionString.ToString());
        SqlCommand sqlCom;
        if (obj_inicio.iTipoUsuario == 1)
            sqlCom = new SqlCommand("SELECT iIdTipoPantalla, sNombrePantalla FROM cc_TipoPantallaInicio WHERE iEstado=1", sqlCon);
        else
            sqlCom = new SqlCommand("SELECT iIdTipoPantalla, sNombrePantalla FROM cc_TipoPantallaInicio WHERE iEstado=1 and iIdTipoPantalla !=2", sqlCon);
        SqlDataReader sqlDr = null;
        try
        {
            sqlCon.Open();
            sqlDr = sqlCom.ExecuteReader();
            this.addlPantallaInicio.Items.Clear();
            while (sqlDr.Read())
            {
                this.addlPantallaInicio.Items.Add(new ListItem(Convert.ToString(sqlDr[1]).Trim(), Convert.ToString(sqlDr[0]).Trim()));
            }
            this.addlPantallaInicio.SelectedIndex = this.addlPantallaInicio.Items.Count - 1;
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }
        finally
        {
            sqlCon.Close();
            sqlDr.Dispose();
        }
    }
    #endregion


    


   


    #region fn_mostrar_dialog_aviso
    public void fn_mostrar_dialog_aviso(string sCveUser)
    {
        ///INSTANCIA A LA CLASE CONEXIÓN
        Conexion conn = new Conexion();
        ///Instancia a objeto para desencryptar id de usuario
        Security encUser = new Security(sCveUser.ToString());
        ///QUERY PARA RECUPERAR TOTAL DE AVISOS
        string sQuery = "SELECT COUNT(*) FROM tb_Avisos WHERE iIdUsuario = " + encUser.DesEncriptar() + " AND ISNULL(iVisto,0) NOT IN(1)";
        ///EJECUTA Y ASIGNA VALORES
        string[] sTotal = conn.ejecutarConsultaRegistroSimple(sQuery);
        ///VERIFICA ÉXITO EN EJECUCIÓN
        if (sTotal[0] == "1")
        {
            ///VERIFICA SI EL TOTAL ES DIFERENTE DE 0
            if (sTotal[1] != "0")
            {
                ///INSTANCIA A LA CLASE AVISOS
                Avisos aviso = new Avisos();
                ///ASIGNA ID DE USUARIO
                aviso.gsUsuario = int.Parse(encUser.DesEncriptar());
                ///EJECUTA MÉTODO PARA RECUPERAR DETALLE DE AVISOS
                aviso.getGeneraListadoAvisos(aviso, 1);
                ///VERIFICA ÉXITO EN LA EJECUCIÓN DEL MÉTODO
                if (aviso.iResultado == 1)
                {
                    ///ASIGNA RESULTADOS A CAMPO
                    alblListadoAvisos.Text = aviso.sContenido;
                    ///MUESTRA EL MODAL
                    ClientScript.RegisterStartupScript(this.GetType(), "funcion", "<script>{$('#hdvAvisosUsuario').modal('show');}</script>");
                }
            }
        }
    }
    #endregion
    /// <summary>
    /// MÉTODO PARA ACTUALIZAR ESTATUS DE VISTO A AVISO
    /// </summary>
    /// <param name="sIdUsuario"></param>
    /// <returns></returns>
    #region fn_actualizar_estatus_aviso_usuario
    [WebMethod]
    public static Avisos fn_actualizar_estatus_aviso_usuario(string sIdUsuario) { 
        ///
        Avisos aviso = new Avisos();
        ///
        Security secUser = new Security(sIdUsuario);
        ///
        aviso.gsUsuario = int.Parse(secUser.DesEncriptar());
        //
        aviso.getActualizarEstatusAviso(aviso, 1);
        ///
        return aviso;
    }
    #endregion
}