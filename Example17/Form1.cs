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

namespace Example17
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        static string constr = @"Data Source=(localDB)\v11.0;Initial Catalog=std;Integrated Security=True;Pooling=False";
        SqlConnection cn = new SqlConnection(constr);

        private void Form1_Load(object sender, EventArgs e)
        {
            cn.Open();
            string query = "Select * from Staff";
            SqlDataAdapter da = new SqlDataAdapter(query,cn);
            /*da.SelectCommand = new SqlCommand();
            da.SelectCommand.Connection = cn;
            da.SelectCommand.CommandText = "Select * from Staff";
            da.SelectCommand.CommandType = CommandType.Text;*/
            DataSet ds = new DataSet();
            da.Fill(ds);

            DataTable dt = ds.Tables[0];

            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.DataSource = dt;
        }
    }
}
