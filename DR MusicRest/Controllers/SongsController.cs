using Microsoft.AspNetCore.Mvc;
using DR_MusicRest.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DR_MusicRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongsController : ControllerBase
    {
        private readonly ISongsRepo _songsRepo;

        public SongsController(ISongsRepo songsRepo)
        {
            _songsRepo = songsRepo;
        }

        // GET: api/<SongsController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Song>> Get()
        {
            return Ok(_songsRepo.GetAll());
        }

        // GET api/<SongsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<SongsController>
        [HttpPost]
        public ActionResult<Song> Post([FromBody] Song songs)
        {
            if (songs == null)
            {
                return BadRequest() ;
            }
            else
            {
                _songsRepo.Add(songs);
                return CreatedAtAction(nameof(Get), new { id = songs.Id }, songs);
            }
                
        }

        // PUT api/<SongsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<SongsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
