using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashCards.Showroom;

public partial class CategoriesCollectionPage : ContentPage
{
    public CategoriesCollectionPage(CategoriesPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}