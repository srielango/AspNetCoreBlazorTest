﻿using AspNetCoreBlazorTest.Configuration;
using AspNetCoreBlazorTest.Models;
using Blazorise;
using Blazorise.DataGrid;
using Blazorise.LoadingIndicator;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.Net.Http.Json;

namespace AspNetCoreBlazorTest.Pages
{
	public partial class ListAdmin
	{
		[Inject] IJSRuntime JSRuntime { get; set; }
		[Inject] ClientAppSettings ClientAppSettings { get; set; }

		LoadingIndicator loadingIndicator;

		private ObservableCollection<Class1> _admins;
		private bool _showConfirmation = false;
		private int _selectedRowId = 0;

		private DataGridRowStyling _headerStyle = new DataGridRowStyling() { Class = "text-center roboto-medium" };
		private DataGridRowStyling _rowStyle = new DataGridRowStyling() { Class = "text-center" };

        protected override async Task OnInitializedAsync()
		{
			_admins = await GetAdmins("https://jsonplaceholder.typicode.com/users");
			await base.OnInitializedAsync();
		}

		private readonly JsonSerializerOptions _options = new()
		{
			PropertyNameCaseInsensitive = true
		};

		private async Task<ObservableCollection<Class1>> GetAdmins(string path)
		{
			//await Task.CompletedTask;

			//var result = JsonSerializer.Deserialize<ObservableCollection<Class1>>(GetJson());
			//return result;

			var httpClient = new HttpClient();

			var admins = await httpClient.GetFromJsonAsync<ObservableCollection<Class1>>(path, _options);
			return admins ?? new ObservableCollection<Class1>();
		}

		private async Task UpdateAdmin(Class1 selectedAdmin)
		{
			var confirmation = await JSRuntime.InvokeAsync<bool>("confirm", "This will update the selected row");

			if (confirmation)
			{
				await loadingIndicator.Show();
				await Task.Delay(ClientAppSettings.ConfirmationDelayInMilliSeconds);
				await loadingIndicator.Hide();
			}
		}

		private async Task DeleteAdmin(Class1 selectedAdmin)
		{
			_selectedRowId = selectedAdmin.id;
			_showConfirmation = true;
		}

        private void HandleConfirmationResult((bool confirmed, int rowId) result)
        {
            _showConfirmation = false;

            if (result.confirmed)
            {
                var item = _admins.Where(x => x.id == result.rowId).First();
                _admins.Remove(item);
            }
        }

		private void OnRowStyling(Class1 admin, DataGridRowStyling styling)
		{
			styling.Class = "";
		}
        private void OnSelectedRowStyling(Class1 admin, DataGridRowStyling styling)
		{
			styling.Class = "";

		}

