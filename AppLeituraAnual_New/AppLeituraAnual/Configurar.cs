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


namespace AppBibliaNVI
{
    [Activity(Label = "Configurar")]
    public class Configurar : Activity
    {

        string sTipoLivro = "";
        string sNomeLivro = "";
        string sCapitulo = "";
        string sDesc = "";
        string sVersiculo = "";
        string TagName = "";
        string sTipoLei = "";
        string sTextoVersiculo = "";
        string sNumeroVersiculo = "";
        public ArrayList listaSalva;
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

        public Button BotaoPesquisa;


        bool eTelaBusca = false;
        public ArrayList lista;
        public ArrayList lista_ref;
        public ArrayAdapter adp1;
        public ListView lv1;
        SearchView sBusca;
        Button BotaoLimpar;

        string sTextoPesq = "";

        bool SelecaoRef = false;
        bool SelecaoText = false;

        string sLivroBiblia = "";
        string sCapituloLivroBiblia = "";
        string sVersiculoCapituloLivroBiblia = "";
        string sTextoVersiculoCapituloLivroBiblia = "";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Configurar);

            var TxtTitulo = FindViewById<TextView>(Resource.Id.TextoUM);
            TxtTitulo.SetTextColor(Android.Graphics.Color.Yellow);

            var TxtPesquisa1 = FindViewById<TextView>(Resource.Id.TxtDOIS);
            TxtPesquisa1.SetTextColor(Android.Graphics.Color.Gold);

            var TxtPesquisa2 = FindViewById<TextView>(Resource.Id.TxtTRES);
            TxtPesquisa2.SetTextColor(Android.Graphics.Color.Gold);

            //BotaoPesquisa = (Button)FindViewById(Resource.Id.BtnPesquisa);

            RadioButton Livro = FindViewById<RadioButton>(Resource.Id.rdLivro);
            RadioButton Referencia = FindViewById<RadioButton>(Resource.Id.rdReferencia);
            //RadioButton Texto = FindViewById<RadioButton>(Resource.Id.rdTexto);
            
            BotaoLimpar = (Button)FindViewById(Resource.Id.btnLimpar);

            Livro.SetTextColor(Android.Graphics.Color.Gold);
            Referencia.SetTextColor(Android.Graphics.Color.Gold);

            Livro.Click += RdLivroClick;
            Referencia.Click += RdReferenciaClick;

            // ABRIR XML DA BIBLIA
            // PESQUISAR LIVRO E REFERENCIA DO TEXTO
            // MOSTRAR AO USUÁRIO
            lista = new ArrayList();

            // 07/06/2021 17:24h
            // Lista criada para as pesquisas por referência que não devem ser deletadas
            lista_ref = new ArrayList();

