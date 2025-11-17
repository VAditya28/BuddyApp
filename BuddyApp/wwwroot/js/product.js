function loadDataTable() {
    $('#mydata').DataTable({
        ajax: { url: '/admin/product/getall' },
        columns: [
            { data: 'name', width: "15%" },
            { data: 'isbn', width: "15%" },
            { data: 'author', width: "15%" },
            { data: 'listPrice', width: "15%" },
            { data: 'category.name', width: "15%" }
        ]
    });
}

$(document).ready(function () {
    loadDataTable();
});



//function loadDataTable() {
//    dataTable = $('#tbldata').DataTable({
//        "ajax": { url: '/admin/product/getall' },
//        "columns":
//            [{
//                data: 'name', "width": "15%"},
//                { data: 'isbn', "width": "15%" },
//                { data: 'author', "width": "15%" },
//                { data: 'listPrice', "width": "15%" },
//                {
//                    data: 'Category.name', "width": "15%"
//                }]
//    });
//}
//$(document).ready(function ()
//{
//    loadDataTable();
//});