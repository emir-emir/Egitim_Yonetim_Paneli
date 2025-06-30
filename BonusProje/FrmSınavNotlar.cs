using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BonusProje
{
    public partial class FrmSınavNotlar : Form
    {
        public FrmSınavNotlar()
        {
            InitializeComponent();
        }

        sqlBaglantisi bgl = new sqlBaglantisi(); // Sql bağlantısı için tanımlama

        DataSet1TableAdapters.TBL_NOTLARTableAdapter ds = new DataSet1TableAdapters.TBL_NOTLARTableAdapter(); // Notlar için TableAdapter tanımlama
        private void BtnAra_Click(object sender, EventArgs e)
        {

            dataGridView1.DataSource = ds.NotListesi(int.Parse(TxtOgrenciid.Text)); // DataGridView'e not listesini ata
            if (dataGridView1.Rows.Count == 0) // Eğer not bulunamazsa
            {
                MessageBox.Show("Öğrenciye ait not bulunamadı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                MessageBox.Show("Notlar başarıyla listelendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void FrmSınavNotlar_Load(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("SELECT * FROM TBL_DERSLER", bgl.baglanti());
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            CmbDers.DisplayMember = "DERSAD"; // Kulüp adı görünecek
            CmbDers.ValueMember = "DERSID";   // ID değeri kullanılacak
            CmbDers.DataSource = dt;
        }
        int notid; // Not ID'si için tanımlama

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           int.Parse( dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());


            notid = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
            TxtOgrenciid.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                TxtSınav1.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString(); // Sınav 1 notunu al
            TxtSınav2.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString(); // Sınav 1 notunu al
            TxtSınav3.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            TxtProje.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
            TxtOrtalama.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
            TxtDurum.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();


        }

        private void BtnHesapla_Click(object sender, EventArgs e)
        {
            int sinav1, sinav2, sinav3, proje;
            double ortalama;
            string durum;
            sinav1 = Convert.ToInt32(TxtSınav1.Text); // Sınav 1 notunu al
            sinav2 = Convert.ToInt32(TxtSınav2.Text); // Sınav 2 notunu al
            sinav3 = Convert.ToInt32(TxtSınav3.Text); // Sınav 3 notunu al
            proje = Convert.ToInt32(TxtProje.Text); // Proje notunu al
            ortalama = (sinav1 + sinav2 + sinav3 + proje) / 4; // Ortalama hesapla
            TxtOrtalama.Text = ortalama.ToString(); // Ortalama değerini TextBox'a ata

            if (ortalama >= 50) // Eğer ortalama 50 ve üzeri ise
            {
                durum = "True"; // Durum "Geçti"
            }
            else
            {
                durum = "False"; // Durum "Kaldı"
            }
            TxtDurum.Text = durum; // Durum TextBox'ını temizle
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
       
            ds.NotGüncelle(
                byte.Parse(CmbDers.SelectedValue.ToString()),
                int.Parse(TxtOgrenciid.Text),
                byte.Parse(TxtSınav1.Text),
                byte.Parse(TxtSınav2.Text),
                byte.Parse(TxtSınav3.Text),
                byte.Parse(TxtProje.Text, CultureInfo.InvariantCulture),
              decimal.Parse(TxtOrtalama.Text, CultureInfo.InvariantCulture),
            bool.Parse(TxtDurum.Text),
                byte.Parse(notid.ToString())
            );
            MessageBox.Show("Not güncelleme işlemi başarılı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            dataGridView1.DataSource = ds.NotListesi(int.Parse(TxtOgrenciid.Text)); // Güncellenen notları DataGridView'e ata

        }
    }
}
