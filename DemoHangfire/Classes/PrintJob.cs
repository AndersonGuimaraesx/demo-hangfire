using DemoHangfire.Interfaces;
using System;

namespace DemoHangfire.Classes
{
    public class PrintJob : IPrintJob
    {
        public void Print()
        {
            Console.WriteLine($"Hanfire recurring job!");
        }
    }
}
