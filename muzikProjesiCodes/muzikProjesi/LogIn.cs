using newMuzikDosyam.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace muzikProjesi
{
    public partial class LogIn : Form
    {
        public static KullaniciBilgi kullaniciBilgi;
        private db_muzikEntities db = new db_muzikEntities();
        
        public LogIn()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String kullaniciAdi = textBox1.Text;
            String kullaniciEmail = textBox2.Text;
            if (!string.IsNullOrEmpty(kullaniciAdi) && !string.IsNullOrEmpty(kullaniciEmail))
            {

                kullaniciBilgi = db.tblKullanicis.Where(x => x.Ad == kullaniciAdi && x.Parola == kullaniciEmail).Select(x => new KullaniciBilgi { Ad = x.Ad, Email = x.Email, AbonelikTur = x.AbonelikTur, ID = x.ID, UlkeID = x.UlkeID, RoleID = x.RoleID }).FirstOrDefault();

                
                if (kullaniciBilgi != null)
                { 
                    if (kullaniciBilgi.RoleID == 1)
                    {
                        kullaniciFormu kForm = new kullaniciFormu();
                        kForm.Show();
                    }
                    else if (kullaniciBilgi.RoleID == 2)
                    {
                        yoneticiFormu yForm = new yoneticiFormu();

                        yForm.Show();
                    }
                }
                else
                {
                    MessageBox.Show("Kullanici sistemde kayıtlı değil");
                }
            }
            else
            {
                MessageBox.Show("Kullanıcı adını ve emailini giriniz");
            }

        }

        private void LogIn_Load(object sender, EventArgs e)
        {
            textBox1.Text = "veli";
            textBox2.Text = "12345";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            kullaniciKayit kKayit = new kullaniciKayit();
            kKayit.Show();
        }
    }
}
