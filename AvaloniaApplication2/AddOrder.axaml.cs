using System.Collections.Generic;
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
    public AddOrder()
    {
        InitializeComponent();
        Update();
    }
    public void Update()
    {
        _masters = new List<masters>();
        _orders = new List<order>();
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

        CbMaster.ItemsSource = _masters;
    }
    

    private void Incert(object? sender, RoutedEventArgs e)
    {
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
                cmd.ExecuteNonQuery();
            }
            conn.Close();
        }
        Update();
    }

}