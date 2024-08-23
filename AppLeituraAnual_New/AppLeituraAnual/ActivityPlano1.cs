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
using System.Threading;
using System.Timers;
using Java.Lang;
using Java.Interop;
using System.Collections;
using System.Xml;
using System.IO;
using Android.Content.PM;
using Android.Graphics;
using Android.Views.Animations;
using Android.Content.Res;
using Android.Media;
using static Android.Provider.MediaStore.Images;
namespace AppLeituraAnual
{
    [Activity(Label = "ActivityPlano1")]
    public class ActivityPlano1 : Activity
    {
        [Export("btnSalvar2Clicked")]
        public void btnFecharClicked_Click(View v)
        {
            // Fechar a tela Atual 29/12/2020 21:34h            
            Finish();
        }

        ImageView screenshotImage;
        public Button BotaoPrint;
        public Button BotaoProximo;
        public Button BotaoAnterior;
        public Button BotaoVoltar;
        string sValor = "";
        bool FezEscolha = false;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Pega o valor atribuido no Configurar para definir que Plenoa está ativo - 30/12/2020 - 19:11h
            string text_Valor = Intent.GetStringExtra("MyData");
            sValor = text_Valor;

            // Remove título desta Activity
            Window.RequestFeature(WindowFeatures.NoTitle);

            // Create your application here
            SetContentView(Resource.Layout.PlanoPL1);

            // 30/12/2020 20:09h
            // Deve ler a data atual
            // Baseado na data atual mostrar o dia e o mês
            // Para o dia e o mês selecionado mostrar o plano definido para este dua

            string dataAtual = DateTime.Now.ToString("MM/dd/yyyy");
            string iMes = dataAtual.Substring(0, 2);
            string iDia = dataAtual.Substring(3, 2);
            string iANO = dataAtual.Substring(6, 4);

            int sData2 = Convert.ToInt32(iDia);
            int sData = Convert.ToInt32(iDia);
            int sAnoAnt = Convert.ToInt32(iANO);
            int sMesAnt = Convert.ToInt32(iMes);

            //Integer sData2 = Convert.ToInt32(iDia);

            if (text_Valor == "PL1")
            {
                // Mostra o plano da Data Atual
                PlanoPL1(iMes, iDia, iANO);
                PegarOracao();
            }

            BotaoPrint = (Button)FindViewById(Resource.Id.BtnPrint);
            BotaoAnterior = (Button)FindViewById(Resource.Id.BtnAnt);
            BotaoProximo = (Button)FindViewById(Resource.Id.BtnProx);
            BotaoVoltar = (Button)FindViewById(Resource.Id.btnSair);            

        } // Fim do OnCreate

    } //Fim do Activity Plano1

}  // Fim de tudo