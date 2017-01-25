using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using StretchableTest.Extensions;
using StretchableTest.Enums;
using StretchableTest.Interface;

namespace StretchableTest.Controls
{
    /// <summary>
    /// Logica di interazione per StretchableTable.xaml
    /// </summary>
    public partial class StretchableTable : UserControl, IStretchable
    {
        /// <summary>
        /// Request a view update
        /// </summary>
        public event RoutedEventHandler UpdateViewEvent;

        #region Interface Properties

        public double Left { get { return Canvas.GetLeft(this); } }
        public double Top { get { return Canvas.GetTop(this); } }
        public double Right { get { return Canvas.GetLeft(this) + this.ActualWidth; } }
        public double Bottom { get { return Canvas.GetTop(this) + this.ActualHeight; } }
        public Point Pivot { get; set; } = new Point(0.5, 0.5);
        public AnchorMode Anchor { get; set; } = AnchorMode.None;

        #endregion Interface Properties

        private bool _isMoving = false;
        private Point _movDelta = new Point(0, 0);

        public StretchableTable()
        {
            InitializeComponent();

            TopLeftAnchor.MouseDown += TopLeftAnchor_MouseDown;
            TopRightAnchor.MouseDown += TopRightAnchor_MouseDown;
            BottomRightAnchor.MouseDown += BottomRightAnchor_MouseDown;
            BottomLeftAnchor.MouseDown += BottomLeftAnchor_MouseDown;

            MouseDown += StretchableTable_MouseDown;
            MouseMove += StretchableTable_MouseMove;
            MouseUp += StretchableTable_MouseUp;
            MouseLeave += StretchableTable_MouseLeave;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateViewEvent?.Invoke(this, null);
        }

        /// <summary>
        /// Update the anchor and pivot information
        /// </summary>
        /// <param name="newAnchor">AnchorMode Enum</param>
        private void ChangeAnchor(AnchorMode newAnchor)
        {
            Anchor = newAnchor;

            switch(Anchor)
            {
                case AnchorMode.TopLeft:
                    Pivot.Assign(Right, Bottom);
                    break;
                case AnchorMode.TopRight:
                    Pivot.Assign(Left, Bottom);
                    break;
                case AnchorMode.BottomRight:
                    Pivot.Assign(Left, Top);
                    break;
                case AnchorMode.BottomLeft:
                    Pivot.Assign(Right, Top);
                    break;
                case AnchorMode.None:
                    Pivot.Assign(0.5, 0.5);
                    break;
            }
        }

        /// <summary>
        /// Implementation of IStretchable.Stretch method.
        /// Manage stretching operations. 
        /// </summary>
        /// Per una questione di tempo e complessita' il metodo e' uno solo per tutte le ancore
        /// Può essere tipizzato ulteriormente realizzando un metodo per ogni tipo d'ancora
        public void Stretch()
        {
            Point mousePosition = _movDelta;

            double dx = 0;
            double dy = 0;
            switch (Anchor)
            {
                case AnchorMode.TopLeft:
                    dx = Right - _movDelta.X;
                    dy = Bottom - _movDelta.Y;
                    if (dx > MinWidth)
                    {
                        this.Width = dx;
                        Canvas.SetLeft(this, _movDelta.X);
                    }
                    if (dy > MinHeight)
                    {
                        this.Height = dy;
                        Canvas.SetTop(this, _movDelta.Y);
                    }
                    break;

                case AnchorMode.TopRight:
                    dx = _movDelta.X - Left;
                    dy = Bottom - _movDelta.Y;

                    if (dx > MinWidth)
                    {
                        this.Width = dx;
                    }
                    if (dy > MinHeight)
                    {
                        this.Height = dy;
                        Canvas.SetTop(this, _movDelta.Y);
                    }
                    break;

                case AnchorMode.BottomRight:
                    dx = _movDelta.X - Left;
                    dy = _movDelta.Y - Top;

                    if (dx > MinWidth)
                    {
                        this.Width = dx;
                    }
                    if (dy > MinHeight)
                    {
                        this.Height = dy;
                    }
                    break;

                case AnchorMode.BottomLeft:
                    dx = Right - _movDelta.X;
                    dy = _movDelta.Y - Top;

                    if (dx > MinWidth)
                    {
                        this.Width = dx;
                        Canvas.SetLeft(this, _movDelta.X);
                    }
                    if (dy > MinHeight)
                    {
                        this.Height = dy;
                    }
                    break;

                case AnchorMode.None:
                    break;
            }
        }

        // Manage the mouse event for the component
        #region Mouse Events
        private void StretchableTable_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (_isMoving)
            {
                e.Handled = true;
                return;
            }

            this.CaptureMouse();
            _isMoving = true;
        }

        private void StretchableTable_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_isMoving || Anchor == AnchorMode.None)
            {
                e.Handled = true;
                return;
            }

            _movDelta = e.GetPosition(Parent as UIElement);
            Stretch();       

            UpdateViewEvent?.Invoke(this, null);

            e.Handled = true;
        }

        private void StretchableTable_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.ReleaseMouseCapture();
            _isMoving = false;
            e.Handled = true;
        }

        private void StretchableTable_MouseLeave(object sender, MouseEventArgs e)
        {
            this.ReleaseMouseCapture();
            _isMoving = false;
            e.Handled = true;
        }
        #endregion Mouse Events

        // Manage the anchor mouse event for the component. Change the anchor/pivot points
        #region Anchor Mouse Events
        private void TopLeftAnchor_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (_isMoving)
                return;

            ChangeAnchor(AnchorMode.TopLeft);
        }

        private void TopRightAnchor_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (_isMoving)
                return;

            ChangeAnchor(AnchorMode.TopRight);
        }

        private void BottomRightAnchor_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (_isMoving)
                return;

            ChangeAnchor(AnchorMode.BottomRight);
        }

        private void BottomLeftAnchor_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (_isMoving)
                return;

            ChangeAnchor(AnchorMode.BottomLeft);
        }
        #endregion Anchor Mouse Events          
    }
}
