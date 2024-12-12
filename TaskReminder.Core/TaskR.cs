using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskReminderTest
{
    public class TaskR
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public string Description { get; set; }

        public DateTime RemindMeAt {  get; set; } = DateTime.Now.AddMinutes(30);
        public DateTime Deadline { get; set; } = DateTime.Now.AddDays(1);
        public bool IsDead { get; set; } = false;
    }
}
