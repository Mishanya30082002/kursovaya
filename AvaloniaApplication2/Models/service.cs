namespace AvaloniaApplication2.Models;

public class service
{
    private int _id;
    private string _nameOfTheService;

    public service(int id, string nameOfTheService)
    {
        _id = id;
        _nameOfTheService = nameOfTheService;
    }

    public int Id => _id;
    public string NameOfTheService => _nameOfTheService;
}