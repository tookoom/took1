function test(){
alert('teste');
};

function setSearchType() {
    //alert('1');
    var quickSearch = $.url().param('searchType');
    //alert(quickSearch);
    if (quickSearch == 'rent') {
        $('input[id*=radioButtonRent]')[0].checked = true;
    }
    if (quickSearch == 'selling') {
        $('input[id*=radioButtonBuy]')[0].checked = true;
    }
};

function setSearchFieldsVisibility() {
    //alert('2');
    if ($('input[id*=radioButtonRent]').is(":checked")) {
        //alert('rent');
        $("#divRentPriceFrom").show();
        $("#divSellingPriceFrom").hide();

        $("#divRentPriceTo").show();
        $("#divSellingPriceTo").hide();

        $("#divRentCities").show();
        $("#divSellingCities").hide();

        $("#divRentDistricts").show();
        $("#divSellingDistricts").hide();

        $("#divRentSiteTypes").show();
        $("#divSellingSiteTypes").hide();
    }
    else {
        //alert('selling');
        $("#divRentPriceFrom").hide();
        $("#divSellingPriceFrom").show();

        $("#divRentPriceTo").hide();
        $("#divSellingPriceTo").show();

        $("#divRentCities").hide();
        $("#divSellingCities").show();

        $("#divRentDistricts").hide();
        $("#divSellingDistricts").show();

        $("#divRentSiteTypes").hide();
        $("#divSellingSiteTypes").show();
    } 
};

function setQuickSearchFieldsVisibility() {
    //alert('1');
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
