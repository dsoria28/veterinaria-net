; (function () {
    "use strict";

    function setup($) {
        $.fn._fadeIn = $.fn.fadeIn;

        var noOp = $.noop || function () { };

        // this bit is to ensure we don't call setExpression when we shouldn't (with extra muscle to handle
        // confusing userAgent strings on Vista)
        var msie = /MSIE/.test(navigator.userAgent);
        var ie6 = /MSIE 6.0/.test(navigator.userAgent) && !/MSIE 8.0/.test(navigator.userAgent);
        var mode = document.documentMode || 0;
        var setExpr = $.isFunction(document.createElement('div').style.setExpression);

        /*Funcion para bloquear la pantalla*/
        $.bloquearPantalla=function(msj) {
            $.blockUI({
                message: '' + msj + '',
                css: {
                    border: 'none',
                    padding: '15px',
                    backgroundColor: '#000',
                    '-webkit-border-radius': '10px',
                    '-moz-border-radius': '10px',
                    opacity: .5,
                    color: '#fff',
                    "z-index":"25544545454545"
                }
            });
        };

        /*Funcion para desbloquear la pantalla*/
        $.desbloquearPantalla=function() {
            $.unblockUI();
        };

        //Metodo para validar los campos
        $.validarCampos=function(id){ //Inicio Validad Campos
            var resultado="";

            $("#"+id+" .valEmpty").each(function(){
                
                if($.trim($("#"+$(this).attr("id")).val())=='')
                {
                    $("#"+$(this).attr("id")).addClass('errEmpty');
                
                    $("#"+$(this).attr("id")).focus(function(){
                        $("#"+$(this).attr("id")).removeClass('errEmpty');
                    });
                    var name=$(this).attr("name").toUpperCase();

                    resultado+="El Campo "+name+" esta vacío. </br>";
                }
            });

            return resultado;
        };//Fin ValidarCampos


        /*Funcion para validar email*/
        $.validarCampoEmail=function(id) {
	        var email=$("#"+id).val();
            var emailReg = /^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/;
	        if( !emailReg.test( email )) {
		        $("#"+$("#"+id).attr("id")).addClass('errEmpty');
                $("#"+id).focus(function(){
                    $("#"+id).removeClass('errEmpty');
                });
                return "EL CORREO "+email+' ES INVALIDO.';

	        } else {
	            return "";	
	        }
        }


        //Metodo para validar tipo de Hora
        $.validarCampoHora=function(id)
        {
            var resultado="";
            $("#"+id+" .valHour").each(function(){
                
                var hourCompleta = $(this).val();
                var hour = true, point = true, minute = true;
                var comVal = parseInt(hourCompleta.substring(5, 3));
                var comCero = 0;

                if (parseInt(hourCompleta.substring(0, 2)) > 24 && parseInt(hourCompleta.substring(0, 2)) > 7) { hour = false; }
                if(comVal>=comCero && comVal<60) {minute = true; } else { minute=false }
                if (hourCompleta.substring(3, 2) != ":") { point = false; }
                
                if(hour==false || minute==false || point==false)
                {
                    $("#"+$(this).attr("id")).addClass('errEmpty');
                
                    $("#"+$(this).attr("id")).focus(function(){
                        $("#"+$(this).attr("id")).removeClass('errEmpty');
                    });
                    var name=$(this).attr("name").toUpperCase();

                    resultado+="El Campo "+name+" no tiene formato de Hora Válido. </br>";
                }


            });

            return resultado;
        }
        
        //Metodo para validar el campo de Fecha
        $.validarCampoFecha=function(id)
        {
            var resultado="";
            $("#"+id+" .valFecha").each(function(){
                
                if($(this).val()!='')
                {
                    var FechaCompleta = $(this).val();
                    var ano = true, mes = true, day = true,diagonal1=true,diagonal2=true;
                    var valAno=parseInt(FechaCompleta.substring(6,10));
                    var valMes = parseInt(FechaCompleta.substring(3,5));
                    var valDay=parseInt(FechaCompleta.substring(0,2));
                    var valDiag1=FechaCompleta.substring(2,3);
                    var valDiag2=FechaCompleta.substring(5,6);
                    var fechaA = new Date();
                    var anoActual= fechaA.getFullYear();
                
                    if (valAno<1990 || valAno>anoActual) { ano = false; }
                    if(valMes<1 || valMes>12){mes=false;}
                    if(valDay<1 || valDay>31){day=false;}
                    if(valDiag1!="/"){diagonal1=false;}
                    if(valDiag2!="/"){diagonal2=false;}

                    if(day==false || mes==false || ano==false || diagonal1==false || diagonal2==false)
                    {
                        $("#"+$(this).attr("id")).addClass('errEmpty');
                
                        $("#"+$(this).attr("id")).focus(function(){
                            $("#"+$(this).attr("id")).removeClass('errEmpty');
                        });
                        var name=$(this).attr("name").toUpperCase();

                        resultado+="El Campo "+name+" no tiene formato de Fecha Válido. </br>";
                    }
                }

            });

            return resultado;
        }


        //Funcion para agregar estilos a tabla de color AZUL De GridView con TH
        $.dataTableAzul_Grid=function(idTable)
        {
            $("#"+idTable).addClass('datagrid3');
            $("#"+idTable).addClass('dtDatosBusqueda');
            
            $("#"+idTable+" tbody tr:odd").addClass("white"); // filas pares
            $("#"+idTable+" tbody tr:even").addClass("gray"); // filas pares
            $("#"+idTable+" tbody tr td").css("height","12px");

        }
        
        //Funcion para agregar estilos a tabla de color AZUL
        $.dataTableAzul=function(idTable)
        {
            $("#"+idTable).addClass('datagrid3');

            $("#"+idTable+" tbody tr:odd").addClass("white"); // filas pares
            $("#"+idTable+" tbody tr:even").addClass("gray"); // filas pares

            $("#"+idTable+" tfoot td").addClass("foot");
            $("#"+idTable+" tfoot td").addClass("shadow3");
            $("#"+idTable+" tfoot td").addClass("round-down2");

        }

        //Funcion para agregar estilos a tabla de color AZUL con footer
        $.dataTableAzulFooter=function(idTable)
        {
            $("#"+idTable).addClass('datagrid3');

            $("#"+idTable+" tbody tr:odd").addClass("white"); // filas pares
            $("#"+idTable+" tbody tr:even").addClass("gray"); // filas pares

        }


        //Funcion para agregar estilos a la table de color GRIS
        $.dataTableGris=function(idTable)
        {
            $("#"+idTable +" thead tr").attr('id','grid-head2');
            $("#"+idTable+" tbody").attr('id','grid-body');

            $("#"+idTable+" tbody tr:odd").addClass("white"); // filas pares
            $("#"+idTable+" tbody tr:even").addClass("gray"); // filas pares

            $("#"+idTable+" tfoot td").addClass("foot");
            $("#"+idTable+" tfoot td").addClass("shadow3");
            $("#"+idTable+" tfoot td").addClass("round-down2");
        }

         //Funcion para agregar estilos a la table de color GRIS
        $.dataTableGrisFooter=function(idTable)
        {
            $("#"+idTable +" thead tr").attr('id','grid-head2');
            $("#"+idTable+" tbody").attr('id','grid-body');

            $("#"+idTable+" tbody tr:odd").addClass("white"); // filas pares
            $("#"+idTable+" tbody tr:even").addClass("gray"); // filas pares

            $("#"+idTable +" tfoot tr").attr('id','grid-head2');
        }


        ///Funcion general para mostrar estilo de combobox
        $.estyleComboboxAutocomplete=function(){
            
            (function ($) {
                $.widget("custom.combobox", {
                    _create: function () {
                        this.wrapper = $("<span>").addClass("custom-combobox").insertAfter(this.element);
                        this.element.hide();
                        this._createAutocomplete();
                        this._createShowAllButton();
                    },_createAutocomplete: function () {
                        var selected = this.element.children(":selected"),
                        value = selected.val() ? selected.text() : "";
                        this.input = $("<input>").appendTo(this.wrapper).val(value).attr("title", "").css("width", "85%")
                        .addClass("custom-combobox-input ui-widget ui-widget-content ui-state-default ui-corner-left")
                        .autocomplete({
                            delay: 0,
                            minLength: 0,
                            source: $.proxy(this, "_source")
                        }).tooltip({
                            tooltipClass: "ui-state-highlight"
                        });
                        this._on(this.input, {
                            autocompleteselect: function (event, ui) {
                                ui.item.option.selected = true;
                                this._trigger("select", event, {
                                    item: ui.item.option
                                });
                            },autocompletechange: "_removeIfInvalid"
                        });
                    },_createShowAllButton: function () {
                        var input = this.input,
                        wasOpen = false;
                        $("<a>").attr("tabIndex", -1).attr("title", "Show All Items").tooltip().appendTo(this.wrapper)
                        .button({
                            icons: {
                                primary: "ui-icon-triangle-1-s"
                            },text: false
                        }).removeClass("ui-corner-all").addClass("custom-combobox-toggle ui-corner-right").mousedown(function () {
                            wasOpen = input.autocomplete("widget").is(":visible");
                        }).click(function () {
                            input.focus();
                            // Close if already visible
                            if (wasOpen) {
                                return;
                            }
                            // Pass empty string as value to search for, displaying all results
                            input.autocomplete("search", "");
                        });
                    },_source: function (request, response) {
                        var matcher = new RegExp($.ui.autocomplete.escapeRegex(request.term), "i");
                        response(this.element.children("option").map(function () {
                            var text = $(this).text();
                            if (this.value && (!request.term || matcher.test(text)))
                                return {
                                    label: text,
                                    value: text,
                                    option: this
                                };
                            }));
                    },_removeIfInvalid: function (event, ui) {
                        // Selected an item, nothing to do
                        if (ui.item) {
                            return;
                        }
                        // Search for a match (case-insensitive)
                        var value = this.input.val(),
                        valueLowerCase = value.toLowerCase(),
                        valid = false;
                        this.element.children("option").each(function () {
                            if ($(this).text().toLowerCase() === valueLowerCase) {
                                this.selected = valid = true;
                                return false;
                            }
                        });
                        // Found a match, nothing to do
                        if (valid) {
                            return;
                        }
                        // Remove invalid value
                        this.input.val("").attr("title", value + " didn't match any item").tooltip("open");
                        this.element.val("");
                        this._delay(function () {
                            this.input.tooltip("close").attr("title", "");
                        }, 2500);
                        this.input.autocomplete("instance").term = "";
                    },_destroy: function () {
                        this.wrapper.remove();
                        this.element.show();
                    }
                });
            })(jQuery);
            
        }





    }


    /*global define:true */
    if (typeof define === 'function' && define.amd && define.amd.jQuery) {
        define(['jquery'], setup);
    } else {
        setup(jQuery);
    }


})();
