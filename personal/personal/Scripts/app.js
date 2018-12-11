
(function (AppNew, $) {
    var page = $("main").attr("class");

    AppNew.methods = AppNew.methods || {
        init: function () {
            $(document).ready(function () {
                AppNew.methods.modalidades();
            });
        },
        modalidades: function () {

            if ($("body").hasClass('modalidades')) {


                $('[data-toggle="tooltip"]').tooltip();
                var actions = $("table td:last-child").html();
                // Append table with add row form on add new button click
                $(".add-new").click(function () {

                    var row = "";

                    row += '<table class="table table-striped">' +
                        '<thead>' +
                        '<tr>' +
                        '<th scope="col">Esporte</th>' +
                        '<th scope="col">Adicionar horários</th>' +
                        '<th scope="col">Excluir</th>' +
                        '</tr>' +
                        '</thead > ' +
                        '<tbody>' +
                        '<tr>' +
                        '<td style="width:50%">' +
                        '<select class="form-control" id="sEsporte" style= "width:50%"> ' +
                        '<option value=""></option>' +
                        '<option value="1">MUAY THAI</option>' +
                        '<option value="2">FUNCIONAL</option>' +
                        '<option value="3">CORRIDA</option>' +
                        '<option value="4">SLACKLINE</option>' +
                        '<option value="5">SURF</option>' +
                        '<option value="6">MUSCULAÇÃO</option>' +
                        '</select ></td>' +
                        '</td>' +
                        '<td>' +
                        '<button id="btnAdd" class="btn btn-mini" title="Adicionar" style="background: #f9f9f9" data-toggle="modal" data-target="#ModalAdd">' +
                        '<i class="fa fa-plus" style="font-size: 20px; "></i>' +
                        '</button > ' +
                        '<button id= "btnHorarios" name= "btnHorarios" class="btn btn-mini" title= "Horarios" style= "background: #f9f9f9" data- toggle="modal" data- target="#ModalHorarios" > ' +
                        '<i class="fas fa-clock" style= "font-size: 20px; " ></i >' +
                        '</button > ' +
                        '<button id="btnMoney" name="btnMoney" class="btn btn-mini" title="Money" style="background: #f9f9f9" data-toggle="modal" data-target="#ModalMoney">' +
                        '<i class="fas fa-dollar-sign" style= "font-size: 20px; " ></i > ' +
                        '</button > ' +
                        '<button id= "btnHorariosSemanal" name= "btnHorariosSemanal" onclick="agendamento()" class="btn btn-mini" title= "HorariosSemanal" style= "background: #f9f9f9" data- toggle="modal" data- target="#ModalAgendamentos" > ' +
                        '<i class="fas fa-calendar-alt" style= "font-size: 20px; " ></i > ' +
                        '</button > ' +
                        '</td > ' +
                        '<td>' +
                        '<button id="btnExcluirMod" name="btnExcluirMod" class="btn btn-mini" title="Excluir Modalidade" style="background: #f9f9f9;">' +
                        '<i class="fas fa-trash-alt" style="font-size: 20px; "></i>' +
                        ' </button>' +
                        '</td>' +
                        '</tr>' +
                        '</tbody>' +
                        '</table>';

                    $("#add_new").append(row);
                });

                // Add row on add button click
                $(document).on("click", ".add", function () {
                    var empty = false;
                    var input = $(this).parents("tr").find('input[type="text"]');
                    input.each(function () {
                        if (!$(this).val()) {
                            $(this).addClass("error");
                            empty = true;
                        } else {
                            $(this).removeClass("error");
                        }
                    });
                    $(this).parents("tr").find(".error").first().focus();
                    if (!empty) {
                        input.each(function () {
                            $(this).parent("td").html($(this).val());
                        });
                        $(this).parents("tr").find(".add, .edit").toggle();
                        $(".add-new").removeAttr("disabled");
                    }
                });

                $('.datepicker').datepicker({
                    format: 'mm/dd/yyyy'
                });

            }
        },
    };

    AppNew.methods.init();
})(window.AppNew = window.AppNew || {}, $);