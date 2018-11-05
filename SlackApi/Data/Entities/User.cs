using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SlackApi.Data.Entities
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        //[Column(TypeName = "nvarchar(MAX)")]
        public Account_Type Account_Type { get; set; }
        //[Column(TypeName = "nvarchar(MAX)")]
        public Billing_Status Billing_Status { get; set; }
        public string Authentication { get; set; }
    }


    public enum Account_Type
    {
        Workspace_Owner,
        Workspace_Admin,
        Full_Member,
        Guest
        
    }

    public enum Billing_Status
    {
        Active,
        Inactive
    }

}
