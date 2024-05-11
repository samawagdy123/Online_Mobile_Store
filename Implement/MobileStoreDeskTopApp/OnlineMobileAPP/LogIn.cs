using System.Data;
using System.Data.SqlClient;


namespace OnlineMobileAPP
{
    public partial class LogIn : Form
    {
        string Email;
        string Password;
        public static int userID = 0;

        public LogIn()
        {

            InitializeComponent();
        }


        private string validateUserPassword()
        {
            Email = textBox1.Text;
            Password = textBox2.Text;

           
            string connectionString = "Data Source=.;Initial Catalog=mobilestore_db;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand checkUserCommand = new SqlCommand("SELECT Role, Password FROM dbo.Users WHERE Email = @Email", connection);
                checkUserCommand.Parameters.AddWithValue("@Email", Email);
                SqlDataReader reader = checkUserCommand.ExecuteReader();

                if (reader.Read())
                {
                    string storedPassword = reader["Password"].ToString();
                    if (Password == storedPassword)
                    {
                        return reader["Role"].ToString(); // Login successful, return user's role
                    }
                    else
                    {
                        return "Password is invalid";
                    }
                }
                else
                {
                    return "User Name is invalid";
                }
            }
        }

        private void check(object sender, EventArgs e)
        {
            string output = validateUserPassword();
            switch (output)
            {
                case "admin":
                    Admin admin = new Admin();
                    admin.Show();
                    break;
                case "client":
                    // Redirect to client page
                    break;
                case "supplier":
                    string connectionString = "Data Source=.;Initial Catalog=mobilestore_db;Integrated Security=True";
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand("SELECT ID FROM dbo.Users WHERE Email = @Email", connection);
                        command.Parameters.AddWithValue("@Email", Email);
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            userID = Convert.ToInt32(reader["ID"]);
                        }
                    }
                    Supplier supplierForm = new Supplier(userID);
                    supplierForm.Show();
              
                    break;
                default:
                    MessageBox.Show(output, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    break;
            }
        }

        private void NeedToJoin(object sender, EventArgs e)
        {
            SignUp join = new SignUp();
            join.Show();
           
        }
    }
}
