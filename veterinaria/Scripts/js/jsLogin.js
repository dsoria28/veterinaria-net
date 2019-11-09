
//Inicio Document Ready
$(document).ready(function () {
    //se llaman a funciones
    //Validar form
    fn_validaInicioSesion();
    fn_validaCambioPassCaptcha();
    fn_validaCambioPassSistema();

    ///Funcion para validar el Login
    $("#txtID").keydown(function (e) {
        ///recupera el codigo
        var code = e.which;
        ///valida eventos
        if (e.keyCode == 13 || e.keyCode == 9 || code == 9) {
            ///llama funcion
            fn_validaIDSistema();            
        }
    }); 
        
});

//Funcion Para Abrir Dialog de Cambio obligatorio de Contraseña
function fn_abreDialogCambioPass(sMensaje,sTitulo,sOnclik) {
    //se agrega Mensaje
    $("#hdvMensajeCambioPass").html(sMensaje);
    //*********
    //se agrega titulo
    $("#hhTitulo > b").html(sTitulo);
    //********************
    //se agrega formato a boton
    $("#abtnEnviaCambio").attr("onclick", sOnclik);
    //$("#hbtnBotonConfig > b").html("Aceptar");
    //************
    //se abre dialog
    $("#hdvCambioPassword").modal("toggle");
    //****
}
//***************



//FUNCION PARA VALIDAR CAMPOS
function fn_ValidaForm(sContenido, sIdSmallError, sMensajeVacio, sExpresion, idInput, sIcono, sidButton, sRespuesta, sRespuestaConse) {
    //se valida los campos vacios
    if (sContenido.trim() == "") {
        $("#" + idInput).addClass("has-feedback has-error");
        $("#" + idInput).removeClass("has-success");
        $("#" + sIdSmallError).html(sMensajeVacio);
        $("#" + sIdSmallError).attr("style", "color: #a94442;");
        $("#" + sIcono).addClass("fa fa-times-circle icono-rojo");
        $("#" + sRespuesta).html("0");


        if ($("#" + sRespuestaConse).html() != "0" && $("#" + sRespuesta).html() != "0") {
            $("#" + sidButton).prop("disabled", false);
        } else {
            $("#" + sidButton).prop("disabled", true);
        }
        
        

    } else if (sExpresion != "") {
        var expre = new RegExp(sExpresion);
        if (expre.exec(sContenido)) {
            $("#" + idInput).addClass("has-feedback has-success");
            $("#" + idInput).removeClass("has-error");
            $("#" + sIdSmallError).html("");
            $("#" + sIcono).addClass("fa fa-check-circle icono-verde");
            $("#" + sRespuesta).html("1");

            if ($("#" + sRespuestaConse).html() != "0" && $("#" + sRespuesta).html() != "0") {
                $("#" + sidButton).prop("disabled", false);
            } else {
                $("#" + sidButton).prop("disabled", true);
            }

        } else {
            $("#" + idInput).addClass("has-feedback has-error");
            $("#" + idInput).removeClass("has-success");
            $("#" + sIdSmallError).html("Formato Invalido");
            $("#" + sIdSmallError).attr("style", "color: #a94442;");
            $("#" + sIcono).addClass("fa fa-times-circle icono-rojo");
            $("#" + sRespuesta).html("0");

            if ($("#" + sRespuestaConse).html() != "0" && $("#" + sRespuesta).html() != "0") {
                $("#" + sidButton).prop("disabled", false);
            } else {
                $("#" + sidButton).prop("disabled", true);
            }
        }
    }
    else {
        $("#" + idInput).addClass("has-feedback has-success");
        $("#" + idInput).removeClass("has-error");
        $("#" + sIdSmallError).html("");
        $("#" + sIdSmallError).attr("style", "color: #a94442;");
        $("#" + sIcono).addClass("fa fa-check-circle icono-verde");
        $("#" + sRespuesta).html("1");
        if ($("#" + sRespuestaConse).html() != "0" && $("#" + sRespuesta).html() != "0") {
            $("#" + sidButton).prop("disabled", false);
        } else {
            $("#" + sidButton).prop("disabled", true);
        }
    }
}
//**************

