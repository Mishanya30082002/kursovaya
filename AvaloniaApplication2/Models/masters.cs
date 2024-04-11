namespace AvaloniaApplication2.Models;

public class masters
{
    private int _id;
    private string _firstName;
    private string _secondName;
    private string _lastName;
    private string _login;
    private string _password;

    public masters(int id, string firstName, string secondName, string lastName, string login, string password)
    {
        _id = id;
        _firstName = firstName;
        _secondName = secondName;
        _lastName = lastName;
        _login = login;
        _password = password;
    }
    public bool log(string login, string password)
    {
        if (_login == login && _password == password) return true;
        return false;
    }

    public int Id => _id;
    public string FirstName => _firstName;
    public string SecondName => _secondName;
    public string LastName => _lastName;
    public string Login => _login;
    public string Password => _password;
    
}