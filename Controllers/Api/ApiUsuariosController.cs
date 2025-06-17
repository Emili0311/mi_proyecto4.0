using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;

[ApiController]
[Route("api/usuarios")]
public class ApiUsuariosController : ControllerBase
{
    private readonly IMongoCollection<Usuarios> collection;

    public ApiUsuariosController()
    {
        var client = new MongoClient(CadenasConexion.MONGO_DB);
        var database = client.GetDatabase("Escuela_Emili_Milka");
        this.collection = database.GetCollection<Usuarios>("Usuarios");
    }

    [HttpGet]
    public IActionResult ListarUsuarios(string? texto)
    {
        var filter = FilterDefinition<Usuarios>.Empty;
        if (!string.IsNullOrWhiteSpace(texto))
        {
            var filterNombre = Builders<Usuarios>.Filter.Regex(u => u.Nombre, new BsonRegularExpression(texto, "i"));
            var filterCorreo = Builders<Usuarios>.Filter.Regex(u => u.Correo, new BsonRegularExpression(texto, "i"));
            filter = Builders<Usuarios>.Filter.Or(filterNombre, filterCorreo);
        }
        var list = this.collection.Find(filter).ToList();
        return Ok(list);
    }

    [HttpDelete("{id}")]
    public IActionResult EliminarUsuario(string id)
    {
        var filter = Builders<Usuarios>.Filter.Eq(x => x.Id, id);
        var item = this.collection.Find(filter).FirstOrDefault();
        if (item != null)
        {
            this.collection.DeleteOne(filter);
        }
        return NoContent();
    }

    [HttpPost]
    public IActionResult Create(UsuarioRequest model)
    {
        if (string.IsNullOrWhiteSpace(model.Correo))
        {
            return BadRequest("El correo es requerido");
        }

        if (string.IsNullOrWhiteSpace(model.Password))
        {
            return BadRequest("El password es requerido");
        }

        if (string.IsNullOrWhiteSpace(model.Nombre))
        {
            return BadRequest("El nombre es requerido");
        }

        var filter = Builders<Usuarios>.Filter.Eq(x => x.Correo, model.Correo);
        var item = this.collection.Find(filter).FirstOrDefault();
        if (item != null)
        {
            return BadRequest("El correo '" + model.Correo + "' ya existe en la base de datos");
        }

        Usuarios bd = new Usuarios();
        bd.Nombre = model.Nombre;
        bd.Correo = model.Correo;
        bd.Password = model.Password;

        this.collection.InsertOne(bd);
        return Ok();
    }

    [HttpGet("{id}")]
    public IActionResult Read(string id)
    {
        var filter = Builders<Usuarios>.Filter.Eq(x => x.Id, id);
        var item = this.collection.Find(filter).FirstOrDefault();
        if (item == null)
        {
            return NotFound("No existe un usuario con el ID proporcionado");
        }

        return Ok(item);
    }

    [HttpPut("{id}")]
    public IActionResult Update(string id, UsuarioRequest model)
    {
        if (string.IsNullOrWhiteSpace(model.Nombre))
        {
            return BadRequest("El nombre es requerido");
        }

        var filterId = Builders<Usuarios>.Filter.Eq(x => x.Id, id);
        var item = this.collection.Find(filterId).FirstOrDefault();
        if (item == null)
        {
            return NotFound("No existe un usuario con el ID proporcionado");
        }

        // Validar que el correo no esté duplicado en otro usuario
        var filterCorreo = Builders<Usuarios>.Filter.Eq(x => x.Correo, model.Correo);
        var existente = this.collection.Find(filterCorreo).FirstOrDefault();
        if (existente != null && existente.Id != id)
        {
            return BadRequest("El correo '" + model.Correo + "' ya existe en la base de datos");
        }

        var updateOptions = Builders<Usuarios>.Update
        .Set(x => x.Correo, model.Correo)
        .Set (x => x.Nombre, model.Nombre)
        .Set (x => x.Password, model.Password);

        this.collection.UpdateOne(filterId, updateOptions);

        return Ok();
    }
}