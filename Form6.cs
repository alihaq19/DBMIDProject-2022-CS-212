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
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
            combo();
            comboBox();
        }

        private void ShowForm()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select * from AssessmentComponent", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Insert into AssessmentComponent values (@Name, @RubricId,@TotalMarks,@DateCreated,@DateUpdated,@AssessmentId)", con);


            cmd.Parameters.AddWithValue("@Name", textBox2.Text);

            cmd.Parameters.AddWithValue("@DateCreated", DateTime.Now);
            cmd.Parameters.AddWithValue("@DateUpdated", DateTime.Now);
            cmd.Parameters.AddWithValue("@TotalMarks", textBox3.Text);
            cmd.Parameters.AddWithValue("@RubricId", comboBox1.Text);
            cmd.Parameters.AddWithValue("@AssessmentId", comboBox2.Text);

            cmd.ExecuteNonQuery();
            MessageBox.Show("Successfully added");
            ShowForm();
        }

        private void comboBox()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select * from Rubric", con);
            SqlDataAdapter dat = new SqlDataAdapter(cmd);
            DataTable dtable = new DataTable();
            dat.Fill(dtable);
            comboBox1.DataSource = dtable;
            comboBox1.DisplayMember = "Rubric";
            comboBox1.ValueMember = "ID";
        }

        private void combo()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select * from Assessment", con);
            SqlDataAdapter dat = new SqlDataAdapter(cmd);
            DataTable dtable = new DataTable();
            dat.Fill(dtable);
            comboBox2.DataSource = dtable;
            comboBox2.DisplayMember = "Assessment";
            comboBox2.ValueMember = "ID";
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            ShowForm();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("Do you want to delete?", "Confirmation", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    int selectedAssessmentComponentID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Id"].Value);

                    var con = Configuration.getInstance().getConnection();


                    SqlCommand cmd = new SqlCommand("DELETE FROM AssessmentComponent where Id = @AssessmentComponentID ", con);
                    cmd.Parameters.AddWithValue("@AssessmentComponentID", selectedAssessmentComponentID);
                    cmd.ExecuteNonQuery();


                    dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);

                    MessageBox.Show("Successfully Deleted");
                    ShowForm();
                }
            }
            else
            {
                MessageBox.Show("Please select an assessment component to delete.");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();

            if (dataGridView1.SelectedRows.Count > 0)
            {

                int selectedAssessmentComponentID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Id"].Value);

                if (textBox2.Text != "")
                {
                    SqlCommand cmd = new SqlCommand("UPDATE AssessmentComponent SET Name = @Name WHERE Id = @Id", con);
                    cmd.Parameters.AddWithValue("@Name", textBox2.Text);
                    cmd.Parameters.AddWithValue("@Id", selectedAssessmentComponentID);
                    cmd.ExecuteNonQuery();

                }


                else if (textBox3.Text != "")
                {
                    SqlCommand cmd = new SqlCommand("UPDATE AssessmentComponent SET TotalMarks = @TotalMarks WHERE Id = @Id", con);
                    cmd.Parameters.AddWithValue("@TotalMarks", textBox3.Text);
                    cmd.Parameters.AddWithValue("@Id", selectedAssessmentComponentID);
                    cmd.ExecuteNonQuery();

                }

                ShowForm();
                MessageBox.Show("Information updated successfully.");

            }
            else
            {
                MessageBox.Show("Please select an assessment component to update.");
            }
        }
    }
}
