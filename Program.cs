using System;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace mongodb_poc
{
    class Program
    {
        static void Main(string[] args)
        {
            //Allows for honest type serialization 
            BsonSerializer.RegisterSerializer<Name>(new NameSerializer());

            var pack = new ConventionPack();
            pack.Add(new MongoMappingConvention()); //Allows for protected constructor and sets Id generation
            ConventionRegistry.Register("Custom convention", pack, _ => true);

            var database = CreateMongoDbClient();
            var collection = database.GetCollection<Category>("bar");

            var document = new Category(new Name("test categ"));
            document.ChangeName((Name)"new name 2222");
            document.AddTag("test");

            collection.InsertOne(document);

            //Proves that we can filter based on honest types
            var filter = Builders<Category>.Filter.Regex(c => c.Name, new BsonRegularExpression(".*name.*"));
            var result = collection.Find(filter).ToList();

            Console.WriteLine("Hello World!");
        }

        private static IMongoDatabase CreateMongoDbClient()
        {
            var client = new MongoClient("mongodb://root:example@localhost:27017");
            var database = client.GetDatabase("foo");
            return database;
        }
    }
}
