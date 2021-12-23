using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace do_an_ao_hoa.Models
{
    public class MongoHelper
    {
        public static IMongoClient client { get; set; }
        public static IMongoDatabase database { get; set; }
        public static string MongoDatabase = "quanly";
        public static string MongoConnection = @"mongodb + srv://sa:123@cluster0.jkkil.mongodb.net/quanly?retryWrites=true&w=majority";
        public static IMongoCollection<Student> students_collection { get; set; }
        // You might need to replace & with &amp; in your Mongo Connection string stored in Web.Config
        internal static void ConnectToMongoService()
        {
            var settings = MongoClientSettings.FromConnectionString("mongodb+srv://sa:123@cluster0.jkkil.mongodb.net/quanly?retryWrites=true&w=majority");
            var client = new MongoClient(settings);
           database = client.GetDatabase("quanly");
        }
    }
}