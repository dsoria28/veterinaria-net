using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Xml.Linq;
using System.Xml;
using System.Net.Mail;
using System.Net;



//using 
using System.Drawing;
using System.Text;
using System.Net.Sockets;
using System.IO;
using System.Text.RegularExpressions;
using System.ComponentModel;
using System.Drawing.Imaging;
using System.Web.Services;
/************************/

public partial class Login : System.Web.UI.Page
{
    #region Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        ///Valida HTTPS
        ///validateUrl(true);
        
    }

    /// <summary>
    /// Método para redireccionar a subdominio
    /// </summary>
    /// <param name="iValida"></param>
    protected void validateUrl(bool iValida)
    {
        ///verifica si necesita validación
        if (iValida == true)
        {
            if (!Request.IsLocal && !Request.IsSecureConnection)
            {
                //string redirectUrl = Request.Url.ToString().Replace("http:", "https:");
                string redirectUrl = "https://sop.nadglobal.com/sop_nad/Login.aspx";
                Response.Redirect(redirectUrl, false);
                //HttpContext.ApplicationInstance.CompleteRequest();
            }
            
        }
    }

    #endregion

    #region Metodo para abrir el Dialog del Capctha

    protected void vAbreDialogCambioContraseña(object sender, EventArgs e)
    {
        //manda llamar la función de javaScript de abrir Dialog
        ClientScript.RegisterStartupScript(this.GetType(), "funcion", "<script>{$('#hdvDialogCambioPass').modal('show');$('#atxtUsuario').val('');$('#atxtCaptcha').val('');}</script>");
        /****************************************************************/
    }
    #endregion

    #region Metodo Para Entrar al sistema
    protected void btnEntrar_Click(object sender, EventArgs e)
    {
        
        //Instancias 
        //Clase Conexion
        Conexion obj_Conexion = new Conexion();
        //Clase LoginDatos
        LoginDatos obj_Login = new LoginDatos();
        //Clase de Security
        Security obj_secDatos;
        //**
        //Inicio TRY
        try
        {
            



            //se recuperan valores ingresados por el usuario
            //nombre de usuario
            string sUsuario = "";
            //contraseña
            string sPassword = "";
            //*********************************

            //se agrega el valor de los campos a los metodos set
            obj_Login.gssUsuario = sUsuario;
            obj_Login.gssPassword = sPassword;
            //*****

            obj_Login.vValidaUsuario(obj_Login);

            

            if (obj_Login.gsiResultado == 1)
            {
                //manda llamar la función de javaScript de abrir alerta y redireccionar
                ClientScript.RegisterStartupScript(this.GetType(), "funcion", "<script>{$.notificacionMsj(" + obj_Login.gsiResultado + ", \" " + obj_Login.gssMensaje + "\");/*setTimeout(function(){window.location.href='Vista/Inicio/Inicio.aspx';},0)*/}</script>");
                /****************************************************************/
                //se guardan las variables de sesión
                //Id de usuario
                //se encripta
                obj_secDatos = new Security(obj_Login.gsiIdUsuario.ToString());
                Session["iIdUsuario"] = obj_secDatos.Encriptar();
                //
                //Tipo de Usuario
                //se encripta
                obj_secDatos = new Security(obj_Login.gsiTipoUsuario.ToString());
                Session["iTipoUsuario"] = obj_secDatos.Encriptar();
                //Nombre de Usuario
                //se encripta
                string NombreUsuario = obj_Login.gssNombre + " " + obj_Login.gssApePat + " " + obj_Login.gssApeMat;
                obj_secDatos = new Security(NombreUsuario);
                Session["NombreUsuario"] = obj_secDatos.Encriptar();

                string a = Session["NombreUsuario"].ToString();

                obj_secDatos = new Security(a);

                string d = obj_secDatos.DesEncriptar();
                //se redirecciona a la pagina de inicio
                Response.Redirect("Vista/Inicio/Inicio.aspx", false);
                /**************************************************/
            }
            else if (obj_Login.gsiResultado == 4)
            {

                //manda llamar la funcion de javaScript de abrir alerta
                ClientScript.RegisterStartupScript(this.GetType(), "funcion", "<script>{/*$.notificacionMsj(" + obj_Login.gsiResultado + ", \" " + obj_Login.gssMensaje + "\");*/"+
                    "fn_abreDialogCambioPass('" + obj_Login.gssContenido + "','Cambio de contraseña','javaScript:alert(" + obj_Login.gsiIdUsuario + ")');$('#txtCambioPassword').val('');$('#atxtRepitePass').val('');}</script>");
                /****************************************************************/

                //atxtUsuarioCambioPassword.Text = obj_Login.gssUsuario;
                //ahiddenId.Value = obj_Login.gsiAccion.ToString();
                //ahiddenNombre.Value = obj_Login.gssUsuario;
            }
            else
            {
                //manda llamar la funcion de javaScript de abrir alerta
                ClientScript.RegisterStartupScript(this.GetType(), "funcion", "<script>{$.notificacionMsj(" + obj_Login.gsiResultado + ", \" " + obj_Login.gssMensaje + "\");}</script>");
                /****************************************************************/

            }
            
        }
        /********/
        /*Inicio CATCH*/
        catch (Exception ex)
        {
            //manda llamar la funcion de javaScript de abrir alerta
            ClientScript.RegisterStartupScript(this.GetType(), "funcion", "<script>{ $.notificacionMsj(3, 'Lo sentimos, ha sucedido un error de tipo <b>EXEPCIÓN</b> comunícate con el departamento de TI, Error:" + ex.Message + "');}</script>");
            /****************************************************************/
        }
        /**********/

    }
    #endregion*/


    #region  Metodo para elegir nuevo  captcha

    /// <summary>
    /// Metodo para elejir otro captcha
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void vElegirNuevoCaptcha(object sender, EventArgs e)
    {
        //se limpian las cajas de texto y mensaje de captcha
        //atxtUsuario.Text = "";
        //atxtCaptcha.Text = "";
        //*****************************************************
        //se agrega la nueva imagen en la etiqueta image
        //imageCaptcha.ImageUrl = "Vista/Utilerias/CrearCaptcha.aspx?New=1";
        //******************************************************

        //se habre el dialog donde se encuentra el captcha
        ClientScript.RegisterStartupScript(this.GetType(), "funcion", "<script>{$('#hdvDialogCambioPass').modal('show')}</script>");
        //********************************************************************************************************************************************************************************************
    }


    #endregion

    #region Metodo para hacer el Cambio de contraseña 
    public void vCambioPassword(object sender, EventArgs e)
    {

        try
        {
            /*Instancia a la clase conexion*/
            Conexion con = new Conexion();
            /******************************/


            //se crea objeto de respuesta
            LoginDatos obj_Login = new LoginDatos();
            //*************

            /*se recupera el usuario y captcha ingresados por el usuario*/
            string sUsuario = "";
            string sCaptcha = "";
            /***********************************************************/

            /*se recupera el codigo captcha de session encriptado*/
            string sCaptchaEncry = Session["CaptchaCode"].ToString();
            /****************************************************/

            /*Instancia a la clase security y se manda como parametro el codigo encryptado*/
            Security obj_SecCaptcha = new Security(sCaptchaEncry);
            /***********************************************************************/

            /*Se recupera el captcha de sesión desencriptado*/
            string sCaptchaDes = obj_SecCaptcha.DesEncriptar();
            /************************************************/

            /*Se valida que el usuario haya introducido la captcha correcta*/
            /*Si es correcta*/
            if (sCaptcha == sCaptchaDes)
            {
                //se settean las variables
                obj_Login.gssUsuario = sUsuario;
                obj_Login.gssPassword = rsNuevaPassword();
                obj_Login.gsiAccion = 1;
                /*Se manda llamar al metodo de Cambiar contraseña*/
                obj_Login.vCambiaPassword(obj_Login);
                /*********************************************************/

                if (obj_Login.gsiResultado == 1)
                {

                    obj_Login.vGeneraCorreo(obj_Login);
                    if (obj_Login.gsiResultado == 1)
                    {
                        //manda llamar la función de javaScript de abrir Dialog
                        ClientScript.RegisterStartupScript(this.GetType(), "funcion", "<script>{$.notificacionMsj(1,'Cambio de contraseña exitoso');$('#hdvConfirmacion').modal('toggle');}</script>");
                        /****************************************************************/
                    }
                    else
                    {
                        //manda llamar la función de javaScript de abrir Dialog
                        ClientScript.RegisterStartupScript(this.GetType(), "funcion", "<script>{$.notificacionMsj(\"" + obj_Login.gsiResultado + "\",\"" + obj_Login.gssMensaje + "\");$('#hdvDialogCambioPass').modal('show');}</script>");
                        /****************************************************************/
                    }
                }
                else
                {
                    //manda llamar la función de javaScript de abrir Dialog
                    ClientScript.RegisterStartupScript(this.GetType(), "funcion", "<script>{$.notificacionMsj(\""+obj_Login.gsiResultado+"\",\""+obj_Login.gssMensaje+"\");$('#hdvDialogCambioPass').modal('show');}</script>");
                    /****************************************************************/
                }
                /*******************************************************************************************************************************************************************************************************/

            }
            //Si el captcha no es correcto
            else
            {
                /*se limpian las cajas de texto*/
                //atxtUsuario.Text = "";
                //atxtCaptcha.Text = "";
                /***********************************************************/

                //manda llamar la funcion de javaScript de abrir Dialog
                ClientScript.RegisterStartupScript(this.GetType(), "funcion", "<script>{$.notificacionMsj(2,'El código ingresado no es el correcto intenta nuevamente.');$('#hdvDialogCambioPass').modal('show');}</script>");
                /****************************************************************/
                /**********************************************************************************************************************************************************************************************/
            }
                
        }
        catch (Exception ex)
        {
            /*se limpian las cajas de texto*/
            //atxtUsuario.Text = "";
            //atxtCaptcha.Text = "";
            /***********************************************************/
            //manda llamar la funcion de javaScript de abrir Dialog
            ClientScript.RegisterStartupScript(this.GetType(), "funcion", "<script>{$.notificacionMsj(2,'Error al realizar cambio de contraseña: "+ex.Message+"');$('#hdvDialogCambioPass').modal('show');}</script>");
            /****************************************************************/
        }



    }
    #endregion

    #region Metodo para crear  la nueva contraseña
    /// <summary>
    /// Metodo para cambiar la contraseña 
    /// </summary>
    /// <returns>string nueva contraseña</returns>
    public string rsNuevaPassword()
    {
        //se crea random para realizar contraseña
        Random rRandom = new Random();
        //***********************************
        //se crea variable con caracteres que puedan estar en la contraseña
        string sCaracteres = "_$-#&!¡%012345679ACEFGHKLMNPRSWXZab_$-#&!¡%cdefghijkhlmnopqrstuvwxyz_$-#&!¡%";
        //****************
        //se declara stringbuilder para almacenar la contraseña
        StringBuilder sContrasena = new StringBuilder();
        //***************

        //ciclo para recorrer caracterres y agregarla a la contraseña
        for (int i = 0; i < 15; i++)
        {
            sContrasena.Append(sCaracteres[rRandom.Next(sCaracteres.Length)]);
        }
        //*********

        //se retorna la contraseña
        return sContrasena.ToString();
        //**************

    }
    #endregion

    #region Metodo Para cambiar contraseña
    protected void vCambiaPasswordInicio(object sender, EventArgs e)
    {
        //***********************************************************/
        //Instancias 
        //Clase Conexion
        Conexion obj_Conexion = new Conexion();
        //Clase Login_Datos
        LoginDatos obj_Login = new LoginDatos();
        //Clase de Security
        Security obj_secDatos;
        //Inicio TRY
        try
        {




            //se recuperan Valores Ingresados por el usuario
            //nombre de usuario
            string sUsuario = "";// ahiddenNombre.Value;
            //contraseña
            string sPassword = "";// txtCambioPassword.Text;
            //Accion
            string sAccion = "";// ahiddenId.Value;
            //*********************************

            //se agrega el valor de los campos a los metodos set
            obj_Login.gssUsuario = sUsuario;
            obj_Login.gssPassword = sPassword;
            obj_Login.gsiAccion = Int32.Parse(sAccion);
            //*****
            obj_Login.vCambiaPassword(obj_Login);

            if (obj_Login.gsiResultado == 1)
            {
                obj_Login.vValidaUsuario(obj_Login);
                if (obj_Login.gsiResultado == 1)
                {
                    //manda llamar la funcion de javaScript de abrir alerta y redireccionar
                    ClientScript.RegisterStartupScript(this.GetType(), "funcion", "<script>{$.notificacionMsj(" + obj_Login.gsiResultado + ", \" " + obj_Login.gssMensaje + "\");setTimeout(function(){window.location.href='Vista/Inicio/Inicio.aspx';},0)}</script>");
                    /****************************************************************/
                    //se guardan las Variables de sesion
                    //Id de usuario
                    //se encripta
                    obj_secDatos = new Security(obj_Login.gsiIdUsuario.ToString());
                    Session["iIdUsuario"] = obj_secDatos.Encriptar();
                    //
                    //Tipo de Usuario
                    //se encripta
                    obj_secDatos = new Security(obj_Login.gsiTipoUsuario.ToString());
                    Session["iTipoUsuario"] = obj_secDatos.Encriptar();
                    //Nombre de Usuario
                    //se encripta
                    string NombreUsuario = obj_Login.gssNombre + " " + obj_Login.gssApePat + " " + obj_Login.gssApeMat;
                    obj_secDatos = new Security(NombreUsuario);
                    Session["NombreUsuario"] = obj_secDatos.Encriptar();

                    string a = Session["NombreUsuario"].ToString();

                    obj_secDatos = new Security(a);

                    string d = obj_secDatos.DesEncriptar();
                    //se redirecciona a la pagina de inicio
                    //Response.Redirect("Vista/Inicio/Inicio.aspx", false);
                    /**************************************************/
                }
                else if (obj_Login.gsiResultado == 4)
                {

                    //manda llamar la funcion de javaScript de abrir alerta
                    ClientScript.RegisterStartupScript(this.GetType(), "funcion", "<script>{$.notificacionMsj(" + obj_Login.gsiResultado + ", \" " + obj_Login.gssMensaje + "\");" +
                        "fn_abreDialogCambioPass('" + obj_Login.gssContenido + "','Cambio de contraseña','javaScript:alert(" + obj_Login.gsiIdUsuario + ")');$('#txtCambioPassword').val('');$('#atxtRepitePass').val('');}</script>");
                    /****************************************************************/

                    //atxtUsuarioCambioPassword.Text = obj_Login.gssUsuario;
                    //ahiddenId.Value = obj_Login.gsiAccion.ToString();
                }
                else
                {
                    //manda llamar la funcion de javaScript de abrir alerta
                    ClientScript.RegisterStartupScript(this.GetType(), "funcion", "<script>{$.notificacionMsj(" + obj_Login.gsiResultado + ", \" " + obj_Login.gssMensaje + "\");}</script>");
                    /****************************************************************/

                }
            }
            else
            {
                //manda llamar la funcion de javaScript de abrir alerta y redireccionar
                ClientScript.RegisterStartupScript(this.GetType(), "funcion", "<script>{$.notificacionMsj(" + obj_Login.gsiResultado + ", \" " + obj_Login.gssMensaje +"\");}</script>");
                /****************************************************************/
            }




        }
        /********/
        /*Inicio CATCH*/
        catch (Exception ex)
        {
            //manda llamar la funcion de javaScript de abrir alerta
            ClientScript.RegisterStartupScript(this.GetType(), "funcion", "<script>{ $.notificacionMsj(3, 'Lo sentimos, ha sucedido un error de tipo <b>EXEPCIÓN</b> comunícate con el departamento de TI, Error:" + ex.Message + "');}</script>");
            /****************************************************************/
        }
        /**********/

    }
    #endregion

    /*Funcion para iniciar sesion en el sistema*/
    #region Funcion para iniciar sesion en el sistema
    /// <summary>
    /// Funcion para iniciar sesion en el sistema
    /// </summary>
    /// <param name="sUsuario"></param>
    /// <param name="sPassword"></param>
    /// <returns></returns>
    [WebMethod]
    public static LoginDatos fn_inicioSesionSistema(string sUsuario,string sPassword)
    {
        //se declara objeto de rspuesta
        LoginDatos obj_login = new LoginDatos();
        //Inicio try
        try
        {
            //Instancias
            Security obj_secVariables;
            //se agrega el valor de los campos a los metodos set
            obj_login.gssUsuario = sUsuario;
            obj_login.gssPassword = sPassword;
            //*****
            //se manda llamar a la funcion de validar usuario
            obj_login.vValidaUsuario(obj_login);
            //se valida el resultado
            if (obj_login.gsiResultado == 1)
            {
                //se guardan las variables de sesión
                //Id de usuario
                obj_secVariables = new Security(obj_login.gsiIdUsuario.ToString());
                HttpContext.Current.Session["iIdUsuario"] = obj_secVariables.Encriptar();
                //Tipo de Usuario
                obj_secVariables = new Security(obj_login.gsiTipoUsuario.ToString());
                HttpContext.Current.Session["iTipoUsuario"] = obj_secVariables.Encriptar();
                //Nombre de Usuario
                //se encripta
                string NombreUsuario = obj_login.gssNombre + " " + obj_login.gssApePat + " " + obj_login.gssApeMat;
                obj_secVariables = new Security(NombreUsuario);
                HttpContext.Current.Session["NombreUsuario"] = obj_secVariables.Encriptar();

                //se redirecciona a la pagina de inicio
                //HttpContext.Current.Response.Redirect("Vista/Inicio/Inicio.aspx", false);
                /**************************************************/
            }
        }
        //inicio catch
        catch (Exception ex)
        {
            //se manda resultado
            obj_login.gsiResultado = 3;
            obj_login.gssMensaje = "Lo sentimos, ha sucedido un error de tipo <b>EXEPCIÓN</b> al momento de validar la expiraciòn de tu contraseña comunícate con el departamento de TI, Error:" + ex.Message + "'";
        }
        //se retorna respuesta
        return obj_login;
    }
    #endregion
    /*=========================================*/

    /*Funcion para buscar nombre de usuario*/
    #region Funcion para buscar nombre de usuario
    /// <summary>
    /// Funcion para buscar nombre de usuario
    /// </summary>
    /// <param name="sUsuario"></param>
    /// <param name="sPassword"></param>
    /// <returns></returns>
    [WebMethod]
    public static LoginDatos fn_validaDatosCambioPassCaptcha(string sUsuario,string sCaptcha)
    {
        //se declaran variables
        LoginDatos obj_login = new LoginDatos();
        //Inicio try
        try
        {
            //Instancias
            Security obj_secVariables;
            Conexion obj_conexion = new Conexion();
            //se crea query 
            string sQuery = "Select count(*) from tb_Usuarios where sUsuario ='"+sUsuario+"'";
            //se ejecuta query
            string[] srespuesta = obj_conexion.ejecutarConsultaRegistroSimple(sQuery);
            //se valida resultado
            if (srespuesta[0] == "1")
            {
                if (int.Parse(srespuesta[1]) > 0)
                {
                    /*se recupera el codigo captcha de session encriptado*/
                    string sCaptchaEncry = HttpContext.Current.Session["CaptchaCode"].ToString();
                    /****************************************************/

                    /*Instancia a la clase security y se manda como parametro el codigo encryptado*/
                    Security obj_SecCaptcha = new Security(sCaptchaEncry);
                    /***********************************************************************/

                    /*Se recupera el captcha de sesión desencriptado*/
                    string sCaptchaDes = obj_SecCaptcha.DesEncriptar();
                    /************************************************/

                    //se valida que sea el mismo captcha
                    if (sCaptcha == sCaptchaDes)
                    {
                        //se settean las variables
                        obj_login.gssUsuario = sUsuario;
                        obj_login.gssPassword = obj_login.fn_CreaNuevaPassword();
                        obj_login.gsiAccion = 1;
                        /*Se manda llamar al metodo de Cambiar contraseña*/
                        obj_login.vCambiaPassword(obj_login);
                        /*********************************************************/
                        //se valida el resultado del cambio de la contraseña
                        if (obj_login.gsiResultado == 1)
                        {
                            //se manda correo
                            obj_login.vGeneraCorreo(obj_login);
                        }
                    }
                    else
                    {
                        //se manda resultado
                        obj_login.gsiResultado = 2;
                        obj_login.gssMensaje = "El codigo captcha no es el correcto";
                    }
                }
                else
                {
                    //se manda resultado
                    obj_login.gsiResultado = 2;
                    obj_login.gssMensaje = "El usuario ingresado no existe.";
                }
            }
            else
            {
                //se manda resultado
                obj_login.gsiResultado = 3;
                obj_login.gssMensaje = "Error al recuperar usuari: "+srespuesta[0];
            }
        }
        //inicio catch
        catch (Exception ex)
        {
            //se manda resultado
            obj_login.gsiResultado = 3;
            obj_login.gssMensaje = "Error al recuperar usuario: "+ex.Message;
        }
        //se retorna respuesta
        return obj_login;
    }
    #endregion
    /*=========================================*/

    /*Funcion para cambiar contraseña desde inicio de sesion*/
    #region Funcion para cambiar contraseña desde inicio de sesion
    /// <summary>
    /// Funcion para cambiar contraseña desde inicio de sesion
    /// </summary>
    /// <param name="sUsuario"></param>
    /// <param name="sPassword"></param>
    /// <returns></returns>
    [WebMethod]
    public static LoginDatos fn_cambiaPassInicio(string sUsuario, string sPassword,int iAccion)
    { //***********************************************************/
        //Instancias 
        //Clase Conexion
        Conexion obj_Conexion = new Conexion();
        //Clase Login_Datos
        LoginDatos obj_login = new LoginDatos();
        //Clase de Security
        Security obj_secDatos;
        //Inicio TRY
        try
        {
            //se agrega el valor de los campos a los metodos set
            obj_login.gssUsuario = sUsuario;
            obj_login.gssPassword = sPassword;
            obj_login.gsiAccion = iAccion;
            //*****
            obj_login.vCambiaPassword(obj_login);

            if (obj_login.gsiResultado == 1)
            {
                obj_login.vValidaUsuario(obj_login);
                if (obj_login.gsiResultado == 1)
                {
                    //se guardan las Variables de sesion
                    //Id de usuario
                    //se encripta
                    obj_secDatos = new Security(obj_login.gsiIdUsuario.ToString());
                     HttpContext.Current.Session["iIdUsuario"] = obj_secDatos.Encriptar();
                    //
                    //Tipo de Usuario
                    //se encripta
                    obj_secDatos = new Security(obj_login.gsiTipoUsuario.ToString());
                     HttpContext.Current.Session["iTipoUsuario"] = obj_secDatos.Encriptar();
                    //Nombre de Usuario
                    //se encripta
                    string NombreUsuario = obj_login.gssNombre + " " + obj_login.gssApePat + " " + obj_login.gssApeMat;
                    obj_secDatos = new Security(NombreUsuario);
                    HttpContext.Current.Session["NombreUsuario"] = obj_secDatos.Encriptar();

                }
                else if (obj_login.gsiResultado == 4)
                {

                }
                else
                {
                }
            }
            else
            {
               
            }
        }
        /********/
        //inicio catch
        catch (Exception ex)
        {
            //se manda resultado
            obj_login.gsiResultado = 3;
            obj_login.gssMensaje = "Error al realizar cambio de contraseña: " + ex.Message;
        }
        //se retorna respuesta
        return obj_login;
    }
    #endregion
    /*=========================================*/

    
}