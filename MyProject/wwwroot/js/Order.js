var dtable;
$(document).ready(function () {

    var url = window.location.search;
    if (url.includes("pending")) {
        OrderTable("pending")
    }
    else (url.includes("approved"))
    {
        OrderTable("approved")
    }

    
});
function OrderTable(status) {
    dtable = $('#myTable').DataTable({
        "ajax": {
            "url": "/Admin/Order/AllOrders?status="+status

        },

        "columns": [
            { "data": "name" },
            { "data": "phone" },
            {
                "data": "course.driveLink",
                "render": function (data) {
                    return `<a href="${data}" target="_blank"> Drive Link </a>`
                }
            },
            { "data": "orderStatus" },
            { "data": "orderTotal" },
            /*{ "data": "userId" },*/
            //{
            //    "data": "id",
            //    "render": function (data) {
            //        return `
            //                <a href="/Admin/order/orderDetails?id=${data}"> Edit</a>
            //            `
            //    }
            //}
        ]
    });
}