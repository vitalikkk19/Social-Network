using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DTO.Neo4j
{
    public class FollowerPathDTO
    {
        [JsonProperty(PropertyName = "length")]
        public string lenght { get; set; }
    }
}
