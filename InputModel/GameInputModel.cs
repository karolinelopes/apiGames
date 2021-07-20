using System;
using System.ComponentModel.DataAnnotation;

namespace apiGames.InputModel
{
    public class GameInputModel
    {
        [Required]
        [StringLegth(100, MinimumLength = 3, ErrorMessage = "O nome do jogo pode conter enter entre 3 e 100 caracteres")]
        public string Name {get;set;}

        [Required]
        [StringLegth(100, MinimumLength = 3, ErrorMessage = "O nome da produtora pode conter enter entre 3 e 100 caracteres")]
        public string Producer {get; set;}

        [Required]
        [Range(1, 1000, ErrorMessage = "O preço do jogo deve ser no minímo de 1 real e no máximo de 1000 reais")]
        public double Price {get; set;}

    }
}