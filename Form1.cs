// ... (Your using statements)

using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using System;

namespace cauhoitracnghiem
{
    public partial class Form1 : Form
    {
        private List<CauHoi> danhSachCauHoi;
        private int currentIndex;

        public Form1()
        {
            InitializeComponent();

            danhSachCauHoi = LayDanhSachCauHoiTuDatabase();
            currentIndex = 0;
            HienThiCauHoi(currentIndex);
        }

        private List<CauHoi> LayDanhSachCauHoiTuDatabase()
        {
            List<CauHoi> danhSach = new List<CauHoi>();
            string connectionString = "Data Source=LAPTOP_KIET;Initial Catalog=IDName;Integrated Security=True";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM CauHoi";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string noiDung = reader["NoiDung"].ToString();
                                string dapAnA = reader["DapAnA"].ToString();
                                string dapAnB = reader["DapAnB"].ToString();
                                string dapAnC = reader["DapAnC"].ToString();
                                string dapAnD = reader["DapAnD"].ToString();

                                int cauHoiID = Convert.ToInt32(reader["CauHoiID"]); 
                                CauHoi cauHoi = new CauHoi(noiDung, dapAnA, dapAnB, dapAnC, dapAnD, cauHoiID);
                                danhSach.Add(cauHoi);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return danhSach;
        }

        private void HienThiCauHoi(int index)
        {
            if (index >= 0 && index < danhSachCauHoi.Count)
            {
                lblCauHoi.Text = danhSachCauHoi[index].NoiDung;
                rbA.Text = danhSachCauHoi[index].DapAnA;
                rbB.Text = danhSachCauHoi[index].DapAnB;
                rbC.Text = danhSachCauHoi[index].DapAnC;
                rbD.Text = danhSachCauHoi[index].DapAnD;

                string dapAnDung = LayDapAnDungTuDatabase(danhSachCauHoi[index].CauHoiID);

             
                rbA.ForeColor = (dapAnDung == "A") ? Color.Red : SystemColors.ControlText;
                rbB.ForeColor = (dapAnDung == "B") ? Color.Red : SystemColors.ControlText;
                rbC.ForeColor = (dapAnDung == "C") ? Color.Red : SystemColors.ControlText;
                rbD.ForeColor = (dapAnDung == "D") ? Color.Red : SystemColors.ControlText;


                if (dapAnDung == "A")
                    rbA.Checked = true;
                else if (dapAnDung == "B")
                    rbB.Checked = true;
                else if (dapAnDung == "C")
                    rbC.Checked = true;
                else if (dapAnDung == "D")
                    rbD.Checked = true;
            }
        }
        // Khi người dùng chọn đáp án, lưu thông tin vào CSDL
private void LuuDapAnDaChon(int cauHoiID, string dapAnDaChon)
{
    string connectionString = "Data Source=LAPTOP_KIET;Initial Catalog=IDName;Integrated Security=True";

    try
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            string query = "UPDATE CauHoi SET DapAnDaChon = @DapAnDaChon WHERE CauHoiID = @CauHoiID";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@DapAnDaChon", dapAnDaChon);
                command.Parameters.AddWithValue("@CauHoiID", cauHoiID);

                command.ExecuteNonQuery();
            }
        }
    }
    catch (Exception ex)
    {
        MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}

//// Khi người dùng chọn một đáp án, gọi hàm LuuDapAnDaChon
//private void LuuDapAnDaChon(int cauHoiID, string dapAnDaChon)
//{
//    // Gọi hàm này khi người dùng đã chọn đáp án
//    LuuDapAnDaChon(cauHoiID, dapAnDaChon);
//}

        private string LayDapAnDungTuDatabase(int cauHoiID)
        {
            string dapAnDung = string.Empty;
            string connectionString = "Data Source=LAPTOP_KIET;Initial Catalog=IDName;Integrated Security=True";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT DapAnDung FROM CauHoi WHERE CauHoiID = @CauHoiID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CauHoiID", cauHoiID);
                        object result = command.ExecuteScalar();

                        if (result != null)
                        {
                            dapAnDung = result.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return dapAnDung;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            currentIndex++;
            HienThiCauHoi(currentIndex);
        }

    }

    public class CauHoi
    {
        public string NoiDung { get; set; }
        public int CauHoiID { get; set; }
        public string DapAnA { get; set; }
        public string DapAnB { get; set; }
        public string DapAnC { get; set; }
        public string DapAnD { get; set; }

        public CauHoi(string noiDung, string dapAnA, string dapAnB, string dapAnC, string dapAnD, int cauHoiID)
        {
            NoiDung = noiDung;
            DapAnA = dapAnA;
            DapAnB = dapAnB;
            DapAnC = dapAnC;
            DapAnD = dapAnD;
            CauHoiID = cauHoiID;
        }
    }
}
