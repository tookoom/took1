function PropertyAds() { }

PropertyAds.dragShape = null;
PropertyAds.dragPixel = null;
PropertyAds.MapDivId = 'theMap';
PropertyAds._map = null;
PropertyAds._points = [];
PropertyAds._shapes = [];
PropertyAds.ipInfoDbKey = '';

PropertyAds.LoadMap = function (latitude, longitude, onMapLoaded) {
    PropertyAds._map = new VEMap(PropertyAds.MapDivId);

    var options = new VEMapOptions();

    options.EnableBirdseye = false

    this._map.SetDashboardSize(VEDashboardSize.Small);

    if (onMapLoaded != null)
        PropertyAds._map.onLoadMap = onMapLoaded;

    if (latitude != null && longitude != null) {
        var center = new VELatLong(latitude, longitude);
    }

    PropertyAds._map.LoadMap(center, null, null, null, null, null, null, options);
}

PropertyAds.EnableMapMouseClickCallback = function () {
    PropertyAds._map.AttachEvent("onmousedown", PropertyAds.onMouseDown);
    PropertyAds._map.AttachEvent("onmouseup", PropertyAds.onMouseUp);
    PropertyAds._map.AttachEvent("onmousemove", PropertyAds.onMouseMove);
}

PropertyAds.onMouseDown = function (e) {
    if (e.elementID != null) {
        PropertyAds.dragShape = PropertyAds._map.GetShapeByID(e.elementID);
        return true;
    }
}

PropertyAds.onMouseUp = function (e) {
    if (PropertyAds.dragShape != null) {
        var x = e.mapX;
        var y = e.mapY;
        PropertyAds.dragPixel = new VEPixel(x, y);
        var LatLong = PropertyAds._map.PixelToLatLong(PropertyAds.dragPixel);
        $("#Latitude").val(LatLong.Latitude.toString());
        $("#Longitude").val(LatLong.Longitude.toString());
        PropertyAds.dragShape = null;

        PropertyAds._map.FindLocations(LatLong, PropertyAds.getLocationResults);
    }
}

PropertyAds.onMouseMove = function (e) {
    if (PropertyAds.dragShape != null) {
        var x = e.mapX;
        var y = e.mapY;
        PropertyAds.dragPixel = new VEPixel(x, y);
        var LatLong = PropertyAds._map.PixelToLatLong(PropertyAds.dragPixel);
        PropertyAds.dragShape.SetPoints(LatLong);
        return true;
    }
}

PropertyAds.onEndDrag = function (e) {
    $("#Latitude").val(e.LatLong.Latitude.toString());
    $("#Longitude").val(e.LatLong.Longitude.toString());
}

PropertyAds.ClearMap = function () {
    if (PropertyAds._map != null) {
        PropertyAds._map.Clear();
    }
    PropertyAds._points = [];
    PropertyAds._shapes = [];
}

PropertyAds.LoadPin = function (LL, name, description, draggable) {
    if (LL.Latitude == 0 || LL.Longitude == 0) {
        return;
    }

    var shape = new VEShape(VEShapeType.Pushpin, LL);

    if (draggable == true) {
        shape.Draggable = true;
        shape.onenddrag = PropertyAds.onEndDrag;
    }

    //Make a Pushpin with a title and description
    shape.SetTitle("<span class=\"pinTitle\"> " + escape(name) + "</span>");

    if (description !== undefined) {
        shape.SetDescription("<p class=\"pinDetails\">" + escape(description) + "</p>");
    }

    PropertyAds._map.AddShape(shape);
    PropertyAds._points.push(LL);
    PropertyAds._shapes.push(shape);
}

PropertyAds.FindAddressOnMap = function (where) {
    var numberOfResults = 1;
    var setBestMapView = true;
    var showResults = true;
    var defaultDisambiguation = true;

    PropertyAds._map.Find("", where, null, null, null,
                         numberOfResults, showResults, true, defaultDisambiguation,
                         setBestMapView, PropertyAds._callbackForLocation);
}

PropertyAds._callbackForLocation = function (layer, resultsArray, places, hasMore, VEErrorMessage) {
    PropertyAds.ClearMap();

    if (places == null) {
        PropertyAds._map.ShowMessage(VEErrorMessage);
        return;
    }

    //Make a pushpin for each place we find
    $.each(places, function (i, item) {
        var description = "";
        if (item.Description !== undefined) {
            description = item.Description;
        }
        var LL = new VELatLong(item.LatLong.Latitude,
                        item.LatLong.Longitude);

        PropertyAds.LoadPin(LL, item.Name, description, true);
    });

    //Make sure all pushpins are visible
    if (PropertyAds._points.length > 1) {
        PropertyAds._map.SetMapView(PropertyAds._points);
    }

    //If we've found exactly one place, that's our address.
    //lat/long precision was getting lost here with toLocaleString, changed to toString
    if (PropertyAds._points.length === 1) {
        $("#Latitude").val(PropertyAds._points[0].Latitude.toString());
        $("#Longitude").val(PropertyAds._points[0].Longitude.toString());
    }
}



PropertyAds._renderDonors = function (donors) {

    PropertyAds.ClearMap();

    $.each(donors, function (i, donor) {

        var LL = new VELatLong(donor.Latitude, donor.Longitude, 0, null);

        // Add Pin to Map
        PropertyAds.LoadPin(LL, donor.DonorID, donor.Description, false);


    });

    // Adjust zoom to display all the pins.
    if (PropertyAds._points.length > 1) {
        PropertyAds._map.SetMapView(PropertyAds._points);
    }

    // Display the event's pin-bubble on hover.
    $(".DonorsItem").each(function (i, Donors) {
        $(Donors).hover(
            function () { PropertyAds._map.ShowInfoBox(PropertyAds._shapes[i]); },
            function () { PropertyAds._map.HideInfoBox(PropertyAds._shapes[i]); }
        );
    });

}

PropertyAds.FindAddress = function (where) {
    var numberOfResults = 1;
    var setBestMapView = true;
    var showResults = true;
    var defaultDisambiguation = true;

    PropertyAds._map.Find("", where, null, null, null,
                         numberOfResults, showResults, true, defaultDisambiguation,
                         setBestMapView, PropertyAds._callbackUpdateMapDonors);

}

PropertyAds._callbackUpdateMapDonors = function (layer, resultsArray, places, hasMore, VEErrorMessage) {

    var center = PropertyAds._map.GetCenter();

    var json = { latitude: center.Latitude, longitude: center.Longitude };
    $.ajax({
        type: "GET",
        url: "/Search/SearchByLocation",
        data: json,
        dataType: "json",
        success: PropertyAds._renderDonors,
        error: PropertyAds.ajaxFailure
    });
}

PropertyAds.ajaxFailure = function (request, status, error) {
    alert(error);
}