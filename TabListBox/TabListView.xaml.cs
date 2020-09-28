using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace TabListBox
{
    /// <summary>
    /// Interaction logic for TabListView.xaml
    /// </summary>
    public partial class TabListView : UserControl
    {
        PictureCollection p1;
        PictureCollection p2;
        int ComboValue;
        int List1Value;
        int List2Value;

        public TabListView()
        {
            InitializeComponent();
            combo.Items.Add(2);
            combo.Items.Add(3);
            combo.Items.Add(4);
            combo.Items.Add(5);
            combo.Items.Add(6);
            image1.Visibility = Visibility.Hidden;
            image2.Visibility = Visibility.Hidden;
            listBox1.Visibility = Visibility.Hidden;
            listBox2.Visibility = Visibility.Hidden;
        }
      
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List1Value = -1;
            List2Value = -1;
            WriteText();
       
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            List2Value = -1;
            WriteText();
       
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            WriteText();
        }

        private void listBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List1Value = listBox1.SelectedIndex;
            p2 = new PictureCollection(ComboValue, List1Value);
            listBox2.DataContext = p2.PObjects;
            listBox2.Visibility = Visibility.Visible;
            image1.Visibility = Visibility.Visible;
            image1.Source = new BitmapImage(p1.PObjects[List1Value].CellImage);
        }

        private void listBox2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List2Value = listBox2.SelectedIndex;
            image2.Visibility = Visibility.Visible;
            image2.Source = new BitmapImage(p2.PObjects[List2Value].CellImage);
        }
        
        private void combo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboValue = (int)combo.Items[combo.SelectedIndex];
            p1 = new PictureCollection(ComboValue);
            listBox1.DataContext = p1.PObjects;
            listBox1.Visibility = Visibility.Visible;
        }

        private void WriteText()
        {
            StreamWriter strw = new StreamWriter("task.txt");
            strw.WriteLine(ComboValue);
            strw.WriteLine(List1Value);
            strw.WriteLine(List2Value);
            strw.Close();
        }
        
    }
}
