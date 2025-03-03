using Microsoft.AspNetCore.Mvc;
using ModelLayer.Model;

namespace HelloGreetingApplication.Controllers;

/// <summary>
/// This Class is used to create API Endpoints for Greeting App
/// </summary>
[ApiController]
[Route("[controller]")]
public class HelloGreetingController : ControllerBase
{
    /// <summary>
    /// Get Method to get the Greeting Message
    /// </summary>
    /// <returns>"Hello World!"</returns>
    [HttpGet]
    public IActionResult Get()
    {
        ResponseModel<string> responseModel = new ResponseModel<string>();

        responseModel.Success = true;
        responseModel.Message = "Hello from Greeting App API Endpoint";
        responseModel.Data = "Hello World";
        return Ok(responseModel);
    }

    /// <summary>
    /// Post Method to Post the Greeting Message
    /// </summary>
    /// <param name="requestModel">Request Model</param>
    /// <returns>Response Model</returns>
    [HttpPost]
    public IActionResult Post([FromBody]RequestModel requestModel)
    {
        ResponseModel<string> responseModel = new ResponseModel<string>();

        responseModel.Success = true;
        responseModel.Message = "Request recieved successfully";
        responseModel.Data = $"Key: {requestModel.Key}, Value: {requestModel.Value}";
        return Ok(responseModel);
    }

    /// <summary>
    /// Put Method to Update the Greeting Message
    /// </summary>
    /// <param name="requestModel">Request Model</param>
    /// <returns>Response Model</returns>
    [HttpPut]
    public IActionResult Put([FromBody]RequestModel requestModel)
    {
        ResponseModel<string> responseModel = new ResponseModel<string>();
        responseModel.Success = true;
        responseModel.Message = "key value updated successfully";
        responseModel.Data = $"Key: {requestModel.Key}, Value: {requestModel.Value}";
        return Ok(responseModel);
    }

    /// <summary>
    /// Patch Method to Update the Greeting Message
    /// </summary>
    /// <param name="requestModel">Request Model</param>
    /// <returns>Response Model</returns>
    [HttpPatch]
    public IActionResult Patch([FromBody]RequestModel requestModel)
    {
        ResponseModel<string> responseModel = new ResponseModel<string>();
        responseModel.Success = true;
        responseModel.Message = "key value updated partially successfully";
        responseModel.Data = $"Key: {requestModel.Key}, Value: {requestModel.Value}";
        return Ok(responseModel);
    }

    /// <summary>
    /// Delete Method to Delete the Greeting Message
    /// </summary>
    /// <returns>Response Model</returns>
    [HttpDelete]
    public IActionResult Delete()
    {
        ResponseModel<string> responseModel = new ResponseModel<string>();
        responseModel.Success = true;
        responseModel.Message = "key value deleted successfully";
        responseModel.Data = "Key: Value";
        return Ok(responseModel);
    }
}
