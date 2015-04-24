function Mapper() { }
Mapper.BingKey = "AnuuWOkXPOoOTQoMMDXgjjlHsVvGsEBcaCnA0xpFM2suSOlI-Sgc-4XtT2e4dkyu";
Mapper.MapDivId = 'theMap';
Mapper.MessageDivId = 'messages';
Mapper.DialogDivId = 'dialog';

Mapper.Bounds = null;
Mapper.DebugMode = true;
Mapper.GroupMode = true;

Mapper.map;
Mapper.searchManager;
Mapper.geoLocationProvider;

Mapper.gpsLayer;
Mapper.gpsEnabled = false;
Mapper.trafficLayer;
Mapper.trafficEnabled = false;

Mapper.infoBoxes = null;
Mapper.currentInfoBox = null;
Mapper.pushpins = null;

Mapper.ClearMap = function () {
    if (Mapper.map != null) {
        Mapper.infoBoxes = new JSdict();
        Mapper.pushpins = new JSdict();
        Mapper.map.entities.clear();
    }
}
Mapper.FindAds = function (zoomLevel) {
    var bounds = Mapper.map.getBounds();

    if (zoomLevel > 15) {
        var latMin = 0, latMax = 0, lonMin = 0, lonMax = 0;

        if (Mapper.GroupMode) {
            Mapper.ClearMap();
            Mapper.GroupMode = false;
        }


        if (Mapper.Bounds == null) {
            Mapper.Bounds = Microsoft.Maps.LocationRect.fromEdges(bounds.getNorth(), bounds.getWest(), bounds.getSouth(), bounds.getEast());
            Mapper.FindAdsByLocation(bounds.getSouth(), bounds.getNorth(), bounds.getWest(), bounds.getEast());
        }
    
        if (bounds.getNorth() > Mapper.Bounds.getNorth()) {
            latMin = Mapper.Bounds.getNorth();
            latMax = bounds.getNorth();
            Mapper.Bounds = Microsoft.Maps.LocationRect.fromEdges(bounds.getNorth(), Mapper.Bounds.getWest(), Mapper.Bounds.getSouth(), Mapper.Bounds.getEast());
            Mapper.FindAdsByLocation(latMin, latMax, Mapper.Bounds.getWest(), Mapper.Bounds.getEast());
            //Mapper.showMessage("N latMax: " + latMax + " latMin: " + latMin );
        }
        if (bounds.getSouth() < Mapper.Bounds.getSouth()) {
            latMin = bounds.getSouth();
            latMax = Mapper.Bounds.getSouth();
            Mapper.Bounds = Microsoft.Maps.LocationRect.fromEdges(Mapper.Bounds.getNorth(), Mapper.Bounds.getWest(), bounds.getSouth(), Mapper.Bounds.getEast());
            Mapper.FindAdsByLocation(latMin, latMax, Mapper.Bounds.getWest(), Mapper.Bounds.getEast());
            //Mapper.showMessage("S latMax: " + latMax + " latMin: " + latMin);
        }

        if (bounds.getWest() < Mapper.Bounds.getWest()) {
            lonMin = bounds.getWest();
            lonMax = Mapper.Bounds.getWest();
            Mapper.Bounds = Microsoft.Maps.LocationRect.fromEdges(Mapper.Bounds.getNorth(), bounds.getWest(), Mapper.Bounds.getSouth(), Mapper.Bounds.getEast());
            Mapper.FindAdsByLocation(Mapper.Bounds.getSouth(), Mapper.Bounds.getNorth(), lonMin, lonMax);
            //Mapper.showMessage("E lonMax: " + lonMax + " lonMin: " + lonMin);
        }
        if (bounds.getEast() > Mapper.Bounds.getEast()) {
            lonMin = Mapper.Bounds.getEast();
            lonMax = bounds.getEast();
            Mapper.Bounds = Microsoft.Maps.LocationRect.fromEdges(Mapper.Bounds.getNorth(), Mapper.Bounds.getWest(), Mapper.Bounds.getSouth(), bounds.getEast());
            Mapper.FindAdsByLocation(Mapper.Bounds.getSouth(), Mapper.Bounds.getNorth(), lonMin, lonMax);
            //Mapper.showMessage("W lonMax: " + lonMax + " lonMin: " + lonMin);
        }
        //Mapper.showMessage("N: " + Mapper.Bounds.getNorth() + " W: " + Mapper.Bounds.getWest() + " S: " + Mapper.Bounds.getSouth()+ " E: " + Mapper.Bounds.getEast());
    }
    else if (zoomLevel > 12) {
        Mapper.Bounds = null;
        Mapper.GroupMode = true;
        Mapper.ClearMap();
        Mapper.FindAdsByLocationGroup(bounds.getSouth(), bounds.getNorth(), bounds.getWest(), bounds.getEast());
        Mapper.showMessage("FindAdsByLocationGroup");

    }
    else {
        Mapper.Bounds = null;
        Mapper.GroupMode = true;
        Mapper.ClearMap();
        Mapper.FindAdsByCity(bounds.getSouth(), bounds.getNorth(), bounds.getWest(), bounds.getEast());
        Mapper.showMessage("FindAdsByCity");
    }
}
Mapper.FindAdsByCity = function (latMin, latMax, lonMin, lonMax) {
    var json = { latMin: latMin, latMax: latMax, lonMin: lonMin, lonMax: lonMax };

    $.ajax({
        type: "GET",
        url: "/en/Map/Search/SearchByCity",
        data: json,
        dataType: "json",
        success: Mapper.renderItemsGroup,
        error: Mapper.ajaxFailure
    });
}
Mapper.FindAdsByLocation = function (latMin, latMax, lonMin, lonMax) {
    var json = { latMin: latMin, latMax: latMax, lonMin: lonMin, lonMax: lonMax };

    $.ajax({
        type: "GET",
        url: "/en/Map/Search/SearchByLocation",
        data: json,
        dataType: "json",
        success: Mapper.renderItems,
        error: Mapper.ajaxFailure
    });
}
Mapper.FindAdsByLocationGroup = function (latMin, latMax, lonMin, lonMax) {
    var json = { latMin: latMin, latMax: latMax, lonMin: lonMin, lonMax: lonMax };

    $.ajax({
        type: "GET",
        url: "/en/Map/Search/SearchByLocationGroup",
        data: json,
        dataType: "json",
        success: Mapper.renderItemsGroup,
        error: Mapper.ajaxFailure
    });
}
Mapper.Init = function () {
    Microsoft.Maps.loadModule('Microsoft.Maps.Themes.BingTheme', { callback: Mapper.initCallback });

    if (Mapper.infoBoxes == null) {
        Mapper.infoBoxes = new JSdict();
    }
    if (Mapper.pushpins == null) {
        Mapper.pushpins = new JSdict();
    }

}
Mapper.Search = function (query) {
    if (Mapper.searchManager) {
        var request = {
            where: query,
            count: 1,
            callback: Mapper.geocodeCallback,
            errorCallback: Mapper.geocodeErrorCallback
        };

        Mapper.searchManager.geocode(request);
    } else {
        //Load the Search module and create a search manager.
        Microsoft.Maps.loadModule('Microsoft.Maps.Search', {
            callback: function () {
                //Create the search manager
                Mapper.searchManager = new Microsoft.Maps.Search.SearchManager(Mapper.map);

                //Perfrom search logic
                Mapper.Search(query);
            }
        });
    }
}

