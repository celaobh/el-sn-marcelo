// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.

$(document).ready(function () {
    $(".cpf").each(function (item) {
        $(this).mask('000.000.000-00', { reverse: true });
    });

    $(".cep").mask("99.999-999", { reverse: true });

    $("input").prop('required', true);

    $("select").prop('required', true);

    $("form").on("submit", function () {
        modal();
    })

    $('.money').mask('#.##0,00', { reverse: true });

    $('input[name=placa]').mask('AAA 0U00', {
        translation: {
            'A': {
                pattern: /[A-Za-z]/
            },
            'U': {
                pattern: /[A-Za-z0-9]/
            },
        },
        onKeyPress: function (value, e, field, options) {
            // Convert to uppercase
            e.currentTarget.value = value.toUpperCase();

            // Get only valid characters
            let val = value.replace(/[^\w]/g, '');

            // Detect plate format
            let isNumeric = !isNaN(parseFloat(val[4])) && isFinite(val[4]);
            let mask = 'AAA 0U00';
            if (val.length > 4 && isNumeric) {
                mask = 'AAA-0000';
            }
            $(field).mask(mask, options);
        }
    });
});

function modal() {
    $('.modal').modal('show');
}

function modal_off() {
    $('.modal').modal('hide');
}