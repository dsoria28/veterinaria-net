$(document).ready(function () {
    /*======================INICIO DE DOCUMENT======================*/
    ///TAMAÑO AL 50% DEFAULT
    $("#hrgAjustaTamano").val(0);
    $("#htxtOpcionAjuste").val(0);
    fn_aumentaDisminuye_tamanio(0, 12, 1);
    ///
    $("#hrgAjustaTamano").change(function () {
        //alert($("#hrgAjustaTamano").val());
        var iTamano = parseInt($("#hrgAjustaTamano").val());
        var iOpcion = parseInt($("#htxtOpcionAjuste").val());
        //fn_Aumenta_Disminuye_Tamano(iTamano, 12, iOpcion);
        fn_aumentaDisminuye_tamanio(iTamano, 12, iOpcion);
    });
    fn_CreaEtiquetaRange("hrgAjustaTamano");
    /*========================FIN DE DOCUMENT======================*/
});

//Función para generar la tabla de pantallas frecuentes
function fn_generar_lista_previo() {
    $.ajax({
        url: "Inicio.aspx/fn_generar_lista_previo",
        type: "POST",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            //Se crea la estructura de la tabla
            $("#alblPendientesTramitador").html(data.d.sContenido);
            //Se llena con datos del handler la tabla
            fn_listar_actividades();
        }, error: function (xhr, status, error) {
            var err = eval("(" + xhr.responseText + ")");
            $.notificacionMsj(3, err.Message);
        }
    });
}

/*========================INICIO FUNCIÓN PARA LISTAR ACTIVIDADES PENDIENTES======================*/
function fn_listar_actividades() {
    var iIdOficina = 0;
    ///Se crea dataTable con filtros de busqueda
    var selected = [];
    $("#tb_tramitador").dataTable({
        //"searching": false,
        "jQueryUI": true,
        //"scrollX": true,
        "processing": true,
        "serverSide": true,
        "ajax": {
            "url": "../../Handlers/Previo/h_pendientes_tramitador.ashx",
            "type": "POST",
            "data": { "iIdUser": '' + $("#ahddUser").val() + '', "iIdAction": '' + $("#ahdActionPrevio").val() + '', "iIdOficina": '' + iIdOficina + '' },
            "rowCallback": function (row, data) {
                if ($.inArray(data.DT_RowId, selected) !== -1) {
                    $(row).addClass('selected');
                }
            }
        },
        "initComplete": function () {
            $("[data-toggle='tooltip']").tooltip();
        },
        "columns": [
                { "data": "sGuiaMaster" },
                { "data": "sGuiaHouse" },
                { "data": "sNombreCliente" },
                { "data": "iNumPrevio" },
                { "data": "sFueParcial" },
                { "data": "iBultosActuales" },
                /*{ "data": "rPeso" },*/
                { "data": "rValorDolares" },
                { "data": "sPredocumentacion" },
                { "data": "dFechaEntrada" },
                { "data": "sTiempo" },
                { "data": "iIdActividadUsuario" }
        ]
    });
    // DataTable
    var table = $('#tb_tramitador').DataTable();
    ///Se Aplica la búsqueda
    table.columns().eq(0).each(function (colIdx) {
        $('input', table.column(colIdx).footer()).on('keydown', function (e) {
            var code = e.which;
            if (e.keyCode == 13 || code == 9) {
                table.column(colIdx).search(this.value).draw();
            }
        });
    });
}
/*========================FIN FUNCIÓN PARA LISTAR ACTIVIDADES PENDIENTES======================*/

/*========================INICIO FUNCIÓN PARA RECUPERAR DATOS======================*/
function fn_recupera_datos(idActividad) {
    ///Se asigna valor al hidden
    $("#ahddIdActividad").val(idActividad);
}
/*========================FIN FUNCIÓN PARA RECUPERAR DATOS======================*/

