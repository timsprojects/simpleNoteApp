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
using SQLite;

namespace myNotePad
{
    public class Notes
    {
        [PrimaryKey, AutoIncrement]
        public int NoteId { get; set; }
        public string Title { get; set; }
        public string NoteDetails { get; set; }
        public DateTime Date { get; set; }

        public Notes()
        {
        }
    }
}