using StretchableTest.Components;
using StretchableTest.Extensions;
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

namespace StretchableTest.Controls
{
    /// <summary>
    /// Container class manager for pair Table/Chairs
    /// </summary>
    /// 
    /// Rappresenta una singola scena per il task, puo' essere scalata
    /// gestendo una collezione di coppie Table/Chairs
    public partial class SpaceManager : UserControl
    {
        StretchableTable _table;
        int _chairOffset = 50;

        public SpaceManager()
        {
            InitializeComponent();

            Setup();
        }

        /// <summary>
        /// Init method.
        /// </summary>
        private void Setup()
        {
            _table = ProductFactory.GetTable();
            _table.UpdateViewEvent += Table_UpdateViewEvent;

            Board.AddAndPlace(_table, 250, 250);
        }

        /// <summary>
        /// Repaint delegate for chair painting
        /// </summary>
        /// 
        /// L'update della gui in questo modo non rappresenta una soluzione ottimale in termini computazionali,
        /// in questo caso risulta essere un compromesso sul tempo di sviluppo del task e la piattaforma utilizzata
        /// in quanto WPF non e' pensato per questo genere di applicazioni. 
        /// Una versione più adeguata alla piattaforma sarebbe quella di richiedre l'update alla fine della sequenza
        /// e non durante lo spostamento.
        /// 
        private void Table_UpdateViewEvent(object sender, RoutedEventArgs e)
        {
            ClearChairs();
            PaintChairs();
        }

        /// <summary>
        /// Remove the chairs from the table
        /// </summary>
        private void ClearChairs()
        {
            var elements = Board.Children.OfType<Chair>().ToList();
            foreach(var element in elements)
            {
                Board.Children.Remove(element);
            }
        }

        /// <summary>
        /// Paint the chairs in their optimal position.
        /// </summary>
        private void PaintChairs()
        { 
            int hspace = (int)_table.Width / _chairOffset;
            int vspace = (int)_table.Height / _chairOffset;
            Chair chairTop;
            Chair chairBottom;
            Chair chairLeft;
            Chair chairRight;
            double x = 0;
            double y = 0;

            double hOffset = _table.Width / hspace - _chairOffset;
            double vOffset = _table.Height / vspace - _chairOffset;

            for (int i=0; i<hspace; i++)
            {
                chairTop = ProductFactory.GetChair();
                chairBottom = ProductFactory.GetChair();

                x = _table.Left + (i * _chairOffset) + (_chairOffset / 2) - (chairTop.Width / 2) + (i+2) * hOffset / 2;
                y = _table.Top - chairTop.Height;

                Board.AddAndPlace(chairTop, x, y);

                x = _table.Left + (i * _chairOffset) + (_chairOffset / 2) - (chairBottom.Width / 2) + (i+2) * hOffset / 2;
                y = _table.Bottom;

                Board.AddAndPlace(chairBottom, x, y);
            }

            for (int i = 0; i < vspace; i++)
            {
                chairLeft = ProductFactory.GetChair();
                chairRight = ProductFactory.GetChair();

                x = _table.Left - chairLeft.Width;
                y = _table.Top + (i * _chairOffset) + (_chairOffset / 2) - (chairLeft.Height / 2) + (i+1) * vOffset / 2;

                Board.AddAndPlace(chairLeft, x, y);

                x = _table.Right;
                y = _table.Top + (i * _chairOffset) + (_chairOffset / 2) - (chairRight.Height / 2) + (i+1) * vOffset / 2;

                Board.AddAndPlace(chairRight, x, y);
            }
        }
    }
}
