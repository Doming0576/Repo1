using Microsoft.Extensions.Logging.Console;
using Microsoft.VisualBasic;
using MongoDB.Driver;

public static class CategoriasRequestHandler{
    public static IResult Crear(CategoriaDbMap datos){
        if (string.IsNullOrEmpty(datos.UrlIcono)){
            return Results.BadRequest("No se encontro el tipo de icono");
        }
        if(string.IsNullOrEmpty(datos.Nombre)){
            return Results.BadRequest("Tienes que escribir la categoria");
        }
        var filterBuilder = new FilterDefinitionBuilder<CategoriaDbMap>();
        var filter=filterBuilder.Eq(x => x.Nombre, datos.Nombre);
        BaseDatos bd = new BaseDatos();
        var coleccion = bd.ObtenerColeccion<CategoriaDbMap>("Categorias");
        CategoriaDbMap? registro = coleccion.Find(filter).FirstOrDefault();
        if(registro != null){
            return Results.BadRequest($"La categoria {datos.Nombre} ya existe en la base de datos");
        }
        registro = new CategoriaDbMap();
        registro.Nombre = datos.Nombre;
        registro.UrlIcono = datos.UrlIcono;
        coleccion!.InsertOne(registro);
        string nuevoId = registro.id.ToString();
        return Results.Ok(nuevoId);
    }
    public static IResult Listar(){
        var filterBuilder = new FilterDefinitionBuilder<CategoriaDbMap>();
        var filter = filterBuilder.Empty;
        BaseDatos bd = new BaseDatos();
        var coleccion = bd.ObtenerColeccion<CategoriaDbMap>("Categorias");
        List<CategoriaDbMap> mongoDbList = coleccion.Find(filter).ToList();
        var lista = mongoDbList.Select(x => new {
            Id = x.id.ToString(),
            Nombre = x.Nombre,
            UrlIcono = x.UrlIcono
        }).ToList();
        return Results.Ok(lista);
    }
}