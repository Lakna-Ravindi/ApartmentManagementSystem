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
    public partial class Apartments : Form
    {
        public Apartments()
        {
            InitializeComponent();
            GetCategories();
            GetOwner();
            ShowAparts();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=DESKTOP-VQMI1MT;Initial Catalog=""apartment management system"";Integrated Security=True");
        private void GetCategories()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("select CNum from CategoryTbl", Con);
            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("CNum",typeof(int));
            dt.Load(Rdr);
            TypeTb.ValueMember = "CNum";
            TypeTb.DataSource = dt;
            Con.Close();
        }

        private void GetOwner()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("select LLId from LandLordTbl", Con);
            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("LLId", typeof(int));
            dt.Load(Rdr);
            LLcb.ValueMember = "LLId";
            LLcb.DataSource = dt;
            Con.Close();
        }

        private void label23_Click(object sender, EventArgs e)
        {

        }

        private void ShowAparts()
        {
            Con.Open();
            string Query = "select * from Apartmenttbl ";
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
            ApNameTb.Text = "";
            LLcb.SelectedIndex = -1;
            CostTb.Text = "";
            TypeTb.SelectedIndex = -1;
            AddressTb.Text = "";
        }


        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (ApNameTb.Text == "" || LLcb.SelectedIndex == -1 || CostTb.Text == "" || TypeTb.SelectedIndex == -1||AddressTb.Text=="")
            {

                MessageBox.Show("Missing Information!!!");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into ApartmentTbl(AName,AAdress,AType,ACost,Owner)values(@AN,@AAdd,@AT,@AC,@AO)", Con);
                    cmd.Parameters.AddWithValue("@AN", ApNameTb.Text);
                    cmd.Parameters.AddWithValue("@AAdd", AddressTb.Text);
                    cmd.Parameters.AddWithValue("@AT", TypeTb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@AC", CostTb.Text);
                    cmd.Parameters.AddWithValue("@AO", LLcb.SelectedValue.ToString());

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Apartment Added!!");
                    Con.Close();
                    ResetData();
                    ShowAparts();

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }

        }

        int Key = 0;
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            {
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    ApNameTb.Text = row.Cells["AName"].Value.ToString();

                    AddressTb.Text = row.Cells["AAdress"].Value.ToString();
                    TypeTb.Text = row.Cells["AType"].Value.ToString();
                    CostTb.Text = row.Cells["ACost"].Value.ToString();
                    LLcb.Text = row.Cells["Owner"].Value.ToString();
                    Key = Convert.ToInt32(row.Cells["ANum"].Value.ToString());
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ApNameTb.Text == "" || LLcb.SelectedIndex == -1 || CostTb.Text == "" || TypeTb.SelectedIndex == -1 || AddressTb.Text == "")
            {

                MessageBox.Show("Missing Information!!!");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("update  ApartmentTbl set AName=@AN,AAdress=@AAdd,AType=@AT,ACost=@AC,Owner=@AO Where ANum=@AKey", Con);
                    cmd.Parameters.AddWithValue("@AN", ApNameTb.Text);
                    cmd.Parameters.AddWithValue("@AAdd", AddressTb.Text);
                    cmd.Parameters.AddWithValue("@AT", TypeTb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@AC", CostTb.Text);
                    cmd.Parameters.AddWithValue("@AO", LLcb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@AKey",Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("apartment updated!!");
                    Con.Close();
                    ResetData();
                    ShowAparts();

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (Key == 0)
            {
                MessageBox.Show("select an Aparatment!!");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("delete from Apartmenttbl where ANum=@AKey", Con);
                    cmd.Parameters.AddWithValue("@AKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Apartment deleted!!");
                    Con.Close();
                    ResetData();
                    ShowAparts();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void label16_Click(object sender, EventArgs e)
        {
            Tenants Obj = new Tenants();
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
            {
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    ApNameTb.Text = row.Cells["AName"].Value.ToString();

                    AddressTb.Text = row.Cells["AAdress"].Value.ToString();
                    TypeTb.Text = row.Cells["AType"].Value.ToString();
                    CostTb.Text = row.Cells["ACost"].Value.ToString();
                    LLcb.Text = row.Cells["Owner"].Value.ToString();
                    Key = Convert.ToInt32(row.Cells["ANum"].Value.ToString());
                }
            }
        }
    }
}
