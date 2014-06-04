using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAcess.Models.ConferenceMember
{
    [Table("MembersWorks")]
    public class MemberWorkModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int MemberId { get; set; }
        public string FileName { get; set; }
    }
}