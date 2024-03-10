using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MidProject
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void ShowForm()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select * from Clo", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            ShowForm();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Insert into Clo values ( @Name, @DateCreated,@DateUpdated)", con);

            cmd.Parameters.AddWithValue("@Name", textBox2.Text);
            cmd.Parameters.AddWithValue("@DateCreated", DateTime.Now);
            cmd.Parameters.AddWithValue("@DateUpdated", DateTime.Now);

            cmd.ExecuteNonQuery();
            MessageBox.Show("CLO added successfully");

            textBox2.Text = "";
            ShowForm();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("Do you want to delete this CLO?", "Confirmation", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    int selectedCLOID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Id"].Value);

                    var con = Configuration.getInstance().getConnection();

                    // Delete from AssessmentComponent
                    SqlCommand cmd = new SqlCommand("DELETE FROM AssessmentComponent WHERE RubricId = @CloId", con);
                    cmd.Parameters.AddWithValue("@CloId", selectedCLOID);
                    cmd.ExecuteNonQuery();

                    // Delete from Rubric
                    cmd = new SqlCommand("DELETE FROM Rubric WHERE CloId = @CloId", con);
                    cmd.Parameters.AddWithValue("@CloId", selectedCLOID);
                    cmd.ExecuteNonQuery();

                    // Delete from Clo
                    cmd = new SqlCommand("DELETE FROM Clo WHERE Id = @CloId OR Name = @CloName", con);
                    cmd.Parameters.AddWithValue("@CloId", selectedCLOID);
                    cmd.Parameters.AddWithValue("@CloName", textBox2.Text);
                    cmd.ExecuteNonQuery();

                    dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);

                    MessageBox.Show("CLO Successfully Deleted");
                    ShowForm();
                }
            }
            else
            {
                MessageBox.Show("Please select a CLO to delete.");
            }
        }

      

        private void button3_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();

            if (dataGridView1.SelectedRows.Count > 0)
            {

                int selectedCloID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Id"].Value);

                if (textBox2.Text != "")
                {
                    SqlCommand cmd = new SqlCommand("UPDATE Clo SET Name = @Name WHERE Id = @Id", con);
                    cmd.Parameters.AddWithValue("@Name", textBox2.Text);
                    cmd.Parameters.AddWithValue("@Id", selectedCloID);
                    cmd.ExecuteNonQuery();
                   
                }
                ShowForm();
                MessageBox.Show("CLO information updated successfully.");

            }
            else
            {
                MessageBox.Show("Please select a CLO to update.");
            }

        }
    }
}
