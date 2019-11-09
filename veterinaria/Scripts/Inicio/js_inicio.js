$(document).ready(function () {
    ////Función que muestra el inicio del usuario dependiendo la configuración
    //fn_mostrar_pantalla_inicio();
    ////Función que escucha cuando se selecciona un item en el combobox addlPantallaInicio
    //$("#addlPantallasFrecuentes").change(function () {
    //    var iIdPantallaFrecuente = $("#addlPantallasFrecuentes option:selected").val();
    //    if (iIdPantallaFrecuente != "")
    //    //Se manda llamar la función que valida el poder guardar una pantalla frecuente
    //        fn_validar_guardar_pantalla_frecuente(iIdPantallaFrecuente);
    //});
    ////Evento al cerrar dialog de agregar pantallas frecuentes
    //$('#dialogSeleccionarPantallaFrecuente').on('hidden.bs.modal', function () {
    //    //Función que muestra el inicio del usuario dependiendo la configuración
    //    fn_mostrar_pantalla_inicio();
    //});
    //$("#addlPantallaInicio").select2({
    //    width: "100%",
    //    placeholder: "Selecionar..."
    //});

    //$("#addlPantallasFrecuentes").select2({
    //    width: "100%",
    //    placeholder: "Selecionar..."
    //});

    //$(".htblDetalleAviso").DataTable({
    //    "iDisplayLength": -1,
    //    "paging": false
    //});
});

//Función para guardar la pantalla de inicio
function fn_guardar_pantalla_inicio() {
    //Obtener el id de la pantalla seleccionada
    var iIdPantallaInicio = $("#addlPantallaInicio option:selected").val();
    //Obtener id del usuario en sesión
    var sIdUsuario = $("#ahddUser").val();
    //Se bloquea la pantalla
    $.bloquearPantalla("Guardando...");
    $.ajax({
        url: "Inicio.aspx/fn_guardar_pantalla_inicio",
        type: "POST",
        dataType: "JSON",
        data: " { iIdPantallaInicio:" + iIdPantallaInicio + ", sIdUsuario:'" + sIdUsuario + "' } ",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            //Desbloquear pantalla
            $.desbloquearPantalla();
            //Verificar si se ejecuto correctamente el procedimiento
            if (data.d.iResultado == 1) {
                //Se actualiza el contenido de la pantalla
                fn_mostrar_pantalla_inicio();
                //Cerrar dialog guardar editar oficina
                $('#dialogConfigurarInicio').modal('hide');
                //Notificacióon de éxito
                $.notificacionMsj(1, data.d.sMensaje);
            } else {
                //Notificacióon de error
                $.notificacionMsj(3, data.d.sMensaje);
            }
        }, error: function (xhr, status, error) {
            //Desbloquear pantalla
            $.desbloquearPantalla();
            var err = eval("(" + xhr.responseText + ")");
            //Notificación error
            $.notificacionMsj(3, err.Message);
        }
    });
}

//Función para mostrar la pantalla de inicio
function fn_mostrar_pantalla_inicio() {
    //Obtener id del usuario en sesión
    var sIdUsuario = $("#ahddUser").val();
    //Se bloquea la pantalla
    $.bloquearPantalla("Cargando...");
    $.ajax({
        url: "Inicio.aspx/fn_mostrar_pantalla_inicio",
        type: "POST",
        dataType: "JSON",
        data: " { sIdUsuario:'" + sIdUsuario + "' } ",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            //Desbloquear pantalla
            $.desbloquearPantalla();
            //Verificar si se ejecuto correctamente el procedimiento
            if (data.d.iResultado == 1) {
                //Depediendo la pantalla configurada para el usuario se mostrará
                if (data.d.iIdPantallaInicio == "0") {
                    //Se oculta div de tramitador
                    $("#dvPendientes").hide();
                    //Se pone vacia la vista de inicio
                    $("#hlblVistaInicio").html("");
                } else if (data.d.iIdPantallaInicio == "1") {
                    //Se oculta div de tramitador
                    $("#dvPendientes").hide();
                    //Se llama el método para mostrar las pantallas frecuentes del usuario
                    fn_vista_pantallas_frecuentes();
                } else if (data.d.iIdPantallaInicio == "2") {
                    ///Se llama al método para generar la lista
                    fn_generar_lista_previo()
                    //Limpiar pantalla 
                    $("#hlblVistaInicio").html("");
                    //Se muestra div de tramitador
                    $("#dvPendientes").show();
                } else {
                    //Se oculta div de tramitador
                    $("#dvPendientes").hide();
                    //Se pone vacia la vista de inicio
                    $("#hlblVistaInicio").html("");
                }
            } else {
                //Notificacióon de error
                $.notificacionMsj(3, data.d.sMensaje);
            }
        }, error: function (xhr, status, error) {
            //Desbloquear pantalla
            $.desbloquearPantalla();
            var err = eval("(" + xhr.responseText + ")");
            //Notificación error
            $.notificacionMsj(3, err.Message);
        }
    });
}

