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
using Newtonsoft.Json;
using projet_chat.Adapters;
using projet_chat.Modeles;

namespace projet_chat.Activitys
{
    [Activity(Label = "MessageActivity")]
    public class MessageActivity : Activity
    {
        Database db;
        int idSujet;
        int idUser;
        Button btnAddMessage;
        ListView lstMessage;
       // List<Modeles.Message> lesMessages;
        MessageAdapter adapter;
        //SujetAdapter adapterSujet;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.MessageLayout);
            db = new Database();

            idSujet = Intent.GetIntExtra("idSujet",0);
            idUser = Intent.GetIntExtra("idUser", 0);

            btnAddMessage = FindViewById<Button>(Resource.Id.btnAddMessage);
            lstMessage = FindViewById<ListView>(Resource.Id.lstMessage);

            btnAddMessage.Click += BtnAddMessage_Click;



            if(db.getAllMessegesByIdSujet(idSujet).Count != 0)
            {
                this.getListeMessage();
            }

        }

        private void BtnAddMessage_Click(object sender, EventArgs e)
        {
            this.getDialogNewMessage();
        }

        public void getListeMessage()
        {
            List<Modeles.Message> lesMessages = new List<Modeles.Message>();
            lesMessages = db.getAllMessegesByIdSujet(idSujet);
            adapter = new MessageAdapter(this, lesMessages);
            lstMessage.Adapter = adapter;
        }



        public void getDialogNewMessage()
        {
            View view = LayoutInflater.Inflate(Resource.Layout.createMessageDialogLayout, null);
            AlertDialog alertDialog = new AlertDialog.Builder(this).Create();

            alertDialog.SetView(view);
            alertDialog.SetCanceledOnTouchOutside(false);

            Button btnEnvoyerMessage = view.FindViewById<Button>(Resource.Id.btnEnvoyerMessageDialog);
            Button btnExit = view.FindViewById<Button>(Resource.Id.btnSortirMessageDialog);
            EditText txtMessage = view.FindViewById<EditText>(Resource.Id.txtMessageDialog);


            btnExit.Click += delegate
            {
                alertDialog.Dismiss();
            };

            btnEnvoyerMessage.Click += delegate
            {
                var message = txtMessage.Text;
                if (message != "")
                {
                    Random aleatoire = new Random();
                    int idRandom = aleatoire.Next();
                    Modeles.Message m = new Modeles.Message() { idMessage = idRandom, idSujet = idSujet, idCreateur = idUser, textMessage = message };
                    db.addMessage(m);
                    alertDialog.Dismiss();
                    this.getListeMessage();

                    //List<Sujet> lesSujs = new List<Sujet>();
                    //lesSujs = db.getAllAbonementsByUser(idUser);
                    //adapterSujet = new SujetAdapter(this, lesSujs);
                    //ListView lstSujet = FindViewById<ListView>(Resource.Id.lstSujet);
                    //lstSujet.Adapter = adapterSujet;
                }
                else
                {
                    Toast.MakeText(this, "Veuillez saisir le message!", ToastLength.Short).Show();
                }

            };

            alertDialog.Show();
        }
    }
}