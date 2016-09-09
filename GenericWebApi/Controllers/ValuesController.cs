using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GenericWebApi.Controllers
{

    public class ValuesController : BaseApiController
    {
        [HttpGet]
        [HttpOptions]
        public object Get(string proc)
        {
            object response;
            try
            {
                response = base.Invoke(proc, Request.RequestUri.Query);
            }
            catch (Exception e)
            {
                response = e.Message;
            }
            return response;
        }


        [HttpPost]
        [HttpOptions]
        public object Post(string proc, [FromBody]object value)
        {
            object response;
            try
            {
                  response = base.Invoke(proc, value);
            }
            catch (Exception e)
            {
                return e.Message;
            }
            return response;
        }

    }
}
