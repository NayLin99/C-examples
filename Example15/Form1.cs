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

namespace Example15
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            string constr = @"Data Source=(localDB)\v11.0;Initial Catalog=std;Integrated Security=True;Pooling=False";
            SqlConnection cn = new SqlConnection(constr);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandText = "Select count(rno) from student where rno=@rno";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@rno", txtrno.Text);
            cn.Open();

            int row = (int)cmd.ExecuteScalar();

            if(row>0)
            {
                MessageBox.Show("Already exist in table!");
            }
            else
            {
                cmd.Parameters.Clear();
                cmd.CommandText = "insert into student(rno,name,dob,class) values(@rno,@name,@dob,@class)";
                cmd.Parameters.AddWithValue("@rno", txtrno.Text);
                cmd.Parameters.AddWithValue("@name", txtname.Text);
                cmd.Parameters.AddWithValue("@dob", txtdob.Text);
                cmd.Parameters.AddWithValue("@class", txtclass.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Inserted!");
                cn.Close();
            }
        }
    }
}
