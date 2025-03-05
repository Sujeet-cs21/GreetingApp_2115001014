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
            try
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
            catch (System.Exception e)
            {
                logger.Error(e, "An error occurred in GetGreeting method");
                throw;
            }
        }

        public GreetingEntity AddGreeting(GreetingModel greeting)
        {
            try
            {
                logger.Info("Business Layer - AddGreeting method started.");
                return _greetingRL.AddGreeting(greeting);
            }
            catch (System.Exception e)
            {
                logger.Error(e, "Business Layer - Error occurred in AddGreeting method.");
                throw;
            }
        }

        public GreetingResponseModel FindGreetingById(FindByIdGreetingModel findByIdGreetingModel)
        {
            try
            {
                logger.Info("Business Layer - FindGreetingById method started.");
                var result = _greetingRL.FindGreetingById(findByIdGreetingModel);

                logger.Info("Business Layer - Greeting found successfully.");
                return new GreetingResponseModel { Id = result.Id, Greeting = result.Greeting };
            }
            catch (System.Exception e)
            {
                logger.Error(e, "Business Layer - Error occurred in FindGreetingById method.");
                throw;
            }
        }

        public List<GreetingEntity> GetAllGreetings()
        {
            try
            {
                logger.Info("Business Layer - GetAllGreetings method started.");
                return _greetingRL.GetAllGreetings();
            }
            catch (System.Exception e)
            {
                logger.Error(e, "Business Layer - Error occurred in GetAllGreetings method.");
                throw;
            }
        }

        public GreetingEntity EditGreeting(GreetingReqModel reqModel)
        {
            try
            {
                logger.Info("Business Layer - EditGreeting method started.");
                return _greetingRL.EditGreeting(reqModel);
            }
            catch (System.Exception e)
            {
                logger.Error(e, "Business Layer - Error occurred in EditGreeting method.");
                throw;
            }
        }
    }
}
