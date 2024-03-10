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
    public partial class Form10 : Form
    {
        public Form10()
        {
            InitializeComponent();
            comboo();
            comboBox();
            combo();
        }

        private void ShowForm()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select * from StudentResult", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        private void Form10_Load(object sender, EventArgs e)
        {
            ShowForm();
        }

        private void comboBox()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select * from AssessmentComponent", con);
            SqlDataAdapter dat = new SqlDataAdapter(cmd);
            DataTable dtable = new DataTable();
            dat.Fill(dtable);
            comboBox2.DataSource = dtable;
            comboBox2.DisplayMember = "AssessmentComponent";
            comboBox2.ValueMember = "ID";
        }

        private void combo()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select * from RubricLevel", con);
            SqlDataAdapter dat = new SqlDataAdapter(cmd);
            DataTable dtable = new DataTable();
            dat.Fill(dtable);
            comboBox3.DataSource = dtable;
            comboBox3.DisplayMember = "RubricLevel";
            comboBox3.ValueMember = "ID";
        }


        private void comboo()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select * from Student", con);
            SqlDataAdapter dat = new SqlDataAdapter(cmd);
            DataTable dtable = new DataTable();
            dat.Fill(dtable);
            comboBox1.DataSource = dtable;
            comboBox1.DisplayMember = "Student";
            comboBox1.ValueMember = "ID";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Insert into StudentResult values (@StudentId, @AssessmentComponentId, @RubricMeasurementId,@EvaluationDate)", con);

            cmd.Parameters.AddWithValue("@StudentId", comboBox1.Text);
            cmd.Parameters.AddWithValue("@AssessmentComponentId", comboBox2.Text);
            cmd.Parameters.AddWithValue("@RubricMeasurementId", comboBox3.Text);
            cmd.Parameters.AddWithValue("@EvaluationDate", DateTime.Now);

            cmd.ExecuteNonQuery();
            ShowForm();
            MessageBox.Show("Added Successfully!");
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (dataGridView1.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("Do you want to delete?", "Confirmation", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
             
                    var con = Configuration.getInstance().getConnection();

                    SqlCommand cmd = new SqlCommand("delete from StudentResult where AssessmentComponentId=" + comboBox2.Text + " AND StudentId=" + comboBox1.Text, con);
                    cmd.ExecuteNonQuery();
                    dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);
                    ShowForm();
                    MessageBox.Show("Successfully Deleted");
                    
                }
            }
            else
            {
                MessageBox.Show("Please select a student ID to delete.");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("Update StudentResult Set AssessmentComponentId=@AssessmentComponentId,RubricMeasurementId=@RubricMeasurementId,Evaluationdate=@Evaluationdate where StudentId=" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "and AssessmentComponentId=" + dataGridView1.CurrentRow.Cells[1].Value.ToString(), con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                cmd.Parameters.AddWithValue("@AssessmentComponentId", comboBox2.Text);
                cmd.Parameters.AddWithValue("@RubricMeasurementId", comboBox3.Text);
                cmd.Parameters.AddWithValue("@Evaluationdate", DateTime.Now);

                cmd.ExecuteNonQuery();
                ShowForm();
                MessageBox.Show("Updated Successfully!");
               
            }
        }
    }
}
