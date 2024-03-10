using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MidProject
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
            comboBox();
        }

        private void ShowForm()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select * from Rubric", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }


        private void Form4_Load(object sender, EventArgs e)
        {
            ShowForm();
        }

        private void comboBox()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select * from Clo", con);
            SqlDataAdapter dat = new SqlDataAdapter(cmd);
            DataTable dtable = new DataTable();
            dat.Fill(dtable);
            comboBox1.DataSource = dtable;
            comboBox1.DisplayMember = "Clo";
            comboBox1.ValueMember = "ID";
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Insert into Rubric values (@Id, @Details, @CloId)", con);

            cmd.Parameters.AddWithValue("@Id", textBox1.Text);
            cmd.Parameters.AddWithValue("@Details", textBox2.Text);
            cmd.Parameters.AddWithValue("@CloId", comboBox1.Text);


            cmd.ExecuteNonQuery();
            MessageBox.Show("Rubric Added Successfully!");
            ShowForm();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("Do you want to delete this Rubric?", "Confirmation", MessageBoxButtons.YesNo);

                
                if (result == DialogResult.Yes)
                {
                    int selectedRubricID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Id"].Value);

                    using (var con = Configuration.getInstance().getConnection())
                    {
                        // Delete from StudentResult
                        using (SqlCommand cmd = new SqlCommand("DELETE FROM StudentResult WHERE RubricMeasurementId IN (SELECT Id FROM RubricLevel WHERE RubricId = @RubricId)", con))
                        {
                            cmd.Parameters.AddWithValue("@RubricId", selectedRubricID);
                            cmd.ExecuteNonQuery();
                        }

                        // Delete from RubricLevel
                        using (SqlCommand cmd = new SqlCommand("DELETE FROM RubricLevel WHERE RubricId = @RubricId", con))
                        {
                            cmd.Parameters.AddWithValue("@RubricId", selectedRubricID);
                            cmd.ExecuteNonQuery();
                        }

                        // Delete from StudentResult
                        using (SqlCommand cmd = new SqlCommand("DELETE FROM StudentResult WHERE AssessmentComponentId IN (SELECT Id FROM AssessmentComponent WHERE RubricId = @RubricId)", con))
                        {
                            cmd.Parameters.AddWithValue("@RubricId", selectedRubricID);
                            cmd.ExecuteNonQuery();
                        }

                        // Delete from AssessmentComponent
                        using (SqlCommand cmd = new SqlCommand("DELETE FROM AssessmentComponent WHERE RubricId = @RubricId", con))
                        {
                            cmd.Parameters.AddWithValue("@RubricId", selectedRubricID);
                            cmd.ExecuteNonQuery();
                        }

                        // Delete from Rubric
                        using (SqlCommand cmd = new SqlCommand("DELETE FROM Rubric WHERE Id = @RubricId", con))
                        {
                            cmd.Parameters.AddWithValue("@RubricId", selectedRubricID);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);

                    MessageBox.Show("Rubric Deleted Successfully!");
                  
                }
            }
            else
            {
                MessageBox.Show("Please select a Rubric to delete.");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();

            if (dataGridView1.SelectedRows.Count > 0)
            {

                int selectedRubricID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Id"].Value);


                if (textBox2.Text != "")
                {
                    SqlCommand cmd = new SqlCommand("UPDATE Rubric SET Details = @Details WHERE Id = @Id", con);
                    cmd.Parameters.AddWithValue("@Details", textBox2.Text);
                    cmd.Parameters.AddWithValue("@Id", selectedRubricID);
                    cmd.ExecuteNonQuery();

                }
                ShowForm();
                MessageBox.Show("Rubric information updated successfully.");

            }
            else
            {
                MessageBox.Show("Please select a rubric to update.");
            }
        }
    }
}
