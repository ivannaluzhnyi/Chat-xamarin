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
    public class MessageAdapter : ArrayAdapter<Modeles.Message>
    {
        Activity context;
        List<Modeles.Message> lesMessages;

        public MessageAdapter(Activity unContext, List<Modeles.Message> desMessages)
            : base(unContext, Resource.Layout.ItemMessage, desMessages)
        {
            context = unContext;
            lesMessages = desMessages;
        }


        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            Database db = new Database();
            User user = db.getUserById(lesMessages[position].idCreateur);
            var imgUser = user.image;
            var view = context.LayoutInflater.Inflate(Resource.Layout.ItemMessage, null);
            view.FindViewById<TextView>(Resource.Id.txtTextMessageItemM).Text = lesMessages[position].textMessage;

            // un peu de triche :)
            if (imgUser == 2130837504) { view.FindViewById<ImageView>(Resource.Id.imgUser).SetImageResource(Resource.Drawable.Image1); }
            if (imgUser == 2130837505) { view.FindViewById<ImageView>(Resource.Id.imgUser).SetImageResource(Resource.Drawable.Image2); }
            if (imgUser == 2130837506) { view.FindViewById<ImageView>(Resource.Id.imgUser).SetImageResource(Resource.Drawable.Image3); }
            if (imgUser == 2130837507) { view.FindViewById<ImageView>(Resource.Id.imgUser).SetImageResource(Resource.Drawable.Image4); }


            return view;
        }
    }
}
    
    
