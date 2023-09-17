var dataTable;
$(document).ready(function () {
    loadDataTable();
})

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Admin/Product/GetAll"
        },
        "columns": [
            { "data": "title", "width": "15%" },
            { "data": "description", "width": "25%" },
            { "data": "author", "width": "15%" },
            { "data": "isbn", "width": "15%" },
            { "data": "price", "width": "15%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                            <div class="text-center">
                                <a href="/Admin/Product/Upsert/${data}" class="btn btn-info">
                                    <i class="fas fa-edit"></i> 
                                </a>
                                <a onclick=Delete("/Admin/Product/Delete/${data}") class="btn btn-danger">
                                    <i class="fas fa-trash-alt"></i>
                                </a>
                            </div>
                            `;
                }
            }
        ]
    })
}
function Delete(url) {
    swal({
        title: "Are you sure you want to delete this data?",
        text: "Deleted data can't be recovered again",
        buttons: true,
        icon: "warning",
        dangerModel: true
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                url: url,
                type: "DELETE",
                success: function (data) {
                    if (data.success)
                    {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    }
                    else
                    {
                        toastr.error(data.message);
                    }
                }
            })
        }
    })
}