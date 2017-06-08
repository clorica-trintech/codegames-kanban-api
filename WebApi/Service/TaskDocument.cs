using System;
using Nest;

namespace WebApi.Service
{
    [ElasticsearchType]
    public class TaskDocument
    {
        public string logsid;
        public string id { get; set; }
        public string name { get; set; }
        public string action_plan { get; set; }
        public string action { get; set; }
        public string priority { get; set; }
        public string status { get; set; }
        public string entity { get; set; }
        public string close_period { get; set; }
        public DateTime duedate { get; set; }
        public string closeperiod { get; set; }
        public DateTime startdate { get; set; }
        public string owner { get; set; }
        public string approver { get; set; }
        public string reviewer { get; set; }
    }
}