//Función para pendientes de previo
function fn_pendientes_previo() {
    //Obtener id del usuario en sesión
    var sIdUsuario = $("#ahddUser").val();
    //Se bloquea la pantalla
    $.bloquearPantalla("Cargando...");
    $.ajax({
        url: "Inicio.aspx/fn_pendientes_previo",
        type: "POST",
        dataType: "JSON",
        data: " { sIdUsuario:'" + sIdUsuario + "' } ",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            //Desbloquear pantalla
            $.desbloquearPantalla();
            //Verificar si se ejecuto correctamente el procedimiento
            if (data.d.iResultado == 1) {
                //Se pone vacia la vista de inicio
                $("#hlblVistaInicio").html(data.d.sContenido);
            } else {
                //Notificación de error
                $.notificacionMsj(3, data.d.sMensaje);
            }
        }, error: function (xhr, status, error) {
            //Desbloquear pantalla
            $.desbloquearPantalla();
            var err = eval("(" + xhr.responseText + ")");
            //Notificación error
            $.notificacionMsj(3, err.Message);
        }
    });
}

//Función para mostrar la vista Pantallas Frecuentes
function fn_vista_pantallas_frecuentes() {
    //Obtener id del usuario en sesión
    var sIdUsuario = $("#ahddUser").val();
    //Se bloquea la pantalla
    $.bloquearPantalla("Cargando...");
    $.ajax({
        url: "Inicio.aspx/fn_vista_pantallas_frecuentes",
        type: "POST",
        dataType: "JSON",
        data: " { sIdUsuario:'" + sIdUsuario + "' } ",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            //Desbloquear pantalla
            $.desbloquearPantalla();
            //Verificar si se ejecuto correctamente el procedimiento
            if (data.d.iResultado == 1) {
                //Se pone vacia la vista de inicio
                $("#hlblVistaInicio").html(data.d.sContenido);
            } else {
                //Notificación de error
                $.notificacionMsj(3, data.d.sMensaje);
            }
        }, error: function (xhr, status, error) {
            //Desbloquear pantalla
            $.desbloquearPantalla();
            var err = eval("(" + xhr.responseText + ")");
            //Notificación error
            $.notificacionMsj(3, err.Message);
        }
    });
}

//Función para generar la tabla de pantallas frecuentes
function fn_generar_lista_pantallas_frecuentes() {
    $.ajax({
        url: "Inicio.aspx/fn_generar_lista_pantallas_frecuentes",
        type: "POST",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            //Se crea la estructura de la tabla
            $("#hlblListaPantallasFrecuentes").html(data.d.sContenido);
            //Se llena con datos del handler la tabla
            fn_listar_pantallas_frecuentes();
        }, error: function (xhr, status, error) {
            var err = eval("(" + xhr.responseText + ")");
            $.notificacionMsj(3, err.Message);
        }
    });
}

//Función para listar pantallas frecuentes del usuario
function fn_listar_pantallas_frecuentes() {
    //Obtener id del usuario en sesión
    var sIdUsuario = $("#ahddUser").val();
    //Función para generar tabla pantallas frecuentes
    $("#htblPantallasFrecuentes").dataTable({
        "JQueryUI": true,
        "processing": true,
        "serverSide": true,
        "ajax": {
            "url": "../../Handlers/Inicio/h_listar_pantallas_frecuentes.ashx",
            "type": "POST",
            "data": {
                "sIdUsuario": sIdUsuario
            }
        },
        "columns": [
                { "data": "sNombrePantallaFrecuente" },
                { "data": "sMenu" },
                { "data": "iIdPantallaEliminar" }
            ]
    });
    var table = $('#htblPantallasFrecuentes').DataTable();
    $("#htblPantallasFrecuentes_filter").hide();
    table.columns().eq(0).each(function (colIdx) {
        $('input', table.column(colIdx).footer()).on('keydown', function (e) {
            var code = e.which;
            if (code == 9) {
                table.column(colIdx).search(this.value).draw();
            }
        });
    });
}

