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
    /// Interaction logic for UnitCountControl.xaml
    /// </summary>
    public partial class UnitCountControl : UserControl
    {

        /// <summary>
        /// The Count of the control
        /// </summary>
        public uint Count
        {
            get
            {
                return (uint)GetValue(CountProperty);
            }
            set
            {
                SetValue(CountProperty, value);
            }
        }

        /// <summary>
        /// A dependency property called Count for the control
        /// </summary>
        public static readonly DependencyProperty CountProperty = DependencyProperty.Register(nameof(Count), typeof(uint), typeof(UnitCountControl), new PropertyMetadata(1u));

        /// <summary>
        /// The constructor of the UnitCountControl
        /// </summary>
        public UnitCountControl()
        {
            InitializeComponent();
            
        }

        /// <summary>
        /// Event handler that is called when the count is changed
        /// </summary>
        public event EventHandler<CountChangedEventArgs>? CountChanged;

        /// <summary>
        /// Increments the count of the control
        /// </summary>
        /// <param name="sender">The sender of this event</param>
        /// <param name="e">Metadata for the event</param>
        private void HandleIncrement(object sender, RoutedEventArgs e)
        {
            if(DataContext is ChickenNuggetsMeal || DataContext is CornDogBitesMeal)
            {
                if (Count < 8)
                {
                    Count++;
                    CountChanged?.Invoke(this, new CountChangedEventArgs(Count));
                }
            }
            else if(DataContext is SlidersMeal)
            {
                if(Count < 4)
                {
                    Count++;
                    CountChanged?.Invoke(this, new CountChangedEventArgs(Count));
                }
            }
        }

        /// <summary>
        /// Decrements the count of the control
        /// </summary>
        /// <param name="sender">The sender of this event</param>
        /// <param name="e">Metadata for the event</param>
        private void HandleDecrement(object sender, RoutedEventArgs e)
        {
            if (DataContext is ChickenNuggetsMeal || DataContext is CornDogBitesMeal)
            {
                if (Count > 5)
                {
                    Count--;
                    CountChanged?.Invoke(this, new CountChangedEventArgs(Count));
                }
            }
            else if (DataContext is SlidersMeal)
            {
                if (Count > 2)
                {
                    Count--;
                    CountChanged?.Invoke(this, new CountChangedEventArgs(Count));
                }
            }
        }
    }
}
