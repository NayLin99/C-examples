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
namespace Assignment
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        static String constr = @"Data Source=(localDB)\v11.0;Initial Catalog=std;MultipleActiveResultSets=True;Integrated Security=True;Pooling=False";
        SqlConnection cn = new SqlConnection(constr);
        
        private void btnCalculate_Click(object sender, EventArgs e)
        {
            cn.Open();
            int p1 = Convert.ToInt32(txtPaperI.Text);
            int p2 = Convert.ToInt32(txtPaperII.Text);
            int total = p1 + p2;
            lblTotal.Text = total.ToString();
            if(p1>49 && p2>49)
            {
                lblResult.Text = "Pass";
            }
            else
            {
                lblResult.Text = "Fail";
            }

            String insertquery = "insert into marks(rno,p1,p2,total,result) values(@rno,@p1,@p2,@total,@result)";
            SqlCommand cmd = new SqlCommand(insertquery, cn);
            cmd.Parameters.AddWithValue("@rno", txtRollNo.Text.ToString());
            cmd.Parameters.AddWithValue("@p1", Convert.ToInt32(txtPaperI.Text));
            cmd.Parameters.AddWithValue("@p2", Convert.ToInt32(txtPaperII.Text));
            cmd.Parameters.AddWithValue("@total", total);
            cmd.Parameters.AddWithValue("@result", lblResult.Text.ToString());
            cmd.ExecuteNonQuery();
            MessageBox.Show("Inserted");
            cn.Close();
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            cn.Open();
            String str = "";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandText = "Select * from marks";
            cmd.CommandType = CommandType.Text;
            SqlDataReader rdr = cmd.ExecuteReader();
            if(rdr.HasRows)
            {
                while(rdr.Read())
                {
                    str += rdr["rno"].ToString() +"\t"+ rdr["p1"].ToString() + "\t" + rdr["p2"].ToString() + "\t" + rdr["total"].ToString() + "\t" + rdr["result"].ToString()+"\n";

                }
                MessageBox.Show(str);
            }
            else
            {
                MessageBox.Show("No Data");
            }
            cn.Close();
        }
    }
}
