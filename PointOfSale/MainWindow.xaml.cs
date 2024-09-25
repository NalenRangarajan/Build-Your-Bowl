using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BuildYourBowl.Data;

namespace BuildYourBowl.PointOfSale
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Constructor for MainWindow
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new Order();
            MenuItemDisplay.ItemChanged += HandleMenuItemChanged!;
            OrderSummaryDisplay.ItemChanged += HandleMenuItemChanged!;
            EntreeDisplay.IngredientChanged += HandleIngredientChange!;
            EntreeDisplay.LoadPossibleChoices();
            
        }

        /// <summary>
        /// Clears the current order
        /// </summary>
        /// <param name="sender">The sender of this event</param>
        /// <param name="e">Metadata for the event</param>
        public void CancelOrderClick(object? sender, RoutedEventArgs e)
        {
            if(sender is Button)
            {
                DataContext = new Order();
                ResetScreen();
            }
        }

        /// <summary>
        /// Displays payment control
        /// </summary>
        /// <param name="sender">The sender of this event</param>
        /// <param name="e">Metadata for the event</param>
        public void CompleteOrderClick(object? sender, RoutedEventArgs e)
        {
            ResetScreen();
            PaymentDisplay.Visibility = Visibility.Visible;
            PaymentDisplay.DataContext = new PaymentViewModel((Order)DataContext);
        }

        /// <summary>
        /// Goes back to the MenuItemSelectionControl
        /// </summary>
        /// <param name="sender">The sender of this event</param>
        /// <param name="e">Metadata for the event</param>
        public void BackToMenuClick(object sender, RoutedEventArgs e)
        {
            ResetScreen();
        }

        /// <summary>
        /// Method that handles a menu click
        /// </summary>
        /// <param name="sender">The sender of this event</param>
        /// <param name="e">Metadata for the event</param>
        private void HandleMenuItemChanged(object sender, MenuItemEventArgs e)
        {
            //switch which control is displayed

            if (e.MenuItem is Fries f)
            {
                FriesDisplay.Visibility = Visibility.Visible;
                MenuItemDisplay.Visibility = Visibility.Hidden;
                FriesDisplay.DataContext = e.MenuItem;
            }
            else if (e.MenuItem is RefriedBeans)
            {
                RefriedBeansDisplay.Visibility = Visibility.Visible;
                MenuItemDisplay.Visibility = Visibility.Hidden;
                RefriedBeansDisplay.DataContext = e.MenuItem;
            }
            else if(e.MenuItem is StreetCorn)
            {
                StreetCornDisplay.Visibility = Visibility.Visible;
                MenuItemDisplay.Visibility = Visibility.Hidden;
                StreetCornDisplay.DataContext = e.MenuItem;
            }
            else if(e.MenuItem is AguaFresca)
            {
                AguaFrescaDisplay.Visibility = Visibility.Visible;
                MenuItemDisplay.Visibility = Visibility.Hidden;
                AguaFrescaDisplay.DataContext = e.MenuItem;
            }
            else if(e.MenuItem is Horchata)
            {
                HorchataDisplay.Visibility = Visibility.Visible;
                MenuItemDisplay.Visibility = Visibility.Hidden;
                HorchataDisplay.DataContext = e.MenuItem;
            }
            else if(e.MenuItem is Milk)
            {
                MilkDisplay.Visibility = Visibility.Visible;
                MenuItemDisplay.Visibility = Visibility.Hidden;
                MilkDisplay.DataContext = e.MenuItem;
            }
            else if(e.MenuItem is ChickenNuggetsMeal c)
            {
                KidsMealDisplay.Visibility = Visibility.Visible;
                MenuItemDisplay.Visibility = Visibility.Hidden;
                KidsMealDisplay.DataContext = e.MenuItem;
                KidsMealDisplay.CountControl.Count = 5;
                KidsMealDisplay.FriesRadioButton.IsChecked = true;
                KidsMealDisplay.MilkRadioButton.IsChecked = true;

                if(c.SideChoice is Fries fr)
                {
                    KidsMealDisplay.FriesSideDisplay.DataContext = fr;
                }
                else if(c.SideChoice is RefriedBeans rb)
                {
                    KidsMealDisplay.BeansSideDisplay.DataContext = rb;
                }
                else if(c.SideChoice is StreetCorn s)
                {
                    KidsMealDisplay.CornSideDisplay.DataContext = s;
                }

                KidsMealDisplay.CheeseBox.Visibility = Visibility.Hidden;
            }
            else if(e.MenuItem is CornDogBitesMeal cd){
                KidsMealDisplay.Visibility = Visibility.Visible;
                MenuItemDisplay.Visibility = Visibility.Hidden;
                KidsMealDisplay.DataContext = e.MenuItem;
                KidsMealDisplay.CountControl.Count = 5;
                KidsMealDisplay.FriesRadioButton.IsChecked = true;
                KidsMealDisplay.MilkRadioButton.IsChecked = true;

                if (cd.SideChoice is Fries fr)
                {
                    KidsMealDisplay.FriesSideDisplay.DataContext = fr;
                }
                else if (cd.SideChoice is RefriedBeans rb)
                {
                    KidsMealDisplay.BeansSideDisplay.DataContext = rb;
                }
                else if (cd.SideChoice is StreetCorn s)
                {
                    KidsMealDisplay.CornSideDisplay.DataContext = s;
                }

                KidsMealDisplay.CheeseBox.Visibility = Visibility.Hidden;
            }
            else if(e.MenuItem is SlidersMeal s)
            {
                KidsMealDisplay.Visibility = Visibility.Visible;
                MenuItemDisplay.Visibility = Visibility.Hidden;
                KidsMealDisplay.DataContext = e.MenuItem;
                KidsMealDisplay.CountControl.Count = 2;
                KidsMealDisplay.FriesRadioButton.IsChecked = true;
                KidsMealDisplay.MilkRadioButton.IsChecked = true;

                if (s.SideChoice is Fries fr)
                {
                    KidsMealDisplay.FriesSideDisplay.DataContext = fr;
                }
                else if (s.SideChoice is RefriedBeans rb)
                {
                    KidsMealDisplay.BeansSideDisplay.DataContext = rb;
                }
                else if (s.SideChoice is StreetCorn sc)
                {
                    KidsMealDisplay.CornSideDisplay.DataContext = sc;
                }

                KidsMealDisplay.CheeseBox.Visibility = Visibility.Visible;
            }
            else
            {
                EntreeDisplay.Visibility = Visibility.Visible;
                MenuItemDisplay.Visibility = Visibility.Hidden;
                EntreeDisplay.DataContext = e.MenuItem;

                EntreeDisplay.LoadPossibleChoices();
            }

        }

        /// <summary>
        /// Resets controls to default POS screen
        /// </summary>
        public void ResetScreen()
        {
            MenuItemDisplay.Visibility = Visibility.Visible;
            FriesDisplay.Visibility = Visibility.Hidden;
            RefriedBeansDisplay.Visibility = Visibility.Hidden;
            StreetCornDisplay.Visibility = Visibility.Hidden;
            AguaFrescaDisplay.Visibility = Visibility.Hidden;
            HorchataDisplay.Visibility = Visibility.Hidden;
            MilkDisplay.Visibility = Visibility.Hidden;
            KidsMealDisplay.Visibility = Visibility.Hidden;
            EntreeDisplay.Visibility = Visibility.Hidden;
            PaymentDisplay.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Handles an Ingredient change
        /// </summary>
        /// <param name="sender">The sender of this event</param>
        /// <param name="e">Metadata for the event</param>
        public void HandleIngredientChange(object sender, IngredientEventArgs e)
        {
            if (sender is EntreeControl entreeControl)
            {
                if(entreeControl.DataContext is Entree entree)
                {
                    if (e.Add)
                    {
                        entree.EditOrderAdd(e.Ingredient);
                    }
                    else
                    {
                        entree.EditOrderRemove(e.Ingredient);
                    }
                }
            }
        }
    }
}
