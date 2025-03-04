using ModelLayer.Model;
using NLog;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;

namespace RepositoryLayer.Service
{
    public class GreetingRL : IGreetingRL
    {
        private readonly GreetingContext _context;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        public GreetingRL(GreetingContext context)
        {
            _context = context;
        }

        public GreetingEntity AddGreeting(GreetingModel greeting)
        {
            try
            {
                logger.Info("Repository Layer - AddGreeting method started.");
                GreetingEntity greetingEntity = new GreetingEntity()
                { Greeting = greeting.GreetMessage };

                _context.Greetings.Add(greetingEntity);
                _context.SaveChanges();

                logger.Info("Repository Layer - Greeting added successfully.");
                return greetingEntity;
            }
            catch(Exception e)
            {
                logger.Error(e, "Repository Layer - Error occurred in AddGreeting method.");
                throw new Exception();
            }
        }
    }
}
