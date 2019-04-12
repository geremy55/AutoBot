using System;
using System.Windows;
using System.Windows.Input;

namespace AutoBot.Views
{
    /// <summary>
    /// Interaction logic for NewAccountView.xaml
    /// </summary>
    public partial class NewAccountView : Window
    {
        public NewAccountView()
        {
            InitializeComponent();
        }       

        private void PassWindow_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                this.DragMove();
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }        
    }
}
