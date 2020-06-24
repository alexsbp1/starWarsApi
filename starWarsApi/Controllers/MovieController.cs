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
    public class MovieController : Controller
    {
		[OutputCache(Duration = 600, VaryByParam = "link")]
		public ActionResult MovieList(string link)
		{
			var webClient = new WebClient();
			var json = webClient.DownloadString(@"https://swapi.dev/api/films/");
			Models.Application<MoviesModel> objJson = JsonConvert.DeserializeObject<Models.Application<MoviesModel>>(json);

			return View(objJson);
		}


		[OutputCache(Duration = 600, VaryByParam = "link")]
		public ActionResult SelectedMovie(string link)
		{
			var webClient = new WebClient();
			var json = webClient.DownloadString(link);
			Models.MoviesModel objJson = JsonConvert.DeserializeObject<Models.MoviesModel>(json);

			return View(objJson);
		}


		public ActionResult MovieSearch(string searchName)
		{
			List<Tuple<string, string>> searchList = new List<Tuple<string, string>>();
			var webClient = new WebClient();

			if (!searchName.IsNullOrWhiteSpace())
			{

				var json = webClient.DownloadString(@"https://swapi.dev/api/films/");
				Models.Application<MoviesModel> objJson = JsonConvert.DeserializeObject<Models.Application<MoviesModel>>(json);

				foreach (var item in objJson.results)
				{
					string name = item.title;
					if (name.ToLower().Contains(searchName.ToLower()))
					{
						searchList.Add(new Tuple<string, string>(name, item.url.ToString()));
					}
				}
				
			}
			else
				return Redirect(Request.UrlReferrer.ToString());

			return View(searchList);
		}
	}
}