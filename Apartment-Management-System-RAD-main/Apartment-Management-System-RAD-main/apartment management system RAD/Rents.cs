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
    public partial class Rents : Form
    {
        public Rents()
        {
            InitializeComponent();
            GetApart();
            GetTenant();
            ShowRents();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=DESKTOP-VQMI1MT;Initial Catalog=""apartment management system"";Integrated Security=True");
        private void GetApart()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("select ANum from ApartmentTbl", Con);
            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("ANum", typeof(int));
            dt.Load(Rdr);
            ApartCb.ValueMember = "ANum";
            ApartCb.DataSource  = dt;
            Con.Close();
        }
        private void GetTenant()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("select Tenanat_id from tbltenant", Con);
            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("Tenanat_id", typeof(int));
            dt.Load(Rdr);
            TenantCb.ValueMember = "Tenanat_id";
            TenantCb.DataSource = dt;
            Con.Close();
        }
        private void ShowRents()
        {
            Con.Open();
            string Query = "select * from RentTbl ";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            RentsDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void ResetData()
        {
            AmountCb.Text = "";    
        }



        private void label24_Click(object sender, EventArgs e)
        {
            Con.Open();
            String Query = "select * from ApartmentTbl where ANum=" + ApartCb.SelectedValue.ToString() + "";
            SqlCommand cmd= new SqlCommand(Query, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda= new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach(DataRow dr in dt.Rows)
            {
                AmountCb.Text = dr["ACost"].ToString();
            }
            Con.Close();
        }

        private void GetCost()
        {

        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {

            if (ApartCb.SelectedIndex == -1 || TenantCb.SelectedIndex == -1 || AmountCb.Text == "")
            {

                MessageBox.Show("Missing Information!!!");
            }
            else
            {
                try
                {
                    String Period = RDate.Value.Date.Month+"-"+RDate.Value.Date.Year;
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into RentTbl(Apartment,Tenant,Period,Amount)values(@RA,@RT,@RP,@RAm)", Con);
                    cmd.Parameters.AddWithValue("@RA", ApartCb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@RT", TenantCb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@RP",Period );
                    cmd.Parameters.AddWithValue("@RAm", AmountCb.Text);                 
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Apartment Rented!!");
                    Con.Close();
                    ResetData();
                    ShowRents();

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }

        }

        private void ApartCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetCost();
        }

        private void label18_Click(object sender, EventArgs e)
        {
            Login Obj = new Login();
            Obj.Show();
            this.Hide();
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

        private void label14_Click(object sender, EventArgs e)
        {
            Landlords Obj = new Landlords();
            Obj.Show();
            this.Hide();
        }

        private void label12_Click(object sender, EventArgs e)
        {
            Categories Obj = new Categories();
            Obj.Show();
            //this.Hide();
        }

        private void AmountCb_TextChanged(object sender, EventArgs e)
        {

        }

        private void RentsDGV_SelectionChanged(object sender, EventArgs e)
        {

            foreach (DataGridViewRow row in RentsDGV.SelectedRows)
            {
                ApartCb.Text = row.Cells["Apartment"].Value.ToString();

                AmountCb.Text = row.Cells["Amount"].Value.ToString();
                TenantCb.Text = row.Cells["Tenant"].Value.ToString();
                RDate.Text = row.Cells["Period"].Value.ToString();
              
            }
        }
    }
}
