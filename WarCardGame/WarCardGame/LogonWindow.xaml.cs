using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
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
	[CallbackBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant, UseSynchronizationContext = false)]
	public partial class LogonWindow : Window, WarCardGame.ServiceReference1.IUserCallback
	{
		private UserClient gameBrd = null;
		
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

			connectToMessageBoard();
		}

		private void connectToMessageBoard()
		{
			try
			{
				gameBrd = new UserClient(new InstanceContext(this));

				gameBrd.Join(tbName.Text);

				MainWindow main = new MainWindow();
				main.Show();
				this.Close();

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		public void SendAllMessages(string[] messages)
		{
			
		}
	}
}