/*========================INICIO FUNCIÓN PARA INICIAR UNA ACTIVIDAD======================*/
function fn_inicia_actividad(iType) {
    ///Se recupera el valor del ID
    var idActividad = $("#ahddIdActividad").val();
    ///Se bloquea pantalla 
    $.bloquearPantalla("Cargando...");
    $.ajax({
        url: "Inicio.aspx/fn_inicia_actividad_pendiente",
        type: "POST",
        data: "{ sIdActividad:'" + idActividad + "', iType:" + iType + " }",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            ///Si el iResultado es exitoso
            if (data.d.iResultado == 1) {
                ///Se cierra el dialog
                $('#hdvIniciarActividad').modal('hide');
                $('#hdvConfirmaNuevaActividad').modal('hide');
                ///Se manda mensaje de confirmación
                $.notificacionMsj(1, data.d.sMensaje);
                //Se redirecciona a detalle de guía
                $(location).attr("href", "../Previo/detalle_guias.aspx?sTypeCtrlAc=MQA0AA==&sIdGuia=" + data.d.gsIdEncActividad);
            } else if (data.d.iResultado == 2) {
                ///Se cierra el dialog
                $('#hdvIniciarActividad').modal('hide');
                //$("#hbtnDialogAct").val();
                $('#hbtnDialogAct').trigger('click');
                ///Se manda mensaje de confirmación
                //$('#hdvConfirmaNuevaActividad').modal('show');
                //$.notificacionMsj(data.d.iResultado, data.d.sMensaje); 
            } else if (data.d.iResultado == 3) {
                ///Se cierra el dialog
                $('#hdvIniciarActividad').modal('hide');
                $('#hdvConfirmaNuevaActividad').modal('hide');
                ///Se manda mensaje de confirmación
                $.notificacionMsj(2, data.d.sMensaje);
            } else {
                ///Se cierra el dialog
                $('#hdvIniciarActividad').modal('hide');
                $('#hdvConfirmaNuevaActividad').modal('hide');
                ///Se manda mensaje de confirmación
                $.notificacionMsj(3, data.d.sMensaje);
            }
            ///Se desbloquea pantalla
            $.desbloquearPantalla();
        }, error: function (xhr, status, error) {
            var err = eval("(" + xhr.responseText + ")");
            ///Se visualiza mensaje de error
            $.notificacionMsj(3, err.Message);
            ///Se desbloquea pantalla
            $.desbloquearPantalla();
        }
    });
}
/*========================FIN FUNCIÓN PARA INICIAR UNA ACTIVIDAD======================*/

function fn_CreaEtiquetaRange(siIdRange) {
    var elInput = document.querySelector('#' + siIdRange + '');
    if (elInput) {
        var etiqueta = document.querySelector('#etiqueta');
        if (etiqueta) {
            etiqueta.innerHTML = "<b>" + elInput.value + "%</b>";

            elInput.addEventListener('input', function () {
                etiqueta.innerHTML = "<b>" + elInput.value + "%</b>";
            }, false);
        }
    }
}

function fn_aumentaDisminuye_tamanio(iTamaño, iTamañoCambio, iOpcion) {
    $("#hdvContador").attr("style", "padding: " + (3 + ((iTamaño * 6 / 100))) + "px " + (3 + ((iTamaño * 6 / 100))) + "px;font-size: " + (14 + ((iTamaño * 10 / 100))) + "px;");
    $("#hbtnRegresar").attr("style", "height:" + (28 + ((iTamaño * 25 / 100))) + "px;padding: " + (5 + ((iTamaño * 5 / 100))) + "px " + (10 + ((iTamaño * 4 / 100))) + "px;font-size: " + (12 + ((iTamaño * 10 / 100))) + "px;");
    /*Aumenta de Tamaño el DataTable*/
    $("#hdvTablaTramitador .dataTables_length").attr("style", "height:" + (26 + ((iTamaño * 50 / 100))) + "px;padding: " + (3 + ((iTamaño * 6 / 100))) + "px " + (3 + ((iTamaño * 6 / 100))) + "px;font-size: " + (1 + ((iTamaño * 2 / 100))) + "em;width:40em;");
    $("#hdvTablaTramitador .dataTables_length select").attr("style", "height:" + (19 + ((iTamaño * 30 / 100))) + "px;font-size: " + (12 + ((iTamaño * 22 / 100))) + "px;width:" + (75 + ((iTamaño * 90 / 100))) + "px;");
    // 60.7
    $("#tb_tramitador thead tr").attr("style", "font-size:" + (1 + ((iTamaño * 4 / 100))) + "em");
    $("#tb_tramitador tfoot input").attr("style", "height:" + (22 + ((iTamaño * 42 / 100))) + "px !important;/*padding:" + (12 + (iTamaño)) + "px 16px*/;font-size:" + (12 + ((iTamaño * 22 / 100))) + "px;" +
                                                       "line-height: 1.3333333;");
    //11px
    $("#tb_tramitador tbody").attr("style", "font-size:" + (11 + ((iTamaño * 20 / 100))) + "px");
    //dataTables_info
    $("#hdvTablaTramitador .dataTables_info").attr("style", "font-size:" + (14 + ((iTamaño * 22 / 100))) + "px");
    $("#tb_tramitador tr td >a >span").attr("style", "font-size:" + (30 + ((iTamaño * 50 / 100))) + "px");
    //padding: 6px 12px;
    $("#hdvTablaTramitador .pagination > li >a").attr("style", "padding: " + (6 + ((iTamaño * 10 / 100))) + "px " + (12 + ((iTamaño * 20 / 100))) + "px;font-size: " + (12 + ((iTamaño * 20 / 100))) + "px;height: auto;");

    //se agrega aumento a dialog
    $("div .modal").on('shown.bs.modal', function () {
        //alert($(this).html());300px600px900px
        var sAuto = iTamaño < 12 ? "" : " auto ";
        //tamaño del dialog     width: 1188px;
        $("#" + this.id + " .modal-dialog").css("width", sAuto);
        $("#" + this.id + " .modal-dialog").css("max-width", "1188px");
        $("#" + this.id + " .modal-sm").css("width", (300 + ((iTamaño * 95 / 100))) + "px");
        $("#" + this.id + " .modal-lg").css("width", sAuto);
        //tamaño del heading
        $("#" + this.id + " .modal-title").css("font-size", (18 + ((iTamaño * 18 / 100))) + "px");

        //Contenido 12 28
        $("#" + this.id + " .modal-body").css("font-size", (14 + ((iTamaño * 14 / 100))) + "px");
        $("#" + this.id + " table").css("font-size", (14 + ((iTamaño * 14 / 100))) + "px");
        $("#" + this.id + " table th").css("font-size", (12 + ((iTamaño * 12 / 100))) + "px");
        $("#" + this.id + " .input-sm").css("font-size", (12 + ((iTamaño * 12 / 100))) + "px");
        $("#" + this.id + " .input-sm").css("height", (28 + ((iTamaño * 28 / 100))) + "px");
    });
}

