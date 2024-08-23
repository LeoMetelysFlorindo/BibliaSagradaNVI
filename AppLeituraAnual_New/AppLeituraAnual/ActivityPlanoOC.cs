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
using Com.Google.Android.Exoplayer2;
using Com.Google.Android.Exoplayer2.Upstream;
using Com.Google.Android.Exoplayer2.Source;
using Com.Google.Android.Exoplayer2.Util;
using Com.Google.Android.Exoplayer2.Extractor;
using Com.Google.Android.Exoplayer2.Trackselection;

[assembly: Permission(Name = "android.permission.READ_EXTERNAL_STORAGE")]
[assembly: Permission(Name = "android.permission.WRITE_EXTERNAL_STORAGE")]
[assembly: Permission(Name = "android.permission.INTERNET")]
namespace AppBibliaNVI
{
    [Activity(Label = "ActivityPlanoOC", ScreenOrientation = ScreenOrientation.Portrait)]


    public class ActivityPlanoOC : Activity
    {
        [Export("btnSalvarClicked")]
        public void btnFecharClicked_Click(View v)
        {

            // Modificado 15/03/2021 11:28h
            //Finish();  // Fecha a Activity corrente

            // 07/06/2021 17:42h - Fechar de vez
            FinishAffinity();                       
        }

        public SimpleExoPlayer exoPlayer;                
        public Button BotaoProximo;
        public Button BotaoAnterior;
        public Button BotaoVoltar;
        public Button BotaoCapAnterior;
        public Button BotaoProximoCap;
        public Button BotaoTocar;
        public Button BotaoPausar;
        public Button BotaoParar;
        public Button BotaoReiniciar;

        Ringtone mRingtone;
        MediaPlayer player;

        string sValor = "";
        string sAudio = "";
        

        //Pegar os outros dados da lei e mostrar ao usuário                              
        string sTipoLei = "";
        string sNomeLivro = "";
        string TagName = "";
        string sCapitulo = "";
        string sNumeroVersiculo = "";
        string sTextoVersiculo = "";
        int PrimeirVersculo;
        int UltimoVersiculo;
        int PrimeiroVersiculoAnterior = 0; // Variável de controle entre páginas de versiculos 
        string text_Valor = "";
        int ContaCliques = 0;
        string sUltimoVersiculonaTela = "";
        bool sChegouNoUltimo = false;
        int SNumeroTotalVersiculos = 0;
        int sContaVersiculosvv = 0;
        string sTotalCapitulos = "";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Pega o valor atribuido no Configurar para definir que Livro está ativo - 30/12/2020 - 19:11h
            text_Valor = Intent.GetStringExtra("MyData");
            sValor = text_Valor;  // Tipo de Livro AT ou NT

            // Remove título desta Activity
            Window.RequestFeature(WindowFeatures.NoTitle);

            // Create your application here
            SetContentView(Resource.Layout.PlanoOC);

            // Receber dados do livro e Capitulo selecionados
            string snome = Intent.GetStringExtra("MyDataLivro");
            string sdesc = Intent.GetStringExtra("MyDataCapitulo");            
            sTotalCapitulos = Intent.GetStringExtra("MyDataTotCapitulos"); // Total de capítulos do livro selecionado

            string sFezPesquisa = Intent.GetStringExtra("MyDataUnicVersiculo");

            var TxtNomeLivro = FindViewById<TextView>(Resource.Id.TxtMES);
            TxtNomeLivro.SetTextColor(Android.Graphics.Color.Yellow);

            // Repassar o valor
            TxtNomeLivro.Text = snome;

            var TxtCapitulo = FindViewById<TextView>(Resource.Id.TxtDIA);
            TxtCapitulo.SetTextColor(Android.Graphics.Color.Gold);

            // Repassar o valor
            TxtCapitulo.Text = sdesc;


            var txtNumeroVersiculo1 = FindViewById<TextView>(Resource.Id.TxtNumero1);
            var TxtVersiculo1 = FindViewById<TextView>(Resource.Id.TxtLeitura1);
            var txtNumeroVersiculo2 = FindViewById<TextView>(Resource.Id.TxtNumero2);
            var TxtVersiculo2 = FindViewById<TextView>(Resource.Id.txtLeitura2);
            var txtNumeroVersiculo3 = FindViewById<TextView>(Resource.Id.TxtNumero3);
            var TxtVersiculo3 = FindViewById<TextView>(Resource.Id.TxtOracao);

            // 15/04/2021 11:51h
            // FEZ PESQUISA E TEM TODOS OS DADOS, SÓ APRESENTAR AO USUÁRIO
            if (sFezPesquisa == "1")             
            {

                txtNumeroVersiculo1.Text = Intent.GetStringExtra("MyDataVersiculoBiblia");
                TxtVersiculo1.Text = Intent.GetStringExtra("MyDataTextoVersiculo");
                PrimeirVersculo = Int32.Parse(txtNumeroVersiculo1.Text);
                PrimeiroVersiculoAnterior = PrimeirVersculo; // registra o primeiro versiculo da tela atual

                txtNumeroVersiculo2.Text = "";
                TxtVersiculo2.Text = "";

                txtNumeroVersiculo3.Text = "";
                TxtVersiculo3.Text = "";



            }
            else
            {            
                // Ler XML da Bíblia NVI
                // 03/03/2021 16h            
                bool AchouLivro = false;
                bool AchouCapitulo = false;

                XmlReader xReader = XmlReader.Create(Assets.Open("nvi.xml"));
                XmlReader xmlReader = XmlReader.Create(Assets.Open("nvi.xml"));

                var bookFound = false;
                var capFound = false;
                int sVersos = 0;

                // 18/03/2021 - função criada para obter o número total de versiculos
                // do livro e capítulo selecionado
                while (xmlReader.Read())
                {
                        if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.Name == "book")
                        {
                            var sLivro = xmlReader.GetAttribute(0);
                            bookFound = sLivro == snome;
                        }

                        if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.Name == "c")
                        {
                            var sCapitulo = xmlReader.GetAttribute(0);
                            capFound = sCapitulo == sdesc;
                        }

                        if (bookFound && capFound && xmlReader.NodeType == XmlNodeType.Element && xmlReader.Name == "v")
                        {
                            sVersos++;
                        
                        }
                    
                    }

                    SNumeroTotalVersiculos = sVersos; // Guarda o número total de versiculos do capítulo selecionado

