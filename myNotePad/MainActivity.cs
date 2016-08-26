using System;
using System.IO;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Content.PM;

namespace myNotePad
{
    [Activity(ConfigurationChanges = ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait, Label = "NeatNotes", MainLauncher = false, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        ListView lvNoteList;
        List<Notes> myNoteList;
        EditText txtSearch;
        static string dbName = "NeatNotes.sqlite";
        string dbPath = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.ToString(), dbName);
        DatabaseManager objDb;
        DataStorage objDs;

        protected override void OnCreate(Bundle savedInstanceState)
        { 
            //if (savedInstanceState != null)
            //{
            //    searchstring = savedInstanceState.GetString("_searchstring", "");
            //    txtSearch.Text = searchstring;
            //    PopulateNoteList();
            //}
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            lvNoteList = FindViewById<ListView>(Resource.Id.lvNoteList);
            txtSearch = FindViewById<EditText>(Resource.Id.txtSearch);

            CopyDatabase();

            PopulateNoteList(txtSearch.Text);

            lvNoteList.ItemClick += OnLvNoteListItemClick;
            txtSearch.TextChanged += TxtSearch_TextChanged;
        }

        private void PopulateNoteList(string searchstring)
        {
            objDb = new DatabaseManager();

            if (searchstring != "")
            {
                myNoteList = objDb.SearchNotes(searchstring);
                lvNoteList.Adapter = new DataAdapter(this, myNoteList);
                //txtSearch.RequestFocus();
                txtSearch.SetSelection(txtSearch.Length());
                return;
            }
            myNoteList = objDb.ViewAllNotes();
            lvNoteList.Adapter = new DataAdapter(this, myNoteList);
            //txtSearch.RequestFocus();
            txtSearch.SetSelection(txtSearch.Length());
        }

        private void RestoreSettings()
        {
            objDs = new DataStorage(this);
            txtSearch.Text = objDs.getSearchString();
        }

        private void SaveSettings()
        {
            objDs = new DataStorage(this);
            objDs.saveSearchString(txtSearch.Text);      
        }

        //protected override void OnSaveInstanceState(Bundle savedInstanceState)
        //{
        //    savedInstanceState.PutString("_searchstring", searchstring);

        //    base.OnSaveInstanceState(savedInstanceState);
        //}

        //protected override void OnRestoreInstanceState(Bundle savedInstanceState)
        //{
        //    if (savedInstanceState != null)
        //    {
        //        searchstring = savedInstanceState.GetString("_searchstring", "");  
        //        PopulateNoteList();
        //        txtSearch.Text = searchstring;
        //    }
        //    base.OnRestoreInstanceState(savedInstanceState);
        //}


        private void TxtSearch_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            PopulateNoteList(txtSearch.Text);
        }

        private void OnLvNoteListItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var NotesItem = myNoteList[e.Position];
            var editnote = new Intent(this, typeof(EditNote));

            editnote.PutExtra("Title", NotesItem.Title);
            editnote.PutExtra("Details", NotesItem.NoteDetails);
            editnote.PutExtra("NoteId", NotesItem.NoteId);

            StartActivity(editnote);
        }

        public void CopyDatabase()
        {
            if (!File.Exists(dbPath))
            {
                using (BinaryReader br = new BinaryReader(Assets.Open(dbName)))
                {
                    using (BinaryWriter bw = new BinaryWriter(new FileStream(dbPath, FileMode.Create)))
                    {
                        byte[] buffer = new byte[2048];
                        int len = 0;
                        while ((len = br.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            bw.Write(buffer, 0, len);
                        }

                    }
                }
            }
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            menu.Add("Add");
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            var itemTitle = item.TitleFormatted.ToString();
            switch (itemTitle)
            {
                case "Add":
                    StartActivity(typeof(AddNote));
                    break;
            }
            return base.OnOptionsItemSelected(item);
        }

        protected override void OnPause()
        {
            SaveSettings();
            base.OnPause();
        }

        protected override void OnResume()
        {
            RestoreSettings();
            PopulateNoteList(txtSearch.Text);
            base.OnResume();
        }

    }
}

