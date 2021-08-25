using System;
using Dabble.Data.Abstractions.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Dabble.GraphQL.Models
{
    /// <summary>
    /// DTO object for a Statistic
    /// </summary>
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class StatisticDto
    {
        
        //public string Id { get; set; }

        /// <summary>
        /// Country
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Year
        /// </summary>
        public string Year { get; set; }

        /// <summary>
        /// Area
        /// </summary>
        public int Area { get; set; }

        /// <summary>
        /// Population
        /// </summary>
        public int Population { get; set; }


        /// <summary>
        /// Converts an <see cref="Statistic"/> into an <see cref="StatisticDto"/>
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static explicit operator StatisticDto(Statistic entity) =>
            entity is null
                ? null
                : new StatisticDto
                {
                    Country = entity.Country,
                    Year = entity.Year,
                    Area = entity.Area,
                    Population = entity.Population
                };
    }
}
