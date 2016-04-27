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

            string baseUri = "http://localhost:4711";
            //string baseUri = "http://herrober.rdeadmin.waters.com:4711";

            Console.WriteLine("Herr Ober is starting ...");
            WebApp.Start<Startup>(baseUri);
            Console.WriteLine("Herr Ober is listening at {0} ... ", baseUri);

            Console.ReadLine();
        }
    }
}
