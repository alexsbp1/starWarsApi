using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using starWarsApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;

namespace starWarsApi.Controllers
{
    public class CharacterController : Controller
    {
		public ActionResult CharacterListJQuery()
		{
			return View(); 
		}

		//If CharacterList gets cached, 'Next' and 'Previous' links doesnt work
		public ActionResult CharacterList(string link)
		{
			Models.Application<CharactersModel> objJson;
			var webClient = new WebClient();
			var json = ""; 

			if (link == null)
				json = webClient.DownloadString(@"https://swapi.dev/api/people/");
			else
				json = webClient.DownloadString(link);

			objJson = JsonConvert.DeserializeObject<Models.Application<CharactersModel>>(json);
			return View(objJson); 
		}

		[OutputCache(Duration = 600, VaryByParam = "link")]
		public ActionResult SelectedCharacter(string link)
		{
			var webClient = new WebClient();
			var json = webClient.DownloadString(link);
			Models.CharactersModel objJson = JsonConvert.DeserializeObject<Models.CharactersModel>(json);

			return View(objJson);
		}

		public ActionResult CharacterSearch(string searchName)
		{
			int characterPages = 9;
			List<Tuple<string, string>> searchList = new List<Tuple<string, string>>(); 
			var webClient = new WebClient();

			if (!searchName.IsNullOrWhiteSpace())
			{
				for (int i = 1; i <= characterPages; i++)
				{
					var json = webClient.DownloadString(@"https://swapi.dev/api/people/?page=" + i.ToString());
					Models.Application<CharactersModel> objJson = JsonConvert.DeserializeObject<Models.Application<CharactersModel>>(json);

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

		public JsonResult CharacterListJson(string link)
		{
			//int characterPages = 9;

			Models.Application<CharactersModel> objJson = new Models.Application<CharactersModel>(); 
			var webClient = new WebClient();
			var json = "";

			//for (int i = 1; i <= characterPages; i++)
			//{
				if (link == null)
					json = webClient.DownloadString(@"https://swapi.dev/api/people/?page=");
				else
					json = webClient.DownloadString(link);

				objJson = JsonConvert.DeserializeObject<Models.Application<CharactersModel>>(json);
			//}
			
			 
			
			return Json(objJson, JsonRequestBehavior.AllowGet);
		}


	}
}