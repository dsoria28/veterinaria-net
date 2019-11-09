$(document).ready(function () {
    fn_generar_tabla();
    fn_agregar_acceso_pdf();
});

///Función para generar la tabla
function fn_generar_tabla() {
    ///Se crea dataTable con filtros de busqueda
    $("#tb_list_Actualizacion").dataTable({
        //"searching": false,
        "jQueryUI": true,
        //"scrollX": true,
        "processing": true,
        "serverSide": true,
        "ajax": {
            "url": "../../Handlers/Inicio/h_listar_actualizaciones.ashx",
            "type": "POST",
            "data": {}
        },
        "columns": [
                { "data": "iSec"/*, "width": "1%" */ },
                { "data": "sVersion"/*, "width": "1%" */ },
                { "data": "dFechaNotificacionAccion"/*, "width": "10%"*/ },
                { "data": "sDescripcion"/*, "width": "5%" */ }
        ]
    });
    // DataTable
    var objTable = $('#tb_list_Actualizacion').DataTable();
    ///Se Aplica la busqueda
    objTable.columns().eq(0).each(function (colIdx) {
        $('input', objTable.column(colIdx).footer()).on('keydown', function (e) {
            var iCode = e.which;
            if (e.keyCode == 13 || iCode == 9) {
                objTable.column(colIdx).search(this.value).draw();
            }
        });
    });
}

///Función que colapsa/maximiza la tabla
function fn_colapsar_tabla() {
    ///Se obtiene la tabla
    var objTable = $('#tb_list_Actualizacion').DataTable();
    ///Verifica el estado de la tabla
    if ($("#atxtAct").val() == 1) {
        /// colapsa la tabla
        $("#atxtAct").val(2);
        $("#hspnColAct").removeClass("fa fa-minus-circle");
        $("#hspnColAct").addClass("fa fa-plus-circle");
        $("#hspnTextAct").html("Maximizar");
        ///$("#hdvActualizaciones").removeClass("col-xs-8 col-sm-8 col-md-8 col-lg-8");
        $("#hdvActualizaciones").addClass("col-xs-4 col-sm-4 col-md-4 col-lg-4");
        ///oculta la columna descripción
        objTable.columns([3]).visible(false, false);
        ///muestra el contenedor de detalles de la actualizacion
        $("#hdvContentAct").removeClass("hide");

        ///función que agrega el acceso a la visualizacion del pfd
        ///fn_agregar_acceso_pdf();

    } else {
        /// maximiza la tabla
        $("#atxtAct").val(1);
        $("#hspnColAct").removeClass("fa fa-plus-circle");
        $("#hspnColAct").addClass("fa fa-minus-circle");
        $("#hspnTextAct").html("Colapsar");
        ///$("#hdvActualizaciones").addClass("col-xs-8 col-sm-8 col-md-8 col-lg-8");
        $("#hdvActualizaciones").removeClass("col-xs-4 col-sm-4 col-md-4 col-lg-4");
        ///muestra columna descripción
        objTable.columns([3]).visible(true, false);
        ///oculta el contenedor de detalles
        $("#hdvContentAct").addClass("hide");
        ///limpia los spans que contienen la información que se recupera
        $("#hspVersion").html("");
        $("#hspFechaPu").html("");
        $("#hspFechaIni").html("");
        $("#hspFechaFin").html("");
        $("#hspDescripcion").html("");

        ///elimina el contenido del pdf que se mostró anteriormente
        $("#hdvContentPdf").html("");

        ///elimina las funciones que agregan el acceso al pdf
        ///$('#tb_list_Actualizacion tbody').unbind('click');

    }
}

///Función para agregar el acceso a la visualización del pdf
function fn_agregar_acceso_pdf() {
    ///Se le agrega el evento a cada tr de la tabla recuperando su id
    $('#tb_list_Actualizacion tbody').on('click', 'tr', function (e) {
        fn_generarContentPDF(this.id);
    });

}

///Función para generar la visualización al pdf
function fn_generarContentPDF(sIdNotificacion) {
    ///Se bloquea pantalla
    $.bloquearPantalla("Cargando...");
    if ($("#atxtAct").val() == 1) {
        fn_colapsar_tabla();
    }
    //recupera los datos de la actualizacion
    fn_recupera_datos_actualizacion(sIdNotificacion);
    ///Inicio método AJAX
    $.ajax({
        url: "actualizaciones.aspx/fn_generar_PDF",
        data: "{ sIdNotificacion: '" + sIdNotificacion + "' }",
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8;",
        success: function (data) {
            ///Se desbloquea pantalla
            $.desbloquearPantalla();
            ///Si el iResultado es de éxito
            if (data.d.iResultado == 1) {
                ///Se inicia el hdvContentPdf
                $("#hdvContentPdf").html("<a class='pdf-print' id='wp-submit'></a><iframe frameborder='0' height='100%' width='100%' id='"
                    + data.d.giIdArchivo + "' name='" + data.d.giIdArchivo + "' src='" + data.d.gsURL + "/" + data.d.gsNombreArchivo
                    + "?#zoom=75'></iframe>");
                ///Se imprime en pantalla el documento
                $(".pdf-print").on("click", function () {
                    var sName = data.d.gsNombreArchivo;
                    window.frames[sName].focus();
                    window.frames[sName].print();
                    return false;
                });
            }
                /// En caso de que el archivo no sea pdf
            else if (data.d.iResultado == 4) {
                ///Se muestra un botón para descargar el archivo con cualquier otra extensión
                var sBoton = "<div class='row form-group text-right'><a class='shadow btn btn-greenS btn-sm input-sm' id='hbtnDesAct' "
                    + " href='" + data.d.gsURL + "/" + data.d.gsNombreArchivo + "' download>"
                    + "Descargar</a></div>";
                $("#hdvContentPdf").html(sBoton);
            }
                ///En caso de error se manda el mensaje con el error
            else {
                $.notificacionMsj(3, data.d.sMensaje);
            }
        }, error: function (xhr, error, status) {
            ///Se desbloquea pantalla
            $.desbloquearPantalla();
            var arrError = eval("(" + xhr.responseText + ")");
            ///Se manda notificación de error
            $.notificacionMsj(3, arrError.Message);
        }
    });
}

///Función que recupera los detalles de la actualización
function fn_recupera_datos_actualizacion(sIdNotificacion) {
    ///Se bloquea pantalla
    $.bloquearPantalla("Cargando...");
    ///Inicio método AJAX
    $.ajax({
        url: "actualizaciones.aspx/fn_recupera_datos_actualizacion",
        data: "{ sIdNotificacion: '" + sIdNotificacion + "' }",
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8;",
        success: function (data) {
            ///Se desbloquea pantalla
            $.desbloquearPantalla();
            ///Si el iResultado es de éxito
            if (data.d.iResultado == 1) {
                ///Se muestran los detalles en las etiquetas que corresponde
                $("#hspVersion").html(data.d.gsVersion);
                $("#hspFechaPu").html(data.d.gsFechaNotificacionAccion);
                $("#hspFechaIni").html(data.d.gsFechaNotificacionInicio);
                $("#hspFechaFin").html(data.d.gsFechaNotificacionFin);
                $("#hspDescripcion").html(data.d.gsDescripcion);
            }
                ///En caso de error se manda el mensaje con el error
            else {
            }
        }, error: function (xhr, error, status) {
            ///Se desbloquea pantalla
            $.desbloquearPantalla();
            //var arrError = eval("(" + xhr.responseText + ")");
            //$.notificacionMsj(3, arrError.Message);
        }
    });
}