//funcion para validar Contraseña de Cambio
function fn_ValidaPassword(sContenido,sIdInput) {
    var sPassword = $("#txtCambioPassword").val();

    if (sContenido != sPassword) {
        $("#" + sIdInput).attr("class", "has-error");
        $("#" + sIdInput + " > i").html("Las contraseñas no concuerdan o esta vacia");
        $("#" + sIdInput + " > i").attr("style", "color: #a94442;");
        $("#" + sIdInput +" > span").attr("class", "glyphicon glyphicon-remove form-control-feedback");
        $("#" + sIdInput + " > small").html("0");


        if ($("#" + sIdInput + " > small").html() != "0" && $("#hsmRespuestaCambioPass").html() != "0") {
            $("#abtnCambioPassSistema").prop("disabled", false);
        } else {
            $("#abtnCambioPassSistema").prop("disabled", true);
        }


    } else {
        $("#" + sIdInput).attr("class", "has-success");
        $("#" + sIdInput + " > i").html("");
        $("#" + sIdInput + " > i").attr("style", "color: #a94442;");
        $("#" + sIdInput + " > span").attr("class", "glyphicon glyphicon-ok form-control-feedback");
        $("#" + sIdInput + " > small").html("1");

        if ($("#" + sIdInput + " > small").html() != "0" && $("#hsmRespuestaCambioPass").html() != "0") {
            $("#abtnCambioPassSistema").prop("disabled", false);
        } else {
            $("#abtnCambioPassSistema").prop("disabled", true);
        }
    }
}
//*****************


/*Funcion para validar Formulario de inicio de sesion*/
function fn_validaInicioSesion() {
    
    $('#hfrmInicioSesion').bootstrapValidator({
        message: 'El valor es inválido.',
        feedbackIcons: {
            valid: 'fa fa-check-circle icono-verde',
            invalid: 'fa fa-times-circle icono-rojo',
            validating: 'glyphicon glyphicon-refresh'
        },
        live: 'enabled',
        fields: {
            txtLogin: {
                validators: {
                    //vacio
                    notEmpty: {
                        message: "Favor de ingresar un Usuario"
                    },
                    //caracteres y formato
                    regexp: {
                        regexp: /^[0-9a-zA-Z._-]+$/i,
                        message: 'Formato Invalido.'
                    }
                }
            },
            txtPassword: {
                validators: {
                    //vacio
                    notEmpty: {
                        message: "Favor de ingresar un ID de Tarjeta"
                    }
                }
            }
        }
    }).on('success.form.bv', function (e) {
        e.preventDefault();
        //se llama funcion de iniciar sesion
        fn_inicioSesionSistema();
    });
}//Fin FUNCION============================================>

/*Funcion para validar Formulario de cambio de contraseña desde link*/
function fn_validaCambioPassCaptcha() {
    $('#hfrmCambioPasswordCaptcha').bootstrapValidator({
        message: 'El valor es inválido.',
        feedbackIcons: {
            valid: 'fa fa-check-circle icono-verde',
            invalid: 'fa fa-times-circle icono-rojo',
            validating: 'glyphicon glyphicon-refresh'
        },
        fields: {
            htxtUsuario: {
                message: 'El nombre de usuario es invalido',
                validators: {
                }
            }
        }
    }).on('success.form.bv', function (e) {
        e.preventDefault();
        //se llama funcion de iniciar sesion
        //fn_cambioPassCaprcha();
    });
}//Fin FUNCION============================================>


