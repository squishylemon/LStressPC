using System;
using System.Diagnostics;
using System.Threading;
using Microsoft.VisualBasic;

class Program
{
    static void Main()
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(@"
        
.____       _________ __                               ___________________  
|    |     /   _____//  |________   ____   ______ _____\______   \_   ___ \ 
|    |     \_____  \\   __\_  __ \_/ __ \ /  ___//  ___/|     ___/    \  \/ 
|    |___  /        \|  |  |  | \/\  ___/ \___ \ \___ \ |    |   \     \____
|_______ \/_______  /|__|  |__|    \___  >____  >____  >|____|    \______  /
        \/        \/                   \/     \/     \/                  \/ 

        ");

        // Display the available options to the user
        Console.WriteLine("Please select an option:");
        Console.WriteLine("1. Stress test RAM");
        Console.WriteLine("2. Stress test CPU");
        Console.WriteLine("3. Stress test internet");
        Console.WriteLine();

        // Get the user's choice
        Console.Write("Enter your choice (1-3): ");
        int choice = int.Parse(Console.ReadLine());

        // Get the duration of the stress test from the user
        Console.Write("Enter the duration of the stress test in seconds: ");
        int duration = int.Parse(Console.ReadLine());

        // Start the stress test based on the user's choice
        switch (choice)
        {
            case 1:
                Console.WriteLine("How much ram?");
                int ramamount = int.Parse(Console.ReadLine());

                Console.WriteLine("Stress testing RAM for {0} seconds...", duration);
                StressTestRAM(duration, ramamount);
                break;
            case 2:
                Console.WriteLine("Stress testing CPU for {0} seconds...", duration);
                StressTestCPU(duration);
                break;
            case 3:
                Console.WriteLine("Stress testing internet for {0} seconds...", duration);
                StressTestInternet(duration);
                break;
            default:
                Console.WriteLine("Invalid choice.");
                break;
        }

        // Wait for the user to press a key before exiting
        Console.WriteLine("Press any key to exit.");
        Console.ReadKey();
    }



    static void StressTestRAM(int duration, int rammount)
    {
        // Calculate the amount of memory to allocate (in bytes)
        long totalMemory = rammount * 1024; // convert to bytes
        long targetMemory = totalMemory;

        // Create an expandable memory stream
        var memoryStream = new MemoryStream();
        var buffer = new byte[4096]; // use a small buffer for writing to the stream

        // Start the stress test threads
        List<Thread> threads = new List<Thread>();
        while (memoryStream.Length < targetMemory)
        {
            Thread thread = new Thread(() =>
            {
                while (true)
                {
                    // Write to the memory stream to stress test the RAM
                    try
                    {
                        memoryStream.Write(buffer, 0, buffer.Length);
                        Console.WriteLine("");
                    }
                    catch (Exception)
                    {
                        // ignore exceptions from write operations
                    }
                }
            });
            thread.Start();
            threads.Add(thread);
        }

        // Wait for the stress test to complete
        Thread.Sleep(duration * 1000);

        // Stop the stress test threads
        foreach (Thread thread in threads)
        {
            thread.Abort();
        }

        // Dispose the memory stream
        memoryStream.Dispose();
    }






    static void StressTestCPU(int duration)
    {
        // Get the number of available CPU cores
        int numCores = Environment.ProcessorCount;

        // Start a stress test thread for each CPU core
        Thread[] threads = new Thread[numCores];
        for (int i = 0; i < numCores; i++)
        {
            threads[i] = new Thread(() =>
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                while (stopwatch.Elapsed.TotalSeconds < duration)
                {
                 
                    // Perform a busy loop to stress test the CPU
                    double x = Math.Sqrt(Math.PI);
                    for (int j = 0; j < 1000000; j++)
                    {
                        x += Math.Sqrt(Math.PI + x);
                    }
       
                }
                stopwatch.Stop();
            });
            threads[i].Start();
        }

        // Wait for all the stress test threads to complete
        foreach (Thread thread in threads)
        {
            thread.Join();
        }
    }

    static void StressTestInternet(int duration)
    {
        // Get the number of available CPU cores
        int numThreads = Environment.ProcessorCount;

        // Start a stress test thread for each CPU core
        Thread[] threads = new Thread[numThreads];
        for (int i = 0; i < numThreads; i++)
        {
            threads[i] = new Thread(() =>
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                while (stopwatch.Elapsed.TotalSeconds < duration)
                {
                    // Access a website to stress test the internet
                    using (var client = new System.Net.WebClient())
                    {
                        string result = client.DownloadString("http://speedtest.ftp.otenet.gr/files/test1Mb.db");
                        Console.WriteLine("downloading file");
                    }
                }
                stopwatch.Stop();
            });
            threads[i].Start();
        }

        // Wait for all the stress test threads to complete
        foreach (Thread thread in threads)
        {
            thread.Join();
        }
    }

}
