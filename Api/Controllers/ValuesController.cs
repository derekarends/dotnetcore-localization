using System.Collections.Generic;
using Core.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ValuesController : ControllerBase
  {
    private readonly IStringLocalizer<SharedResources> _localizer;

    public ValuesController(IStringLocalizer<SharedResources> localizer)
    {
      _localizer = localizer;
    }
    
    // GET api/values
    [HttpGet]
    public ActionResult<IEnumerable<string>> Get()
    {
      return new string[] {_localizer["value1"], _localizer["value2"]};
    }
  }
}