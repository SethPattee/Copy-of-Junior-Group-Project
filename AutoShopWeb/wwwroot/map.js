//var map;

//function initializeMap(mapsKey) {
//    // Initialize the Azure Maps
//    atlas.setSubscriptionKey(mapsKey);
//    // Create the map instance
//    map = new atlas.Map("mapDiv", {
//        view: "Auto",
//        center: [-122.129, 47.640],
//        zoom: 3
//    });

//    navigator.geolocation.getCurrentPosition(function (position) {
//        var userLocation = [position.coords.longitude, position.coords.latitude];
//        map.setCamera({
//            center: userLocation,
//            zoom: 13
//        });
//    });

//    var marker = new atlas.HtmlMarker({
//        color: 'DodgerBlue',
//        text: 'O',
//        position: [- 111.583795, 39.360108],
//        popup: new atlas.Popup({
//            content: `<div style="padding:10px">You Are Here =)</div>`,
//            pixelOffset: [0, -30]
//        })
//    });
//    map.markers.add(marker);

//    map.events.add('click', marker, () => {
//        marker.togglePopup();
//    });
//}

var map;

function initializeMap(mapsKey) {
    // Initialize the Azure Maps
    atlas.setSubscriptionKey(mapsKey);
    // Create the map instance
    map = new atlas.Map("mapDiv", {
        view: "Auto",
        center: [-111.8056, 39.5546], // Centered on Fountain Green, Utah
        zoom: 10 // Zoom level adjusted to show the surrounding area
        //center: [-122.129, 47.640],
        //zoom: 3
    });

    // Specify the longitude and latitude coordinates below
    var longitude = -111.634008298091765; // Example longitude
    var latitude = 39.62945368691943; // Example latitude

    var marker = new atlas.HtmlMarker({
        color: 'DodgerBlue',
        text: 'O',
        position: [longitude, latitude], // Specify the longitude and latitude here
        popup: new atlas.Popup({
            content: `<div style="padding:10px">F'NG Autowerks</div>`,
            pixelOffset: [0, -30]
        })
    });
    map.markers.add(marker);

    map.events.add('click', marker, () => {
        marker.togglePopup();
    });
}
