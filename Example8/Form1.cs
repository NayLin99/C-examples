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

namespace Example8
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void txtInsert_Click(object sender, EventArgs e)
        {
            string constr = @"Data Source=(localDB)\v11.0;Initial Catalog=std;Integrated Security=True;Pooling=False";
            SqlConnection cn = new SqlConnection(constr);
            string query = "Select * from student";
            SqlDataAdapter da = new SqlDataAdapter(query, cn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataTable dt = ds.Tables[0];
            DataRow rr = dt.NewRow();
            rr["rno"] = txtRollNo.Text.ToString();
            rr["name"] = txtName.Text.ToString();
            rr["dob"] = DateTime.Parse(txtDob.Text);
            rr["add"] = txtAddress.Text.ToString();
            dt.Rows.Add(rr);

            SqlCommandBuilder cb = new SqlCommandBuilder(da);
            da.Update(ds);

            MessageBox.Show("Inserted");
        }
    }
}
