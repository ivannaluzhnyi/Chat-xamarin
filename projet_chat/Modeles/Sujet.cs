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
using SQLite;

namespace projet_chat.Modeles
{
    public class Sujet
    {
        [PrimaryKey, AutoIncrement]
        [JsonProperty("ID")]
        public int idSujet { get; set; }
        public string nomSujet { get; set; }
        public int idUser { get; set; }
    }

}