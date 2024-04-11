namespace AvaloniaApplication2.Models;

public class order_spare_part
{
    private int _id;
    private int _order;
    private int _scarePart;
    private double _costPrice;
    private double _price;

    public order_spare_part(int id, int order, int scarePart, double costPrice, double price)
    {
        _id = id;
        _order = order;
        _scarePart = scarePart;
        _costPrice = costPrice;
        _price = price;
    }

    public int Id => _id;
    public int Order => _order;
    public int ScarePart => _scarePart;
    public double CostPrice => _costPrice;
    public double Price => _price;
}