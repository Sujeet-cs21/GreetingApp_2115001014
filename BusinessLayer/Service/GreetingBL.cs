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
    }
}
