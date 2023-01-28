using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using NotesKeeper.Models;
using Xamarin.Forms;

namespace NotesKeeper.ViewModels
{
    public class NewItemViewModel : BaseViewModel
    {
        public Note Note { get; set; }
        public IList<string> Courses { get; set; }

        public NewItemViewModel()
        {
            Init();
            Note = new Note();
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged += 
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(NoteHeading)
                && !String.IsNullOrWhiteSpace(NoteText)
                && !String.IsNullOrWhiteSpace(NoteCourse);
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

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel()
        {
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            Note newNote = new Note()
            {
                Id = Guid.NewGuid().ToString(),
                Heading = NoteHeading,
                Text = NoteText,
                Course = NoteCourse
            };
            
            await PluralsightDataStore.AddNoteAsync(newNote);
            await Shell.Current.GoToAsync("..");
        }

        async void Init()
        {
            Courses = await PluralsightDataStore.GetCoursesAsync();
        }
    }
}

