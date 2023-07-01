using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Student_Database
{
    public partial class Form1 : Form
    {
        private MySqlConnection connect;
        private string data_server;
        private string data_name;
        private string data_user;
        private string data_password;
        public Form1()
        {
            data_server = "localhost";
            data_name = "student_database";
            data_user = "root";
            data_password = "";

            InitializeComponent();
        }
        //validate user entry
        private bool studenttest()
        {
            if (namebox.Text == string.Empty || matbox.Text == string.Empty || emailbox.Text == string.Empty || dobox.Text == string.Empty)
            {
                return false;

            }
            return true;
        }
        private bool awardtest()
        {
            if(holderbox.Text == string.Empty || matbox2.Text == String.Empty || titlebox.Text == string.Empty)
            {
                return false;
            }
            return true;
        }
        private bool disctest()
        {
            if(nametext.Text == string.Empty || matext.Text == string.Empty || offencetext.Text == string.Empty || verdictext.Text == string.Empty )
            {
                return false ;
            }
            return true;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (!studenttest())
            {
                MessageBox.Show("Please fill in all fields");
            }
            else
            {
                string name = namebox.Text;
                string mat = matbox.Text;
                string email = emailbox.Text;
                string dob = dobox.Text;
                string keyconnection = $"SERVER={data_server};USERNAME={data_user};PASSWORD={data_password};DATABASE={data_name}";
                MySqlConnection make_connect = new MySqlConnection(keyconnection);
                string new_student = "INSERT INTO students(id,name,matric_no,email,dob) VALUES ('',@name,@mat,@email,@dob)";
                MySqlCommand cmd = new MySqlCommand (new_student,make_connect);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@mat", mat);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@dob", dob);

                try
                {
                    make_connect.Open ();
                    if(cmd.ExecuteNonQuery() > 0)
                    {
                        MessageBox.Show("Student added to school");
                        namebox.Clear();
                        matbox.Clear();
                        emailbox.Clear();
                        dobox.Clear();
                        viewgrid();
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show (ex.Message);
                }

            }
        }
        private void viewgrid()
        {
            string keyconnection = $"SERVER={data_server};USERNAME={data_user};PASSWORD={data_password};DATABASE={data_name}";
            MySqlConnection make_connect = new MySqlConnection(keyconnection);
            string show = $"SELECT * FROM students";
            MySqlDataAdapter adapter = new MySqlDataAdapter(show,make_connect);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            studentDGV.DataSource = dataTable;
        }
        private void viewgrid2()
        {
            string keyconnection = $"SERVER={data_server};USERNAME={data_user};PASSWORD={data_password};DATABASE={data_name}";
            MySqlConnection make_connect = new MySqlConnection(keyconnection);
            string show = $"SELECT * FROM awards";
            MySqlDataAdapter adapter = new MySqlDataAdapter(show, make_connect);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            awardGDV.DataSource = dataTable;
        }
        private void viewgrid3()
        {
            string keyconnection = $"SERVER={data_server};USERNAME={data_user};PASSWORD={data_password};DATABASE={data_name}";
            MySqlConnection main_connect = new MySqlConnection(keyconnection);
            string show = $"SELECT * FROM disciplinary_cases";
            MySqlDataAdapter adapter = new MySqlDataAdapter(show, main_connect);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            discDGV.DataSource = dataTable;
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(studentDGV.SelectedRows.Count > 0)
            {
                string name = namebox.Text = studentDGV.SelectedRows[0].Cells[1].Value.ToString();
                string mat = matbox.Text = studentDGV.SelectedRows[0].Cells[2].Value.ToString();
                string email = emailbox.Text = studentDGV.SelectedRows[0].Cells[3].Value.ToString();
                string dob = dobox.Text = studentDGV.SelectedRows[0].Cells[4].Value.ToString();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            viewgrid();
            viewgrid2();
            viewgrid3();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            string id = studentDGV.SelectedRows[0].Cells[0].Value.ToString();
            string name = namebox.Text;
            string mat = matbox.Text;
            string email = emailbox.Text;
            string dob = dobox.Text;
            string keyconnection = $"SERVER={data_server};USERNAME={data_user};PASSWORD={data_password};DATABASE={data_name}";
            MySqlConnection make_connect = new MySqlConnection(keyconnection);
            string update_student = $"UPDATE students SET name=@name, matric_no=@mat, email=@email, dob=@dob WHERE id={id}";
            MySqlCommand cmd = new MySqlCommand(update_student, make_connect);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@mat", mat);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@dob", dob);
            try
            {
                make_connect.Open();
                if(cmd.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Student detail updated");
                    viewgrid();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex .Message);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string id = studentDGV.SelectedRows[0].Cells[0].Value.ToString();
            string keystring = $"SERVER={data_server};USERNAME={data_user};PASSWORD={data_password};DATABASE={data_name}";
            MySqlConnection the_connection_p = new MySqlConnection(keystring);
            string delete = $"DELETE FROM students where id={id}";
            MySqlCommand cmd = new MySqlCommand(delete, the_connection_p);
            try
            {
                the_connection_p.Open();

                if (cmd.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Student has been deleted");
                    viewgrid();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (!awardtest())
            {
                MessageBox.Show("Please fill in all fields");
            }
            else
            {
                string mat2 = matbox2.Text;
                string holder = holderbox.Text;
                string title = titlebox.Text;
                string keyconnection = $"SERVER={data_server};USERNAME={data_user};PASSWORD={data_password};DATABASE={data_name}";
                MySqlConnection make_connect = new MySqlConnection(keyconnection);
                string new_award = "INSERT INTO awards(id,title,matric_no,holder) VALUES ('',@title,@mat,@holder)";
                MySqlCommand cmd1 = new MySqlCommand(new_award, make_connect);
                cmd1.Parameters.AddWithValue("@title", title);
                cmd1.Parameters.AddWithValue("@mat", mat2);
                cmd1.Parameters.AddWithValue("@holder", holder);
                try
                {
                    make_connect.Open();
                    if (cmd1.ExecuteNonQuery() > 0)
                    {
                        MessageBox.Show("Student added to school");
                        titlebox.Clear();
                        matbox2.Clear();
                        holderbox.Clear();
                        
                        viewgrid2();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string id2 = awardGDV.SelectedRows[0].Cells[0].Value.ToString();
            string mat2 = matbox2.Text;
            string holder = holderbox.Text;
            string title = titlebox.Text;
            string keyconnection = $"SERVER={data_server};USERNAME={data_user};PASSWORD={data_password};DATABASE={data_name}";
            MySqlConnection make_connect = new MySqlConnection(keyconnection);
            string update_award = $"UPDATE awards SET title=@title, matric_no=@mat2, holder=@holder WHERE id={id2}";
            MySqlCommand cmd1 = new MySqlCommand(update_award, make_connect);
            cmd1.Parameters.AddWithValue("@title", title);
            cmd1.Parameters.AddWithValue("@mat2", mat2);
            cmd1.Parameters.AddWithValue("@holder", holder);
            try
            {
                make_connect.Open();
                if (cmd1.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("award detail updated");
                    viewgrid2();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void awardGDV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (awardGDV.SelectedRows.Count > 0)
            {
               titlebox.Text = awardGDV.SelectedRows[0].Cells[2].Value.ToString();
               holderbox.Text = awardGDV.SelectedRows[0].Cells[3].Value.ToString();
               matbox2.Text = awardGDV.SelectedRows[0].Cells[1].Value.ToString();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string id2 = awardGDV.SelectedRows[0].Cells[0].Value.ToString();
            string keystring = $"SERVER={data_server};USERNAME={data_user};PASSWORD={data_password};DATABASE={data_name}";
            MySqlConnection the_connection_p = new MySqlConnection(keystring);
            string delete_award = $"DELETE FROM awards where id={id2}";
            MySqlCommand cmd1 = new MySqlCommand(delete_award, the_connection_p);
            try
            {
                the_connection_p.Open();

                if (cmd1.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Title has been deleted");
                    viewgrid();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (!disctest())
            {
                MessageBox.Show("Please ensure you fill all fields");
            }
            else
            {
                string offender = nametext.Text;
                string mat3 = matext.Text;
                string offence = offencetext.Text;
                string verdict = verdictext.Text;
                string keyconnection = $"SERVER={data_server};USERNAME={data_user};PASSWORD={data_password};DATABASE={data_name}";
                MySqlConnection main_connect = new MySqlConnection(keyconnection);
                string new_disciplinary = $"INSERT INTO disciplinary_cases(id,name,matric_no,offence,verdict) VALUES ('',@offender,@mat3,@offence,@verdict)";
                MySqlCommand cmd2 = new MySqlCommand(new_disciplinary, main_connect);
                cmd2.Parameters.AddWithValue("@offender", offender);
                cmd2.Parameters.AddWithValue("@mat3",mat3);
                cmd2.Parameters.AddWithValue("offence",offence);
                cmd2.Parameters.AddWithValue("@verdict",verdict);

                try
                {
                    main_connect.Open();
                    if(cmd2.ExecuteNonQuery() > 0)
                    {
                        MessageBox.Show("New case added");
                        nametext.Clear();
                        matext.Clear();
                        offencetext.Clear();
                        verdictext.Clear();
                        viewgrid3();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void discDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (discDGV.SelectedRows.Count > 0)
            {
                nametext.Text = discDGV.SelectedRows[0].Cells[1].Value.ToString();
                matext.Text = discDGV.SelectedRows[0].Cells[2].Value.ToString();
                offencetext.Text = discDGV.SelectedRows[0].Cells[3].Value.ToString();
                verdictext.Text = discDGV.SelectedRows[0].Cells[4].Value.ToString();
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            string id2 = discDGV.SelectedRows[0].Cells[0].Value.ToString();
            string mat3 = matext.Text;
            string offender = nametext.Text;
            string offence = offencetext.Text;
            string verdict = verdictext.Text;   
            string keyconnection = $"SERVER={data_server};USERNAME={data_user};PASSWORD={data_password};DATABASE={data_name}";
            MySqlConnection make_connect = new MySqlConnection(keyconnection);
            string update_case = $"UPDATE disciplinary_cases SET name=@offender, matric_no=@mat3, offence=@offence, verdict=@verdict WHERE id={id2}";
            MySqlCommand cmd1 = new MySqlCommand(update_case, make_connect);
            cmd1.Parameters.AddWithValue("@offender", offender);
            cmd1.Parameters.AddWithValue("@mat3", mat3);
            cmd1.Parameters.AddWithValue("@offence", offence);
            cmd1.Parameters.AddWithValue("@verdict", verdict);
            try
            {
                make_connect.Open();
                if (cmd1.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Case updated");
                    viewgrid3();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string id3 = discDGV.SelectedRows[0].Cells[0].Value.ToString();
            string keystring = $"SERVER={data_server};USERNAME={data_user};PASSWORD={data_password};DATABASE={data_name}";
            MySqlConnection the_connection_p = new MySqlConnection(keystring);
            string delete_award = $"DELETE FROM disciplinary_cases where id={id3}";
            MySqlCommand cmd2 = new MySqlCommand(delete_award, the_connection_p);
            try
            {
                the_connection_p.Open();

                if (cmd2.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Case has been deleted");
                    viewgrid();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
