using SkiaSharp;

namespace Sheet.Skia.Controls
{
    public class Cell(int index, string content)
    {
        public int Index { get; } = index;
        public string Content { get; } = content;
        public int Span { get; set; } = 1;

        public SKRect rect = new();

        public bool Selected { get; set; }

        public void SetRect(SKRect value)
        {
            rect = value;
        }
    }

    public record Column(int Index, string Content, int Width = 200);
    public record Row(int Index, List<Cell> Cells, int Height = 50);
    public record Header(List<Column> Columns, int Height = 50);

    public class SheetList
    {
        public Header? Header { get; set; } = null;
        public List<Row> Rows { get; set; } = [];
    }
}