/*Cancelar Actividad=============================>*/

/*Funcion para abrir dialog de confirmar cancelacion*/
function fn_confirmaCancelacion(sIdActividad) {
    fn_abreDialogConfirma("Cancela actividad", "<i class='fa fa-exclamation-circle icono-rojo'>&nbsp;</i> ¿Estás seguro de cancelar esta actividad?", "fn_cancelaActividad('" + sIdActividad + "')")
}//Fin funcion ============================>

//fUNCION PARA CANCELAR ACTIVIDAD
function fn_cancelaActividad(sidActividad) {
    ///Se bloquea pantalla 
    $.bloquearPantalla("Cancelando...");
    //se recupera el id del usuario
    var sIdUsuario = $("#ahddUser").val();
    $.ajax({
        url: "Inicio.aspx/fn_cancelaActividad",
        type: "POST",
        data: "{ sIdActividad:'" + sidActividad + "', sIdUsuario:'" + sIdUsuario + "' }",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            ///Se desbloquea pantalla
            $.desbloquearPantalla();
            //se manda notificacion
            $.notificacionMsj(data.d.iResultado, data.d.sMensaje);
            //se cierra dialog
            $("#hdvConfirmar").modal("toggle");
            //se valida resultado 
            if (data.d.iResultado == 1) {
                //se recarga tabla
                // DataTable
                var table = $('#tb_tramitador').DataTable();
                table.draw();
            }

        }, error: function (xhr, status, error) {
            ///Se desbloquea pantalla
            $.desbloquearPantalla();
            try {
                var err = eval("(" + xhr.responseText + ")");
                ///Se visualiza mensaje de error
                $.notificacionMsj(3, err.Message);
            }
            catch (e) {
                $.notificacionMsj(3, "Error al cancelar actividad");
            }
        }
    });
}//FIN FUNCION

/*Funcion para abrir dialog de confirmacion*/
function fn_abreDialogConfirma(sTitle, sMensaje, sOnclick) {
    //se agrega titulo
    $("#hlblMensajeTitle").html(sTitle);
    //**************

    /*se crea mensaje*/
    $("#hlblMensajeBody").html(sMensaje);
    /**/

    /*se agregan propiedades al boton*/
    $("#abtnGuardar").attr("onclick", sOnclick);
    $("#abtnGuardar > span").html("Aceptar");
    /**/


    /*Se abre el Dialog*/
    $("#hdvConfirmar").modal("toggle");
    /************/
}//FIN FUNCION===================================*/