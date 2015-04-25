using System;
using System.Collections.Generic;
using System.Web.Http;

namespace TaskFlamingo.Controllers
{
    public class TaskController : ApiController
    {
        public ScheduleTaskDto Get(string id)
        {
            return new ScheduleTaskDto {Name = "moo"};
        }

        public void Post( [FromBody]ScheduleTaskDto dto )
        {
            var command = new ScheduleTaskCommand(dto);
            var commandHandler = new CommandHandler();
            commandHandler.Handle(command);
        }

        // PUT api/values/5
        public void Put( int id, [FromBody]string value ) {
        }

        // DELETE api/values/5
        public void Delete( int id ) {
        }
    }

    public class CommandHandler
    {
        public void Handle(ScheduleTaskCommand command)
        {

        }
    }

    public class ScheduleTaskCommand
    {
        private readonly ScheduleTaskDto _dto;

        public ScheduleTaskCommand(ScheduleTaskDto dto)
        {
            _dto = dto;
        }
    }

    public class ScheduleTaskDto
    {
        public string Name { get; set; }
        public string Instructions { get; set; }
        public DateTime DueDate { get; set; }
        public List<string> Assignees { get; set; }
    }
}