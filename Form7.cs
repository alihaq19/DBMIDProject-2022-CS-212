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
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
            comboBox();
        }



        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void Form7_Load(object sender, EventArgs e)
        {
            ShowForm();
        }

        private void ShowForm()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select * from RubricLevel", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
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

        private void button1_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Insert into RubricLevel values ( @RubricId, @Details, @MeasurementLevel)", con);


            cmd.Parameters.AddWithValue("@Details", textBox2.Text);
            cmd.Parameters.AddWithValue("@MeasurementLevel", textBox3.Text);
            cmd.Parameters.AddWithValue("@RubricId", comboBox1.Text);


            cmd.ExecuteNonQuery();
            MessageBox.Show("Successfully added");
            ShowForm();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("Do you want to delete?", "Confirmation", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    int selectedRubricLevelID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Id"].Value);

                    var con = Configuration.getInstance().getConnection();


                    SqlCommand cmd = new SqlCommand("DELETE FROM RubricLevel where Id = @RubricLevelID ", con);
                    cmd.Parameters.AddWithValue("@RubricLevelID", selectedRubricLevelID);
                    cmd.ExecuteNonQuery();


                    dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);

                    MessageBox.Show("Successfully Deleted");
                    ShowForm();
                }
            }
            else
            {
                MessageBox.Show("Please select a Rubric Level to delete.");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();

            if (dataGridView1.SelectedRows.Count > 0)
            {

                int selectedRubricLevelID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Id"].Value);

                if (textBox2.Text != "")
                {
                    SqlCommand cmd = new SqlCommand("UPDATE RubricLevel SET Details = @Details WHERE Id = @Id", con);
                    cmd.Parameters.AddWithValue("@Details", textBox2.Text);
                    cmd.Parameters.AddWithValue("@Id", selectedRubricLevelID);
                    cmd.ExecuteNonQuery();

                }


                else if (textBox3.Text != "")
                {
                    SqlCommand cmd = new SqlCommand("UPDATE RubricLevel SET MeasurementLevel = @MeasurementLevel WHERE Id = @Id", con);
                    cmd.Parameters.AddWithValue("@MeasurementLevel", textBox3.Text);
                    cmd.Parameters.AddWithValue("@Id", selectedRubricLevelID);
                    cmd.ExecuteNonQuery();

                }

                ShowForm();
                MessageBox.Show("Information updated successfully.");

            }
            else
            {
                MessageBox.Show("Please select a Rubric Level to update.");
            }
        }
    }
}
