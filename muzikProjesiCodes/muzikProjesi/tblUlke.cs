//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace muzikProjesi
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblUlke
    {
        public tblUlke()
        {
            this.tblKullanicis = new HashSet<tblKullanici>();
            this.tblSanatcis = new HashSet<tblSanatci>();
        }
    
        public int ID { get; set; }
        public string UlkeIsmi { get; set; }
    
        public virtual ICollection<tblKullanici> tblKullanicis { get; set; }
        public virtual ICollection<tblSanatci> tblSanatcis { get; set; }
    }
}