Mapper.addListener = function (element, eventName, eventHandler) {
    //Cross browser support for adding events. Mainly for IE7/8
    if (element.addEventListener) {
        element.addEventListener(eventName, eventHandler, false);
    } else if (element.attachEvent) {
        if (eventName == 'DOMContentLoaded') {
            eventName = 'readystatechange';
        }
        element.attachEvent('on' + eventName, eventHandler);
    }
}
Mapper.addPin = function (locationKey) {
    var ad = AdController.Get(locationKey);

    if (ad != null & Mapper.pushpins.getVal(locationKey) == null) {

        //var descriptionText = ad.Description + " " + ad.Value;
        //var titleClick = function () {
        //    window.open(ad.Url, '_blank');
        //}
        //var infobox = new Microsoft.Maps.Infobox(location, { title: title, titleClickHandler: titleClick, description: descriptionText, visible: false });
        //Microsoft.Maps.Events.addHandler(infobox, 'mouseleave', Mapper.onInfoboxMouseLeave);
        //Mapper.map.entities.push(infobox);
        //Mapper.infoBoxes.add(locationKey, infobox);

        //var pushpin = new Microsoft.Maps.Pushpin(location, { infobox: infobox });
        var pushpin = new Microsoft.Maps.Pushpin(location);
        Microsoft.Maps.Events.addHandler(pushpin, 'mouseover', Mapper.onPinMouseOver);
        Mapper.map.entities.push(pushpin);
        Mapper.pushpins.add(locationKey, pushpin);
    }

}
Mapper.addPinGroup = function (location, title, description, url, value) {
    var locationKey = Mapper.getLocationKey(location);

    if (Mapper.pushpins.getVal(locationKey) == null) {

        var descriptionText = description;
        //var titleClick = function () {
        //    Mapper.Search(url);
        //}
        var infobox = new Microsoft.Maps.Infobox(location, { title: title, description: descriptionText, visible: false });
        Microsoft.Maps.Events.addHandler(infobox, 'mouseleave', Mapper.onInfoboxMouseLeave);
        Mapper.map.entities.push(infobox);
        Mapper.infoBoxes.add(locationKey, infobox);

        var pushpin = new Microsoft.Maps.Pushpin(location, { text: description, infobox: infobox });
        //Microsoft.Maps.Events.addHandler(pushpin, 'mousedown', Mapper.onPinMouseDown);
        //Microsoft.Maps.Events.addHandler(pushpin, 'mouseout', Mapper.onPinMouseOut);
        Microsoft.Maps.Events.addHandler(pushpin, 'mouseover', Mapper.onPinMouseOver);
        Mapper.map.entities.push(pushpin);
        Mapper.pushpins.add(locationKey, pushpin);
    }

}
Mapper.ajaxFailure = function (request, status, error) {
    Mapper.showMessage("Ajax Failure: " + request.statusText);
    Mapper.showDialog(request.responseText);
}
Mapper.getLocationKey = function (location) {
    var key = "LAT" + location.latitude + "LON" + location.longitude;
    return key;
}
Mapper.hideInfobox = function (location) {
    var key = Mapper.getLocationKey(location);
    var infobox = Mapper.infoBoxes.getVal(key);
    infobox.setOptions({ visible: false });
    Mapper.currentInfoBox = null;

    var pushpin = Mapper.pushpins.getVal(key);
    pushpin.setOptions({ visible: true });

}
Mapper.renderItems = function (mapItems) {
    Mapper.showMessage("Ajax Render");
    var count = 0;
    $.each(mapItems, function (i, mapItem) {
        count = count + 1;
        var location = new Microsoft.Maps.Location(mapItem.Latitude, mapItem.Longitude, 0, null);
        var locationKey = Mapper.getLocationKey(location);
        var newAd = new Ad();
        Ad.Location = location;
        Ad.Title = mapItem.Title;
        Ad.Description = mapItem.Description;
        Ad.Url = mapItem.Url;
        Ad.Value = mapItem.Value;

        AdController.Set(locationKey)
        Mapper.addPin(locationKey);
    });
    Mapper.showMessage("Ads Loaded: " + count);
}
Mapper.renderItemsGroup = function (mapItems) {
    Mapper.showMessage("Ajax Render");
    var count = 0;
    $.each(mapItems, function (i, mapItem) {
        count = count + 1;
        var location = new Microsoft.Maps.Location(mapItem.Latitude, mapItem.Longitude, 0, null);
        Mapper.addPinGroup(location, mapItem.Title, mapItem.Description, mapItem.Url, mapItem.Value);
    });
    Mapper.showMessage("Grouped Ads Loaded: " + count);
}
Mapper.setMapMode = function (mode) {
    var m;

    switch (mode) {
        case 'auto':
            m = Microsoft.Maps.MapTypeId.auto;
            break;
        case 'aerial':
            m = Microsoft.Maps.MapTypeId.aerial;
            break;
        case 'birdseye':
            m = Microsoft.Maps.MapTypeId.birdseye;
            break;
        case 'os':
            m = Microsoft.Maps.MapTypeId.ordnanceSurvey;
            break;
        case 'road':
        default:
            m = Microsoft.Maps.MapTypeId.road;
            break;
    }

    Mapper.map.setView({ mapTypeId: m });
}
Mapper.showDialog = function (msg) {
    if (Mapper.DebugMode) {
        var div = $("#" + Mapper.DialogDivId);
        div.html(msg);
        $("#" + Mapper.DialogDivId).dialog();
        //alert(msg);
        //div.html("");
    }
}
Mapper.showInfobox = function (locationKey) {
    var ad = AdController.Get(locationKey);

    if (ad != null) {
        var descriptionText = ad.Description + " " + ad.Value;
        var titleClick = function () {
            window.open(ad.Url, '_blank');
        }

        var infobox = new Microsoft.Maps.Infobox(location, { title: ad.Title, titleClickHandler: titleClick, description: descriptionText, visible: true });
        Microsoft.Maps.Events.addHandler(infobox, 'mouseleave', Mapper.onInfoboxMouseLeave);
        Mapper.map.entities.push(infobox);
        //Mapper.infoBoxes.add(locationKey, infobox);
        if (Mapper.currentInfoBox != null) {
            Mapper.currentInfoBox.setOptions({ visible: false });
        }
        Mapper.currentInfoBox = infobox;

        var pushpin = Mapper.pushpins.getVal(key);
        pushpin.setOptions({ visible: false });
    }


    //var infobox = Mapper.infoBoxes.getVal(key);
    //infobox.setOptions({ visible: true });

}
Mapper.showMessage = function (msg) {
    if (Mapper.DebugMode) {
        var div = $("#" + Mapper.MessageDivId);
        var content = msg + "<br/>" + div.html();
        div.html(content);
        //alert(msg);
    }
}
Mapper.toggleGPS = function() {
    Mapper.gpsEnabled = !gpsEnabled;

        // Initialize the location provider
    if (!Mapper.geoLocationProvider) {
        Mapper.geoLocationProvider = new Microsoft.Maps.GeoLocationProvider(map);
        }

        //Clear the GPS layer 
        Mapper.gpsLayer.clear();

        if (Mapper.gpsEnabled) {
            // Get the user's current location
            Mapper.geoLocationProvider.getCurrentPosition({
                successCallback: function (e) {
                    Mapper.gpsLayer.push(new Microsoft.Maps.Pushpin(e.center));
                },
                errorCallback: function (e) {
                    Mapper.showMessage(e.internalError);
                }
            });
        } else {
            //Remove the accuracy circle and cancel any request that might be processing
            Mapper.geoLocationProvider.removeAccuracyCircle();
            Mapper.geoLocationProvider.cancelCurrentRequest();
        }
    }
