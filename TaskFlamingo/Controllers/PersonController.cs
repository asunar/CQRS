using System;
using System.Collections.Generic;
using System.Web.Http;
using TaskFlamingo.Data;

namespace TaskFlamingo.Controllers
{
  
  public class PersonController : ApiController
  {
      [Route("api/people")]
      public IEnumerable<PersonListItem> Get()
      {
          var personRetriever = new PersonRetriever();
          return personRetriever.GetAll();
      }
  }

  public class PersonListItem
  {
    public Guid PersonId { get; set; }
    public string Name { get; set; }
  }

}