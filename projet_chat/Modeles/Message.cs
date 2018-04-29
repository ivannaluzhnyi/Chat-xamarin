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

namespace projet_chat.Modeles
{
    public class Message
    {
        public int idMessage { get; set; }
        public string textMessage { get; set; }
        public int idSujet { get; set; }
        public int idCreateur { get; set; }
    }
}