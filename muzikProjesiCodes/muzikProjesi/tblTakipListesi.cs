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
    
    public partial class tblTakipListesi
    {
        public int ID { get; set; }
        public int TakipEdenKullaniciID { get; set; }
        public Nullable<int> TakipEdilenKullaniciID { get; set; }
    
        public virtual tblKullanici tblKullanici { get; set; }
        public virtual tblKullanici tblKullanici1 { get; set; }
    }
}