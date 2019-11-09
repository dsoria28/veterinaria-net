using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de Header
/// </summary>
public class Header
{
    //VARIABLES DEL OBJETO
    #region VARIABLES DEL OBJETO
    public int gsiIdUsuario { get; set; }
    public string gssFecha { get; set; }
    public int gsiHoy { get; set; }
    public bool gsiIsRestriccion { get; set; }
    public bool gsiIsAlerta { get; set; }
    public string gssIcono { get; set; }
    #endregion
    //========

    //VARIABLES DE RESPUESTA
    #region VARIABLES DE RESPUESTA
    public string gssMensaje { get; set; }
    public int gsiResultado { get; set; }
    public string gssContenido { get; set; }
    public int gsiNumeroRegistros { get; set; }
    public string[] gssArrayString { get; set; }
    public int[] gssArrayInt { get; set; }
    public string gssTitle { get; set; }
    #endregion
    //=======

    //METODO PARA RECUPERAR NUMERO DE NOTIFICACIONES POR USUARIO
    #region METODO PARA RECUPERAR NUMERO DE NOTIFICACIONES POR USUARIO
    /// <summary>
    /// METODO PARA RECUPERAR NUMERO DE NOTIFICACIONES POR USUARIO
    /// </summary>
    public void fn_recuperaNumeroNotificacionesUsuario(Header obj_head)
    {
        //INICIO TRY
        try
        {
            //Instancias
            Conexion obj_conexion = new Conexion();
            //se declaran variables
            string[] sDatos = new string[2];
            //se crea query
            string sQuery = "Select count(*) as iNumNotificaciones from tb_NotificacionUsuario where iIdUsuario = "+obj_head.gsiIdUsuario+" and Cast(dFechaAccion as date) = Cast(dbo.fn_GetDate() as date) and (iVisto = 0 or iVisto is null)";
            //se ejecuta query
            sDatos = obj_conexion.ejecutarConsultaRegistroSimple(sQuery);
            //se valida resultado
            if (sDatos[0].Equals("1"))
            {
                //se verifica si hay datos
                if (sDatos.Length > 1)
                {
                    //se recuperan datos
                    obj_head.gsiNumeroRegistros = !string.IsNullOrEmpty(sDatos[1]) ? int.Parse(sDatos[1]) : 0;
                    //se manda mensaje
                    obj_head.gsiResultado = 1;
                    obj_head.gssMensaje = "Número de notificaciones recuperadas con éxito";
                }
                else
                {
                    //se manda mensaje
                    obj_head.gsiResultado = 2;
                    obj_head.gssMensaje = "No se encontro información del usuario";
                }
            }
            else
            {
                //se manda mensaje
                obj_head.gsiResultado = 3;
                obj_head.gssMensaje = "Error al recuperar Número de notificaciones:"+sDatos[0];
            }
        }
        //FIN TRY
        //INICIO CATCH
        catch (Exception ex)
        {
            //se manda mensaje
            obj_head.gsiResultado = 3;
            obj_head.gssMensaje = "Error al recuperar Número de notificaciones:"+ex.Message;
        }
        //FIN CATCH
    }
    #endregion
    //********************

    //METODO PARA RECUPERAR NUMERO DE Pendientes POR USUARIO
    #region METODO PARA RECUPERAR NUMERO DE Pendientes POR USUARIO
    /// <summary>
    /// METODO PARA RECUPERAR NUMERO DE Pendientes POR USUARIO
    /// </summary>
    public void fn_recuperaNumeroPendientesUsuario(Header obj_head)
    {
        //INICIO TRY
        try
        {
            //Instancias
            Conexion obj_conexion = new Conexion();
            //se declaran variables
            string[] sDatos = new string[2];
            //se crea query
            string sQuery = "Select count(*) from tb_ActividadesPendientesUsuario where iIdUsuario = " + obj_head.gsiIdUsuario + " and iIdEstatus = 1";
            //se ejecuta query
            sDatos = obj_conexion.ejecutarConsultaRegistroSimple(sQuery);
            //se valida resultado
            if (sDatos[0].Equals("1"))
            {
                //se verifica si hay datos
                if (sDatos.Length > 1)
                {
                    //se recuperan datos
                    obj_head.gsiNumeroRegistros = !string.IsNullOrEmpty(sDatos[1]) ? int.Parse(sDatos[1]) : 0;
                    //se manda mensaje
                    obj_head.gsiResultado = 1;
                    obj_head.gssMensaje = "Número de Pendientes recuperadas con éxito";
                }
                else
                {
                    //se manda mensaje
                    obj_head.gsiResultado = 2;
                    obj_head.gssMensaje = "No se encontro información del usuario";
                }
            }
            else
            {
                //se manda mensaje
                obj_head.gsiResultado = 3;
                obj_head.gssMensaje = "Error al recuperar Número de Pendientes:" + sDatos[0];
            }
        }
        //FIN TRY
        //INICIO CATCH
        catch (Exception ex)
        {
            //se manda mensaje
            obj_head.gsiResultado = 3;
            obj_head.gssMensaje = "Error al recuperar Número de Pendientes:" + ex.Message;
        }
        //FIN CATCH
    }
    #endregion
    //********************

