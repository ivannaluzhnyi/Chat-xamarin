using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;

namespace projet_chat.Modeles
{
    public class Database
    {
        private string dossier = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
        private SQLiteConnection db;

        public Database()
        {
            db = new SQLiteConnection(Path.Combine(dossier, "chat_Project.db"));
            if (File.Exists(Path.Combine(dossier, "chat_Project.db")) == false)
            {
                db.CreateTable<User>();
                db.CreateTable<Sujet>();
                db.CreateTable<Message>();
                db.CreateTable<Abonnement>();
            }
            db.CreateTable<User>();
            db.CreateTable<Sujet>();
            db.CreateTable<Message>();
            db.CreateTable<Abonnement>();


        }

        public Abonnement getAbonnementByIdUser(int id)
        {
            return db.Find<Abonnement>(x => x.idUserAbon == id);
        }

        public Sujet getSujetByName(string name)
        {
            return db.Find<Sujet>(x => x.nomSujet == name);
        }

        public List<Abonnement> getAllAbonnementByIdUser(int id)
        {
            List<Abonnement> lesAbonnements = new List<Abonnement>();
            foreach (Abonnement ab in db.Table<Abonnement>().ToList<Abonnement>())
            {
                if (ab.idUserAbon == id)
                {
                    lesAbonnements.Add(ab);
                }
            }
            return lesAbonnements;
        }

        public void addAbonnement(Abonnement ab)
        {
            db.Insert(ab);
        }

        public List<Abonnement> getAllAbonnements()
        {
            return db.Table<Abonnement>().ToList<Abonnement>();
        }

        public void addMessage(Message m)
        {
            db.Insert(m);
        }

        public List<Message> getAllMessegesByIdSujet(int id)
        {
            List<Message> lesMessage = new List<Message>();
            foreach (Message ms in db.Table<Message>().ToList<Message>())
            {
                if (ms.idSujet == id)
                {
                    lesMessage.Add(ms);
                }
            }
            return lesMessage;
        }

        public void addUser(User u)
        {
            db.Insert(u);
        }

        public void addSujet( Sujet ab)
        {
            db.Insert(ab);
        }

        public Sujet getSujetByPosition(int index)
        {
            return db.Table<Sujet>().ElementAt(index);
        }

        public List<Sujet> getAllSujets()
        {
            return db.Table<Sujet>().ToList<Sujet>();
        }


        public List<Sujet> getAllSujetsByUser(int id)
        {
            List<Sujet> lesSujetsByUser = new List<Sujet>();
            foreach (Sujet ab in db.Table<Sujet>().ToList<Sujet>())
            {
                if (ab.idUser == id)
                {
                    lesSujetsByUser.Add(ab);
                }
            }
            return lesSujetsByUser;
        } 

        public Sujet getSujetByIdUser(int id)
        {
            return db.Find<Sujet>(x => x.idUser == id);
        }

        public List<User> getAllUsers()
        {
            return db.Table<User>().ToList<User>();
        }

        public User getUserById(int id)
        {
            return db.Find<User>(x => x.idUser == id);
        }
    }
}