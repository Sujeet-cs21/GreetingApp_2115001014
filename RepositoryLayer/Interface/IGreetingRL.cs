using ModelLayer.Model;
using RepositoryLayer.Entity;

namespace RepositoryLayer.Interface
{
    public interface IGreetingRL
    {
        GreetingEntity AddGreeting(GreetingModel greeting);
        GreetingEntity FindGreetingById(FindByIdGreetingModel findByIdGreetingModel);
        List<GreetingEntity> GetAllGreetings();
    }
}
