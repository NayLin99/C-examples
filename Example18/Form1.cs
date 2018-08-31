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
namespace Example18
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

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            cn.Open();
            string query = "select * from stock; select * from sales;";
            da = new SqlDataAdapter(query, cn);
            da.Fill(ds);

            DataTable dt1 = ds.Tables[0];
            DataTable dt2 = ds.Tables[1];
            if (dt2.Rows.Count > 0)
            {
                foreach (DataRow rr in dt2.Rows)
                {
                    DataRow row = dt1.Select("Id = " + rr["id"]).First();

                    if (row != null)
                    {
                        int a = Convert.ToInt32(row["bal"]);
                        int b = Convert.ToInt32(rr["qty"]);
                        row["bal"] = a - b;
                    }
                }
                SqlCommandBuilder cb = new SqlCommandBuilder(da);
                da.Update(ds);
                MessageBox.Show("Updated!");
            }
            if (dt2.Rows.Count > 0)
            {
                string delquery = "delete from sales";
                SqlCommand cmd = new SqlCommand(delquery, cn);
                cmd.ExecuteNonQuery();
            }
            string str = "";
            foreach (DataRow row in dt1.Rows)
            {
                str += row["id"].ToString() +"\t"+ row["item"] +"\t"+ row["bal"] + "\n";
            }
            MessageBox.Show(str);
        }
    }
}
