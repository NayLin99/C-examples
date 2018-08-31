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

namespace Example9
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
            SqlDataAdapter da = new SqlDataAdapter(query, cn);
            DataSet ds = new DataSet();
            da.Fill(ds);

            DataTable dt = ds.Tables[0];

            foreach(DataRow rr in dt.Rows)
            {
                decimal s = Convert.ToDecimal(rr["Salary"]);
                rr["Salary"] = s + (decimal)10000.00;
            }
            SqlCommandBuilder cb = new SqlCommandBuilder(da);
            da.Update(ds);
            MessageBox.Show("Updated!");
            cn.Close();
        }
    }
}
