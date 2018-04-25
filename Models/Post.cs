using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeltExamCSharp2.Models
{
    public  class Post: BaseEntity {
        
        public int PostId {get;set;}
        
        public string Content {get;set;}
        
        [ForeignKey("Writer")]
        public int WriterId {get;set;}
        
        public User Writer {get;set;}

        public List<Like> Likes {get;set;}
        
        public Post(){
            Likes = new List<Like>();
        }
    }
}