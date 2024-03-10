using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MidProject
{
    public partial class Form8 : Form
    {
        public Form8()
        {
            InitializeComponent();
            combo();
            ShowForm();
        }


        private void ShowForm()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT s.Id, CONCAT(s.FirstName, ' ', s.LastName) as Name, s.RegistrationNumber, s.Contact as Status, s.Email as Date FROM Student s WHERE s.Status = 5", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            foreach (DataGridViewRow d in dataGridView1.Rows)
            {
                d.Cells[3].Value = "Present";
                d.Cells[4].Value = DateTime.Now;
            }
        }
        private void Form8_Load(object sender, EventArgs e)
        {
            ShowForm();
        }

        private void combo()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select * from ClassAttendance", con);
            SqlDataAdapter dat = new SqlDataAdapter(cmd);
            DataTable dtable = new DataTable();
            dat.Fill(dtable);
            comboBox1.DataSource = dtable;
            comboBox1.DisplayMember = "ClassAttendance";
            comboBox1.ValueMember = "ID";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int ar = 4;

            string a = dataGridView1.CurrentRow.Cells[3].Value.ToString();


            if (a == "Present")
            {
                ar = 1;
            }
            else if (a == "Absent")
            {
                ar = 2;
            }
            else if (a == "Leave")
            {
                ar = 3;
            }
            else if (a == "Late")
            {
                ar = 4;
            }

            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Insert into StudentAttendance values (@AttendanceId,@StudentId, @AttendanceStatus)", con);

            cmd.Parameters.AddWithValue("@AttendanceId", comboBox1.Text);
            cmd.Parameters.AddWithValue("@StudentId", textBox1.Text);

            cmd.Parameters.AddWithValue("@AttendanceStatus", ar);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Attendance Marked Successfully!");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int ar = 4;

            string a = dataGridView1.CurrentRow.Cells[3].Value.ToString();

            if (a == "Present")
            {
                ar = 1;
            }
            else if (a == "Absent")
            {
                ar = 2;
            }
            else if (a == "Leave")
            {
                ar = 3;
            }
            else if (a == "Late")
            {
                ar = 4;
            }

            var con = Configuration.getInstance().getConnection();

            if (dataGridView1.SelectedRows.Count > 0)
            {
                int selectedStudentId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Id"].Value);

                SqlCommand cmd = new SqlCommand("UPDATE StudentAttendance SET AttendanceStatus=@AttendanceStatus WHERE StudentId=@StudentId AND AttendanceId=" + comboBox1.Text, con);
                cmd.Parameters.AddWithValue("@AttendanceStatus", ar);
                cmd.Parameters.AddWithValue("@StudentId", selectedStudentId);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Updated Successfully!");
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView1.CurrentRow.Cells[3].Value = comboBox3.SelectedItem.ToString();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.CurrentRow.Cells[0].Selected = true;
            textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            comboBox3.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
        }
    }
}
