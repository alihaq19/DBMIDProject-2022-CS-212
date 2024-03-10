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
    public partial class Form9 : Form
    {
        public Form9()
        {
            InitializeComponent();
        }

        private void ShowForm()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select * from ClassAttendance ", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;

        }
        private void Form9_Load(object sender, EventArgs e)
        {
            ShowForm();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime time = dateTimePicker1.Value;

            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Insert into ClassAttendance values (@AttendanceDate)", con);


            cmd.Parameters.AddWithValue("@AttendanceDate", time.Date);



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
                    int selectedClassAttendanceID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Id"].Value);

                    var con = Configuration.getInstance().getConnection();


                    SqlCommand cmd = new SqlCommand("DELETE FROM StudentAttendance where StudentId = (select Id from ClassAttendance where Id = @ClassAttendanceId)", con);
                    cmd.Parameters.AddWithValue("@ClassAttendanceId", selectedClassAttendanceID);
                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("DELETE FROM StudentAttendance  where AttendanceId=(select Id from ClassAttendance where Id = @ClassAttendanceId)", con);
                    cmd.Parameters.AddWithValue("@ClassAttendanceId", selectedClassAttendanceID);
                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("DELETE FROM ClassAttendance where Id = @ClassAttendanceId", con);
                    cmd.Parameters.AddWithValue("@ClassAttendanceId", selectedClassAttendanceID);
                    cmd.ExecuteNonQuery();

                    dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);

                    MessageBox.Show("Successfully Deleted");
                    ShowForm();
                }
            }
            else
            {
                MessageBox.Show("Please select an Attendance ID to delete.");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form8 form = new Form8();
            form.ShowDialog();
        }
    }
}
