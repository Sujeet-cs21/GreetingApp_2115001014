using BusinessLayer.Interface;
using ModelLayer.Model;
using NLog;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;

namespace BusinessLayer.Service
{
    public class GreetingBL : IGreetingBL
    {
        private readonly IGreetingRL _greetingRL;
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();

        public GreetingBL(IGreetingRL greetingRL)
        {
            _greetingRL = greetingRL;
        }
        public string GetGreeting(string firstName, string lastName)
        {
            logger.Info($"GetGreeting method called with parameters: FirstName={firstName}, LastName={lastName}");
            //Have firstName and lastName both
            if (!string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName))
            {
                logger.Info("Returning greeting message with both first name and last name");
                return $"Hello {firstName} {lastName}";
            }
            //Have firstName only
            else if (!string.IsNullOrEmpty(firstName))
            {
                logger.Info("Returning greeting message with first name only");
                return $"Hello {firstName}";
            }
            //Have last name only
            else if (!string.IsNullOrEmpty(lastName))
            {
                logger.Info("Returning greeting message with last name only");
                return $"Hello {lastName}";
            }
            //Have no names
            else
            {
                logger.Info("Returning default greeting message");
                return "Hello World";
            }
        }

        public GreetingEntity AddGreeting(GreetingModel greeting, int userId)
        {
            logger.Info("Business Layer - AddGreeting method started.");
            return _greetingRL.AddGreeting(greeting,userId);
        }

        public GreetingResponseModel FindGreetingById(FindByIdGreetingModel findByIdGreetingModel, int userId)
        {
            logger.Info("Business Layer - FindGreetingById method started.");
            var result = _greetingRL.FindGreetingById(findByIdGreetingModel,userId);
            if (result == null)
            {
                logger.Error("Business Layer - Greeting not found.");
                return null;
            }
            logger.Info("Business Layer - Greeting found successfully.");
            return new GreetingResponseModel { Id = result.Id, Greeting = result.Greeting };
        }

        public List<GreetingEntity> GetAllGreetings(int userId)
        {
            logger.Info("Business Layer - GetAllGreetings method started.");
            return _greetingRL.GetAllGreetings(userId);
        }

        public GreetingEntity EditGreeting(GreetingReqModel reqModel, int userId)
        {
            logger.Info("Business Layer - EditGreeting method started.");
            return _greetingRL.EditGreeting(reqModel,userId);
        }

        public GreetingEntity DeleteGreeting(FindByIdGreetingModel findByIdGreetingModel, int userId)
        {
            logger.Info("Business Layer - DeleteGreeting method started.");
            return _greetingRL.DeleteGreeting(findByIdGreetingModel,userId);
        }
    }
}
