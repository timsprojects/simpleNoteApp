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
using Android.Content.PM;

namespace myNotePad
{
    [Activity(ConfigurationChanges = ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait, Label = "AddNote")]
    public class AddNote : Activity
    {
        Button btnAddNote;
        EditText txtNoteTitle;
        EditText txtNoteDescription;

        DatabaseManager objdb = new DatabaseManager();
        DataStorage objDs;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Create your application here
            SetContentView(Resource.Layout.AddNote);

            btnAddNote = FindViewById<Button>(Resource.Id.btnAddNote);
            txtNoteTitle = FindViewById<EditText>(Resource.Id.txtNoteTitle);
            txtNoteDescription = FindViewById<EditText>(Resource.Id.txtNoteDescription);

            btnAddNote.Click += OnBtnAddNoteClick;
        }

        private void OnBtnAddNoteClick(object sender, EventArgs e)
        {
            if (txtNoteTitle.Text != "" && txtNoteDescription.Text != "")
            {    
                objdb.AddNote(txtNoteTitle.Text, txtNoteDescription.Text);
                Toast.MakeText(this, "Note Added", ToastLength.Long).Show();
                ClearSavedData();
                this.Finish();
                StartActivity(typeof(MainActivity));
            }
        }

        private void SaveSettings()
        {
            objDs = new DataStorage(this);
            objDs.saveNoteTitle(txtNoteTitle.Text);
            objDs.saveNoteDetails(txtNoteDescription.Text);
        }

        private void ClearSavedData()
        {
            txtNoteTitle.Text = "";
            txtNoteDescription.Text = "";
            SaveSettings();
        }

        private void RestoreSettings()
        {
            objDs = new DataStorage(this);
            txtNoteTitle.Text = objDs.getNoteTitle();
            txtNoteDescription.Text = objDs.getNoteDetails();
            if (txtNoteTitle.Text != "" && txtNoteDescription.Text != "")
            {
                txtNoteDescription.RequestFocus();
                txtNoteDescription.SetSelection(txtNoteDescription.Length());
                return;
            }
            txtNoteTitle.RequestFocus();    
        }

        protected override void OnPause()
        {
            SaveSettings();
            base.OnPause();
        }

        protected override void OnResume()
        {
            RestoreSettings();
            base.OnResume();
        }

       
    }
}