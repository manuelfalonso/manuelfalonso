namespace SombraStudios.Shared.Systems.Resource
{
    /// <summary>
    /// Base class for all resources.
    /// A Resource has a Name and description and it can be increased and decreased.
    /// </summary>
    public abstract class ResourceBase
    {
        public string Name { get; private set; }
        public string Description { get; private set; }


        protected ResourceBase(string name, string description)
        {
            Name = name;
            Description = description;
        }


        public override string ToString()
        {
            return $"{this} => {Name}. {Description}";
        }


        protected abstract void IncreaseResource(float amountToIncrease);
        protected abstract void DecreaseResource(float amountToDecrease);
    }
}
