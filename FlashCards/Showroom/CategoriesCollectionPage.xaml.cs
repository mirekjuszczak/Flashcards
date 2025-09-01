using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace FlashCards.Showroom;

public partial class CategoriesCollectionPage : ContentPage
{
    public CategoriesCollectionPage(CategoriesPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}