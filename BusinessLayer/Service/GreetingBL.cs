using BusinessLayer.Interface;

namespace BusinessLayer.Service
{
    public class GreetingBL : IGreetingBL
    {
        public string GetGreeting()
        {
            return "Hello! from GreetingBL";
        }
    }
}
