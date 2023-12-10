using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StarlinkDC.Data;
using IG.ENMS.Models.Starlink.In.APIResponses;
using static System.Net.Mime.MediaTypeNames;

namespace StarlinkDC.Controllers;

[ApiController]
[Route("[controller]")]
public class SystemStatusController : ControllerBase
{
    private readonly ILogger<SystemStatusController> _logger;
    private readonly StarlinkData _starlinkData;

    public SystemStatusController(ILogger<SystemStatusController> logger, StarlinkData starlinkData)
    {
        _logger = logger;
        _starlinkData = starlinkData;
    }

    [HttpGet(Name = "GetAccountHierarchy")]
	public string Get()
    {
		Console.WriteLine("In Get");
        string retVal = JsonConvert.SerializeObject(_starlinkData);

        return retVal;
    }
}