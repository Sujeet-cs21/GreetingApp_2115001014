using ModelLayer.Model;
using RepositoryLayer.Entity;

namespace RepositoryLayer.Interface
{
    public interface IGreetingRL
    {
        GreetingEntity AddGreeting(GreetingModel greeting, int userId);
        GreetingEntity FindGreetingById(FindByIdGreetingModel findByIdGreetingModel, int userId);
        List<GreetingEntity> GetAllGreetings(int userId);
        GreetingEntity EditGreeting(GreetingReqModel reqModel,int userId );
        GreetingEntity DeleteGreeting(FindByIdGreetingModel findByIdGreetingModel, int userId);
    }
}
