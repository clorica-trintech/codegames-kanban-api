using System;
using System.Linq;
using Nest;

namespace WebApi.Service
{
    public class SearchService
    {
        public TaskDocument[] Search()
        {
            
            var settings = new ConnectionSettings(new Uri("http://localhost:9200"))
                .DefaultIndex("close");
            var client = new ElasticClient(settings);
            var request = client.Search<TaskDocument>(x=>x.AllIndices().Type("task"));
            return request.Documents.ToArray();
        }

    }
}
