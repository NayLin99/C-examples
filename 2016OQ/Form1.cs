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

namespace _2016OQ
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        static string constr = @"Data Source=(localDB)\v11.0;Initial Catalog=std;Integrated Security=True;Pooling=False";
        SqlConnection cn = new SqlConnection(constr);


        private void btnDisplay_Click(object sender, EventArgs e)
        {
            cn.Open();
            string query = "select * from staffOQ2016";
            SqlDataAdapter da = new SqlDataAdapter(query, cn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataTable dt = ds.Tables[0];
            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.DataSource = dt;
            cn.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            cn.Open();
            string query = "select count(sid) from staffOQ2016 where sid=@sid";
            SqlCommand cmd = new SqlCommand(query, cn);
            cmd.Parameters.AddWithValue("@sid", txtSid.Text);
            int row = (int)cmd.ExecuteScalar();
            if(row>0)
            {
                double sal = Convert.ToDouble(txtSalary.Text);
                double bonus = 0.0;
                double totalsal = 0.0;
                if(sal>100000)
                {
                    bonus = 0.1;
                    totalsal = (sal * bonus) + sal;
                }
                else if(sal>=50000&&sal<=100000)
                {
                    bonus = 0.05;
                    totalsal = (sal * bonus) + sal;
                }
                DialogResult yn = MessageBox.Show("Are you sure you want to update?", "Confirm", MessageBoxButtons.YesNo);
                if(yn==DialogResult.Yes)
                {
                    cmd.Parameters.Clear();
                    cmd.CommandText = "update staffOQ2016 set salary=@salary,bonus=@bonus,totalpay=@totalpay where sid=@sid";
                    cmd.Parameters.AddWithValue("@salary", sal);
                    cmd.Parameters.AddWithValue("@bonus", bonus);
                    cmd.Parameters.AddWithValue("@totalpay", totalsal);
                    cmd.Parameters.AddWithValue("@sid", txtSid.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Updated!");
                }
            }
            else
            {
                MessageBox.Show("Not found in table");
            }
            cn.Close();
        }
    }
}
