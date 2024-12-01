using SkiaSharp;
using SkiaSharp.Views.Maui;
using SkiaSharp.Views.Maui.Controls;

namespace Sheet.Skia.Controls
{
    public partial class SheetView : Grid
    {
        private readonly SKCanvasView canvasView = new();
        public const string PromptSignalName = "Microsoft.Maui.Controls.SendPrompt";

        public SheetView()
        {
            canvasView.PaintSurface += CanvasView_PaintSurface;
            canvasView.Touch += CanvasView_Touch;
            RowDefinitions = [new RowDefinition { Height = GridLength.Star },];
            ColumnDefinitions = [new ColumnDefinition { Width = GridLength.Star },];
            this.Add(canvasView,0,0);
            InputTransparent = true;
        }

        private float _fontSize = 24;

        private void CanvasView_PaintSurface(object? sender, SkiaSharp.Views.Maui.SKPaintSurfaceEventArgs e)
        {
            if (sender is SKCanvasView)
            {
                var canvas = e.Surface.Canvas;
                canvas.Clear(SKColors.Transparent);

                var rectPaint = new SKPaint
                {
                    Color = SKColors.Gray,
                    Style = SKPaintStyle.Stroke,
                    StrokeWidth = 2
                };

                var textPaint = new SKPaint
                {
                    Color = SKColors.Black,
                    TextSize = _fontSize,
                    IsAntialias = true
                };
                int y = 0;
                var header = ItemsSource.Header;

                int header_x = 0;
                foreach (var column in header.Columns)
                {
                    header_x = column.Index == 0 ? 0 : header_x + header.Columns[column.Index - 1].Width;

                    int width = header_x + header.Columns[column.Index].Width;
                    int height = y + header.Height;

                    SKRect _textRect = new(header_x, y, width, height);
                    canvas.DrawRect(_textRect, rectPaint);

                    var textBounds = new SKRect();
                    textPaint.MeasureText(column.Content, ref textBounds);

                    // Calculate position to center the text
                    float _x = _textRect.Left + (_textRect.Width - textBounds.Width) / 2;
                    float _y = _textRect.Top + (_textRect.Height + textBounds.Height) / 2;

                    canvas.DrawText(column.Content, _x, _y, textPaint);
                }

                foreach (var row in ItemsSource.Rows)
                {
                    y = row.Index == 0 ? 0 : y +row.Height;
                    int x = 0;
                    foreach (var cell in row.Cells)
                    {
                        x = cell.Index == 0 ? 0 : x + header.Columns[cell.Index-1].Width;

                        int width = x + header.Columns[cell.Index].Width;
                        int height = y + row.Height;

                        SKRect _textRect = new(x, y, width, height);
                        canvas.DrawRect(_textRect, rectPaint);

                        var textBounds = new SKRect();
                        textPaint.MeasureText(cell.Content, ref textBounds);

                        // Calculate position to center the text
                        float _x = _textRect.Left + (_textRect.Width - textBounds.Width) / 2;
                        float _y = _textRect.Top + (_textRect.Height + textBounds.Height) / 2;

                        canvas.DrawText(cell.Content, _x, _y, textPaint);
                    }
                }
            }
        }

        static void OnBindablePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((SheetView)bindable).canvasView.InvalidateSurface();
        }

        private void CanvasView_Touch(object sender, SKTouchEventArgs e)
        {
            Console.WriteLine($"X {e.Location.X} / Y {e.Location.Y}");

            //if (e.ActionType == SKTouchAction.Pressed && _textRect.Contains(e.Location))
            //{
            //    // Start editing
            //    _isEditing = true;
            //    DisplayPromptAsync("Edit Text", "Enter new text:", "OK", "Cancel", _text)
            //        .ContinueWith(task =>
            //        {
            //            if (task.Result != null)
            //            {
            //                _text = task.Result;
            //                CanvasView.InvalidateSurface();
            //            }
            //            _isEditing = false;
            //        }, TaskScheduler.FromCurrentSynchronizationContext());
            //}

            e.Handled = true;
        }

        #region RowStroke
        public static readonly BindableProperty RowStrokeProperty = BindableProperty.Create("RowStroke", typeof(int), typeof(SheetView), 5, propertyChanged: OnBindablePropertyChanged);

        public int RowStroke
        {
            get => (int)GetValue(RowStrokeProperty);
            set => SetValue(RowStrokeProperty, value);
        }
        #endregion

        #region RowStrokeColor
        public static readonly BindableProperty RowStrokeColorProperty = BindableProperty.Create("RowStrokeColor", typeof(SKColor), typeof(SheetView), SKColors.Blue, propertyChanged: OnBindablePropertyChanged);

