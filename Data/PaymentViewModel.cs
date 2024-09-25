using BuildYourBowl.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BuildYourBowl.Data
{
    /// <summary>
    /// ViewModel class that wraps around PaymentControl
    /// </summary>
    public class PaymentViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// An event indicating that a property has changed
        /// </summary>
        public virtual event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Helper method to indicate that a property has changed
        /// </summary>
        /// <param name="propertyName">The property that has changed</param>
        private void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// The order that this class wraps
        /// </summary>
        private Order _order;

        /// <summary>
        /// The order's Subtotal
        /// </summary>
        public decimal Subtotal => _order.Subtotal;

        /// <summary>
        /// The order's Tax
        /// </summary>
        public decimal Tax => _order.Tax;

        /// <summary>
        /// The order's Total
        /// </summary>
        public decimal Total => _order.Total;

        /// <summary>
        /// Private backing field for ValidPayment
        /// </summary>
        private bool _validPayment = true;

        /// <summary>
        /// Whether the payment amount is valid
        /// </summary>
        public bool ValidPayment
        {
            get
            {
                return _validPayment;
            }
            set
            {
                _validPayment = value;
            }
        }

        /// <summary>
        /// Private backing field for Paid
        /// </summary>
        private decimal _paid;

        /// <summary>
        /// How much the user paid for the order
        /// </summary>
        public decimal Paid
        {
            get
            {
                return _paid;
            }
            set
            {
                if(value < Total)
                {
                    ValidPayment = false;
                    _paid = Total; //Causes Change to display $0
                    OnPropertyChanged(nameof(ValidPayment));
                    OnPropertyChanged(nameof(Paid));
                    OnPropertyChanged(nameof(Change));
                    throw new ArgumentException("You cannot pay less than the total cost");
                }
                else
                {
                    _paid = value;
                    ValidPayment = true;
                    OnPropertyChanged(nameof(ValidPayment));
                    OnPropertyChanged(nameof(Paid));
                    OnPropertyChanged(nameof(Change));
                }
            }
        }

        /// <summary>
        /// Determines the user's change
        /// </summary>
        public decimal Change
        {
            get
            {
                return Paid - Total;
            }
        }

        /// <summary>
        /// The receipt for the order
        /// </summary>
        public string Receipt
        {
            get
            {
                StringBuilder s = new StringBuilder();

                s.AppendLine($"Order Number: {_order.Number}");
                s.AppendLine($"{_order.PlacedAt}\n");

                foreach (IMenuItem item in _order)
                {
                    s.AppendLine(item.Name + "   $" + item.Price);
                    foreach(string str in item.PreparationInformation)
                    {
                        s.AppendLine("\t" + str);
                    }
                    s.AppendLine();
                }

                s.AppendLine($"Subtotal: ${Subtotal}");
                s.AppendLine($"Tax: ${Tax}");
                s.AppendLine($"Total: ${Total}\n");
                s.AppendLine($"Paid: ${Paid}");
                s.AppendLine($"Change: ${Change}");
                s.AppendLine("---------------------------\n");

                return s.ToString();
            }
        }

        /// <summary>
        /// Constructs a new PaymentViewModel
        /// </summary>
        /// <param name="order">The order wrapped in this ViewModel</param>
        public PaymentViewModel(Order order)
        {
            _order = order;
            _paid = order.Total;
        }
    }
}
