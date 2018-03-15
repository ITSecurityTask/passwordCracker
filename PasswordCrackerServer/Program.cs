using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;              

namespace PasswordCrackerServer
{
    class Program
    {
        public static List<TcpClient> connectedClients = new List<TcpClient>();        

        static void Main(string[] args)
        {
            // ##########################################      
            // ################# SERVER #################    
            // ##########################################     

            Cracking runCracking = new Cracking();
            runCracking.RunCracking();

            try
            {
                Console.WriteLine("Server Started");

                // set the TcpListener on port 13000
                int port = 3215;
                TcpListener server = new TcpListener(IPAddress.Any, port);

                // Start listening for client requests
                server.Start();



                //Enter the listening loop
                while (true)
                {
                    Console.WriteLine("Press any key to start cracking proccess");
                    Console.Write("Waiting for a connection... ");

                    // Perform a blocking call to accept requests.
                    // You could also user server.AcceptSocket() here.
                    TcpClient client = server.AcceptTcpClient();

                    

                    ThreadPool.QueueUserWorkItem(ClientProcess, client);



                }
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }


            Console.WriteLine("Hit enter to continue...");
            Console.Read();
        }
        private static void ClientProcess(object obj)
        {
            TcpClient client = (TcpClient)obj;
            connectedClients.Add(client);

            string message;

            Console.WriteLine(connectedClients.Count + " client(s) Connected!");

            // Get a stream object for reading and writing
            NetworkStream stream = client.GetStream();

            StreamReader streamReader = new StreamReader(stream);
            StreamWriter streamWriter = new StreamWriter(stream);
            streamWriter.AutoFlush = true; // enable automatic flushing


            // Loop to receive all the data sent by the client.
            message = streamReader.ReadLine();

            while (!string.IsNullOrEmpty(message))
            {


                // Write received message.                                                  
                Console.WriteLine($"Received From Client: {message}");

                // Process the data sent by the client.
                message = message.ToUpper();

                // Send back a response.
                streamWriter.WriteLine(message);

                Console.WriteLine($"Sent To Client: {message}");

                // Loop to receive all the data sent by the client.
                message = streamReader.ReadLine();
            }

            // Shutdown and end connection
            client.Close();
        }
    }
}
