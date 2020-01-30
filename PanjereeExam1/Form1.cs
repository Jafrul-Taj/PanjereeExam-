using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;

namespace PanjereeExam1
{
    public partial class ProofReadingForm : Form
    {
        public ProofReadingForm()
        {
            InitializeComponent();
            GetRecords();

        }
        SqlConnection con = new SqlConnection("Server=DESKTOP-6EO4U7F;Database=PanjereeExam1;User ID=JafrulHasan;Password=123456;");
        public int SL { get; set; }
        private void Form1_Load(Object sender, EventArgs e)
        {

           // GetRecords();
        }

        private void GetRecords()
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM PRB", con);
            DataTable dt = new DataTable();
            con.Open();

            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();

            ProofReadingGridView.DataSource=dt;
        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if(IsValid())
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO PRB" +
                    " (BillNo,Date,DeptName,BookName,PRN,ProofQty) " +
                    "VALUES (@billNo,@date,@departmentName,@bookName,@proofReadingName,@proofQty)",
                    con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@billNo" ,txtBillNo.Text);
                cmd.Parameters.AddWithValue("@date", txtDate.Text);
                cmd.Parameters.AddWithValue("@departmentName", txtDepartmentName.Text);
                cmd.Parameters.AddWithValue("@bookName", txtBookName.Text);
                cmd.Parameters.AddWithValue("@proofReadingName", txtProofReaderName.Text);
                cmd.Parameters.AddWithValue("@proofQty", txtProofQty.Text);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("A record successfully Created", "Success");
                GetRecords();
                ResetFormControls();
            }
            else
            {
                MessageBox.Show("Every Field should be filles", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool IsValid()
        {
            if (txtBillNo.Text == string.Empty)
                return false;
            if (txtBookName.Text == string.Empty)
                return false;
            if (txtDate.Text == string.Empty)
                return false;
            if (txtDepartmentName.Text == string.Empty)
                return false;
            if (txtProofReaderName.Text == string.Empty)
                return false;
            if (txtProofQty.Text == string.Empty)
                return false;
         
            return true;
        }

        private void ResetFormControls()
        {
            txtBillNo.Clear();
            txtBookName.Clear();
            txtDate.Clear();
            txtDepartmentName.Clear();
            txtProofQty.Clear();
            txtProofReaderName.Clear();

            txtBillNo.Focus();
        }

        private void ProofReadingGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            SL = int.Parse(ProofReadingGridView.SelectedRows[0].Cells[0].Value.ToString());
            txtBillNo.Text = ProofReadingGridView.SelectedRows[0].Cells[1].Value.ToString();
            txtBookName.Text = ProofReadingGridView.SelectedRows[0].Cells[2].Value.ToString();
            txtDate.Text = ProofReadingGridView.SelectedRows[0].Cells[3].Value.ToString();
            txtDepartmentName.Text = ProofReadingGridView.SelectedRows[0].Cells[4].Value.ToString();
            txtProofQty.Text = ProofReadingGridView.SelectedRows[0].Cells[5].Value.ToString();
            txtProofReaderName.Text = ProofReadingGridView.SelectedRows[0].Cells[6].Value.ToString();

        }

        private void Button4_Click(object sender, EventArgs e)
        {
            if(SL>0)
            {
                SqlCommand cmd = new SqlCommand(
                  "UPDATE PRB SET BillNo = @billNo, Date = @date, DeptName = @departmentName, " +
                  "BookName = @bookName, PRN = @proofReadingName, ProofQty = @proofQty WHERE SL = @SL", con);

                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@billNo", txtBillNo.Text);
                cmd.Parameters.AddWithValue("@date", txtDate.Text);
                cmd.Parameters.AddWithValue("@departmentName", txtDepartmentName.Text);
                cmd.Parameters.AddWithValue("@bookName", txtBookName.Text);
                cmd.Parameters.AddWithValue("@proofReadingName", txtProofReaderName.Text);
                cmd.Parameters.AddWithValue("@proofQty", txtProofQty.Text);
                cmd.Parameters.AddWithValue("@SL", this.SL);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("A Record successfully Updated", "Update");
                GetRecords();
                ResetFormControls();
            }
            else
            {
                MessageBox.Show("Plase select a record to update", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if(SL>0)
            {
                SqlCommand cmd = new SqlCommand(
                 "DELETE FROM  PRB WHERE SL = @SL ", con);

                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@SL", this.SL);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("A Record successfully deleted", "Delete");
                GetRecords();
                ResetFormControls();
            }
            else
            {
                MessageBox.Show("Plase select a record to Delete", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (txtSearchBox.Text != string.Empty)
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM PRB WHERE BillNo = @billNo", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@billNo", txtSearchBox.Text);

                DataTable dt = new DataTable();
                con.Open();

                SqlDataReader sdr = cmd.ExecuteReader();
                dt.Load(sdr);
                con.Close();

                ProofReadingGridView.DataSource = dt;
            }

        }
    }
}
