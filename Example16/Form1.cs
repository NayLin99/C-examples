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

namespace Example16
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            String constr = @"Data Source=(localDB)\v11.0;Initial Catalog=std;MultipleActiveResultSets=True;Integrated Security=True;Pooling=False";
            SqlConnection cn = new SqlConnection(constr);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandText = "Select count(rno) from student where rno=@rno";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@rno", txtRno.Text);
            cn.Open();

            int row = (int)cmd.ExecuteScalar();

            if(row>0)
            {
                cmd.Parameters.Clear();
                cmd.CommandText = "update student set class=@class where rno=@rno";
                cmd.Parameters.AddWithValue("@class", txtClass.Text);
                cmd.Parameters.AddWithValue("@rno", txtRno.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Updated!");
            }
            else
            {
                MessageBox.Show("Data Not Exist!");
            }
            cn.Close();
        }
    }
}
