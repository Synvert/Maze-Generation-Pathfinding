using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using MazeGenAndPathFinding.Models;

namespace MazeGenAndPathFinding.Controls
{
    public class MazeViewer : Control
    {
        #region Dependency Properties

        #region MazeProperty

        public static readonly DependencyProperty MazeProperty = DependencyProperty.Register(
            "Maze", typeof(Maze), typeof(MazeViewer), new PropertyMetadata(default(Maze), MazePropertyChangedCallback));

        private static void MazePropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            ((MazeViewer)dependencyObject).OnMazeChanged((Maze)args.OldValue, (Maze)args.NewValue);
        }

        private void OnMazeChanged(Maze oldValue, Maze newValue)
        {
            if (oldValue != null)
            {
                oldValue.CellsChanged -= MazeOnCellsChanged;
            }
            if (newValue != null)
            {
                newValue.CellsChanged += MazeOnCellsChanged;
            }
            InvalidateVisual();
        }

        private void MazeOnCellsChanged(object sender, EventArgs args)
        {
            InvalidateVisual();
        }

        public Maze Maze
        {
            get { return (Maze)GetValue(MazeProperty); }
            set { SetValue(MazeProperty, value); }
        }

        #endregion
        
        #endregion

        #region Fields

        private readonly Pen _linePen;

        #endregion

        #region Constructor

        public MazeViewer()
        {
            _linePen = new Pen(new SolidColorBrush(Colors.Black), 1);
            _linePen.Brush.Freeze();
            _linePen.Freeze();
        }

        #endregion

        #region Methods

        /// <summary>
        /// When overridden in a derived class, participates in rendering operations that are directed by the layout system.
        /// The rendering instructions for this element are not used directly when this method is invoked, and are instead preserved for later asynchronous use by layout and drawing.
        /// </summary>
        /// <param name="drawingContext">The drawing instructions for a specific element. This context is provided to the layout system.</param>
        protected override void OnRender(DrawingContext drawingContext)
        {
            if (Maze == null)
            {
                return;
            }

            var cellWidth = ActualWidth / Maze.Width;
            var cellHeight = ActualHeight / Maze.Height;

            drawingContext.DrawRectangle(Background, null, new Rect(new Size(ActualWidth, ActualHeight)));

            // Draw outer walls manually because the method used for drawing interior walls leaves gaps.
            drawingContext.DrawLine(_linePen, new Point(0, 0), new Point(ActualWidth, 0));
            drawingContext.DrawLine(_linePen, new Point(ActualWidth, 0), new Point(ActualWidth, ActualHeight));
            drawingContext.DrawLine(_linePen, new Point(ActualWidth, ActualHeight), new Point(0, ActualHeight));
            drawingContext.DrawLine(_linePen, new Point(0, ActualHeight), new Point(0, -1)); // The -1 is to close a small gap in the top left.
            
            foreach (var cell in Maze.EnumerateCellsWithUniqueWalls())
            {
                var topLeft = new Point(cellWidth * cell.X, cellHeight * cell.Y);
                var topRight = new Point(cellWidth * cell.X + cellWidth, cellHeight * cell.Y);
                var bottomLeft = new Point(cellWidth * cell.X, cellHeight * cell.Y + cellHeight);
                var bottomRight = new Point(cellWidth * cell.X + cellWidth, cellHeight * cell.Y + cellHeight);

                if (cell.Walls[Direction.North])
                {
                    drawingContext.DrawLine(_linePen, topLeft, topRight);
                }
                if (cell.Walls[Direction.East])
                {
                    drawingContext.DrawLine(_linePen, topRight, bottomRight);
                }
                if (cell.Walls[Direction.South])
                {
                    drawingContext.DrawLine(_linePen, bottomRight, bottomLeft);
                }
                if (cell.Walls[Direction.West])
                {
                    drawingContext.DrawLine(_linePen, bottomLeft, topLeft);
                }
            }
        }

        #endregion
    }
}
