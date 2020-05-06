using System.Collections.Generic;
using System.Linq;

namespace mongodb_poc
{
    public class Category
    {
        public string Id { get; private set; }
        public Name Name { get; private set; }
        public IReadOnlyCollection<string> Tags 
        { 
            get => _tags; 
            private set { _tags = value.ToList(); } //mongodb needs a private setter to link the property to the backing field
        }
        protected Category()
        {
            
        }
        
        public Category(Name name)
        {
            Name = name;
        }

        public void ChangeName(Name name) => Name = name;
        public void AddTag(string tag) => _tags.Add(tag);

        private List<string> _tags = new List<string>();
    }
}