/*Funcion para validar Formulario de cambio de contraseña desde inicio sistema*/
function fn_validaIDSistema() {

    ////Valida información
    $('#hfrmInicioSesionID').bootstrapValidator({
        message: 'El valor es inválido.',
        feedbackIcons: {
            valid: 'fa fa-check-circle icono-verde',
            invalid: 'fa fa-times-circle icono-rojo',
            validating: 'glyphicon glyphicon-refresh'
        },
        live: 'enabled',
        fields: {
            txtID: {
                validators: {
                    //vacio
                    notEmpty: {
                        message: "Favor de ingresar un ID de Tarjeta"
                    },
                    //caracteres y formato
                    regexp: {
                        regexp: /^[0-9a-zA-Z]+$/i,
                        message: 'Formato Invalido.'
                    }
                }
            }
        }
    }).on('success.form.bv', function (e) {
        //se llama funcion de iniciar sesion
        e.preventDefault();
        $('#hfrmInicioSesionID').bootstrapValidator("destroy", true); 
        ///valida información
        fn_inicioSesionSistema_ID();
    });
}//Fin FUNCION============================================>




/*Funcion para validar Formulario de cambio de contraseña desde inicio sistema*/
function fn_validaCambioPassSistema() {
    $('#hfrmCambioPassInicio').bootstrapValidator({
        message: 'El valor es inválido.',
        feedbackIcons: {
            valid: 'fa fa-check-circle icono-verde',
            invalid: 'fa fa-times-circle icono-rojo',
            validating: 'glyphicon glyphicon-refresh'
        },
        fields: {
            htxtPasswordSistema: {
                validators: {
                    //vacio
                    notEmpty: {
                        message: "Favor de ingresar Contraseña"
                    },
                    //caracteres y formato
                    regexp: {

                        regexp: /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&_+.\s/#.-])([A-Za-z\d$@$!%*?&_+.\s/#.-]|[^ ]){8,80}$/,
                        message: 'Formato invalido de contraseña, debe contener mínimo 8 digitos, entre los cuales debe encontrase minimo 1 letra minuscula, 1 mayuscula, 1 caracter especial y 1 numero'
                    },
                    //*********************
                    //total de caracteres
                    stringLength: {
                        min: 8,
                        message: "Contraseña debe contener 8 o más caracteres"
                    }
                    //*****
                    ,
                    identical: {
                        field: "txtConfirmaContraseña",
                        message: "Tu contraseña no concuerda con la confirmación"
                    }
                }
            },
            htxtPasswordSistemaRep: {
                validators: {
                    //vacio
                    notEmpty: {
                        message: "Favor de Confirmar Contraseña"
                    },
                    //******
                    //contraseñas iguales
                    identical: {
                        field: "htxtPasswordSistema",
                        message: "Tus contraseñas no concuerdan"
                    }
                    //********
                }
            }
        }
    }).on('success.form.bv', function (e) {
        e.preventDefault();
        //se llama funcion de iniciar sesion
        var iAccion = $("#htxtAccionPass").val();
        fn_cambioPassInicioSistema(iAccion);
    });
}//Fin FUNCION============================================>

