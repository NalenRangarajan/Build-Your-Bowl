using BuildYourBowl.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.NetworkInformation;
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
    /// Interaction logic for EntreeControl.xaml
    /// </summary>
    public partial class EntreeControl : UserControl
    {
        /// <summary>
        /// Constructor for EntreeControl
        /// </summary>
        public EntreeControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Event handler that is called when an ingredient is changed
        /// </summary>
        public event EventHandler<IngredientEventArgs>? IngredientChanged;

        /// <summary>
        /// Loads CheckBoxes in the GUI for each possible ingredient for an entree
        /// </summary>
        public void LoadPossibleChoices()
        {
            if(DataContext is Entree e)
            {
                bool check = false;

                StackPanel stack = new StackPanel();

                if (IngredientsDock == null)
                {
                    // If the DockPanel doesn't exist, create a new one and add it to the grid
                    IngredientsDock = new DockPanel();
                    check = true;
                }
                else
                {
                    // Remove existing child controls from the DockPanel
                    IngredientsDock.Children.Clear();
                }

                foreach (KeyValuePair<Ingredient, IngredientItem> ingredient in e.AdditionalIngredients)
                {
                    //create checkboxes for each possible ingredient
                    CheckBox box = new CheckBox();
                    box.DataContext = ingredient.Value;
                    box.Checked += OnCheck;
                    box.Unchecked += OnUnCheck;

                    Binding binding = new Binding();
                    binding.Path = new PropertyPath(nameof(ingredient.Value.Included));
                    binding.Mode = BindingMode.TwoWay;
                    BindingOperations.SetBinding(box, CheckBox.IsCheckedProperty, binding);

                    TextBlock block = new TextBlock();
                    block.Text = ingredient.Value.Name;
                    box.Content = block;

                    stack.Children.Add(box);
                }

                IngredientsDock.Children.Add(stack);

                if (check)
                {
                    EntreeGrid.Children.Add(IngredientsDock);
                }

                Grid.SetRow(IngredientsDock, 3);
            }
        }
        
        

        /// <summary>
        /// Adds an Ingredient
        /// </summary>
        /// <param name="sender">The sender of this event</param>
        /// <param name="e">Metadata for the event</param>
        private void OnCheck(object sender, RoutedEventArgs e)
        {
            if(DataContext is Entree entree)
            {
                if (sender is CheckBox box)
                {
                    if (((CheckBox)sender).DataContext is IngredientItem i)
                    {
                        foreach (KeyValuePair<Ingredient, IngredientItem> ingredient in entree.AdditionalIngredients)
                        {
                            if (ingredient.Value == i)
                            {
                                IngredientChanged?.Invoke(this, new IngredientEventArgs(ingredient.Key, true));
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Removes an Ingredient
        /// </summary>
        /// <param name="sender">The sender of this event</param>
        /// <param name="e">Metadata for the event</param>
        private void OnUnCheck(object sender, RoutedEventArgs e)
        {
            if (DataContext is Entree entree)
            {
                if (sender is CheckBox box)
                {
                    if (((CheckBox)sender).DataContext is IngredientItem i)
                    {
                        foreach (KeyValuePair<Ingredient, IngredientItem> ingredient in entree.AdditionalIngredients)
                        {
                            if (ingredient.Value == i)
                            {
                                IngredientChanged?.Invoke(this, new IngredientEventArgs(ingredient.Key, false));
                            }
                        }
                    }
                }
            }
        }

    }
}
