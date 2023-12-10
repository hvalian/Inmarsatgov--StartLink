using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using IG.ENMS.Starlink.StateMachines;
using IG.ENMS.Starlink.Data;
using WebAPIModels = IG.ENMS.Models.Starlink.Out.WebAPI;
using StarlinkModels = IG.ENMS.Starlink.Models;
using static System.Net.Mime.MediaTypeNames;

//IG.ENMS.NMS.Starlink.Controllers
namespace IG.ENMS.Starlink.Controllers;

[ApiController]
[Route("[controller]")]
public class UserTerminalsController : ControllerBase
{
    private readonly ILogger<UserTerminalsController> _logger;
    private readonly StarlinkSM _starlinkSM;

    public UserTerminalsController(ILogger<UserTerminalsController> logger, StarlinkSM starlinkSM)
    {
        _logger = logger;
        _starlinkSM = starlinkSM;
    }

    [HttpGet("{UserTerminalId?}")]
    public IActionResult Get(string? UserTerminalId = null)
    {
        _logger.LogInformation("In Get Accounts");

        UserTerminals userTerminals = _starlinkSM.UserTerminals;
        List<WebAPIModels.UserTerminalModel> accountList = new List<WebAPIModels.UserTerminalModel>();

        if (userTerminals is null)
            return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving list of User Terminals.");

        if (UserTerminalId is not null) {
			StarlinkModels.UserTerminal userTerminal = userTerminals.Get(UserTerminalId);
            if (userTerminal is not null) {
                accountList.Add(new WebAPIModels.UserTerminalModel() { UserTerminalId = userTerminal.UserTerminalId, KitSerialNumber = userTerminal.KitSerialNumber, DishSerialNumber = userTerminal.DishSerialNumber, ServiceLineNumber = userTerminal.ServiceLineNumber, Active = userTerminal.Active });
            } else {
                return NotFound();
            }
        } else {
            foreach (StarlinkModels.UserTerminal userTerminal in userTerminals) {
                accountList.Add(new WebAPIModels.UserTerminalModel() { UserTerminalId = userTerminal.UserTerminalId, KitSerialNumber = userTerminal.KitSerialNumber, DishSerialNumber = userTerminal.DishSerialNumber, ServiceLineNumber = userTerminal.ServiceLineNumber, Active = userTerminal.Active });
            }
        }

        string retJson = JsonConvert.SerializeObject(accountList);

        string retVal = @"{ ""data"": " + retJson + " }";

        return Ok(retVal);
    }

   // [HttpGet("{AccountNumber}/userTerminals/{TerminalId?}")]
   // public IActionResult GetAccountTerminals(string AccountNumber, string? TerminalId)
   // {
   //     _logger.LogInformation("In Get Account Terminals");

   //     List<NMSModels.UserTerminalModel> userTerminals = _starlinkData.TerminalList(AccountNumber);
   //     List<WebAPIModels.UserTerminalModel> terminalList = new List<WebAPIModels.UserTerminalModel>();

   //     if (userTerminals is null)
   //         return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving list of User Terminals.");

   //     if (TerminalId is not null) {
   //         NMSModels.UserTerminalModel userTerminal = userTerminals.Find(s => s.UserTerminalId == TerminalId);
   //         if (userTerminal is not null) {
   //             terminalList.Add(new WebAPIModels.UserTerminalModel() { UserTerminalId = userTerminal.UserTerminalId, Active = userTerminal.Active, DishSerialNumber = userTerminal.DishSerialNumber, KitSerialNumber = userTerminal.KitSerialNumber, ServiceLineNumber = userTerminal.ServiceLineNumber });
   //         } else {
   //             return NotFound();
   //         }
   //     } else {
   //         foreach (NMSModels.UserTerminalModel userTerminal in userTerminals) {
   //             terminalList.Add(new WebAPIModels.UserTerminalModel() { UserTerminalId = userTerminal.UserTerminalId, Active = userTerminal.Active, DishSerialNumber = userTerminal.DishSerialNumber, KitSerialNumber = userTerminal.KitSerialNumber, ServiceLineNumber = userTerminal.ServiceLineNumber });
			//}
   //     }

   //     string retVal = JsonConvert.SerializeObject(terminalList);

   //     return Ok(retVal);
   // }
}