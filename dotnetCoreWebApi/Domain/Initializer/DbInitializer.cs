using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AltamiraTaskApi.Domain.Initializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public DbInitializer(IServiceScopeFactory scopeFactory)
        {
            this._scopeFactory = scopeFactory;
        }

        public void Initialize()
        {
            using (var serviceScope = _scopeFactory.CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<AltamiraContext>())
                {
                    context.Database.EnsureCreated();
                }
            }
        }

        public void SeedData()
        {
            using (var serviceScope = _scopeFactory.CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<AltamiraContext>())
                {
                    if (!context.User.Any())
                    {
                        RestClient client = new RestClient("https://jsonplaceholder.typicode.com");
                        var response = client.Execute(new RestRequest("users"));
                        JArray users = JArray.Parse(response.Content);
                        for (int i = 0; i < users.Count; i++)
                        {
                            var user = (JObject)users[i];
                            User _user = new User();
                            Address _address = new Address();
                            Geo _geo = new Geo();
                            Company _company = new Company();

                            foreach (JProperty property in user.Properties())
                            {
                                if (property.Name == "name")
                                {
                                    _user.Name = property.Value.ToString();
                                }
                                else if (property.Name == "username")
                                {
                                    _user.Username = property.Value.ToString();
                                }
                                else if (property.Name == "email")
                                {
                                    _user.Email = property.Value.ToString();
                                }
                                else if (property.Name == "address")
                                {
                                    string deneme = property.Value.ToString();
                                    JObject json = JObject.Parse(deneme);
                                    foreach (var item in json.Properties())
                                    {
                                        Console.WriteLine(item.Name + " - " + item.Value);
                                        if (item.Name == "street")
                                        {
                                            _address.Street = item.Value.ToString().Replace("{", "").Replace("}", "");
                                        }
                                        else if (item.Name == "suite")
                                        {
                                            _address.Suite = item.Value.ToString().Replace("{", "").Replace("}", "");
                                        }
                                        else if (item.Name == "city")
                                        {
                                            _address.City = item.Value.ToString().Replace("{", "").Replace("}", "");
                                        }
                                        else if (item.Name == "zipcode")
                                        {
                                            _address.Zipcode = item.Value.ToString().Replace("{", "").Replace("}", "");
                                        }

                                        else if (item.Name == "geo")
                                        {
                                            string geo = item.Value.ToString();
                                            JObject geos = JObject.Parse(geo);
                                            foreach (var geo_ in geos.Properties())
                                            {
                                                if (geo_.Name == "lat")
                                                {
                                                    _geo.Lat = geo_.Value.ToString().Replace("{", "").Replace("}", "");
                                                }
                                                else if (geo_.Name == "lng")
                                                {
                                                    _geo.Lng = geo_.Value.ToString().Replace("{", "").Replace("}", "");

                                                }

                                            }

                                        }
                                    }
                                }
                                else if (property.Name == "phone")
                                {
                                    _user.Phone = property.Value.ToString();

                                }
                                else if (property.Name == "website")
                                {
                                    _user.Website = property.Value.ToString();

                                }
                                else if (property.Name == "company")
                                {
                                    string deneme = property.Value.ToString();
                                    JObject json = JObject.Parse(deneme);
                                    foreach (var item in json.Properties())
                                    {
                                        Console.WriteLine(item.Name + " - " + item.Value);
                                        if (item.Name == "name")
                                        {
                                            _company.Name = item.Value.ToString().Replace("{", "").Replace("}", "");
                                        }
                                        else if (item.Name == "catchPhrase")
                                        {
                                            _company.CatchPhrase = item.Value.ToString().Replace("{", "").Replace("}", "");
                                        }
                                        else if (item.Name == "bs")
                                        {
                                            _company.Bs = item.Value.ToString().Replace("{", "").Replace("}", "");
                                        }

                                    }
                                }
                                _user.Address = _address;
                                _user.Address.Geo = _geo;
                                _user.Company = _company;

                            }
                            context.AddRange(_user);
                            context.SaveChanges();
                        }
                     
                    }

                }
            }
        }
    }
}
