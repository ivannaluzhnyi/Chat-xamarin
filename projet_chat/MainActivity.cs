using Android.App;
using Android.Widget;
using Android.OS;
using projet_chat.Modeles;
using static System.Net.Mime.MediaTypeNames;
using System.Collections.Generic;
using Android.Content;
using projet_chat.Activitys;
using static Android.Bluetooth.BluetoothClass;

namespace projet_chat
{
    [Activity(Label = "Connexion", MainLauncher = true)]
    public class MainActivity : Activity
    {
        Database db;
        EditText txtLogin;
        EditText txtPassword;
        Button btnValiderConnexion;
        List<User> lesUsers;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);

            User u1 = new User() { idUser = 1, login = "Ivan", nomUser = "Naluzhnyi", password = "iv", prenomUser = "Ivan", image=2130837504 };
            User u2 = new User() { idUser = 2, login = "Sandra", nomUser = "Lefebvre", password = "sa", prenomUser = "Sandra", image = 2130837505 };
            User u3 = new User() { idUser = 3, login = "Jacques", nomUser = "Buffeteau", password = "ja", prenomUser = "Jacques ",image = 2130837506 };
            User u4 = new User() { idUser = 4, login = "Thomas", nomUser = "Perna", password = "th", prenomUser = "Thomas", image= 2130837507 };

            db = new Database();
            if (db.getAllUsers().Count == 0)
            {
                db.addUser(u1);
                db.addUser(u2);
                db.addUser(u3);
                db.addUser(u4);
            }

            txtLogin = FindViewById<EditText>(Resource.Id.txtLogin);
            txtPassword = FindViewById<EditText>(Resource.Id.txtPassword);
            btnValiderConnexion = FindViewById<Button>(Resource.Id.btnValiderConnexion);

            btnValiderConnexion.Click += BtnValiderConnexion_Click;
        }

        private void BtnValiderConnexion_Click(object sender, System.EventArgs e)
        {
            lesUsers = db.getAllUsers();
            var chekLog = lesUsers.Find(x => x.login == txtLogin.Text);
            var checkPass = lesUsers.Find(x => x.password == txtPassword.Text);

            if (chekLog != null && checkPass != null)
            {
                Intent intent = new Intent(this, typeof(SujetActivity));
                intent.PutExtra("idUser", chekLog.idUser);
                StartActivity(intent);
            }
            else
            {
                Toast.MakeText(this, "Connexion ipossible!", ToastLength.Long).Show();
            }
        
        }
    }
}

