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

namespace OQII
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
        SqlCommand cmd = new SqlCommand();
        private void btnShow_Click(object sender, EventArgs e)
        {
            cn.Open();
            ds.Clear();
            string query = "";
            //string result = "";
            if(rdbPass.Checked)
            {
                query = "select student.rno,student.name,marks.result from student,marks where student.rno=marks.rno and marks.result='pass'";
                //cmd.Parameters.AddWithValue("@result", "Pass");
                cmd = new SqlCommand(query, cn);
                //result = "Pass";
            }
            if(rdbFail.Checked)
            {
                query = "select student.rno,student.name,marks.result from student,marks where student.rno=marks.rno and marks.result='fail'";
                //cmd.Parameters.AddWithValue("@result", "Fail");
                cmd = new SqlCommand(query, cn);
                //result = "Fail";
            }
            if(rdbAll.Checked)
            {
                query = "select student.rno,student.name,marks.result from student,marks where student.rno=marks.rno";
                cmd = new SqlCommand(query, cn);
            }
            //cmd.Parameters.AddWithValue("@result", result);
            
            da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            DataTable dt = ds.Tables[0];
            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.DataSource = dt;
            cn.Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ds.Clear();
            dataGridView1.DataSource = null;
        }
    }
}
