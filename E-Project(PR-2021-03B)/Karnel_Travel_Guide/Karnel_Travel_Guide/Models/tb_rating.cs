//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Karnel_Travel_Guide.Models
{
    using System;
    using System.Collections.Generic;
    using System.Web;

    public partial class tb_rating
    {
        public int r_id { get; set; }
        public string r_comment { get; set; }
        public Nullable<int> r_rating { get; set; }
        public int r_user_id_foreign_key { get; set; }
        public int r_pro_id_foreign_key { get; set; }

        public HttpPostedFileBase ImageFile { get; set; }
    }
}
