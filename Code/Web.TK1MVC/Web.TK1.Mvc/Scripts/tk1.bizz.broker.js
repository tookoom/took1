function test(){
alert('teste');
};

function initializeSelects(customer) {
    //alert(customer);
    //AD TYPE SELECT
    $("#adTypeSelect").change(function (e) {
        var adType = "";
        adType = $(this).val();
        var json = { "customerCode": customer, "adType": adType };
        $.ajax({
            type: "GET",
            url: "/broker/Cities",
            data: json,
            dataType: "json",
            success: adTypeCompleted,
            error: ajaxFailure
        });
    });

}
function adTypeCompleted(results) {
    var options = '';
    for (var i = 0; i < results.length; i++) {
        options += '<option value="' + results[i].Value + '" selected="' + results[i].Selected + '">' + results[i].Text + '</option>';
    }
    $("#citySelect").html(options);
}

function ajaxFailure(request, status, error) {
    alert(error);
}
