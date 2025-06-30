using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Data.SqlClient; // SqlConnection, SqlCommand gibi sınıfları kullanmak için gerekli kütüphane

namespace BonusProje
{
    public partial class FrmOgrenciler : Form
    {
        public FrmOgrenciler()
        {
            InitializeComponent();
        }
        sqlBaglantisi bgl = new sqlBaglantisi(); // Sql bağlantısı için tanımlama
        DataSet1TableAdapters.DataTable1TableAdapter ds = new DataSet1TableAdapters.DataTable1TableAdapter();
        string cinsiyet = "";
        string cinsiyetDegeri()
        {
            if (RbErkek.Checked == true)
            {
                cinsiyet = "ERKEK";
            }
            if (RbKız.Checked == true)
            {
                cinsiyet = "KIZ";
            }

            return cinsiyet;
        }
        void listele()
        {
            dataGridView1.DataSource = ds.OgrenciListesi();
        }

        // Dataset class içinde
        public void NotlariSil(int ogrenciId)
        {
            string query = "DELETE FROM TBL_NOTLAR WHERE OGRENCIID = @OgrenciId";
            SqlCommand cmd = new SqlCommand(query, bgl.baglanti());
            cmd.Parameters.AddWithValue("@OgrenciId", ogrenciId);

            cmd.ExecuteNonQuery();
        
        }
        private void FrmOgrenciler_Load(object sender, EventArgs e)
        {
            listele();
            // Kulüp listesini ComboBox'a yükle
          
           
            SqlCommand komut = new SqlCommand("SELECT * FROM TBL_KULUPLER ", bgl.baglanti());
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            CmbKulüp.DisplayMember = "KULUPAD"; // Kulüp adı görünecek
            CmbKulüp.ValueMember = "KULUPID";   // ID değeri kullanılacak
            CmbKulüp.DataSource = dt;

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex; // Seçilen hücrenin satır indeksini al
            TxtOgrenciid.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString(); // İlk hücredeki değeri al
            TxOgrenciAdı.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString(); // İkinci hücredeki değeri al
            TxtOgrenciSoyad.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString(); // Üçüncü hücredeki değeri al
            CmbKulüp.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();

            if (dataGridView1.Rows[secilen].Cells[4].Value.ToString() == "ERKEK")
            {
                RbErkek.Checked = true; // Erkek seçeneğini işaretle
            }
            else if (dataGridView1.Rows[secilen].Cells[4].Value.ToString() == "KIZ")
            {
                RbKız.Checked = true; // Kadın seçeneğini işaretle
            }


        }
   
        private void BtnEkle_Click(object sender, EventArgs e)
        {
       
        

            // Seçili kulüp ID'sini al
            if (CmbKulüp.SelectedValue != null)
            {
              
                ds.OgrenciEkle(TxOgrenciAdı.Text, TxtOgrenciSoyad.Text, byte.Parse(CmbKulüp.SelectedValue.ToString()), cinsiyetDegeri());
                MessageBox.Show("Öğrenci başarıyla eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
            }
            else
            {
                MessageBox.Show("Lütfen bir kulüp seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void CmbKulüp_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            int ogrenciId = int.Parse(TxtOgrenciid.Text);
            NotlariSil(ogrenciId); // Önce notları sil
            ds.OgrSil(ogrenciId);  // Sonra öğrenciyi sil
            listele();             // Listeyi güncelle


            //      listele(); // Listeyi güncelle
        }

        private void BtnGüncelle_Click(object sender, EventArgs e)
        {
        
            ds.OgrGüncelle(TxOgrenciAdı.Text, TxtOgrenciSoyad.Text,byte.Parse(CmbKulüp.SelectedValue.ToString()), cinsiyetDegeri(), Int32.Parse(TxtOgrenciid.Text));
            MessageBox.Show("Öğrenci başarıyla güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele(); // Listeyi güncelle
        }

        private void BtnAra_Click(object sender, EventArgs e)
        {
        
       dataGridView1.DataSource=     ds.OgrArama(TxtAra.Text);
            MessageBox.Show("Öğrenci başarıyla bulundu.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
          

        }
    }
}
