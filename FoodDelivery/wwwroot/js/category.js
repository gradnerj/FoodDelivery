var dataTable;

$(document).ready(function () {
    loadList();
});

function loadList() {
    dataTable = $('#DT_load').DataTable({
        "ajax": {
            "url": "/api/category",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { data: "name", width: "40%" },
            { data: "displayOrder", width: "30%" },
            { data: "id", width: "30%" }
        ],
        "language": {
            "emptyTable": "no data found."
        },
        "width": "100%"
    });
}