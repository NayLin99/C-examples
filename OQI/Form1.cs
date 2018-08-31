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

namespace OQI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        static string constr = @"Data Source=(localDB)\v11.0;Initial Catalog=std;Integrated Security=True;Pooling=False";
        SqlConnection cn = new SqlConnection(constr);
        SqlDataAdapter da;
        DataSet ds = new DataSet();
        SqlCommand cmd = new SqlCommand();
        private void Form1_Load(object sender, EventArgs e)
        {
            cn.Open();
            string query = "Select distinct dept from Staff";
            cmd = new SqlCommand(query, cn);
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                lstDept.Items.Add(rdr[0]);
            }
            rdr.Close();
            cn.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SqlCommandBuilder cb = new SqlCommandBuilder(da);
            da.Update(ds);
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            cn.Open();
            ds.Clear();
            string query = "select * from staff where dept=@dept";
            cmd = new SqlCommand(query, cn);
            cmd.Parameters.AddWithValue("@dept", lstDept.SelectedItem);
            da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            DataTable dt = ds.Tables[0];
            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.DataSource = dt;
            cn.Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            /* dataGridView1.Rows.Clear();
             dataGridView1.Refresh();*/
            dataGridView1.DataSource = null;
        }
    }
}
