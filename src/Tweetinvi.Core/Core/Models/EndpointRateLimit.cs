﻿using System;
using Newtonsoft.Json;
using Tweetinvi.Models;

namespace Tweetinvi.Core.Models
{
    public class EndpointRateLimit : IEndpointRateLimit
    {
        /// <summary>
        /// Number of operation available on the specific endpoint.
        /// </summary>
        [JsonProperty("remaining")]
        public int Remaining { get; set; }

        private long _reset;

        /// <summary>
        /// Integer representing the datetime when the endpoint rate limit will be reset.
        /// </summary>
        [JsonProperty("reset")]
        public long Reset
        {
            get => _reset;
            set
            {
                _reset = value;
                ResetDateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                ResetDateTime = ResetDateTime.AddSeconds(_reset).ToLocalTime();
            }
        }

        /// <summary>
        /// Maximum number of operations that can be performed in 15 minutes.
        /// </summary>
        [JsonProperty("limit")]
        public int Limit { get; set; }

        /// <summary>
        /// Duration in seconds after which the endpoint rate limit will be reset.
        /// </summary>
        [JsonIgnore]
        public double ResetDateTimeInSeconds
        {
            get
            {
                if (ResetDateTime <= DateTime.Now)
                {
                    return 0;
                }

                return (ResetDateTime - DateTime.Now).TotalSeconds;
            }
        }

        /// <summary>
        /// Duration in milliseconds after which the endpoint rate limit will be reset.
        /// </summary>
        [JsonIgnore]
        public double ResetDateTimeInMilliseconds => ResetDateTimeInSeconds * 1000;

        /// <summary>
        /// DateTime when the endpoint rate limit will be reset.
        /// </summary>
        [JsonIgnore]
        public DateTimeOffset ResetDateTime { get; private set; }

        public bool IsCustomHeaderRateLimit { get; set; }

        public override string ToString()
        {
            return $"{Remaining}/{Limit} (Reset in {ResetDateTimeInSeconds} seconds)";
        }
    }
}