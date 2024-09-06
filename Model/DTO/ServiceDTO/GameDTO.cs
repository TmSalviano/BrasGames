using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrasGames.Model.ServiceModels;

namespace BrasGames.Model.DTO.ServiceDTO
{
    public class GameDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? AgeRestriction { get; set; }
        public string? Genre { get; set; }
        
        public float Price { get; set; }

        public GameDTO(Game game) {
            Id = game.Id;
            Name = game.Name;
            AgeRestriction = game.AgeRestriction;
            Genre = game.Genre;
            Price = game.Price;
        }
    }
}