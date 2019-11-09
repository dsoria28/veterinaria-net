<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Inicio.aspx.cs" Inherits="Vista_Inicio_Inicio" MasterPageFile="~/Vista/Diseno/MasterPage.master" %>

<%@ Register Src="~/Vista/Diseno/brearCrumbs.ascx" TagName="breadCrumbs" TagPrefix="bread" %>

<asp:Content runat="server" ID="contend1" ContentPlaceHolderID="head">
    <link href="../../Styles/CSS/dataTables.bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../../Scripts/jsplugin/select2Plugin/select2.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jsplugin/select2Plugin/select2.js" type="text/javascript"></script>
    <script src="../../Scripts/Inicio/js_inicio.js" type="text/javascript"></script>
    <script src="../../Scripts/jsplugin/jquery.dataTables.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jsplugin/dataTables.bootstrap.min.js" type="text/javascript"></script>
    <!-- Inicio Pendientes previo -->
    <link href="../../Styles/CSS/cssEstilosVarios.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/Inicio/js_pendientes_tramitador.js" type="text/javascript"></script>
    <style type="text/css">
        /*Estilo para tr de actividad activa*/
        .startProcess {
            background-color: #D3FEC5 !important;
        }

        tr.startProcess:hover {
            background: #85c555 !important;
        }
        /*Estilo para tr de actividad inasctiva o detenida*/
        .endProcess {
            background-color: #FEDDDD !important;
        }

        tr.endProcess:hover {
            background: #85c555 !important;
        }

        .panel-default {
            background-color: #e6e8eb;
            border-color: transparent;
        }
        /*.panel-heading { background-color:#FFF; border: 1px solid #1d6688 !important; cursor:pointer; }*/
        .panel-title {
            color: #1d6688;
        }

            .panel-title:hover {
                cursor: pointer;
            }

        .icon_inicio {
            width: 105px;
            display: inline-block;
            /*box-shadow: 10px 10px 7px -8px rgba(0,0,0,0.75);*/
            box-shadow: 10px 10px 7px -8px #1d6688;
            padding: 6px 5px;
            border: solid 1px #DEDEDE;
            border-radius: 6px;
            margin: 2px 2px 50px 2px;
            background-color: #EFEFEF;
            -webkit-transition: all 0.2s ease-in-out;
            transition: all 0.2s ease-in-out;
        }

            .icon_inicio:hover {
                -moz-transform: scale(1.1);
                -webkit-transform: scale(1.1);
                -o-transform: scale(1.1);
                -ms-transform: scale(1.1);
                transform: scale(1.1);
            }

        .spn_title {
            white-space: nowrap;
            text-overflow: ellipsis;
            display: block;
            overflow: hidden;
            font-size: 12px;
        }
    </style>
    <!-- Fin Pendientes previo -->
