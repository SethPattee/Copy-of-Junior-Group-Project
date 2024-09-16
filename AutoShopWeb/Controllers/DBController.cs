using AutoShopAppLibrary.Data;
using AutoShopAppLibrary.Services;

using AutoShopWeb.Services;

using Microsoft.AspNetCore.Mvc;


namespace AutoShopWeb.Controllers;

[Microsoft.AspNetCore.Mvc.Route("[controller]")]
[ApiController]
public class DBController : Controller
{
    private readonly IntakeService _intakeService;
    private readonly DBService _dbService;

    public DBController(IntakeService intake, DBService db)
    {
        _intakeService = intake;
        _dbService = db;
    }

    [HttpPost]
    public async Task<IActionResult> IntakeCustomer([FromBody] EmailDTO form)
    {
        try
        {
            //await _dbService.StoreCustomerDetailsInDatabase(form);
            await _intakeService.SendEmail(form);
        }
        catch (Exception ex)
        {
            switch (ex.Message)
            {
                case "odometer":
                    {
                        return StatusCode(StatusCodes.Status418ImATeapot, "You provided an invalid odometer value, shame on you!");
                    }
                default:
                    {
                        return StatusCode(StatusCodes.Status500InternalServerError);
                    }
            }
        }

        return StatusCode(StatusCodes.Status200OK);
    }

    [HttpPost("/FormDetails")]
    public async Task<IActionResult> PostFormDetailsToDB([FromBody] EmailDTO formDetails)
    {
        //call the web db service to write to the database
        try
        {
            await _dbService.StoreCustomerDetailsInDatabase(formDetails);
            return StatusCode(StatusCodes.Status200OK);
        }
        catch (Exception ex)
        {
            //later, log the error

            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}