using StretchableTest.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StretchableTest.Interface
{
    /// <summary>
    /// Defines the interface for a stretchable object
    /// </summary>
    /// La scelta e' relativa alla modalita' di visualizzazione di 
    /// Wpf che utilizza Left-Right Top-Bottom come riferimenti
    /// La struttura e' scalabile aggiungendo ulteriori punti di controllo
    public interface IStretchable
    {
        /// <summary>
        /// Defines the Left Property
        /// </summary>
        double Left { get; }

        /// <summary>
        /// Defines the Top Property
        /// </summary>
        double Top { get; }

        /// <summary>
        /// Defines the Right Property
        /// </summary>
        double Right { get; }

        /// <summary>
        /// Defines the Bottom Property
        /// </summary>
        double Bottom { get; }

        /// <summary>
        /// Defines the transformation origin point
        /// </summary>
        Point Pivot { get; set; }

        /// <summary>
        /// Defines which anchor type is currently active
        /// </summary>
        AnchorMode Anchor { get; set; }

        /// <summary>
        /// Stretching function
        /// </summary>
        void Stretch();
    }
}
