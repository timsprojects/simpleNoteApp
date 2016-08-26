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
using Android.Preferences;

namespace myNotePad
{
    public class DataStorage
    {
        private ISharedPreferences mSharedPrefs;
        private ISharedPreferencesEditor mPrefsEditor;
        private Context mContext;

        private static String PREFERENCE_SEARCH_STRING = "PREFERENCE_SEARCH_STRING";
        private static String PREFERENCE_NOTE_TITLE = "PREFERENCE_NOTE_TITLE";
        private static String PREFERENCE_NOTE_DETAILS = "PREFERENCE_NOTE_DETAILS";

        public DataStorage(Context context)
        {
            this.mContext = context;
            mSharedPrefs = PreferenceManager.GetDefaultSharedPreferences(mContext);
            mPrefsEditor = mSharedPrefs.Edit();
        }

        public void saveSearchString(string searchstringtosave)
        {
            mPrefsEditor.PutString(PREFERENCE_SEARCH_STRING, searchstringtosave);
            mPrefsEditor.Commit();
        }

        public string getSearchString()
        {
            return mSharedPrefs.GetString(PREFERENCE_SEARCH_STRING, "");
        }

        public void saveNoteTitle(string notetitle)
        {
            mPrefsEditor.PutString(PREFERENCE_NOTE_TITLE, notetitle);
            mPrefsEditor.Commit();
        }

        public string getNoteTitle()
        {
            return mSharedPrefs.GetString(PREFERENCE_NOTE_TITLE, "");
        }

        public void saveNoteDetails(string notedetails)
        {
            mPrefsEditor.PutString(PREFERENCE_NOTE_DETAILS, notedetails);
            mPrefsEditor.Commit();
        }

        public string getNoteDetails()
        {
            return mSharedPrefs.GetString(PREFERENCE_NOTE_DETAILS, "");
        }

    }
}