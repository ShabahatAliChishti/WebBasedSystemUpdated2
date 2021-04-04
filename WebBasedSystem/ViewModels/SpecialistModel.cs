using System;
using System.Collections.Generic;
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
        public int Age { get; set; }
        public int SubMenuId { get; set; }
        public int TitleId { get; set; }


    }
}