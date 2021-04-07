using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi_PayRoll.Models.Api
{
    /// <summary>
    /// Holds information returned by a WebApi
    /// </summary>
    public class Responser
    {
        /// <summary>
        /// Status Code returned 
        /// </summary>
            public int Status { get; set; }


        /// <summary>
        /// Status Message description
        /// </summary>
            public string StatusMessage { get; set; }
            
        /// <summary>
        /// Information returned
        /// </summary>
            public object Data { get; set; }
      
    }

    /// <summary>
    /// Extensions for Resonser
    /// </summary>
    public static class ResponserExtended
    {
    
        #region "------------------------------------- Conversions -------------------------------------"
    
            /// <summary>
            /// Get information within Data
            /// </summary>
            /// <typeparam name="T">Type of items</typeparam>
            /// <returns></returns>
                public static List<T> GetAll<T>(this object data_) 
                {
                    if (data_ is List<T>)
                    {
                       List<T> listToReturn = new List<T>();
                       var enumerator = ((List<T>)data_).GetEnumerator();
    
                       while (enumerator.MoveNext())
                       {
                          listToReturn.Add(enumerator.Current);
                       }
    
                       return listToReturn;
                    }
                
                    return new List<T>();
                }
    
            /// <summary>
            /// Get information within Data
            /// </summary>
            /// <typeparam name="T">Type of item</typeparam>
            /// <returns></returns>
                public static T Get<T>(this object data_) 
                {
                
                    T itemToReturn = (T)data_;
    
                    return itemToReturn;
    
                }
    
        #endregion
    
        #region "-------------------------------------- Json Management --------------------------------------"
        
            /// <summary>
            /// Get a Json deserialized from a Json Format
            /// </summary>
            /// <param name="json">Json string</param>
            /// <returns></returns>
                public static dynamic ToJsonDeserialized(this string json)
                {
                    dynamic jsonDeserialized = JsonConvert.DeserializeObject(json);
        
                    return jsonDeserialized;
                }
        
            /// <summary>
            /// Get a Json deserialized from a Json Format
            /// </summary>
            /// <param name="json">Json string</param>
            /// <returns>Mapped information</returns>
                public static dynamic MapFromJson<T>(this string json)
                {
                    T jsonDeserialized = JsonConvert.DeserializeObject<T>(json);
        
                    return jsonDeserialized;
                }
        
        #endregion


    }            
            
}