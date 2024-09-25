using System;
using System.Collections.Generic;
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
    /// Interaction logic for MenuItemSelectionControl.xaml
    /// </summary>
    public partial class MenuItemSelectionControl : UserControl
    {
        /// <summary>
        /// A constructor that initializes components to be used in the MenuItemSelectionControl
        /// </summary>
        public MenuItemSelectionControl()
        {
            InitializeComponent();
            CustomBowlButton.Tag = new Bowl();
            CarnitasButton.Tag = new CarnitasBowl();
            GreenChickenButton.Tag = new GreenChickenBowl();
            SpicySteakButton.Tag = new SpicySteakBowl();
            CustomNachosButton.Tag = new Nachos();
            ChickenFajitasButton.Tag = new ChickenFajitaNachos();
            ClassicNachosButton.Tag = new ClassicNachos();
            FriesButton.Tag = new Fries();
            StreetCornButton.Tag = new StreetCorn();
            RefriedBeansButton.Tag = new RefriedBeans();
            AguaFrescaButton.Tag = new AguaFresca();
            HorchataButton.Tag = new Horchata();
            MilkButton.Tag = new Milk();
            ChickenNuggetsButton.Tag = new ChickenNuggetsMeal();
            CornDogBitesButton.Tag = new CornDogBitesMeal();
            SlidersButton.Tag = new SlidersMeal();
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
        /// Adds a MenuItem to the list
        /// </summary>
        /// <param name="sender">The sender of this event</param>
        /// <param name="e">Metadata for the event</param>
        public void FoodButtonClick(object sender, RoutedEventArgs e)
        {
            if((sender is Button b) && (b.Tag is IMenuItem m))
            {
                if(DataContext is ICollection<IMenuItem> list)
                {
                    
                    DependencyObject? parent = FindParent(this);

                    if (parent is MainWindow w)
                    {
                        if (m is Fries)
                        {
                            Fries f = new Fries();
                            list.Add(f);
                            ItemChanged?.Invoke(this, new MenuItemEventArgs(f));
                        }
                        else if (m is RefriedBeans)
                        {
                            RefriedBeans bn = new RefriedBeans();
                            list.Add(bn);
                            ItemChanged?.Invoke(this, new MenuItemEventArgs(bn));
                        }
                        else if (m is StreetCorn)
                        {
                            StreetCorn s = new StreetCorn();
                            list.Add(s);
                            ItemChanged?.Invoke(this, new MenuItemEventArgs(s));
                        }
                        else if (m is AguaFresca)
                        {
                            AguaFresca a = new AguaFresca();
                            list.Add(a);
                            ItemChanged?.Invoke(this, new MenuItemEventArgs(a));
                        }
                        else if (m is Horchata)
                        {
                            Horchata h = new Horchata();
                            list.Add(h);
                            ItemChanged?.Invoke(this, new MenuItemEventArgs(h));
                        }
                        else if (m is Milk)
                        {
                            Milk mk = new Milk();
                            list.Add(mk);
                            ItemChanged?.Invoke(this, new MenuItemEventArgs(mk));
                        }
                        else if (m is ChickenNuggetsMeal)
                        {
                            ChickenNuggetsMeal c = new ChickenNuggetsMeal();
                            list.Add(c);
                            ItemChanged?.Invoke(this, new MenuItemEventArgs(c));
                        }
                        else if (m is CornDogBitesMeal)
                        {
                            CornDogBitesMeal c = new CornDogBitesMeal();
                            list.Add(c);
                            ItemChanged?.Invoke(this, new MenuItemEventArgs(c));
                        }
                        else if (m is SlidersMeal)
                        {
                            SlidersMeal s = new SlidersMeal();
                            list.Add(s);
                            ItemChanged?.Invoke(this, new MenuItemEventArgs(s));
                        }
                        else if (m is CarnitasBowl)
                        {
                            CarnitasBowl c = new CarnitasBowl();
                            list.Add(c);
                            ItemChanged?.Invoke(this, new MenuItemEventArgs(c));
                        }
                        else if (m is GreenChickenBowl)
                        {
                            GreenChickenBowl c = new GreenChickenBowl();
                            list.Add(c);
                            ItemChanged?.Invoke(this, new MenuItemEventArgs(c));
                        }
                        else if (m is SpicySteakBowl)
                        {
                            SpicySteakBowl s = new SpicySteakBowl();
                            list.Add(s);
                            ItemChanged?.Invoke(this, new MenuItemEventArgs(s));
                        }
                        else if (m is ChickenFajitaNachos)
                        {
                            ChickenFajitaNachos c = new ChickenFajitaNachos();
                            list.Add(c);
                            ItemChanged?.Invoke(this, new MenuItemEventArgs(c));
                        }
                        else if (m is ClassicNachos)
                        {
                            ClassicNachos c = new ClassicNachos();
                            list.Add(c);
                            ItemChanged?.Invoke(this, new MenuItemEventArgs(c));
                        }
                        else if(m is Bowl)
                        {
                            Bowl bl = new Bowl();
                            list.Add(bl);
                            ItemChanged?.Invoke(this, new MenuItemEventArgs(bl));
                        }
                        else if (m is Nachos)
                        {
                            Nachos n = new Nachos();
                            list.Add(n);
                            ItemChanged?.Invoke(this, new MenuItemEventArgs(n));
                        }
                        
                    }
                }
            }
        }
    }
}
