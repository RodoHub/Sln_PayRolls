using System;
using System.Collections.Generic;

namespace WebPayRoll_.Models.Extensions
{
    /// <summary>
    /// Provides extensions for RBS.Solutions.Web.Components.Midas
    /// </summary>
    public static class Generics
    {

        /// <summary>
        /// Maps an entity into another
        /// </summary>
        /// <typeparam name="U">Type of object to map</typeparam>
        /// <typeparam name="T">Type of mapped object to return</typeparam>
        /// <param name="dataSource">source object</param>
        /// <returns>mapped object</returns>
        public static T MapTo<U, T>(this U dataSource) where T : class, new()
        {

            //Use Reflection to get the information about objects to try
            Type reflectionObjectSource = typeof(U);
            Type reflectionObjectReturn = typeof(T);

            //Get the properties of each object to try
            System.Reflection.PropertyInfo[] propertiesObjectSource = reflectionObjectSource.GetProperties();
            System.Reflection.PropertyInfo[] propertiesObjectReturn = reflectionObjectReturn.GetProperties();

            //Prepare data objects to use                                            
            T itemToReturn = new T();

            //Map information
            //Parse the properties in order to get the values to map
            foreach (System.Reflection.PropertyInfo propertySource in propertiesObjectSource)
            {

                foreach (System.Reflection.PropertyInfo propertyDestiny in propertiesObjectReturn)
                {
                    if (propertySource.Name.Trim().ToLower() == propertyDestiny.Name.Trim().ToLower())
                    {
                        propertyDestiny.SetValue(itemToReturn, propertySource.GetValue(dataSource, null));
                        break;
                    }
                }
            }

            return itemToReturn;
        }

        /// <summary>
        /// Maps a list of objects into another list of objects
        /// </summary>
        /// <typeparam name="U">Type of list object to map</typeparam>
        /// <typeparam name="T">Type of mapped list object to return</typeparam>
        /// <param name="dataSource">source list object</param>
        /// <returns>mapped list object</returns>
        public static List<T> MapTo<U, T>(this List<U> dataSource) where T : class, new()
        {
            //Prepare data objects to use                                            
            List<T> listToReturn = new List<T>();

            //Map information
            //Parse the properties in order to get the values to map
            if (dataSource.Count > 0)
            {
                foreach (U itemToAdd in dataSource)
                {
                    listToReturn.Add(itemToAdd.MapTo<U, T>());
                }
            }

            return listToReturn;
        }

    }
}
