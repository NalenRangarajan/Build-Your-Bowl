using BuildYourBowl.Data;
using BuildYourBowl.PointOfSale.EventArgs;
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

namespace BuildYourBowl.PointOfSale
{
    /// <summary>
    /// Interaction logic for KidsMealMain.xaml
    /// </summary>
    public partial class KidsMealMainControl : UserControl
    {
        /// <summary>
        /// Constructor for KidsMealMainControl
        /// </summary>
        public KidsMealMainControl()
        {
            InitializeComponent();
            FriesSideDisplay.DataContext = new Fries() { SizeType = Data.Size.Kids};
            BeansSideDisplay.DataContext = new RefriedBeans() { SizeType = Data.Size.Kids };
            CornSideDisplay.DataContext = new StreetCorn() { SizeType = Data.Size.Kids };
            AguaFrescaDrinkDisplay.DataContext = new AguaFresca() { DrinkSize = Data.Size.Kids };
            HorchataDrinkDisplay.DataContext = new Horchata() { DrinkSize = Data.Size.Kids };
            MilkDrinkDisplay.DataContext = new Milk();
            CountControl.CountChanged += HandleCountChanged!;
        }

        /// <summary>
        /// Determines what type of item the Count property is representing
        /// </summary>
        /// <returns>A string representing the type of item in the kids meal</returns>
        public string DetermineCountType()
        {
            if(DataContext is ChickenNuggetsMeal)
            {
                return "Nuggets";
            }
            else if(DataContext is CornDogBitesMeal)
            {
                return "Bites";
            }
            else
            {
                return "Sliders";
            }
        }

        /// <summary>
        /// Handler method that changes the side depending on the radio button clicked
        /// </summary>
        /// <param name="sender">The sender of this event</param>
        /// <param name="e">Metadata for the event</param>
        public void CheckedChangeForSide(object sender, RoutedEventArgs e)
        {
            if(sender is RadioButton r)
            {
                if(DataContext is KidsMeal m)
                {
                    if (r == FriesRadioButton)
                    {
                        Fries f = new Fries() { SizeType = Data.Size.Kids };

                        m.SideChoice = f;
                        FriesSideDisplay.Visibility = Visibility.Visible;
                        BeansSideDisplay.Visibility = Visibility.Hidden;
                        CornSideDisplay.Visibility = Visibility.Hidden;
                        FriesSideDisplay.DataContext = f;
                    }
                    else if (r == BeansRadioButton)
                    {
                        RefriedBeans rb = new RefriedBeans() { SizeType = Data.Size.Kids };
                        m.SideChoice = rb;
                        FriesSideDisplay.Visibility = Visibility.Hidden;
                        BeansSideDisplay.Visibility = Visibility.Visible;
                        CornSideDisplay.Visibility = Visibility.Hidden;
                        BeansSideDisplay.DataContext = rb;
                    }
                    else if (r == CornRadioButton)
                    {
                        StreetCorn s = new StreetCorn() { SizeType = Data.Size.Kids };
                        m.SideChoice = s;
                        FriesSideDisplay.Visibility = Visibility.Hidden;
                        BeansSideDisplay.Visibility = Visibility.Hidden;
                        CornSideDisplay.Visibility = Visibility.Visible;
                        CornSideDisplay.DataContext = s;
                    }
                }
            }
        }

        /// <summary>
        /// Handler method that changes the drink depending on the radio button clicked
        /// </summary>
        /// <param name="sender">The sender of this event</param>
        /// <param name="e">Metadata for the event</param>
        public void CheckedChangeForDrink(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton r)
            {
                if(DataContext is KidsMeal m)
                {
                    if (r == AguaFrescaRadioButton)
                    {
                        AguaFresca a = new AguaFresca() { DrinkSize = Data.Size.Kids };
                        m.DrinkChoice = a;
                        AguaFrescaDrinkDisplay.Visibility = Visibility.Visible;
                        HorchataDrinkDisplay.Visibility = Visibility.Hidden;
                        MilkDrinkDisplay.Visibility = Visibility.Hidden;
                        AguaFrescaDrinkDisplay.DataContext = a;

                    }
                    else if (r == HorchataRadioButton)
                    {
                        Horchata h = new Horchata() { DrinkSize = Data.Size.Kids };
                        m.DrinkChoice = h;
                        AguaFrescaDrinkDisplay.Visibility = Visibility.Hidden;
                        HorchataDrinkDisplay.Visibility = Visibility.Visible;
                        MilkDrinkDisplay.Visibility = Visibility.Hidden;
                        HorchataDrinkDisplay.DataContext = h;
                    }
                    else if (r == MilkRadioButton)
                    {
                        Milk mk = new Milk();
                        m.DrinkChoice = mk;
                        AguaFrescaDrinkDisplay.Visibility = Visibility.Hidden;
                        HorchataDrinkDisplay.Visibility = Visibility.Hidden;
                        MilkDrinkDisplay.Visibility = Visibility.Visible;
                        MilkDrinkDisplay.DataContext = mk;
                    }
                }
            }
        }

        /// <summary>
        /// Handles a Count change in the UnitCount Control
        /// </summary>
        /// <param name="sender">The sender of this event</param>
        /// <param name="e">Metadata for the event</param>
        public void HandleCountChanged(object sender, CountChangedEventArgs e)
        {
            if(DataContext is KidsMeal k)
            {
                k.ItemCount = e.NewCount;
            }
        }
    }
}
