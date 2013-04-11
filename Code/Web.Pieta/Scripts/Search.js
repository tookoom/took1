function test(){
alert('teste');
};

function setSearchType() {
    //alert('teste');
//    var pathname = window.location;
//    alert(pathname);
    var quickSearch = $.url().param('quickSearch');
    if (quickSearch == 'rent') {
        $('input[id*=radioButtonRent]')[0].checked = true;
        //        window.location = window.location.pathname;
        alert('1');
//        window.location.search = 'quickSearch=no';
    }
    if (quickSearch == 'selling') {
        $('input[id*=radioButtonBuy]')[0].checked = true;
        //        window.location = window.location.pathname;
//        window.location.search = 'quickSearch=no';
    }
//    window.location.search = 'param=value';
};

function setSearchFieldsVisibility() {
    if ($('input[id*=radioButtonRent]').is(":checked")) {
        //alert('rent');
        $("#divRentParameters").show();
        $("#divSellingParameters").hide();

        $("#divRentCities").show();
        $("#divSellingCities").hide();

        $("#divRentDistricts").show();
        $("#divSellingDistricts").hide();
    }
    else {
        //alert('selling');
        $("#divRentParameters").hide();
        $("#divSellingParameters").show();

        $("#divRentCities").hide();
        $("#divSellingCities").show();

        $("#divRentDistricts").hide();
        $("#divSellingDistricts").show();
    } 
};

function setQuickSearchFieldsVisibility() {
    if ($('input[id*=radioButtonRent]').is(":checked")) {
        //alert('rent');
        $("#divRentSiteTypes").show();
        $("#divSellingSiteTypes").hide();

        $("#divRentCities").show();
        $("#divSellingCities").hide();

        $("#divRentDistricts").show();
        $("#divSellingDistricts").hide();
    }
    else {
        //alert('selling');
        $("#divRentSiteTypes").hide();
        $("#divSellingSiteTypes").show();

        $("#divRentCities").hide();
        $("#divSellingCities").show();

        $("#divRentDistricts").hide();
        $("#divSellingDistricts").show();
    }
};
