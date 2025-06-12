using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
 
 [ApiController]
 [Route("api/usuarios")]
 public class ApiUsuariosController : ControllerBase
 {
    // Metodos para hacer las operaciones CRUD
    // C = Create
    //R = Read
    //U = Update
    //D = Delete

   private readonly IMongoCollection<Usuarios> collection;
 
   public ApiUsuariosController()
   {
     MongoClient client = new MongoClient (CadenasConexion.MONGO_DB);
    var database = client.GetDatabase ("Escuela_Emili_Milka");
    this.collection = database.GetCollection<Usuarios>("Usuarios");

   }
    [HttpGet]
    public IActionResult ListarUsuarios()
    {
        var filter = FilterDefinition<Usuarios>.Empty;
        var list = this.collection.Find(filter).ToList();
     return Ok(list);
    }
 }