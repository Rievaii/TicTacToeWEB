using System.ComponentModel.DataAnnotations.Schema;

namespace TicTacToeWEB.Models.Data
{
    /* Поле крестики нолики 3х3
         * [0] [1] [2] 
         * [3] [4] [5]
         * [6] [7] [8]
    */
    public class Field
    {
       
        public int Id { get; set; }

        public Session? Session { get; set; }

        public int SessionId { get; set; }

        public int TotalMoves { get; set; }

        public char Tile0 { get; set; } = '0'; public char Tile1 { get; set; } = '1'; public char Tile2 { get; set; } = '2';

        public char Tile3 { get; set; } = '3'; public char Tile4 { get; set; } = '4'; public char Tile5 { get; set; } = '5';

        public char Tile6 { get; set; } = '6'; public char Tile7 { get; set; } = '7'; public char Tile8 { get; set; } = '8';

    }
}
