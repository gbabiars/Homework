using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ServiceStack.Redis;

namespace Homework.Api
{
    public class RedisApiControllerBase : ApiController
    {
		private readonly string connectionString = ConfigurationManager.AppSettings["REDISTOGO_URL"];

		protected RedisClient GetRedisClient() {
			return new RedisClient(new Uri(connectionString));
		}
	}
}
