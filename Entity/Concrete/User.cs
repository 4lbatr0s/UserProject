using Core.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace Entity
{
    public class User:IEntity
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
