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
using projet_chat.Modeles;

namespace projet_chat.Adapters
{
    public class SujetAdapter : ArrayAdapter<Sujet>
    {
        Activity context;
        List<Sujet> lesAbonnements;

        public SujetAdapter(Activity unContext, List<Sujet> desAbonnements)
            : base(unContext, Resource.Layout.ItemSujet, desAbonnements)
        {
            context = unContext;
            lesAbonnements = desAbonnements;
        }


        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            Database db = new Database();
            User user = db.getUserById(lesAbonnements[position].idUser);
            var imgUser = user.image;
            //var imgRessource = Resource.Drawable. + imgUser;
            List<Modeles.Message> lesMessages = new List<Modeles.Message>();
            lesMessages = db.getAllMessegesByIdSujet(lesAbonnements[position].idSujet);

            var view = context.LayoutInflater.Inflate(Resource.Layout.ItemSujet, null);
            view.FindViewById<TextView>(Resource.Id.txtNomSujetItem).Text = lesAbonnements[position].nomSujet;
            view.FindViewById<TextView>(Resource.Id.txtNbMessageItem).Text = lesMessages.Count.ToString() ;
            view.FindViewById<TextView>(Resource.Id.txtNomUserItem).Text = user.nomUser;
            view.FindViewById<TextView>(Resource.Id.txtPrenomUserItem).Text = user.prenomUser;

            // un peu de triche :)
            if(imgUser == 2130837504) {  view.FindViewById<ImageView>(Resource.Id.imgUser).SetImageResource(Resource.Drawable.Image1); }
            if(imgUser == 2130837505) { view.FindViewById<ImageView>(Resource.Id.imgUser).SetImageResource(Resource.Drawable.Image2); }
            if(imgUser == 2130837506) { view.FindViewById<ImageView>(Resource.Id.imgUser).SetImageResource(Resource.Drawable.Image3); }
            if(imgUser == 2130837507) { view.FindViewById<ImageView>(Resource.Id.imgUser).SetImageResource(Resource.Drawable.Image4); }

            return view;
        }
    }
}