using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StretchableTest.Controls;

/// <summary>
/// Generict product factory
/// </summary>
/// Scalabile tipizzando le factory e i prodotti
namespace StretchableTest.Components
{
    public static class ProductFactory
    {
        /// <summary>
        /// Get a Chair Object
        /// </summary>
        /// <returns>Chair</returns>
        public static Chair GetChair()
        {
            return new Chair();
        }

        /// <summary>
        /// Get a Table object
        /// </summary>
        /// <returns>StretchableTable</returns>
        public static StretchableTable GetTable()
        {
            return new StretchableTable();
        }
    }
}
