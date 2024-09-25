using BuildYourBowl.Data;
using System;
using System.Collections.Generic;
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

namespace BuildYourBowl.PointOfSale
{
    /// <summary>
    /// Interaction logic for OrderSummaryControl.xaml
    /// </summary>
    public partial class OrderSummaryControl : UserControl
    {
        /// <summary>
        /// Constructor for OrderSummaryControl
        /// </summary>
        public OrderSummaryControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Event handler that is called when an item is changed
        /// </summary>
        public event EventHandler<MenuItemEventArgs>? ItemChanged;

        /// <summary>
        /// Methods that finds a specific type of parent object if it exists
        /// </summary>
        /// <param name="child">The object to start traversing the tree at</param>
        /// <returns>The parent object of the type we want to find, or null</returns>
        public DependencyObject? FindParent(DependencyObject child)
        {
            DependencyObject parent = child;
            do
            {
                // Get this node's parent
                parent = LogicalTreeHelper.GetParent(parent);
            }
            // Invariant: there is a parent element, and it is not a ListSwitcher 
            while (!(parent is null || parent is MainWindow));

            return parent;
        }

        /// <summary>
        /// Removes a MenuItem from the list
        /// </summary>
        /// <param name="sender">The sender of this event</param>
        /// <param name="e">Metadata for the event</param>
        public void RemoveItemClick(object sender, RoutedEventArgs e)
        {
            if (sender is Button b && b.DataContext is IMenuItem m)
            {
                if(DataContext is ICollection<IMenuItem> list)
                {
                    list.Remove(m);
                    DependencyObject? parent = FindParent(this);
                    if(parent is MainWindow w)
                    {
                        HideAll(w);
                        w.MenuItemDisplay.Visibility = Visibility.Visible;
                        
                    }
                }
            }
        }

