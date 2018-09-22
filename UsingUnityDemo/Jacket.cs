namespace UsingUnityDemo
{
    public class Jacket : Product
    {
        public Jacket()
        {
            Name = "jacket";
        }

        public Jacket(string name)
        {
            Name = Name +" made in "+name;
        }
    }
}
