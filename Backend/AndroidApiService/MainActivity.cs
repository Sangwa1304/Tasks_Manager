using Android.App;
using Android.Content;
using Android.OS;

namespace AndroidApiService;

[Activity(Label = "AndroidApiService", MainLauncher = true, Exported = true)]
public class MainActivity : Activity
{
    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);

        // Lancement préventif du service au premier lancement de l'app si nécessaire
        StartBackgroundService();
    }

    private void StartBackgroundService()
    {
        var serviceIntent = new Intent(this, typeof(AndroidBackgroundService));

        if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
        {
            StartForegroundService(serviceIntent);
        }
        else
        {
            StartService(serviceIntent);
        }
    }
}