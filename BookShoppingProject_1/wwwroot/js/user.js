var dataTable;

$(document).ready(function () {
    loadDataTable();
})

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url":"/Admin/User/GetAll"
        },
        "columns": [
            { "data": "name", "width": "15%" },
            { "data": "email", "width": "15%" },
            { "data": "phoneNumber", "width": "15%" },
            { "data": "company.name", "width": "15%" },
            { "data": "role", "width": "15%" },
            {
                "data": {
                    id:"id", lockoutEnd : "lockoutEnd"
                },
                "render": function (data) {
                    var today = new Date().getTime();
                    var lockOut = new Date(data.lockoutEnd).getTime();
                    if(lockOut > today) {
                        //UserLocked
                        return `
                                <div class="text-center">
                                    <a class="btn btn-danger" onclick=LockUnlock('${data.id}')> Unlock </a>
                                </div>
                                `;
                    }
                    else {
                        //UserUnlocked
                        return `
                                <div class="text-center">
                                    <a class="btn btn-success" onclick=LockUnlock('${data.id}')>Lock</a>
                                </div>
                                `;
                    }
                }
            }
        ]
    })
}

function LockUnlock(id) {
    $.ajax({
        type: "POST",
        url: "/Admin/User/LockUnlock",
        data: JSON.stringify(id),
        contentType: "application/json",
        success: function (data) {
            if (data.success) {
                toastr.success(data.message);
                dataTable.ajax.reload();
            }
            else {
                toastr.error(data.message);
            }
        }
    })
}