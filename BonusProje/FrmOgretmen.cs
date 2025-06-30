using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BonusProje
{
    public partial class FrmOgretmen : Form
    {
        public FrmOgretmen()
        {
            InitializeComponent();
        }

        private void BtnKulupİslemleri_Click(object sender, EventArgs e)
        {
            FrmKulupIslemleri frm = new FrmKulupIslemleri();
            frm.Show();
        }

        private void FrmOgretmen_Load(object sender, EventArgs e)
        {

        }

        private void BtnDersİslemleri_Click(object sender, EventArgs e)
        {
            FrmDersler frm = new FrmDersler();
            frm.Show();
        }

        private void BtnOgrenciİslemleri_Click(object sender, EventArgs e)
        {
            FrmOgrenciler frmOgrenciler = new FrmOgrenciler();
            frmOgrenciler.Show();
        }

        private void BtnSınavNotları_Click(object sender, EventArgs e)
        {
            FrmSınavNotlar frm = new FrmSınavNotlar();
            frm.Show();
        }
    }
}
