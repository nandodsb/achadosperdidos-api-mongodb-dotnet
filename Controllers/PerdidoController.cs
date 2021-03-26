using System;
using achadoperdido_api_mongodb_dotnet.Collections;
using achadoperdido_api_mongodb_dotnet.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace achadoperdido_api_mongodb_dotnet.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class PerdidoController : ControllerBase
  {
    Data.MongoDB _mongoDB;
    IMongoCollection<Perdido> _perdidosCollection;

    public PerdidoController(Data.MongoDB mongoDB) //
    {
      _mongoDB = mongoDB;
      _perdidosCollection = _mongoDB.DB.GetCollection<Perdido>(typeof(Perdido).Name.ToLower());
    }

    [HttpPost]
    public ActionResult SalvarInfectado([FromBody] PerdidoDto dto)
    {
      var perdido = new Perdido(dto.DataPerdido, dto.Descricao, dto.Latitude, dto.Longitude);

      _perdidosCollection.InsertOne(perdido);

      return StatusCode(201, "Requisição de objeto perdido cadastrado com sucesso");
    }

    [HttpGet]
    public ActionResult ObterPerdidos()
    {
      var perdidos = _perdidosCollection.Find(Builders<Perdido>.Filter.Empty).ToList();

      return Ok(perdidos);
    }

    [HttpPut]
    public ActionResult AtualizarPerdido([FromBody] PerdidoDto dto)
    {


      var perdido = new Perdido(dto.DataPerdido, dto.Descricao, dto.Latitude, dto.Longitude);

      _perdidosCollection.UpdateOne(Builders<Perdido>.Filter.Where(_ => _.DataPerdido == dto.DataPerdido), Builders<Perdido>.Update.Set("descricao", dto.Descricao));

      return Ok("Atualizado com sucesso");
    }


    [HttpDelete("{dataPerda}")]
    public ActionResult Delete(DateTime dataPerda)
    {
      _perdidosCollection.DeleteOne(Builders<Perdido>.Filter.Where(_ => _.DataPerdido == dataPerda));

      return Ok("Objeto encontra, apagado o registro com sucesso");
    }
  }
}