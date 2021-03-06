using System.Threading.Tasks;
using Newtonsoft.Json;
using Tweetinvi.Core.Web;
using Tweetinvi.Models;
using Tweetinvi.Models.V2;
using Tweetinvi.Parameters.V2;
using HttpMethod = Tweetinvi.Models.HttpMethod;

namespace Tweetinvi.Controllers.Streams
{
    public interface IStreamsV2QueryExecutor
    {
        Task<ITwitterResult<FilteredStreamRulesV2Response>> GetRulesForFilteredStreamV2Async(IGetRulesForFilteredStreamV2Parameters parameters, ITwitterRequest request);
        Task<ITwitterResult<FilteredStreamRulesV2Response>> AddRulesToFilteredStreamAsync(IAddRulesToFilteredStreamV2Parameters parameters, ITwitterRequest request);
        Task<ITwitterResult<FilteredStreamRulesV2Response>> DeleteRulesFromFilteredStreamAsync(IDeleteRulesFromFilteredStreamV2Parameters parameters, ITwitterRequest request);
        Task<ITwitterResult<FilteredStreamRulesV2Response>> TestFilteredStreamRulesV2Async(IAddRulesToFilteredStreamV2Parameters parameters, ITwitterRequest request);
    }

    public class StreamsV2QueryExecutor : IStreamsV2QueryExecutor
    {
        private readonly JsonContentFactory _jsonContentFactory;
        private readonly IStreamsV2QueryGenerator _streamsV2QueryGenerator;
        private readonly ITwitterAccessor _twitterAccessor;

        public StreamsV2QueryExecutor(
            JsonContentFactory jsonContentFactory,
            IStreamsV2QueryGenerator streamsV2QueryGenerator,
            ITwitterAccessor twitterAccessor)
        {
            _jsonContentFactory = jsonContentFactory;
            _streamsV2QueryGenerator = streamsV2QueryGenerator;
            _twitterAccessor = twitterAccessor;
        }

        public Task<ITwitterResult<FilteredStreamRulesV2Response>> GetRulesForFilteredStreamV2Async(IGetRulesForFilteredStreamV2Parameters parameters, ITwitterRequest request)
        {
            request.Query.Url = _streamsV2QueryGenerator.GetRulesForFilteredStreamV2Query(parameters);
            return _twitterAccessor.ExecuteRequestAsync<FilteredStreamRulesV2Response>(request);
        }

        public Task<ITwitterResult<FilteredStreamRulesV2Response>> AddRulesToFilteredStreamAsync(IAddRulesToFilteredStreamV2Parameters parameters, ITwitterRequest request)
        {
            var content = new FilteredStreamOperations { add = parameters.Rules };

            request.Query.Url = _streamsV2QueryGenerator.GetAddRulesToFilteredStreamQuery(parameters);
            request.Query.HttpMethod = HttpMethod.POST;
            request.Query.HttpContent = _jsonContentFactory.Create(content);
            return _twitterAccessor.ExecuteRequestAsync<FilteredStreamRulesV2Response>(request);
        }

        public Task<ITwitterResult<FilteredStreamRulesV2Response>> DeleteRulesFromFilteredStreamAsync(IDeleteRulesFromFilteredStreamV2Parameters parameters, ITwitterRequest request)
        {
            var content = new FilteredStreamOperations { delete = new FilteredStreamDeleteOperation(parameters.RuleIds)};

            request.Query.Url = _streamsV2QueryGenerator.GetDeleteRulesFromFilteredStreamQuery(parameters);
            request.Query.HttpMethod = HttpMethod.POST;
            request.Query.HttpContent = _jsonContentFactory.Create(content);
            return _twitterAccessor.ExecuteRequestAsync<FilteredStreamRulesV2Response>(request);
        }

        public Task<ITwitterResult<FilteredStreamRulesV2Response>> TestFilteredStreamRulesV2Async(IAddRulesToFilteredStreamV2Parameters parameters, ITwitterRequest request)
        {
            var content = new FilteredStreamOperations { add = parameters.Rules };

            request.Query.Url = _streamsV2QueryGenerator.GetTestFilteredStreamRulesV2Query(parameters);
            request.Query.HttpMethod = HttpMethod.POST;
            request.Query.HttpContent = _jsonContentFactory.Create(content);
            return _twitterAccessor.ExecuteRequestAsync<FilteredStreamRulesV2Response>(request);
        }

        private class FilteredStreamDeleteOperation
        {
            public FilteredStreamDeleteOperation(string[] ids)
            {
                this.ids = ids;
            }
            [JsonProperty("ids")] public string[] ids { get; set; }
        }


        private class FilteredStreamOperations
        {
            [JsonProperty("add")] public FilteredStreamRuleConfig[] add { get; set; }
            [JsonProperty("delete")] public FilteredStreamDeleteOperation delete { get; set; }
        }
    }
}