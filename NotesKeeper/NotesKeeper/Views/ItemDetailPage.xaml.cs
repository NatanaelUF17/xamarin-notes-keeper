using System.ComponentModel;
using Xamarin.Forms;
using NotesKeeper.ViewModels;
using NotesKeeper.Models;
using System.Collections.Generic;
using NotesKeeper.Services;
using System.Linq;

namespace NotesKeeper.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        ItemDetailViewModel viewModel;

        public ItemDetailPage()
        {
            InitializeComponent();

            viewModel = new ItemDetailViewModel();
            BindingContext = viewModel;
        }

        public void OnCancel_Clicked(System.Object sender, System.EventArgs e)
        {
            DisplayAlert("Cancel option", "Cancel was clicked", "Ok", "Cancel");
        }

        public void OnSave_Clicked(System.Object sender, System.EventArgs e)
        {
            DisplayAlert("Save option", "Save was clicked", "Ok", "Cancel");
        }
    }
}
