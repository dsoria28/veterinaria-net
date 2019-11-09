using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Vista_Inicio_avisos_generales : System.Web.UI.Page
{
    private int iMODIFICAR = 1;
    private int iCONSULTA = 2;
    private int iSIN_ACCESO = 3;

    /// <summary>
    /// PAGE_LOAD
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        //Instancia a la clase SessionTimeOut
        SessionTimeOut obj_Session = new SessionTimeOut();
        //****************
        //se manda llamar al metodo de validar sesion
        bool bSessionE = obj_Session.IsSessionTimedOut();
        //***********

        //se valida la sesion
        if (!bSessionE)
        {
            //SE asigna el ID de usuario Loggeado
            ahddUser.Value = Session["iIdUsuario"].ToString();
            //**************************

            //Inicio TRY
            try
            {
                //Se declaran y crean los breadCrumbs
                string[] sDatos = { "Inicio", "Avisos" };
                string[] sUrl = { "" };
                breadCrum.migajas(sDatos, sUrl);
                //*********************************
                ///MANDA LLAMAR MÉTODO PARA GENERAR DETALLE
                fn_generar_detalle_aviso(Session["iIdUsuario"].ToString());
                ///INSTANCIA A LA CLASE AVISOS
                Avisos aviso = new Avisos();
                ///VARIABLE PARA RECUPERAR ID DE USUARIO
                Security secUser = new Security(Session["iIdUsuario"].ToString());
                aviso.gsUsuario = int.Parse(secUser.DesEncriptar());
                ///SE MANDA LLAMAR MÉTODO PARA ACTUALIZAR ESTATUS
                aviso.getActualizarEstatusAviso(aviso, 2);
            }
            //*******
            //Inicio Catch
            catch
            {
                //si ocurre algun error se redirecciona al usuario al Inicio 
                Response.Redirect("../Inicio/Inicio.aspx");
                //******
            }
            //******************

        }
        //**********
        else
        {
            //si se termino el tiempo de sesion se redirecciona al Login y se limpia sesion
            Session.Clear();
            Session.Abandon();
            Response.Redirect("../../Login.aspx");
            //***********
        }
        //*************
    }
    #endregion

    /// <summary>
    /// MÉTODO PARA GENERAR DETALLE DE AVISOS SIN VER
    /// </summary>
    /// <param name="sCveUser"></param>
    #region fn_generar_detalle_aviso
    public void fn_generar_detalle_aviso(string sCveUser)
    {
        ///
        Conexion conn = new Conexion();
        ///
        string dFECHA_ACTUAL = DateTime.Now.ToString("yyyy-MM-dd");
        ///Instancia a objeto para desencryptar id de usuario
        Security encUser = new Security(sCveUser.ToString());
        ///
        string sQuery = "SELECT COUNT(*) FROM tb_Avisos WHERE '" + dFECHA_ACTUAL + " 00:00:00' BETWEEN dFechaInicio AND dFechaFin AND iIdUsuario = " + encUser.DesEncriptar();
        string[] sTotal = conn.ejecutarConsultaRegistroSimple(sQuery);

        if (sTotal[0] == "1")
        {
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none","<script>$('#hdvAvisosUsuario').modal('show');</script>", false);
            if (sTotal[1] != "0")
            {
                ///INTANCIA A CLASE AVISOS
                Avisos aviso = new Avisos();
                ///ASIGNA ID DE USUARIO
                aviso.gsUsuario = int.Parse(encUser.DesEncriptar());
                ///MANDA LLAMAR MÉTODO PARA GENERAR LISTADO
                aviso.getGeneraListadoAvisos(aviso, 2);
                ///VERIFICA ÉXITO EN EJECUCIÓN
                if (aviso.iResultado == 1)
                {
                    ///ASIGNA CONTENIDO A VARIABLE
                    alblListadoAvisos.Text = aviso.sContenido;
                }
            }
            else {
                hdvAlertaAviso.Visible = true;
            }
        }
    }
    #endregion
}