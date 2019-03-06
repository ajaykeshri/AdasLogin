using ADASMobileClient.Core.model;
using System;
using System.Collections.Generic;

using System.Collections.ObjectModel;


using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ADASMobileClient.Core
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CalibrationOrderSetupPage : ContentPage
    {
        private readonly ObservableCollection<ListItemCalibrationOrderSetup> multiSelectListItems;

        /// <summary>
        ///     Object used to identify if the user tappep on a selected cell.
        /// </summary>
        private bool _isSelectedItemTap;

        /// <summary>
        ///     Object holding the reference of current user selection.
        /// </summary>
        private int _selectedItemIndex;

        private IList<ListItemCalibrationOrderSetup> selectedItems = new List<ListItemCalibrationOrderSetup>();
        public CalibrationOrderSetupPage()
        {
            InitializeComponent();

            BindingContext = this;
            multiSelectListItems = new ObservableCollection<ListItemCalibrationOrderSetup>();
            multiSelectListItems.Add(new ListItemCalibrationOrderSetup()
            {
               // CheckboxImage = "filter.png",
                ItemImage = "edit",
                AdasModuleName = "Item1",
                ModuleAvailability = "Available",
                TargetAvailability = "Available",
                Status="Progress"


            });

            multiSelectListItems.Add(new ListItemCalibrationOrderSetup()
            {
                CheckboxImage = "icons8_unchecked_checkbox.png",
                ItemImage = "edit",
                AdasModuleName = "Item2",
                ModuleAvailability="NotAvailable",
                 TargetAvailability = "Available",
                  Status = "Progress"


            });

            multiSelectListItems.Add(new ListItemCalibrationOrderSetup()
            {
                CheckboxImage = "icons8_unchecked_checkbox.png",
                ItemImage = "edit",
                AdasModuleName = "Item3",
                ModuleAvailability="Available",
                TargetAvailability = "Available",
                 Status = "Progress"


            });

            multiSelectListItems.Add(new ListItemCalibrationOrderSetup()
            {
                CheckboxImage = "icons8_unchecked_checkbox.png",
                ItemImage = "edit",
                AdasModuleName = "Item4",
                ModuleAvailability="Optinal",
                TargetAvailability = "Available",
                 Status = "Progress"


            });

            multiSelectListItems.Add(new ListItemCalibrationOrderSetup()
            {
                CheckboxImage = "icons8_unchecked_checkbox.png",
                ItemImage = "edit",
                AdasModuleName = "Item5",
                ModuleAvailability="Standard",
                 TargetAvailability = "Available",
                  Status = "Progress"


            });

            MultiSelectListView.ItemsSource = multiSelectListItems;
        }


        private void MultiSelectListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }

        private void MultiSelectListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {

        }
    }
}