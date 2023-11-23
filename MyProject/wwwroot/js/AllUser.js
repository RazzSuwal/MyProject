var dtable;
$(document).ready(function () {
    dtable = $('#myTable').DataTable({
        "ajax": {
            "url": "/Admin/User/AllUser"

        },

        "columns": [
            { "data": "name" },
            { "data": "address" },
            { "data": "country" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                            <a class="text-danger" onClick =RemoveCourse("/Admin/User/Delete/${data}")> Remove</a> 
                        `
                }
            }
        ]
    });
});
function RemoveCourse(url) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    if (data.success) {
                        dtable.ajax.reload();
                        toastr.success(data.message);
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            })
        }
    })
}