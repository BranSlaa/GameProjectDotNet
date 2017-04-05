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
using WarCardGame.ServiceReference1;
using WarCardGameLibrary;

namespace WarCardGame
{
	/// <summary>
	/// Interaction logic for LogonWindow.xaml
	/// </summary>
	public partial class LogonWindow : Window
	{
		private string name = "";
		public LogonWindow()
		{
			InitializeComponent();
		}

		private void btnSubmit_Click(object sender, RoutedEventArgs e)
		{
			ValidateName();
		}

		private void ValidateName()
		{
			if (String.IsNullOrWhiteSpace(tbName.Text))
			{
				tbName.BorderBrush = new SolidColorBrush(Colors.Red);
				return;
			}
			tbName.BorderBrush = new SolidColorBrush(Colors.Black);
			MainWindow main = new MainWindow();
			main.Show();
			this.Close();
		}
	}
}
