namespace TicTacToeWEB.Handlers
{
    public class FieldHandler : IFieldHandler
    {

        public static string CheckWin(List<char> FieldCells)
        {

            string winner = "";


            //Winning Condition For First Row
            if (FieldCells[0] == FieldCells[1] && FieldCells[1] == FieldCells[2])
            {
                winner += FieldCells[0];
                return winner;
            }
            //Winning Condition For Second Row
            else if (FieldCells[3] == FieldCells[4] && FieldCells[4] == FieldCells[5])
            {
                winner += FieldCells[3];
                return winner;
            }
            //Winning Condition For Third Row
            else if (FieldCells[5] == FieldCells[6] && FieldCells[6] == FieldCells[7])
            {
                winner += FieldCells[5];
                return winner;
            }
            //Winning Condition For First Column
            else if (FieldCells[0] == FieldCells[3] && FieldCells[3] == FieldCells[6])
            {
                winner += FieldCells[0];
                return winner;
            }
            //Winning Condition For Second Column 
            else if (FieldCells[1] == FieldCells[4] && FieldCells[4] == FieldCells[7])
            {
                winner += FieldCells[1];
                return winner;
            }
            //Winning Condition For Third Column
            else if (FieldCells[2] == FieldCells[5] && FieldCells[5] == FieldCells[8])
            {
                winner += FieldCells[2];
                return winner;
            }
            //Diagonals conditions
            else if (FieldCells[0] == FieldCells[4] && FieldCells[4] == FieldCells[8])
            {
                winner += FieldCells[0];
                return winner;
            }
            else if (FieldCells[2] == FieldCells[4] && FieldCells[4] == FieldCells[6])
            {
                winner += FieldCells[2];
                return winner;
            }

            else if (FieldCells[0] != '?' && FieldCells[1] != '?' && FieldCells[2] != '?' && FieldCells[3] != '?' && FieldCells[4] != '?' && FieldCells[5] != '?' && FieldCells[6] != '?' && FieldCells[7] != '?' && FieldCells[8] != '?')
            {
                //game is ended no one won
                winner = "Draw";
                return winner;
            }
            return winner;

        }

        public bool isCellOccupied(List<char> FieldGrid, int Cell)
        {
            if (Cell > 8) { throw new IndexOutOfRangeException(); }
            else
            {
                if (FieldGrid[Cell] != '?')
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }

        }
        //public List<char> GetFieldCells(int _SessionId)
        //{
        //    using (var _context = new Context())
        //    {
        //        var CurrentSession = _context.Field.Where(i => i.SessionId == _SessionId);

        //        List<char> FieldCells = new List<char>();
        //        foreach (var Cell in CurrentSession)
        //        {
        //            FieldCells.Add(Cell.Tile0); FieldCells.Add(Cell.Tile1); FieldCells.Add(Cell.Tile2);
        //            FieldCells.Add(Cell.Tile3); FieldCells.Add(Cell.Tile4); FieldCells.Add(Cell.Tile5);
        //            FieldCells.Add(Cell.Tile6); FieldCells.Add(Cell.Tile7); FieldCells.Add(Cell.Tile8);
        //        }
        //        return FieldCells;
        //    }
        //}
    }
}
