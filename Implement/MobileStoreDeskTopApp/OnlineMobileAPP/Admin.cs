
namespace OnlineMobileAPP
{
    public partial class Admin : Form
    {
        public Admin()
        {
            InitializeComponent();
        }

        private void btn_deleteproduct_Click(object sender, EventArgs e)
        {
            DeleteProducts obj = new DeleteProducts();
            obj.Show();
        }

        private void btn_delete_User_Click(object sender, EventArgs e)
        {
            deleteuserForm deleteUserFormObj = new deleteuserForm();
            deleteUserFormObj.Show();
        }
    }
}