Mapper.toggleTraffic = function() {
    Mapper.trafficEnabled = !Mapper.trafficEnabled;

        //Check to see if the traffic layer exists
    if (Mapper.trafficLayer) {
        if (Mapper.trafficEnabled) {
            Mapper.trafficLayer.show();
            } else {
            Mapper.trafficLayer.hide();
            }
        } else {
            //Load the traffic module and create the traffic layer.
            Microsoft.Maps.loadModule('Microsoft.Maps.Traffic', {
                callback: function () {
                    //Create the traffic layer
                    Mapper.trafficLayer = new Microsoft.Maps.Traffic.TrafficLayer(map);

                    //Get the base tile layer and set the opacity
                    var layer = trafficLayer.getTileLayer();
                    layer.setOptions({ opacity: 0.5 });

                    Mapper.trafficLayer.show();
                }
            });
        }
}
Mapper.updateNavBar = function () {
    //if (Mapper.map.isRotationEnabled()) {
    //        document.getElementById('rotationBtns').style.display = '';
    //    } else {
    //        document.getElementById('rotationBtns').style.display = 'none';
    //    }
}

Mapper.geocodeCallback = function (response, userData) {
    if (response &&
        response.results &&
        response.results.length > 0) {
        var result = response.results[0];
        var location = new Microsoft.Maps.Location(result.location.latitude, result.location.longitude);
        var locationRect = new Microsoft.Maps.LocationRect(location, result.bestView.width, result.bestView.height);
        //Zoom to result
        Mapper.map.setView({ center: location, bounds: locationRect });
        //Mapper.map.setView(result.bestView);
        Mapper.FindAds(Mapper.map.getZoom());

    } else {
        showMessage("Not results found.");
    }
}
Mapper.geocodeErrorCallback = function (request) {
    Mapper.showMessage("Unable to Geocode request.");

    //document.getElementById('searchPanel').style.display = 'none';
}
Mapper.initCallback = function () {
    var mapOptions = {
        credentials: Mapper.BingKey,
        showDashboard: true,
        showCopyright: false,
        showScalebar: false,
        enableSearchLogo: false,
        enableClickableLogo: false,
        backgroundColor: new Microsoft.Maps.Color(255, 255, 255, 255)
    };

    // Initialize the map
    Mapper.map = new Microsoft.Maps.Map(document.getElementById(Mapper.MapDivId), mapOptions);
    Microsoft.Maps.Events.addHandler(Mapper.map, 'viewchangeend', Mapper.onViewChange);

    Mapper.gpsLayer = new Microsoft.Maps.EntityCollection();
    Mapper.map.entities.push(Mapper.gpsLayer);
}
Mapper.onInfoboxMouseLeave = function (e) {
    if (e.targetType == 'infobox') {
        Mapper.hideInfobox(e.target.getLocation());
    }
}
Mapper.onPinMouseDown = function (e) {
    if (e.targetType == 'pushpin') {
        Mapper.showInfobox(e.target.getLocation());
    }
}
Mapper.onPinMouseOut = function (e) {
    if (e.targetType == 'pushpin') {
        //Mapper.showInfobox(e.target.getLocation());
    }
}
Mapper.onPinMouseOver = function (e) {
    if (e.targetType == 'pushpin') {
        var locationKey = Mapper.getLocationKey(e.target.getLocation());
        Mapper.showInfobox(locationKey);
    }
}
Mapper.onViewChange = function (e) {
    Mapper.FindAds(Mapper.map.getZoom());
}


