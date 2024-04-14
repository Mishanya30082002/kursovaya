using System.Collections.Generic;
using System.Data;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using AvaloniaApplication2.Models;
using MySqlConnector;

namespace AvaloniaApplication2;

public partial class editingAnOrder : Window
{
    private DBHelper db = new DBHelper();
    private List<masters> _masters;
    private List<order> _orders;
    private List<order_spare_part> _spare;
    private List<order_service> _orderServices;
    private List<service> _services;
    private List<spare_part> _spareParts;
    private order _selectedOrder;
    public List<masters> Masters => _masters;

    public editingAnOrder()
    {
        InitializeComponent();
        Update();
    }

    public void Update()
    {
        _masters = new List<masters>();
        _orders = new List<order>();
        _orderServices = new List<order_service>();
        _services = new List<service>();
        _spareParts = new List<spare_part>();
        _spare = new List<order_spare_part>();

        using (var conn = new MySqlConnection(db._connectionString.ConnectionString))
        {
            _orders = new List<order>();
            _masters = new List<masters>();
            conn.Open();
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM orders " +
                                  "JOIN masters " +
                                  "ON orders.master = masters.id ";
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    _orders.Add(new order(
                        reader.GetInt16("id"),
                        reader.GetString("client_first_name"),
                        reader.GetString("client_second_name"),
                        reader.GetString("client_last_name"),
                        reader.GetString("client_phone_number"),
                        reader.GetString("model"),
                        reader.GetString("imei_or_serial_number"),
                        reader.GetString("first_name") + " " +
                        reader.GetString("second_name") + " " +
                        reader.GetString("last_name"),
                        reader.GetString("comment")));
                }

            }

            conn.Close();
        }

        List.ItemsSource = _orders;
    }


    private void Back(object? sender, RoutedEventArgs e)
    {
        new MainWindow().Show();
        this.Close();
    }

    private void newOrder(object? sender, RoutedEventArgs e)
    {
        new AddOrder().Show();
        this.Close();
    }

    private void List_SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (List.SelectedItem is order selectedOrder)
        {
            _selectedOrder = selectedOrder;
        }
    }

    private void EditOrder(object? sender, RoutedEventArgs e)
    {
        order list1 = (order)List.SelectedItem;
        new RedactOrder(list1).Show();
    }

    private void Search_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        var _list = _orders.Where(order =>
            order.Imei.Contains(Search.Text) || order.ClientFirstName.ToLower().Contains(Search.Text.ToLower()) ||
            order.ClientSecondName.ToLower().Contains(Search.Text.ToLower()) || order.ClientLastName.ToLower().Contains(Search.Text.ToLower()) ||
            order.ClientPhoneNumber.ToLower().Contains(Search.Text.ToLower())).ToList();
        List.ItemsSource = _list;
    }
}   