/*Funcion para entrar al sistema*/
function fn_inicioSesionSistema_ID() {
    //se bloquea Pantalla
    $.bloquearPantalla("Validando...");
    //Inicio TRY
    try {
        //Se recuperan datos
        var sID = $("#txtID").val();
        var sDatos = "{sID:'" + sID + "'}";
        //Inicio llamada AJAX
        $.ajax({
            url: "LoginID.aspx/fn_inicioSesionSistema",
            data: sDatos,
            type: "POST",
            dataType: "JSON",
            contentType: "application/json; charset=utf-8;",
            success: function (data) {
                //se desbloquea la Pantalla
                $.desbloquearPantalla();
                //************
                //se manda notificacion
                $.notificacionMsj(data.d.gsiResultado, data.d.gssMensaje);
                //====================
                //se valida el resultado
                if (data.d.gsiResultado == 1) {
                    //se redirecciona a inicio
                    $(location).attr("href", "Vista/Entregas/entregas_cliente.aspx");
                } else {
                    $("#txtID").val('');
                }
            },
            error: function (xhr, status) {
                //se desbloquea la Pantalla
                $.desbloquearPantalla();
                //************
                //Inicio TRY
                try {
                    //se recupera el error
                    var err = eval("(" + xhr.responseText + ")");
                    //Se muestra notificación de error
                    $.notificacionMsj(3, "Error al intentar iniciar sesión:" + err.Message);
                    //***********************
                }//FIN TRY
                //Inico CATCH
                catch (err) {
                    //Se muestra notificación de error
                    $.notificacionMsj(3, "Error al intentar iniciar sesión:" + err.message);
                    /*===============*/
                }//FIN CATCH
            }
        });
        //***********
    }//=================
    //Inico CATCH
    catch (err) {
        //se desbloquea pantalla
        $.desbloquearPantalla();
        //se manda notificacion
        $.notificacionMsj(3, "Error al intentar iniciar sesión:" + err.message);
    }

}//FIN FUNCION==============================================>



/*Funcion para entrar al sistema*/
function fn_inicioSesionSistema() {
    //se bloquea Pantalla
    $.bloquearPantalla("Iniciando sesión...");
    //Inicio TRY
    try {
        //Se recuperan datos
        var sUsuario = $("#txtLogin").val();
        var sPassword = $("#txtPassword").val();
        var sDatos = "{sUsuario:'" + sUsuario + "',sPassword:'" + sPassword + "'}";
        //Inicio llamada AJAX
        $.ajax({
            url: "Login.aspx/fn_inicioSesionSistema",
            data: sDatos,
            type: "POST",
            dataType: "JSON",
            contentType: "application/json; charset=utf-8;",
            success: function (data) {
                //se desbloquea la Pantalla
                $.desbloquearPantalla();
                //************
                //se manda notificacion
                $.notificacionMsj(data.d.gsiResultado, data.d.gssMensaje);
                //====================
                //se valida el resultado
                if (data.d.gsiResultado == 1) {
                    //se redirecciona a inicio
                    $(location).attr("href", "Vista/Inicio/Inicio.aspx");
                }
                else if (data.d.gsiResultado == 4) {
                    //se agregan valores al dialog
                    //se agrega Mensaje
                    $("#hdvMensajeCambioPass").html(data.d.gssContenido);
                    //*********
                    //se agrega titulo
                    $("#hhTitulo").html("Cambio de contraseña");
                    //********************
                    //se abre dialog
                    $("#hdvCambioPassword").modal("toggle");
                    //se agrega usuario 
                    $("#htxtUsuarioCambioPassword").val(data.d.gssUsuario);
                    //****
                    setTimeout(function () {
                        $('#hfrmCambioPassInicio').data("bootstrapValidator").resetForm(true);
                    }, 180);
                    $("#htxtAccionPass").val(data.d.gsiAccion);

                }
                else {
                    $("#hbtnIniciaSesion").prop("disabled",false);
                }

            },
            error: function (xhr, status) {
                //se desbloquea la Pantalla
                $.desbloquearPantalla();
                //************
                //Inicio TRY
                try {
                    //se recupera el error
                    var err = eval("(" + xhr.responseText + ")");
                    //Se muestra notificación de error
                    $.notificacionMsj(3, "Error al intentar iniciar sesión:" + err.Message);
                    //***********************
                }//FIN TRY
                //Inico CATCH
                catch (err) {
                    //Se muestra notificación de error
                    $.notificacionMsj(3, "Error al intentar iniciar sesión:" + err.message);
                    /*===============*/
                }//FIN CATCH
            }
        });
        //***********
    }//=================
    //Inico CATCH
    catch (err) {
        //se desbloquea pantalla
        $.desbloquearPantalla();
        //se manda notificacion
        $.notificacionMsj(3, "Error al intentar iniciar sesión:" + err.message);
    }

}//FIN FUNCION==============================================>

