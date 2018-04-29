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
using projet_chat.Adapters;
using projet_chat.Modeles;

namespace projet_chat.Activitys
{
    [Activity(Label = "Abonnement")]
    public class AbonnementActivity : Activity
    {
        int idUser;
        ListView lstSujetAbon;
        SujetAdapter adapter;
        Database db;
        List<Sujet> lesSujets;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.AbonnementLayout);

            idUser = Intent.GetIntExtra("idUser", 0);
            db = new Database();
            lesSujets = db.getAllSujets();

            lstSujetAbon = FindViewById<ListView>(Resource.Id.lstSujetsAbon);
            adapter = new SujetAdapter(this, lesSujets);
            lstSujetAbon.Adapter = adapter;
            
            if(lesSujets.Count == 0)
            {
                Toast.MakeText(this, "Pourpour l'instant il n'existe pas de sujets!", ToastLength.Long).Show();
            }

            lstSujetAbon.ItemClick += LstSujetAbon_ItemClick;
        }

        private void LstSujetAbon_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            Sujet s = lesSujets.ElementAt(e.Position);
            Abonnement a = new Abonnement() { idUserAbon = idUser, NomSujetAbon = s.nomSujet };
            var checkAbon = db.getAllAbonnementByIdUser(idUser).Find(x => x.NomSujetAbon == s.nomSujet);
            if (checkAbon == null)
            {
                db.addAbonnement(a);
                Intent intent = new Intent(this, typeof(SujetActivity));
                intent.PutExtra("idUser", idUser);
                StartActivity(intent);
            }
            else
            {
                Toast.MakeText(this, "Vous etes dèja abonner à cet sujet!", ToastLength.Short).Show();
            }
        }
    }
}