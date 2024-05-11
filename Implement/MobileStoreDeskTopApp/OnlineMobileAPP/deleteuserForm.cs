using MongoDB.Driver.Core.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OnlineMobileAPP
{
    public partial class deleteuserForm : Form
    {
        string connectionString = "Data Source=.;Initial Catalog=mobilestore_db;Integrated Security=True";
        public deleteuserForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int userId;
            if (int.TryParse(textBox1.Text, out userId))
            {
                // Perform database operations
                DeleteUserProduct(userId);
                DeleteUser(userId);
            }
            else
            {
                MessageBox.Show("Please enter a valid user ID.");
            }
        }

        private bool CheckUserExistence(int userId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT COUNT(*) FROM Users WHERE ID = @UserId";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserId", userId);
                        int count = (int)command.ExecuteScalar();
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error checking user existence: " + ex.Message);
                return false; // Return false in case of an exception
            }
        }

        private void DeleteUserProduct(int userId)
        {
            try
            {
                if (!CheckUserExistence(userId))
                {
                    MessageBox.Show("User with the specified ID does not exist.");
                    return; // Exit the method since the user doesn't exist
                }

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "DELETE FROM Users_Products WHERE user_id = @UserId";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserId", userId);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting user products: " + ex.Message);
            }
        }

        private void DeleteUser(int userId)
        {
            try
            {
                if (!CheckUserExistence(userId))
                {
                    MessageBox.Show("User with the specified ID does not exist.");
                    return; // Exit the method since the user doesn't exist
                }

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "DELETE FROM Users WHERE ID = @UserId";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserId", userId);
                        command.ExecuteNonQuery();
                        MessageBox.Show("User deleted successfully.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting user: " + ex.Message);
            }
        }

    }
}
