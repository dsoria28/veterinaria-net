using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de Actualizacion
/// </summary>
public class Actualizacion
{
    #region definición_variables
    /// <summary>
    /// Variables con método SET/GET
    /// </summary>
    public int giIdNotificacion { set; get; }

    public string gsFechaNotificacionInicio { set; get; }

    public string gsFechaNotificacionFin { set; get; }

    public string gsFechaNotificacionAccion { set; get; }

    public string gsDescripcion { set; get; }

    public string gsVersion { set; get; }

    public int giIdArchivo { set; get; }
    public string gsNombreArchivo { set; get; }
    public string gsURL { set; get; }

    /// <summary>
    /// Variables para retornar resultados
    /// </summary>
    /// iResultado: 1-->Exito, 2-->Alerta, 3-->Error
    public int iResultado { set; get; }
    public string sMensaje { set; get; }
    public string sContenido { set; get; }
    #endregion

    #region constructor
    public Actualizacion()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }
    #endregion

    #region getContenidoEncabezado
    public void getContenidoEncabezado(Actualizacion resActualizacion)
    {
        ///Arreglo para columnas
        string[] sColumnas = { "Sec", "Versión", "Fecha", "Descripción" };

        ///INICIA TABLA
        resActualizacion.sContenido = "<div class='table-responsive'><table id='tb_list_Actualizacion' class='table table-striped table-bordered table-hover' cellspacing='0' width='100%'>" +
                              "<thead style='display:table-row-group;'>" +
                              "<tr>";
        ///CICLO PARA RECORRER COLUMNAS
        foreach (string sColumna in sColumnas)
        {
            resActualizacion.sContenido += "<th>" + sColumna + "</th>";
        }///FIN CICLO PARA RECORRER COLUMNAS

        ///CIERRA ENCABEZADO
        resActualizacion.sContenido += "</tr></thead>";
        resActualizacion.sContenido += "<tfoot style='display: table-header-group;'><tr>";

        ///CICLO PARA MOSTRAR BUSCADDOR EN FOOTER
        foreach (string sColumna in sColumnas)
        {
            ///VERIFICA SI ESTA VACIO PARA NO MOSTRAR BUSCADOR
            if (sColumna.Equals(""))
            {
                ///SOLO MUESTRA REGISTRO VACIO
                resActualizacion.sContenido += "<td></td>";
            }///
            ///INICIO ELSE PARA MOSTRAR BUSCADOR
            else
            {
                ///MUESTRA BUSCADOR
                resActualizacion.sContenido += "<td><input type='text' style='width: 98%;' class='form-control input-sm' /></td>";
            }

        }///FIN CICLO PARA MOSTRAR BUSCADOR

        resActualizacion.sContenido += "</tr></tfoot><tbody></tbody></table></div>";
    }
    #endregion

    #region recuperaDatosManual
    public void recuperaDatosManual(Actualizacion resActualizacion)
    {
        try
        {
            int iExito = 1;
            ///Instancia a clase Conexión
            Conexion obj_conexion = new Conexion();
            ///query para recuperar los datos de la patente
            string sQuery = "SELECT iIdAvisosN, sNombreArchivo , '../../Documentos/NotificacionesGenerales/' + sNombreCarpeta  FROM ct_AvisosNotificacion WHERE iIdNotificacion =" + resActualizacion.giIdNotificacion;
            ///Lista para recuperar resultado
            List<string> slResultado;
            ///Ejecuta Query y asinga resultado
            slResultado = obj_conexion.ejecutarConsultaRegistroMultiples(sQuery);
            ///VERIFICA QUE SI EL RESULTADO ES CORRECTO 
            if (slResultado[0].Equals(iExito.ToString()))
            {
                ///Verifica si existen avisos y si éstos cuentan con archivos
                if (slResultado.Count > 1 && slResultado[2] != "")
                {
                    ///indica la ubicacion del archivo para extraer su extension
                    string sExt = System.IO.Path.GetExtension(HttpContext.Current.Server.MapPath(slResultado[3] + "/" + slResultado[2]));
                    sExt = sExt.ToLower();
                    ///verifica si es un archivo pdf
                    if (sExt == ".pdf")
                    {
                        ///Se asignan los valores a las variables de patente
                        resActualizacion.giIdArchivo = int.Parse(slResultado[1]);
                        resActualizacion.gsNombreArchivo = slResultado[2];
                        resActualizacion.gsURL = slResultado[3];
                        ///Se asigna valor de exito
                        resActualizacion.iResultado = 1;
                        resActualizacion.sMensaje = "Datos obtenidos con éxito.";
                    }
                    ///En caso de ser un archivo con otra extensión
                    else
                    {
                        resActualizacion.gsNombreArchivo = slResultado[2];
                        resActualizacion.gsURL = slResultado[3];
                        resActualizacion.iResultado = 4;
                    }
                }
                ///En caso de no existir un archivo para esa notificación
                else
                {
                    ///Se asigna el documento por defecto para no disponible
                    resActualizacion.giIdArchivo = 0;
                    resActualizacion.gsNombreArchivo = "unavailable.pdf";
                    resActualizacion.gsURL = "../../Documentos/NotificacionesGenerales/";
                    ///Se asigna valor de éxito
                    resActualizacion.iResultado = 1;
                    resActualizacion.sMensaje = "Datos obtenidos con éxito.";
                }
            }
        }///INICIO CATCH
        catch (Exception ex)
        {
            ///RETORNA ERROR DE REGISTROS en Catch
            resActualizacion.iResultado = 3;
            resActualizacion.sMensaje = "Error general al recuperar los datos: " + ex.Message;
        }///FIN CATCH
    }
    #endregion

    #region recuperaDatosActualizacion
    public void recuperaDatosActualizacion(Actualizacion resActualizacion)
    {
        try
        {
            int iExito = 1;
            ///Instancia a clase Conexión
            Conexion obj_conexion = new Conexion();
            ///query para recuperar los datos de la patente
            string sQuery = "SELECT sVersion, dFechaNotificacionAccion, dFechaNotificacionInicio, dFechaNotificacionFin, sDescripcion "
                + "FROM v_ListaActualizaciones WHERE iIdNotificacion =" + resActualizacion.giIdNotificacion;
            ///Lista para recuperar resultado
            List<string> slResultado;
            ///Ejecuta Query y asinga resultado
            slResultado = obj_conexion.ejecutarConsultaRegistroMultiples(sQuery);
            ///VERIFICA QUE SI EL RESULTADO ES CORRECTO 
            if (slResultado[0].Equals(iExito.ToString()))
            {
                if (slResultado.Count > 1)
                {
                    ///Se asignan los valores a las variables de actualizacion
                    resActualizacion.gsVersion = slResultado[1];
                    resActualizacion.gsFechaNotificacionAccion = slResultado[2];
                    resActualizacion.gsFechaNotificacionInicio = slResultado[3];
                    resActualizacion.gsFechaNotificacionFin = slResultado[4];
                    resActualizacion.gsDescripcion = slResultado[5];
                    ///Se asigna valor de exito
                    resActualizacion.iResultado = 1;
                    //resActualizacion.sMensaje = "Datos obtenidos con éxito.";
                }
                else
                {
                    ///Se asignan los valores vacios
                    resActualizacion.gsVersion = "";
                    resActualizacion.gsFechaNotificacionAccion = "";
                    resActualizacion.gsFechaNotificacionInicio = "";
                    resActualizacion.gsFechaNotificacionFin = "";
                    resActualizacion.gsDescripcion = "";
                    ///Se asigna valor de éxito
                    resActualizacion.iResultado = 1;
                    resActualizacion.sMensaje = "Datos obtenidos con éxito.";
                }
            }
        }///INICIO CATCH
        catch (Exception ex)
        {
            ///RETORNA ERROR DE REGISTROS en Catch
            resActualizacion.iResultado = 3;
            resActualizacion.sMensaje = "Error general al recuperar los datos: " + ex.Message;
        }///FIN CATCH
    }
    #endregion
}