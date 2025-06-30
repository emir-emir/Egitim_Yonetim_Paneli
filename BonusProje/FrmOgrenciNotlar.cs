using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BonusProje
{
    public partial class FrmOgrenciNotlar : Form
    {
        public FrmOgrenciNotlar()
        {
            InitializeComponent();
        }
        sqlBaglantisi bgl = new sqlBaglantisi();
        public string numara;
        public string adSoyad;
        private void FrmOgrenciNotlar_Load(object sender, EventArgs e)
        {


            SqlCommand komut = new SqlCommand("select DERSAD, SINAV1, SINAV2, SINAV3, PROJE, ORTALAMA, TBL_NOTLAR.DURUM from TBL_NOTLAR INNER JOIN TBL_DERSLER ON TBL_NOTLAR.DERSID = TBL_DERSLER.DERSID where OGRENCIID=@p1", bgl.baglanti());

            komut.Parameters.AddWithValue("@p1", numara);

            this.Text = adSoyad + " Öğrencinin Notları";


            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            bgl.baglanti().Close();
         
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

