﻿using Mapsui.Cache;
using Mapsui.Layers;
using Mapsui.Logging;
using Mapsui.Providers.Wfs;
using Mapsui.Styles;
using System.Net;
using System.Threading.Tasks;

#pragma warning disable IDISP001 // Dispose created

namespace Mapsui.Samples.Common.Maps.DataFormats;

public class WfsPointsSample : ISample
{
    public string Name => " 7 WFS Points";
    public string Category => "Data Formats";
    public static IUrlPersistentCache? DefaultCache { get; set; }

    private const string crs = "EPSG:31254";

    public async Task<Map> CreateMapAsync()
    {
        try
        {
            var map = new Map() { CRS = crs };
            var provider = await CreateWfsProviderAsync();
            map.Layers.Add(CreateWfsLayer(provider));

            MRect bbox = new(
                -34900
                , 255900
                , -34800
                , 256000
            );

            map.Home = n => n.CenterOnAndZoomTo(new MPoint(-34800, 255950), 10);

            return map;

        }
        catch (WebException ex)
        {
            Logger.Log(LogLevel.Warning, ex.Message, ex);
            throw;
        }
    }

    private static ILayer CreateWfsLayer(WFSProvider provider)
    {
        return new Layer("Laser Points")
        {
            SymbolStyle = new SymbolStyle { Fill = new Brush(Color.Red), SymbolScale = 1 },
            DataSource = provider,
            IsMapInfoLayer = true
        };
    }

    private static async Task<WFSProvider> CreateWfsProviderAsync()
    {
        var provider = await WFSProvider.CreateAsync(
            "https://vogis.cnv.at/geoserver/vogis/laser_2002_04_punkte/ows",
            "vogis",
            "laser_2002_04_punkte", 
            WFSProvider.WFSVersionEnum.WFS_1_1_0,
            persistentCache: DefaultCache);

        provider.CRS = crs;

        await provider.InitAsync();
        return provider;
    }
}
