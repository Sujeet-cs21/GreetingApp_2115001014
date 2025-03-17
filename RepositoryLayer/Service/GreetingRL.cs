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

        public GreetingEntity AddGreeting(GreetingModel greeting, int userId)
        {
            logger.Info($"Repository Layer - AddGreeting method started for UserId={userId}");

            var greetingEntity = new GreetingEntity
            {
                Greeting = greeting.GreetMessage,
                UserId = userId
            };

            _context.Greetings.Add(greetingEntity);
            _context.SaveChanges();

            logger.Info($"Repository Layer - Greeting added successfully for UserId={userId}");
            return greetingEntity;
        }

        public GreetingEntity FindGreetingById(FindByIdGreetingModel findByIdGreetingModel, int userId)
        {
            logger.Info($"Repository Layer - FindGreetingById method started for UserId={userId}");

            var greetingEntity = _context.Greetings
                                         .FirstOrDefault(g => g.Id == findByIdGreetingModel.Id && g.UserId == userId);

            if (greetingEntity == null)
            {
                logger.Warn($"Greeting not found for Id={findByIdGreetingModel.Id} and UserId={userId}");
                throw new UnauthorizedAccessException("You are not authorized to access this greeting.");
            }

            logger.Info("Repository Layer - Greeting found successfully.");
            return greetingEntity;
        }

        public List<GreetingEntity> GetAllGreetings(int userId)
        {
            logger.Info($"Repository Layer - GetAllGreetings method started for UserId={userId}");

            var greetings = _context.Greetings.Where(g => g.UserId == userId).ToList();

            if (!greetings.Any())
            {
                logger.Warn($"No greetings found for UserId={userId}");
                throw new Exception("No greetings found.");
            }

            logger.Info("Repository Layer - Greetings found successfully.");
            return greetings;
        }

        public GreetingEntity EditGreeting(GreetingReqModel reqModel, int userId)
        {
            logger.Info($"Repository Layer - EditGreeting method started for UserId={userId}");

            var greetingEntity = _context.Greetings.FirstOrDefault(g => g.Id == reqModel.Id && g.UserId == userId);

            if (greetingEntity == null)
            {
                logger.Warn($"Greeting not found or unauthorized access for Id={reqModel.Id} and UserId={userId}");
                throw new UnauthorizedAccessException("You are not authorized to edit this greeting.");
            }

            greetingEntity.Greeting = reqModel.GreetMessage;
            _context.SaveChanges();

            logger.Info("Repository Layer - Greeting edited successfully.");
            return greetingEntity;
        }

        public GreetingEntity DeleteGreeting(FindByIdGreetingModel findByIdGreetingModel, int userId)
        {
            logger.Info($"Repository Layer - DeleteGreeting method started for UserId={userId}");

            var greetingEntity = _context.Greetings.FirstOrDefault(g => g.Id == findByIdGreetingModel.Id && g.UserId == userId);

            if (greetingEntity == null)
            {
                logger.Warn($"Greeting not found or unauthorized access for Id={findByIdGreetingModel.Id} and UserId={userId}");
                throw new UnauthorizedAccessException("You are not authorized to delete this greeting.");
            }

            _context.Greetings.Remove(greetingEntity);
            _context.SaveChanges();

            logger.Info("Repository Layer - Greeting deleted successfully.");
            return greetingEntity;
        }
    }
}
