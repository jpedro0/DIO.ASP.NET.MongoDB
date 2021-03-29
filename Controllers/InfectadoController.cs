using DIO.ASP.NET.MongoDB.Data.Collections;
using DIO.ASP.NET.MongoDB.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace DIO.ASP.NET.MongoDB.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InfectadoController : ControllerBase
    {
        private readonly IMongoCollection<Infectado> _infectadosCollection;

        public InfectadoController(Data.MongoDB mongoDB) => 
            (_infectadosCollection) = (mongoDB.DB.GetCollection<Infectado>(typeof(Infectado).Name.ToLower()));

        [HttpGet]
        public async Task<IActionResult> ObterInfectadosAsync() => 
            Ok((await _infectadosCollection.FindAsync(Builders<Infectado>.Filter.Empty)).ToList());

        [HttpPost]
        public async Task<IActionResult> SalvarInfectadoAsync([FromBody] InfectadoDto dto)
        {
            var infectado = new Infectado(dto.DataNascimento, dto.Sexo, dto.Latitude, dto.Longitude);

            await _infectadosCollection.InsertOneAsync(infectado);

            return Created("Infectado/ObterInfectados", "Infectado adicionado com sucesso");
        }
    }
}