    //METODO PARA RECUPERAR NOTIFICACIONES POR USUARIO
    #region METODO PARA RECUPERAR NOTIFICACIONES POR USUARIO
    /// <summary>
    /// METODO PARA RECUPERAR NOTIFICACIONES POR USUARIO
    /// </summary>
    public void fn_recuperaNotificacionesUsuario(Header obj_head)
    {
        //INICIO TRY
        try
        {
            //Instancias
            Conexion obj_conexion = new Conexion();
            //se declaran variables
            DataTable sDatos = new DataTable();
            string sCuerpo = "";
            string sElemento = "";
            string sPrioridad = "";
            string sNombrePrioridad = "";
            string sClassVisto = "";
            int iResultados = 0;
            //se crea query
            string sQuery = "Select top 50* from v_listaNotificacionesUsuario as notifi " +
                            " where iIdUsuario = " + obj_head.gsiIdUsuario + " and CAST(dFechaAccion as date) = CAST(dbo.fn_Getdate() AS DATE)" +
                            " ORDER BY notifi.dFechaAccion desc";
            //se ejecuta query
            sDatos = obj_conexion.ejecutarConsultaRegistroMultiplesData(sQuery);
            //se valida resultado
            if (!sDatos.HasErrors)
            {
                //se verifica si hay datos
                if (sDatos.Rows.Count > 0)
                {
                    foreach(DataRow dtaRow in sDatos.Rows)
                    {
                        //se recuperan variables
                        sPrioridad = !string.IsNullOrEmpty(dtaRow["sColor"].ToString()) ? dtaRow["sColor"].ToString() : "form-circulo form-circulo-blanco form-circulo-default";
                        sNombrePrioridad = !string.IsNullOrEmpty(dtaRow["sNombrePrioridad"].ToString()) ? dtaRow["sNombrePrioridad"].ToString() : "Normal";
                        sClassVisto = !string.IsNullOrEmpty(dtaRow["iVisto"].ToString()) ? bool.Parse(dtaRow["iVisto"].ToString()) ? "icono-azul" : "icono-verde" : "icono-verde";
                        //se crea elemento
                        sElemento = "<li>";
                        //Inicio Prioridad
                        sElemento += "<div class='col-md-3 col-sm-3 col-xs-3'>";
                        sElemento += "<div class='notify-info'>";
                        sElemento += "<div class='" + sPrioridad + "' style='margin: 7px auto auto auto;' data-toggle='tooltip' title='"+sNombrePrioridad+"' data-placement='right'></div>";
                        sElemento += "</div>";
                        //fin Prioridad
                        sElemento += "</div>";
                        sElemento += "<div class='col-md-9 col-sm-9 col-xs-9 pd-l0'>";
                        sElemento += "<span>" + dtaRow["sTitulo"].ToString() + "</span> <a href='#_' class='rIcon'><i class='fa fa-dot-circle-o " + sClassVisto + "'></i></a>";
                        sElemento += "<p>"+dtaRow["sContenido"].ToString()+"</p>";
                        sElemento += "<p class='time'>" + dtaRow["sFechaAccion"].ToString() + "</p>";
                        sElemento += "</div>";
                        //se termina elemnto
                        sElemento += "</li>";
                        //se agrega elemnto 
                        sCuerpo += sElemento;
                        //se incrementan los resultados 
                        iResultados++;
                    }
                    //se manda mensaje
                    /*obj_head.gsiResultado = 1;
                    obj_head.gssMensaje = "Notificaciones recuperadas con éxito";*/
                    //se verifica su hubo resultados
                    if (iResultados > 0)
                    {
                        //se actualizan notificaciones
                        obj_head.fn_ActualizarNotificaciones(obj_head);
                    }
                    else
                    {
                        //se agregan resultados
                        obj_head.gsiResultado = 1;
                        obj_head.gssMensaje = "Notificaciones recuperadas con exito";
                    }
                    //se mandan notificaciones
                    obj_head.gssContenido = sCuerpo;
                }
                else
                {
                    //se manda mensaje
                    obj_head.gsiResultado = 2;
                    obj_head.gssMensaje = "Aquí se muestran las notificaciones.";
                    obj_head.gssContenido = "<li>"+
                                                "<div class='col-xs-12 pd-l0' style='text-align:center;font-size: 15px;'><i class='fa fa-smile-o'>&nbsp;</i>Sin notificaciones</div>" +
                                            "</li>";
                }
            }
            else
            {
                //se manda mensaje
                obj_head.gsiResultado = 3;
                obj_head.gssMensaje = "Se ha producido un error inesperado. Vuelve a intentarlo más tarde.";
                obj_head.gssTitle = "Error datatable";
            }
        }
        //FIN TRY
        //INICIO CATCH
        catch (Exception ex)
        {
            //se manda mensaje
            obj_head.gsiResultado = 3;
            obj_head.gssMensaje = "Se ha producido un error inesperado. Vuelve a intentarlo más tarde.";
            obj_head.gssTitle = ex.Message;
        }
        //FIN CATCH
    }
    #endregion
    //********************

