using System.ComponentModel.DataAnnotations;
using Dabble.Data.Abstractions.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Dabble.GraphQL.Models
{
    /// <summary>
    /// DTO object for creating a new Statistic
    /// </summary>
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class StatisticCreationDto
    {
        /// <summary>
        /// Country
        /// </summary>
        [Required]
        [MinLength(2)]
        [JsonProperty(Required = Required.Always)]
        public string Country { get; set; }

        /// <summary>
        /// Year
        /// </summary>
        [Required]
        [MinLength(8)]
        [JsonProperty(Required = Required.Always)]
        public string Year { get; set; }

        /// <summary>
        /// First name
        /// </summary>
        [Required]
        [JsonProperty(Required = Required.Always)]
        public int Area { get; set; }

        /// <summary>
        /// Area
        /// </summary>
        [MinLength(1)]
        [JsonProperty(Required = Required.Always)]
        public int Population { get; set; }

        /// <summary>
        /// Converts the DTO into a <see cref="Statistic"/> entity
        /// </summary>
        public static explicit operator Statistic(StatisticCreationDto dtoModel) =>
            dtoModel is null
                ? null
                : new Statistic
                {
                    Country = dtoModel.Country,
                    Year = dtoModel.Year,
                    Area = dtoModel.Area,
                    Population = dtoModel.Population
                };
    }
}
