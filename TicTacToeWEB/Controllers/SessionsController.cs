using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using TicTacToeWEB;
using TicTacToeWEB.Models;

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

        // POST: api/Sessions - Start new game
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
                    GameId = random.Next(0, 99999),
                    Turn = true
                };
                _context.Session.Add(newGame);
                await _context.SaveChangesAsync();

                return Ok("Game created with id: " + newGame.GameId);
            }
            else
            {
                return BadRequest("Player Id's cannot be same value");
            }
        }

        [HttpPatch]
        public async Task<IActionResult> MakeMove(int GameId, int PlayerId, int Cell)
        {
            //=> you made a move
            //=> cell is already busy 

            var Game = _context.Session.Where(i => i.GameId == GameId);
            
            if (Game == null)
            {
                return NotFound("Game with such Id does not exist");
            }
            
            if (GameAvailable(GameId) && Game.Any(q => q.Field.TotalMoves < 9))
            {
                //check if it is the last move -> 9 move
                //check who's turn => place such char into requered cell if it is not busy 

                if (Game.Any(j => j.Turn == true))
                {
                    //place X
                    //HTTP PATCH = make move
                    //totalmoves++;

                }
                if (Game.Any(j => j.Turn == false))
                {
                    //place O
                    //HTTP PATCH = make move
                    //totalmoves++;
                }
                return Ok("You made a move");
            }
            else 
            {
                //else if
                //check if cell is busy of unavailable
                return BadRequest();
            }
        }
        

        private bool GameAvailable(int Gameid)
        {
            if (_context.Session.Where(g => g.GameId == Gameid).Any(i => i.GameOver == false))
            {

                return true;
            }
            else
            {
                return false; 
            }
        }
        [HttpGet]
        public IActionResult isGameWon(int _SessionId)
        {
            //GAME CAN ALREADY BE WON 
            /* get cells != '?' and if this cells have the same char on positions => GameOver
             * 0 1 2
             * 3 4 5 
             * 5 6 7 
             * 
             * (0,1,2) (3,4,5) (5,6,7)
             * (0,3,5) (1,4,6) (2,5,7)
             * (0,4,7) (2,4,5)
             * 
            */

            //get players => whos was the last turn and condition above approved => won the game 
            //get all tiles where sesseion id is the same as gameid session id 
            var CurrentSession = _context.Field.Where(i => i.SessionId == _SessionId);
            List<char?> FieldCells = new List<char?>();
            var news = "";
            foreach(var Cell in CurrentSession)
            {
                FieldCells.Add(Cell.Tile0);
                FieldCells.Add(Cell.Tile1);
                FieldCells.Add(Cell.Tile2);
                FieldCells.Add(Cell.Tile3);
                FieldCells.Add(Cell.Tile4);
                FieldCells.Add(Cell.Tile5);
                FieldCells.Add(Cell.Tile6);
                FieldCells.Add(Cell.Tile7);
                FieldCells.Add(Cell.Tile8);

                //... others


            }
            return Ok(news);
        }
    }
}
