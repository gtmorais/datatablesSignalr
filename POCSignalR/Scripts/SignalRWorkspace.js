$(function () {
    var workspace = $.connection.liveWorkspace;

    //adds a function to be called by the server -- binds the datatables.net
    workspace.client.notifyNewContent = function () {
        $('#gridResultado').DataTable().ajax.reload();
    };
    
    //open server connection
    $.connection.hub.start().done(function () {
        LoadDataTables();
    });

    $('#button').click(function () {
        $.ajax({
            url: '/Home/CreateNewTask',
            success: function (data) {
               
            }
        });
    });

    function LoadDataTables() {
        var dataTable = null;
        var responsiveHelper_dt_basic = undefined;
        var breakpointDefinition = {
            tablet: 1024,
            phone: 480
        };

        var renderizarTempo = function (data, type, row, meta) {
            return '<div class="dtcountdown">' + row.DataFimTexto + '</div>';
        };

        $('#filtros').on('submit', function (e) {
            e.preventDefault();
            e.stopPropagation();
            if ($(this).valid()) {
                dataTable.draw();
            }
        });

        $('#gridResultado').on('draw.dt', function () {
            $(this).find('a').on('click', function (e) {
                e.preventDefault();
                alert('get [' + this.href + '] para id [' + $(this).data('id') + ']');
            });
        });

        dataTable = $('#gridResultado').DataTable({
            "sDom": "<'dt-toolbar'<'col-xs-12 col-sm-6'f><'col-sm-6 col-xs-12 hidden-xs'l>r>" +
                         "t" +
                         "<'dt-toolbar-footer'<'col-sm-6 col-xs-12 hidden-xs'i><'col-xs-12 col-sm-6'p>>",

            "columns": [
                { "data": "Responsavel", "title": "Assigned To", "orderable": false },
                { "data": "Titulo", "title": "Task", "orderable": false },
                { "data": "Prioridade", "orderable": true },
                { "data": "TipoProcesso", "title": "Type", "orderable": true },
                { "data": "Origem", "orderable": false },
                { "data": "DataFimTexto", "title": "Due Date", "orderable": true },
                { "data": "Tempo", "orderable": false, "title": "Time", "render": renderizarTempo }
            ],
            "preDrawCallback": function () {
                // Initialize the responsive datatables helper once.
                if (!responsiveHelper_dt_basic) {
                    responsiveHelper_dt_basic = new ResponsiveDatatablesHelper($('#gridResultado'), breakpointDefinition);
                }
            },
            "drawCallback": function (oSettings) {
                responsiveHelper_dt_basic.respond();
            },
            "rowCallback": function (nRow, aData) {
                responsiveHelper_dt_basic.createExpandIcon(nRow);

                $(nRow).click(function () { alert(aData.ID); });

                $(nRow).find('.dtcountdown').each(function () {
                    var $this = $(this);
                    var dataFim = $this.text();
                    //var finalDate = $.format.date(dataFim, "MM/dd/yyyy HH:mm:ss");
                    $this.countdown(dataFim, function (event) {
                        $this.html(event.strftime('%H:%M:%S'));

                        //colors
                        if (event.offset.hours == 0 && event.offset.minutes < 30) {
                            if (event.offset.minutes < 1 && event.offset.seconds == 0) {
                                var elementoTr = $this.closest("tr");
                                elementoTr.addClass('gridExpired');
                                $this.countdown('stop');
                            } else {
                                var elementoTr = $this.closest("tr");
                                elementoTr.addClass('gridWarning');
                            }
                        }
                    });

                }).on('finish.countdown', function (event) {
                    $(this).closest("tr").find('td').each(function () {
                        $(this).removeClass('gridWarning');
                        $(this).addClass('gridExpired');
                    });
                });
            },

            "ajax": {
                "url": "/Home/ListTasks",
                "type": 'GET',
                "dataType": 'json',
                "contentType": 'application/x-www-form-urlencoded; charset=UTF-8' // NOTE: 'application/json', not working with DataTablesBinder
                ,
                "data": function (d) {
                    $('#filtros').find('.dt-filtro').each(function () {
                        d[this.name] = this.value;
                    });
                    return d;
                }
            }
        });

        //$(".countDown").countDown({ startTime: $(".countDown").text() });
    }
});