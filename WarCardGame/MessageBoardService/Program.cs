/*
 *  Program:        MessageBoardService.exe 
 *  Module:         Program.cs
 *  Author:         T. Haworth
 *  Date:           March 19, 2017
 *  Description:    This assembly provides a process for hosting a WCF service implemented by
 *                  MessageBoardLibrary.MessageBoard.
 *                  This version includes a BASIC callback feature as outlined in the Lab 10 instructions.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ServiceModel;
using MessageBoardLibrary;

namespace MessageBoardService
{
    public class Program
    {
        static void Main(string[] args)
        {
            ServiceHost servHost = null;
            try
            {
                // Create the service host 
                servHost = new ServiceHost( typeof(MessageBoard) );

                // Start the service
                servHost.Open();
                Console.WriteLine("Service started. Press a key to quit.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.ReadKey();
                if (servHost != null)
                    servHost.Close();
            }
        }

    }
}