    /*Metodo para Actualizar Notificaciones*/
    #region Metodo para Actualizar Notificaciones
    /// <summary>
    /// Metodo para Actualizar Notificaciones
    /// </summary>
    public void fn_ActualizarNotificaciones(Header obj_head)
    {
        //INICIO TRY
        try
        {
            //INSTANCIAS
            Security obj_secVariables;
            Conexion obj_conexion = new Conexion();

            //se crea query 
            string sQuery = "Exec [pa_ActualizaNotificacionesUsuario] " + obj_head.gsiIdUsuario;
            //se ejecuta query 
            List<string> lstDatos = new List<string>();
            lstDatos = obj_conexion.ejecutarConsultaRegistroMultiples(sQuery);
            //se valida resultado del query
            if (lstDatos[0].Equals("1"))
            {
                //se verifica que haya resultados
                if (lstDatos.Count > 1)
                {
                    //se agregan resultado
                    obj_head.gsiResultado = int.Parse(lstDatos[1]);
                    obj_head.gssMensaje = lstDatos[2];
                }
                else
                {
                    //se manda mensaje
                    obj_head.gsiResultado = 3;
                    obj_head.gssMensaje = "Se ha producido un error inesperado. Vuelve a intentarlo más tarde.";
                    obj_head.gssTitle = "Sin resultados al actualizar notificaciones";
                }
            }
            else
            {
                //se manda mensaje
                obj_head.gsiResultado = 3;
                obj_head.gssMensaje = "Se ha producido un error inesperado. Vuelve a intentarlo más tarde.";
                obj_head.gssTitle = "Error al Actualizar Notificaciones: :" + lstDatos[0];
            }

        }
        //FIN TRY
        //INICIO CATCH
        catch (Exception ex)
        {
            //se manda mensaje
            obj_head.gsiResultado = 3;
            obj_head.gssMensaje = "Se ha producido un error inesperado. Vuelve a intentarlo más tarde.";
            obj_head.gssTitle = "Error al Actualizar Notificaciones: " +ex.Message;
        }
        //FIN CATCH
    }
    #endregion
    /*========================================*/

    //METODO PARA RECUPERAR NOTIFICACIONES Historial POR USUARIO
    #region METODO PARA RECUPERAR NOTIFICACIONES Historial POR USUARIO
    /// <summary>
    /// METODO PARA RECUPERAR NOTIFICACIONES Historial POR USUARIO
    /// </summary>
    public void fn_recuperaNotificacionesHistorialUsuario(Header obj_head)
    {
        //INICIO TRY
        try
        {
            //Instancias
            Conexion obj_conexion = new Conexion();
            //se declaran variables
            DataTable sDatos = new DataTable();
            string sCuerpoHoy = ""; ;
            string sCuerpoAnteriores = "";
            string sCuerpoTotal = "";
            string sElemento = "";
            string sPrioridad = "";
            string sNombrePrioridad = "";
            string sClassVisto = "";
            int iResultadosHoy = 0;
            int iResultadosAnteriores = 0;
            int iHoy = 0;
            string sWhere = "";
            //se crea clausula where 
            if (obj_head.gsiHoy == 1)
            {
                sWhere = "and iHoy = 1";

            }
            else
            {
                sWhere = "and cast(dFechaAccion as date) = convert(date,'"+obj_head.gssFecha+"',103)";
            }
            //se crea query
            string sQuery = "Select * from v_listaNotificacionesUsuario as notifi " +
                            " where iIdUsuario = " + obj_head.gsiIdUsuario + " " + sWhere +
                            " ORDER BY notifi.dFechaAccion desc";
            //se ejecuta query
            sDatos = obj_conexion.ejecutarConsultaRegistroMultiplesData(sQuery);
            //se valida resultado
            if (!sDatos.HasErrors)
            {
                //se verifica si hay datos
                if (sDatos.Rows.Count > 0)
                {
                    sCuerpoHoy = "";
                    sCuerpoAnteriores = ""; ;
                    foreach (DataRow dtaRow in sDatos.Rows)
                    {
                        //se recuperan variables
                        sPrioridad = !string.IsNullOrEmpty(dtaRow["sColor"].ToString()) ? dtaRow["sColor"].ToString() : "form-circulo form-circulo-blanco form-circulo-default";
                        sNombrePrioridad = !string.IsNullOrEmpty(dtaRow["sNombrePrioridad"].ToString()) ? dtaRow["sNombrePrioridad"].ToString() : "Normal";
                        sClassVisto = !string.IsNullOrEmpty(dtaRow["iVisto"].ToString()) ? bool.Parse(dtaRow["iVisto"].ToString()) ? "icono-azul" : "icono-verde" : "icono-verde";
                        iHoy = int.Parse(dtaRow["iHoy"].ToString());
                        //se crea elemento
                        sElemento = "<li>";
                        //Inicio Prioridad
                        sElemento += "<div class='col-xs-12 col-sm-1'>";
                        sElemento += "<div class='cont-info'>";
                        sElemento += "<div class='" + sPrioridad + "' style='margin:7px auto 6px auto;' data-toggle='tooltip' title='" + sNombrePrioridad + "' data-placement='right'></div>";
                        sElemento += "</div>";
                        //fin Prioridad
                        sElemento += "</div>";
                        sElemento += "<div class='col-xs-11 pd-l0'>";
                        sElemento += "<span class='titulo'>" + dtaRow["sTitulo"].ToString() + "</span> <a href='#_' class='rIcon'><i class='fa fa-dot-circle-o " + sClassVisto + "'></i></a>";
                        sElemento += "<p class='contenido'>" + dtaRow["sContenido"].ToString() + "</p>";
                        sElemento += "<p class='time'>" + dtaRow["sFechaAccion"].ToString() + "</p>";
                        sElemento += "</div>";
                        //se termina elemnto
                        sElemento += "</li>";
                        //se agrega elemnto 
                        if(iHoy == 1){
                            sCuerpoHoy += sElemento;
                            //se incrementan los resultados 
                            iResultadosHoy++;
                        }
                        else{
                            sCuerpoAnteriores += sElemento;
                            //se incrementan los resultados 
                            iResultadosAnteriores++;
                        }
                        obj_head.gsiNumeroRegistros++;
                        sCuerpoTotal += sElemento;
                    }
                    if (iResultadosHoy == 0)
                    {
                        sCuerpoHoy = "<div class='col-xs-12 pd-l0' style='text-align:center;font-size: 15px;'><i class='fa fa-smile-o'>&nbsp;</i>Sin Notificaciones en Historial</div>";
                    } 
                    if (iResultadosAnteriores == 0)
                    {
                        sCuerpoAnteriores = "<div class='col-xs-12 pd-l0' style='text-align:center;font-size: 15px;'><i class='fa fa-smile-o'>&nbsp;</i>Sin Notificaciones en Historial</div>";
                    }
                    if (obj_head.gsiNumeroRegistros == 0)
                    {
                        sCuerpoTotal = "<div class='col-xs-12 pd-l0' style='text-align:center;font-size: 15px;'><i class='fa fa-smile-o'>&nbsp;</i>Sin Notificaciones en Historial</div>";
                    }
                    //se termina de construir listado
                    //sCuerpoHoy += "</div></ul>";
                    //sCuerpoAnteriores += "</div></ul>";
                    //se manda mensaje
                    obj_head.gsiResultado = 1;
                    obj_head.gssMensaje = "Notificaciones recuperadas con éxito";
                    obj_head.gssArrayString = new string[] { sCuerpoHoy, sCuerpoAnteriores ,sCuerpoTotal};
                    obj_head.gssArrayInt = new int[] { iResultadosHoy, iResultadosAnteriores };
                }
                else
                {
                    //se manda mensaje
                    obj_head.gsiResultado = 2;
                    obj_head.gssMensaje = "Sin notificaciones pendientes.";
                    obj_head.gssContenido =  "<div class='col-xs-12 pd-l0' style='text-align:center;font-size: 15px;'><i class='fa fa-smile-o'>&nbsp;</i>Sin Notificaciones en Historial</div>";
                }
            }
            else
            {
                //se manda mensaje
                obj_head.gsiResultado = 3;
                obj_head.gssMensaje = "Se ha producido un error inesperado. Vuelve a intentarlo más tarde.";
                obj_head.gssTitle = "Error datatable";
            }
        }
        //FIN TRY
        //INICIO CATCH
        catch (Exception ex)
        {
            //se manda mensaje
            obj_head.gsiResultado = 3;
            obj_head.gssMensaje = "Se ha producido un error inesperado. Vuelve a intentarlo más tarde.";
            obj_head.gssTitle = ex.Message;
        }
        //FIN CATCH
    }
    #endregion
    //********************

