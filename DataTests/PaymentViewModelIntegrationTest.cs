using BuildYourBowl.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Sdk;

namespace BuildYourBowl.DataTests
{
    /// <summary>
    /// Contains the Integration tests for the PaymentViewModel class
    /// </summary>
    public class PaymentViewModelIntegrationTest
    {
        /// <summary>
        /// An integration test that tests the PaymentViewModel class
        /// </summary>
        [Fact]
        public void IntegrationTest()
        {
            Order firstOrder = new Order();

            Assert.Equal(1, firstOrder.Number);

            FirstAdd(firstOrder);

            Assert.Equal(33.47m, firstOrder.Subtotal);
            Assert.Equal(3.06m, firstOrder.Tax);
            Assert.Equal(36.53m, firstOrder.Total);

            PaymentViewModel firstModel = new PaymentViewModel(firstOrder);

            firstModel.Paid = 40.00m;

            Assert.Equal(3.47m, firstModel.Change);

            Order secondOrder = new Order();

            Assert.Equal(2, secondOrder.Number);

            SecondAdd(secondOrder);

            Assert.Equal(17.99m, secondOrder.Subtotal);
            Assert.Equal(1.65m, secondOrder.Tax);
            Assert.Equal(19.64m, secondOrder.Total);

            PaymentViewModel secondModel = new PaymentViewModel(secondOrder);

            Action action = () => secondModel.Paid = 15.00m;

            Assert.Throws<ArgumentException>(action);

        }

        /// <summary>
        /// Private method that performs the first round of item adds to the order
        /// </summary>
        /// <param name="order">The order to add items to</param>
        private void FirstAdd(Order order)
        {
            order.Add(new SlidersMeal()
            {
                ItemCount = 3,
                AmericanCheese = false,
                SideChoice = new RefriedBeans() { SizeType = Size.Medium, Onions = false },
                DrinkChoice = new AguaFresca() { DrinkFlavor = Flavor.Tamarind, DrinkSize = Size.Large, Ice = false }
            });

            Bowl b = new Bowl();
            b.EditOrderAdd(Ingredient.Steak);
            b.EditOrderAdd(Ingredient.Queso);
            b.EditOrderAdd(Ingredient.SourCream);

            order.Add(b);

            ChickenFajitaNachos c = new ChickenFajitaNachos() { SalsaType = Salsa.None};
            c.EditOrderAdd(Ingredient.Guacamole);
            c.EditOrderRemove(Ingredient.SourCream);

            order.Add(c);
        }

        /// <summary>
        /// Private method that performs the second round of item adds to the order
        /// </summary>
        /// <param name="order">The order to add items to</param>
        private void SecondAdd(Order order)
        {
            order.Add(new StreetCorn()
            {
                SizeType = Size.Large,
                Cilantro = false
            });

            order.Add(new AguaFresca()
            {
                DrinkSize = Size.Kids,
                DrinkFlavor = Flavor.Tamarind
            });

            Nachos n = new Nachos() { SalsaType = Salsa.Green};
            n.EditOrderAdd(Ingredient.BlackBeans);
            n.EditOrderAdd(Ingredient.PintoBeans);
            n.EditOrderAdd(Ingredient.Veggies);

            order.Add(n);
        }
    }
}
