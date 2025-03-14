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
            logger.Info("Repository Layer - AddGreeting method started.");

            var greetingEntity = new GreetingEntity { Greeting = greeting.GreetMessage };

            _context.Greetings.Add(greetingEntity);
            _context.SaveChanges();

            logger.Info("Repository Layer - Greeting added successfully.");
            return greetingEntity;
        }

        public GreetingEntity FindGreetingById(FindByIdGreetingModel findByIdGreetingModel)
        {
            logger.Info("Repository Layer - FindGreetingById method started.");
            var greetingEntity = _context.Greetings.Find(findByIdGreetingModel.Id)
                                 ?? throw new Exception("Greeting not found.");

            logger.Info("Repository Layer - Greeting found successfully.");
            return greetingEntity;
        }

        public List<GreetingEntity> GetAllGreetings()
        {
            logger.Info("Repository Layer - GetAllGreetings method started.");
            var greetings = _context.Greetings.ToList();

            if (!greetings.Any())
                throw new Exception("No greetings found.");

            logger.Info("Repository Layer - Greetings found successfully.");
            return greetings;
        }

        public GreetingEntity EditGreeting(GreetingReqModel reqModel)
        {
            logger.Info("Repository Layer - EditGreeting method started.");
            var greetingEntity = _context.Greetings.Find(reqModel.Id)
                                 ?? throw new Exception("Greeting not found.");

            greetingEntity.Greeting = reqModel.GreetMessage;
            _context.SaveChanges();

            logger.Info("Repository Layer - Greeting edited successfully.");
            return greetingEntity;
        }

        public GreetingEntity DeleteGreeting(FindByIdGreetingModel findByIdGreetingModel)
        {
            logger.Info("Repository Layer - DeleteGreeting method started.");
            var greetingEntity = _context.Greetings.Find(findByIdGreetingModel.Id)
                                 ?? throw new Exception("Greeting not found.");

            _context.Greetings.Remove(greetingEntity);
            _context.SaveChanges();

            logger.Info("Repository Layer - Greeting deleted successfully.");
            return greetingEntity;
        }
    }
}
