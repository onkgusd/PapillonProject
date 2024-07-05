using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using System;

namespace PapillonProject.Droid
{
    [Activity(Label = "PapillonProject", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        //MapView _mapView;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            Xamarin.FormsMaps.Init(this, savedInstanceState);

            LoadApplication(new App());

            try
            {
                /*Native.Initialize();

                _mapView = new MapView(this, new MapViewSurface(this));
                _mapView.MapTilt = 0;
                _mapView.MapCenter = new GeoCoordinate(52.207767, 8.803513);
                _mapView.MapZoom = 12;
                _mapView.Map = new Map();

                _mapLayer = _mapView.Map.AddLayerTile("http://a.tile.openstreetmap.de/tiles/osmde/{0}/{1}/{2}.png");
                
                SetContentView(_mapView);*/
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}