function JSdict() {
    this.Keys = [];
    this.Values = [];
}

// Check if dictionary extensions aren't implemented yet.
// Returns value of a key
if (!JSdict.prototype.getVal) {
    JSdict.prototype.getVal = function (key) {
        if (key == null) {
            return "Key cannot be null";
        }
        for (var i = 0; i < this.Keys.length; i++) {
            if (this.Keys[i] == key) {
                return this.Values[i];
            }
        }
        return null;
    }
}
// Check if dictionary extensions aren't implemented yet.
// Updates value of a key
if (!JSdict.prototype.update) {
    JSdict.prototype.update = function (key, val) {
        if (key == null || val == null) {
            return "Key or Value cannot be null";
        }
        // Verify dict integrity before each operation
        if (keysLength != valsLength) {
            return "Dictionary inconsistent. Keys length don't match values!";
        }
        var keysLength = this.Keys.length;
        var valsLength = this.Values.length;
        var flag = false;
        for (var i = 0; i < keysLength; i++) {
            if (this.Keys[i] == key) {
                this.Values[i] = val;
                flag = true;
                break;
            }
        }
        if (!flag) {
            return "Key does not exist";
        }
    }
}
// Check if dictionary extensions aren't implemented yet.
// Adds a unique key value pair
if (!JSdict.prototype.add) {
    JSdict.prototype.add = function (key, val) {
        // Allow only strings or numbers as keys
        if (typeof (key) == "number" || typeof (key) == "string") {
            if (key == null || val == null) {
                return "Key or Value cannot be null";
            }
            if (keysLength != valsLength) {
                return "Dictionary inconsistent. Keys length don't match values!";
            }
            var keysLength = this.Keys.length;
            var valsLength = this.Values.length;
            for (var i = 0; i < keysLength; i++) {
                if (this.Keys[i] == key) {
                    return "Duplicate keys not allowed!";
                }
            }
            this.Keys.push(key);
            this.Values.push(val);
        }
        else {
            return "Only number or string can be key!";
        }
    }
}
// Check if dictionary extensions aren't implemented yet.
// Removes a key value pair
if (!JSdict.prototype.remove) {
    JSdict.prototype.remove = function (key) {
        if (key == null) {
            return "Key cannot be null";
        }
        if (keysLength != valsLength) {
            return "Dictionary inconsistent. Keys length don't match values!";
        }
        var keysLength = this.Keys.length;
        var valsLength = this.Values.length;
        var flag = false;
        for (var i = 0; i < keysLength; i++) {
            if (this.Keys[i] == key) {
                this.Keys.shift(key);
                this.Values.shift(this.Values[i]);
                flag = true;
                break;
            }
        }
        if (!flag) {
            return "Key does not exist";
        }
    }
}