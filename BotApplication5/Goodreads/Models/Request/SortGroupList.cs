﻿using Goodreads.Http;

namespace Goodreads.Models.Request
{
    /// <summary>
    /// The options for sorting the Goodreads group list.
    /// </summary>
    [QueryParameterKey("sort")]
    public enum SortGroupList
    {
        /// <summary>
        /// Sort by my activity.
        /// </summary>
        [QueryParameterValue("my_activity")]
        MyActivity,

        /// <summary>
        /// Sort by members.
        /// </summary>
        [QueryParameterValue("members")]
        Members,

        /// <summary>
        /// Sort by the last activity.
        /// </summary>
        [QueryParameterValue("last_activity")]
        LastActivity,

        /// <summary>
        /// Sort by title.
        /// </summary>
        [QueryParameterValue("title")]
        Title
    }
}
