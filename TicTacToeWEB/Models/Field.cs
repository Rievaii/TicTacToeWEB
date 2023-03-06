namespace TicTacToeWEB.Models
{
    public class Field
    {
        public int Id { get; set; }

        public Session Session { get; set; } 
        public int SessionId { get; set; }

        public char? Tile0 { get; set; } = '?';  public char? Tile1 { get; set; } = '?'; public char? Tile2 { get; set; } = '?';

        public char? Tile3 { get; set; } = '?';  public char? Tile4 { get; set; } = '?'; public char? Tile5 { get; set; } = '?';

        public char? Tile6 { get; set; } = '?';  public char? Tile7 { get; set; } = '?'; public char? Tile8 { get; set; } = '?';

        /* Поле крестики нолики 3х3
         * [0] [1] [2] 
         * [3] [4] [5]
         * [6] [7] [8]
        */
    }
}
