using Android.App;
using Android.Content.PM;
using Android.OS;
using Android;
using Android.Util;
using Permission = Android.Content.PM.Permission;
using System.IO;
using Acr.UserDialogs;

namespace MobilSemProjekt.Droid {
    [Activity(Label = "XamarinMapsGarbage", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity {

        readonly string[] PermissionsLocation =
        {
    Manifest.Permission.AccessCoarseLocation,
    Manifest.Permission.AccessFineLocation
    };

        const int RequestLocationId = 0;
        protected override void OnCreate(Bundle savedInstanceState) {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            base.OnCreate(savedInstanceState);


            const string permission = Manifest.Permission.AccessFineLocation;
            if (CheckSelfPermission(permission) == (int)Permission.Granted) {
                Log.Error("ERROR", "GRANTED");
            }
            else {
                Log.Error("ERROR", "DENIED");
                RequestPermissions(PermissionsLocation, RequestLocationId);
                if (CheckSelfPermission(permission) == (int)Permission.Granted) {
                    Log.Error("ERROR", "NOW ITS GRANTED");
                }
                else {
                    Log.Error("ERROR", "STILL DENIED");
                }
            }
            string dbName = "Marker.db";
            string folderPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string dbPath = Path.Combine(folderPath, dbName);

            Xamarin.FormsGoogleMaps.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            UserDialogs.Init(this);
            LoadApplication(new App());


        }
    }
}