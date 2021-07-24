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
    public partial class kullaniciFormu : Form
    {
        private db_muzikEntities db = new db_muzikEntities();
        private int ID;
        private int secilenKayitSayisi = 0;

        public kullaniciFormu()
        {
            InitializeComponent();
        }

        private void kullaniciFormu_Load(object sender, EventArgs e)
        {
            istatistikOlustur();
            KullaniciYukle();
            premTakipYukle();
            tumSarkiYukle();
            calmaListeleriniYukle();
            String kullaniciExtraBilgi = LogIn.kullaniciBilgi.AbonelikTur == 1 ? "Premium Kullanıcı" : "Normal Kullanıcı";
            



            if (LogIn.kullaniciBilgi.AbonelikTur != 1)
            {
                checkedListBox1.Visible = false;
                label8.Text = "Kullanıcı Adı:  " + LogIn.kullaniciBilgi.Ad + "  " + kullaniciExtraBilgi;
            }
            bool odendi = db.tblPremiumKullaniciOdendiBilgis.Where(x => x.KullaniciID == LogIn.kullaniciBilgi.ID).Select(z => z.Odendi).FirstOrDefault() ?? false;
            if (odendi)
            {
                checkedListBox1.SetItemCheckState(0, CheckState.Checked);
                kullaniciExtraBilgi = kullaniciExtraBilgi + " (Ücret Ödendi)";
                label8.Text = "Kullanıcı Adı:  " + LogIn.kullaniciBilgi.Ad + "  " + kullaniciExtraBilgi;
            }
            else
            {
                if (LogIn.kullaniciBilgi.AbonelikTur == 1)
                {
                    kullaniciExtraBilgi = kullaniciExtraBilgi + " (Ücret Ödenmedi)";
                    label8.Text = "Kullanıcı Adı:  " + LogIn.kullaniciBilgi.Ad + "  " + kullaniciExtraBilgi;
                    checkedListBox1.SetItemCheckState(0, CheckState.Unchecked);
                }
            }
        }

        private void tumSarkiYukle()
        {
            var tablo = db.tblSarkis
            .Join(db.tblSanatcininSarkilaris, sar => sar.ID, ss => ss.SarkiID, (sar, ss) => new { sar, ss })
            .Join(db.tblAlbums, ss1 => ss1.ss.AlbumID, alb => alb.ID, (ss1, alb) => new { ss1, alb })
            .Join(db.tblSanatcis, ss2 => ss2.ss1.ss.SanatciID, san => san.ID, (ss2, san) => new { ss2, san })
            .Join(db.tblSarkiTurs, sar1 => sar1.ss2.ss1.sar.Tur, sartur => sartur.ID, (sar1, sartur) => new { sar1, sartur })
            .OrderBy(y => y.sar1.ss2.ss1.sar.Ad).ThenBy(y => y.sartur.Tur)
            .Select(m => new
            {
                ID = m.sar1.ss2.ss1.ss.ID,
                Tur = m.sar1.ss2.ss1.sar.Tur,
                SarkiAd = m.sar1.ss2.ss1.sar.Ad,
                SanatciAd = m.sar1.san.Ad,
                SarkiTur = m.sartur.Tur,
                Sure = m.sar1.ss2.ss1.ss.SarkiSure,
                DAdedi = m.sar1.ss2.ss1.ss.DinlenmeSayisi,
            }).ToList();
            if (tablo != null)
            {
                this.dataGridView3.DataSource = tablo;
                dataGridView3.Columns[0].Visible = false;
                dataGridView3.Columns[1].Visible = false;
            }
        }

        private void premKullaniciSarkiYukle(int ID)
        {
            var tablo = db.tblKullaniciCalmaListesis.Join(db.tblCalmaListelerindekiSarkilars, kcl => kcl.ID, cls => cls.KullaniciCalmaListesiID, (kcl, cls) => new { kcl, cls })
            .Join(db.tblSanatcininSarkilaris, cls1 => cls1.cls.SarkiID, ss => ss.ID, (cls1, ss) => new { cls1, ss })
            .Join(db.tblSarkis, ss1 => ss1.ss.SarkiID, sar => sar.ID, (ss1, sar) => new { ss1, sar })
            .Join(db.tblAlbums, ss2 => ss2.ss1.ss.AlbumID, alb => alb.ID, (ss2, alb) => new { ss2, alb })
            .Join(db.tblSanatcis, ss3 => ss3.ss2.ss1.ss.SanatciID, san => san.ID, (ss3, san) => new { ss3, san })
            .Join(db.tblSarkiTurs, ss4 => ss4.ss3.ss2.sar.Tur, sartur => sartur.ID, (ss4, sartur) => new { ss4, sartur })
            .OrderBy(y => y.ss4.ss3.ss2.sar.Ad).ThenBy(y => y.sartur.ID)
            .Where(x => x.ss4.ss3.ss2.ss1.cls1.kcl.KullaniciID == ID)
            .Select(m => new
            {
                ID = m.ss4.ss3.ss2.ss1.ss.ID,
                Tur = m.ss4.ss3.ss2.ss1.cls1.kcl.CalmaListesiTuru,
                SarkiAd = m.ss4.ss3.ss2.sar.Ad,
                AlbumAd = m.ss4.ss3.alb.Ad,
                SanatciAd = m.ss4.san.Ad,
                SarkiTur = m.sartur.Tur,
                Sure = m.ss4.ss3.ss2.ss1.ss.SarkiSure,
                DAdedi = m.ss4.ss3.ss2.ss1.ss.DinlenmeSayisi
            }).ToList();
            if (tablo != null)
            {
                this.dataGridView7.DataSource = tablo;
                dataGridView7.Columns[0].Visible = false;
                dataGridView7.Columns[1].Visible = false;
            }
        }

        private void calmaListeleriniYukle()
        {
            var tablo = db.tblKullaniciCalmaListesis.Join(db.tblCalmaListelerindekiSarkilars, kcl => kcl.ID, cls => cls.KullaniciCalmaListesiID, (kcl, cls) => new { kcl, cls })
            .Join(db.tblSanatcininSarkilaris, cls1 => cls1.cls.SarkiID, ss => ss.ID, (cls1, ss) => new { cls1, ss })
            .Join(db.tblSarkis, ss1 => ss1.ss.SarkiID, sar => sar.ID, (ss1, sar) => new { ss1, sar })
            .Join(db.tblAlbums, ss2 => ss2.ss1.ss.AlbumID, alb => alb.ID, (ss2, alb) => new { ss2, alb })
            .Join(db.tblSanatcis, ss3 => ss3.ss2.ss1.ss.SanatciID, san => san.ID, (ss3, san) => new { ss3, san })
            .Join(db.tblSarkiTurs, ss4 => ss4.ss3.ss2.sar.Tur, sartur => sartur.ID, (ss4, sartur) => new { ss4, sartur })
            .OrderBy(y => y.ss4.ss3.ss2.sar.Ad).ThenBy(y => y.sartur.ID)
            .Where(x => x.ss4.ss3.ss2.ss1.cls1.kcl.KullaniciID == LogIn.kullaniciBilgi.ID && x.ss4.ss3.ss2.ss1.cls1.kcl.CalmaListesiTuru == 1)
            .Select(m => new
            {
                ID = m.ss4.ss3.ss2.ss1.ss.ID,
                SarkiAd = m.ss4.ss3.ss2.sar.Ad,
                AlbumAd = m.ss4.ss3.alb.Ad,
                SanatciAd = m.ss4.san.Ad,
                SarkiTur = m.sartur.Tur
            }).ToList();

            if (tablo != null)
            {
                this.dataGridView4.DataSource = tablo;
                dataGridView4.Columns[0].Visible = false;
            }


            var tablo1 = db.tblKullaniciCalmaListesis.Join(db.tblCalmaListelerindekiSarkilars, kcl => kcl.ID, cls => cls.KullaniciCalmaListesiID, (kcl, cls) => new { kcl, cls })
            .Join(db.tblSanatcininSarkilaris, cls1 => cls1.cls.SarkiID, ss => ss.ID, (cls1, ss) => new { cls1, ss })
            .Join(db.tblSarkis, ss1 => ss1.ss.SarkiID, sar => sar.ID, (ss1, sar) => new { ss1, sar })
            .Join(db.tblAlbums, ss2 => ss2.ss1.ss.AlbumID, alb => alb.ID, (ss2, alb) => new { ss2, alb })
            .Join(db.tblSanatcis, ss3 => ss3.ss2.ss1.ss.SanatciID, san => san.ID, (ss3, san) => new { ss3, san })
            .Join(db.tblSarkiTurs, ss4 => ss4.ss3.ss2.sar.Tur, sartur => sartur.ID, (ss4, sartur) => new { ss4, sartur })
            .OrderBy(y => y.ss4.ss3.ss2.sar.Ad).ThenBy(y => y.sartur.ID)
            .Where(x => x.ss4.ss3.ss2.ss1.cls1.kcl.KullaniciID == LogIn.kullaniciBilgi.ID && x.ss4.ss3.ss2.ss1.cls1.kcl.CalmaListesiTuru == 2)
            .Select(m => new
            {
                ID = m.ss4.ss3.ss2.ss1.ss.ID,
                SarkiAd = m.ss4.ss3.ss2.sar.Ad,
                AlbumAd = m.ss4.ss3.alb.Ad,
                SanatciAd = m.ss4.san.Ad,
                SarkiTur = m.sartur.Tur
            }).ToList();
            if (tablo1 != null)
            {
                this.dataGridView5.DataSource = tablo1;
                dataGridView5.Columns[0].Visible = false;
            }

            var tablo2 = db.tblKullaniciCalmaListesis.Join(db.tblCalmaListelerindekiSarkilars, kcl => kcl.ID, cls => cls.KullaniciCalmaListesiID, (kcl, cls) => new { kcl, cls })
            .Join(db.tblSanatcininSarkilaris, cls1 => cls1.cls.SarkiID, ss => ss.ID, (cls1, ss) => new { cls1, ss })
            .Join(db.tblSarkis, ss1 => ss1.ss.SarkiID, sar => sar.ID, (ss1, sar) => new { ss1, sar })
            .Join(db.tblAlbums, ss2 => ss2.ss1.ss.AlbumID, alb => alb.ID, (ss2, alb) => new { ss2, alb })
            .Join(db.tblSanatcis, ss3 => ss3.ss2.ss1.ss.SanatciID, san => san.ID, (ss3, san) => new { ss3, san })
            .Join(db.tblSarkiTurs, ss4 => ss4.ss3.ss2.sar.Tur, sartur => sartur.ID, (ss4, sartur) => new { ss4, sartur })
            .OrderBy(y => y.ss4.ss3.ss2.sar.Ad).ThenBy(y => y.sartur.ID)
            .Where(x => x.ss4.ss3.ss2.ss1.cls1.kcl.KullaniciID == LogIn.kullaniciBilgi.ID && x.ss4.ss3.ss2.ss1.cls1.kcl.CalmaListesiTuru == 3)
            .Select(m => new
            {
                ID = m.ss4.ss3.ss2.ss1.ss.ID,
                SarkiAd = m.ss4.ss3.ss2.sar.Ad,
                AlbumAd = m.ss4.ss3.alb.Ad,
                SanatciAd = m.ss4.san.Ad,
                SarkiTur = m.sartur.Tur
            }).ToList();
            if (tablo2 != null)
            {
                this.dataGridView6.DataSource = tablo2;
                dataGridView6.Columns[0].Visible = false;
            }
        }
        private void premTakipYukle()
        {
            var tablo = db.tblKullanicis.Join(db.tblTakipListesis, kul => kul.ID, tl => tl.TakipEdilenKullaniciID, (kul, tl) => new { kul, tl })
            .Where(x => x.tl.TakipEdenKullaniciID == LogIn.kullaniciBilgi.ID)
            .Select(m => new
            {
                KullaniciID = m.kul.ID,
                ID = m.tl.ID,
                Ad = m.kul.Ad,
                KullaniciTur = (m.kul.AbonelikTur == 1) ? "Premium" : "Normal"
            }).ToList();
            if (tablo != null)
            {
                this.dataGridView2.DataSource = tablo;
                dataGridView2.Columns[0].Visible = false;
                dataGridView2.Columns[1].Visible = false;
            }
        }

        private void KullaniciYukle()
        {

            var tablo = db.tblKullanicis.Where(x => x.AbonelikTur == 1)
             .Select(m => new
             {
                 ID = m.ID,
                 Ad = m.Ad,
                 KullaniciTur = (m.AbonelikTur == 1) ? "Premium" : "Normal"

             }).ToList();
            if (tablo != null)
            {
                this.dataGridView1.DataSource = tablo;
                dataGridView1.Columns[0].Visible = false;
            }
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                tblPremiumKullaniciOdendiBilgi odeme = db.tblPremiumKullaniciOdendiBilgis.Where(x => x.KullaniciID == LogIn.kullaniciBilgi.ID).FirstOrDefault();
                //if (checkedListBox1.Items[0].Selected())
                odeme.Odendi = true;
                db.SaveChanges();
                //checkedListBox1.Enabled = false;
                checkedListBox1.SetItemCheckState(0, CheckState.Checked);
            }
            catch (Exception mess)
            {
                HatalariSil();
                MessageBox.Show(mess.InnerException.InnerException.Message);
            }



        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            ID = 0;
            secilenKayitSayisi = dataGridView2.SelectedRows.Count;
            if (secilenKayitSayisi == 1)
            {
                foreach (DataGridViewRow row in dataGridView2.SelectedRows)
                {
                    ID = Convert.ToInt32(row.Cells[0].Value.ToString());
                }
                //premSarkiListeyeEkle(Tur);
                premKullaniciSarkiYukle(ID);
            }
        }

        private void dataGridView7_SelectionChanged(object sender, EventArgs e)
        {

        }
        private void dataGridView3_SelectionChanged(object sender, EventArgs e)
        {

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ID = 0;

            if (tabControl1.SelectedIndex == 0)
            {
                tumSarkiYukle();
                calmaListeleriniYukle();
            }

            if (tabControl1.SelectedIndex == 1)
            {
                KullaniciYukle();
                premTakipYukle();
            }

            else if (tabControl1.SelectedIndex == 2)
            {
                istatistikOlustur();
            }
        }

        private void sarkiEkle(int ID, int Tur)
        {
            try
            {
                int kullanniciCalmaListesiID;
                tblCalmaListelerindekiSarkilar calmaListesi = new tblCalmaListelerindekiSarkilar();
                kullanniciCalmaListesiID = db.tblKullaniciCalmaListesis
                                           .Where(z => z.CalmaListesiTuru == Tur && z.KullaniciID == LogIn.kullaniciBilgi.ID).Select(y => y.ID).FirstOrDefault();
                calmaListesi.KullaniciCalmaListesiID = kullanniciCalmaListesiID;
                calmaListesi.SarkiID = ID;

                db.tblCalmaListelerindekiSarkilars.Add(calmaListesi);
                db.SaveChanges();
            }
            catch (Exception mess)
            {
                HatalariSil();
                MessageBox.Show(mess.InnerException.InnerException.Message);
            }
        }
        private void premKullaniciEkle(int ID)
        {
            try
            {
                tblTakipListesi premTakip = new tblTakipListesi();
                premTakip.TakipEdenKullaniciID = LogIn.kullaniciBilgi.ID;
                premTakip.TakipEdilenKullaniciID = ID;
                db.tblTakipListesis.Add(premTakip);
                db.SaveChanges();
                premTakipYukle();
            }
            catch (Exception mess)
            {
                HatalariSil();
                MessageBox.Show(mess.InnerException.InnerException.Message);
            }

        }
        private void button1_Click(object sender, EventArgs e)
        {
            ID = 0;
            secilenKayitSayisi = dataGridView1.SelectedRows.Count;
            if (secilenKayitSayisi == 1)
            {
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    ID = Convert.ToInt32(row.Cells[0].Value.ToString());
                }
                premKullaniciEkle(ID);
                premTakipYukle();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ID = 0;
            int Tur = 0;
            secilenKayitSayisi = dataGridView7.SelectedRows.Count;
            if (secilenKayitSayisi > 0)
            {
                foreach (DataGridViewRow row in dataGridView7.SelectedRows)
                {
                    ID = Convert.ToInt32(row.Cells[0].Value.ToString());
                    Tur = Convert.ToInt32(row.Cells[1].Value.ToString());
                    sarkiEkle(ID, Tur);
                }
                calmaListeleriniYukle();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ID = 0;
            int Tur = 0;
            secilenKayitSayisi = dataGridView3.SelectedRows.Count;
            if (secilenKayitSayisi > 0)
            {
                foreach (DataGridViewRow row in dataGridView3.SelectedRows)
                {
                    ID = Convert.ToInt32(row.Cells[0].Value.ToString());
                    Tur = Convert.ToInt32(row.Cells[1].Value.ToString());
                    sarkiEkle(ID, Tur);
                }
                calmaListeleriniYukle();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView2_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            ID = 0;
            secilenKayitSayisi = dataGridView2.SelectedRows.Count;
            if (secilenKayitSayisi == 1)
            {
                foreach (DataGridViewRow row in dataGridView2.SelectedRows)
                {
                    ID = Convert.ToInt32(row.Cells[1].Value.ToString());
                }
                try
                {
                    tblTakipListesi takipListe = db.tblTakipListesis.Where(z => z.ID == ID).FirstOrDefault();
                    if (takipListe != null)
                    {
                        db.tblTakipListesis.Remove(takipListe);
                        db.SaveChanges();
                        premKullaniciSarkiYukle(0);
                        premTakipYukle();
                    }
                }
                catch (Exception mess)
                {
                    HatalariSil();
                    MessageBox.Show(mess.InnerException.InnerException.Message);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ID = 0;
            secilenKayitSayisi = dataGridView4.SelectedRows.Count;
            if (secilenKayitSayisi == 1)
            {
                foreach (DataGridViewRow row in dataGridView4.SelectedRows)
                {
                    ID = Convert.ToInt32(row.Cells[0].Value.ToString());
                }

                try
                {
                    tblSanatcininSarkilari sanatciSarkilari = db.tblSanatcininSarkilaris.Where(z => z.ID == ID).FirstOrDefault();
                    sanatciSarkilari.DinlenmeSayisi = sanatciSarkilari.DinlenmeSayisi + 1;
                    db.SaveChanges();
                    tumSarkiYukle();
                }
                catch (Exception mess)
                {
                    HatalariSil();
                    MessageBox.Show(mess.InnerException.InnerException.Message);
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ID = 0;
            secilenKayitSayisi = dataGridView5.SelectedRows.Count;
            if (secilenKayitSayisi == 1)
            {
                foreach (DataGridViewRow row in dataGridView5.SelectedRows)
                {
                    ID = Convert.ToInt32(row.Cells[0].Value.ToString());
                }

                try
                {
                    tblSanatcininSarkilari sanatciSarkilari = db.tblSanatcininSarkilaris.Where(z => z.ID == ID).FirstOrDefault();
                    sanatciSarkilari.DinlenmeSayisi = sanatciSarkilari.DinlenmeSayisi + 1;
                    db.SaveChanges();
                    tumSarkiYukle();
                }
                catch (Exception mess)
                {
                    HatalariSil();
                    MessageBox.Show(mess.InnerException.InnerException.Message);
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ID = 0;
            secilenKayitSayisi = dataGridView6.SelectedRows.Count;
            if (secilenKayitSayisi == 1)
            {
                foreach (DataGridViewRow row in dataGridView6.SelectedRows)
                {
                    ID = Convert.ToInt32(row.Cells[0].Value.ToString());
                }
                try
                {
                    tblSanatcininSarkilari sanatciSarkilari = db.tblSanatcininSarkilaris.Where(z => z.ID == ID).FirstOrDefault();
                    sanatciSarkilari.DinlenmeSayisi = sanatciSarkilari.DinlenmeSayisi + 1;
                    db.SaveChanges();
                    tumSarkiYukle();
                }
                catch (Exception mess)
                {
                    HatalariSil();
                    MessageBox.Show(mess.InnerException.InnerException.Message);
                }
            }
        }

        private void dataGridView4_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            ID = 0;
            secilenKayitSayisi = dataGridView4.SelectedRows.Count;
            if (secilenKayitSayisi == 1)
            {
                foreach (DataGridViewRow row in dataGridView4.SelectedRows)
                {
                    ID = Convert.ToInt32(row.Cells[0].Value.ToString());
                }
                try
                {
                    tblCalmaListelerindekiSarkilar calmaListe = db.tblCalmaListelerindekiSarkilars.Where(z => z.SarkiID == ID).FirstOrDefault();
                    if (calmaListe != null)
                    {
                        db.tblCalmaListelerindekiSarkilars.Remove(calmaListe);
                        db.SaveChanges();
                        calmaListeleriniYukle();
                    }
                }
                catch (Exception mess)
                {
                    HatalariSil();
                    MessageBox.Show(mess.InnerException.InnerException.Message);
                }
            }

        }

        private void dataGridView5_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            ID = 0;
            secilenKayitSayisi = dataGridView5.SelectedRows.Count;
            if (secilenKayitSayisi == 1)
            {
                foreach (DataGridViewRow row in dataGridView5.SelectedRows)
                {
                    ID = Convert.ToInt32(row.Cells[0].Value.ToString());
                }
                try
                {
                    tblCalmaListelerindekiSarkilar calmaListe = db.tblCalmaListelerindekiSarkilars.Where(z => z.SarkiID == ID).FirstOrDefault();
                    if (calmaListe != null)
                    {
                        db.tblCalmaListelerindekiSarkilars.Remove(calmaListe);
                        db.SaveChanges();
                        calmaListeleriniYukle();
                    }
                }
                catch (Exception mess)
                {
                    HatalariSil();
                    MessageBox.Show(mess.InnerException.InnerException.Message);
                }
            }
        }

        private void dataGridView6_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            ID = 0;
            secilenKayitSayisi = dataGridView6.SelectedRows.Count;
            if (secilenKayitSayisi == 1)
            {
                foreach (DataGridViewRow row in dataGridView6.SelectedRows)
                {
                    ID = Convert.ToInt32(row.Cells[0].Value.ToString());
                }
                try
                {
                    tblCalmaListelerindekiSarkilar calmaListe = db.tblCalmaListelerindekiSarkilars.Where(z => z.SarkiID == ID).FirstOrDefault();
                    if (calmaListe != null)
                    {
                        db.tblCalmaListelerindekiSarkilars.Remove(calmaListe);
                        db.SaveChanges();
                        calmaListeleriniYukle();
                    }
                }
                catch (Exception mess)
                {
                    HatalariSil();
                    MessageBox.Show(mess.InnerException.InnerException.Message);
                }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            var tablo = db.tblKullaniciCalmaListesis.Join(db.tblCalmaListelerindekiSarkilars, kcl => kcl.ID, cls => cls.KullaniciCalmaListesiID, (kcl, cls) => new { kcl, cls })
            .Where(x => x.kcl.KullaniciID == LogIn.kullaniciBilgi.ID)
            .Select(m => new
            {
                ID = m.cls.ID
            }).ToList();

            if (tablo != null)
            {
                foreach (var deger in tablo)
                {
                    try
                    {
                        tblCalmaListelerindekiSarkilar calmaListe = db.tblCalmaListelerindekiSarkilars.Where(z => z.ID == deger.ID).FirstOrDefault();
                        if (calmaListe != null)
                        {
                            db.tblCalmaListelerindekiSarkilars.Remove(calmaListe);
                            db.SaveChanges();
                        }
                    }
                    catch (Exception mess)
                    {
                        HatalariSil();
                        MessageBox.Show(mess.InnerException.InnerException.Message);
                    }
                }
                calmaListeleriniYukle();
            }

        }

        private void istatistikOlustur()
        {
            var tablo = db.tblSanatcininSarkilaris
            .Join(db.tblSarkis, ss => ss.SarkiID, sar => sar.ID, (ss, sar) => new { ss, sar })
            .Join(db.tblSanatcis, ss1 => ss1.ss.SanatciID, san => san.ID, (ss1, san) => new { ss1, san })
            .GroupBy(m => new { m.ss1.sar.Ad, m.san.ID, m.ss1.ss.DinlenmeSayisi })
            .Select(m => new
            {
                SarkiAd = m.Key.Ad,
                SanatciAd = db.tblSanatcis.Where(x => x.ID == m.Key.ID).Select(y => y.Ad).FirstOrDefault(),
                TDSayisi = m.Key.DinlenmeSayisi
            }).OrderByDescending(z => z.TDSayisi).Take(10).ToList();

            var tablo1 = db.tblSanatcininSarkilaris
            .Join(db.tblSarkis, ss => ss.SarkiID, sar => sar.ID, (ss, sar) => new { ss, sar })
            .Join(db.tblSanatcis, ss1 => ss1.ss.SanatciID, san => san.ID, (ss1, san) => new { ss1, san })            
            .Join(db.tblUlkes, ss2 => ss2.san.UlkeID, ulk => ulk.ID, (ss2, ulk) => new {ss2, ulk})
            .GroupBy(m => new { m.ulk.UlkeIsmi, m.ss2.ss1.sar.Ad, m.ss2.ss1.ss.DinlenmeSayisi })
            .Select(m => new
            {                
                SarkiAdi = m.Key.Ad,
                UlkeAdi = m.Key.UlkeIsmi,
                TDSayisi = m.Key.DinlenmeSayisi
            }).OrderByDescending(z => z.TDSayisi).Take(10).ToList();

            var tablo2 = db.tblSanatcininSarkilaris
            .Join(db.tblSarkis, ss => ss.SarkiID, sar => sar.ID, (ss, sar) => new { ss, sar })
            .Join(db.tblSarkiTurs, ss1 => ss1.sar.Tur, sarturl => sarturl.ID, (ss1, sartur) => new { ss1, sartur })
            .Where(x => x.sartur.ID == 1)
            .GroupBy(m => new { m.sartur.Tur, m.ss1.sar.Ad, m.ss1.ss.DinlenmeSayisi })
            .Select(m => new
            {
                MuzikTur = m.Key.Tur,
                SarkiAdi = m.Key.Ad,
                TDSayisi = m.Key.DinlenmeSayisi
            }).OrderByDescending(z => z.TDSayisi).Take(10).ToList();

            var tablo3 = db.tblSanatcininSarkilaris
            .Join(db.tblSarkis, ss => ss.SarkiID, sar => sar.ID, (ss, sar) => new { ss, sar })
            .Join(db.tblSarkiTurs, ss1 => ss1.sar.Tur, sarturl => sarturl.ID, (ss1, sartur) => new { ss1, sartur })
            .Where(x => x.sartur.ID == 2)
            .GroupBy(m => new { m.sartur.Tur, m.ss1.sar.Ad, m.ss1.ss.DinlenmeSayisi })
            .Select(m => new
            {
                MuzikTur = m.Key.Tur,
                SarkiAdi = m.Key.Ad,
                TDSayisi = m.Key.DinlenmeSayisi
            }).OrderByDescending(z => z.TDSayisi).Take(10).ToList();

            var tablo4 = db.tblSanatcininSarkilaris
            .Join(db.tblSarkis, ss => ss.SarkiID, sar => sar.ID, (ss, sar) => new { ss, sar })
            .Join(db.tblSarkiTurs, ss1 => ss1.sar.Tur, sarturl => sarturl.ID, (ss1, sartur) => new { ss1, sartur })
            .Where(x => x.sartur.ID == 3)
            .GroupBy(m => new { m.sartur.Tur, m.ss1.sar.Ad, m.ss1.ss.DinlenmeSayisi })
            .Select(m => new
            {
                MuzikTur = m.Key.Tur,
                SarkiAdi = m.Key.Ad,
                TDSayisi = m.Key.DinlenmeSayisi
            }).OrderByDescending(z => z.TDSayisi).Take(10).ToList();
            this.dataGridView8.DataSource = tablo;
            this.dataGridView9.DataSource = tablo2;
            this.dataGridView10.DataSource = tablo1;
            this.dataGridView11.DataSource = tablo3;
            this.dataGridView12.DataSource = tablo4;
        }

        public void HatalariSil()
        {
            var changedEntriesCopy = db.ChangeTracker.Entries().Where(e => e.State == EntityState.Added || e.State == EntityState.Modified || e.State == EntityState.Deleted).ToList();

            foreach (var e in changedEntriesCopy)
                e.State = EntityState.Detached;
        }


    }
}
