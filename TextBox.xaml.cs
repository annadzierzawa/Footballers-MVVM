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
    
    public partial class TextBox : UserControl
    {
        public string Text
        {
            get  { return textBox.Text;}
            set  {    textBox.Text = value; }
        }

        public Brush TextBoxBorderBrush
        {
            get { return border.BorderBrush; }
            set {border.BorderBrush = value;}
        }
        public static Brush BrushForAll { get; set; } = Brushes.Red;
        public TextBox()
        {
            InitializeComponent();
            border.BorderBrush = BrushForAll;
        }
        public void SetError(string errorText)
        {
            textBlockToolTip.Text = errorText;

            if (errorText != "")
            {
                border.BorderThickness = new Thickness(1);
                toolTip.Visibility = Visibility.Visible;}
            else
            {
                border.BorderThickness = new Thickness(0);
                toolTip.Visibility = Visibility.Hidden;
            }
        }

        public void SetFocus()
        {
            textBox.Focus();
        }
    }
}
