using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebPayRoll_.Controllers;
using WebPayRoll_.Models.Api;

namespace WebPayRoll_.Models.Classes
{
    /// <summary>
    /// Holds the instance of a base model class
    /// </summary>
    public class MBase
    {

        /// <summary>
        /// Token authorization
        /// </summary>
        private Nullable<System.Guid> token;

        /// <summary>
        /// Token authorization
        /// </summary>
        public Nullable<System.Guid> Token 
        {
            get 
            { 
                return (token == null ? new Guid() :  token);
            }
            set
            {
                token = value;
            }
        }
    }
}