using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de Permisos
/// </summary>
public class Permisos
{
    /// <summary>
    /// Declara variables
    /// </summary>
    public int gsResultado { set; get; }
    public String gsMensaje { set; get; }
    public String gsContenido { set; get; }
    /// <summary>
    /// VARIABLE PARA ID DE USUARIO
    /// </summary>
    private String gsIdUsuario { set; get; }
    private String gsIdAcceso { set; get; }
    private int iIdAcceso { set; get; }

    /// <summary>
    /// Declara constantes
    /// </summary>
    private int iEXITO = 1; ///RETORNA EXITO
    private int iALERTA = 2; ///RETORNA ALERTA
    private int iERROR=3; ///RETORNA ERROR

    private int iMODIFICA=1; ///ACCION PARA MODIFICAR REGISTROS
    private int iCOUNSULTA=2; ///ACCION PARA CONSULTAR REGISTROS
    private int iSIN_ACCESO=3; ///SIN ACCESO A SISTEMA

    /// <summary>
    /// CONSTRUCTOR
    /// </summary>
    /// <param name="iIdUser"></param>
    public Permisos(String iIdUser, String iIdAcceso) { this.gsIdUsuario = iIdUser; this.gsIdAcceso = iIdAcceso; }
    /// <summary>
    /// CONSTRUCTOR PARA PERMISOS
    /// </summary>
    /// <param name="iIdUser"></param>
    /// <param name="iIdAcceso"></param>
    public Permisos(String iIdUser, int iIdAcceso) { 
        this.gsIdUsuario = iIdUser; this.iIdAcceso = iIdAcceso;
        ///LLAMADA A METODO
        this.getValidaPermiso();
    }

    /// <summary>
    /// Método para recuperar acceso de Usuario
    /// </summary>
    /// <param name="res_per"></param>
    /// <returns></returns>
    public int getValidaAction(Permisos res_per)
    {
        ///INICIO TRY
        try
        {
            ///clase securiry para desencryptar id de usuario
            Security secIdUser = new Security(gsIdUsuario);
            ///clase securiry para desencryptar id de Submenu
            Security secIdSubMenu = new Security(gsIdAcceso);

            ///instancia a clase conexion
            Conexion conexion = new Conexion();
            ///QUERY PARA RECUPERAR TIPO DE ACCESO
            string sQuery = "select iTipoAcceso from tr_SubMenu_Usuarios where iIdUsuario="+secIdUser.DesEncriptar()+" and iIdSubMenu="+secIdSubMenu.DesEncriptar();
            ///VARIABLE PARA ALMACENAR RESULTADO
            string[] resResultado = conexion.ejecutarConsultaRegistroSimple(sQuery);
            ///verifica si se ejecuta con éxito
            if (resResultado[0].Equals("1"))
            {
                ///VERIFICA QUE TENGA RESULTADO
                if (!resResultado[1].Equals(""))
                {
                    res_per.gsResultado = iEXITO;///retorna resultado el resultado
                    res_per.gsMensaje = "Acceso recuperado con éxito.";///retorna mensaje

                    ///RETORNA EL ACCESO
                    return int.Parse(resResultado[1]);
                }///FIN VERIFICA RESULTADO
                 ///INICIO NO TIENE RESULTADO
                else {
                    res_per.gsResultado = iERROR;///retorna resultado de error
                    res_per.gsMensaje = "No se recuperó resultado.";

                    ///RETORNA SIN ACCESO
                    return iSIN_ACCESO;
                }///FIN NO TIENE RESULTADO
            }///fin verifica se ejecuta con exito
             ///inicio else error consulta
            else {
                res_per.gsResultado = iERROR;///retorna resultado de error
                res_per.gsMensaje = "Error recupear acceso: " + resResultado[0].ToString();///retorna mensaje

                ///RETORNA SIN ACCESO
                return iSIN_ACCESO;
            }///fin else error consulta

        }///FIN TRY
         ///INICIO CATCH
        catch (Exception ex)
        {
            res_per.gsResultado = iERROR;///retorna resultado de error
            res_per.gsMensaje = "Error general: "+ex.Message;///retorna mensaje

            ///RETORNA SIN ACCESO
            return iSIN_ACCESO;
        }///FIN CATCH
         
    }

    /// <summary>
    /// Metodo para recuperar si tiene acceso al permiso
    /// </summary>
    /// <param name="res_per"></param>
    /// <returns></returns>
    public void getValidaPermiso()
    {
        ///INICIO TRY
        try
        {
            ///clase securiry para desencryptar id de usuario
            Security secIdUser = new Security(gsIdUsuario);
            ///clase securiry para desencryptar id de Submenu
            Security secIdSubMenu = new Security(gsIdAcceso);

            ///instancia a clase conexion
            Conexion conexion = new Conexion();
            ///QUERY PARA RECUPERAR TIPO DE ACCESO
            string sQuery = "SELECT iIdTipoAcceso FROM TR_PERMISOSUSUARIOS WHERE iIdUsuario = " + secIdUser.DesEncriptar() + " and iIdPermisoUsuario = " + this.iIdAcceso;
            ///VARIABLE PARA ALMACENAR RESULTADO
            string[] resResultado = conexion.ejecutarConsultaRegistroSimple(sQuery);
            ///verifica si se ejecuta con éxito
            if (resResultado[0].Equals("1"))
            {
                ///VERIFICA QUE TENGA RESULTADO
                if (!resResultado[1].Equals(""))
                {
                    //res_per.gsResultado = iEXITO;///retorna resultado el resultado
                    //res_per.gsMensaje = "Acceso recuperado con éxito.";///retorna mensaje

                    ///RETORNA EL ACCESO
                    this.gsResultado  = int.Parse(resResultado[1]);
                }///FIN VERIFICA RESULTADO
                ///INICIO NO TIENE RESULTADO
                else
                {
                    ///RETORNA SIN ACCESO
                    this.gsResultado = iSIN_ACCESO;
                }///FIN NO TIENE RESULTADO
            }///fin verifica se ejecuta con exito
            ///inicio else error consulta
            else
            {
                ///RETORNA SIN ACCESO
                this.gsResultado = iSIN_ACCESO;
            }///fin else error consulta

        }///FIN TRY
        ///INICIO CATCH
        catch (Exception ex)
        {
            //res_per.gsResultado = iERROR;///retorna resultado de error
            //res_per.gsMensaje = "Error general: " + ex.Message;///retorna mensaje

            ///RETORNA SIN ACCESO
            this.gsResultado = iSIN_ACCESO;
        }///FIN CATCH

    }


}