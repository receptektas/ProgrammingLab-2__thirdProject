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
    
    public partial class tblKullaniciCalmaListesi
    {
        public tblKullaniciCalmaListesi()
        {
            this.tblCalmaListelerindekiSarkilars = new HashSet<tblCalmaListelerindekiSarkilar>();
        }
    
        public int ID { get; set; }
        public int KullaniciID { get; set; }
        public int CalmaListesiTuru { get; set; }
    
        public virtual ICollection<tblCalmaListelerindekiSarkilar> tblCalmaListelerindekiSarkilars { get; set; }
        public virtual tblKullanici tblKullanici { get; set; }
        public virtual tblSarkiTur tblSarkiTur { get; set; }
    }
}
