﻿using ADASMobileClient.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ADASMobileClient.Core.view
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OcrPage : ContentPage
    {
        public OcrPage()
        {
            InitializeComponent();
            BindingContext = new OcrViewModel();
        }
    }
}