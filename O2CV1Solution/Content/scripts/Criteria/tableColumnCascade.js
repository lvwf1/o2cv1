 
    $(function () {
        $("#SelectedTable").change(function () {
            var selectedItem = $(this).val();
            var ddlColumns = $("#SelectColumns");
            var statesProgress = $("#columns-loading-progress");
            statesProgress.show();
            $.ajax({
                cache: false,
                type: "GET",
                url: "@(Url.RouteUrl("GetTableColumns"))",
                data: { "tableName": selectedItem },
            success: function (data) {                       
                ddlColumns.html('');
                $.each(data, function (id, option) {
                    ddlColumns.append($('<option></option>').val(option.id).html(option.name));
                });
                statesProgress.hide();
            },
            error: function (xhr, ajaxOptions, thrownError) {
                alert('Failed to retrieve columns for the table.');
                statesProgress.hide();
            }
        });
    });
});
 
