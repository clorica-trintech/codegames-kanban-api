using System;
using System.Linq;
using Nest;

namespace WebApi.Service
{
    public class SearchService
    {
        private ElasticClient GetClient()
        {
            var settings = new ConnectionSettings(new Uri("http://elasticsearch:9200"))
            //var settings = new ConnectionSettings(new Uri("http://localhost:9200"))
                .DefaultIndex("close");
           return new ElasticClient(settings);
        }
        public TaskDto[] Search()
        {


            var request = GetClient().Search<TaskDocument>(x => x
                .AllIndices()
                .Type("task"));
            return request.Documents.Select(Convert).ToArray();
        }
        public TaskDto[] Search(string owner)
        {
            
           
            var request = GetClient().Search<TaskDocument>(x=>x
            .Index("close")
            .Type("task")
             .Query(q=>q.Term(p=>p.owner,owner))) ;
            
            return request.Documents.Select(Convert).ToArray();
        }

        private TaskDto Convert(TaskDocument doc)
        {
            return new TaskDto()
            {
                Owner = doc.owner?? string.Empty,
                Action = doc.action ?? string.Empty,
                ActionPlan = doc.action_plan ?? string.Empty,
                Assignee = GetAssignee(doc),
                CanReopen = GetCanReOpen(doc),
                ClosePeriod = doc.close_period,
                DoneStatus = GetDoneStatus(doc),
                DueDate = doc.duedate,
                Entity = doc.entity ?? string.Empty,
                Id = doc.id ?? string.Empty,
                InProgressStatus = GetInprogressStatus(doc),
                Name = doc.name ?? string.Empty,
                Priority = doc.priority ?? string.Empty,
                StartDate = doc.startdate,
                Status = doc.status ?? string.Empty,
                ToDoRole = GetToDoRole(doc),
                WorkFlowTag = GetWorkFlowTag(doc),
                LogSid = doc.logsid ?? string.Empty,
                Approver = doc.approver ?? string.Empty,
                Reviewer = doc.reviewer ?? string.Empty

            };
        }

        private string GetDoneStatus(TaskDocument doc)
        {
            if (doc.status == "Deleted") return "Cancelled";
            if (doc.status == "Complete") return "Completed";
            return String.Empty;
        }

        private string GetToDoRole(TaskDocument doc)
        {
            if (doc.status == "In Progress" 
                && string.IsNullOrWhiteSpace(doc.approver) 
                && string.IsNullOrWhiteSpace(doc.reviewer))
                return "Preparer";

            if (doc.status == "Not Started"
                && doc.owner == doc.approver)
                return "Approver";


            if (doc.status == "Not Started"
                && doc.owner == doc.reviewer)
                return "Reviewer";

            return "Proxy";
        }

        private string GetWorkFlowTag(TaskDocument doc)
        {
            switch (doc.status)
            {
                case "Not Started": return "To Do";
                case "In Progress": return "In Progress";
                case "Complete": return "Completed";
                case "Deleted": return "Completed";
            }
            return string.Empty;
        }

        private bool GetCanReOpen(TaskDocument doc)
        {
            if (doc.status == "Deleted") return false;
            return doc.status == "Complete" 
                && string.IsNullOrWhiteSpace(doc.approver) 
                && string.IsNullOrWhiteSpace(doc.reviewer);
        }

        private string GetInprogressStatus(TaskDocument doc)
        {
            if (doc.status == "In Progress")
            {
                if (!string.IsNullOrWhiteSpace(doc.approver))
                    return "In Review";
                if (!string.IsNullOrWhiteSpace(doc.approver))
                    return "In Approval";
            }
            return string.Empty;
        }

        private string GetAssignee(TaskDocument doc)
        {
            if (doc.status == "In Progress" && !string.IsNullOrWhiteSpace(doc.approver))
                return doc.approver;
            if (doc.status == "In Progress" && !string.IsNullOrWhiteSpace(doc.reviewer))
                return doc.reviewer;
            return doc.owner;
        }
    }
}
