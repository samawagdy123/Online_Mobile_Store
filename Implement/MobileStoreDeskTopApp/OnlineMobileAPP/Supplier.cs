using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace OnlineMobileAPP
{
    public partial class Supplier : Form
    {
        string prod_name;
        double price = 0f;
        string type;
        string quantity;
        int sellerid;
  

        public Supplier(int sellerid)
        {
            InitializeComponent();
            this.sellerid = sellerid;
        }

        private void AddItemToDataBase(object sender, EventArgs e)
        {
            prod_name = textBox1.Text;
            price = Convert.ToDouble(textBox3.Text);
            quantity = textBox5.Text;
            type = textBox4.Text;
            sellerid =LogIn.userID;


            // Insert item into the database
            string connectionString = "Data Source=.;Initial Catalog=mobilestore_db;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("INSERT INTO products (prod_name, price, quantity, type, sellerid) VALUES (@prod_name, @price, @quantity, @type, @sellerid)", connection))
                {
                    command.Parameters.AddWithValue("@prod_name", prod_name);
                    command.Parameters.AddWithValue("@price", price);
                    command.Parameters.AddWithValue("@quantity", quantity);
                    command.Parameters.AddWithValue("@type", type);
                    command.Parameters.AddWithValue("@sellerid", sellerid);

                    // Execute the insert command and get the newly inserted ID
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        // Fetch the updated list of items and display in DataGridView
                        string query = "SELECT * FROM products";
                        SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                        DataSet dataSet = new DataSet();

                        try
                        {
                            adapter.Fill(dataSet, "Products");
                            dataGridView1.DataSource = dataSet.Tables["Products"];
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error: " + ex.Message);
                        }
                    }
                }
            }
        }
    }
}
