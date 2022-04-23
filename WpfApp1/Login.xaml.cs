using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        DataBase database = new DataBase();
        public Login()
        {
            InitializeComponent();
        }


        private void BtnEnter_Click(object sender, RoutedEventArgs e)
        {
            var LoginUser = login.Text;
            var PasswordUser = password.Password;

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            string querystring = $"select id_user, Login_User, password from Registr where Login_User = '{LoginUser}' and password = '{PasswordUser}'";

            SqlCommand command = new SqlCommand(querystring, database.getConnection());

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if( table.Rows.Count == 1)
            {
                MessageBox.Show("Пользователь авторизовался");
                MainWindow mainWindow = new MainWindow();
                this.Close();
                mainWindow.ShowDialog();
                this.Show();
            }
            else
                MessageBox.Show("Пароль или логин неправильно введен");
        }
    }
}