        public SKColor RowStrokeColor
        {
            get => (SKColor)GetValue(RowStrokeColorProperty);
            set => SetValue(RowStrokeColorProperty, value);
        }

        #endregion

        #region RowFontSize
        public static readonly BindableProperty RowFontSizeProperty = BindableProperty.Create("RowFontSize", typeof(int), typeof(SheetView), 12, propertyChanged: OnBindablePropertyChanged);

        public int RowFontSize
        {
            get => (int)GetValue(RowFontSizeProperty);
            set => SetValue(RowFontSizeProperty, value);
        }

        #endregion

        #region RowTypeface
        public static readonly BindableProperty RowTypefaceProperty = BindableProperty.Create("RowTypeface", typeof(string), typeof(SheetView), "Arial", propertyChanged: OnBindablePropertyChanged);

        public string RowTypeface
        {
            get => (string)GetValue(RowTypefaceProperty);
            set => SetValue(RowTypefaceProperty, value);
        }

        #endregion

        #region ColumnStroke
        public static readonly BindableProperty ColumnStrokeProperty = BindableProperty.Create("ColumnStroke", typeof(int), typeof(SheetView), 5, propertyChanged: OnBindablePropertyChanged);

        public int ColumnStroke
        {
            get => (int)GetValue(ColumnStrokeProperty);
            set => SetValue(ColumnStrokeProperty, value);
        }
        #endregion

        #region ColumnStrokeColor
        public static readonly BindableProperty ColumnStrokeColorProperty = BindableProperty.Create("ColumnStrokeColor", typeof(SKColor), typeof(SheetView), SKColors.Blue, propertyChanged: OnBindablePropertyChanged);

        public SKColor ColumnStrokeColor
        {
            get => (SKColor)GetValue(ColumnStrokeColorProperty);
            set => SetValue(ColumnStrokeColorProperty, value);
        }
        #endregion

        #region ColumnFontSize
        public static readonly BindableProperty ColumnFontSizeProperty = BindableProperty.Create("ColumnFontSize", typeof(int), typeof(SheetView), 16, propertyChanged: OnBindablePropertyChanged);

        public int ColumnFontSize
        {
            get => (int)GetValue(ColumnFontSizeProperty);
            set => SetValue(ColumnFontSizeProperty, value);
        }

        #endregion

        #region ColumnTypeface
        public static readonly BindableProperty ColumnTypefaceProperty = BindableProperty.Create("ColumnTypeface", typeof(string), typeof(SheetView), "Arial", propertyChanged: OnBindablePropertyChanged);

        public string ColumnTypeface
        {
            get => (string)GetValue(ColumnTypefaceProperty);
            set => SetValue(ColumnTypefaceProperty, value);
        }

        #endregion

        #region BorderStroke
        public static readonly BindableProperty BorderStrokeProperty = BindableProperty.Create("BorderStroke", typeof(int), typeof(SheetView), 5, propertyChanged: OnBindablePropertyChanged);

        public int BorderStroke
        {
            get => (int)GetValue(BorderStrokeProperty);
            set => SetValue(BorderStrokeProperty, value);
        }
        #endregion

        #region BorderStrokeColor
        public static readonly BindableProperty BorderStrokeColorProperty = BindableProperty.Create("BorderStrokeColor", typeof(SKColor), typeof(SheetView), SKColors.Blue, propertyChanged: OnBindablePropertyChanged);

        public SKColor BorderStrokeColor
        {
            get => (SKColor)GetValue(BorderStrokeColorProperty);
            set => SetValue(BorderStrokeColorProperty, value);
        }
        #endregion

        #region ItemsSource
        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(
            nameof(ItemsSource), typeof(SheetList), typeof(SheetView), null, propertyChanged: OnBindablePropertyChanged);

        public SheetList ItemsSource
        {
            get => (SheetList)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }
        #endregion

        public Task<string> DisplayPromptAsync(string title, string message, string accept = "OK", string cancel = "Cancel", string placeholder = null, int maxLength = -1, Keyboard keyboard = default(Keyboard), string initialValue = "")
        {
            var args = new Microsoft.Maui.Controls.Internals.PromptArguments(title, message, accept, cancel, placeholder, maxLength, keyboard, initialValue);
            return args.Result.Task;
        }

        #region Private Methods
        private SKPaint GetRowPaint()
        {
            return new SKPaint
            {
                TextSize = RowFontSize,
                Typeface = SKTypeface.FromFamilyName(RowTypeface),
                IsAntialias = true
            };
        }

        private static SKPaint GetColumnPaint(SheetView sheet)
        {
            return new SKPaint
            {
                TextSize = sheet.ColumnFontSize,
                Typeface = SKTypeface.FromFamilyName(sheet.ColumnTypeface),
                IsAntialias = true
            };
        }

        #endregion
    }
}
