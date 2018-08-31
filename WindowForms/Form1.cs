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

namespace WindowForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            String constr = @"Data Source=(localDB)\v11.0;Initial Catalog=std;Integrated Security=True;Pooling=False";
            SqlConnection cn = new SqlConnection(constr);
            cn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandText = "Select * from student where name like @name +'%'";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@name", txtName.Text);
            SqlDataReader rdr = cmd.ExecuteReader();
            String str = "";
            if (rdr.HasRows)
            {
                while (rdr.Read())
                {
                    str += rdr["rno"].ToString() + rdr["name"] + "\n";
                }
                MessageBox.Show(str);
            }
            else
                MessageBox.Show("Not Found");
        }
    }
}
