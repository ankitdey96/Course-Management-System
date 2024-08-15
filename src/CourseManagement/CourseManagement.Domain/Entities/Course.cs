using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace CourseManagement.Domain.Entities
{
    public class Course : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Name {  get; set; }
        public string Description { get; set; }
        public Guid TeacherId { get; set; }
        public int NoOfClasses {  get; set; }
        public decimal Fees {  get; set; }
        public User Teacher {  get; set; }
        public byte[] Image { get; set; }  

        [NotMapped] 
        public IFormFile ImageFile { get; set; }  

    }
}
