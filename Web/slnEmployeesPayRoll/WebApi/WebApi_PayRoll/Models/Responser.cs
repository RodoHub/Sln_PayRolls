﻿// Code generated by Microsoft (R) AutoRest Code Generator 0.16.0.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

namespace WebApi_PayRoll.Models
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Microsoft.Rest;
    using Microsoft.Rest.Serialization;

    public partial class Responser
    {
        /// <summary>
        /// Initializes a new instance of the Responser class.
        /// </summary>
        public Responser() { }

        /// <summary>
        /// Initializes a new instance of the Responser class.
        /// </summary>
        public Responser(int? status = default(int?), string statusMessage = default(string), object data = default(object))
        {
            Status = status;
            StatusMessage = statusMessage;
            Data = data;
        }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "status")]
        public int? Status { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "statusMessage")]
        public string StatusMessage { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "data")]
        public object Data { get; set; }

    }
}
