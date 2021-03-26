using System;

namespace achadoperdido_api_mongodb_dotnet.Models

{
  public class PerdidoDto
  {
    public DateTime DataPerdido { get; set; }
    public string Descricao { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
  }
}