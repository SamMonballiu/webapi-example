using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace DataLayer.Models
{
    public class Book : IValidatableObject
    {
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string Name { get; set; }

        [Required]
        public int PublicationYear { get; set; }

        public int AuthorId { get; set; }

        [ForeignKey("AuthorId")]
        [JsonIgnore] // json
        [IgnoreDataMember] // xml
        public Author Author { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> vr = new List<ValidationResult>();

            List<Func<int, bool>> yearConditions = new List<Func<int, bool>>()
            {
                (year) => DateTime.Now.Year >= 0,
                (year) => DateTime.Now.Year > year,
                (year) => year.ToString().Length <= 4,
            };

            if (!YearMeetsAllConditions(PublicationYear))
            {
                vr.Add(new ValidationResult("Publication year is not valid."));
            }

            return vr;

            bool YearMeetsAllConditions(int year) => yearConditions.TrueForAll(cond => cond(year));
        }
    }
}