/*Funcion para abrir dialog de cambio de contraseña*/
function fn_abreDialogCambioPassword(iOpcionActual) {
    //se abre dialog
    $("#hdvDialogCambioPass").modal("toggle");
    //se resetea form
    setTimeout(function () {
        $('#hfrmCambioPasswordCaptcha').data("bootstrapValidator").resetForm(true);
    }, 180);
    //se crea nuevo captcha
    fn_nuevoCodigoCaptcha(iOpcionActual);
}//FIN FUNCION==============================================>

/*Funcion para abrir dialog de cambio de contraseña*/
function fn_nuevoCodigoCaptcha(iOptionActual) {
    //se crea consecutivo
    var iConsecutivo = iOptionActual + 1;
    //se crea nueva imagen
    $("#imageCaptcha").attr("src","Vista/Utilerias/CrearCaptcha.aspx?New="+iConsecutivo);
    //se resetea campo de captcha
    setTimeout(function () {
        $('#hfrmCambioPasswordCaptcha').data("bootstrapValidator").updateStatus("htxtCaptcha", "not_validate");
    }, 180);
    //se resetan valores
    fn_resetFuncionesCaptcha(iConsecutivo);
}//FIN  FUNCION==============================================>

/*Funcion pra resetear funciones de nuevo capcha*/
function fn_resetFuncionesCaptcha(iConsecutivo) {
    //se cambian valores
    $("#haOlvidastePass").attr("onclick", "fn_abreDialogCambioPassword(" + iConsecutivo + ");");
    $("#haNuevoCaptcha").attr("onclick", "fn_nuevoCodigoCaptcha(" + iConsecutivo + ");");
    $("#hbtnCambioPassCaptcha").attr("onclick", "fn_validaFormCambioPass(" + iConsecutivo + ");");

}//FIN FUNCION==============================================>

/*Funcion para cambiar contraseña*/
function fn_cambioPassCaptcha(iConsecutivo) {
    //se bloquea Pantalla
    $.bloquearPantalla("Cargando...");
    //Inicio TRY
    try {
        //Se recuperan datos
        var sUsuario = $("#htxtUsuario").val();
        var sPassword = $("#htxtCaptcha").val();
        var sDatos = "{sUsuario:'" + sUsuario + "',sCaptcha:'" + sPassword + "'}";
        //Inicio llamada AJAX
        $.ajax({
            url: "Login.aspx/fn_validaDatosCambioPassCaptcha",
            data: sDatos,
            type: "POST",
            dataType: "JSON",
            contentType: "application/json; charset=utf-8;",
            success: function (data) {
                //se desbloquea la Pantalla
                $.desbloquearPantalla();
                //************
                //se manda notificacion
                $.notificacionMsj(data.d.gsiResultado, data.d.gssMensaje);
                //====================
                //se valida el resultado
                if (data.d.gsiResultado == 1) {
                    //se cierra dialog
                    $("#hdvDialogCambioPass").modal("toggle");
                    //se abre dialog de confirmacion 
                    $("#hdvConfirmacion").modal("toggle");
                }
                else {
                    //se genera nuevo captcha
                    fn_nuevoCodigoCaptcha(iConsecutivo);
                    //se resetea form
                    $('#hfrmCambioPasswordCaptcha').data("bootstrapValidator").resetForm(true);
                }

            },
            error: function (xhr, status) {
                //se desbloquea la Pantalla
                $.desbloquearPantalla();
                //************
                //Inicio TRY
                try {
                    //se recupera el error
                    var err = eval("(" + xhr.responseText + ")");
                    //Se muestra notificación de error
                    $.notificacionMsj(3, "Error al cambiar contraseña:" + err.Message);
                    //***********************
                }//FIN TRY
                //Inico CATCH
                catch (err) {
                    //Se muestra notificación de error
                    $.notificacionMsj(3, "Error al cambiar contraseña:" + err.message);
                    /*===============*/
                }//FIN CATCH
            }
        });
        //***********
    }//=================
    //Inico CATCH
    catch (err) {
        //se desbloquea pantalla
        $.desbloquearPantalla();
        //se manda notificacion
        $.notificacionMsj(3, "Error al cambiar contraseña:" + err.message);
    }

}//FIN FUNCION==============================================>

