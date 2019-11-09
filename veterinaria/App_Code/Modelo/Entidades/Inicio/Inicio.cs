using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Descripción breve de Inicio
/// </summary>
public class Inicio
{
    /// <summary>
    /// MÉTODO CONSTRUCTOR POR DEFECTO
    /// </summary>
    #region método_constructor
    public Inicio() {
        
    }
    #endregion

    /// <summary>
    /// Variables con GET/SET
    /// </summary>
    #region declaración_varaibles
    public int iIdUsuario { set; get; }
    public int iIdPantallaInicio { set; get; }
    public int iIdPantallaFrecuente { set; get; }
    public int type { set; get; }
    public int iIdUsuarioAccion { set; get; }
    public int iTipoUsuario { get; set; }

    //Variables para retornar el resultado
    public int iResultado { set; get; }
    public string sMensaje { set; get; }
    public string sContenido { set; get; }
    #endregion

    /// <summary>
    /// Método para guardar la pantalla de inicio del usuario
    /// </summary>
    /// <param name="obj_inicio"></param>
    #region fn_guardar_pantalla_inicio
    public void fn_guardar_pantalla_inicio(Inicio obj_inicio){
        //Se instancia la clase conexión 
        Conexion obj_conexion = new Conexion();
        //Nombre del procedimiento almacenado
        string sRes = obj_conexion.generarSP("pa_GuardarPantallaInicio", 0);
        if (sRes == "1")
        {
            try
            {
                //Se pasan los parametros del SP
                obj_conexion.agregarParametroSP("@iIdUsuario", SqlDbType.Int, obj_inicio.iIdUsuario.ToString());
                obj_conexion.agregarParametroSP("@iIdPantallaInicio", SqlDbType.VarChar, obj_inicio.iIdPantallaInicio.ToString());
                //Se ejecuta el SP
                sRes = obj_conexion.ejecutarProc();
            }
            catch (Exception ex)
            {
                //Se captura el error en caso de haber
                sRes = ex.Message;
            }
        }
        //Si el SP se ejecutó correctamente se retorna el mensaje de éxito
        if (sRes == "1")
        {
            obj_inicio.iResultado = 1;
            obj_inicio.sMensaje = "Pantalla de inicio asignada correctamente a usuario.";
        }
        //Si el SP no se ejecutó correctamente se retorna el mensaje de error
        else
        {
            obj_inicio.iResultado = 0;
            obj_inicio.sMensaje = sRes;
        }
    }
    #endregion

    /// <summary>
    /// Método para mostrar la pantalla de inicio 
    /// </summary>
    /// <param name="obj_inicio"></param>
    #region fn_mostrar_pantalla_inicio
    public void fn_mostrar_pantalla_inicio(Inicio obj_inicio)
    {
        //Se crea una variable en donde se almacenará el resultado
        string[] sResultado;
        //Se crea una instancia de la clase Conexion
        Conexion obj_conexion = new Conexion();
        //Se crea la consulta que obtendrá la pantalla que tiene configurada el usuario
        string sQuery = "SELECT iIdTipoPantalla FROM tb_Usuarios WHERE iIdUsuario=" + obj_inicio.iIdUsuario;
        sResultado = obj_conexion.ejecutarConsultaRegistroSimple(sQuery);
        obj_inicio.iResultado = int.Parse(sResultado[0]);
        obj_inicio.iIdPantallaInicio = int.Parse(sResultado[1]);
    }
    #endregion