    //METODO PARA RECUPERAR ALERTA/RESTRICCION FLUJO AEREO
    #region METODO PARA RECUPERAR ALERTA/RESTRICCION FLUJO AEREO
    /// <summary>
    /// METODO PARA RECUPERAR ALERTA/RESTRICCION FLUJO AEREO
    /// </summary>
    public void fn_recuperaAlertaRestriccionFlujo(Header obj_head,int[] iIdFlujos)
    {
        //INICIO TRY
        try
        {
            //Instancias
            Conexion obj_conexion = new Conexion();
            //se declaran variables
            DataTable sDatos = new DataTable();
            string sInFlujos = "";
            bool iError = true;
            bool iRestriccion = false;
            bool iAlerta = false;
            int iTiempo = 0;
            int iIdOficina = 0;
            string sQuery = "";
            string[] sRespuesta = new string[2];
            //SE RECUPERA ACCESO A CLIENTE 
            string sQueryTipo = "select iIdTipoUsuario from tb_Usuarios where iIdUsuario = " + obj_head.gsiIdUsuario;
            string[] sResTipo = obj_conexion.ejecutarConsultaRegistroSimple(sQueryTipo);
            string sWhereCliente = "";
            if (sResTipo[1] == "1")
            {
                sWhereCliente = " iIdCliente in (select iIdCliente " +
                                                " from v_Cliente_Oficina" +
                                                " where  iIdOficina = {0})";
            }
            else
            {
                sWhereCliente = " iIdCliente in (select iIdCliente" +
                                                " from tr_Clientes_Usuarios where iIdUsuario = " + obj_head.gsiIdUsuario + ")";
            }
            //================================>
            #region INICIO VALIDACION RESTRICCIONES
            //se crea clausula In
            for (int i = 0; i < iIdFlujos.Length; i++)
            {
                //se inicializan variables
                iError = true;
                iRestriccion = false;
                //===============================>
                //se crea querys
                sQuery = "select iTiempo,iIdOficina from tc_AlertasFlujoAereo where iIdOficina in(SELECT trou.iIdOficina FROM tr_Oficina_Usuario trou WHERE trou.iIdUsuario="+obj_head.gsiIdUsuario+") and iIdFlujo = " + iIdFlujos[i] + " and iIdTipoAlerta = 2";
                //se ejecuta query
                sDatos = obj_conexion.ejecutarConsultaRegistroMultiplesData(sQuery);
                //se valida resultado
                if (!sDatos.HasErrors)
                {
                    //se verifica si hay datos
                    if (sDatos.Rows.Count > 0)
                    {
                        //se reccorren filas 
                        foreach (DataRow obj_fila in sDatos.Rows)
                        {
                            //se inicializan variables
                            iTiempo = 0;
                            iIdOficina = 0;
                            //se recuperan variables 
                            iTiempo = !string.IsNullOrEmpty(obj_fila["iTiempo"].ToString()) ? int.Parse(obj_fila["iTiempo"].ToString()) : 0;
                            iIdOficina = !string.IsNullOrEmpty(obj_fila["iIdOficina"].ToString()) ? int.Parse(obj_fila["iIdOficina"].ToString()) : 0;
                            //se verifica si es diferente de 0
                            if (iTiempo != 0)
                            {
                                
                                //se crea query para recuperar las guias en restriccion
                                sQuery = "Select Count(*) " +
                                        " from taer_Guia as guia left join " +
                                        " taer_DetalleGuia as detalle on guia.iIdGuia = detalle.iIdGuia " +
                                        " where datediff(minute,dFechaPendiente,dbo.fn_GetDate()) >= "+iTiempo+" and iPendiente = 1 and iIdOficina = " + iIdOficina + " and iIdFlujo = " + iIdFlujos[i] + " and " + string.Format(sWhereCliente, iIdOficina);
                                //se ejecuta query 
                                sRespuesta = obj_conexion.ejecutarConsultaRegistroSimple(sQuery);
                                //se verifica si se ejecuto correctamente la query
                                if (sRespuesta[0].Equals("1"))
                                {
                                    //se verifica si existen guias con restriccion 
                                    if (int.Parse(sRespuesta[1]) > 0)
                                    {
                                        //se agregan valores 
                                        iRestriccion = true;
                                        iError = false;
                                    }
                                    else
                                    {
                                        //se manda mensaje
                                        obj_head.gsiResultado = 1;
                                        obj_head.gssMensaje = "Sin configuración para pendientes de tipo restricción.";
                                        obj_head.gssContenido = "<div class='col-xs-12 pd-l0' style='text-align:center;font-size: 15px;'><i class='fa fa-smile-o'>&nbsp;</i>Sin configuración para pendientes de tipo restricción.</div>";
                                        //se agrega variable
                                        iRestriccion = false;
                                        iError = false;
                                    }
                                }
                                else
                                {
                                    //se manda mensaje
                                    obj_head.gsiResultado = 3;
                                    obj_head.gssMensaje = "Se ha producido un error inesperado. Vuelve a intentarlo más tarde.";
                                    obj_head.gssTitle = "Error:"+sRespuesta[0];
                                    //se detiene ciclo
                                    break;
                                }
                            }
                            else
                            {
                                //se manda mensaje
                                obj_head.gsiResultado = 2;
                                obj_head.gssMensaje = "Sin configuración para pendientes de tipo restricción.";
                                obj_head.gssContenido = "<div class='col-xs-12 pd-l0' style='text-align:center;font-size: 15px;'><i class='fa fa-smile-o'>&nbsp;</i>Sin configuración para pendientes de tipo restricción.</div>";
                                //se agrega variable
                                iRestriccion = false;
                                iError = false;
                            }
                        }
                    }
                    else
                    {
                        //se manda mensaje
                        obj_head.gsiResultado = 2;
                        obj_head.gssMensaje = "Sin configuración para pendientes de tipo restricción.";
                        obj_head.gssContenido = "<div class='col-xs-12 pd-l0' style='text-align:center;font-size: 15px;'><i class='fa fa-smile-o'>&nbsp;</i>Sin configuración para pendientes de tipo restricción.</div>";
                        //se agrega variable
                        iRestriccion = false;
                        iError = false;
                    }
                }
                else
                {
                    //se manda mensaje
                    obj_head.gsiResultado = 3;
                    obj_head.gssMensaje = "Se ha producido un error inesperado. Vuelve a intentarlo más tarde.";
                    obj_head.gssTitle = "Error datatable";
                    //se detiene ciclo
                    break;
                }
                //===============================>
            }
            //Se cierra ciclo
            #endregion
            //================================>

            //se verifica si no hay error
            if (!iError)
            {
                //se verifica si existe restriccion
                if (iRestriccion)
                {
                    //se manda resultado
                    obj_head.gsiResultado = 2;
                    obj_head.gssMensaje = "Tienes Guías sin asignar motivo.";
                    obj_head.gssContenido = "Tienes algunas Guías sin asignar un Motivo del porque esta pendiente";
                    obj_head.gssTitle = "Restricción";
                    obj_head.gssIcono = "fa fa-times-circle icono-20 icono-rojo";
                    obj_head.gsiIsRestriccion = iRestriccion;
                }
                else
                {
                    //================================>
                    #region INICIO VALIDACION ALERTAS
                    //se recorren flujos
                    for (int i = 0; i < iIdFlujos.Length; i++)
                    {
                        //se inicializan variables
                        iError = true;
                        iAlerta = false;
                        //===============================>
                        //se crea querys
                        sQuery = "select iTiempo,iIdOficina from tc_AlertasFlujoAereo where iIdOficina in(SELECT trou.iIdOficina FROM tr_Oficina_Usuario trou WHERE trou.iIdUsuario=" + obj_head.gsiIdUsuario + ") and iIdFlujo = " + iIdFlujos[i] + " and iIdTipoAlerta = 1";
                        //se ejecuta query
                        sDatos = obj_conexion.ejecutarConsultaRegistroMultiplesData(sQuery);
                        //se valida resultado
                        if (!sDatos.HasErrors)
                        {
                            //se verifica si hay datos
                            if (sDatos.Rows.Count > 0)
                            {
                                //se reccorren filas 
                                foreach (DataRow obj_fila in sDatos.Rows)
                                {
                                    //se inicializan variables
                                    iTiempo = 0;
                                    iIdOficina = 0;
                                    //se recuperan variables 
                                    iTiempo = !string.IsNullOrEmpty(obj_fila["iTiempo"].ToString()) ? int.Parse(obj_fila["iTiempo"].ToString()) : 0;
                                    iIdOficina = !string.IsNullOrEmpty(obj_fila["iIdOficina"].ToString()) ? int.Parse(obj_fila["iIdOficina"].ToString()) : 0;
                                    //se verifica si es diferente de 0
                                    if (iTiempo != 0)
                                    {

                                        //se crea query para recuperar las guias en restriccion
                                        sQuery = "Select Count(*) " +
                                                " from taer_Guia as guia left join " +
                                                " taer_DetalleGuia as detalle on guia.iIdGuia = detalle.iIdGuia " +
                                                " where datediff(minute,isnull(dFechaPendiente,dFechaRegistro),dbo.fn_GetDate()) >= " + iTiempo + " and (iPendiente = 1 or iPendiente is null) and iIdOficina = " + iIdOficina + " and iIdFlujo = " + iIdFlujos[i] + " and " + string.Format(sWhereCliente, iIdOficina);
                                        //se ejecuta query 
                                        sRespuesta = obj_conexion.ejecutarConsultaRegistroSimple(sQuery);
                                        //se verifica si se ejecuto correctamente la query
                                        if (sRespuesta[0].Equals("1"))
                                        {
                                            //se verifica si existen guias con restriccion 
                                            if (int.Parse(sRespuesta[1]) > 0)
                                            {
                                                //se agregan valores 
                                                iAlerta = true;
                                                iError = false;
                                            }
                                            else
                                            {
                                                //se manda mensaje
                                                obj_head.gsiResultado = 1;
                                                obj_head.gssMensaje = "Sin configuración para pendientes de tipo Alerta.";
                                                obj_head.gssContenido = "<div class='col-xs-12 pd-l0' style='text-align:center;font-size: 15px;'><i class='fa fa-smile-o'>&nbsp;</i>Sin configuración para pendientes de tipo restricción.</div>";
                                                //se agrega variable
                                                iAlerta = false;
                                                iError = false;
                                            }
                                        }
                                        else
                                        {
                                            //se manda mensaje
                                            obj_head.gsiResultado = 3;
                                            obj_head.gssMensaje = "Se ha producido un error inesperado. Vuelve a intentarlo más tarde.";
                                            obj_head.gssTitle = "Error:" + sRespuesta[0];
                                            //se detiene ciclo
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        //se manda mensaje
                                        obj_head.gsiResultado = 1;
                                        obj_head.gssMensaje = "Sin configuración para pendientes de tipo Alerta.";
                                        obj_head.gssContenido = "<div class='col-xs-12 pd-l0' style='text-align:center;font-size: 15px;'><i class='fa fa-smile-o'>&nbsp;</i>Sin configuración para pendientes de tipo Alerta.</div>";
                                        //se agrega variable
                                        iAlerta = false;
                                        iError = false;
                                    }
                                }
                            }
                            else
                            {
                                //se manda mensaje
                                obj_head.gsiResultado = 2;
                                obj_head.gssMensaje = "Sin configuración para pendientes de tipo restricción.";
                                obj_head.gssContenido = "<div class='col-xs-12 pd-l0' style='text-align:center;font-size: 15px;'><i class='fa fa-smile-o'>&nbsp;</i>Sin configuración para pendientes de tipo Alerta.</div>";
                                //se agrega variable
                                iAlerta = false;
                                iError = false;
                            }
                        }
                        else
                        {
                            //se manda mensaje
                            obj_head.gsiResultado = 3;
                            obj_head.gssMensaje = "Se ha producido un error inesperado. Vuelve a intentarlo más tarde.";
                            obj_head.gssTitle = "Error datatable";
                            //se detiene ciclo
                            break;
                        }
                        //===============================>
                    }
                    //Se cierra ciclo
                    #endregion
                    //================================>
                    //se verifica si no hay error
                    if (!iError)
                    {
                        //se verifica si existe alerta
                        if (iAlerta)
                        {
                            //se manda resultado
                            obj_head.gsiResultado = 2;
                            obj_head.gssMensaje = "Tienes Guías sin asignar motivo.";
                            obj_head.gssContenido = "Tienes algunas Guías sin asignar un Motivo del porque esta pendiente";
                            obj_head.gssTitle = "Alerta";
                            obj_head.gssIcono = "fa fa-exclamation-triangle icono-20 icono-amarillo";
                            obj_head.gsiIsRestriccion = false;
                            obj_head.gsiIsAlerta = true;
                        }
                        else
                        {
                            //se manda resultado
                            obj_head.gsiResultado = 1;
                            obj_head.gssMensaje = "Validación sin configuración";
                            obj_head.gssContenido = "";
                            obj_head.gssTitle = "";
                            obj_head.gssIcono = "";
                            obj_head.gsiIsRestriccion = false;
                            obj_head.gsiIsAlerta = false;
                        }
                    }
                    //==========================>
                }
            }
        }
        //FIN TRY
        //INICIO CATCH
        catch (Exception ex)
        {
            //se manda mensaje
            obj_head.gsiResultado = 3;
            obj_head.gssMensaje = "Se ha producido un error inesperado. Vuelve a intentarlo más tarde.";
            obj_head.gssTitle = ex.Message;
        }
        //FIN CATCH
    }
    #endregion
    //********************

    ///METODO PARA CREAR MENU DE USUARIO
    #region METODO PARA CREAR MENU DE USUARIO
    /// <summary>
    /// METODO PARA CREAR MENU DEL USUARIO
    /// </summary>
    public void fn_creaMenu()
    {
        ///INICIO TRY
        try
        {
            ///INSTANCIAS 
            Conexion obj_conexion = new Conexion();
            DataTable obj_datos = new DataTable();
            ///SE DECLARAN VARIABLES
            string sQuery = "", sMenu = "", sSubMenu = "", sIcono = "", sNombreMenu = "",sRespuesta = "";
            int iContMenus = 0, iIdMenu = 0;
            bool iError = false;
            ///SE CREA QUERY PARA MENUS
            sQuery = "SELECT * FROM CT_MENU WHERE IESTADO = 1 AND (IISMULTINIVEL = 0 OR IISMULTINIVEL IS NULL) AND IIDMENU IN(SELECT DISTINCT IIDMENU FROM CT_SUBMENU AS SUB INNER JOIN TR_SUBMENU_USUARIOS AS TR ON SUB.IIDSUBMENU = TR.IIDSUBMENU WHERE IIDUSUARIO = " + this.gsiIdUsuario + " AND ITIPOACCESO IN (1,2) UNION SELECT IIDMENUMULTINIVEL FROM CT_MENU WHERE IIDMENU IN(SELECT DISTINCT IIDMENU FROM CT_SUBMENU AS SUB INNER JOIN TR_SUBMENU_USUARIOS AS TR ON SUB.IIDSUBMENU = TR.IIDSUBMENU WHERE IIDUSUARIO = " + this.gsiIdUsuario + " AND ITIPOACCESO IN (1,2)) AND IISMULTINIVEL = 1)";
            ///SE EJECUTA QUERY 
            obj_datos = obj_conexion.ejecutarConsultaRegistroMultiplesData(sQuery);
            ///SE VALIDA SI SE EJECUTO LA QUERY CON EXITO 
            if (!obj_datos.HasErrors)
            {
                ///SE VERIFICA SI HAY MENUS
                if (obj_datos.Rows.Count > 0)
                {
                    ///SE RECORREN FILAS
                    foreach (DataRow obj_fila in obj_datos.Rows)
                    {
                        ///SE RECUPERAM VALORES
                        iIdMenu = int.Parse(obj_fila["iIdMenu"].ToString());
                        sIcono = obj_fila["sIcono"].ToString();
                        sNombreMenu = obj_fila["sNombreMenu"].ToString();
                        sMenu = "";
                        ///SE CREAN SUBMENUS
                        this.fn_creaSubmenu(iIdMenu);
                        ///SE VALIDA EL RESULTADO
                        ///CON RESULTADOS
                        if (this.gsiResultado == 1 && this.gsiNumeroRegistros > 0)
                        {
                            ///SE CREA ITEM DE MENU
                            sMenu = "<li><a href='#_'class='dropdown-toggle' data-toggle='dropdown'><i class='" + sIcono + "' style='width:25px;'>&nbsp;</i>" + sNombreMenu + "<b class='caret'></b></a>"+
                                    "<ul class='dropdown-menu multi-level'>"+
                                        this.gssContenido+
                                    "</ul></li>";
                            ///SE INCREMENTA EL CONTADOR
                            iContMenus++;
                        }
                        ///SIN RESULTADOS
                        else if (this.gsiResultado == 2 || (this.gsiResultado == 1 && this.gsiNumeroRegistros == 0))
                        {

                        }
                        ///ERROR
                        else
                        {
                            ///SE AGREGA BANDERA DE ERROR
                            iError = true;
                            ///SE ROMPE CICLO
                            break;
                        }
                        ///SE AGREGA A RESPUESTA
                        sRespuesta += sMenu;
                    }
                    ///SE VALIDA SI HUBO ERROR
                    if (!iError)
                    {
                        ///SE AGREGAN VARIABLES+
                        this.gsiResultado = 1;
                        this.gssMensaje = "Menu Creado con exito";
                        ///SE VERIFICA SI HUBO RESULTADOS
                        if (iContMenus > 0)
                        {
                            ///SE MANDA RESULTADO
                            this.gssContenido = sRespuesta;
                        }
                        else
                        {
                            ///SE MANDA RESULTADO
                            this.gssContenido = "<li><a href='#_'><i class='fa fa-exclamation-triangle'>&nbsp;</i>Sin opciones de navegación</a></li>";
                        }
                    }
                }
                else
                {
                    ///SE MANDA RESULTADO
                    this.gsiResultado = 2;
                    this.gssMensaje = "Sin opciones de navegación";
                    this.gssContenido = "<li><a href='#_'><i class='fa fa-exclamation-triangle'>&nbsp;</i>" + this.gssMensaje + "</a></li>";
                }
            }
            else
            {
                ///SE MANDA RESULTADO
                this.gsiResultado = 3;
                this.gssMensaje = "Error al crear menú: Error en query";
                this.gssContenido = "<li><a href='#_'>" + this.gssMensaje + "</a></li>";
            }
        }
        ///FIN TRY 
        ///INICIO CATCH
        catch (Exception ex)
        {
            ///SE MANDA MENSAJE 
            this.gsiResultado = 0;
            this.gssMensaje = "Error al crear menú: " + ex.Message;
            this.gssContenido = "<li><a href='#_'>Error al crear menú: " + ex.Message + "</a></li>";
        }
        ///FIN CATCH
    }
    #endregion
    ///=================================>

    ///METODO PARA CREAR SUBSUBMENU DE USUARIO
    #region METODO PARA CREAR SUBMENU DE USUARIO
    /// <summary>
    /// METODO PARA CREAR SUBMENU DEL USUARIO
    /// </summary>
    public void fn_creaSubmenu(int iIdMenu)
    {
        ///INICIO TRY
        try
        {
            ///INSTANCIAS 
            Conexion obj_conexion = new Conexion();
            DataTable obj_datos = new DataTable();
            Security obj_secvariables = new Security();
            ///SE DECLARAN VARIABLES
            string sQuery = "",sRespuesta = "", sSubMenu = "", sURL = "", sIcono = "", sIdSubMenu = "", sNombreSubMenu = "";
            int iContSubmenus = 0, iAplicaMultinivel = 0, iIdSubmenu = 0;
            this.gsiNumeroRegistros = 0;
            ///SE CREA QUERY PARA SUBMENUS
            sQuery = "SELECT IIDMENU AS IIDSUBMENU,SNOMBREMENU AS SNOMBRESUBMENU,'#_' AS SURL,SICONO,1 AS IMULTINIVEL FROM CT_MENU WHERE IESTADO = 1 AND IISMULTINIVEL = 1 AND IIDMENUMULTINIVEL = " + iIdMenu + " " +
                     " UNION "+
                     " SELECT DISTINCT SUB.IIDSUBMENU,SNOMBRESUBMENU,SURL,SICONO,0 AS IMULTINIVEL FROM CT_SUBMENU AS SUB INNER JOIN TR_SUBMENU_USUARIOS AS TR ON SUB.IIDSUBMENU = TR.IIDSUBMENU WHERE IIDUSUARIO = " + this.gsiIdUsuario + " AND ITIPOACCESO IN (1,2) AND IESTADO = 1 AND IIDMENU = " + iIdMenu + " AND (IIDTIPOUSUARIO = (SELECT IIDTIPOUSUARIO FROM TB_USUARIOS WHERE IIDUSUARIO = " + this.gsiIdUsuario + ") OR IIDTIPOUSUARIO = 3)";
            ///SE EJECUTA QUERY 
            obj_datos = obj_conexion.ejecutarConsultaRegistroMultiplesData(sQuery);
            ///SE VALIDA SI SE EJECUTO LA QUERY CON EXITO 
            if (!obj_datos.HasErrors)
            {
                ///SE VERIFICA SI HAY SUBMENUS
                if (obj_datos.Rows.Count > 0)
                {
                    ///SE ORDENA DATATABLE
                    obj_datos.DefaultView.Sort = "SNOMBRESUBMENU ASC";
                    ///SE RECORREN FILAS
                    foreach (DataRow obj_fila in obj_datos.Rows)
                    {
                        ///SE RECUPERAN VARIABLES
                        iAplicaMultinivel = int.Parse(obj_fila["IMULTINIVEL"].ToString());
                        iIdSubmenu = int.Parse(obj_fila["IIDSUBMENU"].ToString());
                        sURL = obj_fila["SURL"].ToString();
                        sIcono = obj_fila["SICONO"].ToString();
                        sNombreSubMenu = obj_fila["SNOMBRESUBMENU"].ToString();
                        obj_secvariables = new Security(obj_fila["IIDSUBMENU"].ToString());
                        sIdSubMenu = obj_secvariables.Encriptar();
                        sSubMenu = "";
                        ///SE VERIFICA SI EL CAMPO ES MULTINIVEL
                        if (iAplicaMultinivel == 1)
                        {
                            ///SE CREA MENU MULTINIVEL
                            this.fn_creaSubmenu(iIdSubmenu);
                            ///SE VERIFICA SI HUBO RESULTADOS
                            if (this.gsiResultado == 1 && this.gsiNumeroRegistros > 0)
                            {
                                ///SE CREA ITEM DE MENU
                                sSubMenu = "<li class='dropdown-submenu'><a href='#_' class='dropdown-toggle' data-toggle='dropdown'><i class='" + sIcono + "' style='width:25px;'>&nbsp;</i>" + sNombreSubMenu + "</a>" +
                                        "<ul class='dropdown-menu dropdown-menu-multi'>" +
                                            this.gssContenido +
                                        "</ul></li>";
                                ///SE INCREMENTA SUBMENU
                                iContSubmenus++;
                            }
                        }
                        else
                        {
                            ///SE CREA SUBMENU
                            sSubMenu = "<li><a href='" + sURL + "?sTypeCtrlAc=" + sIdSubMenu + "'><i class='" + sIcono + "' style='width:25px;'>&nbsp;</i>" + sNombreSubMenu + "</a></li>";
                            ///SE INCREMENTA SUBMENU
                            iContSubmenus++;
                        }
                        ///SE AGREGA A RESPUESTA
                        sRespuesta += sSubMenu;
                    }
                    ///SE MANDA RESULTADO
                    this.gsiResultado = 1;
                    this.gssMensaje = "SubMenu creado";
                    this.gssContenido = sRespuesta;
                    this.gsiNumeroRegistros = iContSubmenus;
                }
                else
                {
                    ///SE MANDA RESULTADO
                    this.gsiResultado = 2;
                    this.gssMensaje = "Sin opciones de navegación";
                    this.gssContenido = "<li><a href='#_'>" + this.gssMensaje + "</a></li>";
                }
            }
            else
            {
                ///SE MANDA RESULTADO
                this.gsiResultado = 3;
                this.gssMensaje = "Error al crear menú: Error en query";
                this.gssContenido = "<li><a href='#_'>" + this.gssMensaje + "</a></li>";
            }
        }
        ///FIN TRY 
        ///INICIO CATCH
        catch (Exception ex)
        {
            ///SE MANDA MENSAJE 
            this.gsiResultado = 0;
            this.gssMensaje = "Error al crear menú: " + ex.Message;
            this.gssContenido = "<li><a href='#_'>Error al crear menú: " + ex.Message + "</a></li>";
        }
        ///FIN CATCH
    }
    #endregion
    ///=================================>
    
    //CONSTRUCTOR
    #region Constructor
    public Header()
	{
		//
		// TODO: Agregar aquí la lógica del constructor
		//
    }
    #endregion
}