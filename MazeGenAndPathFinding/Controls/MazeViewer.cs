using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using MazeGenAndPathFinding.Model.DataModels;

namespace MazeGenAndPathFinding.Controls
{
    [TemplatePart(Name = PartCanvasName, Type = typeof(Canvas))]
    public class MazeViewer : Control
    {
        #region Dependency Properties

        public static readonly DependencyProperty MazeProperty = DependencyProperty.Register(
            "Maze", typeof(Maze), typeof(MazeViewer), new PropertyMetadata(default(Maze)));

        public Maze Maze
        {
            get { return (Maze) GetValue(MazeProperty); }
            set { SetValue(MazeProperty, value); }
        }

        #endregion

        #region Fields

        public const string PartCanvasName = "PART_Canvas";

        private const double _cellWallThickness = 1;
        private Canvas _canvas;
        private SolidColorBrush _cellWallColorBrush;

        #endregion

        #region Constructor

        public MazeViewer()
        {
            _cellWallColorBrush = new SolidColorBrush(Colors.Black);
        }

        #endregion

        #region Methods

        /// <summary>
        /// When overridden in a derived class, is invoked whenever application code or internal processes call <see cref="M:System.Windows.FrameworkElement.ApplyTemplate" />.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _canvas = GetTemplateChild(PartCanvasName) as Canvas;
            if (_canvas == null)
            {
                throw new Exception($"{nameof(MazeViewer)} template must include a canvas control with the name {PartCanvasName}");
            }
        }

        /// <summary>
        /// When overridden in a derived class, participates in rendering operations that are directed by the layout system.
        /// The rendering instructions for this element are not used directly when this method is invoked, and are instead preserved for later asynchronous use by layout and drawing.
        /// </summary>
        /// <param name="drawingContext">The drawing instructions for a specific element. This context is provided to the layout system.</param>
        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            var cellWidth = ActualWidth/Maze.Width;
            var cellHeight = ActualHeight/Maze.Height;

            _canvas.Children.Clear();
            for (var x = 0; x < Maze.Width; x++)
            {
                for (var y = 0; y < Maze.Height; y++)
                {
                    var cell = new Border
                    {
                        BorderBrush = _cellWallColorBrush,
                        BorderThickness = GetCellWallThickness(Maze.Cells[x, y]),
                        Width = cellWidth,
                        Height = cellHeight
                    };
                    Canvas.SetLeft(cell, cellWidth * x);
                    Canvas.SetTop(cell, cellHeight* y);
                    _canvas.Children.Add(cell);
                }
            }
        }

        private Thickness GetCellWallThickness(Cell cell)
        {
            return new Thickness
            {
                Top = cell.Walls[Direction.North].IsBroken ? 0.0 : _cellWallThickness,
                Right = cell.Walls[Direction.East].IsBroken ? 0.0 : _cellWallThickness,
                Bottom = cell.Walls[Direction.South].IsBroken ? 0.0 : _cellWallThickness,
                Left = cell.Walls[Direction.West].IsBroken ? 0.0 : _cellWallThickness
            };
        }

        #endregion

    }
}
