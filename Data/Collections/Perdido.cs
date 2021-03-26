using System;
using MongoDB.Driver.GeoJsonObjectModel;

namespace achadoperdido_api_mongodb_dotnet.Collections
{
  public class Perdido
  {
    public Perdido(DateTime dataPerdido, string descricao, double latitude, double longitude)
    {
      this.DataPerdido = dataPerdido;
      this.Descricao = descricao;
      this.Localizacao = new GeoJson2DGeographicCoordinates(longitude, latitude);
    }

    public DateTime DataPerdido { get; set; }
    public string Descricao { get; set; }
    public GeoJson2DGeographicCoordinates Localizacao { get; set; }
  }
}