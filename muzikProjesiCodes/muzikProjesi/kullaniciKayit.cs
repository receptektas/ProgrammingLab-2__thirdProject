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
    public partial class kullaniciKayit : Form
    {

        private db_muzikEntities db = new db_muzikEntities();
        public kullaniciKayit()
        {
            InitializeComponent();
        }

        private void kullaniciKayit_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'db_muzikDataSet.tblUlke' table. You can move, or remove it, as needed.
            this.tblUlkeTableAdapter.Fill(this.db_muzikDataSet.tblUlke);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                tblKullanici kullaniciTablosu = new tblKullanici();
                kullaniciTablosu.Ad = textBox1.Text;
                kullaniciTablosu.Email = textBox2.Text;
                kullaniciTablosu.Parola = textBox3.Text;
                kullaniciTablosu.AbonelikTur = Convert.ToInt16(comboBox1.SelectedIndex);
                kullaniciTablosu.RoleID = 1;
                kullaniciTablosu.UlkeID = comboBox3.SelectedIndex + 1;
                db.tblKullanicis.Add(kullaniciTablosu);
                db.SaveChanges();

                tblKullanici kullaniciTablosu1 = db.tblKullanicis.Where(y => y.Email == kullaniciTablosu.Email).FirstOrDefault();
                int kullaniciID = kullaniciTablosu1.ID;

                if (kullaniciTablosu.AbonelikTur == 1)
                {
                    tblPremiumKullaniciOdendiBilgi odeme = new tblPremiumKullaniciOdendiBilgi();
                    odeme.KullaniciID = kullaniciID;
                    odeme.Odendi = false;
                    db.tblPremiumKullaniciOdendiBilgis.Add(odeme);
                }

                for (int x = 1; x < 4; x++)
                {
                    tblKullaniciCalmaListesi calmaListesi = new tblKullaniciCalmaListesi();
                    calmaListesi.KullaniciID = kullaniciID;
                    calmaListesi.CalmaListesiTuru = x;
                    db.tblKullaniciCalmaListesis.Add(calmaListesi);
                }
                db.SaveChanges();
            }
            catch (Exception mess)
            {                
                MessageBox.Show(mess.InnerException.InnerException.Message);
            }

            this.Close();
        }

    }
}
