using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeltExamCSharp2.Models
{
    public abstract class BaseEntity {
        
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedAt {get;set;}
        
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdatedAt {get;set;}
    }
}