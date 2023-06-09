//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PlayerInformationSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Player
    {
        public int PlayerId { get; set; }

        [Display(Name = "Player Name")]
        [Required(ErrorMessage = "Player Name is required")]
        public string Name { get; set; }

        [Display(Name = "Gender Name")]
        [Required(ErrorMessage = "Gender is required")]
        public Nullable<int> GenderId { get; set; }
        public Nullable<int> Age { get; set; }

        [Display(Name = "Position Name")]
        [Required(ErrorMessage = "Position Name is required")]
        public Nullable<int> PositionId { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Hired Date")]
        public Nullable<System.DateTime> HireDate { get; set; }

        [Display(Name = "Club Name")]
        [Required(ErrorMessage = "Club Name is required")]
        public Nullable<int> ClubId { get; set; }
        public Nullable<System.DateTime> CreatedTime { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedTime { get; set; }
        public string UpdatedBy { get; set; }

        [Display(Name = "Active Status")]
        public Nullable<bool> IsActive { get; set; }

        [Display(Name = "Player Number")]
        public string PlayerNumber { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Expired Date")]
        public Nullable<System.DateTime> ExpiredDate { get; set; }
    
        public virtual Club Club { get; set; }
        public virtual Club Club1 { get; set; }
        public virtual Gender Gender { get; set; }
        public virtual Position Position { get; set; }
    }
}