        /// <summary>
        /// Allows the user to edit a specific menu item already added to the order
        /// </summary>
        /// <param name="sender">The sender of this event</param>
        /// <param name="e">Metadata for the event</param>
        public void EditItem(object sender, RoutedEventArgs e)
        {
            if(sender is Button b && b.DataContext is IMenuItem m)
            {
                DependencyObject? parent = FindParent(this);
                //Reopen controls depending on which item was edited
                
                if(parent is MainWindow w)
                {
                    if (m is Fries f)
                    {
                        w.MenuItemDisplay.FriesButton.DataContext = w.MenuItemDisplay.FriesButton.Tag;
                        HideAll(w);
                        w.FriesDisplay.Visibility = Visibility.Visible;
                    }
                    else if (m is RefriedBeans)
                    {
                        w.MenuItemDisplay.RefriedBeansButton.DataContext = w.MenuItemDisplay.RefriedBeansButton.Tag;
                        HideAll(w);
                        w.RefriedBeansDisplay.Visibility = Visibility.Visible;
                    }
                    else if (m is StreetCorn)
                    {
                        w.MenuItemDisplay.StreetCornButton.DataContext = w.MenuItemDisplay.StreetCornButton.Tag;
                        HideAll(w);
                        w.StreetCornDisplay.Visibility = Visibility.Visible;
                    }
                    else if (m is AguaFresca)
                    {
                        w.MenuItemDisplay.AguaFrescaButton.DataContext = w.MenuItemDisplay.AguaFrescaButton.Tag;
                        HideAll(w);
                        w.AguaFrescaDisplay.Visibility = Visibility.Visible;
                    }
                    else if (m is Horchata)
                    {
                        w.MenuItemDisplay.HorchataButton.DataContext = w.MenuItemDisplay.HorchataButton.Tag;
                        HideAll(w);
                        w.HorchataDisplay.Visibility = Visibility.Visible;
                    }
                    else if (m is Milk)
                    {
                        w.MenuItemDisplay.MilkButton.DataContext = w.MenuItemDisplay.MilkButton.Tag;
                        HideAll(w);
                        w.MilkDisplay.Visibility = Visibility.Visible;
                    }
                    else if (m is ChickenNuggetsMeal ch)
                    {
                        w.MenuItemDisplay.ChickenNuggetsButton.DataContext = w.MenuItemDisplay.ChickenNuggetsButton.Tag;
                        HideAll(w);
                        ItemChanged?.Invoke(this, new MenuItemEventArgs(ch));
                        w.KidsMealDisplay.Visibility = Visibility.Visible;
                    }
                    else if (m is CornDogBitesMeal cn)
                    {
                        w.MenuItemDisplay.CornDogBitesButton.DataContext = w.MenuItemDisplay.CornDogBitesButton.Tag;
                        HideAll(w);
                        ItemChanged?.Invoke(this, new MenuItemEventArgs(cn));
                        w.KidsMealDisplay.Visibility = Visibility.Visible;
                    }
                    else if (m is SlidersMeal s)
                    {
                        w.MenuItemDisplay.SlidersButton.DataContext = w.MenuItemDisplay.SlidersButton.Tag;
                        HideAll(w);
                        ItemChanged?.Invoke(this, new MenuItemEventArgs(s));
                        w.KidsMealDisplay.Visibility = Visibility.Visible;
                    }
                    else if(m is CarnitasBowl c)
                    {
                        w.MenuItemDisplay.CarnitasButton.DataContext = w.MenuItemDisplay.CarnitasButton.Tag;
                        HideAll(w);
                        ItemChanged?.Invoke(this, new MenuItemEventArgs(c));
                        w.EntreeDisplay.Visibility = Visibility.Visible;
                    }
                    else if(m is GreenChickenBowl gc)
                    {
                        w.MenuItemDisplay.GreenChickenButton.DataContext = w.MenuItemDisplay.GreenChickenButton.Tag;
                        HideAll(w);
                        ItemChanged?.Invoke(this, new MenuItemEventArgs(gc));
                        w.EntreeDisplay.Visibility = Visibility.Visible;
                    }
                    else if(m is SpicySteakBowl ssb)
                    {
                        w.MenuItemDisplay.SpicySteakButton.DataContext = w.MenuItemDisplay.SpicySteakButton.Tag;
                        HideAll(w);
                        ItemChanged?.Invoke(this, new MenuItemEventArgs(ssb));
                        w.EntreeDisplay.Visibility = Visibility.Visible;
                    }
                    else if(m is ChickenFajitaNachos cf)
                    {
                        w.MenuItemDisplay.ChickenFajitasButton.DataContext = w.MenuItemDisplay.ChickenFajitasButton.Tag;
                        HideAll(w);
                        ItemChanged?.Invoke(this, new MenuItemEventArgs(cf));
                        w.EntreeDisplay.Visibility = Visibility.Visible;
                    }
                    else if(m is ClassicNachos cl)
                    {
                        w.MenuItemDisplay.ClassicNachosButton.DataContext = w.MenuItemDisplay.ClassicNachosButton.Tag;
                        HideAll(w);
                        ItemChanged?.Invoke(this, new MenuItemEventArgs(cl));
                        w.EntreeDisplay.Visibility = Visibility.Visible;
                    }
                    else if(m is Bowl bl)
                    {
                        w.MenuItemDisplay.CustomBowlButton.DataContext = w.MenuItemDisplay.CustomBowlButton.Tag;
                        HideAll(w);
                        ItemChanged?.Invoke(this, new MenuItemEventArgs(bl));
                        w.EntreeDisplay.Visibility = Visibility.Visible;
                    }
                    else if(m is Nachos ns)
                    {
                        w.MenuItemDisplay.CustomNachosButton.DataContext = w.MenuItemDisplay.CustomNachosButton.Tag;
                        HideAll(w);
                        ItemChanged?.Invoke(this, new MenuItemEventArgs(ns));
                        w.EntreeDisplay.Visibility = Visibility.Visible;
                    }
                }
            }
        }

        /// <summary>
        /// Hides all menu item displays
        /// </summary>
        /// <param name="w">The main window to hide controls of</param>
        private void HideAll(MainWindow w)
        {
            w.FriesDisplay.Visibility = Visibility.Hidden;
            w.StreetCornDisplay.Visibility = Visibility.Hidden;
            w.RefriedBeansDisplay.Visibility = Visibility.Hidden;
            w.AguaFrescaDisplay.Visibility = Visibility.Hidden;
            w.HorchataDisplay.Visibility = Visibility.Hidden;
            w.MilkDisplay.Visibility = Visibility.Hidden;
            w.EntreeDisplay.Visibility = Visibility.Hidden;
            w.KidsMealDisplay.Visibility = Visibility.Hidden;
            w.PaymentDisplay.Visibility = Visibility.Hidden;
        }
    }
}
