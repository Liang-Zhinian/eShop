using System;
using Eva.BuildingBlocks.RESTApiResponseWrapper.Wrappers;
using Microsoft.AspNetCore.Mvc;
using Sequence.Services;

namespace Demo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private IKeyAllocService _keyAllocService;

        public ValuesController(IKeyAllocService keyAllocService)
        {
            _keyAllocService = keyAllocService;
        }

        // GET api/values
        [HttpGet]
        public ApiResponse Get()
        {
            return new ApiResponse(ResponseMessageEnum.Success.ToString(), new string[] { "value1", "value2" });
        }

        // GET api/values
        [HttpGet]
        [Route("Key")]
        public ActionResult<string> Key()
        {
            var seqName = DateTime.Now.ToString("yyyy-MM-dd");
            var key = _keyAllocService.GetKey(seqName).Result;
            return key.ToString("D4");
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
