namespace BuildYourBowl.Data
{
    /// <summary>
    /// The definition of the Bowl class, which is a base class
    /// </summary>
    public class Bowl : Entree
    {
        /// <summary>
        /// The name of the general bowl
        /// </summary>
        public override string Name { get; } = "Build-Your-Own Bowl";

        /// <summary>
        /// The description of the general bowl
        /// </summary>
        public override string Description { get; } = "A bowl you get to build";
    }

}
