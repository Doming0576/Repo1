using MongoDB.Bson;
using MongoDB.Driver;

public class CategoriaDbMap{
    public ObjectId id{get;set;}
    public string Nombre {get;set;}= string.Empty;
    public string UrlIcono {get;set;}
}