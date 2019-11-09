<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no">

    <title>Sistema de Operaciones</title>

    <!--Estilos-->
    <link href="Styles/Bootsrap/FontAwesome/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="Styles/Bootsrap/css/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="Styles/SOP/login.css" rel="stylesheet" type="text/css" />
    <link href="Styles/SOP/stylesSOP.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/CSS/animate.min.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/CSS/css_Loader.css" rel="stylesheet" />
    <!--Scripts-->
    <script src="Scripts/js/jquery-1.11.1.min.js"></script>
    <script src="Scripts/js/jquery-1.10.2.js"></script>
    <script src="Scripts/js/jquery-ui-1.10.4.custom.min.js"></script>
    <script src="Styles/Bootsrap/js/bootstrap.min.js"></script>
    <script src="Scripts/js/jquery.validate.min.js" ></script>
    <script src="Scripts/jsplugin/bootstrapValidator.min.js"></script>
    <script src="Scripts/js/jsLogin.js"></script>
    <script src="Scripts/jsplugin/notify-master/bootstrap-notify.min.js"></script>
    <script src="Scripts/jsplugin/jsblockUI.js"></script>
    <script src="Scripts/jsplugin/jsNotificaciones.js"></script>
    <!--******************************-->

</head>
<body>
    <!-----INICIO FORMULARIO --->
    <form id="frm1" runat="server">

        <!--Form Bandera-->
        <form id="hfrmBandera"></form>
        <!--============-->

        <!----//////////////////////////////INICIO HEADER-->
        <header class="container-fluid header_login_sop">
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 content_header_sop">
                
                <!--inicio login-->
                <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                    <div id="logo_section_sop"></div>
                </div>
                <!--fin login-->

            </div>
        </header>
        <!----//////////////////////////////FIN HEADER--->

        <!----//////////////////////////////CUERPO--->
        <section class="container-fluid section_login_sop">

            <!--INICIO ICON_CONTENIDO-->
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                
            </div>
            <!--FIN ICON_CONTENIDO-->

            <!--INICIO CONTENIDO-->
            <div class="content_section_sop col-lg-5 col-md-7 col-sm-8 col-xs-12 center-block">

                <!--INICIO SUB_CONTENIDO-->
                <div class="subcontent_section_sop row">
                    <!--inicio subcontenido-->

                    <div class="head_content_sop">
                        <!-- Inicio botón cerrar dialog -->
                        <!-- Fin botón cerrar dialog -->
                        <!-- Inicio titulo dialog -->
                        <span id="nom_section_sop">Sistema de Operaciones</span>
                        <!-- Fin titulo dialog -->
                    </div>


                    <!---INICIO CONTENIDO LOGIN--->
                    <div class="row subcontent_content_sop">
                        <!--Inicio Formulario de inicio de sesion-->
                        <form id="hfrmInicioSesion">
                            <div class="content_form_sop col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                <!------datos de usuario-->
                                <div class="row form-group col-xs-12 m-3 text-left">

                                    <div>
                                        <label for="txtLogin">
                                            <asp:Label ID="lblUsuario" runat="server" Text="Usuario:" class="label_user" for="txtLogin"></asp:Label>
                                        </label>
                                    </div>


                                    <div id="hdvTxtLogin" class="input-group input-group-sm">
                                        <span class="input-group-addon">
                                            <span class="glyphicon glyphicon-user icon_green"></span>
                                        </span>
                                        <input type="text" class="form-control" id="txtLogin" name="txtLogin" placeholder="Usuario"
                                            data-bv-notempty data-bv-notempty-message="El usuario es obligatorio" />

                                    </div>
                                </div>
                                <br />
                                <!------datos de pass-->
                                <div class="form-group row col-xs-12 text-left">
                                    <div>
                                        <label>
                                            <asp:Label ID="lblPassword" runat="server" Text="Contraseña:" class="label_user" for="txtPassword"></asp:Label>
                                        </label>
                                    </div>
                                    <div id="hdvTxtPassword" class="input-group input-group-sm">
                                        <span class="input-group-addon">
                                            <span class="glyphicon glyphicon-lock icon_green" aria-hidden="true"></span>
                                        </span>
                                        <input type="password" class="form-control" id="txtPassword" name="txtPassword" placeholder="Contraseña" maxlength="30"
                                            data-bv-notempty data-bv-notempty-message="La contraseña es obligatoria" />
                                    </div>
                                </div>
                                <!------Boton para entrar-->
                                <div class="form-group row col-xs-12 text-right">
                                    <button id="hbtnIniciaSesion" class="shadow btn btn-sm btn_green_sop input-sm" type="submit">
                                        <i class="fa fa-sign-in" aria-hidden="true"></i>
                                        Entrar
                                    </button>
                                </div>
                                <!--============================-->
                                <!------Opción olvidaste tu contraseña-->
                                <div class="form-group row col-xs-12 text-right">
                                    <!--Recuperar contraseña-->
                                    <a id="haOlvidastePass" class="txt-verde label_fotget_sop" href="#_" onclick="javascript:fn_abreDialogCambioPassword(-1)">¿Has olvidado tu contraseña?
                                    </a>
                                    <!--***********************************************************-->
                                </div>


                            </div>
                        </form>
                    </div>
                    <!---FIN CONTENIDO LOGIN--->

                    <!--fin subcontenido-->

                    <div class="">
                        <ul class="nav nav-tabs center-block" id="hlstTabs">
                            <!--por defecto esta seleccionado la de Pendientes-->
                            <li class="active bv-tab-success">
                                <a data-toggle="tab" href="#hdvPendientes" class="icon_sistema_sop" aria-expanded="true">
                                    <span class="fa fa-desktop icon_green_sop"></span>&nbsp;SOP
                                </a>
                            </li>
                            <!--*****************************-->
                            <!--Incompletos-->
                            <li class="" role="button" aria-disabled="false">
                                <asp:LinkButton ID="alkRedirecPrevio" class="icon_sistema_sop" runat="server" PostBackUrl="~/login_previo.aspx">
                                    <span class="fa fa-camera icon_blue_sop"></span>&nbsp;Previos
                                </asp:LinkButton>
                            </li>
                            <!--****************-->
                            <!--Terminados -->
                            <li class="" role="button" aria-disabled="false">
                                <asp:LinkButton ID="alkRedirecEntrega" class="icon_sistema_sop" runat="server" PostBackUrl="~/LoginID.aspx">
                                    <span class="fa fa-credit-card icon_blue_sop"></span>&nbsp;Entregas
                                </asp:LinkButton>
                            </li>
                            <!--*******************-->
                            <!--************-->
                        </ul>
                    </div>

                    <div class="version_sop text-right">Versión 1.0.1</div>



                </div>
                <!--FIN SUB_CONTENIDO-->

            </div>
            <!--FIN CONTENIDO-->




            <!-------INICIO AREA DIALOG CAPCHA---->
            <!--Dialog Cambio de Contraseña Captcha-->
            <div class="modal fade" id="hdvDialogCambioPass" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <!--Inicio Header-->
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <h4 class="modal-title">Cambio de contraseña</h4>
                        </div>
                        <!--*****************-->
                        <!--Inicio Body-->
                        <form id="hfrmCambioPasswordCaptcha">
                            <div class="modal-body">

                                <div class="panel panel-default">
                                    <!-- Default panel contents -->

                                    <!-- List group -->
                                    <ul class="list-group">
                                        <!---Nom Usuario -->
                                        <li class="list-group-item">
                                            <label>Nombre de Usuario:</label>
                                        </li>
                                        <!---Captura Nom Usuario -->
                                        <li class="list-group-item">
                                            <div class="container-fluid">
                                                <div class="col-lg-12 form-group">
                                                    <div id="hdvTxtUsuario">
                                                        <input type="text" id="htxtUsuario" name="htxtUsuario" placeholder="Nombre de Usuario" class="form-control input-sm"
                                                            data-bv-notempty data-bv-notempty-message="El nombre de usuario es obligatorio"
                                                            data-bv-regexp="true"
                                                            data-bv-regexp-regexp="^[A-ZÑa-zñáéíóúÁÉÍÓÚ._-]+$"
                                                            data-bv-regexp-message="Formato de usuario incorrecto" />
                                                    </div>
                                                </div>
                                            </div>

                                        </li>
                                        <!---Codigo Captcha -->
                                        <li class="list-group-item">
                                            <label>Captcha:</label>
                                        </li>
                                        <!---Captura Codigo Captcha -->
                                        <li class="list-group-item">
                                            <div class="container-fluid">
                                                <div class="row form-group">
                                                    <div class="col-lg-12">
                                                        <img id="imageCaptcha" src="Vista/Utilerias/CrearCaptcha.aspx?New=0" style="width: 100%; max-width: 400px;" alt="" />
                                                        <a id="haNuevoCaptcha" class="btn btn-sm shadow" onclick="fn_nuevoCodigoCaptcha();">
                                                            <i aria-hidden="true" class="glyphicon glyphicon-glyphicon glyphicon-refresh" style="font-size: 2em; color: #90C63D"></i>
                                                        </a>
                                                    </div>
                                                </div>


                                                <div class="row form-group ">
                                                    <div class="col-lg-12 form-group">
                                                        <div id="hdvTxtCaptcha">
                                                            <input type="text" id="htxtCaptcha" name="htxtCaptcha" placeholder="Codigo captcha" class="form-control input-sm"
                                                                data-bv-notempty data-bv-notempty-message="El codigo captcha es obligatorio" style="text-transform: none;" />
                                                        </div>
                                                    </div>
                                                </div>


                                            </div>
                                        </li>
                                    </ul>
                                    <!-- List group -->

                                </div>

                                <!--Fin Body-->
                            </div>

                            <!--Inicio Footer-->
                            <div class="modal-footer">
                                <div class="form-group">
                                    <div class="col-lg-9 col-lg-offset-3">
                                        <button id="hbtnCambioPassCaptcha" class="btn btn-sm btn_green_sop shadow" type="submit">
                                            Aceptar
                                        </button>
                                    </div>
                                </div>
                            </div>
                            <!--Fin Footer-->
                        </form>
                    </div>
                </div>
            </div>
            <!-------FIN AREA DIALOG CAPCHA---->

            <!--*********************************************-->
            <!--Dialog de confirmacion de cambio de contraseña-->
            <div class="modal fade" id="hdvConfirmacion" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog modal-sm">
                    <div class="modal-content">
                        <!--Inicio Header-->
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <h4 class="modal-title">Cambio de contraseña exitoso</h4>
                        </div>
                        <!--*****************-->
                        <!--Inicio Body-->
                        <div class="modal-body">
                            <p style="text-align: justify; color: rgb(31, 117, 148);"><span class="glyphicon glyphicon-ok-sign icon_green"></span><b>Su contraseña se ha cambiado correctamente. En estos momentos, se ha enviado un email con una contraseña temporal, la cual deberá utilizar la próxima vez que inicie sesión en el sistema.</b></p>
                            <!--Inicio Footer-->
                            <div class="modal-footer">
                                <div class="form-group">
                                    <div class="col-lg-9 col-lg-offset-3">
                                        <span class="btn btn-sm btn-primary shadow" onclick="javascript:$('#hdvConfirmacion').modal('toggle')">Aceptar</span>
                                    </div>
                                </div>
                            </div>
                            <!--Fin Footer-->
                        </div>
                        <!--Fin Body-->
                    </div>
                </div>
            </div>
            <!---->
            <!-------FIN AREA DIALOG---->



            <!--Dialog Cambio de Contraseña Inicio Sesion-->
            <div class="modal fade" id="hdvCambioPassword" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <!--Inicio Header-->
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <h4 id="hhTitulo" class="modal-title">Cambio de contraseña</h4>
                        </div>
                        <!--*****************-->
                        <form id="hfrmCambioPassInicio">
                            <!--Inicio Body-->
                            <div class="modal-body">
                                <input id="htxtAccionPass" type="hidden" />
                                <label><b id="hdvMensajeCambioPass"></b></label>

                                <div class="panel panel-default">
                                    <!-- Default panel contents -->
                                    <!-- List group -->
                                    <ul class="list-group">
                                        <li class="list-group-item">
                                            <span>Usuario:</span>
                                        </li>

                                        <li class="list-group-item">
                                            <div class="row form-group">
                                                <div class="col-lg-12">
                                                    <div id="">
                                                        <input type="text" id="htxtUsuarioCambioPassword" name="htxtUsuarioCambioPassword" class="form-control input-sm" disabled />
                                                    </div>
                                                </div>
                                            </div>
                                        </li>

                                        <li class="list-group-item">
                                            <label>Nueva contraseña:</label>
                                        </li>

                                        <li class="list-group-item">
                                            <div class="row form-group">
                                                <div class="col-lg-12">
                                                    <div id="hdvTxtCambioPassword" runat="server">
                                                        <input type="password" id="htxtPasswordSistema" name="htxtPasswordSistema" class="form-control input-sm" onkeyup=" $('#hfrmCambioPassInicio').data('bootstrapValidator').revalidateField('htxtPasswordSistemaRep')" />
                                                    </div>
                                                </div>
                                            </div>
                                        </li>

                                        <li class="list-group-item">
                                            <label>Repite Contraseña:</label>
                                        </li>

                                        <li class="list-group-item">
                                            <div>
                                                <div class="row form-group">
                                                    <div class="col-lg-12">

                                                        <div id="hdvTxtRepitePass">
                                                            <input type="password" id="htxtPasswordSistemaRep" name="htxtPasswordSistemaRep" class="form-control input-sm" onkeyup=" $('#hfrmCambioPassInicio').data('bootstrapValidator').revalidateField('htxtPasswordSistema')" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </li>
                                    </ul>
                                </div>

                                <!--Inicio Footer-->
                                <div class="modal-footer">
                                    <div class="form-group">
                                        <div class="col-lg-9 col-lg-offset-3">
                                            <button class="btn btn-greenS btn-sm" type="submit">
                                                Aceptar
                                            </button>
                                        </div>
                                    </div>
                                </div>
                                <!--Fin Footer-->
                            </div>
                            <!--Fin Body-->
                        </form>
                    </div>
                </div>
            </div>



        </section>
        <!----//////////////////////////////FIN CUERPO-->

        <!----//////////////////////////////INICIO FOOTER ---->
        <footer class="container-fluid footer_login_sop">

            <div class="col-xs-6 footer_left_sop">
                <p>NAD Global 2017. ©Todos los derechos reservados.</p>
            </div>
            <!--RIGHT footer-->
            <div class="col-xs-6 text-right text-xs-right footer_right_sop">
                <div class="footer_img_sop">
                </div>
            </div>
        </footer>
        <!----//////////////////////////////FIN FOOTER--->
    </form>
</body>
</html>
