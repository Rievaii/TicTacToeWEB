﻿
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
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

        private FieldHandler fieldHandler = new FieldHandler();
        public SessionsController(Context context)
        {
            _context = context;
        }

        // POST: api/Sessions - Start new game
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

                return Ok("Game created with id: " + newGame.SessionId);
            }
            else
            {
                return BadRequest("Player Id's cannot be same value");
            }
        }
        [Route("api/MakeMove")]
        [HttpPatch]
        public async Task<IActionResult> MakeMove(int _SessionId, int _PlayerId, int _Cell)
        {

            var Game = _context.Session.SingleOrDefault(i => i.SessionId == _SessionId);
            var AssignedField = _context.Field.SingleOrDefault(i => i.SessionId == _SessionId);
            var Cell = _context.Field.Where(s=>s.SessionId == _SessionId).Select(x => x.)
            
            if (Game == null)
            {
                return NotFound("Game with such Id does not exist");
            }

            //Move can be done && No one yet won && cell is not occupied
            if (AssignedField.TotalMoves < 9 && Game.GameOver==false && fieldHandler.isCellOccupied(GetFieldCells(_SessionId), _Cell))
            {
                //check who s turn 
                if (Game.Turn == true && Game.Player1Id == _PlayerId)
                {
                    //place X
                    
                    

                    //totalmoves++;
                    AssignedField.TotalMoves++;

                    //turn false
                    Game.Turn = false;

                    //isGameOver
                    if (AssignedField.TotalMoves == 9)
                    {
                        //check win 
                        Game.GameOver = true;
                    }

                    await _context.SaveChangesAsync();
                }
                else if (Game.Turn == false && Game.Player2Id == _PlayerId)
                {
                    //place O
                    //Map values
                    AssignedField.Tile0 = 'O';

                    //totalmoves++;
                    AssignedField.TotalMoves++;

                    //turn false
                    Game.Turn = true;

                    //isGameOver
                    if(AssignedField.TotalMoves == 9)
                    {
                        //check win
                        Game.GameOver = true;
                    }

                    await _context.SaveChangesAsync();
                }
                else
                {
                    return BadRequest("It is not your turn");
                }
                //save changes
                return Ok("You made a move");
            }
            else
            {
                return BadRequest("Unable to make such move");
            }
        }
        //GET LAST MOVE IN GAME method
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
    }
}
