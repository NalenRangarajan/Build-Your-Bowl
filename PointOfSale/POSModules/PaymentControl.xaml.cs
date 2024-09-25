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
using System.IO;
using BuildYourBowl.Data;

namespace BuildYourBowl.PointOfSale
{
    /// <summary>
    /// Interaction logic for PaymentControl.xaml
    /// </summary>
    public partial class PaymentControl : UserControl
    {
        /// <summary>
        /// Creates a new PaymentControl
        /// </summary>
        public PaymentControl()
        {
            InitializeComponent();
            if (File.Exists("receipts.txt"))
            {
                File.Delete("receipts.txt");
            }
        }

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
        /// Finalizes the payment of an order
        /// </summary>
        /// <param name="sender">The sender of this event</param>
        /// <param name="e">Metadata for the event</param>
        public void FinalizePaymentClick(object sender, RoutedEventArgs e)
        {
            File.AppendAllText("receipts.txt", ((PaymentViewModel)DataContext).Receipt);

            MessageBox.Show("Receipt printed. Click OK to start new order.");

            DependencyObject? parent = FindParent(this);

            if(parent is MainWindow w)
            {
                w.ResetScreen();
                w.DataContext = new Order();
            }
        }
    }
}
