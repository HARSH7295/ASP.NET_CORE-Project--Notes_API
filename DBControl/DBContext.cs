using Microsoft.EntityFrameworkCore;
using NotesAPI___with_Repository_Pattern_and_Dtos.Models;
using System.Collections.Generic;

namespace NotesAPI___with_Repository_Pattern_and_Dtos.DBControl
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {

        }

        public DbSet<Note> Notes { get; set; }

    }
}
