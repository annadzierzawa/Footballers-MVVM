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

namespace Piłkarze
{
      public partial class MainWindow : Window
    {

        public MainWindow()
        {
            TextBox.BrushForAll = Brushes.Red;
            InitializeComponent();
            TextBoxFirstName.SetFocus();
        }
        private bool IsNotEmpty(TextBox textbox)
        {
            if (textbox.Text.Trim() == "")
            {
                textbox.SetError("Empty field!");
                return false;
            }
            textbox.SetError("");
            return true;
        }
        private void Clear()
        {
            TextBoxFirstName.Text = "";
            TextBoxSurname.Text = "";
            sliderWeight.Value = 75;
            sliderAge.Value = 25;
            buttonEdit.IsEnabled = false;
            buttonDelete.IsEnabled = false;
            listBoxFootballer.SelectedIndex = -1;
            TextBoxFirstName.SetFocus();
        }
        private void LoadPlayer(Footballer footballer)
        {
            TextBoxFirstName.Text = footballer.FirstName;
            TextBoxSurname.Text = footballer.Surname;
            sliderWeight.Value = footballer.Weight;
            sliderAge.Value = footballer.Age;
            buttonEdit.IsEnabled = true;
            buttonDelete.IsEnabled = true;
            TextBoxFirstName.SetFocus();
        }
        private void buttonEditClick(object sender, RoutedEventArgs e)
        {
            if (IsNotEmpty(TextBoxFirstName) & IsNotEmpty(TextBoxSurname))
            {
                var currentFootballer = new Footballer(TextBoxFirstName.Text.Trim(), TextBoxSurname.Text.Trim(), (uint)sliderAge.Value, (uint)sliderWeight.Value);
                var isAlreadyOnTheList = false;
                foreach (var p in listBoxFootballer.Items)
                {
                    var footballer = p as Footballer;
                    if (footballer.isTheSame(currentFootballer))
                    {
                        isAlreadyOnTheList = true;
                        break;
                    }
                }
                if (!isAlreadyOnTheList)
                {
                    var dialogResult = MessageBox.Show($"Areyou sure you want to change data  {Environment.NewLine} {listBoxFootballer.SelectedItem}?", "Edit", MessageBoxButton.YesNo);

                    if (dialogResult == MessageBoxResult.Yes)
                    {
                        (listBoxFootballer.Items[listBoxFootballer.SelectedIndex] as Footballer).Age= (uint)sliderAge.Value; 
                        (listBoxFootballer.Items[listBoxFootballer.SelectedIndex] as Footballer).Weight= (uint)sliderWeight.Value; 
                        (listBoxFootballer.Items[listBoxFootballer.SelectedIndex] as Footballer).FirstName = TextBoxFirstName.Text.Trim();
                        (listBoxFootballer.Items[listBoxFootballer.SelectedIndex] as Footballer).Surname = TextBoxSurname.Text.Trim();
                        listBoxFootballer.Items.Refresh();

                    }
                    Clear();
                    listBoxFootballer.SelectedIndex = -1;

                }
                else
                    MessageBox.Show($"{currentFootballer.ToString()} is on the list.", "Warring!");

            }
        }
        private void buttonAddClick(object sender, RoutedEventArgs e)
        {
            if (IsNotEmpty(TextBoxFirstName) & IsNotEmpty(TextBoxSurname))
            {
                var currentFootballer = new Footballer(TextBoxFirstName.Text.Trim(), TextBoxSurname.Text.Trim(), (uint)sliderAge.Value, (uint)sliderWeight.Value);
                var isAlreadyOnTheList = false;
                foreach (var p in listBoxFootballer.Items)
                {
                    var footballer = p as Footballer;
                    if (footballer.isTheSame(currentFootballer))
                    {
                        isAlreadyOnTheList = true;
                        break;
                    }
                }
                if (!isAlreadyOnTheList)
                {
                    listBoxFootballer.Items.Add(currentFootballer);
                    Clear();
                }
                else
                {
                    var dialog = MessageBox.Show($"{currentFootballer.ToString()} już jest na liście {Environment.NewLine} Czy wyczyścić formularz?", "Uwaga", MessageBoxButton.OKCancel);
                    if (dialog == MessageBoxResult.OK)
                    {
                        Clear();
                    }

                }
            }
        }
        private void buttonDeleteClick(object sender, RoutedEventArgs e)
        {
            if (IsNotEmpty(TextBoxFirstName) & IsNotEmpty(TextBoxSurname))
            {
                var currentFootballer = new Footballer(TextBoxFirstName.Text.Trim(), TextBoxSurname.Text.Trim(), (uint)sliderAge.Value, (uint)sliderWeight.Value);

                foreach (var p in listBoxFootballer.Items)
                {
                    var footballer = p as Footballer;
                    if (footballer.isTheSame(currentFootballer))
                    {
                        var dialogResult = MessageBox.Show($"Are you sure you want to delete  {Environment.NewLine} {listBoxFootballer.SelectedItem}?", "Edit", MessageBoxButton.YesNo);

                        if (dialogResult == MessageBoxResult.Yes)
                        {
                            listBoxFootballer.Items.Remove(p);
                        }
                        break;
                    }
                }
                Clear(); 

            }
        }
        private void listBoxfootballereSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listBoxFootballer.SelectedIndex > -1)
            {
                LoadPlayer((Footballer)listBoxFootballer.SelectedItem);
            }

        }
        
        private void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            int n = listBoxFootballer.Items.Count;
            Footballer[] footballers = null;
            if (n > 0)
            {
                footballers = new Footballer[n];
                int index = 0;
                foreach (var o in listBoxFootballer.Items)
                {
                    footballers[index++] = o as Footballer;
                }
                FootballersRepository.WriteToFile(footballers);
            }


        }
  
        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            var footballers = FootballersRepository.ReadFromFile();
            if (footballers != null)
                foreach (var p in footballers)
                {
                    listBoxFootballer.Items.Add(p);
                }

        }
    }
}
