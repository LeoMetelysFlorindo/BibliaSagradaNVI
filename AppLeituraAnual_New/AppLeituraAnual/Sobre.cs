using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Java.Interop;

namespace AppBibliaNVI
{
    [Activity(Label = "Sobre")]
    public class Sobre : Activity
    {

        [Export("btnFecharClicked")]
        public void btnFecharClicked_Click(View v)
        {
                                               
            // Fechar a tela Atual 29/12/2020 21:34h            
            Finish();


        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Sobre);


            var TxtDesenvolvedor = FindViewById<TextView>(Resource.Id.textView1);
            var TxtNomeDesenvolvedor = FindViewById<TextView>(Resource.Id.textView2);
            var TxtVersao = FindViewById<TextView>(Resource.Id.textView29);
            var TxtLocal = FindViewById<TextView>(Resource.Id.textView3);
            var TxtVersiculo = FindViewById<TextView>(Resource.Id.textView4);
            var TxtNVI = FindViewById<TextView>(Resource.Id.textView64);
            var TxtBlog = FindViewById<TextView>(Resource.Id.textView74);

            var TelaSobre = FindViewById<LinearLayout>(Resource.Id.linearLayoutSobre);


            TelaSobre.SetBackgroundColor(Android.Graphics.Color.Blue);
            TxtNomeDesenvolvedor.SetTextColor(Android.Graphics.Color.Orange);
            TxtLocal.SetTextColor(Android.Graphics.Color.Yellow);

            TxtDesenvolvedor.SetTextColor(Android.Graphics.Color.Yellow);
            TxtVersao.SetTextColor(Android.Graphics.Color.Orange);
            TxtNVI.SetTextColor(Android.Graphics.Color.Orange);
            TxtBlog.SetTextColor(Android.Graphics.Color.Yellow);
            TxtVersiculo.SetTextColor(Android.Graphics.Color.Orange);            
            
        }
    }
}