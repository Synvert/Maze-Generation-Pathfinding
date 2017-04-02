using System;
using System.Collections.Generic;
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
            if (!CheckAccess())
            {
                Dispatcher.Invoke(() => OnMazeChanged(oldValue, newValue));
                return;
            }

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
            if(!CheckAccess())
            {
                Dispatcher.Invoke(() => MazeOnCellsChanged(sender, args));
                return;
            }
            InvalidateVisual();
        }

        public Maze Maze
        {
            get { return (Maze)GetValue(MazeProperty); }
            set { SetValue(MazeProperty, value); }
        }

        #endregion

        #region LockAspectRatio

        public static readonly DependencyProperty LockAspectRatioProperty = DependencyProperty.Register(
            "LockAspectRatio", typeof(bool), typeof(MazeViewer), new PropertyMetadata(default(bool), LockAspectRatioChangedCallback));

        public bool LockAspectRatio
        {
            get { return (bool)GetValue(LockAspectRatioProperty); }
            set { SetValue(LockAspectRatioProperty, value); }
        }

        private static void LockAspectRatioChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            ((MazeViewer)dependencyObject).InvalidateVisual();
        }
        
        #endregion

        #endregion

        #region Fields

        private readonly Pen _linePen;
        private readonly Dictionary<Color, SolidColorBrush> _brushCache = new Dictionary<Color, SolidColorBrush>();

        #endregion

        #region Constructor

        public MazeViewer()
        {
            _linePen = new Pen(GetBrush(Colors.Black), 1);
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

            if (LockAspectRatio)
            {
                cellWidth = Math.Min(cellWidth, cellHeight);
                cellHeight = Math.Min(cellWidth, cellHeight);
            }

            drawingContext.DrawRectangle(Background, null, new Rect(new Size(ActualWidth, ActualHeight)));
            
            foreach (var cell in Maze.Cells)
            {
                var topLeft = new Point(cellWidth * cell.X, cellHeight * cell.Y);
                var bottomRight = new Point(cellWidth * cell.X + cellWidth, cellHeight * cell.Y + cellHeight);
                
                drawingContext.DrawRectangle(GetBrush(cell.Background), null, new Rect(topLeft, bottomRight));
            }

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

            var renderWidth = LockAspectRatio ? cellWidth * Maze.Width : ActualWidth;
            var renderHeight = LockAspectRatio ? cellHeight * Maze.Height : ActualHeight;

            // Draw outer walls manually because the method used for drawing interior walls leaves gaps.
            drawingContext.DrawLine(_linePen, new Point(0, 0), new Point(renderWidth, 0));
            drawingContext.DrawLine(_linePen, new Point(renderWidth, 0), new Point(renderWidth, renderHeight));
            drawingContext.DrawLine(_linePen, new Point(renderWidth, renderHeight), new Point(0, renderHeight));
            drawingContext.DrawLine(_linePen, new Point(0, renderHeight), new Point(0, -1)); // The -1 is to close a small gap in the top left.
        }

        private SolidColorBrush GetBrush(Color color)
        {
            if (_brushCache.ContainsKey(color))
            {
                return _brushCache[color];
            }

            var brush = new SolidColorBrush(color);
            brush.Freeze();
            _brushCache[color] = brush;
            return brush;
        }

        #endregion
    }
}
