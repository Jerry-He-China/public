namespace UsingUnityDemo
{
    public class Shoe : Product
    {
        public Shoe()
        {
            Name = "Nike";
        }
        public Shoe(string name)
        {
            Name = Name + " made in " + name;
        }
    }
}
