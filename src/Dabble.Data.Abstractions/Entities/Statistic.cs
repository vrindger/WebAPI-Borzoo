using System;
using System.ComponentModel.DataAnnotations;

namespace Dabble.Data.Abstractions.Entities
{
    /// <summary>
    /// Represents a Statistic
    /// </summary>
    public class Statistic : IEntity
    {
        /// <summary>
        /// Unique identifier in the data store
        /// </summary>
        [Required]
        public string Id { get; set; }

        /// <summary>
        /// Country in the statistic
        /// </summary>
        [Required]
        public string Country { get; set; }

        /// <summary>
        /// Year of statistic
        /// </summary>
        [Required]
        public string Year { get; set; }

        /// <summary>
        /// Area in the statistic
        /// </summary>
        [Required]
        public int Area { get; set; }

        /// <summary>
        /// Population in the statistic
        /// </summary>
        [Required]
        public int Population { get; set; }

    }
}
