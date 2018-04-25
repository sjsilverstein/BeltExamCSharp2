using Microsoft.EntityFrameworkCore;
using BeltExamCSharp2.Models;

namespace BeltExamCSharp2.Context
{
    public class BeltExamCSharp2Context : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        
        public BeltExamCSharp2Context(DbContextOptions<BeltExamCSharp2Context> options) : base(options) { }
        public DbSet<User> Users {get;set;}
        public DbSet<Post> Posts {get;set;}
        public DbSet<Like> Likes {get;set;}
    }
}