using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace apartment_management_system_RAD
{
    public partial class Categories : Form
    {
        public Categories()
        {
            InitializeComponent();
            ShowCategories();

        }
        SqlConnection Con = new SqlConnection(@"Data Source=DESKTOP-VQMI1MT;Initial Catalog=""apartment management system"";Integrated Security=True");
        private void ShowCategories()
        {
            Con.Open();
            String Query = "select * from CategoryTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query,Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds =new DataSet();
            sda.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            Con.Close();

        }
        private void ResetData()
        {
            CategoryTb.Text = "";
            RemarksTb.Text = "";
        }
        

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (CategoryTb.Text == "" || RemarksTb.Text == "")
            {
                MessageBox.Show("Missing information!!!");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into CategoryTbl(category,Remarks)values(@Cat,@Rem)", Con);
                    cmd.Parameters.AddWithValue("@Cat", CategoryTb.Text);
                    cmd.Parameters.AddWithValue("@Rem", RemarksTb.Text);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Category added!!");
                    Con.Close();
                    ResetData();
                    ShowCategories();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

            this.Close();
        }
    }
}
