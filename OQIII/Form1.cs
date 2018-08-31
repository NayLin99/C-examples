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

namespace OQIII
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        static string constr = @"Data Source=(localDB)\v11.0;Initial Catalog=std;Integrated Security=True;MultipleActiveResultSets=True;Pooling=False";
        SqlConnection cn = new SqlConnection(constr);
        SqlCommand cmd = new SqlCommand();
        private void Form1_Load(object sender, EventArgs e)
        {
            cboDept.Items.Add("Admin");
            cboDept.Items.Add("Human Resources");
            cboDept.Items.Add("Sales");
            cboDept.Items.Add("Finance and Production");

            rdoMale.Checked = true;
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            cn.Open();
            string gender = "";

            if (rdoMale.Checked)
                gender = "male";
            else
                gender = "female";

            string insertquery = "insert into Staff(staffid,staffname,dob,rank,dept,address,gender) values(@id,@name,@dob,@rank,@dept,@address,@gender)";
            cmd = new SqlCommand(insertquery, cn);
            cmd.Parameters.AddWithValue("@id", txtID.Text);
            cmd.Parameters.AddWithValue("@name", txtName.Text);
            cmd.Parameters.AddWithValue("@dob", dtDoB.Value);
            cmd.Parameters.AddWithValue("@rank", txtRank.Text);
            cmd.Parameters.AddWithValue("@dept", cboDept.SelectedItem);
            cmd.Parameters.AddWithValue("@address", txtAdd.Text);
            cmd.Parameters.AddWithValue("@gender", gender);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Inserted!");
            cn.Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtID.Clear();
            txtAdd.Clear();
            txtName.Clear();
            txtRank.Clear();
            //dtDoB = Convert.ToDateTime(DateTime.Today);
            dtDoB.Value = DateTime.Today;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
