using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Content.Res;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Collections;
using System.Net;
using Java.Interop;
using Android.Content.PM;
using Uri = Android.Net.Uri;

namespace AppBibliaNVI
{
    [Activity(Label = "LeituraAT")]
    public class LeituraAT : Activity
    {
        [Export("btnSalvarClicked")]
        public void btnFecharClicked_Click(View v)
        {

            // Modificado 15/03/2021 11:28h
            Finish();  // Fecha a Activity corrente

        }

        Spinner spinner;
        Spinner spinner2;

        ArrayAdapter adapter;
        ArrayAdapter adapter2;

        ArrayList itens;
        ArrayList capitulos;

        public string sTipoLivro;
        public string sLivro = "";
        public string sCapitulo = "";
        public string sNumeroVersiculos = "";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //remove título desta Activity
            Window.RequestFeature(WindowFeatures.NoTitle);


            // Create your application here
            SetContentView(Resource.Layout.ListaLei);

            // Receber dados da lei selecionada
            sTipoLivro = Intent.GetStringExtra("MyData");


            if (sTipoLivro == "AT")
            {
                var TxtNumeroLei = FindViewById<TextView>(Resource.Id.textView1);
                TxtNumeroLei.Text = "ANTIGO TESTAMENTO";
                TxtNumeroLei.SetTextColor(Android.Graphics.Color.Yellow);
                ListaLivrosAT();

            }

            if (sTipoLivro == "NT")
            {
                var TxtNumeroLei = FindViewById<TextView>(Resource.Id.textView1);
                TxtNumeroLei.Text = "NOVO TESTAMENTO";
                TxtNumeroLei.SetTextColor(Android.Graphics.Color.Yellow);
                ListaLivrosNT();

            }

            //cria a instância do spinner declarado no arquivo Main
            spinner = FindViewById<Spinner>(Resource.Id.spnDados);
            //cria o adapter usando o leiaute SimpleListItem e o arraylist
            adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, itens);
            //vincula o adaptador ao controle spinner
            spinner.Adapter = adapter;

