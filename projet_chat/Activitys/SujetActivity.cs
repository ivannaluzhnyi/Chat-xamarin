using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Timers;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using projet_chat.Adapters;
using projet_chat.Modeles;
using static Android.Content.ClipData;

namespace projet_chat.Activitys
{
    [Activity(Label = "SujetActivity")]
    public class SujetActivity : Activity
    {
        Database db;
        int idUser;
        Button btnSaboner;
        Button btnCreate;
        ListView lstSujet;
        SujetAdapter adapter;
        List<Sujet> lesSujets;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.SujetLayout);

            db = new Database();
            lesSujets = new List<Sujet>();

            btnCreate = FindViewById<Button>(Resource.Id.btnCreat);
            btnSaboner = FindViewById<Button>(Resource.Id.btnAbonner);
            lstSujet = FindViewById<ListView>(Resource.Id.lstSujet);

            idUser = Intent.GetIntExtra("idUser", 0);        
            if (db.getSujetByIdUser(idUser) != null )
            {
                this.getListeSujets();
            }
            else
            {
                if (db.getAbonnementByIdUser(idUser) != null)
                {
                    this.getListeSujetsJustAbo();
                }
                else
                {
                    this.getDialog();
                }
            }

            btnCreate.Click += BtnCreate_Click;
            lstSujet.ItemClick += LstSujet_ItemClick;
            btnSaboner.Click += BtnSaboner_Click;
        }

        private void BtnSaboner_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(AbonnementActivity));
            intent.PutExtra("idUser", idUser);
            StartActivity(intent);
        }

        private void LstSujet_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {   
            Intent intent = new Intent(this, typeof(MessageActivity));
            intent.PutExtra("idUser", idUser);
            intent.PutExtra("idSujet", lesSujets.ElementAt(e.Position).idSujet);
            StartActivity(intent);
        }

        private void BtnCreate_Click(object sender, EventArgs e)
        {
            this.getDialogCreateSujet();
        }

        public void getListeSujets()
        {
            adapter = new SujetAdapter(this, this.getAllSujets());
            lstSujet.Adapter = adapter; 
        }

        public void getListeSujetsJustAbo()
        {
           // List<Sujet> lsS = new List<Sujet>();
            foreach (Abonnement ab in db.getAllAbonnementByIdUser(idUser))
            {
                Sujet s = db.getSujetByName(ab.NomSujetAbon);
                var ch = lesSujets.Find(x => x.nomSujet == s.nomSujet);
                if (ch == null)
                {
                    lesSujets.Add(s);
                }
            }
            adapter = new SujetAdapter(this, lesSujets);
            lstSujet.Adapter = adapter;
        }

        public List<Sujet> getAllSujets()
        {
            lesSujets = db.getAllSujetsByUser(idUser);
            foreach (Abonnement ab in db.getAllAbonnementByIdUser(idUser))
            {
                Sujet s = db.getSujetByName(ab.NomSujetAbon);
                var ch = lesSujets.Find(x => x.nomSujet == s.nomSujet);
                if (ch == null)
                {
                    lesSujets.Add(s);
                }
            }
            return lesSujets;
        }

        public AlertDialog getDialog()
        {
            AlertDialog.Builder alertDialog = new AlertDialog.Builder(this);
            alertDialog.SetMessage("Vous n'etes abonné à aucun sujet!");
            alertDialog.SetNeutralButton("ok", delegate
            {
                alertDialog.Dispose();
            });
            return alertDialog.Show();
        }

        public void getDialogCreateSujet()
        {
            View view = LayoutInflater.Inflate(Resource.Layout.createSujetDialogLayout, null);
            AlertDialog alertDialog = new AlertDialog.Builder(this).Create();

            alertDialog.SetView(view);
            alertDialog.SetCanceledOnTouchOutside(false);

            Button btnValiderCreate = view.FindViewById<Button>(Resource.Id.btnValiderCreatSujet);
            Button btnExit = view.FindViewById<Button>(Resource.Id.btnExit);
            EditText txtSujet = view.FindViewById<EditText>(Resource.Id.txtNomSujet);


            btnExit.Click += delegate
            {
                alertDialog.Dismiss();
            };

            btnValiderCreate.Click += delegate
            {
                var nomSujet = txtSujet.Text;
                var checkName = db.getAllSujets().Find(x => x.nomSujet == txtSujet.Text);
                if (nomSujet != "")
                {
                    if(checkName == null)
                    {
                        Random aleatoire = new Random();
                        int idRandom = aleatoire.Next();

                        Sujet s = new Sujet() { idSujet = idRandom, idUser = idUser, nomSujet = nomSujet };
                        Abonnement a = new Abonnement() { NomSujetAbon = s.nomSujet, idUserAbon = idUser };

                        db.addSujet(s);
                        db.addAbonnement(a);

                        alertDialog.Dismiss();
                        Toast.MakeText(this, "Le sujet \"" + nomSujet + "\" a été créé", ToastLength.Long).Show();
                        this.getListeSujets();
                    }
                    else
                    {
                        Toast.MakeText(this, "Le sujet avec le nom \"" + txtSujet.Text + "\" existe dèja!", ToastLength.Short).Show();
                    }
                   
                }
                else
                {
                    Toast.MakeText(this, "Veuillez saisir le nom du sujet!", ToastLength.Short).Show();
                }

            };

            alertDialog.Show();
        }
    }
}