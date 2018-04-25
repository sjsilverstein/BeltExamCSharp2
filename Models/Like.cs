using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeltExamCSharp2.Models
{
    public  class Like: BaseEntity {
        
        public int LikeId {get;set;}
                
        public int UserId {get;set;}
        
        public User User {get;set;}
        
        public int PostId {get;set;}
        
        public Post Post{get;set;}
    }
}