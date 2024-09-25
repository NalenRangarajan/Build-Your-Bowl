namespace BuildYourBowl.Data
{
    /// <summary>
    /// The definition of the Nacho class, which is a base class
    /// </summary>
    public class Nachos : Entree
    {
        /// <summary>
        /// The name of the general nachos entree
        /// </summary>
        public override string Name { get; } = "Build-Your-Own Nachos";

        /// <summary>
        /// The description of the general nachos entree
        /// </summary>
        public override string Description { get; } = "Nachos you get to build";

        /// <summary>
        /// The base ingredient for the general nachos entree
        /// </summary>
        public override IngredientItem BaseIngredient { get; } = new IngredientItem(Ingredient.Chips) { Included = true, Default = true };
    }
}