</asp:Content>
<asp:Content runat="server" ID="contend2" ContentPlaceHolderID="contend">
    <bread:breadCrumbs ID="breadCrum" runat="server" />
    <!-- Identificador del usuario -->
    <asp:HiddenField ID="ahddUser" runat="server" />
    <asp:HiddenField ID="ahdAction" runat="server" />
    <asp:HiddenField ID="ahdActionPrevio" runat="server" />
    <asp:HiddenField ID="ahddIdActividad" runat="server" />
    <asp:HiddenField ID="ahddTypeCtrl" runat="server" />
    <!-- Inicio contenedor general bootstrap -->
    <div class="container-fluid">
        <!-- Inicio bóton para configurar inicio -->
        <div class="row form-group text-right hide">
            <input type="button" class="shadow btn btn-sm btn-primary right input-sm" value="Configurar" data-toggle="modal" data-target="#dialogConfigurarInicio" />
        </div>
        <!-- Fin bóton para configurar inicio -->
    </div>
    <!-- Fin contenedor general bootstrap -->
    <!-- Inicio vista inicio -->
    <div class="container-fluid">
        <div class="hide">
            <asp:Label ID="hlblVistaInicio" runat="server"></asp:Label>
        </div>
    </div>

    <!-- Fin vista inicio -->
    <!-- Inicio dialog seleccionar pantalla inicio -->
    <div class="modal fade" id="dialogConfigurarInicio" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog modal-sm" role="document">
            <div class="modal-content">
                <div class="modal-header" style="background: rgb(29, 102, 136) none repeat scroll 0% 0%; color: White;">
                    <!-- Inicio botón cerrar dialog -->
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="color: White;">
                        <span aria-hidden="true">&times;</span></button>
                    <!-- Fin botón cerrar dialog -->
                    <!-- Inicio titulo dialog -->
                    <h5 class="modal-title">
                        <b>Seleccionar pantalla inicio</b></h5>
                    <!-- Fin titulo dialog -->
                </div>
                <div class="modal-body">
                    <label>Pantalla inicio:</label><br />
                    <asp:DropDownList ID="addlPantallaInicio" name="addlPantallaInicio" runat="server"
                        DataTextField="sNombrePantalla" DataValueField="iIdTipoPantalla" required data-bv-notempty-message="Debe seleccionar una pantalla de inicio.">
                    </asp:DropDownList>
                </div>
                <div class="modal-footer">
                    <!-- Incio botón seleccionar -->
                    <button type="button" class="shadow btn btn-sm btn-success btn-greenS input-sm" onclick="javascript:fn_guardar_pantalla_inicio();">Guardar</button>
                    <!-- Fin botón seleccionar -->
                </div>
            </div>
        </div>
    </div>
    <!-- Fin dialog seleccionar pantalla inicio -->
    <!-- Inicio dialog seleccionar pantalla frecuente -->
    <div class="modal fade" id="dialogSeleccionarPantallaFrecuente" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header" style="background: rgb(29, 102, 136) none repeat scroll 0% 0%; color: White;">
                    <!-- Inicio botón cerrar dialog -->
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="color: White;">
                        <span aria-hidden="true">&times;</span></button>
                    <!-- Fin botón cerrar dialog -->
                    <!-- Inicio titulo dialog -->
                    <h5 class="modal-title">
                        <b>Seleccionar pantallas frecuentes</b></h5>
                    <!-- Fin titulo dialog -->
                </div>
                <div class="modal-body">
                    <%--<label>Seleccionar pantalla:</label><br />
                    <asp:DropDownList ID="addlPantallasFrecuentes" name="addlPantallaInicio" runat="server"
                        DataTextField="sNombreSubMenu" DataValueField="iIdSubMenu">
                    </asp:DropDownList>
                    <br />
                    <br />
                    <asp:Label ID="hlblListaPantallasFrecuentes" runat="server"></asp:Label>--%>
                </div>
            </div>
        </div>
    </div>
    <!-- Fin dialog seleccionar pantalla frecuente -->
    <!-- Inicio dialog eliminar pantalla frecuente -->
    <div class="modal fade" id="dialogEliminarPantallaFrecuente" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog modal-sm" role="document">
            <div class="modal-content">
                <div class="modal-header" style="background: rgb(29, 102, 136) none repeat scroll 0% 0%; color: White;">
                    <!-- Inicio botón cerrar dialog -->
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="color: White;">
                        <span aria-hidden="true">&times;</span></button>
                    <!-- Fin botón cerrar dialog -->
                    <!-- Inicio titulo dialog -->
                    <h5 class="modal-title">
                        <b>Eliminar pantalla frecuente</b></h5>
                    <!-- Fin titulo dialog -->
                </div>
                <div class="modal-body">
                    <asp:HiddenField ID="ahddIdPantallaFrecuente" runat="server" />
                    <label>¿Está seguro de eliminar la pantalla frecuente?</label>
                </div>
                <div class="modal-footer">
                    <!-- Incio botón eliminar -->
                    <button type="button" class="btn btn-sm btn-success btn-greenS input-sm" onclick="javascript:fn_guardar_eliminar_pantalla_frecuente(2);">Aceptar</button>
                    <!-- Fin botón eliminar -->
                </div>
            </div>
        </div>
    </div>
    <!-- Fin dialog eliminar pantalla frecuente -->
    <!-- Inicio Pendientes de previo -->
    <!--Ajusta Tamaño-->
    <div id="dvPendientes">
        <%/*<div class="row">
            <div class="col-xs-12">
                <div class="range range-primary">
                    <input type="range" id="hrgAjustaTamano" name="range" onchange="range.value=value" style="outline:none;" />
                    <output id="etiqueta">0%</output>
                </div>
            </div>
        </div>*/%>
        <!---->
        <br />
        <input type="text" id="htxtOpcionAjuste" class="hide" />
        <!--/////Inicio contenido general////-->
        <div class="container-fluid">
            <!--////Botón para regresar a pendientes de usuario////-->
            <%--<div class="row panel panel-default" id="hdvContador">
                <div class="text-left col-xs-12 col-sm-6 col-md-6 col-lg-6" style="padding-top: 8px;">
                    <span style="color: #337ab7">Tiempo total trabajado: </span>
                    <asp:Label runat="server" ID="alblTiempoTotal"></asp:Label>
                </div>
            </div>--%>
        </div>
        <!---/////Lista de pendientes////-->
        <div class="container-fluid">
            <div class="row form-group">
                <asp:Label ID="alblPendientesTramitador" runat="server"></asp:Label>
            </div>
        </div>
    </div>
    <!---/////Inicio contenido general////-->

    <!--Inicio Dialog para confirmar inicio de la actividad-->
    <div class="modal fade" id="hdvIniciarActividad">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <!--Inicio contenido modal-->
                <div class="modal-header" style="background: rgb(29, 102, 136) none repeat scroll 0% 0%; color: White;">
                    <!--Botón para cerrar el dialog(close)-->
                    <button type="button" class="close" data-dismiss="modal" style="color: White">&times;</button>
                    <!--Título del dialog-->
                    <h5 class="modal-title">Confirmar iniciar</h5>
                </div>
                <div class="modal-body">
                    <p>
                        <label>¿Está seguro de iniciar la actividad?</label></p>
                </div>
                <div class="modal-footer">
                    <!--Inicio footer dialog eliminar-->
                    <button type="button" class="shadow btn btn-greenS btn-sm input-sm" onclick="javascript:fn_inicia_actividad(1);" id="hbtnIniciarActividad">Aceptar</button>
                </div>
                <!--Fin footer dialog-->
            </div>
            <!--Fin contenido modal dialog-->
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <!--Fin Dialog para confirmar inicio de actividad-->

    <!--Inicio Dialog para confirmar inicio de la actividad-->
    <div class="modal fade" id="hdvConfirmaNuevaActividad">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <!--Inicio contenido modal-->
                <div class="modal-header" style="background: rgb(29, 102, 136) none repeat scroll 0% 0%; color: White;">
                    <!--Botón para cerrar el dialog(close)-->
                    <button type="button" class="close" data-dismiss="modal" style="color: White">&times;</button>
                    <!--Título del dialog-->
                    <h5 class="modal-title">Confirmar iniciar</h5>
                </div>
                <div class="modal-body">
                    <p class="text-center">
                        <label>Existe una actividad en proceso. ¿Está seguro de iniciar la actividad?</label></p>
                </div>
                <div class="modal-footer">
                    <!--Inicio footer dialog eliminar-->
                    <button type="button" class="shadow btn btn-greenS btn-sm input-sm" onclick="javascript:fn_inicia_actividad(2);" id="hbtnDialogAct">Aceptar</button>
                </div>
                <!--Fin footer dialog-->
            </div>
            <!--Fin contenido modal dialog-->
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <!--Fin Dialog para confirmar inicio de actividad-->
    <!--Dialog de Confirmación-->
    <div class="modal fade " id="hdvConfirmar" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog modal-sm" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <!-- Inicio botón cerrar dialog -->
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="color: White;">
                        <span aria-hidden="true">&times;</span></button>
                    <!-- Fin botón cerrar dialog -->
                    <!-- Inicio titulo dialog -->
                    <h4 class="modal-title" id="hlblMensajeTitle">
                        <b></b>
                    </h4>
                    <!-- Fin titulo dialog -->
                </div>
                <!--Mensaje-->
                <div class="modal-body">
                    <div id="hlblMensajeBody" class="table table-responsive">
                    </div>
                </div>
                <!--Fin Mensaje-->
                <div class="modal-footer">
                    <button type="button" id="abtnGuardar" class="btn btn-sm shadow btn-greenS">
                        <span>Guardar</span>
                    </button>
                    <button type="button" class="btn btn-sm btn-default shadow" onclick="javascript:$('#hdvConfirmar').modal('toggle');">
                        <span>Cancelar</span>
                    </button>
                </div>
            </div>
        </div>
    </div>
    <!--***********************-->
    <!-- Fin Pendientes de previo -->

    <!-- Inicio dialog seleccionar pantalla inicio -->
    <div class="modal fade" id="hdvAvisosUsuario" tabindex="-1" data-backdrop="static" data-keyboard="false" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header" style="background: rgb(29, 102, 136) none repeat scroll 0% 0%; color: White;">
                    <!-- Inicio botón cerrar dialog 
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="color: White;">
                        <span aria-hidden="true">&times;</span></button>-->
                    <!-- Fin botón cerrar dialog -->
                    <!-- Inicio titulo dialog -->
                    <h5 class="modal-title"><b>Aviso de Actualizaciones</b></h5>
                    <!-- Fin titulo dialog -->
                </div>
                <div class="modal-body" style="overflow: auto; max-height: 450px;">
                    <div class="row">
                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 table-responsive">
                            <asp:Label runat="server" ID="alblListadoAvisos"></asp:Label>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <!-- Incio botón seleccionar -->
                    <button type="button" class="shadow btn btn-sm btn-success btn-greenS input-sm" onclick="javascript:fn_actualizar_estatus_aviso_usuario();">Aceptar</button>
                    <!-- Fin botón seleccionar -->
                </div>
            </div>
        </div>
    </div>
    <!-- Fin dialog seleccionar pantalla inicio -->
</asp:Content>
