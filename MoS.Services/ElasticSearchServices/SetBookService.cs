﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MoS.Services.ElasticSearchServices
{
    public class SetBookService
    {
        private readonly ISetBook _repository;

        public SetBookService(ISetBook repository)
        {
            _repository = repository;
        }

        public class ElasticSearchRequestBody
        {
            [JsonPropertyName("id")]
            public Guid Id { get; set; }
            [JsonPropertyName("title")]
            public string Title { get; set; }
        }

        public class Shards
        {
            public int Total { get; set; }
            public int Successful { get; set; }
            public int Failed { get; set; }
        }

        public class ElasticSearchResponseBody
        {
            public string _Index { get; set; }
            public string _Type { get; set; }
            public string _Id { get; set; }
            public int _Version { get; set; }
            public string _Result { get; set; }
            public Shards _Shards { get; set; }
            public int _Seq_no { get; set; }
            public int _Primary_Term { get; set; }
        }

        public interface ISetBook
        {
            Task Set(ElasticSearchRequestBody body, Action<ElasticSearchResponseBody> onSuccess, Action onFail);
        }

        public async Task Set(ElasticSearchRequestBody body, Action<ElasticSearchResponseBody> onSuccess, Action onFail)
        {
            await _repository.Set(body, onSuccess, onFail);
        }
    }
}
