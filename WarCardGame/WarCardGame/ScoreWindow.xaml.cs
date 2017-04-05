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

namespace WarCardGame
{
	/// <summary>
	/// Interaction logic for ScoreWindow.xaml
	/// </summary>
	public partial class ScoreWindow : Window
	{
		public ScoreWindow(Dictionary<string, int> scores)
		{
			InitializeComponent();

			foreach (var score in scores)
			{
				lvScore.Items.Add(score);
			}

			this.DataContext = this;
		}
	}
}
