namespace SignalRTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            HubClient hc = new HubClient("http://localhost:5293/ChatHub");
            Console.WriteLine("press anykey to join chat");
            Console.ReadKey();
            Console.WriteLine();
            hc.Connect();
            Console.ReadKey();
        }
    }
}
