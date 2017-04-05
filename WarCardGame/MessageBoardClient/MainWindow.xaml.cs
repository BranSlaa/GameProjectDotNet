﻿/*
 *  Program:        MessageBoardClient.exe 
 *  Module:         MainWindow.xaml.cs
 *  Author:         T. Haworth
 *  Date:           March 19, 2017
 *  Description:    A modified client for the BASIC MessageBoard WCF service. Allows the user to establish an 
 *                  alias, then lets the user post messages to the message board. All the messages contained 
 *                  on the message board get displayed in a list box in the client's GUI. 
 *                  This version includes a BASIC callback feature.
 */

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
using MessageBoardLibrary;
using System.ServiceModel;
using MessageBoardClient.MessageBoardServiceRef;

namespace MessageBoardClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    ///
    [CallbackBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant, UseSynchronizationContext = false)]
    public partial class MainWindow : Window, MessageBoardServiceRef.IUserCallback
    {
        private Deck deck = new Deck();
        private UserClient msgBrd = null;
        private string name = "";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void buttonSubmit_Click(object sender, RoutedEventArgs e)
        {
            bool canbePlayed = msgBrd.CanBePlayed(name);
            if (canbePlayed)
            {
                try
                {
                    Card card = deck.Draw();
                    string c = card.Name;
                    msgBrd.PostMessage(name + " has played a " + c);
                    string winner = msgBrd.addCard(name, card);
                    if(winner != "")
                    {
                        msgBrd.PostMessage(winner);
                    }
                    
                    textPost.Clear();
                    // ** Now handled by callback **
                    //listMessages.ItemsSource = msgBrd.GetAllMessages();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                msgBrd.PostMessage("You have already played a card");
            }
        }

        private void buttonSet_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                if (textAlias.Text != "")
                {
                    name = textAlias.Text;
                    buttonSet.IsEnabled = textAlias.IsEnabled = false;
                    buttonSubmit.IsEnabled = textPost.IsEnabled = listMessages.IsEnabled = true;
                    
                    connectToMessageBoard();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonRefresh_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                listMessages.ItemsSource = msgBrd.GetAllMessages();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (msgBrd != null)
                msgBrd.Leave(textAlias.Text);
        }

        // Helper methods

        private void connectToMessageBoard()
        {
            try
            {
                msgBrd = new UserClient(new InstanceContext(this));
                
                if (msgBrd.Join(textAlias.Text))
                {
                    // Alias accepted by the service so update GUI
                    listMessages.ItemsSource = msgBrd.GetAllMessages();
                    textAlias.IsEnabled = buttonSet.IsEnabled = false;                
                }
                else
                {
                    // Alias rejected by the service so nullify service proxies
                    msgBrd = null;
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
                    listMessages.ItemsSource = messages;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
                this.Dispatcher.BeginInvoke(new GuiUpdateDelegate(SendAllMessages), new object[] { messages });
        }

    }
}
