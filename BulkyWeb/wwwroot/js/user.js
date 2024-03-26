
var dataTable;
$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblUserData').DataTable({
        "ajax": { url: '/admin/user/getAll' },
        "columns": [
            { data: "name", "width": "20%" },
            { data: "email", "width": "15%" },
            { data: "phoneNumber", "width": "10%" },
            { data: "company.name", "width": "10%" },
            { data: "role", "width": "10%" },
            {
                data: { id: "id", lockoutEnd: "lockoutEnd" },
                "render": function (data) {
                    var today = new Date().getTime();
                    var lockout = new Date(data.lockoutEnd).getTime();

                    if (lockout > today) {
                        return `
                            <div class="text-center">
                                <a onclick=LockUnlock('${data.id}') class="btn btn-danger text-white">
                                    <i class="bi bi-pencil-square"></i> Lock
                                </a>
                                <a href="/admin/user/RoleManagement?userId=${data.id}" class="btn btn-danger text-white">
                                    <i class="bi bi-pencil-square"></i> Permission
                                </a>
                            </div>
                        `
                    } else {
                        return `
                            <div class="text-center">
                                <a onclick=LockUnlock('${data.id}') class="btn btn-success text-white">
                                    <i class="bi bi-pencil-square"></i> Unlock
                                </a>
                                <a href="/admin/user/RoleManagement?userId=${data.id}" class="btn btn-danger text-white">
                                    <i class="bi bi-pencil-square"></i> Permission
                                </a>
                            </div>
                        `
                    }


                }
            }

        ]
    });
}

function LockUnlock(id) {
    $.ajax({
        type: "POST",
        url: "/admin/user/lockunlock",
        data: JSON.stringify(id),
        contentType: "application/json",
        success: function (data) {
            if (data.success) {
                toastr.success(data.message);
                dataTable.ajax.reload();
            }
        }
    })
}

