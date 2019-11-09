using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de Avisos
/// </summary>
public class Avisos
{
    public int gsUsuario { set; get; }
    public string gsVersion { set; get; }
    /// <summary>
    /// Variables para retornar resultados
    /// </summary>
    /// iResultado: 1-->Exito, 2-->Alerta, 3-->Error
    public int iResultado { set; get; }
    public string sMensaje { set; get; }
    public string sContenido { set; get; }

    /// <summary>
    /// Constantes para errores y mensajes
    /// </summary>
    private int iEXITO = 1;
    private int iALERTA = 2;
    private int iERROR = 3;

	public Avisos()
	{
		//
		// TODO: Agregar aquí la lógica del constructor
		//
	}

    public void getEncabezadoAvisos(Avisos resTabla)
    {
        ///varialbe con inicializaion de tabla
        string sResultado = "<div class='table-responsive'><table id='htblAviso' class='table table-striped table-bordered table-hover' cellspacing='0' width='100%'>" +
                            "<thead style='display:table-row-group;'>" +
                            "<tr>";
        ///variable arreglo que deposita nombre de las columnas
        string[] sHeader = new string[] { "TIPO AVISO", "DESCRIPCIÓN", "" };

        ///ciclo para agregar columna al header
        foreach (string sColumna in sHeader)
        {
            sResultado += "<th>" + sColumna + "</th>";
        }
        ///se cierra el header
        sResultado += "</tr></thead>";
        ///se abre footer de la tabla
        sResultado += "<tfoot style='display: table-header-group;'><tr>";
        ///ciclo para agregar columna de busqueda a footer
        foreach (string sColumna in sHeader)
        {
            ///Si la columna es Acción o Eliminar se omite filtro de búsqueda
            if (sColumna != "TIPO AVISO" && sColumna != "DESCRIPCIÓN" && sColumna != "")
                sResultado += "<td><input type='text' style='width: 98%;' class='form-control input-sm' /></td>";
            else
                sResultado += "<td></td>";
        }
        ///se cierra la tabla
        sResultado += "</tr></tfoot><tbody></tbody></table></div>";
        ///Se asigna el resultado a la variable sContenido
        resTabla.sContenido = sResultado;
        //retorno de tabla completa
    }

