using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BeltExamCSharp2.Models;
namespace BeltExamCSharp2.Models
{
    public  class User: BaseEntity {
        
        public int UserId {get;set;}    
        
        public string Email{get;set;}        
        
        public string Alias {get;set;}
        
        public string Name{get;set;}              
        
        public string Password{get;set;}

        [InverseProperty("Writer")]
        public List<Post> PostsWritten {get;set;}

        public List<Like> Likes {get;set;}   

        public User(){
        PostsWritten = new List<Post>();    
        Likes = new List<Like>();    
        
        }
        
    }
    
}