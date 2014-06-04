using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAcess.Models.ConferenceMember
{
    [Table("Members")]
    public class MemberModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int UserId { get; set; }
        public bool IsActive { get; set; }
        public string State { get; set; }
        public string Town { get; set; }
        public string EducationalInstitution { get; set; }
        public string NameOrNumber { get; set; }
        public string ClassOrGroup { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
        public bool IsAdmittedToSpeech { get; set; }
        public bool IsAdmittedToPresentation { get; set; }
    }
}