		//private string GetJson()
		//{
		//	var jsonString = """
		//			[
		//		  {
		//		    "id": 2,
		//		    "name": "Ervin Howell",
		//		    "username": "Antonette",
		//		    "email": "Shanna@melissa.tv",
		//		    "address": {
		//		      "street": "Victor Plains",
		//		      "suite": "Suite 879",
		//		      "city": "Wisokyburgh",
		//		      "zipcode": "90566-7771",
		//		      "geo": {
		//		        "lat": "-43.9509",
		//		        "lng": "-34.4618"
		//		      }
		//		    },
		//		    "phone": "010-692-6593 x09125",
		//		    "website": "anastasia.net",
		//		    "company": {
		//		      "name": "Deckow-Crist",
		//		      "catchPhrase": "Proactive didactic contingency",
		//		      "bs": "synergize scalable supply-chains"
		//		    }
		//		  },
		//		  {
		//		    "id": 3,
		//		    "name": "Clementine Bauch",
		//		    "username": "Samantha",
		//		    "email": "Nathan@yesenia.net",
		//		    "address": {
		//		      "street": "Douglas Extension",
		//		      "suite": "Suite 847",
		//		      "city": "McKenziehaven",
		//		      "zipcode": "59590-4157",
		//		      "geo": {
		//		        "lat": "-68.6102",
		//		        "lng": "-47.0653"
		//		      }
		//		    },
		//		    "phone": "1-463-123-4447",
		//		    "website": "ramiro.info",
		//		    "company": {
		//		      "name": "Romaguera-Jacobson",
		//		      "catchPhrase": "Face to face bifurcated interface",
		//		      "bs": "e-enable strategic applications"
		//		    }
		//		  },
		//		  {
		//		    "id": 4,
		//		    "name": "Patricia Lebsack",
		//		    "username": "Karianne",
		//		    "email": "Julianne.OConner@kory.org",
		//		    "address": {
		//		      "street": "Hoeger Mall",
		//		      "suite": "Apt. 692",
		//		      "city": "South Elvis",
		//		      "zipcode": "53919-4257",
		//		      "geo": {
		//		        "lat": "29.4572",
		//		        "lng": "-164.2990"
		//		      }
		//		    },
		//		    "phone": "493-170-9623 x156",
		//		    "website": "kale.biz",
		//		    "company": {
		//		      "name": "Robel-Corkery",
		//		      "catchPhrase": "Multi-tiered zero tolerance productivity",
		//		      "bs": "transition cutting-edge web services"
		//		    }
		//		  },
		//		  {
		//		    "id": 5,
		//		    "name": "Chelsey Dietrich",
		//		    "username": "Kamren",
		//		    "email": "Lucio_Hettinger@annie.ca",
		//		    "address": {
		//		      "street": "Skiles Walks",
		//		      "suite": "Suite 351",
		//		      "city": "Roscoeview",
		//		      "zipcode": "33263",
		//		      "geo": {
		//		        "lat": "-31.8129",
		//		        "lng": "62.5342"
		//		      }
		//		    },
		//		    "phone": "(254)954-1289",
		//		    "website": "demarco.info",
		//		    "company": {
		//		      "name": "Keebler LLC",
		//		      "catchPhrase": "User-centric fault-tolerant solution",
		//		      "bs": "revolutionize end-to-end systems"
		//		    }
		//		  },
		//		  {
		//		    "id": 6,
		//		    "name": "Mrs. Dennis Schulist",
		//		    "username": "Leopoldo_Corkery",
		//		    "email": "Karley_Dach@jasper.info",
		//		    "address": {
		//		      "street": "Norberto Crossing",
		//		      "suite": "Apt. 950",
		//		      "city": "South Christy",
		//		      "zipcode": "23505-1337",
		//		      "geo": {
		//		        "lat": "-71.4197",
		//		        "lng": "71.7478"
		//		      }
		//		    },
		//		    "phone": "1-477-935-8478 x6430",
		//		    "website": "ola.org",
		//		    "company": {
		//		      "name": "Considine-Lockman",
		//		      "catchPhrase": "Synchronised bottom-line interface",
		//		      "bs": "e-enable innovative applications"
		//		    }
		//		  },
		//		  {
		//		    "id": 7,
		//		    "name": "Kurtis Weissnat",
		//		    "username": "Elwyn.Skiles",
		//		    "email": "Telly.Hoeger@billy.biz",
		//		    "address": {
		//		      "street": "Rex Trail",
		//		      "suite": "Suite 280",
		//		      "city": "Howemouth",
		//		      "zipcode": "58804-1099",
		//		      "geo": {
		//		        "lat": "24.8918",
		//		        "lng": "21.8984"
		//		      }
		//		    },
		//		    "phone": "210.067.6132",
		//		    "website": "elvis.io",
		//		    "company": {
		//		      "name": "Johns Group",
		//		      "catchPhrase": "Configurable multimedia task-force",
		//		      "bs": "generate enterprise e-tailers"
		//		    }
		//		  },
		//		  {
		//		    "id": 8,
		//		    "name": "Nicholas Runolfsdottir V",
		//		    "username": "Maxime_Nienow",
		//		    "email": "Sherwood@rosamond.me",
		//		    "address": {
		//		      "street": "Ellsworth Summit",
		//		      "suite": "Suite 729",
		//		      "city": "Aliyaview",
		//		      "zipcode": "45169",
		//		      "geo": {
		//		        "lat": "-14.3990",
		//		        "lng": "-120.7677"
		//		      }
		//		    },
		//		    "phone": "586.493.6943 x140",
		//		    "website": "jacynthe.com",
		//		    "company": {
		//		      "name": "Abernathy Group",
		//		      "catchPhrase": "Implemented secondary concept",
		//		      "bs": "e-enable extensible e-tailers"
		//		    }
		//		  },
		//		  {
		//		    "id": 9,
		//		    "name": "Glenna Reichert",
		//		    "username": "Delphine",
		//		    "email": "Chaim_McDermott@dana.io",
		//		    "address": {
		//		      "street": "Dayna Park",
		//		      "suite": "Suite 449",
		//		      "city": "Bartholomebury",
		//		      "zipcode": "76495-3109",
		//		      "geo": {
		//		        "lat": "24.6463",
		//		        "lng": "-168.8889"
		//		      }
		//		    },
		//		    "phone": "(775)976-6794 x41206",
		//		    "website": "conrad.com",
		//		    "company": {
		//		      "name": "Yost and Sons",
		//		      "catchPhrase": "Switchable contextually-based project",
		//		      "bs": "aggregate real-time technologies"
		//		    }
		//		  },
		//		  {
		//		    "id": 10,
		//		    "name": "Clementina DuBuque",
		//		    "username": "Moriah.Stanton",
		//		    "email": "Rey.Padberg@karina.biz",
		//		    "address": {
		//		      "street": "Kattie Turnpike",
		//		      "suite": "Suite 198",
		//		      "city": "Lebsackbury",
		//		      "zipcode": "31428-2261",
		//		      "geo": {
		//		        "lat": "-38.2386",
		//		        "lng": "57.2232"
		//		      }
		//		    },
		//		    "phone": "024-648-3804",
		//		    "website": "ambrose.net",
		//		    "company": {
		//		      "name": "Hoeger LLC",
		//		      "catchPhrase": "Centralized empowering task-force",
		//		      "bs": "target end-to-end models"
		//		    }
		//		  }
		//		]
		//		""";
		//	return jsonString;
		//}
	}
}