            //define o evento ItemSelected para exibir o item selecionado
            spinner.ItemSelected += Spinner_ItemSelected;


        } // On Create da apresentação

        private void Spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            string toast = string.Format("Item selecionado: {0}", spinner.GetItemAtPosition(e.Position));
            int posicao = e.Position;

            if (sTipoLivro == "AT")
            {

                if (posicao == 1)  // Livro de Gênesis
                {
                    Toast.MakeText(this, "Gênesis selecionado!", ToastLength.Long).Show();                    
                    ListaCapitulos("Gn");
                    sLivro = "Gênesis";
                }
                if (posicao == 2)  // Livro de Êxodo
                {
                    Toast.MakeText(this, "Êxodo selecionado!", ToastLength.Long).Show();
                    ListaCapitulos("Ex");
                    sLivro = "Êxodo";

                }
                if (posicao == 3)  // Livro de Levítico
                {
                    Toast.MakeText(this, "Levítico selecionado!", ToastLength.Long).Show();
                    ListaCapitulos("Lv");
                    sLivro = "Levítico";

                }
                if (posicao == 4)  // Livro de Números
                {
                    Toast.MakeText(this, "Números selecionado!", ToastLength.Long).Show();
                    ListaCapitulos("Nm");
                    sLivro = "Números";

                }
                if (posicao == 5)  // Livro de Deuteronômio
                {
                    Toast.MakeText(this, "Deuteronômio selecionado!", ToastLength.Long).Show();
                    ListaCapitulos("Dt");
                    sLivro = "Deuteronômio";

                }

                if (posicao == 6)  // Livro de Josué
                {
                    Toast.MakeText(this, "Josué selecionado!", ToastLength.Long).Show();
                    ListaCapitulos("Js");
                    sLivro = "Josué";

                }

                if (posicao == 7)  // Livro de Juízes
                {
                    Toast.MakeText(this, "Juízes selecionado!", ToastLength.Long).Show();
                    ListaCapitulos("Jz");
                    sLivro = "Juízes";

                }

                if (posicao == 8)  // Livro de Rute
                {
                    Toast.MakeText(this, "Rute selecionado!", ToastLength.Long).Show();
                    ListaCapitulos("Rt");
                    sLivro = "Rute";

                }

                if (posicao == 9)  // Livro de 1 Samuel
                {
                    Toast.MakeText(this, "1º Samuel selecionado!", ToastLength.Long).Show();
                    ListaCapitulos("1Sm");
                    sLivro = "1º Samuel";

                }

                if (posicao == 10)  // Livro de 2 Samuel
                {
                    Toast.MakeText(this, "2º Samuel selecionado!", ToastLength.Long).Show();
                    ListaCapitulos("2Sm");
                    sLivro = "2º Samuel";

                }

                if (posicao == 11)  // Livro de 1 Reis
                {
                    Toast.MakeText(this, "2º Reis selecionado!", ToastLength.Long).Show();
                    ListaCapitulos("1Rs");
                    sLivro = "1º Reis";

                }

                if (posicao == 12)  // Livro de 2 Reis
                {
                    Toast.MakeText(this, "2º Reis selecionado!", ToastLength.Long).Show();
                    ListaCapitulos("2Rs");
                    sLivro = "2º Reis";

                }

                if (posicao == 13)  // Livro de 1 Crônicas
                {
                    Toast.MakeText(this, "1º Crônicas selecionado!", ToastLength.Long).Show();
                    ListaCapitulos("1Cr");
                    sLivro = "1º Crônicas";

                }

                if (posicao == 14)  // Livro de 2 Crõnicas
                {
                    Toast.MakeText(this, "2º Crônicas selecionado!", ToastLength.Long).Show();
                    ListaCapitulos("2Cr");
                    sLivro = "2º Crônicas";

                }

                if (posicao == 15)  // Livro de Esdras
                {
                    Toast.MakeText(this, "Esdras selecionado!", ToastLength.Long).Show();
                    ListaCapitulos("Ed");
                    sLivro = "Esdras";

                }

                if (posicao == 16)  // Livro de Esdras
                {
                    Toast.MakeText(this, "Neemias selecionado!", ToastLength.Long).Show();
                    ListaCapitulos("Nm");
                    sLivro = "Neemias";

                }

                if (posicao == 17)  // Livro de Esdras
                {
                    Toast.MakeText(this, "Ester selecionado!", ToastLength.Long).Show();
                    ListaCapitulos("Et");
                    sLivro = "Ester";

                }

                if (posicao == 18)  // Livro de Esdras
                {
                    Toast.MakeText(this, "Jó selecionado!", ToastLength.Long).Show();
                    ListaCapitulos("Jó");
                    sLivro = "Jó";

                }

                if (posicao == 19)  // Livro de Salmos
                {
                    Toast.MakeText(this, "Salmos selecionado!", ToastLength.Long).Show();
                    ListaCapitulos("Sl");
                    sLivro = "Salmos";

                }

                if (posicao == 20)  // Livro de Provérbios
                {
                    Toast.MakeText(this, "Provérbios selecionado!", ToastLength.Long).Show();
                    ListaCapitulos("Pv");
                    sLivro = "Provérbios";

                }

                if (posicao == 21)  // Livro de Eclesiastes
                {
                    Toast.MakeText(this, "Eclesiastes selecionado!", ToastLength.Long).Show();
                    ListaCapitulos("Ec");
                    sLivro = "Eclesiastes";

                }

                if (posicao == 22)  // Livro de Cantares
                {
                    Toast.MakeText(this, "Cântico dos Cânticos selecionado!", ToastLength.Long).Show();
                    ListaCapitulos("Ct");
                    sLivro = "Cânticos";

                }

                if (posicao == 23)  // Livro de Isaías
                {
                    Toast.MakeText(this, "Isaías selecionado!", ToastLength.Long).Show();
                    ListaCapitulos("Is");
                    sLivro = "Isaías";

                }

                if (posicao == 24)  // Livro de Jeremeias
                {
                    Toast.MakeText(this, "Jeremias selecionado!", ToastLength.Long).Show();
                    ListaCapitulos("Jr");
                    sLivro = "Jeremias";

                }

                if (posicao == 25)  // Livro de Lamentações
                {
                    Toast.MakeText(this, "Lamentações selecionado!", ToastLength.Long).Show();
                    ListaCapitulos("Lm");
                    sLivro = "Lamentações";

                }

                if (posicao == 26)  // Livro de Esdras
                {
                    Toast.MakeText(this, "Exequiel selecionado!", ToastLength.Long).Show();
                    ListaCapitulos("Ez");
                    sLivro = "Ezequiel";

                }

                if (posicao == 27)  // Livro de Daniel
                {
                    Toast.MakeText(this, "Daniel selecionado!", ToastLength.Long).Show();
                    ListaCapitulos("Dn");
                    sLivro = "Daniel";

                }

                if (posicao == 28)  // Livro de Oséias
                {
                    Toast.MakeText(this, "Oséias selecionado!", ToastLength.Long).Show();
                    ListaCapitulos("Os");
                    sLivro = "Oséias";

                }

                if (posicao == 29)  // Livro de Joel
                {
                    Toast.MakeText(this, "Joel selecionado!", ToastLength.Long).Show();
                    ListaCapitulos("Jl");
                    sLivro = "Joel";

                }

                if (posicao == 30)  // Livro de Amós
                {
                    Toast.MakeText(this, "Amós selecionado!", ToastLength.Long).Show();
                    ListaCapitulos("Am");
                    sLivro = "Amós";

                }

                if (posicao == 31)  // Livro de Obadias
                {
                    Toast.MakeText(this, "Obadias selecionado!", ToastLength.Long).Show();
                    ListaCapitulos("Ob");
                    sLivro = "Obadias";

                }

                if (posicao == 32)  // Livro de Jonas
                {
                    Toast.MakeText(this, "Jonas selecionado!", ToastLength.Long).Show();
                    ListaCapitulos("Jn");
                    sLivro = "Jonas";

                }

                if (posicao == 33)  // Livro de Miquéias
                {
                    Toast.MakeText(this, "Miquéias selecionado!", ToastLength.Long).Show();
                    ListaCapitulos("Mq");
                    sLivro = "Miquéias";

                }

                if (posicao == 34)  // Livro de Naum
                {
                    Toast.MakeText(this, "Naum selecionado!", ToastLength.Long).Show();
                    ListaCapitulos("Na");
                    sLivro = "Naum";

                }

                if (posicao == 35)  // Livro de Habacuque
                {
                    Toast.MakeText(this, "Habacuque selecionado!", ToastLength.Long).Show();
                    ListaCapitulos("Hc");
                    sLivro = "Habacuque";

                }

                if (posicao == 36)  // Livro de Sofonias
                {
                    Toast.MakeText(this, "Sofonias selecionado!", ToastLength.Long).Show();
                    ListaCapitulos("Sf");
                    sLivro = "Sofonias";

                }

                if (posicao == 37)  // Livro de Ageu
                {
                    Toast.MakeText(this, "Ageu selecionado!", ToastLength.Long).Show();
                    ListaCapitulos("Ag");
                    sLivro = "Ageu";

                }

                if (posicao == 38)  // Livro de Zacarias
                {
                    Toast.MakeText(this, "Zacarias selecionado!", ToastLength.Long).Show();
                    ListaCapitulos("Zc");
                    sLivro = "Zacarias";

                }

                if (posicao == 39)  // Livro de Malaquias
                {
                    Toast.MakeText(this, "Malaquias selecionado!", ToastLength.Long).Show();
                    ListaCapitulos("Ml");
                    sLivro = "Malaquias";

                }

            } // Fim dos Livros de AT

            if (sTipoLivro == "NT")
            {

                if (posicao == 1)  // Livro de Mateus
                {
                    Toast.MakeText(this, "Mateus selecionado!", ToastLength.Long).Show();
                    ListaCapitulos("Mt");
                    sLivro = "Mateus";

                }
                if (posicao == 2)  // Livro de Marcos
                {
                    Toast.MakeText(this, "Marcos selecionado!", ToastLength.Long).Show();
                    ListaCapitulos("Mc");
                    sLivro = "Marcos";

                }
                if (posicao == 3)  // Livro de Lucas
                {
                    Toast.MakeText(this, "Lucas selecionado!", ToastLength.Long).Show();
                    ListaCapitulos("Lc");
                    sLivro = "Lucas";

                }

                if (posicao == 4)  // Livro de João
                {
                    Toast.MakeText(this, "João selecionado!", ToastLength.Long).Show();
                    ListaCapitulos("Jo");
                    sLivro = "João";

                }

                if (posicao == 5)  // Livro de Atos
                {
                    Toast.MakeText(this, "Atos selecionado!", ToastLength.Long).Show();
                    ListaCapitulos("At");
                    sLivro = "Atos";

                }

                if (posicao == 6)  // Livro de Romanos
                {
                    Toast.MakeText(this, "Romanos selecionado!", ToastLength.Long).Show();
                    ListaCapitulos("Rm");
                    sLivro = "Romanos";

                }

                if (posicao == 7)  // Livro de 1 Coríntios
                {
                    Toast.MakeText(this, "1ª Coríntios selecionado!", ToastLength.Long).Show();
                    ListaCapitulos("1Co");
                    sLivro = "1ª Coríntios";

                }

                if (posicao == 8)  // Livro de 2 Coríntios
                {
                    Toast.MakeText(this, "2ª Coríntios selecionado!", ToastLength.Long).Show();
                    ListaCapitulos("2Co");
                    sLivro = "2ª Coríntios";

                }

                if (posicao == 9)  // Livro de Gálatas
                {
                    Toast.MakeText(this, "Gálatas selecionado!", ToastLength.Long).Show();
                    ListaCapitulos("Gl");
                    sLivro = "Gálatas";

                }

                if (posicao == 10)  // Livro de Efésios
                {
                    Toast.MakeText(this, "Efésios  selecionado!", ToastLength.Long).Show();
                    ListaCapitulos("Ef");
                    sLivro = "Efésios";

                }

                if (posicao == 11)  // Livro de Filipenses
                {
                    Toast.MakeText(this, "Filipenses selecionado!", ToastLength.Long).Show();
                    ListaCapitulos("Fp");
                    sLivro = "Filipenses";

                }

                if (posicao == 12)  // Livro de Colossenses
                {
                    Toast.MakeText(this, "Colossenses selecionado!", ToastLength.Long).Show();
                    ListaCapitulos("Cl");
                    sLivro = "Colossenses";

                }

                if (posicao == 13)  // Livro de 1 Tessalonicenses
                {
                    Toast.MakeText(this, "1ª Tessalonicenses selecionado!", ToastLength.Long).Show();
                    ListaCapitulos("1Ts");
                    sLivro = "1ª Tessalonicenses";

                }

                if (posicao == 14)  // Livro de 2 Tessalonicenses
                {
                    Toast.MakeText(this, "2ª Tessalonicenses selecionado!", ToastLength.Long).Show();
                    ListaCapitulos("2Ts");
                    sLivro = "2ª Tessalonicenses";

                }

                if (posicao == 15)  // Livro de 1 Timóteo
                {
                    Toast.MakeText(this, "1ª Timóteo selecionado!", ToastLength.Long).Show();
                    ListaCapitulos("1Tm");
                    sLivro = "1ª Timóteo";

                }

                if (posicao == 16)  // Livro de 1 Coríntios
                {
                    Toast.MakeText(this, "2ª Timóteo selecionado!", ToastLength.Long).Show();
                    ListaCapitulos("2Tm");
                    sLivro = "2ª Timóteo";

                }

                if (posicao == 17)  // Livro de tito
                {
                    Toast.MakeText(this, "Tito selecionado!", ToastLength.Long).Show();
                    ListaCapitulos("Tt");
                    sLivro = "Tito";

                }

                if (posicao == 18)  // Livro de Filemon
                {
                    Toast.MakeText(this, "Filemon selecionado!", ToastLength.Long).Show();
                    ListaCapitulos("Fm");
                    sLivro = "Filemom";

                }

                if (posicao == 19)  // Livro de Hebreus
                {
                    Toast.MakeText(this, "Hebreus selecionado!", ToastLength.Long).Show();
                    ListaCapitulos("Hb");
                    sLivro = "Hebreus";

                }

                if (posicao == 20)  // Livro de Tiago
                {
                    Toast.MakeText(this, "Tiago selecionado!", ToastLength.Long).Show();
                    ListaCapitulos("Tg");
                    sLivro = "Tiago";

                }

                if (posicao == 21)  // Livro de 1 Pedro
                {
                    Toast.MakeText(this, "1ª Pedro selecionado!", ToastLength.Long).Show();
                    ListaCapitulos("1Pe");
                    sLivro = "1ª Pedro";

                }

                if (posicao == 22)  // Livro de 2 Pedro
                {
                    Toast.MakeText(this, "2ª Pedro selecionado!", ToastLength.Long).Show();
                    ListaCapitulos("2Pe");
                    sLivro = "2ª Pedro";

                }

                if (posicao == 23)  // Livro de 1 João
                {
                    Toast.MakeText(this, "1ª João selecionado!", ToastLength.Long).Show();
                    ListaCapitulos("1Jo");
                    sLivro = "1ª João";

                }

                if (posicao == 24)  // Livro de 2 João
                {
                    Toast.MakeText(this, "2ª João selecionado!", ToastLength.Long).Show();
                    ListaCapitulos("2Jo");
                    sLivro = "2ª João";

                }

                if (posicao == 25)  // Livro de 3 João
                {
                    Toast.MakeText(this, "3ª João selecionado!", ToastLength.Long).Show();
                    ListaCapitulos("3Jo");
                    sLivro = "3ª João";
                    
                }

                if (posicao == 26)  // Livro de Judas
                {
                    Toast.MakeText(this, "Judas selecionado!", ToastLength.Long).Show();
                    ListaCapitulos("Jd");
                    sLivro = "Judas";

                }

                if (posicao == 27)  // Livro de Revelações
                {
                    Toast.MakeText(this, "Revelações selecionado!", ToastLength.Long).Show();
                    ListaCapitulos("Rv");
                    sLivro = "Revelações";

                }

            }  // Fim dos Livros de NT



        } // Fim da seleção Inicial do Spinner 1

        // Capítulos do livro selecionado
        private void Spinner2_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            string toast = string.Format("Item selecionado: {0}", spinner.GetItemAtPosition(e.Position));
            //Toast.MakeText(this, toast, ToastLength.Long).Show();
            int posicao = e.Position;

            if ((sTipoLivro == "AT") && (posicao > 0)) // Repassa Livro e capítulo para ser mostrado ao usuário
            {
                // Toast.MakeText(this, "Mostrar capítulo de Gênesis selecionado!", ToastLength.Long).Show();
                // Abrir uma nova tela e repassar o livro e o capítulo do livro selecionados
                // exibir o livro e o capítulo selecionados                   
                sCapitulo = posicao.ToString();
                var activity2 = new Intent(this, typeof(ActivityPlanoOC));
                activity2.PutExtra("MyData", sTipoLivro);
                activity2.PutExtra("MyDataLivro", sLivro);
                activity2.PutExtra("MyDataCapitulo", sCapitulo);
                activity2.PutExtra("MyDataTotCapitulos", sNumeroVersiculos);
                
                StartActivity(activity2);                              

            }

            if ((sTipoLivro == "NT") && (posicao > 0)) // Repassa Livro e capítulo para ser mostrado ao usuário
            {
                sCapitulo = posicao.ToString();
                var activity2 = new Intent(this, typeof(ActivityPlanoOC));
                activity2.PutExtra("MyData", sTipoLivro);
                activity2.PutExtra("MyDataLivro", sLivro);
                activity2.PutExtra("MyDataCapitulo", sCapitulo);
                activity2.PutExtra("MyDataTotCapitulos", sNumeroVersiculos);

                StartActivity(activity2);
                              
            }

        } // Fim da seleção Inicial do Spinner 2

       

        private void ListaCapitulos(string livro)
        {
            capitulos = new ArrayList();
            capitulos.Clear();
            capitulos.Add("Selecione");
            
             
            if (livro == "Gn")  // Livro de Gênesis - 50 capítulos 
            {
                // Exibir os capítulos 
                var TxtCapitulo = FindViewById<TextView>(Resource.Id.textView2);
                TxtCapitulo.SetTextColor(Android.Graphics.Color.Yellow);
                TxtCapitulo.Visibility = ViewStates.Visible;
                sNumeroVersiculos = "50";

                // Gênesis tem 50 capítulos
                for (int x = 1; x <= 50; x++)
                {
                    capitulos.Add(x);

                }            

            }
            if (livro == "Ex")  // Livro de Êxodo - 40 capítulos 
            {
                // Exibir os capítulos 
                var TxtCapitulo = FindViewById<TextView>(Resource.Id.textView2);
                TxtCapitulo.SetTextColor(Android.Graphics.Color.Yellow);
                TxtCapitulo.Visibility = ViewStates.Visible;
                sNumeroVersiculos = "40";
                // Tem 40 capítulos
                for (int x = 1; x <= 40; x++)
                {
                    capitulos.Add(x);

                }

            }
            if (livro == "Lv")  // Livro de Levítico - 27 capítulos 
            {
                // Exibir os capítulos 
                var TxtCapitulo = FindViewById<TextView>(Resource.Id.textView2);
                TxtCapitulo.SetTextColor(Android.Graphics.Color.Yellow);
                TxtCapitulo.Visibility = ViewStates.Visible;
                sNumeroVersiculos = "27";
                // Tem 27 capítulos
                for (int x = 1; x <= 27; x++)
                {
                    capitulos.Add(x);

                }

            }
            if (livro == "Nm")  // Livro de Numeros - 36 capítulos 
            {
                // Exibir os capítulos 
                var TxtCapitulo = FindViewById<TextView>(Resource.Id.textView2);
                TxtCapitulo.SetTextColor(Android.Graphics.Color.Yellow);
                TxtCapitulo.Visibility = ViewStates.Visible;
                sNumeroVersiculos = "36";
                // Tem 36 capítulos
                for (int x = 1; x <= 36; x++)
                {
                    capitulos.Add(x);

                }

            }
            if (livro == "Dt")  // Livro de Deuteronômio - 35 capítulos 
            {
                // Exibir os capítulos 
                var TxtCapitulo = FindViewById<TextView>(Resource.Id.textView2);
                TxtCapitulo.SetTextColor(Android.Graphics.Color.Yellow);
                TxtCapitulo.Visibility = ViewStates.Visible;
                sNumeroVersiculos = "34";
                //  tem 35 capítulos
                for (int x = 1; x <= 34; x++)
                {
                    capitulos.Add(x);

                }

            }
            if (livro == "Js")  // Livro de Josué - 24 capítulos 
            {
                // Exibir os capítulos 
                var TxtCapitulo = FindViewById<TextView>(Resource.Id.textView2);
                TxtCapitulo.SetTextColor(Android.Graphics.Color.Yellow);
                TxtCapitulo.Visibility = ViewStates.Visible;
                sNumeroVersiculos = "24";
                // Tem 24 capítulos
                for (int x = 1; x <= 24; x++)
                {
                    capitulos.Add(x);

                }

            }

            if (livro == "Jz")  // Livro de Juízes - 22 capítulos 
            {
                // Exibir os capítulos 
                var TxtCapitulo = FindViewById<TextView>(Resource.Id.textView2);
                TxtCapitulo.SetTextColor(Android.Graphics.Color.Yellow);
                TxtCapitulo.Visibility = ViewStates.Visible;
                sNumeroVersiculos = "21";
                // Juízes tem 22 capítulos
                for (int x = 1; x <= 21; x++)
                {
                    capitulos.Add(x);

                }

            }

            if (livro == "Rt")  // Livro de Rute - 4 capítulos 
            {
                // Exibir os capítulos 
                var TxtCapitulo = FindViewById<TextView>(Resource.Id.textView2);
                TxtCapitulo.SetTextColor(Android.Graphics.Color.Yellow);
                TxtCapitulo.Visibility = ViewStates.Visible;
                sNumeroVersiculos = "4";
                // Tem 4 capítulos
                for (int x = 1; x <= 4; x++)
                {
                    capitulos.Add(x);

                }

            }

            if (livro == "1Sm")  // Livro de 1 Samuel 31 capítulos 
            {
                // Exibir os capítulos 
                var TxtCapitulo = FindViewById<TextView>(Resource.Id.textView2);
                TxtCapitulo.SetTextColor(Android.Graphics.Color.Yellow);
                TxtCapitulo.Visibility = ViewStates.Visible;
                sNumeroVersiculos = "31";
                // tem 31 capítulos
                for (int x = 1; x <= 31; x++)
                {
                    capitulos.Add(x);

                }

            }

            if (livro == "2Sm")  // Livro de 2 Samuel 24 capítulos 
            {
                // Exibir os capítulos 
                var TxtCapitulo = FindViewById<TextView>(Resource.Id.textView2);
                TxtCapitulo.SetTextColor(Android.Graphics.Color.Yellow);
                TxtCapitulo.Visibility = ViewStates.Visible;
                sNumeroVersiculos = "24";
                // tem 24 capítulos
                for (int x = 1; x <= 24; x++)
                {
                    capitulos.Add(x);

                }

            }

            if (livro == "1Rs")  // Livro de 1 Reis 22 capítulos 
            {
                // Exibir os capítulos 
                var TxtCapitulo = FindViewById<TextView>(Resource.Id.textView2);
                TxtCapitulo.SetTextColor(Android.Graphics.Color.Yellow);
                TxtCapitulo.Visibility = ViewStates.Visible;
                sNumeroVersiculos = "22";
                // tem 22 capítulos
                for (int x = 1; x <= 22; x++)
                {
                    capitulos.Add(x);

                }

            }

            if (livro == "2Rs")  // Livro de 2 Reis 25 capítulos 
            {
                // Exibir os capítulos 
                var TxtCapitulo = FindViewById<TextView>(Resource.Id.textView2);
                TxtCapitulo.SetTextColor(Android.Graphics.Color.Yellow);
                TxtCapitulo.Visibility = ViewStates.Visible;
                sNumeroVersiculos = "25";
                // tem 25 capítulos
                for (int x = 1; x <= 25; x++)
                {
                    capitulos.Add(x);

                }

            }

            if (livro == "1Cr")  // Livro de 1 Crônicas 29 capítulos 
            {
                // Exibir os capítulos 
                var TxtCapitulo = FindViewById<TextView>(Resource.Id.textView2);
                TxtCapitulo.SetTextColor(Android.Graphics.Color.Yellow);
                TxtCapitulo.Visibility = ViewStates.Visible;
                sNumeroVersiculos = "29";
                // tem 29 capítulos
                for (int x = 1; x <= 29; x++)
                {
                    capitulos.Add(x);

                }

            }

            if (livro == "2Cr")  // Livro de 2 Crônicas 29 capítulos 
            {
                // Exibir os capítulos 
                var TxtCapitulo = FindViewById<TextView>(Resource.Id.textView2);
                TxtCapitulo.SetTextColor(Android.Graphics.Color.Yellow);
                TxtCapitulo.Visibility = ViewStates.Visible;
                sNumeroVersiculos = "29";
                // tem 29 capítulos
                for (int x = 1; x <= 29; x++)
                {
                    capitulos.Add(x);

                }

            }

            if (livro == "Ed")  // Livro de Esdras 10 capítulos 
            {
                // Exibir os capítulos 
                var TxtCapitulo = FindViewById<TextView>(Resource.Id.textView2);
                TxtCapitulo.SetTextColor(Android.Graphics.Color.Yellow);
                TxtCapitulo.Visibility = ViewStates.Visible;
                sNumeroVersiculos = "10";
                // tem 10 capítulos
                for (int x = 1; x <= 10; x++)
                {
                    capitulos.Add(x);

                }

            }

            if (livro == "Ne")  // Livro de Neemias 13 capítulos 
            {
                // Exibir os capítulos 
                var TxtCapitulo = FindViewById<TextView>(Resource.Id.textView2);
                TxtCapitulo.SetTextColor(Android.Graphics.Color.Yellow);
                TxtCapitulo.Visibility = ViewStates.Visible;
                sNumeroVersiculos = "13";
                // tem 13 capítulos
                for (int x = 1; x <= 13; x++)
                {
                    capitulos.Add(x);

                }

            }

            if (livro == "Et")  // Livro de Ester 11 capítulos 
            {
                // Exibir os capítulos 
                var TxtCapitulo = FindViewById<TextView>(Resource.Id.textView2);
                TxtCapitulo.SetTextColor(Android.Graphics.Color.Yellow);
                TxtCapitulo.Visibility = ViewStates.Visible;
                sNumeroVersiculos = "10";
                // tem 11 capítulos
                for (int x = 1; x <= 10; x++)
                {
                    capitulos.Add(x);

                }

            }

            if (livro == "Jó")  // Livro de Jó 42 capítulos 
            {
                // Exibir os capítulos 
                var TxtCapitulo = FindViewById<TextView>(Resource.Id.textView2);
                TxtCapitulo.SetTextColor(Android.Graphics.Color.Yellow);
                TxtCapitulo.Visibility = ViewStates.Visible;
                sNumeroVersiculos = "42";
                // tem 42 capítulos
                for (int x = 1; x <= 42; x++)
                {
                    capitulos.Add(x);

                }

            }

            if (livro == "Sl")  // Livro de Salmos tem 150 capítulos 
            {
                // Exibir os capítulos 
                var TxtCapitulo = FindViewById<TextView>(Resource.Id.textView2);
                TxtCapitulo.SetTextColor(Android.Graphics.Color.Yellow);
                TxtCapitulo.Visibility = ViewStates.Visible;
                sNumeroVersiculos = "150";
                // tem 150 capítulos
                for (int x = 1; x <= 150; x++)
                {
                    capitulos.Add(x);

                }

            }

            if (livro == "Pv")  // Livro de Provérbios 31 capítulos 
            {
                // Exibir os capítulos 
                var TxtCapitulo = FindViewById<TextView>(Resource.Id.textView2);
                TxtCapitulo.SetTextColor(Android.Graphics.Color.Yellow);
                TxtCapitulo.Visibility = ViewStates.Visible;
                sNumeroVersiculos = "31";
                // tem 31 capítulos
                for (int x = 1; x <= 31; x++)
                {
                    capitulos.Add(x);

                }

            }

            if (livro == "Ec")  // Livro de Eclesiastes 12 capítulos 
            {
                // Exibir os capítulos 
                var TxtCapitulo = FindViewById<TextView>(Resource.Id.textView2);
                TxtCapitulo.SetTextColor(Android.Graphics.Color.Yellow);
                TxtCapitulo.Visibility = ViewStates.Visible;
                sNumeroVersiculos = "12";
                // tem 12 capítulos
                for (int x = 1; x <= 12; x++)
                {
                    capitulos.Add(x);

                }

            }

            if (livro == "Ct")  // Livro de Cantares 8 capítulos 
            {
                // Exibir os capítulos 
                var TxtCapitulo = FindViewById<TextView>(Resource.Id.textView2);
                TxtCapitulo.SetTextColor(Android.Graphics.Color.Yellow);
                TxtCapitulo.Visibility = ViewStates.Visible;
                sNumeroVersiculos = "8";
                // tem 8 capítulos
                for (int x = 1; x <= 8; x++)
                {
                    capitulos.Add(x);

                }

            }

            if (livro == "Is")  // Livro de Isaías 66 capítulos 
            {
                // Exibir os capítulos 
                var TxtCapitulo = FindViewById<TextView>(Resource.Id.textView2);
                TxtCapitulo.SetTextColor(Android.Graphics.Color.Yellow);
                TxtCapitulo.Visibility = ViewStates.Visible;
                sNumeroVersiculos = "66";
                // tem 66 capítulos
                for (int x = 1; x <= 66; x++)
                {
                    capitulos.Add(x);

                }

            }

            if (livro == "Jr")  // Livro de Jeremias 52 capítulos 
            {
                // Exibir os capítulos 
                var TxtCapitulo = FindViewById<TextView>(Resource.Id.textView2);
                TxtCapitulo.SetTextColor(Android.Graphics.Color.Yellow);
                TxtCapitulo.Visibility = ViewStates.Visible;
                sNumeroVersiculos = "52";
                // tem 52 capítulos
                for (int x = 1; x <= 52; x++)
                {
                    capitulos.Add(x);

                }

            }

            if (livro == "Lm")  // Livro de Lamentações 5 capítulos 
            {
                // Exibir os capítulos 
                var TxtCapitulo = FindViewById<TextView>(Resource.Id.textView2);
                TxtCapitulo.SetTextColor(Android.Graphics.Color.Yellow);
                TxtCapitulo.Visibility = ViewStates.Visible;
                sNumeroVersiculos = "5";
                // tem 5 capítulos
                for (int x = 1; x <= 5; x++)
                {
                    capitulos.Add(x);

                }

            }

            if (livro == "Ez")  // Livro de Ezequiel 48 capítulos 
            {
                // Exibir os capítulos 
                var TxtCapitulo = FindViewById<TextView>(Resource.Id.textView2);
                TxtCapitulo.SetTextColor(Android.Graphics.Color.Yellow);
                TxtCapitulo.Visibility = ViewStates.Visible;
                sNumeroVersiculos = "48";
                // tem 48 capítulos
                for (int x = 1; x <= 48; x++)
                {
                    capitulos.Add(x);

                }

            }

            if (livro == "Dn")  // Livro de Daniel 12 capítulos 
            {
                // Exibir os capítulos 
                var TxtCapitulo = FindViewById<TextView>(Resource.Id.textView2);
                TxtCapitulo.SetTextColor(Android.Graphics.Color.Yellow);
                TxtCapitulo.Visibility = ViewStates.Visible;
                sNumeroVersiculos = "12";
                // tem 12 capítulos
                for (int x = 1; x <= 12; x++)
                {
                    capitulos.Add(x);

                }

            }

            if (livro == "Os")  // Livro de Oséias 14 capítulos 
            {
                // Exibir os capítulos 
                var TxtCapitulo = FindViewById<TextView>(Resource.Id.textView2);
                TxtCapitulo.SetTextColor(Android.Graphics.Color.Yellow);
                TxtCapitulo.Visibility = ViewStates.Visible;
                sNumeroVersiculos = "14";
                // tem 14 capítulos
                for (int x = 1; x <= 14; x++)
                {
                    capitulos.Add(x);

                }

            }

            if (livro == "Jl")  // Livro de Joel 3 capítulos 
            {
                // Exibir os capítulos 
                var TxtCapitulo = FindViewById<TextView>(Resource.Id.textView2);
                TxtCapitulo.SetTextColor(Android.Graphics.Color.Yellow);
                TxtCapitulo.Visibility = ViewStates.Visible;
                sNumeroVersiculos = "3";
                // tem 3 capítulos
                for (int x = 1; x <= 3; x++)
                {
                    capitulos.Add(x);

                }

            }

            if (livro == "Am")  // Livro de Amós 9 capítulos 
            {
                // Exibir os capítulos 
                var TxtCapitulo = FindViewById<TextView>(Resource.Id.textView2);
                TxtCapitulo.SetTextColor(Android.Graphics.Color.Yellow);
                TxtCapitulo.Visibility = ViewStates.Visible;
                sNumeroVersiculos = "9";
                // tem 9 capítulos
                for (int x = 1; x <= 9; x++)
                {
                    capitulos.Add(x);

                }

            }

            if (livro == "Ob")  // Livro de Obadias 1 capítulo 
            {
                // Exibir os capítulos 
                var TxtCapitulo = FindViewById<TextView>(Resource.Id.textView2);
                TxtCapitulo.SetTextColor(Android.Graphics.Color.Yellow);
                TxtCapitulo.Visibility = ViewStates.Visible;
                sNumeroVersiculos = "1";
                // tem 1 capítulo
                for (int x = 1; x <= 1; x++)
                {
                    capitulos.Add(x);

                }

            }

            if (livro == "Jn")  // Livro de Jonas 4 capítulos 
            {
                // Exibir os capítulos 
                var TxtCapitulo = FindViewById<TextView>(Resource.Id.textView2);
                TxtCapitulo.SetTextColor(Android.Graphics.Color.Yellow);
                TxtCapitulo.Visibility = ViewStates.Visible;
                sNumeroVersiculos = "4";
                // tem 4 capítulos
                for (int x = 1; x <= 4; x++)
                {
                    capitulos.Add(x);

                }

            }

            if (livro == "Mq")  // Livro de Miquéias 7 capítulos 
            {
                // Exibir os capítulos 
                var TxtCapitulo = FindViewById<TextView>(Resource.Id.textView2);
                TxtCapitulo.SetTextColor(Android.Graphics.Color.Yellow);
                TxtCapitulo.Visibility = ViewStates.Visible;
                sNumeroVersiculos = "7";
                // tem 7 capítulos
                for (int x = 1; x <= 7; x++)
                {
                    capitulos.Add(x);

                }

            }

            if (livro == "Na")  // Livro de Naum 3 capítulos 
            {
                // Exibir os capítulos 
                var TxtCapitulo = FindViewById<TextView>(Resource.Id.textView2);
                TxtCapitulo.SetTextColor(Android.Graphics.Color.Yellow);
                TxtCapitulo.Visibility = ViewStates.Visible;
                sNumeroVersiculos = "3";
                // tem 3 capítulos
                for (int x = 1; x <= 3; x++)
                {
                    capitulos.Add(x);

                }

            }

            if (livro == "Hc")  // Livro de Habacuque 3 capítulos 
            {
                // Exibir os capítulos 
                var TxtCapitulo = FindViewById<TextView>(Resource.Id.textView2);
                TxtCapitulo.SetTextColor(Android.Graphics.Color.Yellow);
                TxtCapitulo.Visibility = ViewStates.Visible;
                sNumeroVersiculos = "3";
                // tem 3 capítulos
                for (int x = 1; x <= 3; x++)
                {
                    capitulos.Add(x);

                }

            }

            if (livro == "Sf")  // Livro de Sofonias 3 capítulos 
            {
                // Exibir os capítulos 
                var TxtCapitulo = FindViewById<TextView>(Resource.Id.textView2);
                TxtCapitulo.SetTextColor(Android.Graphics.Color.Yellow);
                TxtCapitulo.Visibility = ViewStates.Visible;
                sNumeroVersiculos = "3";
                // tem 3 capítulos
                for (int x = 1; x <= 3; x++)
                {
                    capitulos.Add(x);

                }

            }

            if (livro == "Ag")  // Livro de Ageu 2 capítulos 
            {
                // Exibir os capítulos 
                var TxtCapitulo = FindViewById<TextView>(Resource.Id.textView2);
                TxtCapitulo.SetTextColor(Android.Graphics.Color.Yellow);
                TxtCapitulo.Visibility = ViewStates.Visible;
                sNumeroVersiculos = "2";
                // tem 2 capítulos
                for (int x = 1; x <= 2; x++)
                {
                    capitulos.Add(x);

                }

            }

            if (livro == "Zc")  // Livro de Zacarias 14 capítulos 
            {
                // Exibir os capítulos 
                var TxtCapitulo = FindViewById<TextView>(Resource.Id.textView2);
                TxtCapitulo.SetTextColor(Android.Graphics.Color.Yellow);
                TxtCapitulo.Visibility = ViewStates.Visible;
                sNumeroVersiculos = "14";
                // tem 14 capítulos
                for (int x = 1; x <= 14; x++)
                {
                    capitulos.Add(x);

                }

            }

            if (livro == "Ml")  // Livro de Malaquias 4 capítulos 
            {
                // Exibir os capítulos 
                var TxtCapitulo = FindViewById<TextView>(Resource.Id.textView2);
                TxtCapitulo.SetTextColor(Android.Graphics.Color.Yellow);
                TxtCapitulo.Visibility = ViewStates.Visible;
                sNumeroVersiculos = "4";
                // tem 4 capítulos
                for (int x = 1; x <= 4; x++)
                {
                    capitulos.Add(x);

                }

            }
            // FIM DO ANTIGO TESTAMENTO - NT

            // NOVO TESTAMENTO - NT
            if (livro == "Mt")  // Livro de Matues - 28 capítulos 
            {
                // Exibir os capítulos 
                var TxtCapitulo = FindViewById<TextView>(Resource.Id.textView2);
                TxtCapitulo.SetTextColor(Android.Graphics.Color.Yellow);
                TxtCapitulo.Visibility = ViewStates.Visible;
                sNumeroVersiculos = "28";
                // Mateus tem 28 capítulos
                for (int x = 1; x <= 28; x++)
                {
                    capitulos.Add(x);

                }

            }
            if (livro == "Mc")  // Livro de MaRCOS - 16 capítulos 
            {
                // Exibir os capítulos 
                var TxtCapitulo = FindViewById<TextView>(Resource.Id.textView2);
                TxtCapitulo.SetTextColor(Android.Graphics.Color.Yellow);
                TxtCapitulo.Visibility = ViewStates.Visible;
                sNumeroVersiculos = "16";
                // Matrcos tem 16 capítulos
                for (int x = 1; x <= 16; x++)
                {
                    capitulos.Add(x);

                }

            }
            if (livro == "Lc")  // Livro de Lucas - 24 capítulos 
            {
                // Exibir os capítulos 
                var TxtCapitulo = FindViewById<TextView>(Resource.Id.textView2);
                TxtCapitulo.SetTextColor(Android.Graphics.Color.Yellow);
                TxtCapitulo.Visibility = ViewStates.Visible;
                sNumeroVersiculos = "24";
                // Lucas tem 24 capítulos
                for (int x = 1; x <= 24; x++)
                {
                    capitulos.Add(x);

                }

            }

            if (livro == "Jo")  // Livro de João - 21 capítulos 
            {
                // Exibir os capítulos 
                var TxtCapitulo = FindViewById<TextView>(Resource.Id.textView2);
                TxtCapitulo.SetTextColor(Android.Graphics.Color.Yellow);
                TxtCapitulo.Visibility = ViewStates.Visible;
                sNumeroVersiculos = "21";
                // João tem 21 capítulos
                for (int x = 1; x <= 21; x++)
                {
                    capitulos.Add(x);

                }

            }

            if (livro == "At")  // Livro de Atos 28 capítulos 
            {
                // Exibir os capítulos 
                var TxtCapitulo = FindViewById<TextView>(Resource.Id.textView2);
                TxtCapitulo.SetTextColor(Android.Graphics.Color.Yellow);
                TxtCapitulo.Visibility = ViewStates.Visible;
                sNumeroVersiculos = "28";
                // tem 28 capítulos
                for (int x = 1; x <= 28; x++)
                {
                    capitulos.Add(x);

                }

            }

            if (livro == "Rm")  // Livro de Atos 28 capítulos 
            {
                // Exibir os capítulos 
                var TxtCapitulo = FindViewById<TextView>(Resource.Id.textView2);
                TxtCapitulo.SetTextColor(Android.Graphics.Color.Yellow);
                TxtCapitulo.Visibility = ViewStates.Visible;
                sNumeroVersiculos = "16";
                // tem 16 capítulos
                for (int x = 1; x <= 16; x++)
                {
                    capitulos.Add(x);

                }

            }

            if (livro == "1Co")  // Livro de 1 Coríntios 16 capítulos 
            {
                // Exibir os capítulos 
                var TxtCapitulo = FindViewById<TextView>(Resource.Id.textView2);
                TxtCapitulo.SetTextColor(Android.Graphics.Color.Yellow);
                TxtCapitulo.Visibility = ViewStates.Visible;
                sNumeroVersiculos = "16";
                // tem 16 capítulos
                for (int x = 1; x <= 16; x++)
                {
                    capitulos.Add(x);

                }

            }

            if (livro == "2Co")  // Livro de 2 Coríntios 13 capítulos 
            {
                // Exibir os capítulos 
                var TxtCapitulo = FindViewById<TextView>(Resource.Id.textView2);
                TxtCapitulo.SetTextColor(Android.Graphics.Color.Yellow);
                TxtCapitulo.Visibility = ViewStates.Visible;
                sNumeroVersiculos = "13";
                // tem 13 capítulos
                for (int x = 1; x <= 13; x++)
                {
                    capitulos.Add(x);

                }

            }

            if (livro == "Gl")  // Livro de Gálatas 6 capítulos 
            {
                // Exibir os capítulos 
                var TxtCapitulo = FindViewById<TextView>(Resource.Id.textView2);
                TxtCapitulo.SetTextColor(Android.Graphics.Color.Yellow);
                TxtCapitulo.Visibility = ViewStates.Visible;
                sNumeroVersiculos = "6";
                // tem 6 capítulos
                for (int x = 1; x <= 6; x++)
                {
                    capitulos.Add(x);

                }

            }

            if (livro == "Ef")  // Livro de Efésios 6 capítulos 
            {
                // Exibir os capítulos 
                var TxtCapitulo = FindViewById<TextView>(Resource.Id.textView2);
                TxtCapitulo.SetTextColor(Android.Graphics.Color.Yellow);
                TxtCapitulo.Visibility = ViewStates.Visible;
                sNumeroVersiculos = "6";
                // tem 6 capítulos
                for (int x = 1; x <= 6; x++)
                {
                    capitulos.Add(x);

                }

            }

            if (livro == "Fp")  // Livro de Filipenses 4 capítulos 
            {
                // Exibir os capítulos 
                var TxtCapitulo = FindViewById<TextView>(Resource.Id.textView2);
                TxtCapitulo.SetTextColor(Android.Graphics.Color.Yellow);
                TxtCapitulo.Visibility = ViewStates.Visible;
                sNumeroVersiculos = "4";
                // tem 4 capítulos
                for (int x = 1; x <= 4; x++)
                {
                    capitulos.Add(x);

                }

            }

            if (livro == "Cl")  // Livro de Colossenses 4 capítulos 
            {
                // Exibir os capítulos 
                var TxtCapitulo = FindViewById<TextView>(Resource.Id.textView2);
                TxtCapitulo.SetTextColor(Android.Graphics.Color.Yellow);
                TxtCapitulo.Visibility = ViewStates.Visible;
                sNumeroVersiculos = "4";
                // tem 4 capítulos
                for (int x = 1; x <= 4; x++)
                {
                    capitulos.Add(x);

                }

            }

            if (livro == "1Ts")  // Livro de 1 Tessalonicenses 5 capítulos 
            {
                // Exibir os capítulos 
                var TxtCapitulo = FindViewById<TextView>(Resource.Id.textView2);
                TxtCapitulo.SetTextColor(Android.Graphics.Color.Yellow);
                TxtCapitulo.Visibility = ViewStates.Visible;
                sNumeroVersiculos = "5";
                // tem 5 capítulos
                for (int x = 1; x <= 5; x++)
                {
                    capitulos.Add(x);

                }

            }

            if (livro == "2Ts")  // Livro de 2 Tessalonicenses 3 capítulos 
            {
                // Exibir os capítulos 
                var TxtCapitulo = FindViewById<TextView>(Resource.Id.textView2);
                TxtCapitulo.SetTextColor(Android.Graphics.Color.Yellow);
                TxtCapitulo.Visibility = ViewStates.Visible;
                sNumeroVersiculos = "3";
                // tem 3 capítulos
                for (int x = 1; x <= 3; x++)
                {
                    capitulos.Add(x);

                }

            }

            if (livro == "1Tm")  // Livro de 1 Timóteo 6 capítulos 
            {
                // Exibir os capítulos 
                var TxtCapitulo = FindViewById<TextView>(Resource.Id.textView2);
                TxtCapitulo.SetTextColor(Android.Graphics.Color.Yellow);
                TxtCapitulo.Visibility = ViewStates.Visible;
                sNumeroVersiculos = "6";
                // tem 6 capítulos
                for (int x = 1; x <= 6; x++)
                {
                    capitulos.Add(x);

                }

            }

            if (livro == "2Tm")  // Livro de 2 Timóteo 4 capítulos 
            {
                // Exibir os capítulos 
                var TxtCapitulo = FindViewById<TextView>(Resource.Id.textView2);
                TxtCapitulo.SetTextColor(Android.Graphics.Color.Yellow);
                TxtCapitulo.Visibility = ViewStates.Visible;
                sNumeroVersiculos = "4";
                // tem 4 capítulos
                for (int x = 1; x <= 4; x++)
                {
                    capitulos.Add(x);

                }

            }

            if (livro == "Tt")  // Livro de Tito 3 capítulos 
            {
                // Exibir os capítulos 
                var TxtCapitulo = FindViewById<TextView>(Resource.Id.textView2);
                TxtCapitulo.SetTextColor(Android.Graphics.Color.Yellow);
                TxtCapitulo.Visibility = ViewStates.Visible;
                sNumeroVersiculos = "3";
                // tem 3 capítulos
                for (int x = 1; x <= 3; x++)
                {
                    capitulos.Add(x);

                }

            }

            if (livro == "Fm")  // Livro de Filemon 1 capítulo 
            {
                // Exibir os capítulos 
                var TxtCapitulo = FindViewById<TextView>(Resource.Id.textView2);
                TxtCapitulo.SetTextColor(Android.Graphics.Color.Yellow);
                TxtCapitulo.Visibility = ViewStates.Visible;
                sNumeroVersiculos = "1";
                // tem 1 capítulo
                for (int x = 1; x <= 1; x++)
                {
                    capitulos.Add(x);

                }

            }

            if (livro == "Hb")  // Livro de Hebreus 13 capítulos 
            {
                // Exibir os capítulos 
                var TxtCapitulo = FindViewById<TextView>(Resource.Id.textView2);
                TxtCapitulo.SetTextColor(Android.Graphics.Color.Yellow);
                TxtCapitulo.Visibility = ViewStates.Visible;
                sNumeroVersiculos = "13";
                // tem 13 capítulos
                for (int x = 1; x <= 13; x++)
                {
                    capitulos.Add(x);

                }

            }

            if (livro == "Tg")  // Livro de Tiago 5 capítulos 
            {
                // Exibir os capítulos 
                var TxtCapitulo = FindViewById<TextView>(Resource.Id.textView2);
                TxtCapitulo.SetTextColor(Android.Graphics.Color.Yellow);
                TxtCapitulo.Visibility = ViewStates.Visible;
                sNumeroVersiculos = "5";
                // tem 5 capítulos
                for (int x = 1; x <= 5; x++)
                {
                    capitulos.Add(x);

                }

            }

            if (livro == "1Pe")  // Livro de 1 Pedro 5 capítulos 
            {
                // Exibir os capítulos 
                var TxtCapitulo = FindViewById<TextView>(Resource.Id.textView2);
                TxtCapitulo.SetTextColor(Android.Graphics.Color.Yellow);
                TxtCapitulo.Visibility = ViewStates.Visible;
                sNumeroVersiculos = "5";
                // tem 5 capítulos
                for (int x = 1; x <= 5; x++)
                {
                    capitulos.Add(x);

                }

            }

            if (livro == "2Pe")  // Livro de 2 Pedro 3 capítulos 
            {
                // Exibir os capítulos 
                var TxtCapitulo = FindViewById<TextView>(Resource.Id.textView2);
                TxtCapitulo.SetTextColor(Android.Graphics.Color.Yellow);
                TxtCapitulo.Visibility = ViewStates.Visible;
                sNumeroVersiculos = "3";
                // tem 3 capítulos
                for (int x = 1; x <= 3; x++)
                {
                    capitulos.Add(x);

                }

            }

            if (livro == "1Jo")  // Livro de 1 João 5 capítulos 
            {
                // Exibir os capítulos 
                var TxtCapitulo = FindViewById<TextView>(Resource.Id.textView2);
                TxtCapitulo.SetTextColor(Android.Graphics.Color.Yellow);
                TxtCapitulo.Visibility = ViewStates.Visible;
                sNumeroVersiculos = "5";
                // tem 5 capítulos
                for (int x = 1; x <= 5; x++)
                {
                    capitulos.Add(x);

                }

            }

            if (livro == "2Jo")  // Livro de 2 João 1 capítulo
            {
                // Exibir os capítulos 
                var TxtCapitulo = FindViewById<TextView>(Resource.Id.textView2);
                TxtCapitulo.SetTextColor(Android.Graphics.Color.Yellow);
                TxtCapitulo.Visibility = ViewStates.Visible;
                sNumeroVersiculos = "1";
                // tem 1 capítulo
                for (int x = 1; x <= 1; x++)
                {
                    capitulos.Add(x);

                }

            }

            if (livro == "3Jo")  // Livro de 3 João 1 capítulo
            {
                // Exibir os capítulos 
                var TxtCapitulo = FindViewById<TextView>(Resource.Id.textView2);
                TxtCapitulo.SetTextColor(Android.Graphics.Color.Yellow);
                TxtCapitulo.Visibility = ViewStates.Visible;
                sNumeroVersiculos = "1";
                // tem 1 capítulo
                for (int x = 1; x <= 1; x++)
                {
                    capitulos.Add(x);

                }

            }


            if (livro == "Jd")  // Livro de 3 João 1 capítulo
            {
                // Exibir os capítulos 
                var TxtCapitulo = FindViewById<TextView>(Resource.Id.textView2);
                TxtCapitulo.SetTextColor(Android.Graphics.Color.Yellow);
                TxtCapitulo.Visibility = ViewStates.Visible;
                sNumeroVersiculos = "1";
                // tem 1 capítulo
                for (int x = 1; x <= 1; x++)
                {
                    capitulos.Add(x);

                }

            }

            if (livro == "Rv")  // Livro de Revelações 22 capítulo
            {
                // Exibir os capítulos 
                var TxtCapitulo = FindViewById<TextView>(Resource.Id.textView2);
                TxtCapitulo.SetTextColor(Android.Graphics.Color.Yellow);
                TxtCapitulo.Visibility = ViewStates.Visible;
                sNumeroVersiculos = "22";
                // tem 22 capítulo
                for (int x = 1; x <= 22; x++)
                {
                    capitulos.Add(x);

                }

            }

            // FIM DOS LIVROS DO NOVO TESTAMENTO - NT

            //cria a instância do spinner declarado no arquivo Main
            spinner2 = FindViewById<Spinner>(Resource.Id.spnDados2);
            //cria o adapter usando o leiaute SimpleListItem e o arraylist
            adapter2 = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, capitulos);
            //vincula o adaptador ao controle spinner
            spinner2.Adapter = adapter2;

            spinner2.Visibility = ViewStates.Visible;

            //define o evento ItemSelected para exibir o item selecionado
            spinner2.ItemSelected += Spinner2_ItemSelected;


        }

        private void ListaLivrosAT()
        {
            itens = new ArrayList();
            
            itens.Clear();


            itens.Add("Selecione");
            itens.Add("Gênesis");
            itens.Add("Êxodo");
            itens.Add("Levítico");
            itens.Add("Números");
            itens.Add("Deuteronômio");
            itens.Add("Josué");
            itens.Add("Juízes");
            itens.Add("Rute");
            itens.Add("1º Samuel");
            itens.Add("2º Samuel");
            itens.Add("1º Reis");
            itens.Add("2º Reis");
            itens.Add("1º Crônicas");
            itens.Add("2º Crônicas");
            itens.Add("Esdras");
            itens.Add("Neemias");
            itens.Add("Ester");
            itens.Add("Jó");
            itens.Add("Salmos");
            itens.Add("Provérbios");
            itens.Add("Eclesiastes");
            itens.Add("Cântico dos Cânticos");
            itens.Add("Isaías");
            itens.Add("Jeremias");
            itens.Add("Lamentações");
            itens.Add("Ezequiel");
            itens.Add("Daniel");
            itens.Add("Oséias");
            itens.Add("Joel");
            itens.Add("Amós");
            itens.Add("Obadias");
            itens.Add("Jonas");
            itens.Add("Miquéias");
            itens.Add("Naum");
            itens.Add("Habacuque");
            itens.Add("Sofonias");
            itens.Add("Ageu");
            itens.Add("Zacarias");
            itens.Add("Malaquias");


        } // Função de apoio AT 

        private void ListaLivrosNT()
        {
            itens = new ArrayList();
            
            itens.Clear();

            itens.Add("Selecione");
            itens.Add("Mateus");
            itens.Add("Marcos");
            itens.Add("Lucas");
            itens.Add("João");
            itens.Add("Atos");
            itens.Add("Romanos");
            itens.Add("1ª Coríntios");
            itens.Add("2ª Coríntios");
            itens.Add("Gálatas");
            itens.Add("Efésios");
            itens.Add("Filipenses");
            itens.Add("Colossenses");
            itens.Add("1ª Tessalonicenses");
            itens.Add("2ª Tessalonicenses");
            itens.Add("1ª Timóteo");
            itens.Add("2ª Timóteo");
            itens.Add("Tito");
            itens.Add("Filemom");
            itens.Add("Hebreus");
            itens.Add("Tiago");
            itens.Add("1ª Pedro");
            itens.Add("2ª Pedro");
            itens.Add("1ª João");
            itens.Add("2ª João");
            itens.Add("3ª João");
            itens.Add("Judas");
            itens.Add("Revelações");

        } // Função de apoio AT 

    }
}