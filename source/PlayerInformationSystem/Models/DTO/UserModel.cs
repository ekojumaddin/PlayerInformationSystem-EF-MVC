using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace PlayerInformationSystem.Models.DTO
{
    public class UserModel
    {
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string Email { get; set; }
        public Nullable<int> RoleId { get; set; }

        [Display(Name = "Role Name")]
        public string RoleName { get; set; }

        [Display(Name = "Player Number")]
        public string PlayerNumber { get; set; }

        [Display(Name = "Player Name")]
        public string PlayerName { get; set; }

        [Display(Name = "Gender")]
        public Nullable<int> GenderId { get; set; }
        public Nullable<int> Age { get; set; }

        [Display(Name = "Position")]
        public Nullable<int> PositionId { get; set; }

        [DataType(DataType.Date)]
        public Nullable<System.DateTime> HireDate { get; set; }

        [DataType(DataType.Date)]
        public Nullable<System.DateTime> ExpiredDate { get; set; }

        [Display(Name = "Club")]
        public Nullable<int> ClubId { get; set; }
        public Nullable<bool> IsActive { get; set; }

        public virtual Role Role { get; set; }
        public virtual Club Club { get; set; }
        public virtual Gender Gender { get; set; }
        public virtual Position Position { get; set; }
    }
}