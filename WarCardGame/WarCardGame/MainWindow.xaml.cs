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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WarCardGameLibrary;
using WarCardGame.ServiceReference1;


namespace WarCardGame
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	[CallbackBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant, UseSynchronizationContext = false)]
	public partial class MainWindow : Window, WarCardGame.ServiceReference1.IUserCallback
	{
		private Deck deck = new Deck();
		private UserClient gameBrd = null;
		private string name = "";
		public MainWindow()
		{
			InitializeComponent();
		}

		private void btnPlayNextCard_Click(object sender, RoutedEventArgs e)
		{
			bool canbePlayed = gameBrd.CanBePlayed(name);
			if (canbePlayed)
			{
				try
				{
					Card card = deck.Draw();
					string c = card.Name;
					gameBrd.PostMessage(name + " has played a " + c);
					string winner = gameBrd.addCard(name, card);
					if (winner != "")
						gameBrd.PostMessage(winner);
					
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
				}
			}
			else
			{
				gameBrd.PostMessage("You have already played a card");
			}
		}

		private void btnViewScores_Click(object sender, RoutedEventArgs e)
		{
            Dictionary<string, int> scores = gameBrd.getScores();
		}

		private void connectToMessageBoard()
		{
			try
			{
				gameBrd = new UserClient(new InstanceContext(this));

				if (gameBrd.Join(tbPlayerName.Text))
				{
					// Alias accepted by the service so update GUI
					lbPlayedCards.ItemsSource = gameBrd.GetAllMessages();
					tbPlayerName.IsEnabled = btnSetName.IsEnabled = false;
				}
				else
				{
					// Alias rejected by the service so nullify service proxies
					gameBrd = null;
					MessageBox.Show("ERROR: Alias in use. Please try again.");
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private delegate void GuiUpdateDelegate(string[] messages);

		public void SendAllMessages(string[] messages)
		{
			if (this.Dispatcher.Thread == System.Threading.Thread.CurrentThread)
			{
				try
				{
					lbPlayedCards.ItemsSource = messages;
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
				}
			}
			else
				this.Dispatcher.BeginInvoke(new GuiUpdateDelegate(SendAllMessages), new object[] { messages });
		}

		private void btnSetName_Click(object sender, RoutedEventArgs e)
		{
			try
			{

				if (tbPlayerName.Text != "")
				{
					name = tbPlayerName.Text;
					btnSetName.IsEnabled = tbPlayerName.IsEnabled = false;
					btnPlayNextCard.IsEnabled = lbPlayedCards.IsEnabled = true;

					connectToMessageBoard();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
	}
}