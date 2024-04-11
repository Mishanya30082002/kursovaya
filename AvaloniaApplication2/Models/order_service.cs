

namespace AvaloniaApplication2.Models;

public class order_service
{
    private int _id;
    private int _order;
    private int _service;
    private double _price;

    public order_service(int id, int order, int service, double price)
    {
        _id = id;
        _order = order;
        _service = service;
        _price = price;
    }

    public int Id => _id;
    public int Order => _order;
    public int Service => _service;
    public double Price => _price;
}