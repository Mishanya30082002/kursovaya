namespace AvaloniaApplication2.Models;

public class order
{
    private int _id;
    private string _clientFirstName;
    private string _clientSecondName;
    private string _clientLastName;
    private string _clientPhoneNumber;
    private string _model;
    private string _imei;
    private string _master;
    private string _comment;

    public order(int id, string clientFirstName, string clientSecondName, string clientLastName,
        string clientPhoneNumber, string model, string imei, string master, string comment)
    {
        _id = id;
        _clientFirstName = clientFirstName;
        _clientSecondName = clientSecondName;
        _clientLastName = clientLastName;
        _clientPhoneNumber = clientPhoneNumber;
        _model = model;
        _imei = imei;
        _master = master;
        _comment = comment;

    }

    public int Id => _id;
    public string ClientFirstName => _clientFirstName;
    public string ClientSecondName => _clientSecondName;
    public string ClientLastName => _clientLastName;
    public string ClientPhoneNumber => _clientPhoneNumber;
    public string Model => _model;
    public string Imei => _imei;
    public string Master => _master;
    public string Comment => _comment;
}