using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace IacLabb2.Data.Model
{
    public class PhonebookDBcontext
    {
        private readonly IMongoDatabase _database;

        public PhonebookDBcontext(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("PhConnectionString");
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase("IaclLabb2"); 
        }

        public IMongoCollection<Phonebook> Phonebooks
        {
            get
            {
                return  _database.GetCollection<Phonebook>("Phonebooks");
            }
        }
    }
}