    /// <summary>
    /// MÉTODO PARA GENERAR DETALLE DE AVISOS SIN VER
    /// </summary>
    /// <param name="aviso"></param>
    #region getGeneraListadoAvisos
    public void getGeneraListadoAvisos(Avisos aviso, int iTipo) {
        int iEXITO = 1;
        try {
            ///INSTANCIA A LA CLASE CONEXIÓN
            Conexion conn = new Conexion();
            string dFECHA_ACTUAL = DateTime.Now.ToString("yyyy-MM-dd");
            ///QUERY PARA RECUPERAR DATOS
            string sQuery = "";
            if (iTipo == 1)
            {
                sQuery = "SELECT iIdAviso, iIdUsuario, ctn.sDescripcion as sNombreAviso, sDescripcionAviso, sNombreArchivo, sNombreCarpeta, iVisto, iVistoSistema, sVersion, tng.iIdNotificacion, sNombreNotificacion " +
                                 "FROM tb_Avisos tba JOIN ct_AvisosNotificacion cta ON cta.iIdNotificacion = tba.iIdNotificacion " +
                                 "JOIN tb_NotificacionesGeneral tng On tng.iIdNotificacion = cta.iIdNotificacion JOIN cc_TipoAviso cca " +
                                 "ON cca.iIdTipoAviso = tng.iIdTipoAviso JOIN cc_TipoAvisosNotificaciones ctn ON ctn.iIdTipoAvisoN = cta.iIdTipoAvisoN WHERE ISNULL(iVisto,0) NOT IN(1) AND iIdUsuario = " + aviso.gsUsuario;
            }
            else {
                sQuery = "SELECT iIdAviso, iIdUsuario, ctn.sDescripcion as sNombreAviso, sDescripcionAviso, sNombreArchivo, sNombreCarpeta, iVisto, iVistoSistema, sVersion, tng.iIdNotificacion, sNombreNotificacion " +
                                 "FROM tb_Avisos tba JOIN ct_AvisosNotificacion cta ON cta.iIdNotificacion = tba.iIdNotificacion " +
                                 "JOIN tb_NotificacionesGeneral tng On tng.iIdNotificacion = cta.iIdNotificacion JOIN cc_TipoAviso cca " +
                                 "ON cca.iIdTipoAviso = tng.iIdTipoAviso JOIN cc_TipoAvisosNotificaciones ctn ON ctn.iIdTipoAvisoN = cta.iIdTipoAvisoN WHERE '" + dFECHA_ACTUAL + " 00:00:00' BETWEEN dFechaInicio AND dFechaFin AND iIdUsuario = " + aviso.gsUsuario;
            }
            ///
            DataSet dtsAviso = conn.ejecutarConsultaRegistroMultiplesDataSet(sQuery, "aviso");
            string sRuta = ""; string sCampoAnterior = ""; int iContador = 0;
            string sTabla = ""; int iTotal = 0;
            ///
            //aviso.sContenido += "<table id='htblDetalleAviso' class='table table-striped table-bordered table-hover' cellspacing='0' width='100%'>";
            //aviso.sContenido += "<thead><tr><th>TIPO AVISO</th><th>DESCRIPCIÓN</th><th></th></thead>";
            //aviso.sContenido += "<tbody>";
            ///VERIFICA EXISTENCIA DE DATOS
            if (dtsAviso.Tables["aviso"].Rows.Count > 0)
            {
                iTotal = dtsAviso.Tables["aviso"].Rows.Count;
                ///CICLO PARA RECORRER REGISTROS
                foreach (DataRow registro in dtsAviso.Tables["aviso"].Rows)
                {
                    ///
                    sRuta = "../../Documentos/NotificacionesGenerales/" + registro["sNombreCarpeta"].ToString() + "/" + registro["sNombreArchivo"].ToString();
                    ///INICIA CONSTRUCCIÓN ACCORDION
                    if (iContador == 0) 
                        sContenido += "<div class='panel-group' id='accordion'>";
                    if ((sCampoAnterior != registro["iIdNotificacion"].ToString()) || iContador == 0)
                    {
                        if (iContador != 0)
                        {
                            sContenido += "</tbody></table>";
                            sContenido += "</div></div>";
                            sContenido += "</div></div>";
                            ///FIN CONTENIDO
                            sContenido += "</div>";
                            ///FIN DIVISIÓN
                            sContenido += "</div>";
                        }
                        sTabla = "";
                        ///INICIA DIVISIÓN
                        sContenido += "<div class='panel panel-default' style='border-bottom:2px solid #1d6688;'>";
                        ///INICA HEADER
                        sContenido += "<div class='panel-heading' data-toggle='collapse' data-target='#collapse" + registro["iIdNotificacion"].ToString() + "' style='padding:5px 5px;background-color:#FFF; border: 1px solid #1d6688 !important; cursor:pointer;'>";
                        sContenido += "<div class='col-xs-11' style='display:inline;'>";
                        sContenido += "<h4 class='panel-title' style='vertical-align: middle;display:inline;'>" + registro["sNombreNotificacion"].ToString() + "</h4>";
                        sContenido += "</div>";
                        sContenido += "<div style='display:inline;padding-left:5%;'>";
                        sContenido += "<span class='glyphicon glyphicon-triangle-bottom text-right'></span>";
                        sContenido += "</div>";
                        ///FIN HEADER
                        sContenido += "</div>";
                        ///INICIO CONTENIDO
                        sContenido += "<div id='collapse" + registro["iIdNotificacion"].ToString() + "' class='panel-collapse collapse' style='background-color:#f5f5f5'>";
                        sContenido += "<div class='panel-body' style='padding: 5px 0px;'>";
                        sContenido += "<div class='container-fluid'>";
                        string sVersion = (registro["sVersion"].ToString() == "") ? "N/A" : registro["sVersion"].ToString();
                        sContenido += "<div class='row'>" +
                                "<div class='col-xs-12 col-sm-12 col-md-12 col-lg-12 text-right'>" +
                                    "<label class='text-azul'>Versión: </label> <span>" + sVersion + "</span>" +
                                "</div>" +
                                "</div>";
                                sContenido += "<div class='row'>" +
                                        "<div class='col-xs-12 col-sm-12 col-md-12 col-lg-12 table-responsive'>";
                                ////
                                sContenido += "<table class='table table-striped table-bordered table-hover htblDetalleAviso' cellspacing='0' width='100%'>" +
                                           "<thead><tr><th>TIPO AVISO</th><th>DESCRIPCIÓN</th><th></th></thead>"+
                                           "<tbody>";
                                sContenido += "<tr>";
                                sContenido += "<td>" + registro["sNombreAviso"].ToString() + "</td>";
                                sContenido += "<td>" + registro["sDescripcionAviso"].ToString() + "</td>";
                                ///VERIFICA EXISTENCIA DE DOCUMENTO
                                if (registro["sNombreArchivo"].ToString() != "")
                                    sContenido += "<td><a class='glyphicon glyphicon-download icon_blue icono-15' href='" + sRuta + "' download></a></td>";
                                else
                                    sContenido += "<td></td>";
                                sContenido += "</tr>";
                        //sContenido += "</div>";
                        //sContenido += "</div>";
                        /////FIN CONTENIDO
                        //sContenido += "</div>";
                        /////FIN DIVISIÓN
                        //sContenido += "</div>";
                    }
                    else {
                        sContenido += "<tr>";
                        sContenido += "<td>" + registro["sNombreAviso"].ToString() + "</td>";
                        sContenido += "<td>" + registro["sDescripcionAviso"].ToString() + "</td>";
                        ///VERIFICA EXISTENCIA DE DOCUMENTO
                        if (registro["sNombreArchivo"].ToString() != "")
                            sContenido += "<td><a class='glyphicon glyphicon-download icon_blue icono-15' href='" + sRuta + "' download></a></td>";
                        else
                            sContenido += "<td></td>";
                        sContenido += "</tr>";
                    }
                    
                    //aviso.gsVersion = registro["sVersion"].ToString();
                    ////
                    //aviso.sContenido += "<tr>";
                    //aviso.sContenido += "<td>" + registro["sNombreAviso"].ToString() + "</td>";
                    //aviso.sContenido += "<td>" + registro["sDescripcionAviso"].ToString() + "</td>";
                    /////VERIFICA EXISTENCIA DE DOCUMENTO
                    //if (registro["sNombreArchivo"].ToString() != "")
                    //    aviso.sContenido += "<td><a class='glyphicon glyphicon-download icon_blue icono-15' href='" + sRuta + "' download></a></td>";
                    //else
                    //    aviso.sContenido += "<td></td>";
                    //aviso.sContenido += "</tr>";
                    
                    
                    if ((iContador + 1) == iTotal) {
                        sContenido += "</tbody></table>";
                        sContenido += "</div></div>";
                        sContenido += "</div></div>";
                        ///FIN CONTENIDO
                        sContenido += "</div>";
                        ///FIN DIVISIÓN
                        sContenido += "</div>";
                        ///FIN CONSTRUCCIÓN ACCORDION
                        sContenido += "</div>";
                    }
                    sCampoAnterior = registro["iIdNotificacion"].ToString();
                    iContador++;
                }
            }
            ///
            //aviso.sContenido += "</tbody>";
            //aviso.sContenido += "</table>";
            aviso.iResultado = 1;
        } ///BLOQUE CATCH
        catch (Exception ex) {
            ///ASIGNA VARIABLES DE ERROR
            aviso.iResultado = 0;
            aviso.sMensaje = "Error al recuperar detalle de avisos. " + ex.Message;
        }
    }
    #endregion

