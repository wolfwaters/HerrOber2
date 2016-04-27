using System;
using System.IO;
using System.Reflection;
using HerrOber2.Models;
using Microsoft.Owin.Hosting;

namespace HerrOber2
{
    public class Program
    {
        static void Main(string[] args)
        {
            DataModel.Instance.Load();

            string baseAddress = (args == null || args.Length == 0) ? "http://localhost:4711" : args[0];

            Console.WriteLine("Herr Ober is starting ...");
            WebApp.Start<Startup>(baseAddress);
            Console.WriteLine("Herr Ober is listening at {0} ... ", baseAddress);

            Console.ReadLine();
        }
    }
}
