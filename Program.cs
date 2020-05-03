using System;
using System.Collections.Generic;
using System.Linq;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;

namespace raven_poc
{
    class Program
    {
        static void Main(string[] args)
        {
            using (IDocumentStore store = new DocumentStore
            {
                Urls = new[]                        // URL to the Server,
                {                                   // or list of URLs 
                    "http://localhost:8080"  // to all Cluster Servers (Nodes)
                },
                Database = "poc",             // Default database that DocumentStore will interact with
                Conventions = {  }                   // DocumentStore customizations
            })
            {
                store.Conventions.CustomizeJsonSerializer = s => s.Converters.Add(new NameJsonConverter());
                
                store.Initialize();                 // Each DocumentStore needs to be initialized before use.
                                                    // This process establishes the connection with the Server
                                                    // and downloads various configurations
                                                    // e.g. cluster topology or client configuration

                using (IDocumentSession session = store.OpenSession())  // Open a session for a default 'Database'
                {
                    // var category = new Category((Name)"Database Category");

                    // session.Store(category);                            // Assign an 'Id' and collection (Categories)
                    //                                                    // and start tracking an entity

                    // session.SaveChanges();                              // Send to the Server
                    //                                                     // one request processed in one transaction

                    var loaded = session.Load<Category>("categories/1-A");
                    loaded.ChangeName((Name)"new name 2222");
                    loaded.AddTag("test");

                    session.SaveChanges();

                    //TODO: fix honest types loading
                    string[] names = session
                        .Query<Category>()                               // Query for Products
                        .Where(x => x.Tags.Any(t => t == "test"))                 // Filter
                        .Skip(0).Take(10)                               // Page
                        .Select(x => x.Name.ToString())                            // Project
                        .ToArray();                
                }
            }
            Console.WriteLine("Hello World!");
        }
    }
}
