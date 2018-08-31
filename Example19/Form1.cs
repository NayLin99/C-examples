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

namespace Example19
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
        private void Form1_Load(object sender, EventArgs e)
        {
            cn.Open();
            string query = "Select * from customer";

            da = new SqlDataAdapter(query, cn);
            da.Fill(ds);

            DataTable dt = ds.Tables[0];
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.ColumnCount = 3;

            dataGridView1.Columns[0].Name = "customerid";
            dataGridView1.Columns[0].DataPropertyName = "customerid";
            dataGridView1.Columns[0].HeaderText = "Customer ID";

            dataGridView1.Columns[1].Name = "customerName";
            dataGridView1.Columns[1].DataPropertyName = "customerName";
            dataGridView1.Columns[1].HeaderText = "Customer Name";

            dataGridView1.Columns[2].Name = "country";
            dataGridView1.Columns[2].DataPropertyName = "country";
            dataGridView1.Columns[2].HeaderText = "Country";

            dataGridView1.DataSource = dt;
        }
    }
}
