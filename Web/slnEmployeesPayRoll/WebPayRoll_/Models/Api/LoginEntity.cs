using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebPayRoll_.Models.Api
{
    /// <summary>
    /// Holds information returned by a WebApi
    /// </summary>
    public class LoginEntity
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