using Lab4Client.CovidReference;
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

namespace Lab4Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        CovidServiceClient covidService;
        List<Contact> Contacts;
        Instance instance;
        private void AddInstanceButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (NameTextBox.Text != "" && CityTextBox.Text != "" && FirstSympotmsDatePicker.SelectedDate != null && TestDatePicker.SelectedDate != null)
                {
                    covidService = new CovidServiceClient();
                    instance = new Instance
                    {
                        Name = NameTextBox.Text,
                        City = CityTextBox.Text,
                        FirstSymptomsDate = (DateTime)FirstSympotmsDatePicker.SelectedDate,
                        TestDate = (DateTime)TestDatePicker.SelectedDate
                    };
                    covidService.AddInstance(instance);//(instance.Name,instance.City,instance.FirstSymptomsDate,instance.TestDate);
                    MessageBox.Show("Pomyślnie rozpoczęto sesję");
                }
                else
                {
                    MessageBox.Show("Pola nie mogą być puste");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                if(ex.InnerException != null)
                {
                    MessageBox.Show(ex.InnerException.Message);
                }
            }
        }

        private void AddContactButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Name2TextBox.Text != "" && City2TextBox.Text != "" && ContactDateDatePicker.SelectedDate != null)
                {
                    Contact contact = new Contact
                    {
                        Name = Name2TextBox.Text,
                        City = City2TextBox.Text,
                        ContactDate = (DateTime)ContactDateDatePicker.SelectedDate,
                        ContactWith = instance.Name + "\n" + instance.City
                    };
                    covidService.AddContact(contact);//Name2TextBox.Text, City2TextBox.Text, (DateTime)ContactDateDatePicker.SelectedDate,instance);
                    Contacts = covidService.GetContacts().ToList();
                    ContactsListBox.ItemsSource = Contacts;
                    MessageBox.Show("Pomyślnie dodano kontakt");
                }
                else
                {
                    MessageBox.Show("Pola nie mogą być puste");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                if (ex.InnerException != null)
                {
                    MessageBox.Show(ex.InnerException.Message);
                }
            }
        }

        private void EndContactButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Contacts = covidService.FinishEnteringContacts().ToList();
                ContactsListBox.ItemsSource = Contacts;
                MessageBox.Show("Pomyślnie zakończono sesję");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                if (ex.InnerException != null)
                {
                    MessageBox.Show(ex.InnerException.Message);
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Contacts = covidService.GetContacts().ToList();
                ContactsListBox.ItemsSource = Contacts;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                if (ex.InnerException != null)
                {
                    MessageBox.Show(ex.InnerException.Message);
                }
            }
        }
    }
}
