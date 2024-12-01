namespace Sheet.Skia.Controls
{
    public record Cell(int Index, string? Content, int Span = 1);
    public record Column(int Index, string? Content, int Width = 200);
    public record Row(int Index, List<Cell> Cells, int Height = 50);
    public record Header(List<Column> Columns, int Height = 50);

    public class SheetList
    {
        public Header? Header { get; set; } = null;
        public List<Row> Rows { get; set; } = [];
    }
}
