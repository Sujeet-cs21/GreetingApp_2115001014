using BusinessLayer.Interface;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Model;
using NLog;
using RepositoryLayer.Entity;

namespace HelloGreetingApplication.Controllers;

/// <summary>
/// This Class is used to create API Endpoints for Greeting App
/// </summary>
[ApiController]
[Route("[controller]")]
public class HelloGreetingController : ControllerBase
{
    private readonly IGreetingBL _greetingBL;
    private static readonly Logger logger = LogManager.GetCurrentClassLogger();

    public HelloGreetingController(IGreetingBL greetingBL)
    {
        _greetingBL = greetingBL;
    }

    /// <summary>
    /// Get Method to get the Greeting Message
    /// </summary>
    /// <returns>"Greeting from GreetingBL"</returns>
    [HttpGet]
    public IActionResult Get()
    {
        logger.Info("GET request received at /HelloGreeting");

        try
        {
            ResponseModel<string> responseModel = new ResponseModel<string>
            {
                Success = true,
                Message = "Hello from Greeting App API Endpoint",
                Data = _greetingBL.GetGreeting("", "")
            };

            logger.Info("GET request processed successfully");
            return Ok(responseModel);
        }
        catch (Exception ex)
        {
            logger.Error(ex, "Error occurred in GET method");
            return StatusCode(500, "Internal Server Error");
        }
    }

    /// <summary>
    /// Post Method to Post the Greeting Message
    /// </summary>
    /// <param name="requestModel">RequestModel</param>
    /// <returns>"Greeting from GreetingBL"</returns>
    [HttpPost]
    public IActionResult Post([FromBody] RequestModel requestModel)
    {
        logger.Info($"POST request received with FirstName={requestModel.FirstName}, LastName={requestModel.LastName}");

        try
        {
            ResponseModel<string> responseModel = new ResponseModel<string>
            {
                Success = true,
                Message = "Request received successfully",
                Data = _greetingBL.GetGreeting(requestModel.FirstName, requestModel.LastName)
            };

            logger.Info("POST request processed successfully");
            return Ok(responseModel);
        }
        catch (Exception ex)
        {
            logger.Error(ex, "Error occurred in POST method");
            return StatusCode(500, "Internal Server Error");
        }
    }

    /// <summary>
    /// Post Method to Add Greeting
    /// </summary>
    /// <param name="greetingModel"></param>
    /// <returns>"Greeting Added"</returns>
    [HttpPost("Add")]
    public IActionResult AddGreeting([FromBody] GreetingModel greetingModel)
    {
        try
        {
            logger.Info("POST request received at /HelloGreeting/Add");
            if(greetingModel == null)
            {
                logger.Error("GreetingModel is null");
                return BadRequest("GreetingModel is null");
            }
            var greeting = _greetingBL.AddGreeting(greetingModel);
            var response = new ResponseModel<GreetingEntity>
            {
                Success = true,
                Message = "Greeting added successfully",
                Data = greeting
            };
            logger.Info("POST request processed successfully");
            return Created("Greeting Added", response);
        }
        catch (Exception e)
        {
            logger.Error(e, "Error occurred in POST method");
            return StatusCode(500, "Internal Server Error");
        }
    }

    /// <summary>
    /// Get Method to Find Greeting by Id
    /// </summary>
    /// <param name="findById"></param>
    /// <returns>"GreetingResposeModel"</returns>
    [HttpPost("FindById")]
    public IActionResult FindGeetingById([FromBody]FindByIdGreetingModel findById)
    {
        try
        {
            logger.Info("GET request received at /HelloGreeting/FindById");
            var greeting = _greetingBL.FindGreetingById(findById);
            if (greeting == null)
            {
                logger.Error("Greeting not found");
                return NotFound("Greeting not found");
            }
            var response = new ResponseModel<GreetingResponseModel>
            {
                Success = true,
                Message = "Greeting found successfully",
                Data = greeting
            };
            logger.Info("GET request processed successfully");
            return Ok(response);
        }
        catch (Exception e)
        {
            logger.Error(e, "Error occurred in GET method");
            return StatusCode(500, "Internal Server Error");
        }
    }

