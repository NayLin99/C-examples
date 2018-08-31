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

namespace OQIV
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        static string constr = @"Data Source=(localDB)\v11.0;Initial Catalog=std;Integrated Security=True;Pooling=False";
        SqlConnection cn = new SqlConnection(constr);
        SqlCommand cmd=new SqlCommand();
        SqlDataAdapter da;

        private void cbkDis_CheckedChanged(object sender, EventArgs e)
        {
            if(cbkDis.Checked)
            {
                gpDis.Visible = true;
            }
            else
            {
                gpDis.Visible = false;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            gpDis.Visible = false;
            cn.Open();
            string query = "select distinct ItemId from Item";
            cmd = new SqlCommand(query, cn);
            SqlDataReader rdr = cmd.ExecuteReader();
            while(rdr.Read())
            {
                cboItemId.Items.Add(rdr[0]);
            }
            rdr.Close();
            cn.Close();
        }

        private void cboItemId_SelectedIndexChanged(object sender, EventArgs e)
        {
            cn.Open();
            string query = "select name,price from Item where ItemId=@id";
            cmd = new SqlCommand(query, cn);
            cmd.Parameters.AddWithValue("@id", cboItemId.Text);
            SqlDataReader rdr = cmd.ExecuteReader();
            if(rdr.HasRows)
            {
                while(rdr.Read())
                {
                    txtName.Text = rdr["name"].ToString();
                    txtPrice.Text = rdr["price"].ToString();
                }
            }
            rdr.Close();
            cn.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            cn.Open();
            string query = "insert into Sale values(@vcno,@id,@price,@qty,@discount,@amount)";
            cmd = new SqlCommand(query, cn);
            cmd.Parameters.AddWithValue("@vcno", Convert.ToInt32(txtNo.Text));
            cmd.Parameters.AddWithValue("@id", cboItemId.SelectedItem);
            cmd.Parameters.AddWithValue("@price", Convert.ToInt32(txtPrice.Text));
            cmd.Parameters.AddWithValue("@qty", Convert.ToInt32(txtQty.Text));
            cmd.Parameters.AddWithValue("@discount", Convert.ToInt32(txtDAmt.Text));
            cmd.Parameters.AddWithValue("@amount", Convert.ToInt32(txtAmt.Text));
            cmd.ExecuteNonQuery();
            MessageBox.Show("Saved!");
            cn.Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtAmt.Clear();
            txtDAmt.Clear();
            txtName.Clear();
            txtNo.Clear();
            txtPrice.Clear();
            txtQty.Clear();
            cbkDis.Checked = false;
            gpDis.Visible = false;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            int price = Convert.ToInt32(txtPrice.Text);
            int qty = Convert.ToInt32(txtQty.Text);
            double discount;
            double percent;
            double amount;
            if (rdo5.Checked)
            {
                percent = 0.05;
            }
            else
            {
                percent = 0.1;
            }
            discount = price * percent * qty;
            amount = (qty * price) - discount;

            txtDAmt.Text = discount.ToString();
            txtAmt.Text = amount.ToString();
        }
    }
}
