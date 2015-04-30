namespace TaskFlamingo.UI.Services
{
  using System;
  using System.Collections.Generic;
  using System.Net.Http;
  using System.Net.Http.Headers;
  using System.Threading.Tasks;

  using TaskFlamingo.UI.Models;

  public class PersonService
  {
    private const string HOST = "http://localhost:49445/";

    public async Task<IEnumerable<Person>> GetAllPeopleAsync()
    {
      using (var client = new HttpClient())
      {
        client.BaseAddress = new Uri(HOST);
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        var response = await client.GetAsync("api/person").ConfigureAwait(false); ;
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsAsync<Person[]>();
      }
    }

  }
}