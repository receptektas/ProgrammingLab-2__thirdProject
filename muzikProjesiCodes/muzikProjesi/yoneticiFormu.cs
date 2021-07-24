using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
//using System.Data.Entity;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace muzikProjesi
{
    public partial class yoneticiFormu : Form
    {
        private db_muzikEntities db = new db_muzikEntities();
        public int ID;

        public yoneticiFormu()
        {
            InitializeComponent();
        }

        private void yoneticiFormu_Load(object sender, EventArgs e)
        {
            this.albumTabloTableAdapter.Fill(this.db_muzikDataSet1.albumTablo);
            this.kullanıcı_viewTableAdapter.Fill(this.db_muzikDataSet1.kullanıcı_view);
            this.tblKullaniciRoleTableAdapter.Fill(this.db_muzikDataSet1.tblKullaniciRole);
            this.tblSarkiTableAdapter.Fill(this.db_muzikDataSet1.tblSarki);
            this.tblAlbumTableAdapter.Fill(this.db_muzikDataSet1.tblAlbum);
            this.tblSanatciTableAdapter.Fill(this.db_muzikDataSet1.tblSanatci);
            this.tblUlkeTableAdapter.Fill(this.db_muzikDataSet1.tblUlke);
            this.tblSarkiTurTableAdapter.Fill(this.db_muzikDataSet1.tblSarkiTur);
            this.tblUlkeTableAdapter.Fill(this.db_muzikDataSet.tblUlke);
            this.tblKullaniciRoleTableAdapter.Fill(this.db_muzikDataSet.tblKullaniciRole);
            this.sarkiTabloTableAdapter.Fill(this.db_muzikDataSet.sarkiTablo);
            this.sanatciTabloTableAdapter.Fill(this.db_muzikDataSet.sanatciTablo);
            this.kullanıcı_viewTableAdapter.Fill(this.db_muzikDataSet.kullanıcı_view);

            setComboValues(0, 0);
            setComboValues(0, 0, 0);
            tabloDoldur(0);

            dataGridView1.Columns[0].Visible = false;
            dataGridView2.Columns[0].Visible = false;
            dataGridView3.Columns[0].Visible = false;
            dataGridView4.Columns[0].Visible = false;
            //dataGridView5.Columns[0].Visible = false;
            dataGridView6.Columns[0].Visible = false;
            //dataGridView7.Columns[0].Visible = false;

            dataGridView1.Columns[3].Visible = false;
            dataGridView2.Columns[3].Visible = false;
            dataGridView3.Columns[4].Visible = false;
            dataGridView6.Columns[7].Visible = false;
            dataGridView6.Columns[8].Visible = false;
            dataGridView6.Columns[9].Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ID != 0)
            {
                try
                {
                    tblSarki sarkiTablosu = db.tblSarkis.Where(x => x.ID == ID).FirstOrDefault();
                    sarkiTablosu.Ad = textBox1.Text;
                    sarkiTablosu.Tur = comboBox1.SelectedIndex + 1;
                    db.SaveChanges();
                    clearValues();
                    tabloDoldur(1);
                }
                catch (Exception mess)
                {
                    HatalariSil();
                    MessageBox.Show(mess.InnerException.InnerException.Message);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                tblSarki sarkiTablosu = new tblSarki();
                sarkiTablosu.Ad = textBox1.Text;
                sarkiTablosu.Tur = comboBox1.SelectedIndex + 1;
                db.tblSarkis.Add(sarkiTablosu);
                db.SaveChanges();
                clearValues();
                tabloDoldur(1);
            }
            catch (Exception mess)
            {
                HatalariSil();
                MessageBox.Show(mess.InnerException.InnerException.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (ID != 0)
            {
                try
                {
                    tblSarki sarkiTablosu = db.tblSarkis.Where(x => x.ID == ID).FirstOrDefault();
                    if (sarkiTablosu != null)
                    {
                        db.tblSarkis.Remove(sarkiTablosu);
                        db.SaveChanges();
                        kullaniciRefresh();
                        clearValues();
                        tabloDoldur(1);
                    }
                }
                catch (Exception mess)
                {
                    HatalariSil();
                    MessageBox.Show(mess.InnerException.InnerException.Message);
                }
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (ID != 0)
            {
                try
                {
                    tblSanatci sanatciTablosu = db.tblSanatcis.Where(x => x.ID == ID).FirstOrDefault();
                    sanatciTablosu.Ad = textBox2.Text;
                    sanatciTablosu.UlkeID = comboBox2.SelectedIndex + 1;
                    db.SaveChanges();
                    clearValues();
                    tabloDoldur(2);
                }
                catch (Exception mess)
                {
                    HatalariSil();
                    MessageBox.Show(mess.InnerException.InnerException.Message);
                }
            }
        }

        private void button22_Click(object sender, EventArgs e)
        {
            try
            {
                tblSanatci sanatciTablosu = new tblSanatci();
                sanatciTablosu.Ad = textBox2.Text;
                sanatciTablosu.UlkeID = comboBox2.SelectedIndex + 1;
                db.tblSanatcis.Add(sanatciTablosu);
                db.SaveChanges();
                clearValues();
                tabloDoldur(2);
            }
            catch (Exception mess)
            {
                HatalariSil();
                MessageBox.Show(mess.InnerException.InnerException.Message);
            }

        }

        private void button33_Click(object sender, EventArgs e)
        {
            if (ID != 0)
            {
                try
                {
                    tblSanatci sanatciTablosu = db.tblSanatcis.Where(x => x.ID == ID).FirstOrDefault();
                    if (sanatciTablosu != null)
                    {
                        db.tblSanatcis.Remove(sanatciTablosu);
                        db.SaveChanges();
                        clearValues();
                        tabloDoldur(2);
                    }
                }
                catch (Exception mess)
                {
                    HatalariSil();
                    MessageBox.Show(mess.InnerException.InnerException.Message);
                }
            }
        }

        private void button111_Click(object sender, EventArgs e)
        {
            if (ID != 0)
            {
                try
                {
                    tblAlbum albumTablosu = db.tblAlbums.Where(x => x.ID == ID).FirstOrDefault();
                    albumTablosu.Ad = textBox3.Text;
                    albumTablosu.Tarih = dateTimePicker1.Value;
                    albumTablosu.Tur = comboBox3.SelectedIndex + 1;
                    db.SaveChanges();
                    dateTimePicker1.Value = DateTime.Today;
                    clearValues();
                    tabloDoldur(3);
                }
                catch (Exception mess)
                {
                    HatalariSil();
                    MessageBox.Show(mess.InnerException.InnerException.Message);
                }
            }
        }

        private void button222_Click(object sender, EventArgs e)
        {
            try
            {
                tblAlbum albumTablosu = new tblAlbum();
                albumTablosu.Ad = textBox3.Text;
                albumTablosu.Tarih = dateTimePicker1.Value;
                albumTablosu.Tur = comboBox3.SelectedIndex + 1;
                db.tblAlbums.Add(albumTablosu);
                db.SaveChanges();
                dateTimePicker1.Value = DateTime.Today;
                clearValues();
                tabloDoldur(3);
            }
            catch (Exception mess)
            {
                HatalariSil();
                MessageBox.Show(mess.InnerException.InnerException.Message);
            }
        }

        private void button333_Click(object sender, EventArgs e)
        {
            if (ID != 0)
            {
                try
                {
                    tblAlbum albumTablosu = db.tblAlbums.Where(x => x.ID == ID).FirstOrDefault();
                    if (albumTablosu != null)
                    {
                        db.tblAlbums.Remove(albumTablosu);
                        db.SaveChanges();
                        dateTimePicker1.Value = DateTime.Today;
                        clearValues();
                        tabloDoldur(3);
                    }
                }
                catch (Exception mess)
                {
                    HatalariSil();
                    MessageBox.Show(mess.InnerException.InnerException.Message);
                }
            }
        }
        private void setComboValues(int comboDegeri, int type)
        {
            List<tblSarkiTur> turListe = new List<tblSarkiTur>();
            List<tblUlke> ulkeListe = new List<tblUlke>();
            if (type == 1)
            {
                turListe = db.tblSarkiTurs.ToList();
                comboBox1.DataSource = turListe;
                comboBox1.ValueMember = "ID";
                comboBox1.DisplayMember = "Tur";
                comboBox1.SelectedIndex = comboDegeri;
            }
            else if (type == 2)
            {
                ulkeListe = db.tblUlkes.ToList();
                comboBox2.DataSource = ulkeListe;
                comboBox2.ValueMember = "ID";
                comboBox2.DisplayMember = "UlkeIsmi";
                comboBox2.SelectedIndex = comboDegeri;
            }
            else if (type == 3)
            {
                turListe = db.tblSarkiTurs.ToList();
                comboBox3.DataSource = turListe;
                comboBox3.ValueMember = "ID";
                comboBox3.DisplayMember = "Tur";
                comboBox3.SelectedIndex = comboDegeri;
            }
        }
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                ID = Convert.ToInt32(row.Cells[0].Value.ToString());
                textBox1.Text = row.Cells[1].Value.ToString();
                comboBox1.Text = row.Cells[2].Value.ToString();
                setComboValues(Convert.ToInt32(row.Cells[3].Value.ToString()) - 1, 1);
            }

            var tablo1 = db.tblSarkis.Join(db.tblSanatcininSarkilaris, sar => sar.ID, ss => ss.SarkiID, (sar, ss) => new { sar, ss })
            .Join(db.tblAlbums, ss1 => ss1.ss.AlbumID, alb => alb.ID, (ss1, alb) => new { ss1, alb })
            .Join(db.tblSanatcis, ss2 => ss2.ss1.ss.SanatciID, san => san.ID, (ss2, san) => new { ss2, san })
            .Join(db.tblSarkiTurs, ss3 => ss3.ss2.ss1.sar.tblSarkiTur.ID, sartur => sartur.ID, (ss3, sartur) => new { ss3, sartur })
            .Where(x => x.ss3.ss2.ss1.ss.SarkiID == ID)
            .Select(m => new
            {
                ID = m.ss3.ss2.ss1.ss.SarkiID,
                AlbumAd = m.ss3.ss2.alb.Ad,
                SanatciAd = m.ss3.san.Ad,
                m.sartur.Tur
            }).ToList();

            this.dataGridView7.DataSource = tablo1;
        }
        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView2.SelectedRows)
            {
                ID = Convert.ToInt32(row.Cells[0].Value.ToString());
                textBox2.Text = row.Cells[1].Value.ToString();
                comboBox2.Text = row.Cells[2].Value.ToString();
                setComboValues(Convert.ToInt32(row.Cells[3].Value.ToString()) - 1, 2);
            }

            var tablo2 = db.tblSanatcininSarkilaris.Join(db.tblSanatcis, ss => ss.SanatciID, san => san.ID, (ss, san) => new { ss, san })
            .Join(db.tblSarkis, ss1 => ss1.ss.SarkiID, sar => sar.ID, (ss1, sar) => new { ss1, sar })
            .Join(db.tblAlbums, ss2 => ss2.ss1.ss.AlbumID, alb => alb.ID, (ss2, alb) => new { ss2, alb })
            .Where(x => x.ss2.ss1.ss.SanatciID == ID)
            .Select(m => new
            {
                ID = m.ss2.ss1.ss.ID,
                AlbumAd = m.alb.Ad,
                SanatciAd = m.ss2.ss1.san.Ad,
                SarkiAd = m.ss2.sar.Ad,
                Tarih = m.ss2.ss1.ss.Tarih,
                SarkiSure = m.ss2.ss1.ss.SarkiSure,
                Dnsayisi = m.ss2.ss1.ss.DinlenmeSayisi
            }).ToList();

            this.dataGridView5.DataSource = tablo2;
        }
        private void dataGridView3_SelectionChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView3.SelectedRows)
            {
                ID = Convert.ToInt32(row.Cells[0].Value.ToString());
                textBox3.Text = row.Cells[1].Value.ToString();
                dateTimePicker1.Text = row.Cells[2].Value.ToString();
                comboBox3.Text = row.Cells[3].Value.ToString();
                setComboValues(Convert.ToInt32(row.Cells[4].Value.ToString()) - 1, 3);
            }

            var tablo = db.tblAlbums.Join(db.tblSanatcininSarkilaris, alb => alb.ID, ss => ss.AlbumID, (alb, ss) => new { alb, ss })
            .Join(db.tblSarkis, ss1 => ss1.ss.SarkiID, sar => sar.ID, (ss1, sar) => new { ss1, sar })
            .Join(db.tblSanatcis, ss2 => ss2.ss1.ss.SanatciID, san => san.ID, (ss2, san) => new { ss2, san })
            .Join(db.tblSarkiTurs, ss3 => ss3.ss2.sar.tblSarkiTur.ID, sartur => sartur.ID, (ss3, sartur) => new { ss3, sartur })
            .Where(x => x.ss3.ss2.ss1.ss.AlbumID == ID)
            .Select(m => new
            {
                ID = m.ss3.ss2.ss1.ss.AlbumID,
                SarkiAd = m.ss3.ss2.sar.Ad,
                SanatciAd = m.ss3.san.Ad,
                m.sartur.Tur
            }).ToList();

            this.dataGridView4.DataSource = tablo;

        }

        private void button4_Click(object sender, EventArgs e)
        {
            clearValues();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            try
            {
                tblSanatcininSarkilari sanatciSarkilari = db.tblSanatcininSarkilaris.Where(x => x.ID == ID).FirstOrDefault();
                if (sanatciSarkilari != null)
                {
                    db.tblSanatcininSarkilaris.Remove(sanatciSarkilari);
                    db.SaveChanges();
                    clearValues();
                    sanatciSarkiTabloDoldur();
                }
            }
            catch (Exception mess)
            {
                HatalariSil();
                MessageBox.Show(mess.InnerException.InnerException.Message);
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            try
            {
                tblSanatcininSarkilari sanatciSarkilari = db.tblSanatcininSarkilaris.Where(s => s.ID == ID).FirstOrDefault();
                int albumID = db.tblAlbums.Where(z => z.Ad == comboBox4.Text).Select(x => x.ID).FirstOrDefault();
                int sarkiID = db.tblSarkis.Where(y => y.Ad == comboBox5.Text).Select(a => a.ID).FirstOrDefault();

                sanatciSarkilari.Tarih = dateTimePicker2.Value;
                sanatciSarkilari.SarkiID = sarkiID;

                foreach (DataGridViewRow row in dataGridView2.SelectedRows)
                {
                    sanatciSarkilari.SanatciID = Convert.ToInt32(row.Cells[0].Value.ToString());
                }

                sanatciSarkilari.AlbumID = albumID;
                if (!String.IsNullOrEmpty(textBox4.Text))
                {
                    sanatciSarkilari.SarkiSure = TimeSpan.Parse(textBox4.Text); ;
                }
                db.SaveChanges();
                clearValues();
                sanatciSarkiTabloDoldur();
            }
            catch (Exception mess)
            {
                HatalariSil();
                MessageBox.Show(mess.InnerException.InnerException.Message);
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            try
            {
                tblSanatcininSarkilari sanatciSarkilari = new tblSanatcininSarkilari();
                int albumID = db.tblAlbums.Where(z => z.Ad == comboBox4.Text).Select(x => x.ID).FirstOrDefault();
                int sarkiID = db.tblSarkis.Where(y => y.Ad == comboBox5.Text).Select(a => a.ID).FirstOrDefault();

                sanatciSarkilari.DinlenmeSayisi = 0;
                sanatciSarkilari.Tarih = dateTimePicker2.Value;
                sanatciSarkilari.SanatciID = ID;
                sanatciSarkilari.SarkiID = sarkiID;
                sanatciSarkilari.AlbumID = albumID;
                if (!String.IsNullOrEmpty(textBox4.Text))
                {
                    sanatciSarkilari.SarkiSure = TimeSpan.Parse(textBox4.Text); ;
                }
                db.tblSanatcininSarkilaris.Add(sanatciSarkilari);
                db.SaveChanges();

                sanatciSarkiTabloDoldur();
            }
            catch (Exception mess)
            {
                HatalariSil();
                MessageBox.Show(mess.InnerException.InnerException.Message);
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ID = 0;
            this.tblKullaniciRoleTableAdapter.Fill(this.db_muzikDataSet1.tblKullaniciRole);
            this.tblSarkiTableAdapter.Fill(this.db_muzikDataSet1.tblSarki);
            this.tblAlbumTableAdapter.Fill(this.db_muzikDataSet1.tblAlbum);
            this.tblSanatciTableAdapter.Fill(this.db_muzikDataSet1.tblSanatci);
            this.tblUlkeTableAdapter.Fill(this.db_muzikDataSet1.tblUlke);
            this.tblSarkiTurTableAdapter.Fill(this.db_muzikDataSet1.tblSarkiTur);
            this.tblUlkeTableAdapter.Fill(this.db_muzikDataSet.tblUlke);
            this.tblKullaniciRoleTableAdapter.Fill(this.db_muzikDataSet.tblKullaniciRole);
        }
        // kullanıcı formu
        private void button1000_Click(object sender, EventArgs e)
        {
            try
            {
                tblKullanici kullaniciTablosu = new tblKullanici();
                kullaniciTablosu.Ad = textBox5.Text;
                kullaniciTablosu.Email = textBox6.Text;
                kullaniciTablosu.Parola = textBox7.Text;
                kullaniciTablosu.AbonelikTur = Convert.ToInt16(comboBox6.SelectedIndex);
                kullaniciTablosu.RoleID = comboBox7.SelectedIndex + 1;
                kullaniciTablosu.UlkeID = comboBox8.SelectedIndex + 1;
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
                clearValues();
                kullaniciRefresh();
                this.kullanıcı_viewTableAdapter.Fill(db_muzikDataSet.kullanıcı_view);
            }
            catch (Exception mess)
            {
                HatalariSil();
                MessageBox.Show(mess.InnerException.InnerException.Message);
            }
        }

        private void button1001_Click(object sender, EventArgs e)
        {
            try
            {
                tblKullanici kullaniciTablosu = db.tblKullanicis.Where(x => x.ID == ID).FirstOrDefault();
                kullaniciTablosu.Ad = textBox5.Text;
                kullaniciTablosu.Email = textBox6.Text;
                kullaniciTablosu.Parola = textBox7.Text;
                kullaniciTablosu.AbonelikTur = Convert.ToInt16(comboBox6.SelectedIndex);
                kullaniciTablosu.RoleID = comboBox7.SelectedIndex + 1;
                kullaniciTablosu.UlkeID = comboBox8.SelectedIndex + 1;
                db.SaveChanges();
                clearValues();
                kullaniciRefresh();
                this.kullanıcı_viewTableAdapter.Fill(db_muzikDataSet.kullanıcı_view);
            }
            catch (Exception mess)
            {
                HatalariSil();
                MessageBox.Show(mess.InnerException.InnerException.Message);
            }
        }

        private void kullaniciRefresh()
        {
            this.kullanıcı_viewTableAdapter.Fill(this.db_muzikDataSet1.kullanıcı_view);
        }
        private void setComboValues(int comboDegeri1, int comboDegeri2, int comboDegeri3)
        {
            List<abonelikTuru> aboneTurleri = new List<abonelikTuru>();
            aboneTurleri.Add(new abonelikTuru { ID = 0, aboneTuru = "Normal" });
            aboneTurleri.Add(new abonelikTuru { ID = 1, aboneTuru = "Premium" });

            List<tblKullaniciRole> rolListe = new List<tblKullaniciRole>();
            rolListe = db.tblKullaniciRoles.ToList();

            List<tblUlke> ulkeListe = new List<tblUlke>();
            ulkeListe = db.tblUlkes.ToList();

            comboBox6.DataSource = aboneTurleri;
            comboBox6.ValueMember = "ID";
            comboBox6.DisplayMember = "aboneTuru";
            comboBox6.SelectedIndex = comboDegeri1;

            comboBox7.DataSource = rolListe;
            comboBox7.ValueMember = "ID";
            comboBox7.DisplayMember = "RolIsmi";
            comboBox7.SelectedIndex = comboDegeri2;

            comboBox8.DataSource = ulkeListe;
            comboBox8.ValueMember = "ID";
            comboBox8.DisplayMember = "UlkeIsmi";
            comboBox8.SelectedIndex = comboDegeri3;
        }

        private void clearValues()
        {
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
            textBox3.Text = string.Empty;
            textBox4.Text = string.Empty;
            textBox5.Text = string.Empty;
            textBox6.Text = string.Empty;
            textBox7.Text = string.Empty;
            //comboBox1.SelectedIndex = 0;
            //comboBox2.SelectedIndex = 0;
            //comboBox3.SelectedIndex = 0;
            //comboBox4.SelectedIndex = 0;
            //comboBox5.SelectedIndex = 0;
            //comboBox6.SelectedIndex = 0;
            //comboBox7.SelectedIndex = 0;
            //comboBox8.SelectedIndex = 0;
            ID = 0;
            dateTimePicker1.Value = DateTime.Today;
            dateTimePicker2.Value = DateTime.Today;
        }
        private void dataGridView1001_SelectionChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView6.SelectedRows)
            {
                ID = Convert.ToInt32(row.Cells[0].Value.ToString());
                textBox5.Text = row.Cells[1].Value.ToString();
                textBox6.Text = row.Cells[2].Value.ToString();
                textBox7.Text = row.Cells[3].Value.ToString();
                comboBox8.Text = row.Cells[4].Value.ToString();

                setComboValues(Convert.ToInt32(row.Cells[7].Value.ToString()), Convert.ToInt32(row.Cells[8].Value.ToString()) - 1, Convert.ToInt32(row.Cells[9].Value.ToString()) - 1);

            }

        }

        public partial class abonelikTuru
        {
            public int ID { get; set; }
            public string aboneTuru { get; set; }
        }

        private void button1003_Click(object sender, EventArgs e)
        {
            try
            {
                tblKullanici kullaniciTablosu = db.tblKullanicis.Where(x => x.ID == ID).FirstOrDefault();
                if (kullaniciTablosu != null)
                {
                    db.tblKullanicis.Remove(kullaniciTablosu);
                    db.SaveChanges();
                    clearValues();
                    this.kullanıcı_viewTableAdapter.Fill(db_muzikDataSet1.kullanıcı_view);
                }
            }
            catch (Exception mess)
            {
                HatalariSil();
                MessageBox.Show(mess.InnerException.InnerException.Message);
            }

        }

        private void button1004_Click(object sender, EventArgs e)
        {
            clearValues();
        }

        private void tabloDoldur(int type)
        {
            if (type == 0 || type == 1)
            {
                var tablo1 = db.tblSarkis.Join(db.tblSarkiTurs, sar => sar.Tur, sartur => sartur.ID, (sar, sartur) => new { sar, sartur })
                .Select(m => new
                {
                    ID = m.sar.ID,
                    Ad = m.sar.Ad,
                    AlbumTur = m.sartur.Tur,
                    AlbumID = m.sartur.ID
                }).ToList();

                this.dataGridView1.DataSource = tablo1;
            }
            if (type == 0 || type == 2)
            {
                var tablo2 = db.tblSanatcis.Join(db.tblUlkes, san => san.UlkeID, ulke => ulke.ID, (san, ulke) => new { san, ulke })
                .Select(m => new
                {
                    ID = m.san.ID,
                    Ad = m.san.Ad,
                    Ulke = m.ulke.UlkeIsmi,
                    UlkeID = m.ulke.ID
                }).ToList();

                this.dataGridView2.DataSource = tablo2;
            }

            if (type == 0 || type == 3)
            {

                var tablo3 = db.tblAlbums.Join(db.tblSarkiTurs, alb => alb.Tur, sartur => sartur.ID, (alb, sartur) => new { alb, sartur })
                .Select(m => new
                {
                    ID = m.alb.ID,
                    Ad = m.alb.Ad,
                    Tarih = m.alb.Tarih,
                    AlbumTur = m.sartur.Tur,
                    AlbumID = m.sartur.ID
                }).ToList();

                this.dataGridView3.DataSource = tablo3;
            }

        }

        private void sanatciSarkiTabloDoldur()
        {
            var tablo = db.tblSanatcininSarkilaris.Join(db.tblSanatcis, ss => ss.SanatciID, san => san.ID, (ss, san) => new { ss, san })
            .Join(db.tblSarkis, ss1 => ss1.ss.SarkiID, sar => sar.ID, (ss1, sar) => new { ss1, sar })
            .Join(db.tblAlbums, ss2 => ss2.ss1.ss.AlbumID, alb => alb.ID, (ss2, alb) => new { ss2, alb })
            .Where(x => x.ss2.ss1.ss.SanatciID == ID)
            .Select(m => new
            {
                ID = m.ss2.ss1.ss.ID,
                AlbumAd = m.alb.Ad,
                SanatciAd = m.ss2.ss1.san.Ad,
                SarkiAd = m.ss2.sar.Ad,
                Tarih = m.ss2.ss1.ss.Tarih,
                SarkiSure = m.ss2.ss1.ss.SarkiSure,
                Dnsayisi = m.ss2.ss1.ss.DinlenmeSayisi
            }).ToList();

            this.dataGridView5.DataSource = tablo;
        }

        private void dataGridView5_SelectionChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView5.SelectedRows)
            {
                ID = Convert.ToInt32(row.Cells[0].Value.ToString());
                textBox4.Text = row.Cells[5].Value.ToString();
                dateTimePicker2.Text = row.Cells[4].Value.ToString();


                //String sarkiAD = row.Cells[3].Value.ToString();
                //String albumAD = row.Cells[1].Value.ToString();


                //List<tblSarki> sarkilar = db.tblSarkis.ToList();
                //List<tblAlbum> albumler = db.tblAlbums.ToList();
                //int sarkiID = db.tblSarkis.Where(x => x.Ad == sarkiAD).Select(c => c.ID).FirstOrDefault();
                //int albumID = db.tblAlbums.Where(x => x.Ad == albumAD).Select(c => c.ID).FirstOrDefault();

                //comboBox4.DataSource = sarkilar;
                //comboBox4.ValueMember = "ID";
                //comboBox4.DisplayMember = "Ad";
                //comboBox4.SelectedIndex = sarkiID - 1;

                //comboBox5.DataSource = albumler;
                //comboBox5.ValueMember = "ID";
                //comboBox5.DisplayMember = "Ad";
                //comboBox5.SelectedIndex = albumID - 1;
            }
        }

        public void HatalariSil()
        {
            var changedEntriesCopy = db.ChangeTracker.Entries().Where(e => e.State == EntityState.Added || e.State == EntityState.Modified || e.State == EntityState.Deleted).ToList();

            foreach (var e in changedEntriesCopy)
                e.State = EntityState.Detached;
        }

    }
}
