using BusinessLayer.Interface;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Model;
using NLog;

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
