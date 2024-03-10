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
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
            ShowForm();
        }

        private void ShowForm()
        {

            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select * from Assessment", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void Form5_Load(object sender, EventArgs e)
        {
            ShowForm();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Insert into Assessment values ( @Title, @DateCreated,@TotalMarks,@TotalWeightage)", con);


            cmd.Parameters.AddWithValue("@Title", textBox2.Text);
            cmd.Parameters.AddWithValue("@DateCreated", DateTime.Now);
            cmd.Parameters.AddWithValue("@TotalMarks", textBox3.Text);
            cmd.Parameters.AddWithValue("@TotalWeightage", textBox4.Text);

          
            cmd.ExecuteNonQuery();
            MessageBox.Show("Added Successfully!");
            ShowForm();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("Do you want to delete?", "Confirmation", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    int selectedAssessmentID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Id"].Value);

                    var con = Configuration.getInstance().getConnection();

                   
                    SqlCommand cmd = new SqlCommand("DELETE FROM AssessmentComponent where AssessmentId = (select Id from Assessment where Id = @AssessmentId)",con);
                    cmd.Parameters.AddWithValue("@AssessmentId", selectedAssessmentID);
                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("DELETE FROM Assessment where Id = @AssessmentId ", con);
                    cmd.Parameters.AddWithValue("@AssessmentId", selectedAssessmentID);
                    cmd.ExecuteNonQuery();




                    dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);

                    MessageBox.Show("Successfully Deleted");
                    ShowForm();
                }
            }
            else
            {
                MessageBox.Show("Please select an assessment to delete.");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();

            if (dataGridView1.SelectedRows.Count > 0)
            {

                int selectedAssessmentID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Id"].Value);

                if (textBox2.Text != "")
                {
                    SqlCommand cmd = new SqlCommand("UPDATE Assessment SET Title = @Title WHERE Id = @Id", con);
                    cmd.Parameters.AddWithValue("@Title", textBox2.Text);
                    cmd.Parameters.AddWithValue("@Id", selectedAssessmentID);
                    cmd.ExecuteNonQuery();

                }


                else if (textBox3.Text != "")
                {
                    SqlCommand cmd = new SqlCommand("UPDATE Assessment SET TotalMarks = @TotalMarks WHERE Id = @Id", con);
                    cmd.Parameters.AddWithValue("@TotalMarks", textBox3.Text);
                    cmd.Parameters.AddWithValue("@Id", selectedAssessmentID);
                    cmd.ExecuteNonQuery();

                }

                else if (textBox4.Text != "")
                {
                    SqlCommand cmd = new SqlCommand("UPDATE Assessment SET TotalWeightage = @TotalWeightage WHERE Id = @Id", con);
                    cmd.Parameters.AddWithValue("@TotalWeightage", textBox4.Text);
                    cmd.Parameters.AddWithValue("@Id", selectedAssessmentID);
                    cmd.ExecuteNonQuery();

                }
                ShowForm();
                MessageBox.Show("Assessment information updated successfully.");

            }
            else
            {
                MessageBox.Show("Please select an assessment to update.");
            }
        }
    }
}