            // Limpar a pesquisa
            // 12/10/2021 13:00h
            // 
            BotaoLimpar.Click += (sender, e) =>
            {

                string path2 = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                string filename2 = System.IO.Path.Combine(path2, "pesquisa.txt");
                if (System.IO.File.Exists(filename2))
                {
                    
                    // Deletar o arquivo antigo
                    System.IO.File.Delete(filename2);

                    
                    Android.Widget.Toast.MakeText(this, "Lista de Pesquisa excluída com sucesso!", Android.Widget.ToastLength.Short).Show();
                }

            };

        }
                
        private void RdReferenciaClick(object sender, EventArgs e)
        {
            SelecaoRef = true;

            RadioButton Livro = FindViewById<RadioButton>(Resource.Id.rdLivro);
            RadioButton Referencia = FindViewById<RadioButton>(Resource.Id.rdReferencia);
            //RadioButton Texto = FindViewById<RadioButton>(Resource.Id.rdTexto);
            
            Livro.SetTextColor(Android.Graphics.Color.Gold);
            Referencia.SetTextColor(Android.Graphics.Color.Gold);

            Livro.Checked = false;
            Referencia.Checked = true;
            //Texto.Checked = false;
            
            // 12/04/2021 12:40h MUDAR DAQUI PRA BAIXO
            // DEFINIR PESQUISA POR REFERÊNCIA

            sBusca = FindViewById<SearchView>(Resource.Id.svw1);
            sBusca.QueryTextChange += sBusca_QueryTextChange;
            
            var text = sBusca.Query;
            sTextoPesq = text;

            lv1 = FindViewById<ListView>(Resource.Id.lvw1);                      
            PesquisarReferencia(sTextoPesq);

            // 07/06/2021 18:32h
            // Pega o arquivo-texto criado, abre-o e lê o seu conteúdo
            listaSalva = new ArrayList();

            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string filename = System.IO.Path.Combine(path, "pesquisa.txt");
            var list = new List<string>();

            // 07/06/2021 18:33h Só fazer se o arquivo realmente existir
            if (System.IO.File.Exists(filename))
            {
                using (var streamReader = new StreamReader(filename))
                {
                    string line;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        list.Add(line);
                    }
                }

            }

            // Limpar a pesquisa
            sTextoPesq = "";
            
            // Apresentar a Lei ao Usuário
            adp1 = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, list);
            lv1.Adapter = adp1;            

            lv1.ItemClick += Lv1_ItemClick;

        }
        private void RdLivroClick(object sender, EventArgs e)
        {

            RadioButton Livro = FindViewById<RadioButton>(Resource.Id.rdLivro);
            RadioButton Referencia = FindViewById<RadioButton>(Resource.Id.rdReferencia);
            //RadioButton Texto = FindViewById<RadioButton>(Resource.Id.rdTexto);

            Livro.Checked = true;
            Referencia.Checked = false;
            //Texto.Checked = false;

            sBusca = FindViewById<SearchView>(Resource.Id.svw1);
            sBusca.QueryTextChange += sBusca_QueryTextChange;

            // 15/12/2018 14:32h Alterado a cor programaticamente
            //sBusca.SetBackgroundColor(Color.White);

            lv1 = FindViewById<ListView>(Resource.Id.lvw1);
            AdicionarDados();

            adp1 = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, lista);
            lv1.Adapter = adp1;            
            lv1.ItemClick += Lv1_ItemClick;

        }

        private void sBusca_QueryTextChange(object sender, SearchView.QueryTextChangeEventArgs e)
        {
            adp1.Filter.InvokeFilter(e.NewText);
            
        }

        private void Lv1_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            //Ativa a seleção da busca realizada
            string sSelecao = adp1.GetItem(e.Position).ToString();

            if (sSelecao == "Gênesis")
            {
                sTipoLivro = "AT";
                sNomeLivro = sSelecao;
                sCapitulo = "1";
                string sNumeroCapitulos = "50";
                var activity2 = new Intent(this, typeof(ActivityPlanoOC));
                activity2.PutExtra("MyData", sTipoLivro);
                activity2.PutExtra("MyDataLivro", sNomeLivro);
                activity2.PutExtra("MyDataCapitulo", sCapitulo);
                activity2.PutExtra("MyDataTotCapitulos", sNumeroCapitulos);

                //Abrir a tela 
                StartActivity(activity2);
            }

            if (sSelecao == "Êxodo")
            {
                sTipoLivro = "AT";
                sNomeLivro = sSelecao;
                sCapitulo = "1";
                string sNumeroCapitulos = "40";
                var activity2 = new Intent(this, typeof(ActivityPlanoOC));
                activity2.PutExtra("MyData", sTipoLivro);
                activity2.PutExtra("MyDataLivro", sNomeLivro);
                activity2.PutExtra("MyDataCapitulo", sCapitulo);
                activity2.PutExtra("MyDataTotCapitulos", sNumeroCapitulos);

                //Abrir a tela 
                StartActivity(activity2);
            }

            if (sSelecao == "Levítico")
            {
                sTipoLivro = "AT";
                sNomeLivro = sSelecao;
                sCapitulo = "1";
                string sNumeroCapitulos = "27";
                var activity2 = new Intent(this, typeof(ActivityPlanoOC));
                activity2.PutExtra("MyData", sTipoLivro);
                activity2.PutExtra("MyDataLivro", sNomeLivro);
                activity2.PutExtra("MyDataCapitulo", sCapitulo);
                activity2.PutExtra("MyDataTotCapitulos", sNumeroCapitulos);

                //Abrir a tela 
                StartActivity(activity2);
            }

            if (sSelecao == "Números")
            {
                sTipoLivro = "AT";
                sNomeLivro = sSelecao;
                sCapitulo = "1";
                string sNumeroCapitulos = "36";
                var activity2 = new Intent(this, typeof(ActivityPlanoOC));
                activity2.PutExtra("MyData", sTipoLivro);
                activity2.PutExtra("MyDataLivro", sNomeLivro);
                activity2.PutExtra("MyDataCapitulo", sCapitulo);
                activity2.PutExtra("MyDataTotCapitulos", sNumeroCapitulos);

                //Abrir a tela 
                StartActivity(activity2);
            }

            if (sSelecao == "Deuteronômio")
            {
                sTipoLivro = "AT";
                sNomeLivro = sSelecao;
                sCapitulo = "1";
                string sNumeroCapitulos = "34";
                var activity2 = new Intent(this, typeof(ActivityPlanoOC));
                activity2.PutExtra("MyData", sTipoLivro);
                activity2.PutExtra("MyDataLivro", sNomeLivro);
                activity2.PutExtra("MyDataCapitulo", sCapitulo);
                activity2.PutExtra("MyDataTotCapitulos", sNumeroCapitulos);

                //Abrir a tela 
                StartActivity(activity2);
            }

            if (sSelecao == "Josué")
            {
                sTipoLivro = "AT";
                sNomeLivro = sSelecao;
                sCapitulo = "1";
                string sNumeroCapitulos = "24";
                var activity2 = new Intent(this, typeof(ActivityPlanoOC));
                activity2.PutExtra("MyData", sTipoLivro);
                activity2.PutExtra("MyDataLivro", sNomeLivro);
                activity2.PutExtra("MyDataCapitulo", sCapitulo);
                activity2.PutExtra("MyDataTotCapitulos", sNumeroCapitulos);

                //Abrir a tela 
                StartActivity(activity2);
            }

            if (sSelecao == "Juízes")
            {
                sTipoLivro = "AT";
                sNomeLivro = sSelecao;
                sCapitulo = "1";
                string sNumeroCapitulos = "21";
                var activity2 = new Intent(this, typeof(ActivityPlanoOC));
                activity2.PutExtra("MyData", sTipoLivro);
                activity2.PutExtra("MyDataLivro", sNomeLivro);
                activity2.PutExtra("MyDataCapitulo", sCapitulo);
                activity2.PutExtra("MyDataTotCapitulos", sNumeroCapitulos);

                //Abrir a tela 
                StartActivity(activity2);
            }

            if (sSelecao == "Rute")
            {
                sTipoLivro = "AT";
                sNomeLivro = sSelecao;
                sCapitulo = "1";
                string sNumeroCapitulos = "4";
                var activity2 = new Intent(this, typeof(ActivityPlanoOC));
                activity2.PutExtra("MyData", sTipoLivro);
                activity2.PutExtra("MyDataLivro", sNomeLivro);
                activity2.PutExtra("MyDataCapitulo", sCapitulo);
                activity2.PutExtra("MyDataTotCapitulos", sNumeroCapitulos);

                //Abrir a tela 
                StartActivity(activity2);
            }

            if (sSelecao == "1º Samuel")
            {
                sTipoLivro = "AT";
                sNomeLivro = sSelecao;
                sCapitulo = "1";
                string sNumeroCapitulos = "31";
                var activity2 = new Intent(this, typeof(ActivityPlanoOC));
                activity2.PutExtra("MyData", sTipoLivro);
                activity2.PutExtra("MyDataLivro", sNomeLivro);
                activity2.PutExtra("MyDataCapitulo", sCapitulo);
                activity2.PutExtra("MyDataTotCapitulos", sNumeroCapitulos);

                //Abrir a tela 
                StartActivity(activity2);
            }

            if (sSelecao == "2º Samuel")
            {
                sTipoLivro = "AT";
                sNomeLivro = sSelecao;
                sCapitulo = "1";
                string sNumeroCapitulos = "24";
                var activity2 = new Intent(this, typeof(ActivityPlanoOC));
                activity2.PutExtra("MyData", sTipoLivro);
                activity2.PutExtra("MyDataLivro", sNomeLivro);
                activity2.PutExtra("MyDataCapitulo", sCapitulo);
                activity2.PutExtra("MyDataTotCapitulos", sNumeroCapitulos);

                //Abrir a tela 
                StartActivity(activity2);
            }

            if (sSelecao == "1º Reis")
            {
                sTipoLivro = "AT";
                sNomeLivro = sSelecao;
                sCapitulo = "1";
                string sNumeroCapitulos = "22";
                var activity2 = new Intent(this, typeof(ActivityPlanoOC));
                activity2.PutExtra("MyData", sTipoLivro);
                activity2.PutExtra("MyDataLivro", sNomeLivro);
                activity2.PutExtra("MyDataCapitulo", sCapitulo);
                activity2.PutExtra("MyDataTotCapitulos", sNumeroCapitulos);

                //Abrir a tela 
                StartActivity(activity2);
            }

            if (sSelecao == "2º Reis")
            {
                sTipoLivro = "AT";
                sNomeLivro = sSelecao;
                sCapitulo = "1";
                string sNumeroCapitulos = "25";
                var activity2 = new Intent(this, typeof(ActivityPlanoOC));
                activity2.PutExtra("MyData", sTipoLivro);
                activity2.PutExtra("MyDataLivro", sNomeLivro);
                activity2.PutExtra("MyDataCapitulo", sCapitulo);
                activity2.PutExtra("MyDataTotCapitulos", sNumeroCapitulos);

                //Abrir a tela 
                StartActivity(activity2);
            }

            if (sSelecao == "1º Crônicas")
            {
                sTipoLivro = "AT";
                sNomeLivro = sSelecao;
                sCapitulo = "1";
                string sNumeroCapitulos = "29";
                var activity2 = new Intent(this, typeof(ActivityPlanoOC));
                activity2.PutExtra("MyData", sTipoLivro);
                activity2.PutExtra("MyDataLivro", sNomeLivro);
                activity2.PutExtra("MyDataCapitulo", sCapitulo);
                activity2.PutExtra("MyDataTotCapitulos", sNumeroCapitulos);

                //Abrir a tela 
                StartActivity(activity2);
            }

            if (sSelecao == "2º Crônicas")
            {
                sTipoLivro = "AT";
                sNomeLivro = sSelecao;
                sCapitulo = "1";
                string sNumeroCapitulos = "29";
                var activity2 = new Intent(this, typeof(ActivityPlanoOC));
                activity2.PutExtra("MyData", sTipoLivro);
                activity2.PutExtra("MyDataLivro", sNomeLivro);
                activity2.PutExtra("MyDataCapitulo", sCapitulo);
                activity2.PutExtra("MyDataTotCapitulos", sNumeroCapitulos);

                //Abrir a tela 
                StartActivity(activity2);
            }

            if (sSelecao == "Esdras")
            {
                sTipoLivro = "AT";
                sNomeLivro = sSelecao;
                sCapitulo = "1";
                string sNumeroCapitulos = "10";
                var activity2 = new Intent(this, typeof(ActivityPlanoOC));
                activity2.PutExtra("MyData", sTipoLivro);
                activity2.PutExtra("MyDataLivro", sNomeLivro);
                activity2.PutExtra("MyDataCapitulo", sCapitulo);
                activity2.PutExtra("MyDataTotCapitulos", sNumeroCapitulos);

                //Abrir a tela 
                StartActivity(activity2);
            }

            if (sSelecao == "Neemias")
            {
                sTipoLivro = "AT";
                sNomeLivro = sSelecao;
                sCapitulo = "1";
                string sNumeroCapitulos = "13";
                var activity2 = new Intent(this, typeof(ActivityPlanoOC));
                activity2.PutExtra("MyData", sTipoLivro);
                activity2.PutExtra("MyDataLivro", sNomeLivro);
                activity2.PutExtra("MyDataCapitulo", sCapitulo);
                activity2.PutExtra("MyDataTotCapitulos", sNumeroCapitulos);

                //Abrir a tela 
                StartActivity(activity2);
            }

            if (sSelecao == "Ester")
            {
                sTipoLivro = "AT";
                sNomeLivro = sSelecao;
                sCapitulo = "1";
                string sNumeroCapitulos = "10";
                var activity2 = new Intent(this, typeof(ActivityPlanoOC));
                activity2.PutExtra("MyData", sTipoLivro);
                activity2.PutExtra("MyDataLivro", sNomeLivro);
                activity2.PutExtra("MyDataCapitulo", sCapitulo);
                activity2.PutExtra("MyDataTotCapitulos", sNumeroCapitulos);

                //Abrir a tela 
                StartActivity(activity2);
            }

            if (sSelecao == "Jó")
            {
                sTipoLivro = "AT";
                sNomeLivro = sSelecao;
                sCapitulo = "1";
                string sNumeroCapitulos = "42";
                var activity2 = new Intent(this, typeof(ActivityPlanoOC));
                activity2.PutExtra("MyData", sTipoLivro);
                activity2.PutExtra("MyDataLivro", sNomeLivro);
                activity2.PutExtra("MyDataCapitulo", sCapitulo);
                activity2.PutExtra("MyDataTotCapitulos", sNumeroCapitulos);

                //Abrir a tela 
                StartActivity(activity2);
            }

            if (sSelecao == "Salmos")
            {
                sTipoLivro = "AT";
                sNomeLivro = sSelecao;
                sCapitulo = "1";
                string sNumeroCapitulos = "150";
                var activity2 = new Intent(this, typeof(ActivityPlanoOC));
                activity2.PutExtra("MyData", sTipoLivro);
                activity2.PutExtra("MyDataLivro", sNomeLivro);
                activity2.PutExtra("MyDataCapitulo", sCapitulo);
                activity2.PutExtra("MyDataTotCapitulos", sNumeroCapitulos);

                //Abrir a tela 
                StartActivity(activity2);
            }

            if (sSelecao == "Provérbios")
            {
                sTipoLivro = "AT";
                sNomeLivro = sSelecao;
                sCapitulo = "1";
                string sNumeroCapitulos = "31";
                var activity2 = new Intent(this, typeof(ActivityPlanoOC));
                activity2.PutExtra("MyData", sTipoLivro);
                activity2.PutExtra("MyDataLivro", sNomeLivro);
                activity2.PutExtra("MyDataCapitulo", sCapitulo);
                activity2.PutExtra("MyDataTotCapitulos", sNumeroCapitulos);

                //Abrir a tela 
                StartActivity(activity2);
            }

            if (sSelecao == "Eclesiastes")
            {
                sTipoLivro = "AT";
                sNomeLivro = sSelecao;
                sCapitulo = "1";
                string sNumeroCapitulos = "12";
                var activity2 = new Intent(this, typeof(ActivityPlanoOC));
                activity2.PutExtra("MyData", sTipoLivro);
                activity2.PutExtra("MyDataLivro", sNomeLivro);
                activity2.PutExtra("MyDataCapitulo", sCapitulo);
                activity2.PutExtra("MyDataTotCapitulos", sNumeroCapitulos);

                //Abrir a tela 
                StartActivity(activity2);
            }

            if (sSelecao == "Cântico dos Cânticos")
            {
                sTipoLivro = "AT";
                sNomeLivro = sSelecao;
                sCapitulo = "1";
                string sNumeroCapitulos = "8";
                var activity2 = new Intent(this, typeof(ActivityPlanoOC));
                activity2.PutExtra("MyData", sTipoLivro);
                activity2.PutExtra("MyDataLivro", sNomeLivro);
                activity2.PutExtra("MyDataCapitulo", sCapitulo);
                activity2.PutExtra("MyDataTotCapitulos", sNumeroCapitulos);

                //Abrir a tela 
                StartActivity(activity2);
            }

            if (sSelecao == "Isaías")
            {
                sTipoLivro = "AT";
                sNomeLivro = sSelecao;
                sCapitulo = "1";
                string sNumeroCapitulos = "66";
                var activity2 = new Intent(this, typeof(ActivityPlanoOC));
                activity2.PutExtra("MyData", sTipoLivro);
                activity2.PutExtra("MyDataLivro", sNomeLivro);
                activity2.PutExtra("MyDataCapitulo", sCapitulo);
                activity2.PutExtra("MyDataTotCapitulos", sNumeroCapitulos);

                //Abrir a tela 
                StartActivity(activity2);
            }

            if (sSelecao == "Jeremias")
            {
                sTipoLivro = "AT";
                sNomeLivro = sSelecao;
                sCapitulo = "1";
                string sNumeroCapitulos = "52";
                var activity2 = new Intent(this, typeof(ActivityPlanoOC));
                activity2.PutExtra("MyData", sTipoLivro);
                activity2.PutExtra("MyDataLivro", sNomeLivro);
                activity2.PutExtra("MyDataCapitulo", sCapitulo);
                activity2.PutExtra("MyDataTotCapitulos", sNumeroCapitulos);

                //Abrir a tela 
                StartActivity(activity2);
            }

            if (sSelecao == "Lamentações")
            {
                sTipoLivro = "AT";
                sNomeLivro = sSelecao;
                sCapitulo = "1";
                string sNumeroCapitulos = "5";
                var activity2 = new Intent(this, typeof(ActivityPlanoOC));
                activity2.PutExtra("MyData", sTipoLivro);
                activity2.PutExtra("MyDataLivro", sNomeLivro);
                activity2.PutExtra("MyDataCapitulo", sCapitulo);
                activity2.PutExtra("MyDataTotCapitulos", sNumeroCapitulos);

                //Abrir a tela 
                StartActivity(activity2);
            }

            if (sSelecao == "Ezequiel")
            {
                sTipoLivro = "AT";
                sNomeLivro = sSelecao;
                sCapitulo = "1";
                string sNumeroCapitulos = "48";
                var activity2 = new Intent(this, typeof(ActivityPlanoOC));
                activity2.PutExtra("MyData", sTipoLivro);
                activity2.PutExtra("MyDataLivro", sNomeLivro);
                activity2.PutExtra("MyDataCapitulo", sCapitulo);
                activity2.PutExtra("MyDataTotCapitulos", sNumeroCapitulos);

                //Abrir a tela 
                StartActivity(activity2);
            }

            if (sSelecao == "Daniel")
            {
                sTipoLivro = "AT";
                sNomeLivro = sSelecao;
                sCapitulo = "1";
                string sNumeroCapitulos = "12";
                var activity2 = new Intent(this, typeof(ActivityPlanoOC));
                activity2.PutExtra("MyData", sTipoLivro);
                activity2.PutExtra("MyDataLivro", sNomeLivro);
                activity2.PutExtra("MyDataCapitulo", sCapitulo);
                activity2.PutExtra("MyDataTotCapitulos", sNumeroCapitulos);

                //Abrir a tela 
                StartActivity(activity2);
            }

            if (sSelecao == "Oséias")
            {
                sTipoLivro = "AT";
                sNomeLivro = sSelecao;
                sCapitulo = "1";
                string sNumeroCapitulos = "14";
                var activity2 = new Intent(this, typeof(ActivityPlanoOC));
                activity2.PutExtra("MyData", sTipoLivro);
                activity2.PutExtra("MyDataLivro", sNomeLivro);
                activity2.PutExtra("MyDataCapitulo", sCapitulo);
                activity2.PutExtra("MyDataTotCapitulos", sNumeroCapitulos);

                //Abrir a tela 
                StartActivity(activity2);
            }

            if (sSelecao == "Joel")
            {
                sTipoLivro = "AT";
                sNomeLivro = sSelecao;
                sCapitulo = "1";
                string sNumeroCapitulos = "3";
                var activity2 = new Intent(this, typeof(ActivityPlanoOC));
                activity2.PutExtra("MyData", sTipoLivro);
                activity2.PutExtra("MyDataLivro", sNomeLivro);
                activity2.PutExtra("MyDataCapitulo", sCapitulo);
                activity2.PutExtra("MyDataTotCapitulos", sNumeroCapitulos);

                //Abrir a tela 
                StartActivity(activity2);
            }

            if (sSelecao == "Amós")
            {
                sTipoLivro = "AT";
                sNomeLivro = sSelecao;
                sCapitulo = "1";
                string sNumeroCapitulos = "9";
                var activity2 = new Intent(this, typeof(ActivityPlanoOC));
                activity2.PutExtra("MyData", sTipoLivro);
                activity2.PutExtra("MyDataLivro", sNomeLivro);
                activity2.PutExtra("MyDataCapitulo", sCapitulo);
                activity2.PutExtra("MyDataTotCapitulos", sNumeroCapitulos);

                //Abrir a tela 
                StartActivity(activity2);
            }

            if (sSelecao == "Obadias")
            {
                sTipoLivro = "AT";
                sNomeLivro = sSelecao;
                sCapitulo = "1";
                string sNumeroCapitulos = "1";
                var activity2 = new Intent(this, typeof(ActivityPlanoOC));
                activity2.PutExtra("MyData", sTipoLivro);
                activity2.PutExtra("MyDataLivro", sNomeLivro);
                activity2.PutExtra("MyDataCapitulo", sCapitulo);
                activity2.PutExtra("MyDataTotCapitulos", sNumeroCapitulos);

                //Abrir a tela 
                StartActivity(activity2);
            }

            if (sSelecao == "Jonas")
            {
                sTipoLivro = "AT";
                sNomeLivro = sSelecao;
                sCapitulo = "1";
                string sNumeroCapitulos = "4";
                var activity2 = new Intent(this, typeof(ActivityPlanoOC));
                activity2.PutExtra("MyData", sTipoLivro);
                activity2.PutExtra("MyDataLivro", sNomeLivro);
                activity2.PutExtra("MyDataCapitulo", sCapitulo);
                activity2.PutExtra("MyDataTotCapitulos", sNumeroCapitulos);

                //Abrir a tela 
                StartActivity(activity2);
            }

            if (sSelecao == "Miqueías")
            {
                sTipoLivro = "AT";
                sNomeLivro = sSelecao;
                sCapitulo = "1";
                string sNumeroCapitulos = "7";
                var activity2 = new Intent(this, typeof(ActivityPlanoOC));
                activity2.PutExtra("MyData", sTipoLivro);
                activity2.PutExtra("MyDataLivro", sNomeLivro);
                activity2.PutExtra("MyDataCapitulo", sCapitulo);
                activity2.PutExtra("MyDataTotCapitulos", sNumeroCapitulos);

                //Abrir a tela 
                StartActivity(activity2);
            }

            if (sSelecao == "Naum")
            {
                sTipoLivro = "AT";
                sNomeLivro = sSelecao;
                sCapitulo = "1";
                string sNumeroCapitulos = "3";
                var activity2 = new Intent(this, typeof(ActivityPlanoOC));
                activity2.PutExtra("MyData", sTipoLivro);
                activity2.PutExtra("MyDataLivro", sNomeLivro);
                activity2.PutExtra("MyDataCapitulo", sCapitulo);
                activity2.PutExtra("MyDataTotCapitulos", sNumeroCapitulos);

                //Abrir a tela 
                StartActivity(activity2);
            }

            if (sSelecao == "Habacuque")
            {
                sTipoLivro = "AT";
                sNomeLivro = sSelecao;
                sCapitulo = "1";
                string sNumeroCapitulos = "3";
                var activity2 = new Intent(this, typeof(ActivityPlanoOC));
                activity2.PutExtra("MyData", sTipoLivro);
                activity2.PutExtra("MyDataLivro", sNomeLivro);
                activity2.PutExtra("MyDataCapitulo", sCapitulo);
                activity2.PutExtra("MyDataTotCapitulos", sNumeroCapitulos);

                //Abrir a tela 
                StartActivity(activity2);
            }

            if (sSelecao == "Sofonias")
            {
                sTipoLivro = "AT";
                sNomeLivro = sSelecao;
                sCapitulo = "1";
                string sNumeroCapitulos = "3";
                var activity2 = new Intent(this, typeof(ActivityPlanoOC));
                activity2.PutExtra("MyData", sTipoLivro);
                activity2.PutExtra("MyDataLivro", sNomeLivro);
                activity2.PutExtra("MyDataCapitulo", sCapitulo);
                activity2.PutExtra("MyDataTotCapitulos", sNumeroCapitulos);

                //Abrir a tela 
                StartActivity(activity2);
            }

            if (sSelecao == "Ageu")
            {
                sTipoLivro = "AT";
                sNomeLivro = sSelecao;
                sCapitulo = "1";
                string sNumeroCapitulos = "2";
                var activity2 = new Intent(this, typeof(ActivityPlanoOC));
                activity2.PutExtra("MyData", sTipoLivro);
                activity2.PutExtra("MyDataLivro", sNomeLivro);
                activity2.PutExtra("MyDataCapitulo", sCapitulo);
                activity2.PutExtra("MyDataTotCapitulos", sNumeroCapitulos);

                //Abrir a tela 
                StartActivity(activity2);
            }

            if (sSelecao == "Zacarias")
            {
                sTipoLivro = "AT";
                sNomeLivro = sSelecao;
                sCapitulo = "1";
                string sNumeroCapitulos = "12";
                var activity2 = new Intent(this, typeof(ActivityPlanoOC));
                activity2.PutExtra("MyData", sTipoLivro);
                activity2.PutExtra("MyDataLivro", sNomeLivro);
                activity2.PutExtra("MyDataCapitulo", sCapitulo);
                activity2.PutExtra("MyDataTotCapitulos", sNumeroCapitulos);

                //Abrir a tela 
                StartActivity(activity2);
            }

            if (sSelecao == "Malaquias")
            {
                sTipoLivro = "AT";
                sNomeLivro = sSelecao;
                sCapitulo = "1";
                string sNumeroCapitulos = "4";
                var activity2 = new Intent(this, typeof(ActivityPlanoOC));
                activity2.PutExtra("MyData", sTipoLivro);
                activity2.PutExtra("MyDataLivro", sNomeLivro);
                activity2.PutExtra("MyDataCapitulo", sCapitulo);
                activity2.PutExtra("MyDataTotCapitulos", sNumeroCapitulos);

                //Abrir a tela 
                StartActivity(activity2);
            }

            if (sSelecao == "Mateus")
            {
                sTipoLivro = "NT";
                sNomeLivro = sSelecao;
                sCapitulo = "1";
                string sNumeroCapitulos = "28";
                var activity2 = new Intent(this, typeof(ActivityPlanoOC));
                activity2.PutExtra("MyData", sTipoLivro);
                activity2.PutExtra("MyDataLivro", sNomeLivro);
                activity2.PutExtra("MyDataCapitulo", sCapitulo);
                activity2.PutExtra("MyDataTotCapitulos", sNumeroCapitulos);

                //Abrir a tela 
                StartActivity(activity2);
            }

            if (sSelecao == "Marcos")
            {
                sTipoLivro = "NT";
                sNomeLivro = sSelecao;
                sCapitulo = "1";
                string sNumeroCapitulos = "16";
                var activity2 = new Intent(this, typeof(ActivityPlanoOC));
                activity2.PutExtra("MyData", sTipoLivro);
                activity2.PutExtra("MyDataLivro", sNomeLivro);
                activity2.PutExtra("MyDataCapitulo", sCapitulo);
                activity2.PutExtra("MyDataTotCapitulos", sNumeroCapitulos);

                //Abrir a tela 
                StartActivity(activity2);
            }

            if (sSelecao == "Lucas")
            {
                sTipoLivro = "NT";
                sNomeLivro = sSelecao;
                sCapitulo = "1";
                string sNumeroCapitulos = "24";
                var activity2 = new Intent(this, typeof(ActivityPlanoOC));
                activity2.PutExtra("MyData", sTipoLivro);
                activity2.PutExtra("MyDataLivro", sNomeLivro);
                activity2.PutExtra("MyDataCapitulo", sCapitulo);
                activity2.PutExtra("MyDataTotCapitulos", sNumeroCapitulos);

                //Abrir a tela 
                StartActivity(activity2);
            }

            if (sSelecao == "João")
            {
                sTipoLivro = "NT";
                sNomeLivro = sSelecao;
                sCapitulo = "1";
                string sNumeroCapitulos = "21";
                var activity2 = new Intent(this, typeof(ActivityPlanoOC));
                activity2.PutExtra("MyData", sTipoLivro);
                activity2.PutExtra("MyDataLivro", sNomeLivro);
                activity2.PutExtra("MyDataCapitulo", sCapitulo);
                activity2.PutExtra("MyDataTotCapitulos", sNumeroCapitulos);

                //Abrir a tela 
                StartActivity(activity2);
            }

            if (sSelecao == "Atos")
            {
                sTipoLivro = "NT";
                sNomeLivro = sSelecao;
                sCapitulo = "1";
                string sNumeroCapitulos = "28";
                var activity2 = new Intent(this, typeof(ActivityPlanoOC));
                activity2.PutExtra("MyData", sTipoLivro);
                activity2.PutExtra("MyDataLivro", sNomeLivro);
                activity2.PutExtra("MyDataCapitulo", sCapitulo);
                activity2.PutExtra("MyDataTotCapitulos", sNumeroCapitulos);

                //Abrir a tela 
                StartActivity(activity2);
            }

            if (sSelecao == "Romanos")
            {
                sTipoLivro = "NT";
                sNomeLivro = sSelecao;
                sCapitulo = "1";
                string sNumeroCapitulos = "16";
                var activity2 = new Intent(this, typeof(ActivityPlanoOC));
                activity2.PutExtra("MyData", sTipoLivro);
                activity2.PutExtra("MyDataLivro", sNomeLivro);
                activity2.PutExtra("MyDataCapitulo", sCapitulo);
                activity2.PutExtra("MyDataTotCapitulos", sNumeroCapitulos);

                //Abrir a tela 
                StartActivity(activity2);
            }

            if (sSelecao == "1ª Coríntios")
            {
                sTipoLivro = "NT";
                sNomeLivro = sSelecao;
                sCapitulo = "1";
                string sNumeroCapitulos = "16";
                var activity2 = new Intent(this, typeof(ActivityPlanoOC));
                activity2.PutExtra("MyData", sTipoLivro);
                activity2.PutExtra("MyDataLivro", sNomeLivro);
                activity2.PutExtra("MyDataCapitulo", sCapitulo);
                activity2.PutExtra("MyDataTotCapitulos", sNumeroCapitulos);

                //Abrir a tela 
                StartActivity(activity2);
            }

            if (sSelecao == "2ª Coríntios")
            {
                sTipoLivro = "NT";
                sNomeLivro = sSelecao;
                sCapitulo = "1";
                string sNumeroCapitulos = "13";
                var activity2 = new Intent(this, typeof(ActivityPlanoOC));
                activity2.PutExtra("MyData", sTipoLivro);
                activity2.PutExtra("MyDataLivro", sNomeLivro);
                activity2.PutExtra("MyDataCapitulo", sCapitulo);
                activity2.PutExtra("MyDataTotCapitulos", sNumeroCapitulos);

                //Abrir a tela 
                StartActivity(activity2);
            }

            if (sSelecao == "Gálatas")
            {
                sTipoLivro = "NT";
                sNomeLivro = sSelecao;
                sCapitulo = "1";
                string sNumeroCapitulos = "6";
                var activity2 = new Intent(this, typeof(ActivityPlanoOC));
                activity2.PutExtra("MyData", sTipoLivro);
                activity2.PutExtra("MyDataLivro", sNomeLivro);
                activity2.PutExtra("MyDataCapitulo", sCapitulo);
                activity2.PutExtra("MyDataTotCapitulos", sNumeroCapitulos);

                //Abrir a tela 
                StartActivity(activity2);
            }

            if (sSelecao == "Efésios")
            {
                sTipoLivro = "NT";
                sNomeLivro = sSelecao;
                sCapitulo = "1";
                string sNumeroCapitulos = "6";
                var activity2 = new Intent(this, typeof(ActivityPlanoOC));
                activity2.PutExtra("MyData", sTipoLivro);
                activity2.PutExtra("MyDataLivro", sNomeLivro);
                activity2.PutExtra("MyDataCapitulo", sCapitulo);
                activity2.PutExtra("MyDataTotCapitulos", sNumeroCapitulos);

                //Abrir a tela 
                StartActivity(activity2);
            }

            if (sSelecao == "Filipenses")
            {
                sTipoLivro = "NT";
                sNomeLivro = sSelecao;
                sCapitulo = "1";
                string sNumeroCapitulos = "4";
                var activity2 = new Intent(this, typeof(ActivityPlanoOC));
                activity2.PutExtra("MyData", sTipoLivro);
                activity2.PutExtra("MyDataLivro", sNomeLivro);
                activity2.PutExtra("MyDataCapitulo", sCapitulo);
                activity2.PutExtra("MyDataTotCapitulos", sNumeroCapitulos);

                //Abrir a tela 
                StartActivity(activity2);
            }

            if (sSelecao == "Colossenses")
            {
                sTipoLivro = "NT";
                sNomeLivro = sSelecao;
                sCapitulo = "1";
                string sNumeroCapitulos = "4";
                var activity2 = new Intent(this, typeof(ActivityPlanoOC));
                activity2.PutExtra("MyData", sTipoLivro);
                activity2.PutExtra("MyDataLivro", sNomeLivro);
                activity2.PutExtra("MyDataCapitulo", sCapitulo);
                activity2.PutExtra("MyDataTotCapitulos", sNumeroCapitulos);

                //Abrir a tela 
                StartActivity(activity2);
            }

            if (sSelecao == "1ª Tessalonicenses")
            {
                sTipoLivro = "NT";
                sNomeLivro = sSelecao;
                sCapitulo = "1";
                string sNumeroCapitulos = "5";
                var activity2 = new Intent(this, typeof(ActivityPlanoOC));
                activity2.PutExtra("MyData", sTipoLivro);
                activity2.PutExtra("MyDataLivro", sNomeLivro);
                activity2.PutExtra("MyDataCapitulo", sCapitulo);
                activity2.PutExtra("MyDataTotCapitulos", sNumeroCapitulos);

                //Abrir a tela 
                StartActivity(activity2);
            }

            if (sSelecao == "2ª Tessalonicenses")
            {
                sTipoLivro = "NT";
                sNomeLivro = sSelecao;
                sCapitulo = "1";
                string sNumeroCapitulos = "3";
                var activity2 = new Intent(this, typeof(ActivityPlanoOC));
                activity2.PutExtra("MyData", sTipoLivro);
                activity2.PutExtra("MyDataLivro", sNomeLivro);
                activity2.PutExtra("MyDataCapitulo", sCapitulo);
                activity2.PutExtra("MyDataTotCapitulos", sNumeroCapitulos);

                //Abrir a tela 
                StartActivity(activity2);
            }

            if (sSelecao == "1ª Timóteo")
            {
                sTipoLivro = "NT";
                sNomeLivro = sSelecao;
                sCapitulo = "1";
                string sNumeroCapitulos = "6";
                var activity2 = new Intent(this, typeof(ActivityPlanoOC));
                activity2.PutExtra("MyData", sTipoLivro);
                activity2.PutExtra("MyDataLivro", sNomeLivro);
                activity2.PutExtra("MyDataCapitulo", sCapitulo);
                activity2.PutExtra("MyDataTotCapitulos", sNumeroCapitulos);

                //Abrir a tela 
                StartActivity(activity2);
            }

            if (sSelecao == "2ª Timóteo")
            {
                sTipoLivro = "NT";
                sNomeLivro = sSelecao;
                sCapitulo = "1";
                string sNumeroCapitulos = "4";
                var activity2 = new Intent(this, typeof(ActivityPlanoOC));
                activity2.PutExtra("MyData", sTipoLivro);
                activity2.PutExtra("MyDataLivro", sNomeLivro);
                activity2.PutExtra("MyDataCapitulo", sCapitulo);
                activity2.PutExtra("MyDataTotCapitulos", sNumeroCapitulos);

                //Abrir a tela 
                StartActivity(activity2);
            }

            if (sSelecao == "Tito")
            {
                sTipoLivro = "NT";
                sNomeLivro = sSelecao;
                sCapitulo = "1";
                string sNumeroCapitulos = "3";
                var activity2 = new Intent(this, typeof(ActivityPlanoOC));
                activity2.PutExtra("MyData", sTipoLivro);
                activity2.PutExtra("MyDataLivro", sNomeLivro);
                activity2.PutExtra("MyDataCapitulo", sCapitulo);
                activity2.PutExtra("MyDataTotCapitulos", sNumeroCapitulos);

                //Abrir a tela 
                StartActivity(activity2);
            }

            if (sSelecao == "Filemon")
            {
                sTipoLivro = "NT";
                sNomeLivro = sSelecao;
                sCapitulo = "1";
                string sNumeroCapitulos = "1";
                var activity2 = new Intent(this, typeof(ActivityPlanoOC));
                activity2.PutExtra("MyData", sTipoLivro);
                activity2.PutExtra("MyDataLivro", sNomeLivro);
                activity2.PutExtra("MyDataCapitulo", sCapitulo);
                activity2.PutExtra("MyDataTotCapitulos", sNumeroCapitulos);

                //Abrir a tela 
                StartActivity(activity2);
            }

            if (sSelecao == "Hebreus")
            {
                sTipoLivro = "NT";
                sNomeLivro = sSelecao;
                sCapitulo = "1";
                string sNumeroCapitulos = "12";
                var activity2 = new Intent(this, typeof(ActivityPlanoOC));
                activity2.PutExtra("MyData", sTipoLivro);
                activity2.PutExtra("MyDataLivro", sNomeLivro);
                activity2.PutExtra("MyDataCapitulo", sCapitulo);
                activity2.PutExtra("MyDataTotCapitulos", sNumeroCapitulos);

                //Abrir a tela 
                StartActivity(activity2);
            }

            if (sSelecao == "Tiago")
            {
                sTipoLivro = "NT";
                sNomeLivro = sSelecao;
                sCapitulo = "1";
                string sNumeroCapitulos = "5";
                var activity2 = new Intent(this, typeof(ActivityPlanoOC));
                activity2.PutExtra("MyData", sTipoLivro);
                activity2.PutExtra("MyDataLivro", sNomeLivro);
                activity2.PutExtra("MyDataCapitulo", sCapitulo);
                activity2.PutExtra("MyDataTotCapitulos", sNumeroCapitulos);

                //Abrir a tela 
                StartActivity(activity2);
            }

            if (sSelecao == "1ª Pedro")
            {
                sTipoLivro = "NT";
                sNomeLivro = sSelecao;
                sCapitulo = "1";
                string sNumeroCapitulos = "5";
                var activity2 = new Intent(this, typeof(ActivityPlanoOC));
                activity2.PutExtra("MyData", sTipoLivro);
                activity2.PutExtra("MyDataLivro", sNomeLivro);
                activity2.PutExtra("MyDataCapitulo", sCapitulo);
                activity2.PutExtra("MyDataTotCapitulos", sNumeroCapitulos);

                //Abrir a tela 
                StartActivity(activity2);
            }

            if (sSelecao == "2ª Pedro")
            {
                sTipoLivro = "NT";
                sNomeLivro = sSelecao;
                sCapitulo = "1";
                string sNumeroCapitulos = "3";
                var activity2 = new Intent(this, typeof(ActivityPlanoOC));
                activity2.PutExtra("MyData", sTipoLivro);
                activity2.PutExtra("MyDataLivro", sNomeLivro);
                activity2.PutExtra("MyDataCapitulo", sCapitulo);
                activity2.PutExtra("MyDataTotCapitulos", sNumeroCapitulos);

                //Abrir a tela 
                StartActivity(activity2);
            }

            if (sSelecao == "1ª João")
            {
                sTipoLivro = "NT";
                sNomeLivro = sSelecao;
                sCapitulo = "1";
                string sNumeroCapitulos = "5";
                var activity2 = new Intent(this, typeof(ActivityPlanoOC));
                activity2.PutExtra("MyData", sTipoLivro);
                activity2.PutExtra("MyDataLivro", sNomeLivro);
                activity2.PutExtra("MyDataCapitulo", sCapitulo);
                activity2.PutExtra("MyDataTotCapitulos", sNumeroCapitulos);

                //Abrir a tela 
                StartActivity(activity2);
            }

            if (sSelecao == "2ª João")
            {
                sTipoLivro = "NT";
                sNomeLivro = sSelecao;
                sCapitulo = "1";
                string sNumeroCapitulos = "1";
                var activity2 = new Intent(this, typeof(ActivityPlanoOC));
                activity2.PutExtra("MyData", sTipoLivro);
                activity2.PutExtra("MyDataLivro", sNomeLivro);
                activity2.PutExtra("MyDataCapitulo", sCapitulo);
                activity2.PutExtra("MyDataTotCapitulos", sNumeroCapitulos);

                //Abrir a tela 
                StartActivity(activity2);
            }

            if (sSelecao == "3ª João")
            {
                sTipoLivro = "NT";
                sNomeLivro = sSelecao;
                sCapitulo = "1";
                string sNumeroCapitulos = "1";
                var activity2 = new Intent(this, typeof(ActivityPlanoOC));
                activity2.PutExtra("MyData", sTipoLivro);
                activity2.PutExtra("MyDataLivro", sNomeLivro);
                activity2.PutExtra("MyDataCapitulo", sCapitulo);
                activity2.PutExtra("MyDataTotCapitulos", sNumeroCapitulos);

                //Abrir a tela 
                StartActivity(activity2);

            }

            if (sSelecao == "Judas")
            {
                sTipoLivro = "NT";
                sNomeLivro = sSelecao;
                sCapitulo = "1";
                string sNumeroCapitulos = "1";
                var activity2 = new Intent(this, typeof(ActivityPlanoOC));
                activity2.PutExtra("MyData", sTipoLivro);
                activity2.PutExtra("MyDataLivro", sNomeLivro);
                activity2.PutExtra("MyDataCapitulo", sCapitulo);
                activity2.PutExtra("MyDataTotCapitulos", sNumeroCapitulos);

                //Abrir a tela 
                StartActivity(activity2);
            }

            if (sSelecao == "Revelações")
            {
                sTipoLivro = "NT";
                sNomeLivro = sSelecao;
                sCapitulo = "1";
                string sNumeroCapitulos = "22";
                var activity2 = new Intent(this, typeof(ActivityPlanoOC));
                activity2.PutExtra("MyData", sTipoLivro);
                activity2.PutExtra("MyDataLivro", sNomeLivro);
                activity2.PutExtra("MyDataCapitulo", sCapitulo);
                activity2.PutExtra("MyDataTotCapitulos", sNumeroCapitulos);

                //Abrir a tela 
                StartActivity(activity2);
            }

            if (SelecaoRef == true)
            {                               
                var activity2 = new Intent(this, typeof(ActivityPlanoOC));
                activity2.PutExtra("MyData", sTipoLivro);
                activity2.PutExtra("MyDataLivro", sLivroBiblia);
                activity2.PutExtra("MyDataCapitulo", sCapituloLivroBiblia);
                activity2.PutExtra("MyDataTotCapitulos", sTotalCapitulos);
                activity2.PutExtra("MyDataVersiculoBiblia", sVersiculoCapituloLivroBiblia);
                activity2.PutExtra("MyDataTextoVersiculo", sTextoVersiculoCapituloLivroBiblia);
                activity2.PutExtra("MyDataUnicVersiculo", "1");


                //Abrir a tela 
                StartActivity(activity2);
            }

        }

        private void AdicionarDados()
        {
            lista = new ArrayList();

            // Lista do AT
            lista.Add("Gênesis");
            lista.Add("Êxodo");
            lista.Add("Levítico");
            lista.Add("Números");
            lista.Add("Deuterônimio");
            lista.Add("Josué");
            lista.Add("Juízes");
            lista.Add("Rute");
            lista.Add("1º Samuel");
            lista.Add("2º Samuel");
            lista.Add("1º Reis");
            lista.Add("2º Reis");
            lista.Add("1º Crônicas");
            lista.Add("2º Crônicas");
            lista.Add("Esdras");
            lista.Add("Neemias");
            lista.Add("Ester");
            lista.Add("Jó");
            lista.Add("Salmos");
            lista.Add("Provérbios");
            lista.Add("Eclesiastes");
            lista.Add("Cântico dos Cânticos");
            lista.Add("Isaías");
            lista.Add("Jeremias");
            lista.Add("Lamentações");
            lista.Add("Ezequiel");
            lista.Add("Daniel");
            lista.Add("Oséias");
            lista.Add("Joel");
            lista.Add("Amós");
            lista.Add("Obadias");
            lista.Add("Jonas");
            lista.Add("Miquéias");
            lista.Add("Naum");
            lista.Add("Habacuque");
            lista.Add("Sofonias");
            lista.Add("Ageu");
            lista.Add("Zacarias");
            lista.Add("Malaquias");

            // livros do NT
            lista.Add("Mateus");
            lista.Add("Marcos");
            lista.Add("Lucas");
            lista.Add("João");
            lista.Add("Atos");
            lista.Add("Romanos");
            lista.Add("1ª Coríntios");
            lista.Add("2ª Coríntios");
            lista.Add("Gálatas");
            lista.Add("Efésios");
            lista.Add("Filipenses");
            lista.Add("Colossenses");
            lista.Add("1ª Tessalonicenses");
            lista.Add("2ª Tessalonicenses");
            lista.Add("1ª Timóteo");
            lista.Add("2ª Timóteo");
            lista.Add("Tito");
            lista.Add("Filemom");
            lista.Add("Hebreus");
            lista.Add("Tiago");
            lista.Add("1ª Pedro");
            lista.Add("2ª Pedro");
            lista.Add("1ª João");
            lista.Add("2ª João");
            lista.Add("3ª João");
            lista.Add("Judas");
            lista.Add("Revelações");

        } // PESQUISA POR LIVROS

        private void PesquisarReferencia(string sTextoPesquisa)
        {
            

            XmlReader xReader = XmlReader.Create(Assets.Open("nvi.xml"));
            XmlReader xmlReader = XmlReader.Create(Assets.Open("nvi.xml"));

            var bookFound = false;
            var capFound = false;
            int sVersos = 0;

            bool AchouLivro = false;
            bool AchouCapitulo = false;
            bool AchouVersiculo = false;
            int sTamanho = sTextoPesq.Length;

            string sParte1 = ""; // Possível nome do livro
            string sParte2 = "";  // Possível referência de capitulo 
            string sParte3 = ""; // Possível referência de versiculo 
            
            bool sEspaco = false;  //Define onde encontrou o espaço. Variável de controle
            bool sDoisPontos = false;  //Define onde encontrou os dois pontos. Variável de controle

            string sAux = "";   // Posição de cada letra da Pesquisa
            string sPosicaoEspaco= ""; // Posição onde está o espaço na pesquisa
            string sPosicaoDoisPontos = ""; // Posição onde está o espaço na pesquisa

            // 02/05/2021 16:08h
            // Acha a posição exata do Espaço no Texto            
            for (int z = 0; z < sTextoPesq.Length; z++)
            {
                sAux = sTextoPesq.Substring(z, 1);
                if (sAux == " ")
                {
                    sPosicaoEspaco = z.ToString();
                    sEspaco = true;
                }
                else 
                {
                    sEspaco = false;
                }

                if (sAux == ":")
                {                    
                    
                    sPosicaoDoisPontos = z.ToString();
                    sDoisPontos = true;
                }
                else                
                {                
                    sDoisPontos = false;
                }

            }

            // 02/05/2021 17:08h
            // Analisa a posição do ESPAÇO e dos DOIS PONTOS para
            // pegar a referência da forma adequada
            if (sTamanho == 6)
            {
                if ((sPosicaoEspaco == "2") && (sPosicaoDoisPontos == "4"))
                {
                    sParte1 = sTextoPesq.Substring(0, 2); // Possível nome do livro
                    sParte2 = sTextoPesq.Substring(3, 1);  // Possível referência de capitulo 
                    sParte3 = sTextoPesq.Substring(5, 1); // Possível referência de versiculo 
                }
                
            }

            if (sTamanho == 7)
            {
                // 02/05/2021 16:08h
                // Acha a posição exata do Espaço no Texto
                // Para diferenciar o nome do Livro com duas ou três letras, como 1Sm
                if ((sPosicaoEspaco == "2") && (sPosicaoDoisPontos=="5"))
                {
                    sParte1 = sTextoPesq.Substring(0, 2); // Possível nome do livro
                    sParte2 = sTextoPesq.Substring(3, 2);  // Possível referência de capitulo 
                    sParte3 = sTextoPesq.Substring(6, 1); // Possível referência de versiculo                     
                }

                if ((sPosicaoEspaco == "2") && (sPosicaoDoisPontos == "4"))
                {
                    sParte1 = sTextoPesq.Substring(0, 2); // Possível nome do livro
                    sParte2 = sTextoPesq.Substring(3, 1);  // Possível referência de capitulo 
                    sParte3 = sTextoPesq.Substring(5, 2); // Possível referência de versiculo                     
                }

                // Referência 1Sm 7:1
                if ((sPosicaoEspaco == "3") && (sPosicaoDoisPontos == "5"))
                {
                    sParte1 = sTextoPesq.Substring(0, 3); // Possível nome do livro
                    sParte2 = sTextoPesq.Substring(4, 1);  // Possível referência de capitulo 
                    sParte3 = sTextoPesq.Substring(6, 1); // Possível referência de versiculo                     
                }

            }

            if (sTamanho == 8)
            {
                // 02/05/2021 16:08h
                // Acha a posição exata do Espaço no Texto
                // Para diferenciar o nome do Livro com duas ou três letras, como 1Sm               
                
                // Referência: Js 17:25
                if ((sPosicaoEspaco == "2") && (sPosicaoDoisPontos == "5"))
                {
                    sParte1 = sTextoPesq.Substring(0, 2); // Possível nome do livro
                    sParte2 = sTextoPesq.Substring(3, 2);  // Possível referência de capitulo 
                    sParte3 = sTextoPesq.Substring(6, 2); // Possível referência de versiculo 
                }

                if ((sPosicaoEspaco == "2") && (sPosicaoDoisPontos == "6"))
                {
                    sParte1 = sTextoPesq.Substring(0, 2);  // Possível nome do livro - SL 100:1
                    sParte2 = sTextoPesq.Substring(3, 3);  // Possível referência de capitulo 
                    sParte3 = sTextoPesq.Substring(7, 1);  // Possível referência de versiculo 
                }
                

                if ((sPosicaoEspaco == "3") && (sPosicaoDoisPontos == "5"))
                {
                    sParte1 = sTextoPesq.Substring(0, 3); // Possível nome do livro
                    sParte2 = sTextoPesq.Substring(4, 1);  // Possível referência de capitulo 
                    sParte3 = sTextoPesq.Substring(6, 2); // Possível referência de versiculo 
                }

                if ((sPosicaoEspaco == "3") && (sPosicaoDoisPontos == "6"))
                {
                    sParte1 = sTextoPesq.Substring(0, 3); // Possível nome do livro
                    sParte2 = sTextoPesq.Substring(4, 2);  // Possível referência de capitulo 
                    sParte3 = sTextoPesq.Substring(7, 1); // Possível referência de versiculo 
                }

                // Referência: 1Sm 17:25
                if ((sPosicaoEspaco == "4") && (sPosicaoDoisPontos == "6"))
                {
                    sParte1 = sTextoPesq.Substring(0, 3); // Possível nome do livro
                    sParte2 = sTextoPesq.Substring(3, 1);  // Possível referência de capitulo 
                    sParte3 = sTextoPesq.Substring(7, 1); // Possível referência de versiculo 
                }
                                
            }

            // Refêrencia: 1Sm 17:13
            if (sTamanho == 9)
            {
                // 02/05/2021 16:08h
                // Acha a posição exata do Espaço no Texto
                // Para diferenciar o nome do Livro com duas ou três letras, como 1Sm               

                // Referencia: Js 17:25
                if ((sPosicaoEspaco == "3") && (sPosicaoDoisPontos == "6"))
                {
                    sParte1 = sTextoPesq.Substring(0, 2); // Possível nome do livro
                    sParte2 = sTextoPesq.Substring(3, 3);  // Possível referência de capitulo 
                    if (sParte1 == "sl")
                    {
                        sParte3 = sTextoPesq.Substring(7, 3); // Possível referência de versiculo 
                    }
                    else 
                    {
                        sParte3 = sTextoPesq.Substring(7, 2); // Possível referência de versiculo 
                    }
                }

                if ((sPosicaoEspaco == "2") && (sPosicaoDoisPontos == "6"))
                {
                    sParte1 = sTextoPesq.Substring(0, 2);  // Possível nome do livro - SL 100:12
                    sParte2 = sTextoPesq.Substring(3, 3);  // Possível referência de capitulo 
                    sParte3 = sTextoPesq.Substring(7, 2);  // Possível referência de versiculo 
                }

            }

            // SL 119:113
            if (sTamanho == 10)
            {
                // 02/05/2021 16:08h
                // Acha a posição exata do Espaço no Texto
                // Para diferenciar o nome do Livro com duas ou três letras, como 1Sm               

                // Referencia: Js 17:25
                if ((sPosicaoEspaco == "2") && (sPosicaoDoisPontos == "6"))
                {
                    sParte1 = sTextoPesq.Substring(0, 2); // Possível nome do livro
                    sParte2 = sTextoPesq.Substring(3, 3);  // Possível referência de capitulo 
                    sParte3 = sTextoPesq.Substring(7, 3); // Possível referência de versiculo 
                }

            }


            // DADOS A PESQUISAR 15/04/2021 09:51h
            string snome = "";
            string sdesc = "";
            string sver = "";

            if ((sParte1 == "Gn") || (sParte1 == "GN") || (sParte1 == "gn"))
            {
                snome = "Gênesis";
                sLivroBiblia = snome;
                sTipoLivro = "AT";
                sCapituloLivroBiblia = sParte2;
                sTotalCapitulos = "50";
            }

            if ((sParte1 == "Ex") || (sParte1 == "EX") || (sParte1 == "ex"))
            {
                snome = "Êxodo";
                sLivroBiblia = snome;
                sTipoLivro = "AT";
                sCapituloLivroBiblia = sParte2;
                sTotalCapitulos = "40";
            }

            if ((sParte1 == "Lv") || (sParte1 == "LV") || (sParte1 == "lv"))
            {
                snome = "Levítico";
                sLivroBiblia = snome;
                sTipoLivro = "AT";
                sCapituloLivroBiblia = sParte2;
                sTotalCapitulos = "27";
            }

            if ((sParte1 == "Nm") || (sParte1 == "NM") || (sParte1 == "nm"))
            {
                snome = "Números";
                sLivroBiblia = snome;
                sTipoLivro = "AT";
                sCapituloLivroBiblia = sParte2;
                sTotalCapitulos = "36";
            }

            if ((sParte1 == "Dt") || (sParte1 == "DT") || (sParte1 == "dt"))
            {
                snome = "Deuteronômio";
                sLivroBiblia = snome;
                sTipoLivro = "AT";
                sCapituloLivroBiblia = sParte2;
                sTotalCapitulos = "34";
            }

            if ((sParte1 == "Js") || (sParte1 == "JS") || (sParte1 == "js"))
            {
                snome = "Josué";
                sLivroBiblia = snome;
                sTipoLivro = "AT";
                sCapituloLivroBiblia = sParte2;
                sTotalCapitulos = "24";
            }

            if ((sParte1 == "Jz") || (sParte1 == "JZ") || (sParte1 == "jz"))
            {
                snome = "Juízes";
                sLivroBiblia = snome;
                sTipoLivro = "AT";
                sCapituloLivroBiblia = sParte2;
                sTotalCapitulos = "21";
            }

            if ((sParte1 == "Rt") || (sParte1 == "RT") || (sParte1 == "rt"))
            {
                snome = "Rute";
                sLivroBiblia = snome;
                sTipoLivro = "AT";
                sCapituloLivroBiblia = sParte2;
                sTotalCapitulos = "4";
            }

            if ((sParte1 == "1Sm") || (sParte1 == "1SM") || (sParte1 == "1sm"))
            {
                snome = "1º Samuel";
                sLivroBiblia = snome;
                sTipoLivro = "AT";
                sCapituloLivroBiblia = sParte2;
                sTotalCapitulos = "31";
            }

            if ((sParte1 == "2Sm") || (sParte1 == "2SM") || (sParte1 == "2sm"))
            {
                snome = "2º Samuel";
                sLivroBiblia = snome;
                sTipoLivro = "AT";
                sCapituloLivroBiblia = sParte2;
                sTotalCapitulos = "31";
            }

            if ((sParte1 == "1Rs") || (sParte1 == "1RS") || (sParte1 == "1rs"))
            {
                snome = "1º Reis";
                sLivroBiblia = snome;
                sTipoLivro = "AT";
                sCapituloLivroBiblia = sParte2;
                sTotalCapitulos = "22";
            }

            if ((sParte1 == "2Rs") || (sParte1 == "2RS") || (sParte1 == "2rs"))
            {
                snome = "2º Reis";
                sLivroBiblia = snome;
                sTipoLivro = "AT";
                sCapituloLivroBiblia = sParte2;
                sTotalCapitulos = "25";
            }

            if ((sParte1 == "1Cr") || (sParte1 == "1CR") || (sParte1 == "1cr"))
            {
                snome = "1º Crônicas";
                sLivroBiblia = snome;
                sTipoLivro = "AT";
                sCapituloLivroBiblia = sParte2;
                sTotalCapitulos = "29";
            }

            if ((sParte1 == "2Cr") || (sParte1 == "2CR") || (sParte1 == "2cr"))
            {
                snome = "2º Crônicas";
                sLivroBiblia = snome;
                sTipoLivro = "AT";
                sCapituloLivroBiblia = sParte2;
                sTotalCapitulos = "29";
            }

            if ((sParte1 == "Ed") || (sParte1 == "ED") || (sParte1 == "ed"))
            {
                snome = "Esdras";
                sLivroBiblia = snome;
                sTipoLivro = "AT";
                sCapituloLivroBiblia = sParte2;
                sTotalCapitulos = "10";
            }

            if ((sParte1 == "Ne") || (sParte1 == "NE") || (sParte1 == "ne"))
            {
                snome = "Neemias";
                sLivroBiblia = snome;
                sTipoLivro = "AT";
                sCapituloLivroBiblia = sParte2;
                sTotalCapitulos = "13";
            }

            if ((sParte1 == "Et") || (sParte1 == "ET") || (sParte1 == "et"))
            {
                snome = "Ester";
                sLivroBiblia = snome;
                sTipoLivro = "AT";
                sCapituloLivroBiblia = sParte2;
                sTotalCapitulos = "10";
            }

            if ((sParte1 == "Jo") || (sParte1 == "JO") || (sParte1 == "jo"))
            {
                snome = "Jó";
                sLivroBiblia = snome;
                sTipoLivro = "AT";
                sCapituloLivroBiblia = sParte2;
                sTotalCapitulos = "42";
            }

            if ((sParte1 == "Jo") || (sParte1 == "JO") || (sParte1 == "jo"))
            {
                snome = "Jó";
                sLivroBiblia = snome;
                sTipoLivro = "AT";
                sCapituloLivroBiblia = sParte2;
                sTotalCapitulos = "42";
            }

            if ((sParte1 == "Sl") || (sParte1 == "SL") || (sParte1 == "sl"))
            {
                snome = "Salmos";
                sLivroBiblia = snome;
                sTipoLivro = "AT";
                sCapituloLivroBiblia = sParte2;
                sTotalCapitulos = "150";
            }

            if ((sParte1 == "Pv") || (sParte1 == "PV") || (sParte1 == "pv"))
            {
                snome = "Provérbios";
                sLivroBiblia = snome;
                sTipoLivro = "AT";
                sCapituloLivroBiblia = sParte2;
                sTotalCapitulos = "31";
            }

            if ((sParte1 == "Ec") || (sParte1 == "EC") || (sParte1 == "ec"))
            {
                snome = "Eclesiastes";
                sLivroBiblia = snome;
                sTipoLivro = "AT";
                sCapituloLivroBiblia = sParte2;
                sTotalCapitulos = "12";
            }

            if ((sParte1 == "Ct") || (sParte1 == "CT") || (sParte1 == "ct"))
            {
                snome = "Cantares";
                sLivroBiblia = snome;
                sTipoLivro = "AT";
                sCapituloLivroBiblia = sParte2;
                sTotalCapitulos = "8";
            }

            if ((sParte1 == "Is") || (sParte1 == "IS") || (sParte1 == "is"))
            {
                snome = "Isaías";
                sLivroBiblia = snome;
                sTipoLivro = "AT";
                sCapituloLivroBiblia = sParte2;
                sTotalCapitulos = "66";
            }

            if ((sParte1 == "Jr") || (sParte1 == "JR") || (sParte1 == "jr"))
            {
                snome = "Jeremias";
                sLivroBiblia = snome;
                sTipoLivro = "AT";
                sCapituloLivroBiblia = sParte2;
                sTotalCapitulos = "52";
            }

            if ((sParte1 == "Lm") || (sParte1 == "LM") || (sParte1 == "lm"))
            {
                snome = "Lamentações";
                sLivroBiblia = snome;
                sTipoLivro = "AT";
                sCapituloLivroBiblia = sParte2;
                sTotalCapitulos = "5";
            }

            if ((sParte1 == "Ez") || (sParte1 == "EZ") || (sParte1 == "ez"))
            {
                snome = "Ezequiel";
                sLivroBiblia = snome;
                sTipoLivro = "AT";
                sCapituloLivroBiblia = sParte2;
                sTotalCapitulos = "48";
            }

            if ((sParte1 == "Dn") || (sParte1 == "DN") || (sParte1 == "dn"))
            {
                snome = "Daniel";
                sLivroBiblia = snome;
                sTipoLivro = "AT";
                sCapituloLivroBiblia = sParte2;
                sTotalCapitulos = "12";
            }

            if ((sParte1 == "Os") || (sParte1 == "OS") || (sParte1 == "os"))
            {
                snome = "Oséias";
                sLivroBiblia = snome;
                sTipoLivro = "AT";
                sCapituloLivroBiblia = sParte2;
                sTotalCapitulos = "14";
            }

            if ((sParte1 == "Jl") || (sParte1 == "JL") || (sParte1 == "jl"))
            {
                snome = "Joel";
                sLivroBiblia = snome;
                sTipoLivro = "AT";
                sCapituloLivroBiblia = sParte2;
                sTotalCapitulos = "3";
            }

            if ((sParte1 == "Am") || (sParte1 == "AM") || (sParte1 == "am"))
            {
                snome = "Amós";
                sLivroBiblia = snome;
                sTipoLivro = "AT";
                sCapituloLivroBiblia = sParte2;
                sTotalCapitulos = "9";
            }

            if ((sParte1 == "Ob") || (sParte1 == "OB") || (sParte1 == "ob"))
            {
                snome = "Obadias";
                sLivroBiblia = snome;
                sTipoLivro = "AT";
                sCapituloLivroBiblia = sParte2;
                sTotalCapitulos = "1";
            }

            if ((sParte1 == "Jn") || (sParte1 == "JN") || (sParte1 == "jn"))
            {
                snome = "Jonas";
                sLivroBiblia = snome;
                sTipoLivro = "AT";
                sCapituloLivroBiblia = sParte2;
                sTotalCapitulos = "4";
            }

            if ((sParte1 == "Mq") || (sParte1 == "MQ") || (sParte1 == "mq"))
            {
                snome = "Miquéias";
                sLivroBiblia = snome;
                sTipoLivro = "AT";
                sCapituloLivroBiblia = sParte2;
                sTotalCapitulos = "7";
            }

            if ((sParte1 == "Na") || (sParte1 == "NA") || (sParte1 == "na"))
            {
                snome = "Naum";
                sLivroBiblia = snome;
                sTipoLivro = "AT";
                sCapituloLivroBiblia = sParte2;
                sTotalCapitulos = "3";
            }

            if ((sParte1 == "Hc") || (sParte1 == "HC") || (sParte1 == "hc"))
            {
                snome = "Habacuque";
                sLivroBiblia = snome;
                sTipoLivro = "AT";
                sCapituloLivroBiblia = sParte2;
                sTotalCapitulos = "3";
            }

            if ((sParte1 == "Sf") || (sParte1 == "SF") || (sParte1 == "sf"))
            {
                snome = "Sofonias";
                sLivroBiblia = snome;
                sTipoLivro = "AT";
                sCapituloLivroBiblia = sParte2;
                sTotalCapitulos = "3";
            }

            if ((sParte1 == "Ag") || (sParte1 == "AG") || (sParte1 == "ag"))
            {
                snome = "Ageu";
                sLivroBiblia = snome;
                sTipoLivro = "AT";
                sCapituloLivroBiblia = sParte2;
                sTotalCapitulos = "2";
            }

            if ((sParte1 == "Zc") || (sParte1 == "ZC") || (sParte1 == "zc"))
            {
                snome = "Zacarias";
                sLivroBiblia = snome;
                sTipoLivro = "AT";
                sCapituloLivroBiblia = sParte2;
                sTotalCapitulos = "14";
            }

            if ((sParte1 == "Ml") || (sParte1 == "ML") || (sParte1 == "ml"))
            {
                snome = "Malaquias";
                sLivroBiblia = snome;
                sTipoLivro = "AT";
                sCapituloLivroBiblia = sParte2;
                sTotalCapitulos = "4";
            }
            // FIM DO ANTIGO TESTAMENTO

            // NOVO TESTAMENTO
            if ((sParte1 == "Mt") || (sParte1 == "MT") || (sParte1 == "mt"))
            {
                snome = "Mateus";
                sLivroBiblia = snome;
                sTipoLivro = "NT";
                sCapituloLivroBiblia = sParte2;
                sTotalCapitulos = "28";
            }

            if ((sParte1 == "Mc") || (sParte1 == "MC") || (sParte1 == "mc"))
            {
                snome = "Marcos";
                sLivroBiblia = snome;
                sTipoLivro = "NT";
                sCapituloLivroBiblia = sParte2;
                sTotalCapitulos = "16";
            }

            if ((sParte1 == "Lc") || (sParte1 == "LC") || (sParte1 == "lc"))
            {
                snome = "Lucas";
                sLivroBiblia = snome;
                sTipoLivro = "NT";
                sCapituloLivroBiblia = sParte2;
                sTotalCapitulos = "24";
            }

            if ((sParte1 == "Jo") || (sParte1 == "JO") || (sParte1 == "jo"))
            {
                snome = "João";
                sLivroBiblia = snome;
                sTipoLivro = "NT";
                sCapituloLivroBiblia = sParte2;
                sTotalCapitulos = "21";
            }

            if ((sParte1 == "At") || (sParte1 == "AT") || (sParte1 == "at"))
            {
                snome = "Atos";
                sLivroBiblia = snome;
                sTipoLivro = "NT";
                sCapituloLivroBiblia = sParte2;
                sTotalCapitulos = "28";
            }

            if ((sParte1 == "Rm") || (sParte1 == "RM") || (sParte1 == "rm"))
            {
                snome = "Romanos";
                sLivroBiblia = snome;
                sTipoLivro = "NT";
                sCapituloLivroBiblia = sParte2;
                sTotalCapitulos = "16";
            }

            if ((sParte1 == "1Co") || (sParte1 == "1CO") || (sParte1 == "1co"))
            {
                snome = "1ª Coríntios";
                sLivroBiblia = snome;
                sTipoLivro = "NT";
                sCapituloLivroBiblia = sParte2;
                sTotalCapitulos = "16";
            }

            if ((sParte1 == "2Co") || (sParte1 == "2CO") || (sParte1 == "2co"))
            {
                snome = "2ª Coríntios";
                sLivroBiblia = snome;
                sTipoLivro = "NT";
                sCapituloLivroBiblia = sParte2;
                sTotalCapitulos = "13";
            }

            if ((sParte1 == "Gl") || (sParte1 == "GL") || (sParte1 == "gl"))
            {
                snome = "Gálatas";
                sLivroBiblia = snome;
                sTipoLivro = "NT";
                sCapituloLivroBiblia = sParte2;
                sTotalCapitulos = "6";
            }

            if ((sParte1 == "Ef") || (sParte1 == "EF") || (sParte1 == "ef"))
            {
                snome = "Efésios";
                sLivroBiblia = snome;
                sTipoLivro = "NT";
                sCapituloLivroBiblia = sParte2;
                sTotalCapitulos = "6";
            }

            if ((sParte1 == "Fp") || (sParte1 == "FP") || (sParte1 == "fp"))
            {
                snome = "Filipenses";
                sLivroBiblia = snome;
                sTipoLivro = "NT";
                sCapituloLivroBiblia = sParte2;
                sTotalCapitulos = "4";
            }

            if ((sParte1 == "Cl") || (sParte1 == "CL") || (sParte1 == "cl"))
            {
                snome = "Colossenses";
                sLivroBiblia = snome;
                sTipoLivro = "NT";
                sCapituloLivroBiblia = sParte2;
                sTotalCapitulos = "4";
            }

            if ((sParte1 == "1Ts") || (sParte1 == "1TS") || (sParte1 == "1ts"))
            {
                snome = "1ª Tessalonicenses";
                sLivroBiblia = snome;
                sTipoLivro = "NT";
                sCapituloLivroBiblia = sParte2;
                sTotalCapitulos = "5";
            }

            if ((sParte1 == "2Ts") || (sParte1 == "2TS") || (sParte1 == "2ts"))
            {
                snome = "2ª Tessalonicenses";
                sLivroBiblia = snome;
                sTipoLivro = "NT";
                sCapituloLivroBiblia = sParte2;
                sTotalCapitulos = "3";
            }

            if ((sParte1 == "1Tm") || (sParte1 == "1TM") || (sParte1 == "1tm"))
            {
                snome = "1ª Timóteo";
                sLivroBiblia = snome;
                sTipoLivro = "NT";
                sCapituloLivroBiblia = sParte2;
                sTotalCapitulos = "6";
            }

            if ((sParte1 == "2Tm") || (sParte1 == "2TM") || (sParte1 == "2tm"))
            {
                snome = "2ª Timóteo";
                sLivroBiblia = snome;
                sTipoLivro = "NT";
                sCapituloLivroBiblia = sParte2;
                sTotalCapitulos = "4";
            }

            if ((sParte1 == "Tt") || (sParte1 == "TT") || (sParte1 == "tt"))
            {
                snome = "Tito";
                sLivroBiblia = snome;
                sTipoLivro = "NT";
                sCapituloLivroBiblia = sParte2;
                sTotalCapitulos = "3";
            }

            if ((sParte1 == "Fm") || (sParte1 == "FM") || (sParte1 == "fm"))
            {
                snome = "Filemom";
                sLivroBiblia = snome;
                sTipoLivro = "NT";
                sCapituloLivroBiblia = sParte2;
                sTotalCapitulos = "1";
            }

            if ((sParte1 == "Hb") || (sParte1 == "HB") || (sParte1 == "hb"))
            {
                snome = "Hebreus";
                sLivroBiblia = snome;
                sTipoLivro = "NT";
                sCapituloLivroBiblia = sParte2;
                sTotalCapitulos = "13";
            }

            if ((sParte1 == "Tg") || (sParte1 == "TG") || (sParte1 == "tg"))
            {
                snome = "Tiago";
                sLivroBiblia = snome;
                sTipoLivro = "NT";
                sCapituloLivroBiblia = sParte2;
                sTotalCapitulos = "5";
            }

            if ((sParte1 == "1Pe") || (sParte1 == "1PE") || (sParte1 == "1pe"))
            {
                snome = "1ª Pedro";
                sLivroBiblia = snome;
                sTipoLivro = "NT";
                sCapituloLivroBiblia = sParte2;
                sTotalCapitulos = "5";
            }

            if ((sParte1 == "2Pe") || (sParte1 == "2PE") || (sParte1 == "2pe"))
            {
                snome = "2ª Pedro";
                sLivroBiblia = snome;
                sTipoLivro = "NT";
                sCapituloLivroBiblia = sParte2;
                sTotalCapitulos = "3";
            }

            if ((sParte1 == "1Jo") || (sParte1 == "1JO") || (sParte1 == "1jo"))
            {
                snome = "1ª João";
                sLivroBiblia = snome;
                sTipoLivro = "NT";
                sCapituloLivroBiblia = sParte2;
                sTotalCapitulos = "5";
            }

            if ((sParte1 == "2Jo") || (sParte1 == "2JO") || (sParte1 == "2jo"))
            {
                snome = "2ª João";
                sLivroBiblia = snome;
                sTipoLivro = "NT";
                sCapituloLivroBiblia = sParte2;
                sTotalCapitulos = "1";
            }

            if ((sParte1 == "3Jo") || (sParte1 == "3JO") || (sParte1 == "3jo"))
            {
                snome = "3ª João";
                sLivroBiblia = snome;
                sTipoLivro = "NT";
                sCapituloLivroBiblia = sParte2;
                sTotalCapitulos = "1";
            }

            if ((sParte1 == "Jd") || (sParte1 == "JD") || (sParte1 == "jd"))
            {
                snome = "Judas";
                sLivroBiblia = snome;
                sTipoLivro = "NT";
                sCapituloLivroBiblia = sParte2;
                sTotalCapitulos = "1";
            }

            // 27/06/2021 11:00h - Fazer pesquisa também por AP de Apocalipse
            // Leonardo Metelis
            if ((sParte1 == "Rv") || (sParte1 == "RV") || (sParte1 == "rv") || (sParte1 == "Ap") || (sParte1 == "ap") || (sParte1 == "AP"))
            {
                snome = "Revelações";
                sLivroBiblia = snome;
                sTipoLivro = "NT";
                sCapituloLivroBiblia = sParte2;
                sTotalCapitulos = "22";
            }

            // FIM DO NOVO TESTAMENTO

            sdesc = sParte2;
            sver = sParte3;

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
                            if (sNumeroVersiculo == sver)
                            {
                                AchouVersiculo = true;
                            }
                            else
                            {
                                AchouVersiculo = false;
                                sNumeroVersiculo = "";
                            }

                        }


                        break;


                    case XmlNodeType.Text:


                        // Pega o capitulo do livro
                        if ((AchouLivro == true) && (AchouCapitulo == true) && (AchouVersiculo == true) && (TagName == "v"))
                        {
                            sTextoVersiculo = xReader.Value;  // ler o texto do versiculo
                            sTextoVersiculoCapituloLivroBiblia = sTextoVersiculo;

                            // Adiciona na lista 
                            lista_ref.Add(sTextoVersiculo);

                            // 07/06/2021 18:11 
                            // Ler se existe um arquivo com as pesquisas. Se sim, mostrar ao usuário e acrescentar a última pesquisa
                            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                            string filename = System.IO.Path.Combine(path, "pesquisa.txt");
                            var list = new List<string>();
                            int contalinhas = 0;
                            string snewline = sTextoVersiculo;
                            string slinha = "";

                            // Se o arquivo existir pega os itens dele e joga numa lista
                            if (System.IO.File.Exists(filename))
                            {
                                //Se o arquivo existir, pega a lista de dados dele
                                lista = new ArrayList();

                                using (var streamReader = new StreamReader(filename))
                                {
                                    string line;
                                    while ((line = streamReader.ReadLine()) != null)
                                    {
                                        // Só acrescenta os não repetidos
                                        if (sAux != line)
                                        {
                                            list.Add(line);
                                        }
                                        
                                        sAux = line;
                                    }
                                }

                                list.Add(snewline); // Adiciona o novo item a lista

                                // Deletar o arquivo antigo
                                System.IO.File.Delete(filename);

                                string path3 = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                                string filename3 = System.IO.Path.Combine(path3, "pesquisa.txt");
                                int w = list.Count;
                                for (int x = 0; x < w; x++)
                                {
                                    slinha = list[x].ToString();
                                    using (var streamWriter = new StreamWriter(filename3, true))
                                    {
                                        streamWriter.WriteLine(slinha);
                                    }
                                }

                                
                            }
                            else
                            {
                                // 07/06/2021 18:18h
                                // Cria um arquivo e salva a pesquisa dentro dele no final do arquivo
                                string path3 = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                                string filename3 = System.IO.Path.Combine(path3, "pesquisa.txt");
                                string line = sTextoVersiculo;
                                int w = lista_ref.Count;
                                for (int x = 0; x < w; x++)
                                {
                                    line = lista_ref[x].ToString();
                                    using (var streamWriter = new StreamWriter(filename3, true))
                                    {
                                        streamWriter.WriteLine(line);
                                    }
                                }

                            }
                           
                            sVersiculoCapituloLivroBiblia = sNumeroVersiculo;
                            PrimeirVersculo = Int32.Parse(sNumeroVersiculo);
                            UltimoVersiculo = PrimeirVersculo + 1; // Define o último versiculo como o próximo a ser exibido

                        }

                        break;

                } // fim so swhitch 

            } // fim do loop


        } // PESQUISA POR REFERÊNCIA DE LIVRO E VERSÍCULO

    }

}