using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BonusProje
{
    public partial class FrmGirisPaneli : Form
    {
        public FrmGirisPaneli()
        {
            InitializeComponent();
        }
        sqlBaglantisi bgl = new sqlBaglantisi();
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
            FrmOgrenciNotlar frm = new FrmOgrenciNotlar();
      
            frm.numara = TxtNumara.Text;
            
            SqlCommand komut2 = new SqlCommand("select  OGRAD,OGRSOYAD from TBL_OGRENCILER where OGRID=@p2", bgl.baglanti());
            komut2.Parameters.AddWithValue("@p2", TxtNumara.Text);
            SqlDataReader dr = komut2.ExecuteReader();

      
            if (dr.Read())
            {
 
             //   frm.ad = dr["OGRSOYAD"].ToString(); // veya bu kullanım frm.ad = dr[0].ToString();
                frm.adSoyad = dr["OGRAD"].ToString() + "- " + dr["OGRSOYAD"].ToString();
            }
            dr.Close();

            frm.Show();



        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            FrmOgretmen frm = new FrmOgretmen();
            frm.Show();
        }

        private void FrmGirisPaneli_Load(object sender, EventArgs e)
        {

        }
    }
   
}
