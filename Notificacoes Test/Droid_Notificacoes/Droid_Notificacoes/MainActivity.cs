using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using Android.Support.V4.App;


namespace Droid_Notificacoes
{
    [Activity(Label = "Teste Notificações", MainLauncher = true, Icon = "@drawable/IconeRedond")]
    public class MainActivity : Activity
    {
        int iNotificacao = 50;

        //[System.Obsolete]

        [System.Obsolete]
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            Button btnNotifica = FindViewById<Button>(Resource.Id.btnNotifica);

            btnNotifica.Click += delegate
            {                                              
                
                Intent novaIntent = new Intent(this, typeof(MainActivity));
                PendingIntent resultPendingIntent = PendingIntent.GetActivity(this, 0, novaIntent, PendingIntentFlags.CancelCurrent);
                string conteudo = "Deus abençoe o seu dia. Teste";

                NotificationCompat.Builder builder = new NotificationCompat.Builder(this)
                .SetAutoCancel(true)
                .SetContentIntent(resultPendingIntent)
                .SetContentTitle("Notificacão NVI")
                .SetSmallIcon(Resource.Drawable.Icon)
                .SetStyle(new NotificationCompat.BigTextStyle().BigText(conteudo))
                .SetContentText(conteudo);

                NotificationManager gerenciaNotificacao = (NotificationManager)GetSystemService(Context.NotificationService);
                gerenciaNotificacao.Notify(iNotificacao, builder.Build());
                

            };
        }
    }
}

