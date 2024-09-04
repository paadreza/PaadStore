var dataTable;
$(document).ready(function () {
    loadDataTable();
    console.log(dataTable);
});
function loadDataTable()
{
    dataTable =$('#tblData').DataTable({
        "ajax": { url:'/Admin/Product/GetAll'} ,
        "columns": [
            { data: 'title',"width":"25%" },
            { data: 'isbn', "width": "15%" },
            { data: 'author', "width": "20%" },
            { data: 'price', "width" : "10%" },
            { data: 'category.name', "width": "15%" }
        ]
    });
}
