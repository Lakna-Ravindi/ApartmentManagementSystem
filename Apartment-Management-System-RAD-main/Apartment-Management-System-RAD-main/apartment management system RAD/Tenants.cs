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

namespace apartment_management_system_RAD
{
    public partial class Tenants : Form
    {
        public Tenants()
        {
            InitializeComponent();
            ShowTenants();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=DESKTOP-VQMI1MT;Initial Catalog=""apartment management system"";Integrated Security=True");

        public object TenantsDGV { get; private set; }

        private void ShowTenants()
        {
            Con.Open();
            String Query  = "Select * from tbltenant";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.Columns[1].Width = 300;
            Con.Close();
        }
        private void ResetData()
        {
            TnameTb.Text = "";
            Gencb.SelectedIndex = -1;
            PhoneTb.Text =  "";

        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
          
            
        

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label22_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox37_Click(object sender, EventArgs e)
        {
            if (TnameTb.Text == "" || Gencb.SelectedIndex == -1 || PhoneTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into tbltenant( Tenid,TenPhone,TenGen)values(@TN,@TP,@TG)", Con);
                    cmd.Parameters.AddWithValue("@TP", PhoneTb.Text);
                    cmd.Parameters.AddWithValue("@TG", Gencb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("TN", TnameTb.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Tenant Added");
                    Con.Close();
                    ShowTenants();
                    //helps to add tenants

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }

            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (TnameTb.Text == "" || Gencb.SelectedIndex == -1 || PhoneTb.Text == "")
            {
                MessageBox.Show("missing information");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("update tbltenant set TenId=@TN,TenPhone=@TP,TenGen=@TG where Tenanat_id=@TKey", Con);
                    cmd.Parameters.AddWithValue("@TP", PhoneTb.Text);
                    cmd.Parameters.AddWithValue("@TG", Gencb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@TN", TnameTb.Text);
                    cmd.Parameters.AddWithValue("@TKey", Key);
                    cmd.ExecuteNonQuery();
                    Con.Close();
                    ResetData();
                    ShowTenants();
                }
                catch(Exception Ex)
                {
                    MessageBox.Show("tenant UPDATED!!!");     //edit button
                }
            }
        }

        int Key = 0;

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("select a tenanats");

            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("delete from tbltenant where Tenanat_id=@TKey", Con);
                    cmd.Parameters.AddWithValue("@TKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Tenant deleted!!");
                    Con.Close();
                    ShowTenants();
                    //helps to delete tenants

                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                Con.Close();
            }
        }

        private void label15_Click(object sender, EventArgs e)
        {
            Apartments Obj = new Apartments();
            Obj.Show();
            this.Hide();
        }

        private void label14_Click(object sender, EventArgs e)
        {
            Landlords Obj = new Landlords();
            Obj.Show();
            this.Hide();
        }

        private void label13_Click(object sender, EventArgs e)
        {
            Rents Obj = new Rents();
            Obj.Show();
            this.Hide();
        }

        private void label12_Click(object sender, EventArgs e)
        {
            Categories Obj = new Categories();
            Obj.Show();
            //this.Hide();
        }

        private void label18_Click(object sender, EventArgs e)
        {
            Login Obj = new Login();
            Obj.Show();
            this.Hide();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                TnameTb.Text = row.Cells["TenId"].Value.ToString();

                PhoneTb.Text = row.Cells["TenPhone"].Value.ToString();
                Gencb.Text = row.Cells["TenGen"].Value.ToString();
                Key = Convert.ToInt32(row.Cells["Tenanat_id"].Value.ToString()); //add data to datagridview
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Tenants_Load(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }
    }
}
 