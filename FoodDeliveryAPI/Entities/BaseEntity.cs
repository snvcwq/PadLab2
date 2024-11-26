using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace FoodDeliveryAPI.Entities
{
    public class BaseEntity
    {
        [Key]
/*        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]*/
        public Guid Id { get; set; }

        public DateTime LastChangeAt { get; set; }
    }
}
