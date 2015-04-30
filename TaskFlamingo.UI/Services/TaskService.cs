namespace TaskFlamingo.UI.Services
{
  using System;
  using System.Collections.Generic;
  using System.Net.Http;
  using System.Net.Http.Headers;
  using System.Threading.Tasks;

  using TaskFlamingo.UI.Models;

  public class TaskService
  {
    private const string HOST = "http://localhost:49445/";

    public async Task<IEnumerable<TaskListItem>> GetMyTasksAsync(Guid personId)
    {
        using (var client = new HttpClient())
        {
          client.BaseAddress = new Uri(HOST);
          client.DefaultRequestHeaders.Accept.Clear();
          client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

          var response = await client.GetAsync("api/Task?personId=" + personId).ConfigureAwait(false);;
          response.EnsureSuccessStatusCode();
          return await response.Content.ReadAsAsync<TaskListItem[]>();
        } 
    }
  }
}