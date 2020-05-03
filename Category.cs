using System.Collections.Generic;

namespace raven_poc
{
    public class Name {
        private readonly string value;

        public Name(string value)
        {
            this.value = value;
        }

        public static implicit operator string(Name name) => name.value;
        public static explicit operator Name(string name) => new Name(name);

        public override string ToString() => value;
    }
    internal class Category
    {
        public string Id { get; private set; }
        public Name Name { get; private set; }
        public IReadOnlyCollection<string> Tags => _tags;
        protected Category()
        {
            
        }
        
        public Category(Name name)
        {
            Name = name;
        }

        public void ChangeName(Name name) => Name = name;
        public void AddTag(string tag) => _tags.Add(tag);

        private readonly List<string> _tags = new List<string>();
    }
}