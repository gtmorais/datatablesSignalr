$(function() {
    $.extend(true, $.fn.dataTable.defaults, {
        "autoWidth": true,
        "info": true,
        "paging": true,
        "pageLength": 4,
        "ordering": true,
        "filter": false,
        "lengthChange": false,
        "processing": true,
        "serverSide": true
        //"search" : ""
    });
});


