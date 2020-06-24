using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using starWarsApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace starWarsApi.Controllers
{
    public class PlanetController : Controller
    {
		//If PlanetList gets cached, 'Next' and 'Previous' links doesnt work
		public ActionResult PlanetList(string link)
		{
			Models.Application<PlanetsModel> objJson;
			var webClient = new WebClient();
			var json = "";

			if (link == null)
				json = webClient.DownloadString(@"https://swapi.dev/api/planets/");
			else
				json = webClient.DownloadString(link);

			objJson = JsonConvert.DeserializeObject<Models.Application<PlanetsModel>>(json);
			return View(objJson);

		}

		[OutputCache(Duration = 600, VaryByParam = "link")]
		public ActionResult SelectedPlanet(string link)
		{
			var webClient = new WebClient();
			var json = webClient.DownloadString(link);
			Models.PlanetsModel objJson = JsonConvert.DeserializeObject<Models.PlanetsModel>(json); //PlanetsModel

			return View(objJson);
		}

		public ActionResult PlanetSearch(string searchName)
		{
			int planetPages = 6;
			List<Tuple<string, string>> searchList = new List<Tuple<string, string>>();
			var webClient = new WebClient();

			if (!searchName.IsNullOrWhiteSpace())
			{
				for (int i = 1; i <= planetPages; i++)
				{
					var json = webClient.DownloadString(@"https://swapi.dev/api/planets/?page=" + i.ToString());
					Models.Application<PlanetsModel> objJson = JsonConvert.DeserializeObject<Models.Application<PlanetsModel>>(json);

					foreach (var item in objJson.results)
					{
						string name = item.name;
						if (name.ToLower().Contains(searchName.ToLower()))
						{
							searchList.Add(new Tuple<string, string>(name, item.url.ToString()));
						}
					}
				}
			}
			else
				return Redirect(Request.UrlReferrer.ToString());


			return View(searchList);
		}
	}
}