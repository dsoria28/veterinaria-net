<%@ Control Language="C#" AutoEventWireup="true" CodeFile="header_entregas.ascx.cs" Inherits="Vista_Diseno_header_entregas" %>

<!--IDENTIFICADOR DE USUARIO-->
<asp:HiddenField runat="server" ID="atxtIdUsuarioNotificaciones" />

<div class="container-fluid head-sys">
    <div class="row contenido-menu">
        <!--Inicio Ingresar-->
        <div class="text-right" style="padding-bottom: 5px;padding-top:4px;">
            <!--Imagen Logo-->
            <div style="float: left;">
                <img src="../../Styles/Imagenes/Logos Sistemas-04.png" style="height: 60px;">
            </div>
            <!--INICIO BOTONES NOTIFICACIONES-->
            <div class="contentNotify dropdown dropdown-notify">
                <!--NOTIFICACIONES-->
                <button class="btn btn_gray btn-sm shadow dropdown-toggle" id="Contnotificacion" type="button" title="Notificaciones" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="true"  data-area="1" style="padding: 5px 4px;">
                    <span class="glyphicon glyphicon-bell" aria-hidden="true" style="padding-right: 1px; position: relative; top: 5px; display: inline-block;"></span>
                    <span id="hspNumeroNotificacionesUser" class="careta btn-caret">0</span>
                </button>
                <!--AREA NOTIFICACIONES CUERPO-->
                <ul class="dropdown-menu notify-drop notify-sm cuerpo-notificaciones">
                    <!-- INICIO TITULO NOTIFICACION -->
                    <div class="notify-drop-title">
                        <div class="row">
                            <div class="col-md-6 col-sm-6 col-xs-6">Pendientes (<b id="hbNumeroPendientesUsuario">0</b>)</div>
                            <div class="col-md-6 col-sm-6 col-xs-6 text-right"><a href="#_" class="rIcon allRead"><i class="fa fa-bell-o icono-blanco"></i></a></div>
                        </div>
                    </div>
                    <!-- FIN TITULO NOTIFICACION -->
                    <!--INICIO CONTENIDO NOTIFICACIONES-->
                    <div id="hdvContenidoNotificacionesUsuario" class="drop-content">
                        <li>
                            <div class="col-md-3 col-sm-3 col-xs-3">
                                <div class="notify-img">
                                    <img src="https://placehold.it/45x45" alt=""></div>
                            </div>
                            <div class="col-md-9 col-sm-9 col-xs-9 pd-l0">
                                <i>Aquí se muestran las notificaciones.</i><a href="" class="rIcon"><i class="fa fa-dot-circle-o"></i></a>
                                <p class="time">06/10/2017</p>
                            </div>
                        </li>
                        <li>
                            <div class="col-md-3 col-sm-3 col-xs-3">
                                <div class="notify-img">
                                    <img src="https://placehold.it/45x45" alt=""></div>
                            </div>
                            <div class="col-md-9 col-sm-9 col-xs-9 pd-l0">
                                <span>Notificacion ejemplo</span> <a href="#_" class="rIcon"><i class="fa fa-dot-circle-o icono-azul"></i></a>
                                <p>Parrafo de notificación.</p>
                                <p class="time">hace unos minutos</p>
                            </div>
                        </li>
                    </div>
                    <!--FIN  CONTENIDO NOTIFICACIONES-->
                    <!--INICIO FOOTER CONTENIDO NOTIFICACIONES-->
                    <div class="notify-drop-footer text-center">
                        <a href="../Utilerias/historial_notificaciones.aspx"><i class="fa fa-history">&nbsp;</i>Historial</a>
                    </div>
                    <!--FIN FOOTER CONTENIDO NOTIFICACIONES-->
                </ul>
                <!---->
                <!--AVISOS-->
                
                <!--TAREAS PENDIENTES-->
                
                <!--==========-->
                <!--INFO USUARIO-->
                <div class="btn-group">
                    <!--INICIO BOTON CON DATOS DE USUARIO -->
                    <button type="button" class="btn btn_gray btn-sm dat_user shadow2">
                        <span class="fa fa-user icon_green icono-15" style="font-size: 15px;"></span>
                        <asp:Label runat="server" ID="alblNombreUsuario" CssClass="ellipsis">
                            José Alberto Daniel Francisco Peréz Dominguéz</asp:Label>
                    </button>
                    <!---FIN BOTON CON DATOS DE USUARIO --->
                    <!---INICIO BOTON OPCIONES USUARIO-->
                    <button type="button" class="btn btn_gray btn-sm dropdown-toggle dat_user_toogle shadow2" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                        <span class="caret"></span><span class="sr-only">Toggle Dropdown</span>
                    </button>
                    <ul class="dropdown-menu dropdown-menu-right btn-sm">
                        <li>
                            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">
                                    Cerrar Sesión
                            </asp:LinkButton>
                        </li>
                    </ul>
                    <!---FIN BOTON OPCIONES USUARIO-->
                </div>
                <!--=============-->
            </div>
        </div>
        <br>
        <div class="container-fluid"></div>
        <!--===================-->
        <!--INICIO MENU-->
        <div id="cssmenu" class="menu-nav">
            <nav id="hnavMenu" class="navbar navbar-default navbar-xs shadow">
                <div class="container-fluid navbar-inner-sm">
                    <!-- Brand and toggle get grouped for better mobile display -->
                    <div class="navbar-header">
                        <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#bs-example-navbar-collapse-1" aria-expanded="false">
                            <span class="sr-only">Toggle</span>
                            <span class="icon-bar"></span>
                            <span class="icon-bar"></span>
                            <span class="icon-bar"></span>
                        </button>
                    </div>
                    <!--============================================-->
                    <!--Inicio Menu-->
                    <div class="navbar-collapse collapse" id="bs-example-navbar-collapse-1" aria-expanded="false" style="height: 1px; margin-right: 0 !important;">
                        <!--<div class="navbar-header">
                            <a class="navbar-brand" href="#">WebSiteName</a>
                        </div>-->
                        <ul id="hulMenuUsuario" runat="server" class="nav navbar-nav">
                        </ul>
                    </div>
                    <!--===============-->

                </div>
            </nav>
        </div>
        <!--===========-->
    </div>
</div>
