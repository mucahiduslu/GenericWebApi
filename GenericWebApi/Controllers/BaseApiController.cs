
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Http;

namespace GenericWebApi.Controllers
{
    public class BaseApiController : ApiController
    {

        protected object Invoke(params object[] parameters)
        {
            List<DataAccessLayer.ModelParam> mps = new List<DataAccessLayer.ModelParam>();
            string cacheKey = string.Empty;
            string proc = parameters[0].ToString();
            if (parameters[1] is String)
            {   // parse url parameters , GET
                string param = (string)parameters[1];
                mps = GetModelParamListFromURIQuery(param);
            }
            else
            {   // parse post parameters , POST
                mps = GetModelParamListFromJSONObject((JObject)parameters[1]);
            }

            try
            {
                string alias = WebConfigurationManager.AppSettings[proc];
                if (!String.IsNullOrEmpty(alias))
                    proc = proc.Replace(proc, alias);
            }
            catch
            {
            }

            return DataAccessLayer.DataProvider.Execute(proc, mps);
        }

        public List<DataAccessLayer.ModelParam> GetModelParamListFromURIQuery(string uriQuery)
        {
            if (uriQuery.Contains("&callback"))
            {
                string[] stringSeparators = new string[] { "&callback" };
                uriQuery = uriQuery.Split(stringSeparators, StringSplitOptions.None)[0];
            }
            List<DataAccessLayer.ModelParam> mps = new List<DataAccessLayer.ModelParam>();
            if (!String.IsNullOrEmpty(uriQuery))
            {
                if (uriQuery.ElementAt(0) == '?')
                {
                    string[] ps = uriQuery.Split('?')[1].Split('&');
                    foreach (string p in ps)
                    {
                        mps.Add(
                                new DataAccessLayer.ModelParam
                                {
                                    Name = p.Split('=')[0],
                                    Value = p.Split('=')[1] == string.Empty ? null : p.Split('=')[1]
                                }
                            );
                    }
                }
            }
            return mps;
        }

        public List<DataAccessLayer.ModelParam> GetModelParamListFromJSONObject(JObject jso)
        {
            List<DataAccessLayer.ModelParam> mps = new List<DataAccessLayer.ModelParam>(); ;
            foreach (var jo in jso)
            {
                mps.Add(
                        new DataAccessLayer.ModelParam
                        {
                            Name = jo.Key,
                            Value = jo.Value.ToString() == "" ? null : jo.Value.ToString()
                        }
                    );
            }
            return mps;
        }

        public List<DataAccessLayer.ModelParam> GetModelParamListFromJSONObjectOrderByName(JObject jso)
        {
            List<DataAccessLayer.ModelParam> mps = new List<DataAccessLayer.ModelParam>(); ;
            foreach (var jo in jso)
            {
                mps.Add(
                        new DataAccessLayer.ModelParam
                        {
                            Name = jo.Key,
                            Value = jo.Value.ToString() == "" ? null : jo.Value.ToString()
                        }
                    );
            }
            return mps.OrderBy(o => o.Name).ToList();
        }

    }
}