    /// <summary>
    /// Método para mostrar las Pantallas Frecuentes 
    /// </summary>
    /// <param name="obj_inicio"></param>
    #region fn_vista_pantallas_frecuentes
    public void fn_vista_pantallas_frecuentes(Inicio obj_inicio)
    {
        //Se crea una variable en donde se almacenará el resultado
        string[] sResultado;
        //Se crea una instancia de la clase Conexion
        Conexion obj_conexion = new Conexion();
        //Se crea la variable lista
        List<Inicio> lstLista = new List<Inicio>();
        //Se cre un objeto dataset
        DataSet lstListaPantallasFrecuentes;
        //Variable para almacenar el id del empleado encriptado
        string sIdUsuarioEncrip;
        //Variable para almacenar el tipo del empleado encriptado
        string sIdTiporUsuario;
        //Se crea la consulta que obtendrá si este usuario ya tenga asignadas pantallas
        string sQuery = "SELECT COUNT(*) FROM tr_PantallaFrecuente_Usuario WHERE iIdUsuario=" + obj_inicio.iIdUsuario;
        //Se ejecuta la consulta de registros
        sResultado = obj_conexion.ejecutarConsultaRegistroSimple(sQuery);
        if (sResultado[1] != "0")
        {
            //Consulta para obtener datos de las pantallas frecuentes
            sQuery = "SELECT iIdSubMenu, iIdUsuario, iIdTipoUsuario, sNombreSubMenu, sIcono, sURL FROM v_PantallasFrecuentesUsuario WHERE iIdUsuario=" + obj_inicio.iIdUsuario;
            //Ejecutar consulta
            lstListaPantallasFrecuentes = obj_conexion.ejecutarConsultaRegistroMultiplesDataSet(sQuery, "lstPantallasFrecuentes");
            //Se da un valor a iResultado
            obj_inicio.iResultado = 1;
            if (lstListaPantallasFrecuentes.Tables["lstPantallasFrecuentes"].Rows.Count > 0)
            {
                obj_inicio.sContenido += "<div class='row'>";
                foreach (DataRow registro in lstListaPantallasFrecuentes.Tables["lstPantallasFrecuentes"].Rows)
                {
                    //Se encripta el id de tipo usuario
                    Security typeCtrl = new Security(registro["iIdSubMenu"].ToString());
                    sIdTiporUsuario = typeCtrl.Encriptar();
                    //Se encripta el id de usuario
                    Security idSec = new Security(registro["iIdUsuario"].ToString());
                    sIdUsuarioEncrip = idSec.Encriptar();
                    obj_inicio.sContenido += "<div class='col-xs-6 col-sm-3 col-md-2 col-lg-2' style='text-align:center'>";
                    obj_inicio.sContenido += "<div class='icon_inicio'>";
                    obj_inicio.sContenido += "<a style='text-decoration:none;' href='" + registro["sURL"].ToString() + "?sTypeCtrlAc=" + sIdTiporUsuario + "'><span class='" + registro["sIcono"].ToString() + "' aria-hidden='true' style='font-size:65px;color:#90C63D;margin-bottom:2px;'></span><span class='spn_title' data-toggle='tooltip' data-placement='bottom' data-original-title='" + registro["sNombreSubMenu"].ToString() + "'>" + registro["sNombreSubMenu"].ToString() + "</span></a>";
                    //obj_inicio.sContenido += "<p class='clear'><a href='" + registro["sURL"].ToString() + "?sTypeCtrlAc=" + sIdTiporUsuario + "'>" + registro["sNombreSubMenu"].ToString() + "</a></p>";
                    obj_inicio.sContenido += "</div>";
                    obj_inicio.sContenido += "</div>";
                }
                obj_inicio.sContenido += "</div>";
                if (int.Parse(sResultado[1]) <= 6)
                {
                    obj_inicio.sContenido += "<div class='col-lg-12' style='min-height:150px;'>";
                    obj_inicio.sContenido += "</div>";
                }
            }
        }
        else
        {
            //Si no tiene configuración se crea un div vacio
            obj_inicio.iResultado = 1;
            obj_inicio.sContenido = "<div class='container-fluid'>";
            obj_inicio.sContenido += "<div class='row' style='min-height:250px;'>";
            obj_inicio.sContenido += "</div>";
        }
        //Botón para seleccionar las pantallas frecuentes
        obj_inicio.sContenido += "<div class='row'>";
        obj_inicio.sContenido += "<div class='text-right'><a style='bottom:0;' href='#_' data-toggle='modal' data-target='#dialogSeleccionarPantallaFrecuente' onclick='javascript:fn_generar_lista_pantallas_frecuentes();'>Seleccionar pantalla inicio</a></div>";
        obj_inicio.sContenido += "</div>";
        obj_inicio.sContenido += "</div>";
    }
    #endregion

    /// <summary>
    /// Método para generar la estructura de la lista de pantallas frecuentes
    /// </summary>
    /// <param name="obj_inicio"></param>
    #region fn_generar_lista_pantallas_frecuentes
    public void fn_generar_lista_pantallas_frecuentes(Inicio obj_inicio)
    {
        ///varialbe con inicializaion de tabla
        string sResultado = "<div class='table-responsive'><table id='htblPantallasFrecuentes'  class='table table-striped table-bordered table-hover' cellspacing='0' width='100%'>" +
                            "<thead style='display:table-row-group;'>" +
                            "<tr>";
        ///variable arreglo que deposita nombre de las columnas
        string[] sHeader = new string[] { "Nombre pantalla", "Menú", "Eliminar" };
        ///ciclo para agregar columna al header
        foreach (string sColumna in sHeader)
        {
            sResultado += "<th>" + sColumna + "</th>";
        }
        //se cierra el header
        sResultado += "</tr></thead>";
        //se abre footer de la tabla
        sResultado += "<tfoot style='display: table-header-group;'><tr>";
        ///ciclo para agregar columna de busqueda a footer
        foreach (string sColumna in sHeader)
        {
            if (sColumna != "Eliminar")
                sResultado += "<td><input type='text' style='width: 90%;' class='form-control input-sm' /></td>";
            else
                sResultado += "<td></td>";
        }
        //se cierra la tabla
        sResultado += "</tr></tfoot><tbody></tbody></table></div>";
        obj_inicio.sContenido = sResultado;
        //retorno de tabla completa
    }
    #endregion

