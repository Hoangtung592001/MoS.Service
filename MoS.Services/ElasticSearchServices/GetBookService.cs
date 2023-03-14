using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MoS.Services.ElasticSearchServices
{
    public class GetBookService
    {
        private readonly IGetBook _repository;

        public GetBookService(IGetBook repository)
        {
            _repository = repository;
        }

        public class MatchBoolPrefix
        {
            [JsonPropertyName("title")]
            public string Title { get; set; }
        }

        public class Query
        {
            [JsonPropertyName("match_bool_prefix")]
            public MatchBoolPrefix Match_bool_prefix { get; set; }
        }

        public class ElasticSearchBookResquestBody {
            [JsonPropertyName("query")]
            public Query Query { get; set; }
        }

        public class SearchBookRequest
        {
            public string Title { get; set; }
        }

        public class Shards
        {
            public int Total { get; set; }
            public int Successful { get; set; }
            public int Skipped { get; set; }
            public int Failed { get; set; }
        }

        public class Source
        {
            public Guid Id { get; set; }
            public string Title { get; set; }
            public bool IsDeleted { get; set; }
        }

        public class ResultHit
        {
            [JsonPropertyName("_index")]
            public string _index { get; set; }
            [JsonPropertyName("_type")]
            public string _type { get; set; }
            [JsonPropertyName("_id")]
            public string _id { get; set; }
            [JsonPropertyName("_score")]
            public double _score { get; set; }
            [JsonPropertyName("_source")]
            public Source _source { get; set; }
        }

        public class Total
        {
            [JsonPropertyName("value")]
            public int Value { get; set; }
            [JsonPropertyName("relation")]
            public string Relation { get; set; }
        }

        public class Hits
        {
            [JsonPropertyName("total")]
            public Total Total { get; set; }
            [JsonPropertyName("max_score")]
            public double? Max_score { get; set; }
            [JsonPropertyName("hits")]
            public IList<ResultHit> hits { get; set; }
        }

        public class ElasticSearchBookResponseBody
        {
            [JsonPropertyName("took")]
            public int Took { get; set; }
            [JsonPropertyName("timed_out")]
            public bool Timed_out { get; set; }
            [JsonPropertyName("_shards")]
            public Shards _Shards { get; set; }
            [JsonPropertyName("hits")]
            public Hits Hits { get; set; }
        }

        public class Book
        {
            public Guid Id { get; set; }
            public string Title { get; set; }
        }

        public class WholeBook
        {
            public Guid Id { get; set; }
            public string Title { get; set; }
            public string Url { get; set; }
            public string Author { get; set; }
        }

        public interface IGetBook
        {
            Task<IEnumerable<Book>> Get(string title, int limit);
            Task<IEnumerable<WholeBook>> GetWhole(string title, int limit);
        }

        public async Task<IEnumerable<Book>> Get(string title, int limit)
        {
            return await _repository.Get(title, limit);
        }

        public async Task<IEnumerable<WholeBook>> GetWhole(string title, int limit)
        {
            return await _repository.GetWhole(title, limit);
        }
    }
}
