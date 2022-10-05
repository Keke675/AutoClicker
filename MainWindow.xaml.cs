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
using System.Windows.Forms;

namespace AutoClicker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        Trigger args = new Trigger();

        public MainWindow()
        {
            args.is_unablable = false;
            InitializeComponent();
            MainGrid.MouseUp += new MouseButtonEventHandler(Grid_Up);
        }

        private void Grid_Up(object sender, MouseButtonEventArgs e)
        {
            System.Windows.MessageBox.Show("You clicked me at " + e.GetPosition(this).ToString());
            //System.Windows.MessageBox.Show((e.X) + ", " + (e.Y) + " ClickType:" + e.Button);
            //leftclick(new Point(e.X, e.Y));
        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            key_trigger_label.Content = "Key Pressed : " + "'" + args.KeyTrigger_string + "'";
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListBox_Key.SelectedItem != null)
            {
                //ComboBoxItem cbi1 = (ComboBoxItem)(sender as ComboBox).SelectedItem;
                ComboBoxItem cbi = (ComboBoxItem)ListBox_Key.SelectedItem;
                args.KeyTrigger_string = cbi.Content.ToString();
                switch (cbi.Content.ToString())
                {
                    case "Shift":
                        args.KeyTrigger = ModifierKeys.Shift;
                        break;
                    case "Alt":
                        args.KeyTrigger = ModifierKeys.Alt;
                        break;
                    case "Ctrl":
                        args.KeyTrigger = ModifierKeys.Control;
                        break;
                    case "Windows":
                        args.KeyTrigger = ModifierKeys.Windows;
                        break;
                }
                key_trigger_label.Content = "Key selected is : " + "'" + args.KeyTrigger_string + "'";
                args.is_unablable = false;
                unable_label.Content = "Press " + "'" + args.KeyTrigger_string + "'" + " to unable the autoclick";
                enable_btn.Visibility = Visibility.Visible;
                border_rectangle.Visibility = Visibility.Visible;
            }
        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern void mouse_event(int dwFlags, int dx, int dy, int dwdata, int dwextrainfo);
        public enum mouseeventflags
        {
            LeftDown = 2,
            LeftUp = 4,
        }
        public void leftclick(Point p)
        {
            mouse_event((int)(mouseeventflags.LeftDown), (int)p.X, (int)p.Y, 0, 0);
            mouse_event((int)(mouseeventflags.LeftUp), (int)p.X, (int)p.Y, 0, 0);
        }
    }


    class Trigger
    {
        public ModifierKeys KeyTrigger { get; set; }
        public string KeyTrigger_string { get; set; }
        public bool is_unablable { get; set; }
    }
}
