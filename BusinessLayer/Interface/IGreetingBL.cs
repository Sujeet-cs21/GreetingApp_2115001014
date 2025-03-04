using ModelLayer.Model;
using RepositoryLayer.Entity;

namespace BusinessLayer.Interface
{
    public interface IGreetingBL
    {
        string GetGreeting(string firstname,string lastName);
        GreetingEntity AddGreeting(GreetingModel greeting);
    }
}
