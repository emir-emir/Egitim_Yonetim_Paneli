using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using  System.Data.SqlClient; // SqlConnection, SqlCommand gibi sınıfları kullanmak için gerekli kütüphane
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace BonusProje
{
    public partial class FrmDersler : Form
    {
        public FrmDersler()
        {
            InitializeComponent();
        }
        DataSet1TableAdapters.TBL_DERSLERTableAdapter dt = new DataSet1TableAdapters.TBL_DERSLERTableAdapter();
        sqlBaglantisi bgl = new sqlBaglantisi(); // Sql bağlantısı için tanımlama

        void listele()
        {
            //  dataGridView1.DataSource = dt.DersListesi(); // DataGridView'e ders listesini ata
            SqlCommand komut = new SqlCommand("SELECT * FROM TBL_DERSLER  where Durum=1 ", bgl.baglanti());
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt; // DataGridView'e verileri ata
        }
        private void FrmDersler_Load(object sender, EventArgs e)
        {
            listele();

            // Form tasarımında veya kod ile
          

        }

        private void BtnListele_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = dt.DersListesi(); // Listele butonuna tıklandığında ders listesini güncelle
        }

        private void BtnEkle_Click(object sender, EventArgs e)
        {

            SqlCommand komut = new SqlCommand("INSERT INTO TBL_DERSLER(DERSAD, Durum) VALUES (@p1, 1)", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", TxDersAdı.Text);

            try
            {
                if (string.IsNullOrEmpty(TxDersAdı.Text)) // Kulüp adı boş mu kontrol et
                {
                    MessageBox.Show("Lütfen kulüp adını giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // Boş ise işlemi sonlandır
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Hata durumunda işlemi sonlandır
            }

            komut.ExecuteNonQuery(); // Sorguyu çalıştır
            MessageBox.Show("DERS başarıyla eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            bgl.baglanti().Close(); // Bağlantıyı kapat
            listele(); // Listeyi güncelle
            TxDersAdı.Clear(); // TextBox'ı temizle
            TxtDersid.Clear(); // Kulüp ID'sini temizle
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Update TBL_DERSLER set Durum=0 where DERSID=@p1", bgl.baglanti());

            komut.Parameters.AddWithValue("@p1", TxtDersid.Text); // Silinecek kulüp ID'sini al
            komut.ExecuteNonQuery();
            MessageBox.Show("Kulüp başarıyla silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele(); // Listeyi güncelle
            TxDersAdı.Clear(); // TextBox'ı temizle
            TxtDersid.Clear(); // Kulüp ID'sini temizle
            bgl.baglanti().Close(); // Bağlantıyı kapat

            /*      if (byte.TryParse(TxtDersid.Text, out byte dersId))
                  {
                      dt.DersSil(dersId); // Ders silme işlemi
                      TxtDersid.Clear(); // Ders ID metin kutusunu temizle
                      TxDersAdı.Clear(); // Ders adı metin kutusunu temizle
                      listele(); // Listeyi güncelle
                      MessageBox.Show("Ders başarıyla silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                  }
                  else
                  {
                      MessageBox.Show("Geçerli bir ders ID'si giriniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                  }
            */

            /*   dt.DersSil(byte.Parse(TxtDersid.Text)); // Ders silme işlemi
               TxtDersid.Clear(); // Ders ID metin kutusunu temizle
               TxDersAdı.Clear(); // Ders adı metin kutusunu temizle
               listele();
               MessageBox.Show("Ders başarıyla silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            */
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secili = dataGridView1.SelectedCells[0].RowIndex; // Seçilen satırın indeksini al
            TxtDersid.Text = dataGridView1.Rows[secili].Cells[0].Value.ToString(); // Ders ID'yi al ve metin kutusuna ata
            TxDersAdı.Text = dataGridView1.Rows[secili].Cells[1].Value.ToString(); // Ders adını al ve metin kutusuna ata

        }

        private void BtnGüncelle_Click(object sender, EventArgs e)
        {
            dt.DersGüncelle(TxDersAdı.Text, byte.Parse(TxtDersid.Text));
            TxtDersid.Clear(); // Ders ID metin kutusunu temizle
            TxDersAdı.Clear(); // Ders adı metin kutusunu temizle
            MessageBox.Show("Ders başarıyla güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
           Application.Exit(); // Uygulamadan çık
        }
    }
}
