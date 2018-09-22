namespace UsingUnityDemo
{
    public interface IProductFactory
    {
        string FactoryName { get; set; }
        Product Create();
    }
}
