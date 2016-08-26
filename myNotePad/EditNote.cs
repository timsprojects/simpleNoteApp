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
    [Activity(ConfigurationChanges = ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait, Label = "EditNote")]
    public class EditNote : Activity
    {
        int NoteId;
        string Title;
        string Details;

        EditText txtEditNoteTitle;
        EditText txtEditNoteDescription;
        Button btnEditNote;
        Button btnDeleteNote;
        DatabaseManager objDB;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Create your application here
            SetContentView(Resource.Layout.EditNote);

            txtEditNoteTitle = FindViewById<EditText>(Resource.Id.txtEditNoteTitle);
            txtEditNoteDescription = FindViewById<EditText>(Resource.Id.txtEditNoteDescription);

            btnEditNote = FindViewById<Button>(Resource.Id.btnEditNote);
            btnDeleteNote = FindViewById<Button>(Resource.Id.btnDeleteNote);

            btnEditNote.Click += OnBtnEditNoteClick;
            btnDeleteNote.Click += OnBtnDeleteNoteClick;

            NoteId = Intent.GetIntExtra("NoteId", 0);
            Details = Intent.GetStringExtra("Details");
            Title = Intent.GetStringExtra("Title");

            //txtEditNoteTitle.Text = "";
            //txtEditNoteTitle.Append(Title);
            //txtEditNoteDescription.Text = "";
            //txtEditNoteDescription.Append(Details);

            txtEditNoteTitle.Text = Title;
            txtEditNoteDescription.Text = Details;

            txtEditNoteDescription.RequestFocus();
            txtEditNoteDescription.SetSelection(txtEditNoteDescription.Length());

            objDB = new DatabaseManager();
        }

        private void OnBtnDeleteNoteClick(object sender, EventArgs e)
        {
            try
            {
                objDB.DeleteItem(NoteId);
                Toast.MakeText(this, "Note Deleted", ToastLength.Long).Show();
                this.Finish();
                StartActivity(typeof(MainActivity));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Occurred:" + ex.Message);
            }
        }

        private void OnBtnEditNoteClick(object sender, EventArgs e)
        {
            try
            {
                objDB.EditItem(txtEditNoteTitle.Text, txtEditNoteDescription.Text, NoteId);
                Toast.MakeText(this, "Note Edited", ToastLength.Long).Show();
                this.Finish();
                StartActivity(typeof(MainActivity));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Occurred:" + ex.Message);
            }
        }

        public override void OnBackPressed()
        {
            if (txtEditNoteTitle.Text != Title || txtEditNoteDescription.Text != Details)
            {

                Android.App.AlertDialog.Builder builder = new AlertDialog.Builder(this);

                AlertDialog altDg = builder.Create();
                altDg.SetTitle("Save Changes");
                altDg.SetIcon(Resource.Drawable.Icon);
                altDg.SetCancelable(false);
                altDg.SetMessage("Items have been changed but not saved. Save Changes?");

                altDg.SetButton("Yes", (s, ev) =>
                {

                    objDB.EditItem(txtEditNoteTitle.Text, txtEditNoteDescription.Text, NoteId);
                    base.OnBackPressed();
                });

                altDg.SetButton2("No", (s, ev) =>
                {
                    base.OnBackPressed();
                });
                altDg.Show();
                
            }
            else
            {
                base.OnBackPressed();
            }         
        }

        //protected override void OnUserLeaveHint()
        //{
        //    if (txtEditNoteTitle.Text != Title || txtEditNoteDescription.Text != Details)
        //    {

        //        Android.App.AlertDialog.Builder builder = new AlertDialog.Builder(this);

        //        AlertDialog altDg = builder.Create();
        //        altDg.SetTitle("Save Changes");
        //        altDg.SetIcon(Resource.Drawable.Icon);
        //        altDg.SetMessage("Items have been changed but not saved. Save Changes?");

        //        altDg.SetButton("Yes", (s, ev) => {

        //            objDB.EditItem(txtEditNoteTitle.Text, txtEditNoteDescription.Text, NoteId);
        //        });

        //        altDg.SetButton2("No", (s, ev) => {
        //        });
        //        altDg.Show();
        //    }
        //    base.OnUserLeaveHint();
        //}



    }
}