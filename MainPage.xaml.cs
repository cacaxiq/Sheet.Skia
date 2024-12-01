using Sheet.Skia.Controls;
using Cell = Sheet.Skia.Controls.Cell;

namespace Sheet.Skia
{
    public partial class MainPage : ContentPage
    {
        public SheetList SheetList { get; } =
       new()
       {
           Header = new Header(Columns: [new Column(0, "Name",500), new Column(1, "Age",200), new Column(2, "City",350),]),
           Rows = [
            new Row(Index: 1, Cells: [ new Cell(0,"Julie"),new Cell(1,"33"),new Cell(2,"New York"), ]),
            new Row(Index: 2, Cells: [ new Cell(0,"Allan"),new Cell(1,"34"),new Cell(2,"Chicago"), ]),
            new Row(Index: 3, Cells: [ new Cell(0,"Michel"),new Cell(1,"35"),new Cell(2,"San Franciso"), ]),
            new Row(Index: 4, Cells: [ new Cell(0,"Carl"),new Cell(1,"36"),new Cell(2,"Miami"), ]),
            new Row(Index: 5, Cells: [ new Cell(0,"Michel"),new Cell(1,"37"),new Cell(2,"Los Angeles") ]),
            ]
       };

        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;
        }
    }
}
