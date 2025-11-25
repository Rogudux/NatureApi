using System.Text.Json;
using LibraryAPI.models.DTOS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpenAI.Chat;

namespace NatureAPi.Controllers
{
    [Route("api/trails")]
    [ApiController]
    public class TrailController : ControllerBase
    {
        private readonly NatureDBContext _context;
        private readonly IConfiguration _configuration;

        public TrailController(NatureDBContext context, IConfiguration config)
        {
            _context = context;
            _configuration = config;
        }

        // GET: api/trails?difficulty=Easy
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TrailDTO>>> GetTrails([FromQuery] string? difficulty)
        {
            var query = _context.Trail // <-- ajusta si tu DbSet se llama distinto
                .AsNoTracking()
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(difficulty))
            {
                query = query.Where(t => t.Difficulty == difficulty);
            }

            var trailsDto = await query
                .Select(t => new TrailDTO
                {
                    Name = t.Name,
                    DistanceKm = t.DistanceKm,
                    EstimatedTimeMinutes = t.EstimatedTimeMinutes,
                    Difficulty = t.Difficulty,
                    Path = t.Path,
                    IsLoop = t.IsLoop
                })
                .ToListAsync();

            return Ok(trailsDto);
        }
        
        [HttpGet("ai-analyze")]
        public async Task<ActionResult> AnalyzeTrails(int id)
        {
            //obtener api key
            var openAIKey = _configuration["OpenAIKey"];
            var client = new ChatClient(model:"gpt-5-mini",apiKey: openAIKey);
           
            //obtienene datos
            var trails = await _context.Trail
                .ToListAsync();
            var summary = trails.Select(o => new
            {
                o.Id,
                o.Difficulty,
                o.DistanceKm,
                o.EstimatedTimeMinutes,
                o.IsLoop,
                o.Name
                
            });
            var jsonData = JsonSerializer.Serialize(summary);
            //se hace el prompt
            var prompt = Prompts.GenerateTrailsPrompt(jsonData);
            
            var result = await client.CompleteChatAsync(
                new UserChatMessage(prompt));
            var response = result.Value.Content[0].Text;
            return Ok(response);
        }
    }
}