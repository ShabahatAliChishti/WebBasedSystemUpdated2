using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebBasedSystem.ViewModels
{
    public class SpecialistModel
    {
        public int SpecialistId { get; set; }

        public string Title { get; set; }

        public string SpecialistName { get; set; }

        public string MobileNo { get; set; }

        public string PhoneNo { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string EmailAddress { get; set; }

        public string BusinessName { get; set; }

        public Nullable<int> SpecialistTypeId { get; set; }

        public string Surname { get; set; }

        public string OfficeNo { get; set; }

        public Nullable<bool> IsDeleted { get; set; }

        public string SubMenu { get; set; }
        public Nullable<int> Age { get; set; }
        public int SubMenuId { get; set; }
        public int TitleId { get; set; }
        public string PostCode { get; set; }
        public string Suburbs { get; set; }
        public string State { get; set; }
        public string NotesDescription { get; set; }
        public Nullable<bool> TakingNewPatients { get; set; }

[Required]
[DisplayName("Price")]
[Range(0.01, 100000.00)]
        public Nullable<decimal> price { get; set; }
        public string Waiting_Time { get; set; }
        public Nullable<bool> IsBooking { get; set; }
        public int Waiting_Time_Id { get; set; }


    }
}