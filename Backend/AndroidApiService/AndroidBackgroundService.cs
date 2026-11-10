using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;

namespace AndroidApiService;

[Service(Enabled = true, Exported = false)]
public class AndroidBackgroundService : Service
{
    private const int NotificationId = 10001;
    private const string ChannelId = "BackgroundServiceChannel";

    public override IBinder? OnBind(Intent? intent) => null;

    public override StartCommandResult OnStartCommand(Intent? intent, [GeneratedEnum] StartCommandFlags flags, int startId)
    {
        // 1. Création de la notification obligatoire (Android 8.0+) pour maintenir le service en Foreground
        CreateNotificationChannel();
        var notification = new Notification.Builder(this, ChannelId)
            .SetContentTitle("Gestionnaire de tâches")
            .SetContentText("Le service d'arrière-plan est actif.")
            .SetSmallIcon(Android.Resource.Drawable.IcDialogInfo)
            .Build();

        StartForeground(NotificationId, notification);

        // TODO: Gérez ici vos tâches et alarmes en toute liberté

        // START_STICKY demande au système de relancer le service s'il est éliminé
        return StartCommandResult.Sticky;
    }

    private void CreateNotificationChannel()
    {
        if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
        {
            var channel = new NotificationChannel(ChannelId, "Service d'arrière-plan", NotificationImportance.Low)
            {
                Description = "Canal pour maintenir les tâches et alarmes actives"
            };

            var notificationManager = GetSystemService(NotificationService) as NotificationManager;
            notificationManager?.CreateNotificationChannel(channel);
        }
    }

    public override void OnDestroy()
    {
        // Nettoyage éventuel lors de l'arrêt du service
        base.OnDestroy();
    }
}

[BroadcastReceiver(Enabled = true, Exported = true, DirectBootAware = true)]
[IntentFilter(new[] { Intent.ActionBootCompleted, "android.intent.action.QUICKBOOT_POWERON" })]
public class BootReceiver : BroadcastReceiver
{
    public override void OnReceive(Context? context, Intent? intent)
    {
        if (context == null) return;

        var serviceIntent = new Intent(context, typeof(AndroidBackgroundService));

        if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
        {
            context.StartForegroundService(serviceIntent);
        }
        else
        {
            context.StartService(serviceIntent);
        }
    }
}
