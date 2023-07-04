using System.ComponentModel.DataAnnotations;

namespace ES.BS.Model
{
    public class BuildingSystem
    {
        [Key]
        public Guid Id { get; set; }
        public string? BuildingName { get; set; } 
        public int? NoOfFloors { get; set; }
        public bool? Generator_Staus { get; set; }
    }
}