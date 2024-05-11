using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace OnlineMobileAPP
{
    public partial class DeleteProducts : Form
    {
        public DeleteProducts()
        {
            InitializeComponent();
        }

        private void DeleteProducts_Load(object sender, EventArgs e)
        {
            RefreshDataGridView();
        }

        private void btn_deleteproduct_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox_prodid.Text))
            {
                MessageBox.Show("Please enter the product ID first.");
                return;
            }

            if (!int.TryParse(textBox_prodid.Text, out int productId))
            {
                MessageBox.Show("Please enter a valid integer ID.");
                return;
            }

            string deleteUsersProductsQuery = "DELETE FROM Users_Products WHERE prod_id = @ProductId";
            string deleteProductsQuery = "DELETE FROM Products WHERE prod_id = @ProductId";

            string connectionString = "Data Source=.;Initial Catalog=mobilestore_db;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();


                    using (SqlCommand usersProductsCommand = new SqlCommand(deleteUsersProductsQuery, connection))
                    {
                        usersProductsCommand.Parameters.AddWithValue("@ProductId", productId);
                        int rowsAffected = usersProductsCommand.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            using (SqlCommand productsCommand = new SqlCommand(deleteProductsQuery, connection))
                            {
                                productsCommand.Parameters.AddWithValue("@ProductId", productId);
                                int productsRowsAffected = productsCommand.ExecuteNonQuery();

                                if (productsRowsAffected > 0)
                                {
                                    MessageBox.Show("Item deleted from Users_Products and Products table successfully.");
                                    RefreshDataGridView();
                                }
                                else
                                {
                                    MessageBox.Show("Item deleted from Users_Products table, but not found in Products table.");
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("No items found with the specified ID in Users_Products table.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void RefreshDataGridView()
        {
            string connectionString = "Data Source=.;Initial Catalog=mobilestore_db;Integrated Security=True";
            string query = "SELECT * FROM products";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataSet dataSet = new DataSet();

                try
                {
                    connection.Open();
                    adapter.Fill(dataSet, "Products");
                    dataGridView_products.DataSource = dataSet.Tables["Products"];
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }
    }
}
