//Inicio Document Ready
$(document).ready(function () {

    /*al clickear el boton de notificaciones a unos cargara el lightbox con el contenido que agregamos al momento de crear la capa contenido*/
    $("#Contnotificacion").click(function () {
        SideBar();
        return false;
    });

});




//Inicio Funcion
SideBar = function () {

    //Se le agrega el efecto de side bar al div de notificaciones
    $("#dNotificaciones").animate({ 'width': 'toggle' });
    //********************************************************

    //Inicio ocultar div con tecla ESCAPE
    //se lee entrada del teclado
    $(document).keyup(function (e) {
        // se evalua si la tecla es Esc
        if (e.keyCode == 27) { // escape key maps to keycode `27`
            //Se oculta el div de notificaciones
            $("#dNotificaciones").fadeOut();
            //************************************
        }
    });
    //***************************

    //********************************************

    //Inicio ocultar div con clik  en vista
    //se agrega evento click al form contenedor
    $("#form1").click(function () {
        //se oculta div
        $("#dNotificaciones").fadeOut();
        //**********************************
    });
    //**********************************************
    //**************************************************************



    // Change the selector if needed
    var $table = $('table.scroll'),
         $bodyCells = $table.find('tbody tr:first').children(),
        colWidth;

    // Adjust the width of thead cells when window resizes
    $(window).resize(function () {
        // Get the tbody columns width array
        colWidth = $bodyCells.map(function () {
            return $(this).width();
        }).get();

        // Set the width of thead columns
        $table.find('thead tr').children().each(function (i, v) {
            $(v).width(colWidth[i]);
        });
    }).resize(); // Trigger resize handler


}

