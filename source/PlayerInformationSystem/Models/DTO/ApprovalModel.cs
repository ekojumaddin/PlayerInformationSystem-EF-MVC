using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace PlayerInformationSystem.Models.DTO
{
    public class ApprovalModel
    {
        public int playerId { get; set; }

        [Display(Name = "Player Number")]
        public string playerNumber { get; set; }

        [Display(Name = "Player Name")]
        public string playerName { get; set; }

        [Display(Name = "Club")]
        public string club { get; set; }

        [Display(Name = "Position")]
        public string position { get; set; }

        [Display(Name = "Status")]
        public string status { get; set; }

        [Display(Name = "Note")]
        [Required(ErrorMessage = "Note is required")]
        public string note { get; set; }

        [Display(Name = "Hire Date")]
        [DataType(DataType.Date)]
        public DateTime? hireDate { get; set; }

        [Display(Name = "Expired Date")]
        [DataType(DataType.Date)]
        public DateTime? expiredDate { get; set; }
    }
}