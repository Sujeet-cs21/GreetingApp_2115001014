using BusinessLayer.Interface;
using NLog;

namespace BusinessLayer.Service
{
    public class GreetingBL : IGreetingBL
    {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();
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
    }
}
