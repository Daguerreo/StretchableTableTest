using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace StretchableTest.Extensions
{
    /// <summary>
    /// Add utilty method for Canvas
    /// </summary>
    public static class CanvasExtension
    {
        /// <summary>
        /// Add element to canvas
        /// </summary>
        /// <param name="canvas">Canvas object</param>
        /// <param name="element">UIElement</param>
        public static void AddChild<T>(this Canvas canvas, T element)
        {
            UIElement uiElement = element as UIElement;

            if (uiElement != null && !canvas.Children.Contains(uiElement))
                canvas.Children.Add(uiElement);
        }

        /// <summary>
        /// Add element to canvas
        /// </summary>
        /// <param name="canvas">Canvas object</param>
        /// <param name="element">UIElement</param>
        public static void RemoveChild<T>(this Canvas canvas, T element)
        {
            UIElement uiElement = element as UIElement;

            if (uiElement != null && canvas.Children.Contains(uiElement))
                canvas.Children.Remove(uiElement);
        }

        /// <summary>
        /// Place (if child), element to position x, y
        /// </summary>
        /// <param name="canvas">Canvas object</param>
        /// <param name="element">UIElement</param>
        /// <param name="x">Left position</param>
        /// <param name="y">Top position</param>
        public static void Place<T>(this Canvas canvas, T element, double x, double y)
        {
            UIElement uiElement = element as UIElement;

            if (uiElement != null && canvas.Children.Contains(uiElement))
            {
                Canvas.SetLeft(uiElement, x);
                Canvas.SetTop(uiElement, y);
            }
        }

        /// <summary>
        /// Add element to canvas and place to position x and y
        /// </summary>
        /// <param name="canvas">Canvas object</param>
        /// <param name="element">UIElement</param>
        /// <param name="x">Left position</param>
        /// <param name="y">Top position</param>
        public static void AddAndPlace<T>(this Canvas canvas, T element, double x, double y)
        {
            UIElement uiElement = element as UIElement;

            if (uiElement != null && !canvas.Children.Contains(uiElement))
            {
                canvas.Children.Add(uiElement);
                Canvas.SetLeft(uiElement, x);
                Canvas.SetTop(uiElement, y);
            }
        }
    }
}
