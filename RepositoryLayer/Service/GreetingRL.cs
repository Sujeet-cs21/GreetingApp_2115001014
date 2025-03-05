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

        public GreetingEntity FindGreetingById(FindByIdGreetingModel findByIdGreetingModel)
        {
            try
            {
                logger.Info("Repository Layer - FindGreetingById method started.");
                GreetingEntity greetingEntity = _context.Greetings.Find(findByIdGreetingModel.Id);
                if (greetingEntity == null)
                {
                    logger.Error("Repository Layer - Greeting not found.");
                    throw new Exception();
                }
                logger.Info("Repository Layer - Greeting found successfully.");
                return greetingEntity;
            }
            catch (Exception e)
            {
                logger.Error(e, "Repository Layer - Error occurred in FindGreetingById method.");
                throw new Exception();
            }
        }

        public List<GreetingEntity> GetAllGreetings()
        {
            try
            {
                logger.Info("Repository Layer - GetAllGreetings method started.");
                var greetings = _context.Greetings.ToList();
                if (greetings.Count == 0)
                {
                    logger.Error("Repository Layer - No greetings found.");
                    throw new Exception();
                }
                logger.Info("Repository Layer - Greetings found successfully.");
                return greetings;
            }
            catch (Exception e)
            {
                logger.Error(e, "Repository Layer - Error occurred in GetAllGreetings method.");
                throw new Exception();
            }
        }

        public GreetingEntity EditGreeting(GreetingReqModel reqModel)
        {
            try
            {
                logger.Info("Repository Layer - EditGreeting method started.");
                GreetingEntity greetingEntity = _context.Greetings.Find(reqModel.Id);
                if (greetingEntity == null)
                {
                    logger.Error("Repository Layer - Greeting not found.");
                    throw new Exception();
                }
                greetingEntity.Greeting = reqModel.GreetMessage;
                _context.SaveChanges();
                logger.Info("Repository Layer - Greeting editeded successfully.");
                return greetingEntity;
            }
            catch (Exception e)
            {
                logger.Error(e, "Repository Layer - Error occurred in EditGreeting method.");
                throw new Exception();
            }
        }

        public GreetingEntity DeleteGreeting(FindByIdGreetingModel findByIdGreetingModel)
        {
            try
            {
                logger.Info("Repository Layer - DeleteGreeting method started.");
                GreetingEntity greetingEntity = _context.Greetings.Find(findByIdGreetingModel.Id);
                if (greetingEntity == null)
                {
                    logger.Error("Repository Layer - Greeting not found.");
                    throw new Exception();
                }
                _context.Greetings.Remove(greetingEntity);
                _context.SaveChanges();
                logger.Info("Repository Layer - Greeting deleted successfully.");
                return greetingEntity;
            }
            catch (Exception e)
            {
                logger.Error(e, "Repository Layer - Error occurred in DeleteGreeting method.");
                throw new Exception();
            }
        }
    }
}
