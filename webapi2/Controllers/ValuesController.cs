using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace webapi2.Controllers
{
    [Route("api/value1")]
    [ApiController]
    public class ValuesController : ControllerBase
    {

        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;
        public ValuesController(ILogger<ValuesController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }


        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            var Message = $"About page visited at {DateTime.UtcNow.ToLongTimeString()}";
            _logger.LogInformation("_configuration.", Message);
            return new string[] {
                "ExtraSettingNotInSettingsFile:"
                +_configuration.GetValue<string>("ExtraSettingNotInSettingsFile")
                +"CUSTOMCONNSTR_MONGO:"
                +_configuration.GetConnectionString("CUSTOMCONNSTR_MONGO")
                + "webAPI2: {class: classname, _objectId:webapi2, msg: we then have output from api2}" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
