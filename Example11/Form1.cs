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

namespace Example11
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void txtinsert_Click(object sender, EventArgs e)
        {
            String constr = @"Data Source=(localDB)\v11.0;Initial Catalog=std;Integrated Security=True;Pooling=False";
            SqlConnection cn = new SqlConnection(constr);
            cn.Open();
            String query = "Insert into product(pid,pname,price) values (@id,@name,@price)";
            SqlCommand cmd = new SqlCommand(query, cn);

            cmd.Parameters.AddWithValue("@id", txtid.Text);
            cmd.Parameters.AddWithValue("@name", txtname.Text);
            cmd.Parameters.AddWithValue("@price", Convert.ToInt32(txtprice.Text));
            cmd.ExecuteNonQuery();
            MessageBox.Show("Inserted");

            String q = "Select * from product";
            String str = "";
            cmd = new SqlCommand(q, cn);
            SqlDataReader rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
                str += rdr[0].ToString() + rdr[1].ToString() + rdr[2].ToString() + "\n";
            }
            MessageBox.Show(str);
            rdr.Close();
            cn.Close();
        }
    }
}
