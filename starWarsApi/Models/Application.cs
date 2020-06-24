using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace starWarsApi.Models
{
	public class Application<T>
	{
            public int count { get; set; }
            public string next { get; set; }
            public string previous { get; set; }
            public IList<T> results { get; set; }
    }
}