    /// <summary>
    /// MÉTODO PARA ACTUALIZAR ESTATUS DE VISTO EN AVISOS
    /// </summary>
    /// <param name="aviso"></param>
    /// <param name="iTipo"></param>
    #region getActualizarEstatusAviso
    public void getActualizarEstatusAviso(Avisos aviso, int iTipo) {
        int iEXITO = 1;
        try {
            ///INSTANCIA A LA CLASE CONEXIÓN
            Conexion conn = new Conexion();
            string dFECHA_ACTUAL = DateTime.Now.ToString("yyyy-MM-dd");
            ///QUERY PARA EJECUTAR ACTUALIZACIÓN
            string sQuery = "";
            if (iTipo == 1) 
                sQuery = "UPDATE tb_Avisos SET iVisto = 1, dFechaVisto = dbo.fn_GetDate() WHERE iIdUsuario = " + aviso.gsUsuario + " AND (iVisto IS NULL OR iVisto = 0)";
            else
                sQuery = "UPDATE tb_Avisos SET iVistoSistema = 1, dFechaVistoSistema = dbo.fn_GetDate() WHERE iIdUsuario = " + aviso.gsUsuario + " AND (iVistoSistema IS NULL OR iVistoSistema = 0) AND '" + dFECHA_ACTUAL + " 00:00:00' BETWEEN dFechaInicio AND dFechaFin";
            ///VARIABLE PARA ALMACENAR RESULTADO
            string sResultado = conn.ejecutarComando(sQuery);
            ///VERIFICA ÉXITO EN EJECUCIÓN
            if (sResultado == "1")
            {
                ///ASIGNA VARIABLES DE ÉXITO
                aviso.iResultado = iEXITO;
                aviso.sMensaje = "Aviso actualizado con éxito.";

            }
            else {
                ///ASIGNA VARIABLES DE ERROR
                aviso.iResultado = 3;
                aviso.sMensaje = "Error al gestionar avisos de usuario. " + sResultado;
            }
        } ///BLOQUE CATCH
        catch (Exception ex) {
            ///ASIGNA VARIABLES DE ERROR
            aviso.iResultado = 0;
            aviso.sMensaje = "Error general al gestionar avisos de usuario. " + ex.Message;
        }
    }
    #endregion
}