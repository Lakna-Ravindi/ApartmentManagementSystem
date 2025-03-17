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
    public partial class Landlords : Form
    {
        public Landlords()
        {
            InitializeComponent();
            ShowLLord();
            
        }
        SqlConnection Con =new SqlConnection(@"Data Source=DESKTOP-VQMI1MT;Initial Catalog=""apartment management system"";Integrated Security=True");
        private void ShowLLord()
        {
            Con.Open();
            string Query = "select * from LandLordtbl ";
            SqlDataAdapter sda = new SqlDataAdapter(Query,Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void ResetData()
        {
            PhoneTb.Text = "";
            GenCb.SelectedIndex = -1;
            LLNameTb.Text = "";
        }

         int Key = 0;

        private void button2_Click(object sender, EventArgs e)
        {
            if(Key == 0)
            {
                MessageBox.Show("select a Land Lord!!");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("delete from LandLordtbl where LLID=@LLKey", Con);
                    cmd.Parameters.AddWithValue("@LLKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Landlord deleted!!");
                    Con.Close();
                    ResetData();
                    ShowLLord();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void pictureBox37_Click_1(object sender, EventArgs e)
        {

            if (LLNameTb.Text == "" || GenCb.SelectedIndex == -1 || PhoneTb.Text == "")
            {

                MessageBox.Show("Missing Information!!!");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into LandLordTbl(LLName,LLPhone,LLGen)values(@LLN,@LLP,@LLG)", Con);
                    cmd.Parameters.AddWithValue("@LLN", LLNameTb.Text);
                    cmd.Parameters.AddWithValue("@LLP", PhoneTb.Text);
                    cmd.Parameters.AddWithValue("@LLG", GenCb.SelectedItem.ToString());
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("LandLord Added!!");
                    Con.Close();
                    ResetData();
                    ShowLLord();

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        { 
            if(LLNameTb.Text==""||GenCb.SelectedIndex==-1||PhoneTb.Text=="")
            { 
                MessageBox.Show("Missing information!!");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("update LandLordTbl set LLName=@LLN,LLPhone=@LLP,LLGen=@LLG where LLid=@LLKey", Con);
                    cmd.Parameters.AddWithValue("@LLN", LLNameTb.Text);
                    cmd.Parameters.AddWithValue("@LLP", PhoneTb.Text);
                    cmd.Parameters.AddWithValue("@LLG", GenCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@LLKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Landlord updated!!");
                    Con.Close();
                    ResetData();
                    ShowLLord();
                }
                catch(Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
       
        private void label12_Click(object sender, EventArgs e)
        {
            Categories obj = new Categories();
            obj.Show();
        }

        private void label16_Click(object sender, EventArgs e)
        {
            Tenants Obj = new Tenants();
            Obj.Show();
            this.Hide();
        }

        private void label15_Click(object sender, EventArgs e)
        {
            Apartments Obj = new Apartments();
            Obj.Show();
            this.Hide();
        }

        private void label13_Click(object sender, EventArgs e)
        {
            Rents Obj = new Rents();
            Obj.Show();
            this.Hide();
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
                LLNameTb.Text = row.Cells["LLName"].Value.ToString();
                PhoneTb.Text = row.Cells["LLPhone"].Value.ToString();
                GenCb.Text = row.Cells["LLGen"].Value.ToString();
                Key =Convert.ToInt32(row.Cells["LLid"].Value.ToString());
            }
        }
    }
}
