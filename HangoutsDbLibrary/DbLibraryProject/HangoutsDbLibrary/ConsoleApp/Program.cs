using HangoutsDbLibrary.Data;
using HangoutsDbLibrary.Model;
using HangoutsDbLibrary.Repository;
using Microsoft.EntityFrameworkCore;
using RepositoriesAndUnitOfWork;
using System;
using System.Linq;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            DesignTimeDbContextFactory designTimeDbContextFactory = new DesignTimeDbContextFactory();
            HangoutsContext context = designTimeDbContextFactory.CreateDbContext(args);

            using ( var db = context)
            {
                DbInitializer.Initialize(db);
                UnitOfWork unitOfWork = new UnitOfWork(context);
                ConsoleUI console = new ConsoleUI(unitOfWork);

                console.start();

                Console.ReadLine();
                
            }
        }
    }
}
