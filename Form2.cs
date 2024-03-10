using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace MidProject
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void ShowForm()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select * from Student where status <> 6", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Insert into Student values (@FirstName, @LastName, @Contact,@Email,@RegistrationNumber, @Status)", con);


            cmd.Parameters.AddWithValue("@FirstName", textBox2.Text);
            cmd.Parameters.AddWithValue("@LastName", textBox3.Text);
            cmd.Parameters.AddWithValue("@Contact", textBox4.Text);
            cmd.Parameters.AddWithValue("@Email", textBox5.Text);
            cmd.Parameters.AddWithValue("@RegistrationNumber", textBox6.Text);
            cmd.Parameters.AddWithValue("@Status", textBox7.Text);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Successfully added");

            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            ShowForm();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (dataGridView1.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("Do you want to delete this student?", "Confirmation", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    
                    int selectedStudentID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Id"].Value);

                    
                    DeactivateStudent(selectedStudentID);

                    dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);

                    MessageBox.Show("Student Deleted Successfully");
                }
            }
            else
            {
                MessageBox.Show("Please select a student to delete.");
            }
        }

        private void DeactivateStudent(int Id)
        {
            var con = Configuration.getInstance().getConnection();

            SqlCommand cmd = new SqlCommand("UPDATE Student SET Status = 6 WHERE Id = @Id", con);

            cmd.Parameters.AddWithValue("@Id", Id);

            cmd.ExecuteNonQuery();
        }


        private void button4_Click(object sender, EventArgs e)
        {
          
        }

        private void button3_Click(object sender, EventArgs e)
        {
          var con = Configuration.getInstance().getConnection();

          if (dataGridView1.SelectedRows.Count > 0)
         { 
    
        int selectedStudentID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Id"].Value);

        if (textBox2.Text != "")
        {
            SqlCommand cmd = new SqlCommand("UPDATE Student SET FirstName = @FirstName WHERE Id = @StudentID", con);
            cmd.Parameters.AddWithValue("@FirstName", textBox2.Text);
            cmd.Parameters.AddWithValue("@StudentID", selectedStudentID);
            cmd.ExecuteNonQuery();
        }
        else if (textBox3.Text != "")
        {
            SqlCommand cmd = new SqlCommand("UPDATE Student SET LastName = @LastName WHERE Id = @StudentID", con);
            cmd.Parameters.AddWithValue("@LastName", textBox3.Text);
            cmd.Parameters.AddWithValue("@StudentID", selectedStudentID);
            cmd.ExecuteNonQuery();
        }
        else if (textBox4.Text != "")
        {
            SqlCommand cmd = new SqlCommand("UPDATE Student SET Contact = @Contact WHERE Id = @StudentID", con);
            cmd.Parameters.AddWithValue("@Contact", textBox4.Text);
            cmd.Parameters.AddWithValue("@StudentID", selectedStudentID);
            cmd.ExecuteNonQuery();
         }

        else if (textBox5.Text != "")
        {
             SqlCommand cmd = new SqlCommand("UPDATE Student SET Email = @Email WHERE Id = @StudentID", con);
             cmd.Parameters.AddWithValue("@Email", textBox5.Text);
             cmd.Parameters.AddWithValue("@StudentID", selectedStudentID);
             cmd.ExecuteNonQuery();
        }

        else if (textBox6.Text != "")
        {
             SqlCommand cmd = new SqlCommand("UPDATE Student SET RegistrationNumber = @RegistrationNumber WHERE Id = @StudentID", con);
             cmd.Parameters.AddWithValue("@RegistrationNumber", textBox6.Text);
             cmd.Parameters.AddWithValue("@StudentID", selectedStudentID);
             cmd.ExecuteNonQuery();
        }


        else if (textBox7.Text != "")
        {
             SqlCommand cmd = new SqlCommand("UPDATE Student SET Status = @Status WHERE Id = @StudentID", con);
             cmd.Parameters.AddWithValue("@Status", textBox7.Text);
             cmd.Parameters.AddWithValue("@StudentID", selectedStudentID);
             cmd.ExecuteNonQuery();
        }

                ShowForm();
                MessageBox.Show("Student information updated successfully.");
        }
    else
    {
        MessageBox.Show("Please select a student to update.");
    }
        }

        
        private void Form2_Load(object sender, EventArgs e)
        {
            ShowForm();
        }

        private void label8_Click_1(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select * from Student WHERE  FirstName Like '%" + textBox5.Text + "%' OR LastName Like '%" + textBox5.Text + "%' OR Contact Like '%" + textBox5.Text + "%' OR Email Like '%" + textBox5.Text + "%' OR RegistrationNumber Like '%" + textBox5.Text + "%' OR Status Like '%" + textBox5 + "%'", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
    }
}
