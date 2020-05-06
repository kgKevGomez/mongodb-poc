using System.Linq;
using System.Reflection;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Driver;

namespace mongodb_poc
{
    public class MongoMappingConvention : IClassMapConvention
    {
        public string Name
        {
            get { return "No use for a name"; }
        }

        public void Apply(BsonClassMap classMap)
        {
            var constructors = classMap
                .ClassType
                .GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance)
                .Concat(classMap.ClassType.GetConstructors(BindingFlags.Public | BindingFlags.Instance));

            var shortestContructor = constructors
                .OrderBy(ctor => ctor.GetParameters().Length)
                .ThenBy(ctor => ctor.IsPublic) //Prioritize protected/private constructors
                .FirstOrDefault();

            classMap.MapConstructor(shortestContructor);

            var publicProperties = classMap.ClassType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.CanRead);

            foreach (var publicProperty in publicProperties)
            {
                if (publicProperty.Name == "Id")
                {
                    classMap.MapIdMember(publicProperty).SetIdGenerator(StringObjectIdGenerator.Instance);
                }
            }
        }
    }
}
