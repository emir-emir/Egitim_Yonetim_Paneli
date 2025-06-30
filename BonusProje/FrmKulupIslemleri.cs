using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates; // SqlConnection, SqlCommand gibi sınıfları kullanmak için gerekli kütüphane

namespace BonusProje
{
    public partial class FrmKulupIslemleri : Form
    {
        public FrmKulupIslemleri()
        {
            InitializeComponent();
            
        }
         sqlBaglantisi bgl= new sqlBaglantisi(); // Sql bağlantısı için tanımlama
      public  void listele()
        {

            SqlCommand komut = new SqlCommand("SELECT * FROM TBL_KULUPLER  where Durum=1 ", bgl.baglanti());
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt; // DataGridView'e verileri ata

        }
        private void BtnListele_Click(object sender, EventArgs e)
        {
            listele();
        }

        private void BtnEkle_Click(object sender, EventArgs e)
        {
          
            SqlCommand komut = new SqlCommand("INSERT INTO TBL_KULUPLER (KULUPAD, Durum) VALUES (@p1, 1)", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", TxtKulüpAdı.Text);
           
            try
            {
                if (string.IsNullOrEmpty(TxtKulüpAdı.Text)) // Kulüp adı boş mu kontrol et
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
            MessageBox.Show("Kulüp başarıyla eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
           bgl.baglanti().Close(); // Bağlantıyı kapat
            listele(); // Listeyi güncelle
            TxtKulüpAdı.Clear(); // TextBox'ı temizle
            TxtKulupid.Clear(); // Kulüp ID'sini temizle
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
             int secilen = dataGridView1.SelectedCells[0].RowIndex; // Seçilen hücrenin satır indeksini al
            TxtKulupid.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString(); // İlk hücredeki değeri al
           TxtKulüpAdı.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString(); // İkinci hücredeki değeri al
            TxtDurum.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString(); // Üçüncü hücredeki değeri al
            // DataGridView'de bir hücreye tıklandığında ilgili bilgileri TextBox'lara ata

        }

        private void BtnGüncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("UPDATE TBL_KULUPLER SET KULUPAD=@p1,DURUM=@P2 WHERE KULUPID=@p3", bgl.baglanti());
            
            komut.Parameters.AddWithValue("@p1", TxtKulüpAdı.Text); // Güncellenecek kulüp adını al
            komut.Parameters.AddWithValue("@p2", TxtDurum.Text); // Güncellenecek kulüp ID'sini al
            komut.Parameters.AddWithValue("@p3", TxtKulupid.Text); // Güncellenecek kulüp ID'sini al
            komut.ExecuteNonQuery();
            MessageBox.Show("Kulüp başarıyla güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele(); // Listeyi güncelle
            TxtKulüpAdı.Clear(); // TextBox'ı temizle
            TxtKulupid.Clear(); // Kulüp ID'sini temizle
            bgl.baglanti().Close(); // Bağlantıyı kapat
         
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Update TBL_KULUPLER set Durum=0 where KULUPID=@p1", bgl.baglanti());
        
            komut.Parameters.AddWithValue("@p1", TxtKulupid.Text); // Silinecek kulüp ID'sini al
            komut.ExecuteNonQuery();
            MessageBox.Show("Kulüp başarıyla silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele(); // Listeyi güncelle
            TxtKulüpAdı.Clear(); // TextBox'ı temizle
            TxtKulupid.Clear(); // Kulüp ID'sini temizle
            bgl.baglanti().Close(); // Bağlantıyı kapat
        }

        private void FrmKulupIslemleri_Load(object sender, EventArgs e)
        {
            listele();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            Application.Exit(); // Uygulamadan çıkış yap
        }

        private void pictureBox6_MouseHover(object sender, EventArgs e)
        {
            pictureBox6.BackColor = Color.Red; // Fare imleci resmin üzerine geldiğinde arka plan rengini kırmızı yap
        }

        private void pictureBox6_MouseLeave(object sender, EventArgs e)
        {
            pictureBox6.BackColor = Color.Transparent; // Fare imleci resmin üzerinden ayrıldığında arka plan rengini şeffaf yap
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
 