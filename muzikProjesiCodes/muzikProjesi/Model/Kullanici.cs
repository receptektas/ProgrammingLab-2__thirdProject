using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace newMuzikDosyam.Model
{
    public class KullaniciBilgi
    {
        public int ID { get; set; }
        public string Ad { get; set; }
        public string Email { get; set; }
        public string Parola { get; set; }
        public short AbonelikTur { get; set; }
        public Nullable<int> RoleID { get; set; }
        public Nullable<int> UlkeID { get; set; }
    }
}
