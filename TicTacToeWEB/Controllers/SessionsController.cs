using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using NuGet.Versioning;
using TicTacToeWEB.Handlers;
using TicTacToeWEB.Models.Data;

namespace TicTacToeWEB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionsController : ControllerBase
    {
        private readonly Context _context;
        Random random = new Random();

        public SessionsController(Context context)
        {
            _context = context;
        }

        // POST - Start new game
        [Route("api/StartGame")]
        [HttpPost]
        public async Task<ActionResult> StartNewGame(int player1Id, int player2Id)
        {
            if (player1Id != player2Id)
            {
                Session newGame = new Session()
                {
                    GameOver = false,
                    Field = new Field()
                    {
                        TotalMoves = 0
                    },
                    Player1Id = player1Id,
                    Player2Id = player2Id,
                    Turn = true
                };
                _context.Session.Add(newGame);
                await _context.SaveChangesAsync();
                //check if move => win => X won else return OK(you made a move)
                return Ok("Game created with id: " + newGame.SessionId);
            }
            else
            {
                return BadRequest("Player Id's cannot be same value");
            }
        }

        // PATCH - Make a move 
        [Route("api/MakeMove")]
        [HttpPatch]
        public async Task<IActionResult> MakeMove(int _SessionId, int _PlayerId, int _Cell)
        {
            
            var Game = _context.Session.SingleOrDefault(i => i.SessionId == _SessionId);
            var AssignedField = _context.Field.SingleOrDefault(i => i.SessionId == _SessionId);
            

            if (Game == null)
            {
                return NotFound("Game with such Id does not exist");
            }

            //Move can be done && No one yet won && cell is not occupied
            if (AssignedField.TotalMoves < 9 && Game.GameOver==false && !FieldHandler.isCellOccupied(GetFieldCells(2),_Cell))
            {
                if (Game.Turn == true && Game.Player1Id == _PlayerId)
                {
                    //place X
                    MapCells(_SessionId, _Cell, 'X');

                    AssignedField.TotalMoves++;

                    Game.Turn = false;

                    //isGameOver
                    if (AssignedField.TotalMoves == 9 | FieldHandler.CheckWin(GetFieldCells(2)) != "")
                    {
                        Game.GameOver = true;
                        return Ok("Gameover. " + FieldHandler.CheckWin(GetFieldCells(2)));
                    }
                    //check if this move wins the game
                    await _context.SaveChangesAsync();
                }
                else if (Game.Turn == false && Game.Player2Id == _PlayerId)
                {
                    //place O
                    MapCells(_SessionId, _Cell, 'O');

                    AssignedField.TotalMoves++;

                    Game.Turn = true;

                    if (AssignedField.TotalMoves == 9 | FieldHandler.CheckWin(GetFieldCells(2)) != "")
                    {
                        Game.GameOver = true;
                        return Ok("Gameover. " + FieldHandler.CheckWin(GetFieldCells(2)));
                    }
                    //check if this move wins the game
                    await _context.SaveChangesAsync();
                }
                else
                {
                    return BadRequest("It is not your turn");
                }
                return Ok("You made a move");
            }
            else
            {
                return BadRequest("Unable to make such move");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetGameById(int _SessionId)
        {
            var Game = await _context.Field.FindAsync(_SessionId);

            if (Game == null)
            {
                return NotFound();
            }
            
            return Ok(Game);
        }


        [ApiExplorerSettings(IgnoreApi = true)]
        public List<char> GetFieldCells(int _SessionId)
        {
            var CurrentSession = _context.Field.Where(i => i.SessionId == _SessionId);

            List<char> FieldCells = new List<char>();
            foreach (var Cell in CurrentSession)
            {
                FieldCells.Add(Cell.Tile0); FieldCells.Add(Cell.Tile1); FieldCells.Add(Cell.Tile2);
                FieldCells.Add(Cell.Tile3); FieldCells.Add(Cell.Tile4); FieldCells.Add(Cell.Tile5);
                FieldCells.Add(Cell.Tile6); FieldCells.Add(Cell.Tile7); FieldCells.Add(Cell.Tile8);
            }
            return FieldCells;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public void MapCells(int _SessionId, int Cell, char mark)
        {
            var AssignedCell = _context.Field.FirstOrDefault(i => i.SessionId == _SessionId);

            if(Cell <= 8)
            {
                switch(Cell)
                {
                    case 0: AssignedCell.Tile0 = mark; break; case 1: AssignedCell.Tile1 = mark; break; case 2: AssignedCell.Tile2 = mark; break;
                    case 3: AssignedCell.Tile3 = mark; break; case 4: AssignedCell.Tile4 = mark; break; case 5: AssignedCell.Tile5 = mark; break;
                    case 6: AssignedCell.Tile6 = mark; break; case 7: AssignedCell.Tile7 = mark; break; case 8: AssignedCell.Tile8 = mark; break;
                }
            }
        }
    }
}
