using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicTacToeWEB.Models.Data
{
    public class Session
    {
        public int SessionId { get; set; }

        public bool GameOver { get; set; }

        [Required]
        public Field Field { get; set; }

        [Required]
        public int Player1Id { get; set; }

        [Required]
        public int Player2Id { get; set; }

        //очередность хода true - крестик, false - нолик
        [Required]
        public bool Turn { get; set; }


    }
}
