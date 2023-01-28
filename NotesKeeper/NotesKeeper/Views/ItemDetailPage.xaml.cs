using System.ComponentModel;
using Xamarin.Forms;
using NotesKeeper.ViewModels;

namespace NotesKeeper.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}
