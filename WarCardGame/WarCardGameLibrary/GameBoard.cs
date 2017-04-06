/*
 *  Program:        MessageBoardLibrary.dll 
 *  Module:         MessageBoard.cs
 *  Author:         T. Haworth
 *  Date:           March 19, 2017
 *  Description:    Defines a BASIC MessageBoard type that maintains and returns a collection of message 
 *                  strings that are posted individually by a client (or clients).
 *                  Note that the messages do not persist when the app is shut down.
 *                  This version includes a BASIC callback feature.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace WarCardGameLibrary
{
    /*------------------------------- Client Callback Contracts ------------------------------*/

    public interface IUserCallback
    {
        [OperationContract(IsOneWay = true)]
        void SendAllMessages(string[] messages);
    }

    /*----------------------------------- Service Contracts ----------------------------------*/

    [ServiceContract(CallbackContract = typeof(IUserCallback))]
    public interface IUser
    {
        [OperationContract]
        bool Join(string name);
        [OperationContract]
        bool CanBePlayed(string name);
        [OperationContract]
        string addCard(string name);
        [OperationContract]
        string findWinner();
        [OperationContract(IsOneWay = true)]
        void Leave(string name);
        [OperationContract(IsOneWay = true)]
        void PostMessage(string message);
        [OperationContract]
        Dictionary<string, int> getScores();
        [OperationContract]
        string[] GetAllMessages();
    }

    /*--------------------------------- Service Implementation -------------------------------*/

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class GameBoard : IUser
    {
        private Deck deck = new Deck();
        private Dictionary<string, IUserCallback> userCallbacks
            = new Dictionary<string, IUserCallback>();
        private Dictionary<string, Card> allCards = new Dictionary<string, Card>();
        private Dictionary<string, int> playerPoints = new Dictionary<string, int>();
        private List<string> messages = new List<string>();

        /*----------------------------------- IUser methods ----------------------------------*/

        public bool Join(string name)
        {
            if (userCallbacks.ContainsKey(name.ToUpper()))
                // User alias must be unique
                return false;
            else
            {
                // Retrieve client's callback proxy
                IUserCallback cb =
                    OperationContext.Current.GetCallbackChannel<IUserCallback>();

                // Save alias and callback proxy    
                userCallbacks.Add(name.ToUpper(), cb);

                //add name to points Dictionary
                playerPoints.Add(name, 0);

                return true;
            }
        }

        public void Leave(string name)
        {
            if (userCallbacks.ContainsKey(name.ToUpper()))
            {
                userCallbacks.Remove(name.ToUpper());
                playerPoints.Remove(name);
            }
        }

        public void PostMessage(string message)
        {
            messages.Insert(0, message);
            updateAllUsers();
        }

        public string[] GetAllMessages()
        {
            return messages.ToArray<string>();
        }

        /*---------------------------------- Helper methods ----------------------------------*/

        private void updateAllUsers()
        {
            String[] msgs = messages.ToArray<string>();
            foreach (IUserCallback cb in userCallbacks.Values)
                cb.SendAllMessages(msgs);
        }

        public string addCard(string name)
        {
            Card card = deck.Draw();
            allCards.Add(name, card);
            return card.Name;
        }
        public string findWinner()
        {  
            string whoWon = "";
            string amountOfPoints = "";
            if(allCards.Count == userCallbacks.Count && allCards.Count > 1)
            {
                KeyValuePair<string, Card> tempWinner = allCards.ElementAt(0);
                for(int i = 1; i < allCards.Count; i++)
                {
                    KeyValuePair<string, Card> temp = allCards.ElementAt(i);
                    if(temp.Value.Rank < tempWinner.Value.Rank)
                    {
                        tempWinner = temp;
                    }
                    //compare suit if rank are the same
                    else if(temp.Value.Rank == tempWinner.Value.Rank)
                    {
                        if(temp.Value.Suit > tempWinner.Value.Suit)
                        {
                            tempWinner = temp;
                        }
                    }
                }//end for
                //do a foreach and increase the winners score
                foreach(KeyValuePair<string, int> kvp in playerPoints)
                {
                    if(kvp.Key == tempWinner.Key)
                    {
                        playerPoints[kvp.Key] = kvp.Value + 1;
                        amountOfPoints = playerPoints[kvp.Key].ToString();

                        break;
                    }
                }

                bool endGame = false;
                //check to see if someone has 10 points.  if they do, then end the game
                foreach(KeyValuePair<string, int> kvp in playerPoints)
                {
                    if(kvp.Value >= 10)
                    {
                        endGame = true;
                        break;
                    }
                }
                if(endGame)
                {
                    whoWon = tempWinner.Key + " has 10 points.  They have won the game!\n" +  tempWinner.Key + 
                        " has won with a " + tempWinner.Value.Name + ". They now have " + amountOfPoints + " point(s)";
                }
                else
                {
                    whoWon = tempWinner.Key + " has won with a " + tempWinner.Value.Name + ". They now have " +
                   amountOfPoints + " point(s)";
                    allCards.Clear();
                }
               
            }
            return whoWon;
        }

        public bool CanBePlayed(string name)
        {
            bool canBePlayed = true;
            //go through allcards and see if the name is already in there
            foreach(string n in allCards.Keys)
            {
                if(name == n)
                {
                    canBePlayed = false;
                }
            }
            return canBePlayed;
        }

        public Dictionary<string, int> getScores()
        {
            return playerPoints;
        }
    }
}
