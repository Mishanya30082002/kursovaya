namespace AvaloniaApplication2.Models;

public class order_spare_part
{
    private int _id;
    private string _order;
    private string _scarePart;
    private double _costPrice;
    private double _price;

    public order_spare_part(int id, string order, string scarePart, double costPrice, double price)
    {
        _id = id;
        _order = order;
        _scarePart = scarePart;
        _costPrice = costPrice;
        _price = price;
    }

    public int Id => _id;
    public string Order => _order;
    public string ScarePart => _scarePart;
    public double CostPrice => _costPrice;
    public double Price => _price;
}