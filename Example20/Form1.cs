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

namespace Example20
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        static string constr = @"Data Source=(localDB)\v11.0;Initial Catalog=std;Integrated Security=True;Pooling=False";
        SqlConnection cn = new SqlConnection(constr);
        SqlCommand cmd;
        private void btninsert_Click(object sender, EventArgs e)
        {
            cn.Open();
            string query = "insert into student values(@rno,@name,@dob,@add)";
            cmd = new SqlCommand(query, cn);
            cmd.Parameters.AddWithValue("@rno", txtrollno.Text);
            cmd.Parameters.AddWithValue("@name", txtname.Text);
            cmd.Parameters.AddWithValue("@dob",Convert.ToDateTime(txtdob.Text));
            cmd.Parameters.AddWithValue("@add", txtaddress.Text);
            cmd.ExecuteNonQuery();

            MessageBox.Show("Inserted");
            cn.Close();
        }

        private void btnshow_Click(object sender, EventArgs e)
        {
            cn.Open();
            string query = "select * from student";
            cmd = new SqlCommand(query, cn);
            SqlDataReader rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("rno");
            dt.Columns.Add("name");
            dt.Columns.Add("dob");
            dt.Columns.Add("add");

            if (rdr.HasRows)
            {
                while (rdr.Read())
                {
                    DataRow row = dt.NewRow();
                    row["rno"] = rdr["rno"];
                    row["name"] = rdr["name"];
                    row["dob"] = rdr["dob"];
                    row["add"] = rdr["add"];
                    dt.Rows.Add(row);
                }
            }
            else
                MessageBox.Show("No Data");

            dataGridView1.DataSource = dt;

            rdr.Close();
            cn.Close();
        }

        private void btnclear_Click(object sender, EventArgs e)
        {
            txtrollno.Clear();
            txtname.Clear();
            txtdob.Clear();
            txtaddress.Clear();
            dataGridView1.DataSource = null;
        }
    }
}
