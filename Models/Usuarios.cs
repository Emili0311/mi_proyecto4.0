using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Usuarios
 {
     [ BsonId, BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
     
     public string? Id { get; set; }

     [BsonElement("nombre")]

      public string Nombre { get; set; } = string.Empty;

     [BsonElement("password")]

      public string Password { get; set; } = string.Empty;

    
      [BsonElement("correo")]

      public string Correo { get; set; } = string.Empty;

 }