using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using NotesKeeper.Models;
using NotesKeeper.Services;
using Xamarin.Forms;

namespace NotesKeeper.ViewModels
{
    [QueryProperty(nameof(NoteId), nameof(NoteId))]
    public class ItemDetailViewModel : BaseViewModel
    {
        public Note Note { get; set; }
        public IList<string> CourseList { get; set; }

        public ItemDetailViewModel()
        {
            Title = "Edit note";
            Init();
            Note = new Note();
        }

        public string NoteId
        {
            get => Note.Id;
            set
            {
                Note.Id = value;
                LoadItemId(value);
            }
        }

        public string NoteHeading
        {
            get => Note.Heading;
            set
            {
                Note.Heading = value;
                OnPropertyChanged();
            }
        }

        public string NoteText
        {
            get => Note.Text;
            set
            {
                Note.Text = value;
                OnPropertyChanged();
            }
        }

        public string NoteCourse
        {
            get => Note.Course;
            set
            {
                Note.Course = value;
                OnPropertyChanged();
            }
        }

        public async void LoadItemId(string noteId)
        {
            try
            {
                var item = await PluralsightDataStore.GetNoteAsync(noteId);

                NoteHeading = item.Heading;
                NoteText = item.Text;
                NoteCourse = item.Course;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }

        async void Init()
        {
            CourseList = await PluralsightDataStore.GetCoursesAsync();
        }
    }
}