                    while (xReader.Read())
                    {                                                          

                        switch (xReader.NodeType)
                        {
                                            
                            case XmlNodeType.Element:

                                // Lê a tag inicial
                                TagName = xReader.Name;

                                // se for tipoLei marca sLeiLazer como verdadeiro
                                if (xReader.Name == "book")
                                {
                                    AchouLivro = true;
                                    sTipoLei = xReader.GetAttribute(0); //Nome do Livro
                                    if (snome == sTipoLei)
                                    {
                                        AchouLivro = true;
                                        sNomeLivro = sTipoLei;
                                    }
                                    else
                                    {
                                        AchouLivro = false;
                                        sNomeLivro = "";

                                    }
                                }

                                if (xReader.Name == "c")
                                {
                                    AchouCapitulo = true;
                                    sCapitulo = xReader.GetAttribute(0);  // pegar o capítulo
                                    if (sCapitulo == sdesc)
                                    {
                                        AchouCapitulo = true;
                                    }
                                    else
                                    {
                                        AchouCapitulo = false;
                                        sCapitulo = "";
                                    }

                                }

                                if (TagName == "v")
                                {
                                    sNumeroVersiculo = xReader.GetAttribute(0);  // pegar o número do versiculo                               

                                }


                            break;

                        
                            case XmlNodeType.Text:
                                                   

                                // Pega o capitulo do livro
                                if ((AchouLivro == true) && (AchouCapitulo == true) && (TagName == "v")) 
                                {
                                    sTextoVersiculo = xReader.Value;  // ler o texto do versiculo
                                    if (sNumeroVersiculo == "1")
                                    {
                                        txtNumeroVersiculo1.Text = sNumeroVersiculo.ToString();   
                                        TxtVersiculo1.Text =  sTextoVersiculo;
                                        PrimeirVersculo = Int32.Parse(sNumeroVersiculo);
                                        PrimeiroVersiculoAnterior = PrimeirVersculo; // registra o primeiro versiculo da tela atual
                                        sContaVersiculosvv++;
                                    }

                                    if (sNumeroVersiculo == "2")
                                    {
                                         txtNumeroVersiculo2.Text = sNumeroVersiculo.ToString();
                                         TxtVersiculo2.Text = sTextoVersiculo;
                                         sContaVersiculosvv++;
                                    }

                                    if (sNumeroVersiculo == "3")
                                    {
                                        txtNumeroVersiculo3.Text = sNumeroVersiculo.ToString();
                                        TxtVersiculo3.Text = sTextoVersiculo;
                                        UltimoVersiculo = Int32.Parse(sNumeroVersiculo);
                                        AchouLivro = false;
                                        AchouCapitulo = false;
                                        sContaVersiculosvv++;
                                    }
                                }
                                                      
                            
                                break;


                        } // fim so swhitch 

                } // fim do loop do total de versiculos

            }

            // DEFINE A COR DOS LIVROS DO PENTATEUCO
            if ((snome == "Gênesis") || (snome == "Êxodo") || (snome == "Levítico") || (snome == "Números") || (snome == "Deuteronômio"))
            {

                TxtNomeLivro.SetTextColor(Android.Graphics.Color.Gold);
                TxtCapitulo.SetTextColor(Android.Graphics.Color.White);

                txtNumeroVersiculo1.SetTextColor(Android.Graphics.Color.Gold);
                TxtVersiculo1.SetTextColor(Android.Graphics.Color.White);
                txtNumeroVersiculo2.SetTextColor(Android.Graphics.Color.Gold);
                TxtVersiculo2.SetTextColor(Android.Graphics.Color.White);
                txtNumeroVersiculo3.SetTextColor(Android.Graphics.Color.Gold);
                TxtVersiculo3.SetTextColor(Android.Graphics.Color.White);
            }

            // DEFINE A COR DOS LIVROS HISTÓRICOS
            if ((snome == "Josué") || (snome == "Rute") || (snome == "1º Samuel") || (snome == "2º Samuel") || (snome == "1º Reis"))
            {
                TxtNomeLivro.SetTextColor(Android.Graphics.Color.White);
                TxtCapitulo.SetTextColor(Android.Graphics.Color.Blue);

                txtNumeroVersiculo1.SetTextColor(Android.Graphics.Color.White);
                TxtVersiculo1.SetTextColor(Android.Graphics.Color.Blue);
                txtNumeroVersiculo2.SetTextColor(Android.Graphics.Color.White);
                TxtVersiculo2.SetTextColor(Android.Graphics.Color.Blue);
                txtNumeroVersiculo3.SetTextColor(Android.Graphics.Color.White);
                TxtVersiculo3.SetTextColor(Android.Graphics.Color.Blue);
            }

            if ((snome == "2º Reis") || (snome == "1º Crônicas") || (snome == "2º Crônicas") || (snome == "Esdras") || (snome == "Neemias") || (snome == "Ester"))
            {
                TxtNomeLivro.SetTextColor(Android.Graphics.Color.Blue);
                TxtCapitulo.SetTextColor(Android.Graphics.Color.White);

                txtNumeroVersiculo1.SetTextColor(Android.Graphics.Color.White);
                TxtVersiculo1.SetTextColor(Android.Graphics.Color.Blue);
                txtNumeroVersiculo2.SetTextColor(Android.Graphics.Color.White);
                TxtVersiculo2.SetTextColor(Android.Graphics.Color.Blue);
                txtNumeroVersiculo3.SetTextColor(Android.Graphics.Color.White);
                TxtVersiculo3.SetTextColor(Android.Graphics.Color.Blue);
            }


            // DEFINE A COR DOS LIVROS POÉTICOS
            if ((snome == "Jó") || (snome == "Salmos") || (snome == "Provérbios") || (snome == "Eclesiastes") || (snome == "Cântico doas Cânticos") || (snome == "Lamentações"))
            {
                TxtNomeLivro.SetTextColor(Android.Graphics.Color.Yellow);
                TxtCapitulo.SetTextColor(Android.Graphics.Color.LightBlue);


                txtNumeroVersiculo1.SetTextColor(Android.Graphics.Color.LightBlue);
                TxtVersiculo1.SetTextColor(Android.Graphics.Color.Yellow);
                txtNumeroVersiculo2.SetTextColor(Android.Graphics.Color.LightBlue);
                TxtVersiculo2.SetTextColor(Android.Graphics.Color.Yellow);
                txtNumeroVersiculo3.SetTextColor(Android.Graphics.Color.LightBlue);
                TxtVersiculo3.SetTextColor(Android.Graphics.Color.Yellow);
            }

            // DEFINE A COR DOS LIVROS DOS PROFETAS MAIORES
            if ((snome == "Isaías") || (snome == "Jeremias") || (snome == "Daniel") || (snome == "Ezequiel") || (snome == "Oséias") || (snome == "Zacarias"))
            {
                TxtNomeLivro.SetTextColor(Android.Graphics.Color.Green);
                TxtCapitulo.SetTextColor(Android.Graphics.Color.Yellow);

                txtNumeroVersiculo1.SetTextColor(Android.Graphics.Color.Yellow);
                TxtVersiculo1.SetTextColor(Android.Graphics.Color.Green);
                txtNumeroVersiculo2.SetTextColor(Android.Graphics.Color.Yellow);
                TxtVersiculo2.SetTextColor(Android.Graphics.Color.Green);
                txtNumeroVersiculo3.SetTextColor(Android.Graphics.Color.Yellow);
                TxtVersiculo3.SetTextColor(Android.Graphics.Color.Green);
            }


            // DEFINE A COR DOS LIVROS DOS PROFETAS MENORES
            if ((snome == "Joel") || (snome == "Amós") || (snome == "Obadias") || (snome == "Jonas") || (snome == "Miquéias") || (snome == "Naum"))
            {
                TxtNomeLivro.SetTextColor(Android.Graphics.Color.Red);
                TxtCapitulo.SetTextColor(Android.Graphics.Color.Green);

                txtNumeroVersiculo1.SetTextColor(Android.Graphics.Color.Green);
                TxtVersiculo1.SetTextColor(Android.Graphics.Color.Red);
                txtNumeroVersiculo2.SetTextColor(Android.Graphics.Color.Green);
                TxtVersiculo2.SetTextColor(Android.Graphics.Color.Red);
                txtNumeroVersiculo3.SetTextColor(Android.Graphics.Color.Green);
                TxtVersiculo3.SetTextColor(Android.Graphics.Color.Red);
            }

            // DEFINE A COR DOS LIVROS DOS PROFETAS MENORES
            if ((snome == "Habacuque") || (snome == "Sofonias") || (snome == "Ageu") || (snome == "Malaquias"))
            {
                TxtNomeLivro.SetTextColor(Android.Graphics.Color.Red);
                TxtCapitulo.SetTextColor(Android.Graphics.Color.Green);

                txtNumeroVersiculo1.SetTextColor(Android.Graphics.Color.Green);
                TxtVersiculo1.SetTextColor(Android.Graphics.Color.Red);
                txtNumeroVersiculo2.SetTextColor(Android.Graphics.Color.Green);
                TxtVersiculo2.SetTextColor(Android.Graphics.Color.Red);
                txtNumeroVersiculo3.SetTextColor(Android.Graphics.Color.Green);
                TxtVersiculo3.SetTextColor(Android.Graphics.Color.Red);
            }

            // DEFINE A COR DOS LIVROS DO EVANGELHO
            if ((snome == "Mateus") || (snome == "Marcos") || (snome == "Lucas") || (snome == "João"))
            {
                TxtNomeLivro.SetTextColor(Android.Graphics.Color.Gold);
                TxtCapitulo.SetTextColor(Android.Graphics.Color.White);

                txtNumeroVersiculo1.SetTextColor(Android.Graphics.Color.White);
                TxtVersiculo1.SetTextColor(Android.Graphics.Color.Gold);
                txtNumeroVersiculo2.SetTextColor(Android.Graphics.Color.White);
                TxtVersiculo2.SetTextColor(Android.Graphics.Color.Gold);
                txtNumeroVersiculo3.SetTextColor(Android.Graphics.Color.White);
                TxtVersiculo3.SetTextColor(Android.Graphics.Color.Gold);
            }


            // DEFINE A COR DO LIVRO DE ATOS
            if ((snome == "Atos"))
            {
                TxtNomeLivro.SetTextColor(Android.Graphics.Color.DarkKhaki);
                TxtCapitulo.SetTextColor(Android.Graphics.Color.Black);

                txtNumeroVersiculo1.SetTextColor(Android.Graphics.Color.Black);
                TxtVersiculo1.SetTextColor(Android.Graphics.Color.DarkKhaki);
                txtNumeroVersiculo2.SetTextColor(Android.Graphics.Color.Black);
                TxtVersiculo2.SetTextColor(Android.Graphics.Color.DarkKhaki);
                txtNumeroVersiculo3.SetTextColor(Android.Graphics.Color.Black);
                TxtVersiculo3.SetTextColor(Android.Graphics.Color.DarkKhaki);
            }

            // DEFINE A COR DO LIVRO DE PAULO
            if ((snome == "Romanos") || (snome == "1ª Coríntios") || (snome == "2ª Coríntios") || (snome == "Gálatas"))
            {
                TxtNomeLivro.SetTextColor(Android.Graphics.Color.Yellow);
                TxtCapitulo.SetTextColor(Android.Graphics.Color.Blue);

                txtNumeroVersiculo1.SetTextColor(Android.Graphics.Color.Blue);
                TxtVersiculo1.SetTextColor(Android.Graphics.Color.Yellow);
                txtNumeroVersiculo2.SetTextColor(Android.Graphics.Color.Blue);
                TxtVersiculo2.SetTextColor(Android.Graphics.Color.Yellow);
                txtNumeroVersiculo3.SetTextColor(Android.Graphics.Color.Blue);
                TxtVersiculo3.SetTextColor(Android.Graphics.Color.Yellow);
            }

            if ((snome == "Efésios") || (snome == "Filipenses") || (snome == "Colossenses") || (snome == "1ª Tessalonicenses"))
            {
                TxtNomeLivro.SetTextColor(Android.Graphics.Color.Yellow);
                TxtCapitulo.SetTextColor(Android.Graphics.Color.Blue);

                txtNumeroVersiculo1.SetTextColor(Android.Graphics.Color.Blue);
                TxtVersiculo1.SetTextColor(Android.Graphics.Color.Yellow);
                txtNumeroVersiculo2.SetTextColor(Android.Graphics.Color.Blue);
                TxtVersiculo2.SetTextColor(Android.Graphics.Color.Yellow);
                txtNumeroVersiculo3.SetTextColor(Android.Graphics.Color.Blue);
                TxtVersiculo3.SetTextColor(Android.Graphics.Color.Yellow);
            }

            if ((snome == "2ª Tessalonicenses") || (snome == "1ª Timóteo") || (snome == "2ª Timóteo") || (snome == "Tito") || (snome == "Filemom"))
            {
                TxtNomeLivro.SetTextColor(Android.Graphics.Color.Yellow);
                TxtCapitulo.SetTextColor(Android.Graphics.Color.Blue);

                txtNumeroVersiculo1.SetTextColor(Android.Graphics.Color.Blue);
                TxtVersiculo1.SetTextColor(Android.Graphics.Color.Yellow);
                txtNumeroVersiculo2.SetTextColor(Android.Graphics.Color.Blue);
                TxtVersiculo2.SetTextColor(Android.Graphics.Color.Yellow);
                txtNumeroVersiculo3.SetTextColor(Android.Graphics.Color.Blue);
                TxtVersiculo3.SetTextColor(Android.Graphics.Color.Yellow);
            }

            // DEFINE A COR DOS LIVROS DE JOÃO
            if ((snome == "1ª João") || (snome == "2ª João") || (snome == "3ª João") || (snome == "Revelações"))
            {
                TxtNomeLivro.SetTextColor(Android.Graphics.Color.Pink);
                TxtCapitulo.SetTextColor(Android.Graphics.Color.Navy);

                txtNumeroVersiculo1.SetTextColor(Android.Graphics.Color.Navy);
                TxtVersiculo1.SetTextColor(Android.Graphics.Color.Pink);
                txtNumeroVersiculo2.SetTextColor(Android.Graphics.Color.Navy);
                TxtVersiculo2.SetTextColor(Android.Graphics.Color.Pink);
                txtNumeroVersiculo3.SetTextColor(Android.Graphics.Color.Navy);
                TxtVersiculo3.SetTextColor(Android.Graphics.Color.Pink);
            }

            // DEFINE A COR DOS LIVROS DIVERSOS
            if ((snome == "Hebreus") || (snome == "Tiago") || (snome == "1ª Pedro") || (snome == "2ª Pedro") || (snome == "Judas"))
            {
                TxtNomeLivro.SetTextColor(Android.Graphics.Color.Salmon); 
                TxtCapitulo.SetTextColor(Android.Graphics.Color.Silver);

                txtNumeroVersiculo1.SetTextColor(Android.Graphics.Color.Silver);
                TxtVersiculo1.SetTextColor(Android.Graphics.Color.Salmon);
                txtNumeroVersiculo2.SetTextColor(Android.Graphics.Color.Silver);
                TxtVersiculo2.SetTextColor(Android.Graphics.Color.Salmon);
                txtNumeroVersiculo3.SetTextColor(Android.Graphics.Color.Silver);
                TxtVersiculo3.SetTextColor(Android.Graphics.Color.Salmon);
            }
                       

            BotaoAnterior = (Button)FindViewById(Resource.Id.BtnAnt);
            BotaoProximo = (Button)FindViewById(Resource.Id.BtnProx);
            BotaoVoltar = (Button)FindViewById(Resource.Id.btnSair);
            BotaoCapAnterior = (Button)FindViewById(Resource.Id.BtnCapAnt);
            BotaoProximoCap = (Button)FindViewById(Resource.Id.BtnProxCap);
            BotaoTocar = (Button)FindViewById(Resource.Id.btnTocar);
            BotaoParar = (Button)FindViewById(Resource.Id.btnParar);            

            // SE O LIVRO FOR O DE GÊNESIS 14/04/2021 18:03h
            // Ativar os botões para tocar o áudio do livro
            if (snome == "Gênesis")
            {
                BotaoTocar.Visibility = ViewStates.Visible;
                BotaoParar.Visibility = ViewStates.Visible;                
            }
            else
            {
                BotaoTocar.Visibility = ViewStates.Invisible;
                BotaoParar.Visibility = ViewStates.Invisible;            
            }

            BotaoTocar.Click += Tocar_Click;
            BotaoParar.Click += Parar_Click;            


            // 21/03/2021 15:26h Botao Dia Anterior pressionado
            BotaoCapAnterior.Click += (sender, e) =>
            {                
                sChegouNoUltimo = false;
                // Torna invsível se está na ultima página e retorna a anterior
                BotaoProximoCap.Visibility = ViewStates.Invisible;
                BotaoCapAnterior.Visibility = ViewStates.Invisible;

                int scapituloAnterior = 0;
                
                // Ler o cap[itulo atual e subtrair de um
                scapituloAnterior = Int32.Parse(sdesc);
                scapituloAnterior = scapituloAnterior - 1;

                // repassa o valor de volta ao sdec para assumir este capítulo
                sdesc = scapituloAnterior.ToString();
                
                TxtCapitulo.Text = sdesc; 
                
                // Ler XML da Bíblia NVI
                // 03/03/2021 16h
                bool AchouLivro = false;
                bool AchouCapitulo = false;


                XmlReader xReader = XmlReader.Create(Assets.Open("nvi.xml"));
                XmlReader xmlReader = XmlReader.Create(Assets.Open("nvi.xml"));

                var bookFound = false;
                var capFound = false;
                int sVersos = 0;

                // 18/03/2021 - função criada para obter o número total de versiculos
                // do livro e capítulo selecionado
                while (xmlReader.Read())
                {
                    if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.Name == "book")
                    {
                        var sLivro = xmlReader.GetAttribute(0);
                        bookFound = sLivro == snome;
                    }

                    if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.Name == "c")
                    {
                        var sCapitulo = xmlReader.GetAttribute(0);
                        capFound = sCapitulo == sdesc;
                    }

                    if (bookFound && capFound && xmlReader.NodeType == XmlNodeType.Element && xmlReader.Name == "v")
                    {
                        sVersos++;

                    }

                }

                SNumeroTotalVersiculos = sVersos; // Guarda o número total de versiculos do capítulo selecionado

                while (xReader.Read())
                {

                    switch (xReader.NodeType)
                    {

                        case XmlNodeType.Element:

                            // Lê a tag inicial
                            TagName = xReader.Name;

                            // se for tipoLei marca sLeiLazer como verdadeiro
                            if (xReader.Name == "book")
                            {
                                AchouLivro = true;
                                sTipoLei = xReader.GetAttribute(0); //Nome do Livro
                                if (snome == sTipoLei)
                                {
                                    AchouLivro = true;
                                    sNomeLivro = sTipoLei;
                                }
                                else
                                {
                                    AchouLivro = false;
                                    sNomeLivro = "";

                                }
                            }

                            if (xReader.Name == "c")
                            {
                                AchouCapitulo = true;
                                sCapitulo = xReader.GetAttribute(0);  // pegar o capítulo
                                if (sCapitulo == sdesc)
                                {
                                    AchouCapitulo = true;
                                }
                                else
                                {
                                    AchouCapitulo = false;
                                    sCapitulo = "";
                                }

                            }

                            if (TagName == "v")
                            {
                                sNumeroVersiculo = xReader.GetAttribute(0);  // pegar o número do versiculo                               

                            }


                            break;


                        case XmlNodeType.Text:


                            // Pega o capitulo do livro
                            if ((AchouLivro == true) && (AchouCapitulo == true) && (TagName == "v"))
                            {
                                sTextoVersiculo = xReader.Value;  // ler o texto do versiculo
                                if (sNumeroVersiculo == "1")
                                {
                                    txtNumeroVersiculo1.Text = sNumeroVersiculo.ToString();
                                    TxtVersiculo1.Text = sTextoVersiculo;
                                    PrimeirVersculo = Int32.Parse(sNumeroVersiculo);
                                    PrimeiroVersiculoAnterior = PrimeirVersculo; // registra o primeiro versiculo da tela atual
                                    sContaVersiculosvv++;
                                }

                                if (sNumeroVersiculo == "2")
                                {
                                    txtNumeroVersiculo2.Text = sNumeroVersiculo.ToString();
                                    TxtVersiculo2.Text = sTextoVersiculo;
                                    sContaVersiculosvv++;
                                }

                                if (sNumeroVersiculo == "3")
                                {
                                    txtNumeroVersiculo3.Text = sNumeroVersiculo.ToString();
                                    TxtVersiculo3.Text = sTextoVersiculo;
                                    UltimoVersiculo = Int32.Parse(sNumeroVersiculo);
                                    AchouLivro = false;
                                    AchouCapitulo = false;
                                    sContaVersiculosvv++;
                                }
                            }


                            break;


                    } // fim so swhitch 

                } // FIM DO LOOP QUE CARREGA OS TRÊS PRIMEIROS VERSÍCULOS

                // DEFINE A COR DOS LIVROS DO PENTATEUCO
                if ((snome == "Gênesis") || (snome == "Êxodo") || (snome == "Levítico") || (snome == "Números") || (snome == "Deuteronômio"))
                {

                    TxtNomeLivro.SetTextColor(Android.Graphics.Color.Gold);
                    TxtCapitulo.SetTextColor(Android.Graphics.Color.White);

                    txtNumeroVersiculo1.SetTextColor(Android.Graphics.Color.Gold);
                    TxtVersiculo1.SetTextColor(Android.Graphics.Color.White);
                    txtNumeroVersiculo2.SetTextColor(Android.Graphics.Color.Gold);
                    TxtVersiculo2.SetTextColor(Android.Graphics.Color.White);
                    txtNumeroVersiculo3.SetTextColor(Android.Graphics.Color.Gold);
                    TxtVersiculo3.SetTextColor(Android.Graphics.Color.White);
                }

                // DEFINE A COR DOS LIVROS HISTÓRICOS
                if ((snome == "Josué") || (snome == "Rute") || (snome == "1º Samuel") || (snome == "2º Samuel") || (snome == "1º Reis"))
                {
                    TxtNomeLivro.SetTextColor(Android.Graphics.Color.White);
                    TxtCapitulo.SetTextColor(Android.Graphics.Color.Blue);

                    txtNumeroVersiculo1.SetTextColor(Android.Graphics.Color.White);
                    TxtVersiculo1.SetTextColor(Android.Graphics.Color.Blue);
                    txtNumeroVersiculo2.SetTextColor(Android.Graphics.Color.White);
                    TxtVersiculo2.SetTextColor(Android.Graphics.Color.Blue);
                    txtNumeroVersiculo3.SetTextColor(Android.Graphics.Color.White);
                    TxtVersiculo3.SetTextColor(Android.Graphics.Color.Blue);
                }

                if ((snome == "2º Reis") || (snome == "1º Crônicas") || (snome == "2º Crônicas") || (snome == "Esdras") || (snome == "Neemias") || (snome == "Ester"))
                {
                    TxtNomeLivro.SetTextColor(Android.Graphics.Color.Blue);
                    TxtCapitulo.SetTextColor(Android.Graphics.Color.White);

                    txtNumeroVersiculo1.SetTextColor(Android.Graphics.Color.White);
                    TxtVersiculo1.SetTextColor(Android.Graphics.Color.Blue);
                    txtNumeroVersiculo2.SetTextColor(Android.Graphics.Color.White);
                    TxtVersiculo2.SetTextColor(Android.Graphics.Color.Blue);
                    txtNumeroVersiculo3.SetTextColor(Android.Graphics.Color.White);
                    TxtVersiculo3.SetTextColor(Android.Graphics.Color.Blue);
                }


                // DEFINE A COR DOS LIVROS POÉTICOS
                if ((snome == "Jó") || (snome == "Salmos") || (snome == "Provérbios") || (snome == "Eclesiastes") || (snome == "Cântico doas Cânticos") || (snome == "Lamentações"))
                {
                    TxtNomeLivro.SetTextColor(Android.Graphics.Color.Yellow);
                    TxtCapitulo.SetTextColor(Android.Graphics.Color.LightBlue);


                    txtNumeroVersiculo1.SetTextColor(Android.Graphics.Color.LightBlue);
                    TxtVersiculo1.SetTextColor(Android.Graphics.Color.Yellow);
                    txtNumeroVersiculo2.SetTextColor(Android.Graphics.Color.LightBlue);
                    TxtVersiculo2.SetTextColor(Android.Graphics.Color.Yellow);
                    txtNumeroVersiculo3.SetTextColor(Android.Graphics.Color.LightBlue);
                    TxtVersiculo3.SetTextColor(Android.Graphics.Color.Yellow);
                }

                // DEFINE A COR DOS LIVROS DOS PROFETAS MAIORES
                if ((snome == "Isaías") || (snome == "Jeremias") || (snome == "Daniel") || (snome == "Ezequiel") || (snome == "Oséias") || (snome == "Zacarias"))
                {
                    TxtNomeLivro.SetTextColor(Android.Graphics.Color.Green);
                    TxtCapitulo.SetTextColor(Android.Graphics.Color.Yellow);

                    txtNumeroVersiculo1.SetTextColor(Android.Graphics.Color.Yellow);
                    TxtVersiculo1.SetTextColor(Android.Graphics.Color.Green);
                    txtNumeroVersiculo2.SetTextColor(Android.Graphics.Color.Yellow);
                    TxtVersiculo2.SetTextColor(Android.Graphics.Color.Green);
                    txtNumeroVersiculo3.SetTextColor(Android.Graphics.Color.Yellow);
                    TxtVersiculo3.SetTextColor(Android.Graphics.Color.Green);
                }


                // DEFINE A COR DOS LIVROS DOS PROFETAS MENORES
                if ((snome == "Joel") || (snome == "Amós") || (snome == "Obadias") || (snome == "Jonas") || (snome == "Miquéias") || (snome == "Naum"))
                {
                    TxtNomeLivro.SetTextColor(Android.Graphics.Color.Red);
                    TxtCapitulo.SetTextColor(Android.Graphics.Color.Green);

                    txtNumeroVersiculo1.SetTextColor(Android.Graphics.Color.Green);
                    TxtVersiculo1.SetTextColor(Android.Graphics.Color.Red);
                    txtNumeroVersiculo2.SetTextColor(Android.Graphics.Color.Green);
                    TxtVersiculo2.SetTextColor(Android.Graphics.Color.Red);
                    txtNumeroVersiculo3.SetTextColor(Android.Graphics.Color.Green);
                    TxtVersiculo3.SetTextColor(Android.Graphics.Color.Red);
                }

                // DEFINE A COR DOS LIVROS DOS PROFETAS MENORES
                if ((snome == "Habacuque") || (snome == "Sofonias") || (snome == "Ageu") || (snome == "Malaquias"))
                {
                    TxtNomeLivro.SetTextColor(Android.Graphics.Color.Red);
                    TxtCapitulo.SetTextColor(Android.Graphics.Color.Green);

                    txtNumeroVersiculo1.SetTextColor(Android.Graphics.Color.Green);
                    TxtVersiculo1.SetTextColor(Android.Graphics.Color.Red);
                    txtNumeroVersiculo2.SetTextColor(Android.Graphics.Color.Green);
                    TxtVersiculo2.SetTextColor(Android.Graphics.Color.Red);
                    txtNumeroVersiculo3.SetTextColor(Android.Graphics.Color.Green);
                    TxtVersiculo3.SetTextColor(Android.Graphics.Color.Red);
                }

                // DEFINE A COR DOS LIVROS DO EVANGELHO
                if ((snome == "Mateus") || (snome == "Marcos") || (snome == "Lucas") || (snome == "João"))
                {
                    TxtNomeLivro.SetTextColor(Android.Graphics.Color.Gold);
                    TxtCapitulo.SetTextColor(Android.Graphics.Color.White);

                    txtNumeroVersiculo1.SetTextColor(Android.Graphics.Color.White);
                    TxtVersiculo1.SetTextColor(Android.Graphics.Color.Gold);
                    txtNumeroVersiculo2.SetTextColor(Android.Graphics.Color.White);
                    TxtVersiculo2.SetTextColor(Android.Graphics.Color.Gold);
                    txtNumeroVersiculo3.SetTextColor(Android.Graphics.Color.White);
                    TxtVersiculo3.SetTextColor(Android.Graphics.Color.Gold);
                }


                // DEFINE A COR DO LIVRO DE ATOS
                if ((snome == "Atos"))
                {
                    TxtNomeLivro.SetTextColor(Android.Graphics.Color.DarkKhaki);
                    TxtCapitulo.SetTextColor(Android.Graphics.Color.Black);

                    txtNumeroVersiculo1.SetTextColor(Android.Graphics.Color.Black);
                    TxtVersiculo1.SetTextColor(Android.Graphics.Color.DarkKhaki);
                    txtNumeroVersiculo2.SetTextColor(Android.Graphics.Color.Black);
                    TxtVersiculo2.SetTextColor(Android.Graphics.Color.DarkKhaki);
                    txtNumeroVersiculo3.SetTextColor(Android.Graphics.Color.Black);
                    TxtVersiculo3.SetTextColor(Android.Graphics.Color.DarkKhaki);
                }

                // DEFINE A COR DO LIVRO DE PAULO
                if ((snome == "Romanos") || (snome == "1ª Coríntios") || (snome == "2ª Coríntios") || (snome == "Gálatas"))
                {
                    TxtNomeLivro.SetTextColor(Android.Graphics.Color.Yellow);
                    TxtCapitulo.SetTextColor(Android.Graphics.Color.Blue);

                    txtNumeroVersiculo1.SetTextColor(Android.Graphics.Color.Blue);
                    TxtVersiculo1.SetTextColor(Android.Graphics.Color.Yellow);
                    txtNumeroVersiculo2.SetTextColor(Android.Graphics.Color.Blue);
                    TxtVersiculo2.SetTextColor(Android.Graphics.Color.Yellow);
                    txtNumeroVersiculo3.SetTextColor(Android.Graphics.Color.Blue);
                    TxtVersiculo3.SetTextColor(Android.Graphics.Color.Yellow);
                }

                if ((snome == "Efésios") || (snome == "Filipenses") || (snome == "Colossenses") || (snome == "1ª Tessalonicenses"))
                {
                    TxtNomeLivro.SetTextColor(Android.Graphics.Color.Yellow);
                    TxtCapitulo.SetTextColor(Android.Graphics.Color.Blue);

                    txtNumeroVersiculo1.SetTextColor(Android.Graphics.Color.Blue);
                    TxtVersiculo1.SetTextColor(Android.Graphics.Color.Yellow);
                    txtNumeroVersiculo2.SetTextColor(Android.Graphics.Color.Blue);
                    TxtVersiculo2.SetTextColor(Android.Graphics.Color.Yellow);
                    txtNumeroVersiculo3.SetTextColor(Android.Graphics.Color.Blue);
                    TxtVersiculo3.SetTextColor(Android.Graphics.Color.Yellow);
                }

                if ((snome == "2ª Tessalonicenses") || (snome == "1ª Timóteo") || (snome == "2ª Timóteo") || (snome == "Tito") || (snome == "Filemom"))
                {
                    TxtNomeLivro.SetTextColor(Android.Graphics.Color.Yellow);
                    TxtCapitulo.SetTextColor(Android.Graphics.Color.Blue);

                    txtNumeroVersiculo1.SetTextColor(Android.Graphics.Color.Blue);
                    TxtVersiculo1.SetTextColor(Android.Graphics.Color.Yellow);
                    txtNumeroVersiculo2.SetTextColor(Android.Graphics.Color.Blue);
                    TxtVersiculo2.SetTextColor(Android.Graphics.Color.Yellow);
                    txtNumeroVersiculo3.SetTextColor(Android.Graphics.Color.Blue);
                    TxtVersiculo3.SetTextColor(Android.Graphics.Color.Yellow);
                }

                // DEFINE A COR DOS LIVROS DE JOÃO
                if ((snome == "1ª João") || (snome == "2ª João") || (snome == "3ª João") || (snome == "Revelações"))
                {
                    TxtNomeLivro.SetTextColor(Android.Graphics.Color.Pink);
                    TxtCapitulo.SetTextColor(Android.Graphics.Color.Navy);

                    txtNumeroVersiculo1.SetTextColor(Android.Graphics.Color.Navy);
                    TxtVersiculo1.SetTextColor(Android.Graphics.Color.Pink);
                    txtNumeroVersiculo2.SetTextColor(Android.Graphics.Color.Navy);
                    TxtVersiculo2.SetTextColor(Android.Graphics.Color.Pink);
                    txtNumeroVersiculo3.SetTextColor(Android.Graphics.Color.Navy);
                    TxtVersiculo3.SetTextColor(Android.Graphics.Color.Pink);
                }

                // DEFINE A COR DOS LIVROS DIVERSOS
                if ((snome == "Hebreus") || (snome == "Tiago") || (snome == "1ª Pedro") || (snome == "2ª Pedro") || (snome == "Judas"))
                {
                    TxtNomeLivro.SetTextColor(Android.Graphics.Color.Salmon);
                    TxtCapitulo.SetTextColor(Android.Graphics.Color.Silver);

                    txtNumeroVersiculo1.SetTextColor(Android.Graphics.Color.Silver);
                    TxtVersiculo1.SetTextColor(Android.Graphics.Color.Salmon);
                    txtNumeroVersiculo2.SetTextColor(Android.Graphics.Color.Silver);
                    TxtVersiculo2.SetTextColor(Android.Graphics.Color.Salmon);
                    txtNumeroVersiculo3.SetTextColor(Android.Graphics.Color.Silver);
                    TxtVersiculo3.SetTextColor(Android.Graphics.Color.Salmon);
                }


            }; // FIM DO BOTÃO CAPÍTULO ANTERIOR


            // 21/03/2021 15:33h BOTÃO PRÓXIMO CAPÍTULO
            BotaoProximoCap.Click += (sender, e) =>
            {
                // Torna invsível se está na ultima página e retorna a anterior
                BotaoProximoCap.Visibility = ViewStates.Invisible;
                sChegouNoUltimo = false;

                int sProximoCapitulo = 0;

                // Ler o cap[itulo atual e subtrair de um
                sProximoCapitulo = Int32.Parse(sdesc);
                sProximoCapitulo = sProximoCapitulo + 1;

                // repassa o valor de volta ao sdec para assumir este capítulo
                sdesc = sProximoCapitulo.ToString();
                TxtCapitulo.Text = sdesc;

                // Ler XML da Bíblia NVI
                // 03/03/2021 16h
                bool AchouLivro = false;
                bool AchouCapitulo = false;


                XmlReader xReader = XmlReader.Create(Assets.Open("nvi.xml"));
                XmlReader xmlReader = XmlReader.Create(Assets.Open("nvi.xml"));

                var bookFound = false;
                var capFound = false;
                int sVersos = 0;

                // 18/03/2021 - função criada para obter o número total de versiculos
                // do livro e capítulo selecionado
                while (xmlReader.Read())
                {
                    if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.Name == "book")
                    {
                        var sLivro = xmlReader.GetAttribute(0);
                        bookFound = sLivro == snome;
                    }

                    if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.Name == "c")
                    {
                        var sCapitulo = xmlReader.GetAttribute(0);
                        capFound = sCapitulo == sdesc;
                    }

                    if (bookFound && capFound && xmlReader.NodeType == XmlNodeType.Element && xmlReader.Name == "v")
                    {
                        sVersos++;

                    }

                }

                SNumeroTotalVersiculos = sVersos; // Guarda o número total de versiculos do capítulo selecionado

                while (xReader.Read())
                {

                    switch (xReader.NodeType)
                    {

                        case XmlNodeType.Element:

                            // Lê a tag inicial
                            TagName = xReader.Name;

                            // se for tipoLei marca sLeiLazer como verdadeiro
                            if (xReader.Name == "book")
                            {
                                AchouLivro = true;
                                sTipoLei = xReader.GetAttribute(0); //Nome do Livro
                                if (snome == sTipoLei)
                                {
                                    AchouLivro = true;
                                    sNomeLivro = sTipoLei;
                                }
                                else
                                {
                                    AchouLivro = false;
                                    sNomeLivro = "";

                                }
                            }

                            if (xReader.Name == "c")
                            {
                                AchouCapitulo = true;
                                sCapitulo = xReader.GetAttribute(0);  // pegar o capítulo
                                if (sCapitulo == sdesc)
                                {
                                    AchouCapitulo = true;
                                }
                                else
                                {
                                    AchouCapitulo = false;
                                    sCapitulo = "";
                                }

                            }

                            if (TagName == "v")
                            {
                                sNumeroVersiculo = xReader.GetAttribute(0);  // pegar o número do versiculo                               

                            }


                            break;


                        case XmlNodeType.Text:


                            // Pega o capitulo do livro
                            if ((AchouLivro == true) && (AchouCapitulo == true) && (TagName == "v"))
                            {
                                sTextoVersiculo = xReader.Value;  // ler o texto do versiculo
                                if (sNumeroVersiculo == "1")
                                {
                                    txtNumeroVersiculo1.Text = sNumeroVersiculo.ToString();
                                    TxtVersiculo1.Text = sTextoVersiculo;
                                    PrimeirVersculo = Int32.Parse(sNumeroVersiculo);
                                    PrimeiroVersiculoAnterior = PrimeirVersculo; // registra o primeiro versiculo da tela atual
                                    sContaVersiculosvv++;
                                }

                                if (sNumeroVersiculo == "2")
                                {
                                    txtNumeroVersiculo2.Text = sNumeroVersiculo.ToString();
                                    TxtVersiculo2.Text = sTextoVersiculo;
                                    sContaVersiculosvv++;
                                }

                                if (sNumeroVersiculo == "3")
                                {
                                    txtNumeroVersiculo3.Text = sNumeroVersiculo.ToString();
                                    TxtVersiculo3.Text = sTextoVersiculo;
                                    UltimoVersiculo = Int32.Parse(sNumeroVersiculo);
                                    AchouLivro = false;
                                    AchouCapitulo = false;
                                    sContaVersiculosvv++;
                                }
                            }


                            break;


                    } // fim so swhitch 

                } // FIM DO LOOP QUE CARREGA OS TRÊS PRIMEIROS VERSÍCULOS

                // DEFINE A COR DOS LIVROS DO PENTATEUCO
                if ((snome == "Gênesis") || (snome == "Êxodo") || (snome == "Levítico") || (snome == "Números") || (snome == "Deuteronômio"))
                {

                    TxtNomeLivro.SetTextColor(Android.Graphics.Color.Gold);
                    TxtCapitulo.SetTextColor(Android.Graphics.Color.White);

                    txtNumeroVersiculo1.SetTextColor(Android.Graphics.Color.Gold);
                    TxtVersiculo1.SetTextColor(Android.Graphics.Color.White);
                    txtNumeroVersiculo2.SetTextColor(Android.Graphics.Color.Gold);
                    TxtVersiculo2.SetTextColor(Android.Graphics.Color.White);
                    txtNumeroVersiculo3.SetTextColor(Android.Graphics.Color.Gold);
                    TxtVersiculo3.SetTextColor(Android.Graphics.Color.White);
                }

                // DEFINE A COR DOS LIVROS HISTÓRICOS
                if ((snome == "Josué") || (snome == "Rute") || (snome == "1º Samuel") || (snome == "2º Samuel") || (snome == "1º Reis"))
                {
                    TxtNomeLivro.SetTextColor(Android.Graphics.Color.White);
                    TxtCapitulo.SetTextColor(Android.Graphics.Color.Blue);

                    txtNumeroVersiculo1.SetTextColor(Android.Graphics.Color.White);
                    TxtVersiculo1.SetTextColor(Android.Graphics.Color.Blue);
                    txtNumeroVersiculo2.SetTextColor(Android.Graphics.Color.White);
                    TxtVersiculo2.SetTextColor(Android.Graphics.Color.Blue);
                    txtNumeroVersiculo3.SetTextColor(Android.Graphics.Color.White);
                    TxtVersiculo3.SetTextColor(Android.Graphics.Color.Blue);
                }

                if ((snome == "2º Reis") || (snome == "1º Crônicas") || (snome == "2º Crônicas") || (snome == "Esdras") || (snome == "Neemias") || (snome == "Ester"))
                {
                    TxtNomeLivro.SetTextColor(Android.Graphics.Color.Blue);
                    TxtCapitulo.SetTextColor(Android.Graphics.Color.White);

                    txtNumeroVersiculo1.SetTextColor(Android.Graphics.Color.White);
                    TxtVersiculo1.SetTextColor(Android.Graphics.Color.Blue);
                    txtNumeroVersiculo2.SetTextColor(Android.Graphics.Color.White);
                    TxtVersiculo2.SetTextColor(Android.Graphics.Color.Blue);
                    txtNumeroVersiculo3.SetTextColor(Android.Graphics.Color.White);
                    TxtVersiculo3.SetTextColor(Android.Graphics.Color.Blue);
                }


                // DEFINE A COR DOS LIVROS POÉTICOS
                if ((snome == "Jó") || (snome == "Salmos") || (snome == "Provérbios") || (snome == "Eclesiastes") || (snome == "Cântico doas Cânticos") || (snome == "Lamentações"))
                {
                    TxtNomeLivro.SetTextColor(Android.Graphics.Color.Yellow);
                    TxtCapitulo.SetTextColor(Android.Graphics.Color.LightBlue);


                    txtNumeroVersiculo1.SetTextColor(Android.Graphics.Color.LightBlue);
                    TxtVersiculo1.SetTextColor(Android.Graphics.Color.Yellow);
                    txtNumeroVersiculo2.SetTextColor(Android.Graphics.Color.LightBlue);
                    TxtVersiculo2.SetTextColor(Android.Graphics.Color.Yellow);
                    txtNumeroVersiculo3.SetTextColor(Android.Graphics.Color.LightBlue);
                    TxtVersiculo3.SetTextColor(Android.Graphics.Color.Yellow);
                }

                // DEFINE A COR DOS LIVROS DOS PROFETAS MAIORES
                if ((snome == "Isaías") || (snome == "Jeremias") || (snome == "Daniel") || (snome == "Ezequiel") || (snome == "Oséias") || (snome == "Zacarias"))
                {
                    TxtNomeLivro.SetTextColor(Android.Graphics.Color.Green);
                    TxtCapitulo.SetTextColor(Android.Graphics.Color.Yellow);

                    txtNumeroVersiculo1.SetTextColor(Android.Graphics.Color.Yellow);
                    TxtVersiculo1.SetTextColor(Android.Graphics.Color.Green);
                    txtNumeroVersiculo2.SetTextColor(Android.Graphics.Color.Yellow);
                    TxtVersiculo2.SetTextColor(Android.Graphics.Color.Green);
                    txtNumeroVersiculo3.SetTextColor(Android.Graphics.Color.Yellow);
                    TxtVersiculo3.SetTextColor(Android.Graphics.Color.Green);
                }


                // DEFINE A COR DOS LIVROS DOS PROFETAS MENORES
                if ((snome == "Joel") || (snome == "Amós") || (snome == "Obadias") || (snome == "Jonas") || (snome == "Miquéias") || (snome == "Naum"))
                {
                    TxtNomeLivro.SetTextColor(Android.Graphics.Color.Red);
                    TxtCapitulo.SetTextColor(Android.Graphics.Color.Green);

                    txtNumeroVersiculo1.SetTextColor(Android.Graphics.Color.Green);
                    TxtVersiculo1.SetTextColor(Android.Graphics.Color.Red);
                    txtNumeroVersiculo2.SetTextColor(Android.Graphics.Color.Green);
                    TxtVersiculo2.SetTextColor(Android.Graphics.Color.Red);
                    txtNumeroVersiculo3.SetTextColor(Android.Graphics.Color.Green);
                    TxtVersiculo3.SetTextColor(Android.Graphics.Color.Red);
                }

                // DEFINE A COR DOS LIVROS DOS PROFETAS MENORES
                if ((snome == "Habacuque") || (snome == "Sofonias") || (snome == "Ageu") || (snome == "Malaquias"))
                {
                    TxtNomeLivro.SetTextColor(Android.Graphics.Color.Red);
                    TxtCapitulo.SetTextColor(Android.Graphics.Color.Green);

                    txtNumeroVersiculo1.SetTextColor(Android.Graphics.Color.Green);
                    TxtVersiculo1.SetTextColor(Android.Graphics.Color.Red);
                    txtNumeroVersiculo2.SetTextColor(Android.Graphics.Color.Green);
                    TxtVersiculo2.SetTextColor(Android.Graphics.Color.Red);
                    txtNumeroVersiculo3.SetTextColor(Android.Graphics.Color.Green);
                    TxtVersiculo3.SetTextColor(Android.Graphics.Color.Red);
                }

                // DEFINE A COR DOS LIVROS DO EVANGELHO
                if ((snome == "Mateus") || (snome == "Marcos") || (snome == "Lucas") || (snome == "João"))
                {
                    TxtNomeLivro.SetTextColor(Android.Graphics.Color.Gold);
                    TxtCapitulo.SetTextColor(Android.Graphics.Color.White);

                    txtNumeroVersiculo1.SetTextColor(Android.Graphics.Color.White);
                    TxtVersiculo1.SetTextColor(Android.Graphics.Color.Gold);
                    txtNumeroVersiculo2.SetTextColor(Android.Graphics.Color.White);
                    TxtVersiculo2.SetTextColor(Android.Graphics.Color.Gold);
                    txtNumeroVersiculo3.SetTextColor(Android.Graphics.Color.White);
                    TxtVersiculo3.SetTextColor(Android.Graphics.Color.Gold);
                }


                // DEFINE A COR DO LIVRO DE ATOS
                if ((snome == "Atos"))
                {
                    TxtNomeLivro.SetTextColor(Android.Graphics.Color.DarkKhaki);
                    TxtCapitulo.SetTextColor(Android.Graphics.Color.Black);

                    txtNumeroVersiculo1.SetTextColor(Android.Graphics.Color.Black);
                    TxtVersiculo1.SetTextColor(Android.Graphics.Color.DarkKhaki);
                    txtNumeroVersiculo2.SetTextColor(Android.Graphics.Color.Black);
                    TxtVersiculo2.SetTextColor(Android.Graphics.Color.DarkKhaki);
                    txtNumeroVersiculo3.SetTextColor(Android.Graphics.Color.Black);
                    TxtVersiculo3.SetTextColor(Android.Graphics.Color.DarkKhaki);
                }

                // DEFINE A COR DO LIVRO DE PAULO
                if ((snome == "Romanos") || (snome == "1ª Coríntios") || (snome == "2ª Coríntios") || (snome == "Gálatas"))
                {
                    TxtNomeLivro.SetTextColor(Android.Graphics.Color.Yellow);
                    TxtCapitulo.SetTextColor(Android.Graphics.Color.Blue);

                    txtNumeroVersiculo1.SetTextColor(Android.Graphics.Color.Blue);
                    TxtVersiculo1.SetTextColor(Android.Graphics.Color.Yellow);
                    txtNumeroVersiculo2.SetTextColor(Android.Graphics.Color.Blue);
                    TxtVersiculo2.SetTextColor(Android.Graphics.Color.Yellow);
                    txtNumeroVersiculo3.SetTextColor(Android.Graphics.Color.Blue);
                    TxtVersiculo3.SetTextColor(Android.Graphics.Color.Yellow);
                }

                if ((snome == "Efésios") || (snome == "Filipenses") || (snome == "Colossenses") || (snome == "1ª Tessalonicenses"))
                {
                    TxtNomeLivro.SetTextColor(Android.Graphics.Color.Yellow);
                    TxtCapitulo.SetTextColor(Android.Graphics.Color.Blue);

                    txtNumeroVersiculo1.SetTextColor(Android.Graphics.Color.Blue);
                    TxtVersiculo1.SetTextColor(Android.Graphics.Color.Yellow);
                    txtNumeroVersiculo2.SetTextColor(Android.Graphics.Color.Blue);
                    TxtVersiculo2.SetTextColor(Android.Graphics.Color.Yellow);
                    txtNumeroVersiculo3.SetTextColor(Android.Graphics.Color.Blue);
                    TxtVersiculo3.SetTextColor(Android.Graphics.Color.Yellow);
                }

                if ((snome == "2ª Tessalonicenses") || (snome == "1ª Timóteo") || (snome == "2ª Timóteo") || (snome == "Tito") || (snome == "Filemom"))
                {
                    TxtNomeLivro.SetTextColor(Android.Graphics.Color.Yellow);
                    TxtCapitulo.SetTextColor(Android.Graphics.Color.Blue);

                    txtNumeroVersiculo1.SetTextColor(Android.Graphics.Color.Blue);
                    TxtVersiculo1.SetTextColor(Android.Graphics.Color.Yellow);
                    txtNumeroVersiculo2.SetTextColor(Android.Graphics.Color.Blue);
                    TxtVersiculo2.SetTextColor(Android.Graphics.Color.Yellow);
                    txtNumeroVersiculo3.SetTextColor(Android.Graphics.Color.Blue);
                    TxtVersiculo3.SetTextColor(Android.Graphics.Color.Yellow);
                }

                // DEFINE A COR DOS LIVROS DE JOÃO
                if ((snome == "1ª João") || (snome == "2ª João") || (snome == "3ª João") || (snome == "Revelações"))
                {
                    TxtNomeLivro.SetTextColor(Android.Graphics.Color.Pink);
                    TxtCapitulo.SetTextColor(Android.Graphics.Color.Navy);

                    txtNumeroVersiculo1.SetTextColor(Android.Graphics.Color.Navy);
                    TxtVersiculo1.SetTextColor(Android.Graphics.Color.Pink);
                    txtNumeroVersiculo2.SetTextColor(Android.Graphics.Color.Navy);
                    TxtVersiculo2.SetTextColor(Android.Graphics.Color.Pink);
                    txtNumeroVersiculo3.SetTextColor(Android.Graphics.Color.Navy);
                    TxtVersiculo3.SetTextColor(Android.Graphics.Color.Pink);
                }

                // DEFINE A COR DOS LIVROS DIVERSOS
                if ((snome == "Hebreus") || (snome == "Tiago") || (snome == "1ª Pedro") || (snome == "2ª Pedro") || (snome == "Judas"))
                {
                    TxtNomeLivro.SetTextColor(Android.Graphics.Color.Salmon);
                    TxtCapitulo.SetTextColor(Android.Graphics.Color.Silver);

                    txtNumeroVersiculo1.SetTextColor(Android.Graphics.Color.Silver);
                    TxtVersiculo1.SetTextColor(Android.Graphics.Color.Salmon);
                    txtNumeroVersiculo2.SetTextColor(Android.Graphics.Color.Silver);
                    TxtVersiculo2.SetTextColor(Android.Graphics.Color.Salmon);
                    txtNumeroVersiculo3.SetTextColor(Android.Graphics.Color.Silver);
                    TxtVersiculo3.SetTextColor(Android.Graphics.Color.Salmon);
                }

            };  // FIM DO BOTÃO PRÓXIMO CAPÍTULO

            // 02/01/2021 17:39h Botao Dia Anterior pressionado
            BotaoAnterior.Click += (sender, e) =>
            {
                ContaCliques = ContaCliques + 1;

                // Torna invsível se está na ultima página e retorna a anterior
                BotaoProximoCap.Visibility = ViewStates.Invisible;

                // 22/02/2021 19:36h 
                // AT - VERSICULO ANTERIOR
                if (text_Valor == "AT")
                {
                    bool sVersiculo1Preenchido = false;
                    bool sVersiculo2Preenchido = false;
                    bool sVersiculo3Preenchido = false;
                    int sFirstVersculo = 0;
                    string sVersiculonaTela = txtNumeroVersiculo1.Text;

                    

                    sFirstVersculo = PrimeirVersculo;
                    // São os três primeiros versículos do capítulo deois da primeira interação de Próximo 
                    if ((sFirstVersculo == 1) && (sVersiculonaTela == "1"))
                    {
                        ContaCliques = 0;
                        sContaVersiculosvv = 3;
                        // 4. SE FOR OS 3 PRIMEIRO VERSICULOS DO CAPÍTULO E O LIVRO DE GÊNESIS, NÃO FAZ NADA! NOSTRA MENSAGEM INÍCIO DO CAPÍTULO
                        Toast.MakeText(this, "Início do capítulo!", ToastLength.Long).Show();
                        
                        // Torna os botões Visíveis no final somente para livros com mais de um capítulo e no primeiro capítulo
                        if ((snome == "Obadias") || (snome == "Filemom") || (snome == "Judas") || (snome == "Filemom") || (snome == "2ª João") || (snome == "3ª João") || (sdesc == "1"))
                        {
                            BotaoCapAnterior.Visibility = ViewStates.Invisible;
                        }
                        else 
                        {
                            BotaoCapAnterior.Visibility = ViewStates.Visible;
                        }
                        
                    }
                                       
                    sFirstVersculo = sFirstVersculo - 1;  //Subtrai um do primeiro versiculo

                    // São os três primeiros versículos do capítulo 
                    if ((sFirstVersculo <= 0)  && (ContaCliques <= 1)) 
                    {
                        ContaCliques = 0;
                        sContaVersiculosvv = 3;
                        // 4. SE FOR OS 3 PRIMEIRO VERSICULOS DO CAPÍTULO E O LIVRO DE GÊNESIS, NÃO FAZ NADA! NOSTRA MENSAGEM INÍCIO DO CAPÍTULO
                        Toast.MakeText(this, "Início do capítulo!", ToastLength.Long).Show();
                        if ((snome == "Obadias") || (snome == "Filemom") || (snome == "Judas") || (snome == "Filemom") || (snome == "2ª João") || (snome == "3ª João") || (sdesc == "1"))
                        {
                            BotaoCapAnterior.Visibility = ViewStates.Invisible;
                        }
                        else
                        {
                            BotaoCapAnterior.Visibility = ViewStates.Visible;
                        }
                        
                    }
                    else  // NÃO SE TRATA DO PRIMEIRO VERSICULO DO CAPITULO
                    {
                        
                        string Iversiculo = "";

                        sContaVersiculosvv = sContaVersiculosvv - 3; // Subtrai três versiculos do contador de Versiculos do Botão Próximo
                                                                     // 
                        // No fim dos versículos deve voltar para o primeiro anterior
                        if (sChegouNoUltimo == true) 
                        {
                            Iversiculo = PrimeiroVersiculoAnterior.ToString(); // Pega o primeiroVersiculo Anterior da página
                            sFirstVersculo = Int32.Parse(Iversiculo);                                                       
                            sChegouNoUltimo = false;
                        }
                        else 
                        { 
                            Iversiculo = PrimeiroVersiculoAnterior.ToString(); // Pega o primeiroVersiculo Anterior da página
                            sFirstVersculo = Int32.Parse(Iversiculo);

                            // Limpar os textos dos versiculos assim que entrar
                            TxtVersiculo1.Text = "";
                            TxtVersiculo2.Text = "";
                            TxtVersiculo3.Text = "";

                            // Limpar os números dos versículos assim que entrar
                            txtNumeroVersiculo1.Text = "";
                            txtNumeroVersiculo2.Text = "";
                            txtNumeroVersiculo3.Text = "";
                        }

                        
                        bool AchouLivro = false;
                        bool AchouCapitulo = false;
                        XmlReader xReader = XmlReader.Create(Assets.Open("nvi.xml"));

                        while (xReader.Read())
                        {

                            switch (xReader.NodeType)
                            {

                                case XmlNodeType.Element:

                                    // Lê a tag inicial
                                    TagName = xReader.Name;

                                    // se for tipoLei marca sLeiLazer como verdadeiro
                                    if (xReader.Name == "book")
                                    {
                                        AchouLivro = true;
                                        sTipoLei = xReader.GetAttribute(0); //Nome do Livro
                                        if (snome == sTipoLei)
                                        {
                                            AchouLivro = true;
                                            sNomeLivro = sTipoLei;
                                        }
                                        else
                                        {
                                            AchouLivro = false;
                                            sNomeLivro = "";

                                        }
                                    }

                                    if (xReader.Name == "c")
                                    {
                                        AchouCapitulo = true;
                                        sCapitulo = xReader.GetAttribute(0);  // pegar o capítulo
                                        if (sCapitulo == sdesc)
                                        {
                                            AchouCapitulo = true;
                                        }
                                        else
                                        {
                                            AchouCapitulo = false;
                                            sCapitulo = "";
                                        }

                                    }

                                    if (TagName == "v")
                                    {
                                        sNumeroVersiculo = xReader.GetAttribute(0);  // pegar o número do versiculo

                                    }

                                    break;

                                case XmlNodeType.Text:

                                    // Pega o capitulo do livro
                                    if ((AchouLivro == true) && (AchouCapitulo == true) && (TagName == "v"))
                                    {
                                        sTextoVersiculo = xReader.Value;  // ler o texto do versiculo
                                        if ((sNumeroVersiculo == Iversiculo) && (sVersiculo1Preenchido == false))
                                        {
                                            txtNumeroVersiculo1.Text = sNumeroVersiculo.ToString();
                                            if (PrimeiroVersiculoAnterior > 3) 
                                            {
                                                PrimeiroVersiculoAnterior = sFirstVersculo - 3;
                                                PrimeirVersculo = sFirstVersculo - 3;
                                            }
                                            else
                                            {
                                                PrimeiroVersiculoAnterior = sFirstVersculo;
                                                PrimeirVersculo = sFirstVersculo;
                                            }
                                            TxtVersiculo1.Text = sTextoVersiculo;
                                            sFirstVersculo = sFirstVersculo + 1;
                                            Iversiculo = sFirstVersculo.ToString();
                                            sVersiculo1Preenchido = true;                                                                                        
                                        }

                                        if ((sNumeroVersiculo == Iversiculo) && (sVersiculo2Preenchido == false))
                                        {
                                            txtNumeroVersiculo2.Text = sNumeroVersiculo.ToString();
                                            TxtVersiculo2.Text = sTextoVersiculo;
                                            sFirstVersculo = sFirstVersculo + 1;
                                            Iversiculo = sFirstVersculo.ToString();
                                            sVersiculo2Preenchido = true;
                                        }

                                        if ((sNumeroVersiculo == Iversiculo) && (sVersiculo3Preenchido == false))
                                        {
                                            txtNumeroVersiculo3.Text = sNumeroVersiculo.ToString();
                                            TxtVersiculo3.Text = sTextoVersiculo;                                            
                                            Iversiculo = sFirstVersculo.ToString();
                                            UltimoVersiculo = Int32.Parse(sNumeroVersiculo); // Passa a ser o último versiculo
                                            sVersiculo3Preenchido = true;
                                            AchouLivro = false;
                                            AchouCapitulo = false;
                                            if (UltimoVersiculo == 3)
                                            {
                                                PrimeirVersculo = 1; // Para evitar erro no inicio
                                            }                                            
                                        }
                                    }

                                    break;

                            } // fim so swhitch 

                        } // fim do loop

                    } // Fim do Else 
                                        

                } // Fim da opção de Leitura AT

                // 17/03/2021 16:10h 
                // AT - VERSICULO ANTERIOR
                if (text_Valor == "NT")
                {
                    bool sVersiculo1Preenchido = false;
                    bool sVersiculo2Preenchido = false;
                    bool sVersiculo3Preenchido = false;
                    int sFirstVersculo = 0;
                    string sVersiculonaTela = txtNumeroVersiculo1.Text;

                    sFirstVersculo = PrimeirVersculo;
                    // São os três primeiros versículos do capítulo deois da primeira interação de Próximo 
                    if ((sFirstVersculo == 1) && (sVersiculonaTela == "1"))
                    {
                        ContaCliques = 0;
                        // 4. SE FOR OS 3 PRIMEIRO VERSICULOS DO CAPÍTULO E O LIVRO DE GÊNESIS, NÃO FAZ NADA! NOSTRA MENSAGEM INÍCIO DO CAPÍTULO
                        Toast.MakeText(this, "Início do capítulo!", ToastLength.Long).Show();
                        if ((snome == "Obadias") || (sdesc == "1") || (snome == "Filemom") || (snome == "Judas") || (snome == "Filemom") || (snome == "2ª João") || (snome == "3ª João"))
                        {
                            BotaoCapAnterior.Visibility = ViewStates.Invisible;
                        }
                        else
                        {
                            BotaoCapAnterior.Visibility = ViewStates.Visible;
                        }                        
                    }

                    sFirstVersculo = sFirstVersculo - 1;  //Subtrai um do primeiro versiculo

                    // São os três primeiros versículos do capítulo 
                    if ((sFirstVersculo <= 0) && (ContaCliques <= 1))
                    {
                        ContaCliques = 0;
                        // 4. SE FOR OS 3 PRIMEIRO VERSICULOS DO CAPÍTULO E O LIVRO DE GÊNESIS, NÃO FAZ NADA! NOSTRA MENSAGEM INÍCIO DO CAPÍTULO
                        Toast.MakeText(this, "Início do capítulo!", ToastLength.Long).Show();
                        if ((snome == "Obadias") || (sdesc == "1") || (snome == "Filemom") || (snome == "Judas") || (snome == "Filemom") || (snome == "2ª João") || (snome == "3ª João"))
                        {
                            BotaoCapAnterior.Visibility = ViewStates.Invisible;
                        }
                        else
                        {
                            BotaoCapAnterior.Visibility = ViewStates.Visible;
                        }
                    }
                    else  // NÃO SE TRATA DO PRIMEIRO VERSICULO DO CAPITULO
                    {
                        string Iversiculo = "";

                        if (sChegouNoUltimo == true)
                        {
                            Iversiculo = PrimeiroVersiculoAnterior.ToString(); // Pega o primeiroVersiculo Anterior da página
                            sFirstVersculo = Int32.Parse(Iversiculo);
                            sChegouNoUltimo = false;
                        }
                        else
                        {
                            Iversiculo = PrimeiroVersiculoAnterior.ToString(); // Pega o primeiroVersiculo Anterior da página
                            sFirstVersculo = Int32.Parse(Iversiculo);

                            // Limpar os textos dos versiculos assim que entrar
                            TxtVersiculo1.Text = "";
                            TxtVersiculo2.Text = "";
                            TxtVersiculo3.Text = "";

                            // Limpar os números dos versículos assim que entrar
                            txtNumeroVersiculo1.Text = "";
                            txtNumeroVersiculo2.Text = "";
                            txtNumeroVersiculo3.Text = "";
                        }
                        

                        bool AchouLivro = false;
                        bool AchouCapitulo = false;
                        XmlReader xReader = XmlReader.Create(Assets.Open("nvi.xml"));

                        while (xReader.Read())
                        {

                            switch (xReader.NodeType)
                            {

                                case XmlNodeType.Element:

                                    // Lê a tag inicial
                                    TagName = xReader.Name;

                                    // se for tipoLei marca sLeiLazer como verdadeiro
                                    if (xReader.Name == "book")
                                    {
                                        AchouLivro = true;
                                        sTipoLei = xReader.GetAttribute(0); //Nome do Livro
                                        if (snome == sTipoLei)
                                        {
                                            AchouLivro = true;
                                            sNomeLivro = sTipoLei;
                                        }
                                        else
                                        {
                                            AchouLivro = false;
                                            sNomeLivro = "";

                                        }
                                    }

                                    if (xReader.Name == "c")
                                    {
                                        AchouCapitulo = true;
                                        sCapitulo = xReader.GetAttribute(0);  // pegar o capítulo
                                        if (sCapitulo == sdesc)
                                        {
                                            AchouCapitulo = true;
                                        }
                                        else
                                        {
                                            AchouCapitulo = false;
                                            sCapitulo = "";
                                        }

                                    }

                                    if (TagName == "v")
                                    {
                                        sNumeroVersiculo = xReader.GetAttribute(0);  // pegar o número do versiculo
                                        
                                    }

                                    break;

                                case XmlNodeType.Text:

                                    // Pega o capitulo do livro
                                    if ((AchouLivro == true) && (AchouCapitulo == true) && (TagName == "v"))
                                    {
                                        sTextoVersiculo = xReader.Value;  // ler o texto do versiculo
                                        if ((sNumeroVersiculo == Iversiculo) && (sVersiculo1Preenchido == false))
                                        {
                                            txtNumeroVersiculo1.Text = sNumeroVersiculo.ToString();
                                            if (PrimeiroVersiculoAnterior > 3)
                                            {
                                                PrimeiroVersiculoAnterior = sFirstVersculo - 3;
                                                PrimeirVersculo = sFirstVersculo - 3;
                                            }
                                            else
                                            {
                                                PrimeiroVersiculoAnterior = sFirstVersculo;
                                                PrimeirVersculo = sFirstVersculo;
                                            }
                                            TxtVersiculo1.Text = sTextoVersiculo;
                                            sFirstVersculo = sFirstVersculo + 1;
                                            Iversiculo = sFirstVersculo.ToString();
                                            sVersiculo1Preenchido = true;
                                        }

                                        if ((sNumeroVersiculo == Iversiculo) && (sVersiculo2Preenchido == false))
                                        {
                                            txtNumeroVersiculo2.Text = sNumeroVersiculo.ToString();
                                            TxtVersiculo2.Text = sTextoVersiculo;
                                            sFirstVersculo = sFirstVersculo + 1;
                                            Iversiculo = sFirstVersculo.ToString();
                                            sVersiculo2Preenchido = true;
                                        }

                                        if ((sNumeroVersiculo == Iversiculo) && (sVersiculo3Preenchido == false))
                                        {
                                            txtNumeroVersiculo3.Text = sNumeroVersiculo.ToString();
                                            TxtVersiculo3.Text = sTextoVersiculo;
                                            Iversiculo = sFirstVersculo.ToString();
                                            UltimoVersiculo = Int32.Parse(sNumeroVersiculo); // Passa a ser o último versiculo
                                            sVersiculo3Preenchido = true;
                                            AchouLivro = false;
                                            AchouCapitulo = false;
                                            if (UltimoVersiculo == 3)
                                            {
                                                PrimeirVersculo = 1; // Para evitar erro no inicio
                                            }
                                        }
                                    }

                                    break;

                            } // fim so swhitch 

                        } // fim do loop

                    } // Fim do Else 

                }  // FIM DA OPÇÃO AT

            }; // FIM DO BOTÃO VERSICULO ANTERIOR

            
            // AT - PRÓXIMO VERSICULO
            // 15/03/2021 17:40 finalizado!
            BotaoProximo.Click += (sender, e) =>
            {                
                bool sVersiculo1Preenchido = false;
                bool sVersiculo2Preenchido = false;
                bool sVersiculo3Preenchido = false;
                
                ContaCliques = ContaCliques + 1;

                // Torna invisível se está na primeira página e passa para a próxima
                BotaoCapAnterior.Visibility = ViewStates.Invisible;

                // Validação da última página de versísculos
                if (sChegouNoUltimo == true)
                {
                    Toast.MakeText(this, "Último versículo! Fim do capítulo!!", ToastLength.Long).Show();                    
                    if ((snome == "Obadias") ||  (sdesc == sTotalCapitulos) || (snome == "Filemom") || (snome == "Judas") || (snome == "Filemom") || (snome == "2ª João") || (snome == "3ª João"))
                    {
                        BotaoProximoCap.Visibility = ViewStates.Invisible;
                    }
                    else
                    {
                        BotaoProximoCap.Visibility = ViewStates.Visible;
                    }
                }

                else
                {
                    // 22/02/2021 19:36h 
                    // AT - PRÓXIMO VERSICULO
                    if (text_Valor == "AT")
                    {
                                                
                        string Iversiculo = "";

                        if (sChegouNoUltimo == false)
                        {                       
                            // Passa ao próximo versículo
                            UltimoVersiculo = UltimoVersiculo + 1;
                            Iversiculo = UltimoVersiculo.ToString();

                            // Limpar os textos dos versiculos assim que entrar
                            TxtVersiculo1.Text = "";
                            TxtVersiculo2.Text = "";
                            TxtVersiculo3.Text = "";

                            // Limpar os números dos versículos assim que entrar
                            txtNumeroVersiculo1.Text = "";
                            txtNumeroVersiculo2.Text = "";
                            txtNumeroVersiculo3.Text = "";
                        }
                        

                        bool AchouLivro = false;
                        bool AchouCapitulo = false;
                        XmlReader xReader = XmlReader.Create(Assets.Open("nvi.xml"));

                        while (xReader.Read())
                        {

                            switch (xReader.NodeType)
                            {

                                case XmlNodeType.Element:

                                    // Lê a tag inicial
                                    TagName = xReader.Name;

                                    // se for tipoLei marca sLeiLazer como verdadeiro
                                    if (xReader.Name == "book")
                                    {
                                        AchouLivro = true;
                                        sTipoLei = xReader.GetAttribute(0); //Nome do Livro
                                        if (snome == sTipoLei)
                                        {
                                            AchouLivro = true;
                                            sNomeLivro = sTipoLei;
                                        }
                                        else
                                        {
                                            AchouLivro = false;
                                            sNomeLivro = "";

                                        }
                                    }

                                    if (xReader.Name == "c")
                                    {
                                        AchouCapitulo = true;
                                        sCapitulo = xReader.GetAttribute(0);  // pegar o capítulo
                                        if (sCapitulo == sdesc)
                                        {
                                            AchouCapitulo = true;
                                            
                                        }
                                        else
                                        {
                                            AchouCapitulo = false;
                                            sCapitulo = "";
                                        }

                                    }

                                    if (TagName == "v")
                                    {
                                        sNumeroVersiculo = xReader.GetAttribute(0);  // pegar o número do versiculo
                                        
                                    }

                                    break;

                                
                                case XmlNodeType.Text:

                                    // Pega o capitulo do livro
                                    if ((AchouLivro == true) && (AchouCapitulo == true) && (TagName == "v"))
                                    {
                                        sTextoVersiculo = xReader.Value;  // ler o texto do versiculo
                                        
                                        if ((sNumeroVersiculo == Iversiculo) && (sVersiculo1Preenchido == false))
                                        {
                                            txtNumeroVersiculo1.Text = sNumeroVersiculo.ToString();
                                            TxtVersiculo1.Text = sTextoVersiculo;
                                            
                                            if (SNumeroTotalVersiculos.ToString() == sNumeroVersiculo.ToString())
                                            {
                                                sChegouNoUltimo = true;
                                            }

                                            PrimeirVersculo = UltimoVersiculo; // Faz o primeiro Versiculo receber o valor                                        
                                            if (ContaCliques > 1)
                                            {
                                                PrimeiroVersiculoAnterior = UltimoVersiculo - 3; // Faz o atual ser o marcador
                                            }
                                            UltimoVersiculo = UltimoVersiculo + 1;
                                            Iversiculo = UltimoVersiculo.ToString();
                                            sVersiculo1Preenchido = true;
                                            sContaVersiculosvv++; // Contar os versiculos
                                            

                                        }

                                        if ((sNumeroVersiculo == Iversiculo) && (sVersiculo2Preenchido == false))
                                        {
                                            txtNumeroVersiculo2.Text = sNumeroVersiculo.ToString();
                                            TxtVersiculo2.Text = sTextoVersiculo;

                                            if (SNumeroTotalVersiculos.ToString() == sNumeroVersiculo.ToString())
                                            {
                                                sChegouNoUltimo = true;
                                            }

                                            UltimoVersiculo = UltimoVersiculo + 1;
                                            Iversiculo = UltimoVersiculo.ToString();
                                            sVersiculo2Preenchido = true;
                                            sContaVersiculosvv++; // Contar os versiculos
                                                                                        

                                        }

                                        if ((sNumeroVersiculo == Iversiculo) && (sVersiculo3Preenchido == false))
                                        {
                                            txtNumeroVersiculo3.Text = sNumeroVersiculo.ToString();
                                            TxtVersiculo3.Text = sTextoVersiculo;

                                            if (SNumeroTotalVersiculos.ToString() == sNumeroVersiculo.ToString())
                                            {
                                                sChegouNoUltimo = true;
                                            }

                                            UltimoVersiculo = Int32.Parse(sNumeroVersiculo);
                                            Iversiculo = UltimoVersiculo.ToString();
                                            sVersiculo3Preenchido = true;
                                            AchouLivro = false;
                                            AchouCapitulo = false;
                                            sContaVersiculosvv++; // Contar os versiculos
                                            
                                        }

                                        break;
                                    

                            } // case do swhitch interno

                            break;

                            } // fim so swhitch 

                        } // fim do loop


                    }  // FIM DA OPÇÃO DO AT
                                        

                } // FIM DO ELSE INICIAL

                if (sChegouNoUltimo == true)
                {
                    Toast.MakeText(this, "Último versículo! Fim do capítulo!!", ToastLength.Long).Show();
                    
                    if ((snome == "Obadias") || (sdesc == sTotalCapitulos) || (snome == "Filemom") || (snome == "Judas") || (snome == "Filemom") || (snome == "2ª João") || (snome == "3ª João"))
                    {
                        BotaoProximoCap.Visibility = ViewStates.Invisible;
                    }
                    else
                    {
                        BotaoProximoCap.Visibility = ViewStates.Visible;
                    }
                }

                else
                {
                    // 16/03/2021 19:20h
                    if (text_Valor == "NT")
                    {

                        string Iversiculo = "";

                        if (sChegouNoUltimo == false)
                        {
                            // Passa ao próximo versículo
                            UltimoVersiculo = UltimoVersiculo + 1;
                            Iversiculo = UltimoVersiculo.ToString();

                            // Limpar os textos dos versiculos assim que entrar
                            TxtVersiculo1.Text = "";
                            TxtVersiculo2.Text = "";
                            TxtVersiculo3.Text = "";

                            // Limpar os números dos versículos assim que entrar
                            txtNumeroVersiculo1.Text = "";
                            txtNumeroVersiculo2.Text = "";
                            txtNumeroVersiculo3.Text = "";
                        }


                        bool AchouLivro = false;
                        bool AchouCapitulo = false;
                        XmlReader xReader = XmlReader.Create(Assets.Open("nvi.xml"));

                        while (xReader.Read())
                        {

                            switch (xReader.NodeType)
                            {

                                case XmlNodeType.Element:

                                    // Lê a tag inicial
                                    TagName = xReader.Name;

                                    // se for tipoLei marca sLeiLazer como verdadeiro
                                    if (xReader.Name == "book")
                                    {
                                        AchouLivro = true;
                                        sTipoLei = xReader.GetAttribute(0); //Nome do Livro
                                        if (snome == sTipoLei)
                                        {
                                            AchouLivro = true;
                                            sNomeLivro = sTipoLei;
                                        }
                                        else
                                        {
                                            AchouLivro = false;
                                            sNomeLivro = "";

                                        }
                                    }

                                    if (xReader.Name == "c")
                                    {
                                        AchouCapitulo = true;
                                        sCapitulo = xReader.GetAttribute(0);  // pegar o capítulo
                                        if (sCapitulo == sdesc)
                                        {
                                            AchouCapitulo = true;

                                        }
                                        else
                                        {
                                            AchouCapitulo = false;
                                            sCapitulo = "";
                                        }

                                    }

                                    if (TagName == "v")
                                    {
                                        sNumeroVersiculo = xReader.GetAttribute(0);  // pegar o número do versiculo

                                    }

                                    break;


                                case XmlNodeType.Text:

                                    // Pega o capitulo do livro
                                    if ((AchouLivro == true) && (AchouCapitulo == true) && (TagName == "v"))
                                    {
                                        sTextoVersiculo = xReader.Value;  // ler o texto do versiculo

                                        if ((sNumeroVersiculo == Iversiculo) && (sVersiculo1Preenchido == false))
                                        {
                                            txtNumeroVersiculo1.Text = sNumeroVersiculo.ToString();
                                            TxtVersiculo1.Text = sTextoVersiculo;
                                            
                                            if (SNumeroTotalVersiculos.ToString() == sNumeroVersiculo.ToString())
                                            {
                                                sChegouNoUltimo = true;
                                            }

                                            PrimeirVersculo = UltimoVersiculo; // Faz o primeiro Versiculo receber o valor                                        
                                            if (ContaCliques > 1)
                                            {
                                                PrimeiroVersiculoAnterior = UltimoVersiculo - 3; // Faz o atual ser o marcador
                                            }
                                            UltimoVersiculo = UltimoVersiculo + 1;
                                            Iversiculo = UltimoVersiculo.ToString();
                                            sVersiculo1Preenchido = true;

                                            sContaVersiculosvv++; // Contar os versiculos
                                            

                                        }

                                        if ((sNumeroVersiculo == Iversiculo) && (sVersiculo2Preenchido == false))
                                        {
                                            txtNumeroVersiculo2.Text = sNumeroVersiculo.ToString();
                                            TxtVersiculo2.Text = sTextoVersiculo;

                                            if (SNumeroTotalVersiculos.ToString() == sNumeroVersiculo.ToString())
                                            {
                                                sChegouNoUltimo = true;
                                            }

                                            UltimoVersiculo = UltimoVersiculo + 1;
                                            Iversiculo = UltimoVersiculo.ToString();
                                            sVersiculo2Preenchido = true;
                                            sContaVersiculosvv++; // Contar os versiculos
                                                                                       

                                        }

                                        if ((sNumeroVersiculo == Iversiculo) && (sVersiculo3Preenchido == false))
                                        {
                                            txtNumeroVersiculo3.Text = sNumeroVersiculo.ToString();
                                            TxtVersiculo3.Text = sTextoVersiculo;

                                            if (SNumeroTotalVersiculos.ToString() == sNumeroVersiculo.ToString())
                                            {
                                                sChegouNoUltimo = true;
                                            }
                                            UltimoVersiculo = Int32.Parse(sNumeroVersiculo);
                                            Iversiculo = UltimoVersiculo.ToString();
                                            sVersiculo3Preenchido = true;
                                            AchouLivro = false;
                                            AchouCapitulo = false;
                                            sContaVersiculosvv++; // Contar os versiculos
                                            
                                        }

                                        break;


                                    } // case do swhitch interno

                                    break;

                            } // fim so swhitch 

                        } // fim do loop

                    } // FIM DA OPÇÃO NT

                }   // FIM DO ELSE INICIAL   

            };  // FIM DO BOTÃO PRÓXIMO VERSICULO                        

        } //fim do oncreate              

        private void Tocar_Click(object sender, System.EventArgs e)
        {
            // Receber dados do livro e Capitulo selecionados
            string snome = Intent.GetStringExtra("MyDataLivro");
            string sdesc = Intent.GetStringExtra("MyDataCapitulo");

            if ((snome == "Gênesis") && (sdesc== "1"))
            {
                // Tocar o áudio 
                string uri = "android.resource://" + PackageName + "/" + Resource.Raw.gn_001;
                sAudio = uri;
                Android.Net.Uri no = Android.Net.Uri.Parse(uri);
                mRingtone = RingtoneManager.GetRingtone(this, no);
                mRingtone.Play();
            }

            if ((snome == "Gênesis") && (sdesc == "2"))
            {
                // Tocar o áudio 
                string uri = "android.resource://" + PackageName + "/" + Resource.Raw.gn_002;
                sAudio = uri;
                Android.Net.Uri no = Android.Net.Uri.Parse(uri);
                mRingtone = RingtoneManager.GetRingtone(this, no);
                mRingtone.Play();
            }

            if ((snome == "Gênesis") && (sdesc == "3"))
            {
                // Tocar o áudio 
                string uri = "android.resource://" + PackageName + "/" + Resource.Raw.gn_003;
                sAudio = uri;
                Android.Net.Uri no = Android.Net.Uri.Parse(uri);
                mRingtone = RingtoneManager.GetRingtone(this, no);
                mRingtone.Play();
            }

            if ((snome == "Gênesis") && (sdesc == "4"))
            {
                // Tocar o áudio 
                string uri = "android.resource://" + PackageName + "/" + Resource.Raw.gn_004;
                sAudio = uri;
                Android.Net.Uri no = Android.Net.Uri.Parse(uri);
                mRingtone = RingtoneManager.GetRingtone(this, no);
                mRingtone.Play();
            }

            if ((snome == "Gênesis") && (sdesc == "5"))
            {
                // Tocar o áudio 
                string uri = "android.resource://" + PackageName + "/" + Resource.Raw.gn_005;
                sAudio = uri;
                Android.Net.Uri no = Android.Net.Uri.Parse(uri);
                mRingtone = RingtoneManager.GetRingtone(this, no);
                mRingtone.Play();
            }

            if ((snome == "Gênesis") && (sdesc == "6"))
            {
                // Tocar o áudio 
                string uri = "android.resource://" + PackageName + "/" + Resource.Raw.gn_006;
                sAudio = uri;
                Android.Net.Uri no = Android.Net.Uri.Parse(uri);
                mRingtone = RingtoneManager.GetRingtone(this, no);
                mRingtone.Play();
            }

            if ((snome == "Gênesis") && (sdesc == "7"))
            {
                // Tocar o áudio 
                string uri = "android.resource://" + PackageName + "/" + Resource.Raw.gn_007;
                sAudio = uri;
                Android.Net.Uri no = Android.Net.Uri.Parse(uri);
                mRingtone = RingtoneManager.GetRingtone(this, no);
                mRingtone.Play();
            }

            if ((snome == "Gênesis") && (sdesc == "8"))
            {
                // Tocar o áudio 
                string uri = "android.resource://" + PackageName + "/" + Resource.Raw.gn_008;
                sAudio = uri;
                Android.Net.Uri no = Android.Net.Uri.Parse(uri);
                mRingtone = RingtoneManager.GetRingtone(this, no);
                mRingtone.Play();
            }

            if ((snome == "Gênesis") && (sdesc == "9"))
            {
                // Tocar o áudio 
                string uri = "android.resource://" + PackageName + "/" + Resource.Raw.gn_009;
                sAudio = uri;
                Android.Net.Uri no = Android.Net.Uri.Parse(uri);
                mRingtone = RingtoneManager.GetRingtone(this, no);
                mRingtone.Play();
            }

            if ((snome == "Gênesis") && (sdesc == "10"))
            {
                // Tocar o áudio 
                string uri = "android.resource://" + PackageName + "/" + Resource.Raw.gn_010;
                sAudio = uri;
                Android.Net.Uri no = Android.Net.Uri.Parse(uri);
                mRingtone = RingtoneManager.GetRingtone(this, no);
                mRingtone.Play();
            }

            if ((snome == "Gênesis") && (sdesc == "11"))
            {
                // Tocar o áudio 
                string uri = "android.resource://" + PackageName + "/" + Resource.Raw.gn_011;
                sAudio = uri;
                Android.Net.Uri no = Android.Net.Uri.Parse(uri);
                mRingtone = RingtoneManager.GetRingtone(this, no);
                mRingtone.Play();
            }

            if ((snome == "Gênesis") && (sdesc == "12"))
            {
                // Tocar o áudio 
                string uri = "android.resource://" + PackageName + "/" + Resource.Raw.gn_012;
                sAudio = uri;
                Android.Net.Uri no = Android.Net.Uri.Parse(uri);
                mRingtone = RingtoneManager.GetRingtone(this, no);
                mRingtone.Play();
            }

            if ((snome == "Gênesis") && (sdesc == "13"))
            {
                // Tocar o áudio 
                string uri = "android.resource://" + PackageName + "/" + Resource.Raw.gn_013;
                sAudio = uri;
                Android.Net.Uri no = Android.Net.Uri.Parse(uri);
                mRingtone = RingtoneManager.GetRingtone(this, no);
                mRingtone.Play();
            }

            if ((snome == "Gênesis") && (sdesc == "14"))
            {
                // Tocar o áudio 
                string uri = "android.resource://" + PackageName + "/" + Resource.Raw.gn_014;
                sAudio = uri;
                Android.Net.Uri no = Android.Net.Uri.Parse(uri);
                mRingtone = RingtoneManager.GetRingtone(this, no);
                mRingtone.Play();
            }

            if ((snome == "Gênesis") && (sdesc == "15"))
            {
                // Tocar o áudio 
                string uri = "android.resource://" + PackageName + "/" + Resource.Raw.gn_015;
                sAudio = uri;
                Android.Net.Uri no = Android.Net.Uri.Parse(uri);
                mRingtone = RingtoneManager.GetRingtone(this, no);
                mRingtone.Play();
            }

            if ((snome == "Gênesis") && (sdesc == "16"))
            {
                // Tocar o áudio 
                string uri = "android.resource://" + PackageName + "/" + Resource.Raw.gn_016;
                sAudio = uri;
                Android.Net.Uri no = Android.Net.Uri.Parse(uri);
                mRingtone = RingtoneManager.GetRingtone(this, no);
                mRingtone.Play();
            }

            if ((snome == "Gênesis") && (sdesc == "17"))
            {
                // Tocar o áudio 
                string uri = "android.resource://" + PackageName + "/" + Resource.Raw.gn_017;
                sAudio = uri;
                Android.Net.Uri no = Android.Net.Uri.Parse(uri);
                mRingtone = RingtoneManager.GetRingtone(this, no);
                mRingtone.Play();
            }

            if ((snome == "Gênesis") && (sdesc == "18"))
            {
                // Tocar o áudio 
                string uri = "android.resource://" + PackageName + "/" + Resource.Raw.gn_018;
                sAudio = uri;
                Android.Net.Uri no = Android.Net.Uri.Parse(uri);
                mRingtone = RingtoneManager.GetRingtone(this, no);
                mRingtone.Play();
            }

            if ((snome == "Gênesis") && (sdesc == "19"))
            {
                // Tocar o áudio 
                string uri = "android.resource://" + PackageName + "/" + Resource.Raw.gn_019;
                sAudio = uri;
                Android.Net.Uri no = Android.Net.Uri.Parse(uri);
                mRingtone = RingtoneManager.GetRingtone(this, no);
                mRingtone.Play();
            }

            if ((snome == "Gênesis") && (sdesc == "20"))
            {
                // Tocar o áudio 
                string uri = "android.resource://" + PackageName + "/" + Resource.Raw.gn_020;
                sAudio = uri;
                Android.Net.Uri no = Android.Net.Uri.Parse(uri);
                mRingtone = RingtoneManager.GetRingtone(this, no);
                mRingtone.Play();
            }

            if ((snome == "Gênesis") && (sdesc == "21"))
            {
                // Tocar o áudio 
                string uri = "android.resource://" + PackageName + "/" + Resource.Raw.gn_021;
                sAudio = uri;
                Android.Net.Uri no = Android.Net.Uri.Parse(uri);
                mRingtone = RingtoneManager.GetRingtone(this, no);
                mRingtone.Play();
            }

            if ((snome == "Gênesis") && (sdesc == "22"))
            {
                // Tocar o áudio 
                string uri = "android.resource://" + PackageName + "/" + Resource.Raw.gn_022;
                sAudio = uri;
                Android.Net.Uri no = Android.Net.Uri.Parse(uri);
                mRingtone = RingtoneManager.GetRingtone(this, no);
                mRingtone.Play();
            }

            if ((snome == "Gênesis") && (sdesc == "23"))
            {
                // Tocar o áudio 
                string uri = "android.resource://" + PackageName + "/" + Resource.Raw.gn_023;
                sAudio = uri;
                Android.Net.Uri no = Android.Net.Uri.Parse(uri);
                mRingtone = RingtoneManager.GetRingtone(this, no);
                mRingtone.Play();
            }

            if ((snome == "Gênesis") && (sdesc == "24"))
            {
                // Tocar o áudio 
                string uri = "android.resource://" + PackageName + "/" + Resource.Raw.gn_024;
                sAudio = uri;
                Android.Net.Uri no = Android.Net.Uri.Parse(uri);
                mRingtone = RingtoneManager.GetRingtone(this, no);
                mRingtone.Play();
            }

            if ((snome == "Gênesis") && (sdesc == "25"))
            {
                // Tocar o áudio 
                string uri = "android.resource://" + PackageName + "/" + Resource.Raw.gn_025;
                sAudio = uri;
                Android.Net.Uri no = Android.Net.Uri.Parse(uri);
                mRingtone = RingtoneManager.GetRingtone(this, no);
                mRingtone.Play();
            }

            if ((snome == "Gênesis") && (sdesc == "26"))
            {
                // Tocar o áudio 
                string uri = "android.resource://" + PackageName + "/" + Resource.Raw.gn_026;
                sAudio = uri;
                Android.Net.Uri no = Android.Net.Uri.Parse(uri);
                mRingtone = RingtoneManager.GetRingtone(this, no);
                mRingtone.Play();
            }

            if ((snome == "Gênesis") && (sdesc == "27"))
            {
                // Tocar o áudio 
                string uri = "android.resource://" + PackageName + "/" + Resource.Raw.gn_027;
                sAudio = uri;
                Android.Net.Uri no = Android.Net.Uri.Parse(uri);
                mRingtone = RingtoneManager.GetRingtone(this, no);
                mRingtone.Play();
            }

            if ((snome == "Gênesis") && (sdesc == "28"))
            {
                // Tocar o áudio 
                string uri = "android.resource://" + PackageName + "/" + Resource.Raw.gn_028;
                sAudio = uri;
                Android.Net.Uri no = Android.Net.Uri.Parse(uri);
                mRingtone = RingtoneManager.GetRingtone(this, no);
                mRingtone.Play();
            }

            if ((snome == "Gênesis") && (sdesc == "29"))
            {
                // Tocar o áudio 
                string uri = "android.resource://" + PackageName + "/" + Resource.Raw.gn_029;
                sAudio = uri;
                Android.Net.Uri no = Android.Net.Uri.Parse(uri);
                mRingtone = RingtoneManager.GetRingtone(this, no);
                mRingtone.Play();
            }

            if ((snome == "Gênesis") && (sdesc == "30"))
            {
                // Tocar o áudio 
                string uri = "android.resource://" + PackageName + "/" + Resource.Raw.gn_030;
                sAudio = uri;
                Android.Net.Uri no = Android.Net.Uri.Parse(uri);
                mRingtone = RingtoneManager.GetRingtone(this, no);
                mRingtone.Play();
            }

            if ((snome == "Gênesis") && (sdesc == "31"))
            {
                // Tocar o áudio 
                string uri = "android.resource://" + PackageName + "/" + Resource.Raw.gn_031;
                sAudio = uri;
                Android.Net.Uri no = Android.Net.Uri.Parse(uri);
                mRingtone = RingtoneManager.GetRingtone(this, no);
                mRingtone.Play();
            }

            if ((snome == "Gênesis") && (sdesc == "32"))
            {
                // Tocar o áudio 
                string uri = "android.resource://" + PackageName + "/" + Resource.Raw.gn_032;
                sAudio = uri;
                Android.Net.Uri no = Android.Net.Uri.Parse(uri);
                mRingtone = RingtoneManager.GetRingtone(this, no);
                mRingtone.Play();
            }

            if ((snome == "Gênesis") && (sdesc == "33"))
            {
                // Tocar o áudio 
                string uri = "android.resource://" + PackageName + "/" + Resource.Raw.gn_033;
                sAudio = uri;
                Android.Net.Uri no = Android.Net.Uri.Parse(uri);
                mRingtone = RingtoneManager.GetRingtone(this, no);
                mRingtone.Play();
            }

            if ((snome == "Gênesis") && (sdesc == "34"))
            {
                // Tocar o áudio 
                string uri = "android.resource://" + PackageName + "/" + Resource.Raw.gn_034;
                sAudio = uri;
                Android.Net.Uri no = Android.Net.Uri.Parse(uri);
                mRingtone = RingtoneManager.GetRingtone(this, no);
                mRingtone.Play();
            }

            if ((snome == "Gênesis") && (sdesc == "35"))
            {
                // Tocar o áudio 
                string uri = "android.resource://" + PackageName + "/" + Resource.Raw.gn_035;
                sAudio = uri;
                Android.Net.Uri no = Android.Net.Uri.Parse(uri);
                mRingtone = RingtoneManager.GetRingtone(this, no);
                mRingtone.Play();
            }

            if ((snome == "Gênesis") && (sdesc == "36"))
            {
                // Tocar o áudio 
                string uri = "android.resource://" + PackageName + "/" + Resource.Raw.gn_036;
                sAudio = uri;
                Android.Net.Uri no = Android.Net.Uri.Parse(uri);
                mRingtone = RingtoneManager.GetRingtone(this, no);
                mRingtone.Play();
            }

            if ((snome == "Gênesis") && (sdesc == "37"))
            {
                // Tocar o áudio 
                string uri = "android.resource://" + PackageName + "/" + Resource.Raw.gn_037;
                sAudio = uri;
                Android.Net.Uri no = Android.Net.Uri.Parse(uri);
                mRingtone = RingtoneManager.GetRingtone(this, no);
                mRingtone.Play();
            }

            if ((snome == "Gênesis") && (sdesc == "38"))
            {
                // Tocar o áudio 
                string uri = "android.resource://" + PackageName + "/" + Resource.Raw.gn_038;
                sAudio = uri;
                Android.Net.Uri no = Android.Net.Uri.Parse(uri);
                mRingtone = RingtoneManager.GetRingtone(this, no);
                mRingtone.Play();
            }

            if ((snome == "Gênesis") && (sdesc == "39"))
            {
                // Tocar o áudio 
                string uri = "android.resource://" + PackageName + "/" + Resource.Raw.gn_039;
                sAudio = uri;
                Android.Net.Uri no = Android.Net.Uri.Parse(uri);
                mRingtone = RingtoneManager.GetRingtone(this, no);
                mRingtone.Play();
            }

            if ((snome == "Gênesis") && (sdesc == "40"))
            {
                // Tocar o áudio 
                string uri = "android.resource://" + PackageName + "/" + Resource.Raw.gn_040;
                sAudio = uri;
                Android.Net.Uri no = Android.Net.Uri.Parse(uri);
                mRingtone = RingtoneManager.GetRingtone(this, no);
                mRingtone.Play();
            }

            if ((snome == "Gênesis") && (sdesc == "41"))
            {
                // Tocar o áudio 
                string uri = "android.resource://" + PackageName + "/" + Resource.Raw.gn_041;
                sAudio = uri;
                Android.Net.Uri no = Android.Net.Uri.Parse(uri);
                mRingtone = RingtoneManager.GetRingtone(this, no);
                mRingtone.Play();
            }

            if ((snome == "Gênesis") && (sdesc == "42"))
            {
                // Tocar o áudio 
                string uri = "android.resource://" + PackageName + "/" + Resource.Raw.gn_042;
                sAudio = uri;
                Android.Net.Uri no = Android.Net.Uri.Parse(uri);
                mRingtone = RingtoneManager.GetRingtone(this, no);
                mRingtone.Play();
            }

            if ((snome == "Gênesis") && (sdesc == "43"))
            {
                // Tocar o áudio 
                string uri = "android.resource://" + PackageName + "/" + Resource.Raw.gn_043;
                sAudio = uri;
                Android.Net.Uri no = Android.Net.Uri.Parse(uri);
                mRingtone = RingtoneManager.GetRingtone(this, no);
                mRingtone.Play();
            }

            if ((snome == "Gênesis") && (sdesc == "44"))
            {
                // Tocar o áudio 
                string uri = "android.resource://" + PackageName + "/" + Resource.Raw.gn_044;
                sAudio = uri;
                Android.Net.Uri no = Android.Net.Uri.Parse(uri);
                mRingtone = RingtoneManager.GetRingtone(this, no);
                mRingtone.Play();
            }

            if ((snome == "Gênesis") && (sdesc == "45"))
            {
                // Tocar o áudio 
                string uri = "android.resource://" + PackageName + "/" + Resource.Raw.gn_045;
                sAudio = uri;
                Android.Net.Uri no = Android.Net.Uri.Parse(uri);
                mRingtone = RingtoneManager.GetRingtone(this, no);
                mRingtone.Play();
            }

            if ((snome == "Gênesis") && (sdesc == "46"))
            {
                // Tocar o áudio 
                string uri = "android.resource://" + PackageName + "/" + Resource.Raw.gn_046;
                sAudio = uri;
                Android.Net.Uri no = Android.Net.Uri.Parse(uri);
                mRingtone = RingtoneManager.GetRingtone(this, no);
                mRingtone.Play();
            }

            if ((snome == "Gênesis") && (sdesc == "47"))
            {
                // Tocar o áudio 
                string uri = "android.resource://" + PackageName + "/" + Resource.Raw.gn_047;
                sAudio = uri;
                Android.Net.Uri no = Android.Net.Uri.Parse(uri);
                mRingtone = RingtoneManager.GetRingtone(this, no);
                mRingtone.Play();
            }

            if ((snome == "Gênesis") && (sdesc == "48"))
            {
                // Tocar o áudio 
                string uri = "android.resource://" + PackageName + "/" + Resource.Raw.gn_048;
                sAudio = uri;
                Android.Net.Uri no = Android.Net.Uri.Parse(uri);
                mRingtone = RingtoneManager.GetRingtone(this, no);
                mRingtone.Play();
            }

            if ((snome == "Gênesis") && (sdesc == "49"))
            {
                // Tocar o áudio 
                string uri = "android.resource://" + PackageName + "/" + Resource.Raw.gn_049;
                sAudio = uri;
                Android.Net.Uri no = Android.Net.Uri.Parse(uri);
                mRingtone = RingtoneManager.GetRingtone(this, no);
                mRingtone.Play();
            }

            if ((snome == "Gênesis") && (sdesc == "50"))
            {
                // Tocar o áudio 
                string uri = "android.resource://" + PackageName + "/" + Resource.Raw.gn_050;
                sAudio = uri;
                Android.Net.Uri no = Android.Net.Uri.Parse(uri);
                mRingtone = RingtoneManager.GetRingtone(this, no);
                mRingtone.Play();
            }
        }

        private void Parar_Click(object sender, System.EventArgs e)
        {
            mRingtone.Stop();
        }        

    }  

}