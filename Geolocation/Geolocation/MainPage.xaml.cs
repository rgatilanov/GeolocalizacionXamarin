using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Geolocation
{
    public partial class MainPage : ContentPage
    {
        double latitud;
        double longitud;

        public MainPage()
        {
            InitializeComponent();
            IniciarGPS();
        }

        private async void IniciarGPS()
        {
            // Acceso a la API
            var geolocator = CrossGeolocator.Current;
            // Precisión (en metros)
            geolocator.DesiredAccuracy = 50;
            // Servicio existente en el dispositivo
            if (geolocator.IsGeolocationAvailable)
            {
                // GPS activado en el dispositivo
                if (geolocator.IsGeolocationEnabled)
                {
                    //Si el dispositivo esta listo para obtener los cambios de ubicación.
                    if (!geolocator.IsListening)
                    {
                        //Comienza a escuchar los cambios de la localización cada (Tiempo ,Distancia recorrida)
                        await geolocator.StartListeningAsync(TimeSpan.FromSeconds(1), 5);
                    }
                    geolocator.PositionChanged += (cambio, args) =>
                    {
                        var loc = args.Position;
                        lon.Text = loc.Longitude.ToString();
                        longitud = double.Parse(lon.Text);
                        lat.Text = loc.Latitude.ToString();
                        latitud = double.Parse(lat.Text);
                    };
                }
            }
        }

        private async void MostrarMapa(object sender, EventArgs args1)
        {
            MapLaunchOptions options = new MapLaunchOptions { Name = "Mi posición actual" };
            await Map.OpenAsync(latitud, longitud, options);
        }
    }
}
