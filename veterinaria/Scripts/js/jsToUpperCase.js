$(document).ready(function () {
    ///---///---///---Document ready---///--///

    

    ////*************BLUR**************///////////
    //--Blur text
    $("input[type='text']").bind('blur', function () {
        
        if (!$(this).hasClass('upperExcept')) {
            ///
            $(this).val(function (i, val) {
                return val.toUpperCase();
            });
            
        }
    });

    $("input[type='text']").blur(function () {
        if (!$(this).hasClass('upperExcept')) {
            
            $(this).val($(this).val().toUpperCase());
        }
    });

    //--Blur input
    $('input').bind('blur', function () {
        if (!$(this).hasClass('upperExcept')) {
            ///
            $(this).val(function (i, val) {
                return val.toUpperCase();
            });
        }
    });

    ////*************BLUR**************///////////
    ////Blur texarea
    $('textarea').bind('blur', function () {
        $(this).val(function (i, val) {
            return val.toUpperCase();
        });
    });
    ////
    $('textarea').keyup(function () {
        $(this).val($(this).val().toUpperCase());
    });

    ////*************KEYUP**************///////////
    ////TEXT
    $("input[type='text']").keyup(function () {
        if (!$(this).hasClass('upperExcept')) {
            ///$(this).val().toUpperCase();
            $(this).val($(this).val().toUpperCase());
        }
        
    });
    /////INPUT
    $('input').keyup(function () {
        
        if (!$(this).hasClass('upperExcept')) {
            $(this).val().toUpperCase();
        }
    });

    ////*************KEYPRESS**************///////////
    ////TEXT
    $("input[type='text']").keypress(function () {
        if (!$(this).hasClass('upperExcept')) {
            $(this).val().toUpperCase();
        }
        
    });
    //////INPUT
    $('input').keypress(function () {
        if (!$(this).hasClass('upperExcept')) {
            $(this).val().toUpperCase();
        }
    });

  

    ///---///---///---Document ready---///--///
});