    /// <summary>
    /// Método para validar el agregar la pantalla frecuente
    /// </summary>
    /// <param name="obj_inicio"></param>
    #region fn_validar_agregar_pantalla_frecuente
    public void fn_validar_agregar_pantalla_frecuente(Inicio obj_inicio)
    {
        //Se crea una variable en donde se almacenará el resultado
        string[] sResultado;
        //Se crea una instancia de la clase Conexion
        Conexion obj_conexion = new Conexion();
        //Se crea la consulta que obtendrá si este cliente ya esta asignado a ese usuario
        string sQuery = "SELECT COUNT(*) FROM tr_PantallaFrecuente_Usuario WHERE iIdSubmenu=" + obj_inicio.iIdPantallaFrecuente + " and iIdUsuario=" + obj_inicio.iIdUsuario;
        sResultado = obj_conexion.ejecutarConsultaRegistroSimple(sQuery);
        //Se verifica que se ejecute la consulta de manera exitosa
        if (sResultado[0].ToString() == "1")
        {
            //Si el cliente ya esta asignado se retorna una alerta
            obj_inicio.iResultado = int.Parse(sResultado[0]);
            obj_inicio.sMensaje = sResultado[1];
            obj_inicio.sContenido = "La pantalla frecuente ya se encuentra agregada al usuario.";
        }
        else
        {
            //Se atrapa el error que arroje la consulta
            obj_inicio.iResultado = 0;
            obj_inicio.sMensaje = sResultado[0];
        }
    }
    #endregion

    /// <summary>
    /// Método para guardar o eliminar una pantalla frecuente
    /// </summary>
    /// <param name="obj_inicio"></param>
    #region fn_guardar_eliminar_pantalla_frecuente
    public void fn_guardar_eliminar_pantalla_frecuente(Inicio obj_inicio)
    {
        //Se instancia la clase conexión 
        Conexion obj_conexion = new Conexion();
        //Nombre del procedimiento almacenado
        string sRes = obj_conexion.generarSP("pa_GuardarEliminarPantallaFrecuente", 0);
        if (sRes == "1")
        {
            try
            {
                //Se pasan los parametros del SP
                obj_conexion.agregarParametroSP("@iIdPantallaFrecuente", SqlDbType.Int, obj_inicio.iIdPantallaFrecuente.ToString());
                obj_conexion.agregarParametroSP("@iIdUsuario", SqlDbType.Int, obj_inicio.iIdUsuario.ToString());
                obj_conexion.agregarParametroSP("@type", SqlDbType.Int, obj_inicio.type.ToString());
                obj_conexion.agregarParametroSP("@iIdUsuarioAccion", SqlDbType.Int, obj_inicio.iIdUsuarioAccion.ToString());
                //Se ejecuta el SP
                sRes = obj_conexion.ejecutarProc();
            }
            catch (Exception ex)
            {
                //Se captura el error en caso de haber
                sRes = ex.Message;
            }
        }
        //Si el SP se ejecutó correctamente se retorna el mensaje de éxito
        if (sRes == "1")
        {
            obj_inicio.iResultado = 1;
            if (type == 1)
                obj_inicio.sMensaje = "Pantalla frecuente agregada con éxito.";
            else
                obj_inicio.sMensaje = "Pantalla frecuente eliminada con éxito.";
        }
        //Si el SP no se ejecutó correctamente se retorna el mensaje de error
        else
        {
            obj_inicio.iResultado = 0;
            obj_inicio.sMensaje = sRes;
        }
    }
    #endregion

    /// <summary>
    /// Método para obtener tipo usuario
    /// </summary>
    /// <param name="obj_inicio"></param>
    #region fn_obtener_tipo_usuario
    public void fn_obtener_tipo_usuario(Inicio obj_inicio)
    {
        //Se instancia la clase conexión 
        Conexion obj_conexion = new Conexion();
        //sQuery para validar
        string sQuery = "SELECT tbu.iIdTipoUsuario FROM tb_Usuarios tbu WHERE tbu.iIdUsuario=" + obj_inicio.iIdUsuario;
        string[] sRes = obj_conexion.ejecutarConsultaRegistroSimple(sQuery);
        //Se retorna el sResultado 
        obj_inicio.iTipoUsuario = int.Parse(sRes[1]);
    }
    #endregion
}