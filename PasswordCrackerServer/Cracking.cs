using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using PasswordCrackerServer.FileHandler;
using PasswordCrackerServer.Model;

namespace PasswordCrackerServer
{  
    public class Cracking
    {
        /// <summary>
        /// The algorithm used for encryption.
        /// Must be exactly the same algorithm that was used to encrypt the passwords in the password file
        /// </summary>
        private readonly HashAlgorithm _messageDigest;

        private BlockingCollection<string> collectionOfWords = new BlockingCollection<string>();

        public Cracking()
        {
            _messageDigest = new SHA1CryptoServiceProvider();
            //_messageDigest = new MD5CryptoServiceProvider();
            // seems to be same speed
        }

        //public BlockingCollection<string> Spliting()
        //{
        //    var numberOfClients = Program.connectedClients.Count;
        //    var numberOfWords = collectionOfWords.Count;
        //    var divisionNumber = numberOfWords / numberOfClients;

        //    if (numberOfWords % numberOfClients == 0)
        //    {
        //        for (int i = 0; i < divisionNumber; i++)
        //        {
        //            List<string> dividedWordsList = new List<string>();

        //        }
        //    }

        //}

        /// <summary>
        /// Runs the password cracking algorithm
        /// </summary>
        public void RunCracking()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            List<UserInfo> userInfos =
                PasswordFileHandler.ReadPasswordFile("passwords.txt");
            Console.WriteLine("passwd opeend");

            List<UserInfoClearText> result = new List<UserInfoClearText>();

            using (FileStream fs = new FileStream("webster-dictionary.txt", FileMode.Open, FileAccess.Read)) // deals with byte[] data

            using (StreamReader dictionary = new StreamReader(fs)) // encodes text to byte[]
            {

                
                

                while (!dictionary.EndOfStream)
                {
                    String dictionaryEntry = dictionary.ReadLine();
                    collectionOfWords.Add(dictionaryEntry);
                    Console.WriteLine(dictionaryEntry);

                    foreach (var word in collectionOfWords)
                    {
                        Console.WriteLine(word);
                    }
                  

                        // for int loop until, for instance, 1/3 of the collection, if there are 3 clients connected
                        // Get number of clients and divide collectionOfWords by the number of clients
                        // Send divided parts to the seperate clients

                        Console.ReadKey();
                    // partial result should be send as a request to be proccessed by the slave client

                    //IEnumerable<UserInfoClearText> partialResult = CheckWordWithVariations(dictionaryEntry, userInfos);
                    //result.AddRange(partialResult);
                }
            }
            stopwatch.Stop();
            Console.WriteLine(string.Join(", ", result));
            Console.WriteLine("Out of {0} password {1} was found ", userInfos.Count, result.Count);
            Console.WriteLine();
            Console.WriteLine("Time elapsed: {0}", stopwatch.Elapsed);
        }
    }
}
