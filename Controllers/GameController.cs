using AutoMapper;
using GamehubAPI.Data;
using GamehubAPI.Model;
using GamehubAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GamehubAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {

        private readonly ILogger<GameController> _logger;
        private readonly IGameRepository _gameRepository;
        private APIResponse _apiResponse;
    
        public GameController(ILogger<GameController> logger,
           IGameRepository gameRepository)
        {
            _logger = logger;
            _gameRepository = gameRepository;
             _apiResponse = new();
        }
        [HttpGet]
        [Route("All", Name = "GetAllGames")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //[EnableCors(PolicyName = "AllowOnlyMicrosoft")]
        //[AllowAnonymous]
        public async Task<ActionResult<APIResponse>> GetGamesAsync([FromQuery]PaginationParams paginationParams)
        {
            try
            {
                _logger.LogInformation("GetGames method started");
                var games = await _gameRepository.GetAllAsync(paginationParams);

                _apiResponse.Data = games;
                _apiResponse.Status = true;
                _apiResponse.StatusCode = HttpStatusCode.OK;
                //OK - 200 - Success
                _logger.LogInformation("Successfully retrieved games: {@game}", games);
                return Ok(_apiResponse);
            }
            catch (Exception ex)
            {
                _apiResponse.Errors.Add(ex.Message);
                _apiResponse.StatusCode = HttpStatusCode.InternalServerError;
                _apiResponse.Status = false;
                return _apiResponse;
            }
        }

        [HttpGet]
        [Route("{id:int}", Name = "GetGameById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //[DisableCors]
        public async Task<ActionResult<APIResponse>> GetGameByIdAsync(int id)
        {
            try
            {
                //BadRequest - 400 - Badrequest - Client error
                if (id <= 0)
                {
                    _logger.LogWarning("Bad Request");
                    return BadRequest();
                }

                var game = await _gameRepository.GetAsync(game => game.Id == id);
                //NotFound - 404 - NotFound - Client error
                if (game == null)
                {
                    _logger.LogError("Game not found with given Id:{@id}",id);
                    return NotFound($"The Game with id {id} not found");
                }

                _apiResponse.Data = game;
                _apiResponse.Status = true;
                _apiResponse.StatusCode = HttpStatusCode.OK;
                _logger.LogInformation("Successfully retrieved game: {@game}", game);
                //OK - 200 - Success
                return Ok(_apiResponse);
            }
            catch (Exception ex)
            {
                _apiResponse.Errors.Add(ex.Message);
                _apiResponse.StatusCode = HttpStatusCode.InternalServerError;
                _apiResponse.Status = false;
                _logger.LogInformation("Exception occured: {@message}", ex.Message);
                _logger.LogInformation("Api response details: {@details}", _apiResponse);
                return _apiResponse;
            }

        }
        [Route("GetGameByTitle")]
        [HttpGet]
       
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> GetGameByTitleAsync(string title)
        {
            try
            {
                //BadRequest - 400 - Badrequest - Client error
                if (string.IsNullOrEmpty(title))
                    return BadRequest();

                var game = await _gameRepository.GetAsync(game => game.Title.ToLower().Contains(title.ToLower()));
                //NotFound - 404 - NotFound - Client error
                if (game == null)
                    _logger.LogError("Game not found with given title:{@title}", title);
                return NotFound($"The game with name {title} not found");

                _apiResponse.Data = game;
                _apiResponse.Status = true;
                _apiResponse.StatusCode = HttpStatusCode.OK;
                //OK - 200 - Success
                _logger.LogInformation("Successfully retrieved game: {@game}", game);
                return Ok(_apiResponse);
            }
            catch (Exception ex)
            {
                _apiResponse.Errors.Add(ex.Message);
                _apiResponse.StatusCode = HttpStatusCode.InternalServerError;
                _apiResponse.Status = false;
                _logger.LogInformation("Exception occured: {@message}", ex.Message);
                _logger.LogInformation("Api response details: {@details}", _apiResponse);
                return _apiResponse;
            }

        }
        [HttpGet]
        [Route("GetGameByGenre")]
      
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> GetGameByGenreAsync(string genre)
        {
            try
            {
                //BadRequest - 400 - Badrequest - Client error
                if (string.IsNullOrEmpty(genre))
                    return BadRequest();

                var game = await _gameRepository.GetAsync(game => game.Genre.ToLower().Contains(genre.ToLower()));
                //NotFound - 404 - NotFound - Client error
                if (game == null)
                    return NotFound($"The game with Genre {genre} not found");

                _apiResponse.Data = game;
                _apiResponse.Status = true;
                _apiResponse.StatusCode = HttpStatusCode.OK;
                //OK - 200 - Success
                return Ok(_apiResponse);
            }
            catch (Exception ex)
            {
                _apiResponse.Errors.Add(ex.Message);
                _apiResponse.StatusCode = HttpStatusCode.InternalServerError;
                _apiResponse.Status = false;
                return _apiResponse;
            }

        }

        [HttpPost]
        [Route("Create")]
        //api/game/create
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<APIResponse>> CreateGameAsync([FromBody] Game dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (dto == null)
                    return BadRequest();

                          

                var Gameaftercreation = await _gameRepository.CreateAsync(dto);

                dto.Id = Gameaftercreation.Id;

                _apiResponse.Data = dto;
                _apiResponse.Status = true;
                _apiResponse.StatusCode = HttpStatusCode.OK;
                //Status - 201
                //New game details
                return CreatedAtRoute("GetGameById", new { id = dto.Id }, _apiResponse);
            }
            catch (Exception ex)
            {
                _apiResponse.Errors.Add(ex.Message);
                _apiResponse.StatusCode = HttpStatusCode.InternalServerError;
                _apiResponse.Status = false;
                return _apiResponse;
            }

        }

        [HttpPut]
        [Route("Update")]
        //api/game/update
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<APIResponse>> UpdateGameAsync([FromBody] Game dto)
        {
            try
            {
                if (dto == null || dto.Id <= 0)
                    BadRequest();

                var existingGame = await _gameRepository.GetAsync(game => game.Id == dto.Id, true);

                if (existingGame == null)
                    return NotFound();

              

                await _gameRepository.UpdateAsync(dto);

                return NoContent();
            }
            catch (Exception ex)
            {
                _apiResponse.Errors.Add(ex.Message);
                _apiResponse.StatusCode = HttpStatusCode.InternalServerError;
                _apiResponse.Status = false;
                return _apiResponse;
            }
        }

       


        [HttpDelete("Delete/{id}", Name = "DeleteGameById")]
        //api/game/delete/1
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<APIResponse>> DeleteGameAsync(int id)
        {
            try
            {
                //BadRequest - 400 - Badrequest - Client error
                if (id <= 0)
                    return BadRequest();

                var game = await _gameRepository.GetAsync(game => game.Id == id);
                //NotFound - 404 - NotFound - Client error
                if (game == null)
                    return NotFound($"The game with id {id} not found");

                await _gameRepository.DeleteAsync(game);
                _apiResponse.Data = true;
                _apiResponse.Status = true;
                _apiResponse.StatusCode = HttpStatusCode.OK;
                //OK - 200 - Success
                return Ok(_apiResponse);
            }
            catch (Exception ex)
            {
                _apiResponse.Errors.Add(ex.Message);
                _apiResponse.StatusCode = HttpStatusCode.InternalServerError;
                _apiResponse.Status = false;
                return _apiResponse;
            }

        }



    }
}
