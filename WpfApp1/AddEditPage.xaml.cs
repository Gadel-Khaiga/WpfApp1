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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для AddEditPage.xaml
    /// </summary>
    public partial class AddEditPage : Page
    {
        private Cars _currentCars = new Cars();
        public AddEditPage(Cars selectedCars)
        {
            InitializeComponent();

            if(selectedCars != null)
                _currentCars = selectedCars;

            DataContext = _currentCars;
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.GoBack();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_currentCars.Name))
                errors.AppendLine("Укажите название автомобиля");
            if(_currentCars.Type == null)
                errors.AppendLine("Введите тип автомобиля");
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
            }
            if (_currentCars.ID_Good == 0)
                CarsheringEntities.GetContext().Cars.Add(_currentCars);
            try
            {
                CarsheringEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена");
                Manager.MainFrame.GoBack();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