/*funcion para validar el formulario de cambio de contraseña*/
function fn_validaFormCambioPass(iConsecutivo) {
    $('#hfrmCambioPasswordCaptcha').data("bootstrapValidator").validate();
    var isValid = false;
    isValid = $('#hfrmCambioPasswordCaptcha').data("bootstrapValidator").isValid();
    if (isValid) {
        fn_cambioPassCaptcha(iConsecutivo);
    }
    else {
        
    }
}//FIN FUNCION==============================================>

/*Funcion para cambiar contraseña inicio sistema*/
function fn_cambioPassInicioSistema(iAccion) {
    //se bloquea Pantalla
    $.bloquearPantalla("Cargando...");
    //Inicio TRY
    try {
        //Se recuperan datos
        var sUsuario = $("#htxtUsuarioCambioPassword").val();
        var sPassword = $("#htxtPasswordSistema").val();
        var sDatos = "{sUsuario:'" + sUsuario + "',sPassword:'" + sPassword + "',iAccion:"+iAccion+"}";
        //Inicio llamada AJAX
        $.ajax({
            url: "Login.aspx/fn_cambiaPassInicio",
            data: sDatos,
            type: "POST",
            dataType: "JSON",
            contentType: "application/json; charset=utf-8;",
            success: function (data) {
                //se desbloquea la Pantalla
                $.desbloquearPantalla();
                //************
                //se manda notificacion
                $.notificacionMsj(data.d.gsiResultado, data.d.gssMensaje);
                //====================
                //se abre dialog
                $("#hdvCambioPassword").modal("toggle");
                //se valida el resultado
                if (data.d.gsiResultado == 1) {
                    //se redirecciona a inicio
                    $(location).attr("href", "Vista/Inicio/Inicio.aspx");
                }
                else if (data.d.gsiResultado == 4) {
                    //se agregan valores al dialog
                    //se agrega Mensaje
                    $("#hdvMensajeCambioPass").html(data.d.gssContenido);
                    //*********
                    //se agrega titulo
                    $("#hhTitulo").html("Cambio de contraseña");
                    //********************
                    //se abre dialog
                    $("#hdvCambioPassword").modal("toggle");
                    //se agrega usuario 
                    $("#htxtUsuarioCambioPassword").val(data.d.gssUsuario);
                    //****
                    setTimeout(function () {
                        $('#hfrmCambioPassInicio').data("bootstrapValidator").resetForm(true);
                    },180);
                }
                else {
                }

            },
            error: function (xhr, status) {
                //se desbloquea la Pantalla
                $.desbloquearPantalla();
                //************
                //Inicio TRY
                try {
                    //se recupera el error
                    var err = eval("(" + xhr.responseText + ")");
                    //Se muestra notificación de error
                    $.notificacionMsj(3, "Error al cambiar contraseña:" + err.Message);
                    //***********************
                }//FIN TRY
                //Inico CATCH
                catch (err) {
                    //Se muestra notificación de error
                    $.notificacionMsj(3, "Error al cambiar contraseña:" + err.message);
                    /*===============*/
                }//FIN CATCH
            }
        });
        //***********
    }//=================
    //Inico CATCH
    catch (err) {
        //se desbloquea pantalla
        $.desbloquearPantalla();
        //se manda notificacion
        $.notificacionMsj(3, "Error al cambiar contraseña:" + err.message);
    }

}//FIN FUNCION==============================================>



