namespace AvaloniaApplication2.Models;

public class spare_part
{
    private int _id;
    private string _productName;
    private string _provider;
    private double _costPrice;

    public spare_part(int id, string productName, string provider, double costPrice)
    {
        _id = id;
        _productName = productName;
        _provider = provider;
        _costPrice = costPrice;
    }

    public int Id => _id;
    public string ProductName => _productName;
    public string Provider => _provider;
    public double CostPrice => _costPrice;
}