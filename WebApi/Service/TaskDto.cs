using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace WebApi.Service
{
    public class TaskDto
    {
        public string Id { get; set; }
        public string Owner { get; set; }
        
        public string WorkFlowTag { get; set; }
        public string Name { get; set; }
        public string ActionPlan { get; set; }
        public string Action { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }
        public string Status { get; set; }
        public string Entity { get; set; }
        public string ClosePeriod { get; set; }
        public string Priority { get; set; }
        public string ToDoRole { get; set; }
        public string InProgressStatus { get; set; }
        public string DoneStatus { get; set; }
        public bool CanReopen { get; set; }
        public string LogSid { get; set; }
        public string Approver { get; set; }
        public string Reviewer { get; set; }
        public string Assignee { get; set; }
    }
}
