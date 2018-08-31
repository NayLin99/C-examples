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

namespace Example6
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string constr = @"Data Source=(localDB)\v11.0;Initial Catalog=std;Integrated Security=True;Pooling=False";
            SqlConnection cn = new SqlConnection(constr);
            string qq = "select * from student where rno=@rno";
            SqlCommand cmd = new SqlCommand(qq, cn);
            cmd.Parameters.AddWithValue("@rno", txtRollNo.Text);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            DataTable dt = ds.Tables[0];
            foreach(DataRow rr in dt.Rows)
            {
                rr.Delete();
            }
            cn.Open();
            string query = "delete from student where rno=@rno";
            cmd = new SqlCommand(query, cn);
            cmd.Parameters.AddWithValue("@rno", txtRollNo.Text);

            da.DeleteCommand = cmd;
            da.Update(ds);
            MessageBox.Show("Deleted");
           /* string str = "";
            foreach(DataRow rr in dt.Rows)
            {
                str += rr["rno"].ToString() + "\t" + rr["name"];
            }
            MessageBox.Show(str);*/
            cn.Close();
        }
    }
}
