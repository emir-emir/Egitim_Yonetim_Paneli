﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace BonusProje
{
    internal class sqlBaglantisi
    {
        public SqlConnection baglanti()
        {
            SqlConnection baglan = new SqlConnection("Data Source=DESKTOP-0314TH0\\SQLEXPRESS;Initial Catalog=BonusOkul;Integrated Security=True;");
            baglan.Open();
            return baglan;

        }

    }
}
