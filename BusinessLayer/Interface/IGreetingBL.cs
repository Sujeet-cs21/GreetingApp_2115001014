using ModelLayer.Model;
using RepositoryLayer.Entity;

namespace BusinessLayer.Interface
{
    public interface IGreetingBL
    {
        string GetGreeting(string firstname,string lastName);
        GreetingEntity AddGreeting(GreetingModel greeting,int userId);
        GreetingResponseModel FindGreetingById(FindByIdGreetingModel findByIdGreetingModel,int userId);
        List<GreetingEntity> GetAllGreetings(int userId);
        GreetingEntity EditGreeting(GreetingReqModel reqModel, int userId);
        GreetingEntity DeleteGreeting(FindByIdGreetingModel findByIdGreetingModel, int userId);
    }
}
