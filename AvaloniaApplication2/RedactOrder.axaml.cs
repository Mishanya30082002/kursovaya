using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using AvaloniaApplication2.Models;
using MySqlConnector;

namespace AvaloniaApplication2;

public partial class RedactOrder : Window
{
    private order list1;
    private DBHelper db = new DBHelper();
    public RedactOrder(order list1)
    {
        InitializeComponent();
        this.list1 = list1;
        clientFirstName.Text = list1.ClientFirstName;
        clientSecondName.Text = list1.ClientSecondName;
        clientLastName.Text = list1.ClientLastName;
        clientPhoneNumber.Text = list1.ClientPhoneNumber;
        model.Text = list1.Model;
        imei.Text = list1.Imei;
        comment.Text = list1.Comment;
    }

    private void saveRedact(object? sender, RoutedEventArgs e)
    {
        using (var conn = new MySqlConnection(db._connectionString.ConnectionString))
        {
            conn.Open();
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "UPDATE orders " +
                                  "SET client_first_name = @client_first_name, " +
                                  "client_second_name = @client_second_name, "+
                                  "client_last_name = @client_last_name, "+
                                  "client_phone_number = @client_phone_number, "+
                                  "model = @model, "+
                                  "imei_or_serial_number = @imei_or_serial_number, "+
                                  "comment = @comment "+
                                  "WHERE id = @id";121212

                cmd.Parameters.AddWithValue("@id", list1.Id);
                cmd.Parameters.AddWithValue("@client_first_name", clientFirstName.Text);
                cmd.Parameters.AddWithValue("@client_second_name", clientSecondName.Text);
                cmd.Parameters.AddWithValue("@client_last_name", clientLastName.Text);
                cmd.Parameters.AddWithValue("@client_phone_number", clientPhoneNumber.Text);
                cmd.Parameters.AddWithValue("@model", model.Text);
                cmd.Parameters.AddWithValue("@imei_or_serial_number", imei.Text);
                cmd.Parameters.AddWithValue("@comment", comment.Text);
                cmd.ExecuteNonQuery();
            }
            conn.Close();
        }
    }
}