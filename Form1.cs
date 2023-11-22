using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cauhoitracnghiem
{
    public partial class Form1 : Form
    {
        private List<CauHoi> danhSachCauHoi;
        private int currentIndex;

        public Form1()
        {
            InitializeComponent();

            danhSachCauHoi = LayDanhSachCauHoiTuDatabase(); // Hàm này sẽ trả về danh sách câu hỏi từ SQL
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

                                CauHoi cauHoi = new CauHoi(noiDung, dapAnA, dapAnB, dapAnC, dapAnD);
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
            }
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
        public string DapAnA { get; set; }
        public string DapAnB { get; set; }
        public string DapAnC { get; set; }
        public string DapAnD { get; set; }

        public CauHoi(string noiDung, string dapAnA, string dapAnB, string dapAnC, string dapAnD)
        {
            NoiDung = noiDung;
            DapAnA = dapAnA;
            DapAnB = dapAnB;
            DapAnC = dapAnC;
            DapAnD = dapAnD;
        }
    }
}