function Ad() { }
Ad.Latitude = null;
Ad.Longitude = null;

Ad.Description = null;
Ad.Location = null;
Ad.Title = null;
Ad.Url = null;
Ad.Value = null;

function AdController() { }
AdController.Ads = null;
AdController.SelectedAd = null;

AdController.Init = function () {

    if (AdController.Ads == null) {
        AdController.Ads = new JSdict();
    }

}

AdController.Get = function (key) {

    if (AdController.Ads == null) {
        AdController.Ads = new JSdict();
    }

    return AdController.Ads.getVal(key);

}
AdController.Set = function (key, ad) {

    if (AdController.Ads == null) {
        AdController.Ads = new JSdict();
    }

    if (AdController.Ads.getVal(key) == null) {
        AdController.Ads.add(key, ad);
    }

}


function test() {
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