    /// <summary>
    /// Get Method to Get All Greetings
    /// </summary>
    /// <returns>"All Greetings"</returns>
    [HttpGet("GetAll")]
    public IActionResult GetAllGreeting()
    {
        try
        {
            logger.Info("GET request received at /HelloGreeting/GetAll");
            var greetings = _greetingBL.GetAllGreetings();
            if (greetings == null)
            {
                logger.Error("Greetings not found");
                return NotFound("Greetings not found");
            }
            var response = new ResponseModel<List<GreetingEntity>>
            {
                Success = true,
                Message = "Greetings found successfully",
                Data = greetings
            };
            logger.Info("GET request processed successfully");
            return Ok(response);
        }
        catch (Exception e)
        {
            logger.Error(e, "Error occurred in GET method");
            return StatusCode(500, "Internal Server Error");
        }
    }

    /// <summary>
    /// Put Method to Update the Greeting Message
    /// </summary>
    /// <param name="requestModel">RequestModel</param>
    /// <returns>"Response Model"</returns>
    [HttpPut]
    public IActionResult Put([FromBody] RequestModel requestModel)
    {
        logger.Info($"PUT request received with FirstName={requestModel.FirstName}, LastName={requestModel.LastName}");

        try
        {
            ResponseModel<string> responseModel = new ResponseModel<string>
            {
                Success = true,
                Message = "Updated successfully",
                Data = $"Name: {requestModel.FirstName} {requestModel.LastName}"
            };

            logger.Info("PUT request processed successfully");
            return Ok(responseModel);
        }
        catch (Exception ex)
        {
            logger.Error(ex, "Error occurred in PUT method");
            return StatusCode(500, "Internal Server Error");
        }
    }

    /// <summary>
    /// Patch Method to Update the Greeting Message
    /// </summary>
    /// <param name="requestModel">RequestModel</param>
    /// <returns>"Response Model"</returns>
    [HttpPatch]
    public IActionResult Patch([FromBody] RequestModel requestModel)
    {
        logger.Info($"PATCH request received with FirstName={requestModel.FirstName}, LastName={requestModel.LastName}");

        try
        {
            var updates = new
            {
                firstName = string.IsNullOrEmpty(requestModel.FirstName) ? "Not Updated" : requestModel.FirstName,
                lastName = string.IsNullOrEmpty(requestModel.LastName) ? "Not Updated" : requestModel.LastName
            };

            ResponseModel<string> responseModel = new ResponseModel<string>
            {
                Success = true,
                Message = "Updated partially successfully",
                Data = updates.ToString()
            };

            logger.Info("PATCH request processed successfully");
            return Ok(responseModel);
        }
        catch (Exception ex)
        {
            logger.Error(ex, "Error occurred in PATCH method");
            return StatusCode(500, "Internal Server Error");
        }
    }

    [HttpPatch("Edit")]
    public IActionResult EditGreeting([FromBody] GreetingReqModel reqModel)
    {
        try
        {
            logger.Info("PATCH request received at /HelloGreeting/Edit");
            if (reqModel == null)
            {
                logger.Error("GreetingReqModel is null");
                return BadRequest("GreetingReqModel is null");
            }
            var greeting = _greetingBL.EditGreeting(reqModel);
            var response = new ResponseModel<GreetingEntity>
            {
                Success = true,
                Message = "Greeting edited successfully",
                Data = greeting
            };
            logger.Info("PATCH request processed successfully");
            return Ok(response);
        }
        catch (Exception e)
        {
            logger.Error(e, "Error occurred in PATCH method");
            return StatusCode(500, "Internal Server Error");
        }
    }

    /// <summary>
    /// Delete Method to Delete the Greeting Message
    /// </summary>
    /// <returns>"Deleted Successfully"</returns>
    [HttpDelete]
    public IActionResult Delete()
    {
        logger.Info("DELETE request received at /HelloGreeting");

        try
        {
            ResponseModel<string> responseModel = new ResponseModel<string>
            {
                Success = true,
                Message = "Deleted successfully",
                Data = ""
            };

            logger.Info("DELETE request processed successfully");
            return Ok(responseModel);
        }
        catch (Exception ex)
        {
            logger.Error(ex, "Error occurred in DELETE method");
            return StatusCode(500, "Internal Server Error");
        }
    }
}
