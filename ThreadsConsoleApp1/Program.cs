using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThreadsConsoleApp1.Services;

namespace ThreadsConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            ThreadsService threadsService = new ThreadsService();
            Console.ReadKey();
        } 
    }
}
