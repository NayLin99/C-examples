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

namespace Example12
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            String constr = @"Data Source=(localDB)\v11.0;Initial Catalog=std;MultipleActiveResultSets=True;Integrated Security=True;Pooling=False";
            SqlConnection cn = new SqlConnection(constr);
            cn.Open();

            String query = "Select * from student where rno=@rno";
            SqlCommand cmd = new SqlCommand(query, cn);
            cmd.Parameters.AddWithValue("@rno", txtrno.Text);
            SqlDataReader rdr = cmd.ExecuteReader();

            if (rdr.HasRows)
            {
                while (rdr.Read())
                {
                    DialogResult yn = MessageBox.Show("Are you sure to delete?", "Delete", MessageBoxButtons.YesNo);
                    if (yn == System.Windows.Forms.DialogResult.Yes)
                    {
                        String delquery = "Delete from student where rno=@rno";
                        cmd = new SqlCommand(delquery, cn); 
                        cmd.Parameters.AddWithValue("@rno", txtrno.Text);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Deleted");
                    }
                }
            }
            else
                MessageBox.Show("Not Found");

            rdr.Close();
            cn.Close();
        }
    }
}
