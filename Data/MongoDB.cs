using System;
using achadoperdido_api_mongodb_dotnet.Collections;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace achadoperdido_api_mongodb_dotnet.Data
{
  public class MongoDB
  {
    public IMongoDatabase DB { get; }

    public MongoDB(IConfiguration configuration)
    {
      try
      {
        var settings = MongoClientSettings.FromUrl(new MongoUrl(configuration["ConnectionString"]));
        var client = new MongoClient(settings);
        DB = client.GetDatabase(configuration["NomeBanco"]);
        MapClasses();
      }
      catch (Exception ex)
      {
        throw new MongoException("It was not possible to connect to MongoDB", ex);
      }
    }

    private void MapClasses()
    {
      var conventionPack = new ConventionPack { new CamelCaseElementNameConvention() };
      ConventionRegistry.Register("camelCase", conventionPack, t => true);

      if (!BsonClassMap.IsClassMapRegistered(typeof(Perdido)))
      {
        BsonClassMap.RegisterClassMap<Perdido>(i =>
        {
          i.AutoMap();
          i.SetIgnoreExtraElements(true);
        });
      }
    }
  }
}