# Frequently Asked Questions

### The openstreetmap layer does not show 
The most common reason is that the default user-agent used by the osm layer is blocked.  The default user-agent gets blocked by osm at some point because there is always someone somewhere abusing the api. Within your app you need to use a user-agent that is specific to your app. We change the user agent from time to time so that the samples work but it will probably get blocked again at some point. Better not to wait until that happens and create you own user-agent right away.

```csharp
MapControl.Map.Layers.Add(OpenStreetMap.CreateTileLayer("your-user-agent"));
```

### Why is all my data in a small area near the west coast of Africa?
This is because the background data is in SphericalMercator (it is in the SphericalMercator 
coordinate system) and the foreground data is in WGS84 (latlon). Use 
SphericalMercator.FromLonLat to transform it.
Note: There can be many other forms of mixing up coordinate systems, but this is the most common.

### Why does NavigateTo zoom into an area near the west coast of Africa?
This is because the coordinates you pass to NavigateTo are in WGS84 whereas the
background data is in SphericalMercator. Use SphericalMercator.FromLonLat to transform 
the NavigateTo arguments to SphericalMercator.
Note: There can be many other forms of mixing up coordinate systems, but this is the most common.

### How can I get rid of the white dots or black lines?
In Mapsui v1 and v2 a layer is created with a default style (```Layer.Style```). This style applies to all features
in the layer. You need to set the Style field to null if you do not want to use it (```Layer.Style = null```). 
The default style shows as a white dot on Point geometries and a black line on LineStrings and Polygons.
Since most users work with styles on the *feature* they are not aware of the style on the *layer*, this make 
the default style confusing and it should be removed in v3.
