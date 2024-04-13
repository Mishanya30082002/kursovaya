using System.Collections.Generic;
using System.Data;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using AvaloniaApplication2.Models;
using MySqlConnector;

namespace AvaloniaApplication2;

public partial class AddOrder : Window
{
    private DBHelper db = new DBHelper();
    private List<masters> _masters;
    private List<order> _orders;
    private List<order_spare_part> _spare;
    private List<order_service> _orderServices;
    private List<service> _services;
    private List<spare_part> _spareParts;
    public AddOrder()
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
                        reader.GetString("first_name")+ " "+
                        reader.GetString("second_name")+ " "+
                        reader.GetString("last_name"),
                        reader.GetString("comment")));
                }
                
            }
            conn.Close();
        }
        using (var conn = new MySqlConnection(db._connectionString.ConnectionString))
        {
            conn.Open();
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM order_spare_part " +
                                  "JOIN orders " +
                                  "ON order_spare_part.order = orders.id " +
                                  "JOIN spare_part " +
                                  "ON order_spare_part.scare_part = spare_part.id";
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    _spare.Add(new order_spare_part(
                        reader.GetInt16("id"),
                        reader.GetInt16("order"),
                        reader.GetInt16("scare_part"),
                        reader.GetDouble("cost_price"),
                        reader.GetDouble("price")));
                }
                
            }
            conn.Close();
        }
        using (var conn = new MySqlConnection(db._connectionString.ConnectionString))
        {
            conn.Open();
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM masters";
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    _masters.Add(new masters(
                        reader.GetInt16("id"),
                        reader.GetString("first_name"),
                        reader.GetString("second_name"),
                        reader.GetString("last_name"),
                        reader.GetString("login"),
                        reader.GetString("password")));
                }
                
            }
            conn.Close();
        }
        using (var conn = new MySqlConnection(db._connectionString.ConnectionString))
        {
            conn.Open();
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM order_service";
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    _orderServices.Add(new order_service(
                        reader.GetInt16("id"),
                        reader.GetInt16("order"),
                        reader.GetInt16("service"),
                        reader.GetDouble("price")));
                }
                
            }
            conn.Close();
        }
        using (var conn = new MySqlConnection(db._connectionString.ConnectionString))
        {
            conn.Open();
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM spare_part";
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    _spareParts.Add(new spare_part(
                        reader.GetInt16("id"),
                        reader.GetString("product_name"),
                        reader.GetString("provider"),
                        reader.GetDouble("cost_price")));
                }
                
            }
            conn.Close();
        }
        using (var conn = new MySqlConnection(db._connectionString.ConnectionString))
        {
            conn.Open();
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM service";
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    _services.Add(new service(
                        reader.GetInt16("id"),
                        reader.GetString("name_of_the_service")));
                }
                
            }
            conn.Close();
        }

        CbMaster.ItemsSource = _masters;
        cbUsluga.ItemsSource = _services;
        CbTovar.ItemsSource = _spareParts;
    }
    

    private void Incert(object? sender, RoutedEventArgs e)
    {
        int id = 0;
        using (var conn = new MySqlConnection(db._connectionString.ConnectionString))
        {
            conn.Open();
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "INSERT INTO `orders` (client_first_name, client_second_name, client_last_name, " +
                                  "client_phone_number, model, imei_or_serial_number, master, comment) " +
                                  "VALUES (@client_first_name, @client_second_name, @client_last_name, " +
                                  "@client_phone_number, @model, @imei_or_serial_number, @master, @comment)";
                cmd.Parameters.AddWithValue("@client_first_name", ftClientName.Text);
                cmd.Parameters.AddWithValue("@client_second_name", sndClientName.Text);
                cmd.Parameters.AddWithValue("@client_last_name", ltClientName.Text);
                cmd.Parameters.AddWithValue("@client_phone_number", ClientPhoneNumber.Text);
                cmd.Parameters.AddWithValue("@model", Model.Text);
                cmd.Parameters.AddWithValue("@master",((CbMaster.SelectedItem as masters)!).Id);
                cmd.Parameters.AddWithValue("@imei_or_serial_number", imei.Text);
                cmd.Parameters.AddWithValue("@comment", comments.Text);
                id = cmd.ExecuteNonQuery();
            }
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "INSERT INTO `order_spare_part` (order, scare_part, cost_price, " +
                                  "price) " +
                                  "VALUES (@order, @scare_part, @cost_price, " +
                                  "@price)";
                cmd.Parameters.AddWithValue("@order", id);
                cmd.Parameters.AddWithValue("@scare_part",(CbTovar.SelectedItem as spare_part).Id);
                cmd.Parameters.AddWithValue("@cost_price", costPr.Text);
                cmd.Parameters.AddWithValue("@price", Price.Text);  
            }
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "INSERT INTO `order_service` (order, service, price) " +
                                  "VALUES (@order, @service @price)";
                cmd.Parameters.AddWithValue("@order", id);
                cmd.Parameters.AddWithValue("@scare_part",(cbUsluga.SelectedItem as service).Id);
                cmd.Parameters.AddWithValue("@price", uPrice.Text);  
            }
            conn.Close();
        }
        Update();
    }

}