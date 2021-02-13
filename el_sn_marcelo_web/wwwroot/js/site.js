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
});

function modal() {
    $('.modal').modal('show');
}

function modal_off() {
    $('.modal').modal('hide');
}