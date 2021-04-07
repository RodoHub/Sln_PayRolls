using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft;
using Newtonsoft.Json;

namespace WebPayRoll_.Models.Api
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

            /// <summary>
            /// Get information within Data
            /// </summary>
            /// <typeparam name="T">Type of item</typeparam>
            /// <returns></returns>
                public static T GetFromJSon<T>(this object data_) 
                {
                    string dataJsonString = data_.ToString();
            
                    T itemToReturn = dataJsonString.MapFromJson<T>();
   
                    return itemToReturn;
    
                }


            /// <summary>
            /// Get information within Data
            /// </summary>
            /// <typeparam name="T">Type of item</typeparam>
            /// <returns></returns>
                public static List<T> GetAllFromJSon<T>(this object data_) 
                {
                    string dataJsonString = data_.ToString();
            
                    var itemToReturn_ = dataJsonString.MapAllFromJson();                    

                    List<T> itemToReturn = dataJsonString .MapFromJson<List<T>>();
   
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

            /// <summary>
            /// Get a Json deserialized from a Json Format
            /// </summary>
            /// <param name="json">Json string</param>
            /// <returns>Mapped information</returns>
                public static dynamic MapAllFromJson(this string json)
                {
                    var jsonDeserialized = JsonConvert.DeserializeObject(json);
        
                    return jsonDeserialized;
                }


        
        #endregion

    }            
            
}