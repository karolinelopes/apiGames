using apiGames.InputModel;
using apiGames.ViewModel;
using apiGames.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace apiGames.Controllers.V1
{
    [Route("api/V1/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {

        private readonly IGameService _gameService;

        public GamesController(IGameService gameService) {
            _gameService = gameService;
        }

            /// <summary>
        /// Buscar todos os jogos de forma paginada
        /// </summary>
        /// <remarks>
        /// Não é possível retornar os jogos sem paginação
        /// </remarks>
        /// <param name="page">Indica qual página está sendo consultada. Mínimo 1</param>
        /// <param name="quantity">Indica a quantidade de reistros por página. Mínimo 1 e máximo 50</param>
        /// <response code="200">Retorna a lista de jogos</response>
        /// <response code="204">Caso não haja jogos</response> 

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameViewModel>>> GetGame([FromQuery, Range(1, int.MaxValue)] int page = 1 [FromQuery, Range(1, 50)], int quanity = 5) {
            var games = await _gameService.GetGame(page, quanity);

            if(games.Count() == 0) 
                return NoContent();

            return Ok(games);
        } 

        [HttpGet("{idGame:guid}")]
        public async Task<ActionResult<GameViewModel>> GetGameById(Guid idGame) {
            var game = await _gameService.GetGameById(idGame);

            if(game == null)
                return NoContent();

            return Ok(game);
        } 

        [HttpPost]
        public async Task<ActionResult<GameViewModel>> CreateGame([FromBody] GameInputModel gameInputGame) {

            try {
                var game = await _gameService.Create(gameInputGame);
                return Ok(game);
            }catch (Excepetion ex) {
                return UnprocessableEntity("Já existe esse jogo cadastrado!");
            }

            
        } 

        [HttpPut("{idGame:guid}")]
        public async Task<ActionResult> UpdateGame(Guid idGame, [FromBody] GameInputModel gameInputGame) {
            try {
                await _gameService.UpdateGame(idGame, gameInputGame);
                return Ok();
            } catch(GameNotFoundException ex) {
                return NotFound("Não existe esse jogo");
            }
            
        } 

        [HttpPatch("{idGame:guid}/price/{price:double}")]
        public Task<ActionResult> UpdateGame([FromBody] Guid idGame, double price) {
            try{
                await _gameService.UpdateGame(idGame, price);
                return Ok();

            }catch(GameNotCreateException ex) {
                return NotFound("Não existe esse jogo");
            }
            
        } 

        [HttpDelete("{idGame:guid}")]
        public Task<ActionResult> DeleteGame([FromBody] Guid idGame) {
           try{
                await _gameService.DeleteGame(idGame);
                return Ok();

            }catch(GameNotCreateException ex) {
                return NotFound("Não existe esse jogo");
            }
        } 
        
    }
}