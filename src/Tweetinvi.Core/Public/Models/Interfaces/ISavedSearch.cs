﻿using System;
using Tweetinvi.Models.DTO;

namespace Tweetinvi.Models
{
    /// <summary>
    /// Twitter saved search.
    /// </summary>
    public interface ISavedSearch
    {
        /// <summary>
        /// Saved search backend data.
        /// </summary>
        ISavedSearchDTO SavedSearchDTO { get; set; }

        /// <summary>
        /// Saved search id.
        /// </summary>
        long Id { get; }

        /// <summary>
        /// Id as string.
        /// </summary>
        string IdStr { get; }
        
        /// <summary>
        /// Given name.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Query performed when executing the search.
        /// </summary>
        string Query { get; set; }

        /// <summary>
        /// Creation date.
        /// </summary>
        DateTimeOffset CreatedAt { get; }
    }
}