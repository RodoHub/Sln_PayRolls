using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi_PayRoll.Models.Api
{
    /// <summary>
    /// Holds information returned by a WebApi
    /// </summary>
    public class AM_LoginEntity
    {

        /// <summary>
        /// User name
        /// </summary>
            public string UserName { get; set; }

        /// <summary>
        /// User name
        /// </summary>
            public string Password { get; set; }

    }
}