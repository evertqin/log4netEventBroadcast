using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4netEventBroadcast;

namespace log4netDBLogging
{
    class TestListener
    {
        public static void ListenToEventBroadcast()
        {
            
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Inside the Mainclass");
            TestListener.ListenToEventBroadcast();

            LogGenerationSampleClass logGenerationSampleClass = new LogGenerationSampleClass();
            logGenerationSampleClass.GenerateSomeMeaninglessLog();
        }
    }
}
