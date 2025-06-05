using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace mi_proyecto4._0.Models;

[ApiController]
[Route("mi-proyecto")]

public class MiProyectoController : ControllerBase
{
    [HttpGet("integrantes")]
    public IActionResult Integrantes()
    {
        var proyecto = new MiProyecto
        {
            NombreIntegrante1 = "Milka Maddai Hernandez Ortega",
            NombreIntegrante2 = "Emili Yoselyn Gutierrez Salas"
        };
        return Ok(proyecto);
    }




 [HttpGet("presentacion")]

 public IActionResult Presentacion()
 {
    MongoClient client = new MongoClient (CadenasConexion.MONGO_DB);
    var db = client.GetDatabase ("Escuela_Emili_Milka");
    var collection = db.GetCollection<Equipo>("Equipo");

    var lista = collection.Find(FilterDefinition<Equipo>.Empty).ToList();
    return Ok(lista);
 }
}