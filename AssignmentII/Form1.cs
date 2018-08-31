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

namespace AssignmentII
{ 
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        static string constr = @"Data Source=(localDB)\v11.0;MultipleActiveResultSets=True;Initial Catalog=std;Integrated Security=True;Pooling=False";
        SqlConnection cn = new SqlConnection(constr);
        SqlCommand cmd = new SqlCommand();
        private void Form1_Load(object sender, EventArgs e)
        {
            cn.Open();
            string query = "Select distinct dept from Staff";
            cmd = new SqlCommand(query, cn);
            SqlDataReader rdr = cmd.ExecuteReader();
            while(rdr.Read())
            {
                cbodept.Items.Add(rdr[0]);
            }
            rdr.Close();
            cn.Close();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            cn.Open();
            string squery = "Select count(staffid) from Staff where staffid=@id";
            cmd = new SqlCommand(squery, cn);
            cmd.Parameters.AddWithValue("@id", txtid.Text);

            int row = (int)cmd.ExecuteScalar();

            if(row>0)
            {
                MessageBox.Show("Already exist in table!");
            }
            else
            {
                cmd.Parameters.Clear();
                DialogResult yn = MessageBox.Show("Are you sure to insert?", "insert", MessageBoxButtons.YesNo);
                if(yn==System.Windows.Forms.DialogResult.Yes)
                {
                    
                    string insertqu = "Insert into Staff(staffid,staffname,dob,rank,dept,salary) values(@id,@name,@dob,@rank,@dept,@salary)";
                    cmd = new SqlCommand(insertqu, cn);
                    cmd.Parameters.AddWithValue("@id", txtid.Text);
                    cmd.Parameters.AddWithValue("@name", txtName.Text);
                    cmd.Parameters.AddWithValue("@dob", DateTime.Parse(txtdob.Text));
                    cmd.Parameters.AddWithValue("@rank", txtRank.Text);
                    cmd.Parameters.AddWithValue("@dept", cbodept.SelectedItem);
                    cmd.Parameters.AddWithValue("@salary", Convert.ToInt32(txtSalary.Text));
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Inserted!");   
                }
            }
            cn.Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtid.Clear();
            txtName.Clear();
            txtdob.Clear();
            txtRank.Clear();
            txtSalary.Clear();
        }
    }
}
