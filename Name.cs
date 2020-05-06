namespace mongodb_poc
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
}