using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Interactivity;
using AvaloniaApplication2.Models;
using MySqlConnector;

namespace AvaloniaApplication2;

public partial class MainWindow : Window
{
    private List<masters> _masters;
    DBHelper db = new DBHelper();
    public MainWindow()
    {
        InitializeComponent();
        Update();
    }
    
    private void Update()
    {
        _masters = new List<masters>();
        using (var connection = new MySqlConnection(db._connectionString.ConnectionString))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM masters";
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    _masters.Add(new masters(reader.GetInt16("id"),
                        reader.GetString("first_name"),
                        reader.GetString("second_name"),
                        reader.GetString("last_name"),
                        reader.GetString("login"),
                        reader.GetString("password")));
                }
            }
            connection.Close();
        }
    }
    
    

    private void old(object? sender, RoutedEventArgs e)
    {
        foreach (var emp in _masters)
        {
            if (!emp.log(tbName.Text, tbpassword.Text)) continue;
            new editingAnOrder().Show();
        }
        Close();
    }

    private void @new(object? sender, RoutedEventArgs e)
    {
        foreach (var emp in _masters)
        {
            if (!emp.log(tbName.Text, tbpassword.Text)) continue;
            new AddOrder().Show();
        }
        Close();
    }
}