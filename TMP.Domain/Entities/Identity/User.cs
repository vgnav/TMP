namespace TMP.Domain.Entities.Identity
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    public class User
    {
        public int UserId { get; set; }
        public string IdnetityProviderId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Gender Gender { get; set; }        
        public DateTime? DateOfBirth { get; set; }   
        public DateTime Joined { get; set; }
        public DateTime? LastLoggedIn { get; set; }
        public SubType SubscriptionLevel { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }


        [NotMapped]
        public int? Age
        {
            get
            {
                if (!DateOfBirth.HasValue) return null;
                // TODO: replace with better age calculator
                return Convert.ToInt32(Math.Floor((DateTime.Now - DateOfBirth).Value.TotalDays / 365.25)); 
            }
        }
    }
}
