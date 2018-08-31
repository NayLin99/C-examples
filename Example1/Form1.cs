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
namespace Example1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            string str = "";
            string constr = @"Data Source=(localDB)\v11.0;Initial Catalog=std;Integrated Security=True;Pooling=False";
            SqlConnection cn = new SqlConnection(constr);

            string query = "Select * from student";
            SqlDataAdapter da = new SqlDataAdapter(query, cn);
            DataSet ds = new DataSet();
            da.Fill(ds);

            DataTable dt = ds.Tables[0];
            foreach(DataRow rr in dt.Rows)
            {
                str += rr["rno"] + "\t" + rr["name"] + "\t" + rr["add"] + "\t" + rr["class"] + "\n";
            }
            MessageBox.Show(str);
        }
    }


}
