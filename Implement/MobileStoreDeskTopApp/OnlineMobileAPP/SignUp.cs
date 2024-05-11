using Microsoft.VisualBasic.ApplicationServices;
using System.Data.SqlClient;
using System.Text.RegularExpressions;


namespace OnlineMobileAPP
{
    public partial class SignUp : Form
    {
        string userName;
        string Password;
        string email;
        string role = "";
        string gender = "";
        string repassword;

        public SignUp()
        {
            InitializeComponent();
        }
        private string validateAllReqinfo()
        {
            userName = textBox1.Text;
            email = textBox2.Text;
            Password = textBox3.Text;
            repassword = textBox4.Text;

            // Check if username contains only letters
            if (!Regex.IsMatch(userName, "^[a-zA-Z]+$"))
            {
                return "Username shouldn't contain neither digits nor special characters.";
            }


            // Check if email format is correct
            if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                return "Invalid email format.";
            }
            else
            {
                // Check if email already exists in the database
                string connectionString = "Data Source=.;Initial Catalog=mobilestore_db;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand checkEmailCommand = new SqlCommand("SELECT COUNT(*) FROM dbo.Users WHERE Email = @Email", connection);
                    checkEmailCommand.Parameters.AddWithValue("@Email", email);
                    int emailCount = (int)checkEmailCommand.ExecuteScalar();

                    if (emailCount > 0)
                    {
                        return "Email already exists.";
                    }
                }

            }


            // Check if password meets the criteria (minimum length of 8, contains at least one letter, one digit, and one special character)
            if (!Regex.IsMatch(Password, @"^(?=.*[a-zA-Z])(?=.*\d)(?=.*[^a-zA-Z\d]).{8,}$"))
            {
                return "Password must be at least 8 characters long and contain at least one letter, one digit, and one special character.";
            }

            //check password rematch
            if (Password != repassword)
            {
                return "Password isn't match.";
            }

            // Check gender is selected
            if (radioButton3.Checked)
            {
                gender = "Male";
            }
            else if (radioButton4.Checked)
            {
                gender = "Female";
            }
            else
            {
                return "Gender is required.";
            }

            // Check role is selected
            if (radioButton2.Checked)
            {
                role = "supplier";
            }
            else if (radioButton1.Checked)
            {
                role = "client";
            }
            else
            {
                return "Role is required.";
            }

            return role;

        }

        private void join(object sender, EventArgs e)
        {
            string validationResult = validateAllReqinfo();
            if (validationResult == "Validation passed.")
            {
                // Insert user into the database
                string connectionString = "Data Source=.;Initial Catalog=mobilestore_db;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("INSERT INTO dbo.Users (UserName, Email, Password, Gender, Role) VALUES (@UserName, @Email, @Password, @Gender, @Role)", connection))
                    {
                        command.Parameters.AddWithValue("@UserName", userName);
                        command.Parameters.AddWithValue("@Email", email);
                        command.Parameters.AddWithValue("@Password", Password);
                        command.Parameters.AddWithValue("@Gender", gender);
                        command.Parameters.AddWithValue("@Role", role);

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            // Hide the current form
                            this.Hide();

                            // Show the next form based on the role
                            if (role == "client")
                            {
                                //Redirect to Client
                            }
                            else if (role == "supplier")
                            {
                                using (SqlConnection connection2 = new SqlConnection(connectionString))
                                {
                                    connection2.Open();
                                    using (SqlCommand command2 = new SqlCommand("SELECT ID FROM dbo.Users WHERE UserName = @UserName", connection2))
                                    {
                                        command2.Parameters.AddWithValue("@UserName", userName);
                                        SqlDataReader reader = command2.ExecuteReader();
                                        if (reader.Read())
                                        {
                                            LogIn.userID = Convert.ToInt32(reader["ID"]);
                                        }
                                    }
                                }
                                Supplier supplierForm = new Supplier(LogIn.userID);
                                supplierForm.Show();
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show(validationResult, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
    } 