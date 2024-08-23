using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Lang;
using Java.Interop;
using System.Collections;
using System.Xml;
using System.IO;
using Android.Content.PM;
using Android.Graphics;
using Android.Views.Animations;
using Android.Content.Res;
using System.Threading;
using System.Timers;

[assembly: Permission(Name = "android.permission.READ_EXTERNAL_STORAGE")]
[assembly: Permission(Name = "android.permission.WRITE_EXTERNAL_STORAGE")]
namespace AppBibliaNVI
{
    [Activity(Label = "BÍBLIA SAGRADA NVI",  ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : Activity
    {

        bool eTelaBusca = false;
        public ArrayList lista;
        public ArrayAdapter adp1;
        public ListView lv1;
        string sValor = "";


        [Export("btnOrdemRClicked")]
        public void btnOC_Click(View v)
        {
            // Chamar a tela de Lazer           
            //var atividadeListaCronologica = new Intent(this, typeof(ActivityPlanoOC));
            //StartActivity(atividadeListaCronologica);

            sValor = "AT"; 
            var activity2 = new Intent(this, typeof(LeituraAT));
            activity2.PutExtra("MyData", sValor);
            StartActivity(activity2);
        }

        [Export("btnPlano1Clicked")]
        public void btnPL1_Click(View v)
        {
            // Chamar a tela de Lazer           
            //var atividadeListaCronologica = new Intent(this, typeof(ActivityPlanoOC));
            //StartActivity(atividadeListaCronologica);

            sValor = "NT";
            var activity2 = new Intent(this, typeof(LeituraAT));
            activity2.PutExtra("MyData", sValor);
            StartActivity(activity2);
        }

        [Export("btnSairClicked")]
        public void button_Click(View v)
        {

            // Fechar o Aplicativo
            //var activity = (Activity)this.ApplicationContext;
            //activity.FinishAffinity();

            // Fechar o Aplicativo 21/12/2020 18:03h
            //System.Diagnostics.Process.GetCurrentProcess().Kill(); 

            //Fechar o aplicativo 15/03/20212 11:21h
            // Android.OS.Process.KillProcess(Android.OS.Process.MyPid());  // Testar este se preciso
            System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow(); // Isso fecha o principal!

            //Testar também este se não funcionar o de cima
            //System.exit(0);

        }
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            
            //Algoritimo a desenvolver: 09/04/2017 14:48h         
            var metrics = Resources.DisplayMetrics;
            var widthInDp = ConvertPixelsToDp(metrics.WidthPixels);
            var heightInDp = ConvertPixelsToDp(metrics.HeightPixels);

            //remove título desta Activity
            //Window.RequestFeature(WindowFeatures.NoTitle);

            SetContentView(Resource.Layout.Main);                    


        }
             
        private int ConvertPixelsToDp(float pixelValue)
        {
            var dp = (int)((pixelValue) / Resources.DisplayMetrics.Density);
            return dp;
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            base.OnCreateOptionsMenu(menu);
            MenuInflater inflater = this.MenuInflater;
            inflater.Inflate(Resource.Menu.menu1, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.Pesquisar: 
                //    // Pesquisar no APP                 
                    var atividadeConfig = new Intent(this, typeof(Configurar));
                    StartActivity(atividadeConfig);
                    break;

                case Resource.Id.Sobre:
                    // SOBRE O APP                 
                    var atividadeSobre = new Intent(this, typeof(Sobre));
                    StartActivity(atividadeSobre);
                    break;

            }
            return base.OnOptionsItemSelected(item);
        }

    }
}

