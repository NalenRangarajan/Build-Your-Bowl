using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using Windows.UI.Xaml;

namespace BuildYourBowl.Data
{
    /// <summary>
    /// The definition of the Order class, which contains multiple, potentially customized menu items
    /// </summary>
    public class Order : ICollection<IMenuItem>, INotifyCollectionChanged, INotifyPropertyChanged
    {
        /// <summary>
        /// A List containing all the menu items in the order
        /// </summary>
        private List<IMenuItem> _order = new List<IMenuItem>();

        /// <summary>
        /// This is an event signifying when the Collection has been changed
        /// </summary>
        public event NotifyCollectionChangedEventHandler? CollectionChanged;

        /// <summary>
        /// This is an event signaling when a property has been changed
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// The amount of menu items in the order
        /// </summary>
        public int Count
        {
            get
            {
                return _order.Count;
            }
        }

        /// <summary>
        /// A private backing field representing the last order's number. This is static so that the next order can have a unique number.
        /// </summary>
        private static int _lastNum = 0;

        /// <summary>
        /// A unique integer identifying the order
        /// </summary>
        public int Number { get; init; }

        /// <summary>
        /// The time the order was placed
        /// </summary>
        public DateTime PlacedAt { get; init; }

        /// <summary>
        /// Determines whether the class is read only
        /// </summary>
        public bool IsReadOnly { get; } = false;

        /// <summary>
        /// A property representing the price of all items in the order
        /// </summary>
        public decimal Subtotal
        {
            get
            {
                decimal sum = 0.00m;
                foreach(IMenuItem item in _order)
                {
                    sum += item.Price;
                }

                decimal roundedVal = Math.Round(sum, 2);

                return roundedVal;
            }
        }

        /// <summary>
        /// A private backing field for the TaxRate
        /// </summary>
        private decimal _taxRate = 0.0915m;

        /// <summary>
        /// A property representing the sales tax rate
        /// </summary>
        public decimal TaxRate
        {
            get
            {
                return _taxRate;
            }
            set
            {
                _taxRate = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TaxRate)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Tax)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Total)));
            }
        }

        /// <summary>
        /// A property representing the tax for the order
        /// </summary>
        public decimal Tax
        {
            get
            {
                decimal roundedVal = Math.Round(Subtotal * TaxRate, 2);

                return roundedVal;
            }
        }

        /// <summary>
        /// A property representing the total price of the order
        /// </summary>
        public decimal Total
        {
            get
            {
                decimal roundedVal = Math.Round(Subtotal + Tax, 2);

                return roundedVal;
            }
        }

        /// <summary>
        /// Handler that updates properties of the order
        /// </summary>
        /// <param name="sender">The sender of this event</param>
        /// <param name="e">Metadata for the event</param>
        private void HandleOrderChanged(object? sender, PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Subtotal)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Tax)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Total)));
        }

        /// <summary>
        /// Adds a menu item to the order
        /// </summary>
        /// <param name="item">The menu item to add</param>
        public void Add(IMenuItem item)
        {
            _order.Add(item);
            item.PropertyChanged += HandleOrderChanged;
            
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Subtotal)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Tax)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Total)));
        }

        /// <summary>
        /// Clears the order
        /// </summary>
        public void Clear()
        {
            foreach (IMenuItem item in _order)
            {
                item.PropertyChanged -= HandleOrderChanged;
            }

            _order.Clear();
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Subtotal)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Tax)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Total)));
        }

        /// <summary>
        /// Determines if the menu <paramref name="item"/> is in the order
        /// </summary>
        /// <param name="item">The item to check</param>
        /// <returns>Whether the menu <paramref name="item"/> is in the order</returns>
        public bool Contains(IMenuItem item)
        {
            if (_order.Contains(item))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Copies the order to an <paramref name="array"/> starting at the <paramref name="arrayIndex"/>
        /// </summary>
        /// <param name="array">The array to copy to</param>
        /// <param name="arrayIndex">The index to start copying at</param>
        public void CopyTo(IMenuItem[] array, int arrayIndex)
        {
            _order.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Gets the enumerator for the order which is a list of menu items
        /// </summary>
        /// <returns>The order's enumerator</returns>
        public IEnumerator<IMenuItem> GetEnumerator()
        {
            return _order.GetEnumerator();
        }

        /// <summary>
        /// Removes a menu item from the order
        /// </summary>
        /// <param name="item">The item to remove</param>
        /// <returns>Whether the item was removed</returns>
        public bool Remove(IMenuItem item)
        {
            if (_order.Contains(item))
            {
                int index = _order.IndexOf(item);
                _order.Remove(item);
                item.PropertyChanged -= HandleOrderChanged;

                CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item, index));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Subtotal)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Tax)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Total)));
                return true;
            }
            return false;
        }

        /// <summary>
        /// Returns an enumerator that iterates through the order.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the order</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// A zero-argument constructor that sets the order number and time it was placed at
        /// </summary>
        public Order()
        {
            _lastNum++;
            Number = _lastNum;

            PlacedAt = DateTime.Now;
        }
    }
}