//Función para validar el guardar la pantalla frecuente
function fn_validar_guardar_pantalla_frecuente(iIdPantallaFrecuente) {
    //Obtener id del usuario en sesión
    var sIdUsuario = $("#ahddUser").val();
    //Se bloquea la pantalla
    $.bloquearPantalla("Cargando...");
    $.ajax({
        url: "Inicio.aspx/fn_validar_agregar_pantalla_frecuente",
        type: "POST",
        dataType: "JSON",
        data: " { iIdPantallaFrecuente:" + iIdPantallaFrecuente + ", sIdUsuario:'" + sIdUsuario + "' } ",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            //Desbloquear pantalla
            $.desbloquearPantalla();
            //Verificar si se ejecuto correctamente el procedimiento
            if (data.d.iResultado == 1) {
                if (data.d.sMensaje == "0")
                    //Función para agregar la pantalla frecuente
                    fn_guardar_eliminar_pantalla_frecuente(1);
                else
                    //Notificación de alerta
                    $.notificacionMsj(2, data.d.sContenido);
            } else {
                //Notificación de error
                $.notificacionMsj(3, data.d.sMensaje);
            }
        }, error: function (xhr, status, error) {
            //Desbloquear pantalla
            $.desbloquearPantalla();
            var err = eval("(" + xhr.responseText + ")");
            //Notificación error
            $.notificacionMsj(3, err.Message);
        }
    });
}

//Función para guardar o eliminar la pantalla frecuente del usuario
function fn_guardar_eliminar_pantalla_frecuente(type) {
    //Obtener id del usuario en sesión
    var sIdUsuario = $("#ahddUser").val();
    //Se obtiene el id de la pantalla frecuente 
    if (type == 1)
        var iIdPantallaFrecuente = $("#addlPantallasFrecuentes option:selected").val();
    else
        var iIdPantallaFrecuente = $("#ahddIdPantallaFrecuente").val();
    //Se insancia la tabla
    var table = $('#htblPantallasFrecuentes').DataTable();
    //Se bloquea la pantalla
    $.bloquearPantalla("Cargando...");
    $.ajax({
        url: "Inicio.aspx/fn_guardar_eliminar_pantalla_frecuente",
        type: "POST",
        dataType: "JSON",
        data: " { iIdPantallaFrecuente:" + iIdPantallaFrecuente + ", sIdUsuario:'" + sIdUsuario + "', type:" + type + " } ",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            //Se actualiza la tabla
            table.draw();
            //Desbloquear pantalla
            $.desbloquearPantalla();
            //Verificar si se ejecuto correctamente el procedimiento
            if (data.d.iResultado == 1) {
                if (type == 1)
                    //Se limpia el combobox
                    $("#addlPantallasFrecuentes").prop('selectedIndex', 0);
                else
                    //Cerrar dialog eliminar pantalla frecuente
                    $('#dialogEliminarPantallaFrecuente').modal('hide');
                //Notificación de éxito
                $.notificacionMsj(1, data.d.sMensaje);
            } else {
                //Notificación de error
                $.notificacionMsj(3, data.d.sMensaje);
            }
        }, error: function (xhr, status, error) {
            //Desbloquear pantalla
            $.desbloquearPantalla();
            var err = eval("(" + xhr.responseText + ")");
            //Notificación error
            $.notificacionMsj(3, err.Message);
        }
    });
}

//Función para obtener el id de la pantalla a eliminar
function fn_obtener_id_pantalla_frecuente_eliminar(iIdPantallaFrecuente) {
    $("#ahddIdPantallaFrecuente").val(iIdPantallaFrecuente);
}

function fn_actualizar_estatus_aviso_usuario() {
    ///Oculta modal
    $("#hdvAvisosUsuario").modal("hide");
    //Obtener id del usuario en sesión
    var sIdUsuario = $("#ahddUser").val();
    //Se bloquea la pantalla
    $.bloquearPantalla("Cargando...");
    $.ajax({
        url: "Inicio.aspx/fn_actualizar_estatus_aviso_usuario",
        type: "POST",
        dataType: "JSON",
        data: " { sIdUsuario:'" + sIdUsuario + "' } ",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            //Desbloquear pantalla
            $.desbloquearPantalla();
            //Notificación de error
            if (data.d.iResultado != 1)
                $.notificacionMsj(data.d.iResultado, data.d.sMensaje);

        }, error: function (xhr, status, error) {
            //Desbloquear pantalla
            $.desbloquearPantalla();
            var err = eval("(" + xhr.responseText + ")");
            //Notificación error
            $.notificacionMsj(3, err.Message);
        }
    });
}