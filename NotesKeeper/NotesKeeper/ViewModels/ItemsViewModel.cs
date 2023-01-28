using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using NotesKeeper.Models;
using NotesKeeper.Views;

namespace NotesKeeper.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        private Note _selectedNote;

        public ObservableCollection<Note> Notes { get; }

        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get;  }
        public Command<Note> ItemTapped { get; }

        public ItemsViewModel()
        {
            Title = "Browse";
            Notes = new ObservableCollection<Note>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            ItemTapped = new Command<Note>(OnItemSelected);

            AddItemCommand = new Command(OnAddItem);
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Notes.Clear();
                var notes = await PluralsightDataStore.GetNotesAsync();
                foreach (var note in notes)
                {
                    Notes.Add(note);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedNote = null;
        }

        public Note SelectedNote
        {
            get => _selectedNote;
            set
            {
                SetProperty(ref _selectedNote, value);
                OnItemSelected(value);
            }
        }

        private async void OnAddItem(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewItemPage));
        }

        async void OnItemSelected(Note note)
        {
            if (note == null)
                return;

            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.NoteId)}={note.Id}");
        }
    }
}
