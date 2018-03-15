using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PasswordCrackerClient
{
    class Program
    {
        static void Main(string[] args)
        {
            // ##########################################      
            // ################# CLIENT #################    
            // ##########################################    

            try
            {
                Console.WriteLine("Client Started");

                // set the TcpClient on port 3215
                int port = 3215;
                TcpClient client = new TcpClient("localhost", port);

                Console.Write("Establishing connection... ");     

                // Get a client stream for reading and writing.
                //  Stream stream = client.GetStream();         
                NetworkStream stream = client.GetStream();

                StreamReader streamReader = new StreamReader(stream);
                StreamWriter streamWriter = new StreamWriter(stream);
                streamWriter.AutoFlush = true; // enable automatic flushing

                Console.WriteLine("Connection To Server Established Started");

                while (true)
                {
                    string message = Console.ReadLine();

                    // Send the message to the connected TcpServer. 
                    streamWriter.WriteLine(message);

                    Console.WriteLine("Sent To Server: {0}", message);

                    // Receive the TcpServer.response.

                    string serverMessage = streamReader.ReadLine();

                    // Read the first batch of the TcpServer response bytes.
                    Console.WriteLine("Received From Server: {0}", serverMessage);
                }

                // Close everything.
                stream.Close();
                client.Close();
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("ArgumentNullException: {0}", e);
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }

            Console.WriteLine("\n Press Enter to continue...");
            Console.Read();
        }
                   
    }
}