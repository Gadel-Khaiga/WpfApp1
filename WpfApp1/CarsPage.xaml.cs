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
    /// Логика взаимодействия для CarsPage.xaml
    /// </summary>
    public partial class CarsPage : Page
    {
        public CarsPage()
        {
            InitializeComponent();
            DGridCars.ItemsSource = CarsheringEntities.GetContext().Cars.ToList();
        }
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPage(null));
        }

        private void BtnDel_Click(object sender, RoutedEventArgs e)
        {
            var CarsForRemouving = DGridCars.SelectedItems.Cast<Cars>().ToList();


            if(MessageBox.Show($"Вы точно хоитите удалить {CarsForRemouving.Count()} элементов?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    CarsheringEntities.GetContext().Cars.RemoveRange(CarsForRemouving);
                    CarsheringEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");
                    DGridCars.ItemsSource = CarsheringEntities.GetContext().Cars.ToList();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if(Visibility == Visibility.Visible)
            {
                CarsheringEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                DGridCars.ItemsSource = CarsheringEntities.GetContext().Cars.ToList();
            }
        }

        private void BtnEdit(object sender, RoutedEventArgs e)
        {

            Manager.MainFrame.Navigate(new AddEditPage((sender as Button).DataContext as Cars));
        }
    }
}
