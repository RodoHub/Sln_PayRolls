using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi_PayRoll.Models.Entities
{
    /// <summary>
    /// Api Model Catalog: Holds information related to Catalogs
    /// </summary>
    public class AM_Catalog
    {
        /// <summary>
        /// ID Catalog
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Catalog Name
        /// </summary>
        public string Name { get; set; }
    }
}