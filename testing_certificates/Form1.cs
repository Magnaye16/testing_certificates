using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace testing_certificates
{
    public partial class Form1 : Form
    {

        string studentNo, fullName, emailAddress, schoolYear, allselectedinfo, userRole = "G0", user = "me";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadCadets();
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            SearchCadets();
        }
        private void CadetListDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //SelectedCadets(e);
            MoreSelectedCadets(e);
        }
        private void GiveCertificateBTN_Click(object sender, EventArgs e)
        {
            string selectedCert = CertificateTypeCB.SelectedItem?.ToString();

            foreach (Form openForm in Application.OpenForms)
            {
                if (openForm is Certificates)
                {
                    // Close the existing form
                    openForm.Close();
                    break;
                }
            }

            if (string.IsNullOrEmpty(selectedCert))
            {
                MessageBox.Show("Please select a certificate type.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            switch (selectedCert)
            {
                case "Completion":
                case "Excellence":
                    // Both "Completion" and "Excellence" will open the Certificates form
                    var certificateForm = new Certificates(studentNo, fullName, emailAddress, schoolYear, allselectedinfo, selectedCert, userRole, user);
                    certificateForm.Show();
                    break;

                default:
                    // No certificate selected
                    break;
            }
        }


        private void LoadCadets()
        {
            DataTable dataTable = new DataTable();

            try
            {
                using (MySqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    //string query = "SELECT cadet_id AS `Student No`, " +
                    //       "TRIM(CONCAT(IFNULL(last_name, ''), ', ', " +
                    //       "IFNULL(first_name, ''), ' ', " +
                    //       "IFNULL(middle_name, ''), ' ', " +
                    //       "IFNULL(suffix, ''))) AS `Full Name`, " +
                    //       "email AS `Email Address`, " +
                    //       "contact_number AS `Contact No` " +
                    //       "s.school_year AS `School Year ` " +
                    //       "FROM cadet_info";

                    string query = "SELECT cadet_id AS `Student No`, " +
                                    "TRIM(CONCAT(IFNULL(last_name, ''), ', ', " +
                                    "IFNULL(first_name, ''), ' ', " +
                                    "IFNULL(middle_name, ''), ' ', " +
                                    "IFNULL(suffix, ''))) AS `Full Name`, " +
                                    "email AS `Email Address`, " +
                                    "contact_number AS `Contact No`, " +   
                                    "s.school_year AS `School Year` " +    
                                    "FROM cadet_info ci " +
                                    "JOIN section s ON ci.section_id = s.section_id;";

                    using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(query, conn))
                    {
                        dataAdapter.Fill(dataTable);
                    }
                }

                CadetListDGV.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading cadet data: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void SearchCadets()
        {
            string searchTerm = SearchTXT.Text.Trim().ToLower();
            string selectedCriteria = SearchCB.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(selectedCriteria))
            {
                if (string.IsNullOrEmpty(searchTerm))
                {
                    LoadCadets();
                }
                else
                {
                    (CadetListDGV.DataSource as DataTable).DefaultView.RowFilter =
                        $"Convert([Student No], 'System.String') LIKE '%{searchTerm}%' OR [Full Name] LIKE '%{searchTerm}%'";
                }
                return;
            }

            if (string.IsNullOrEmpty(searchTerm))
            {
                LoadCadets();
                return;
            }

            switch (selectedCriteria)
            {
                case "Name":
                    ApplyFilter($"[Full Name] LIKE '%{searchTerm}%'");
                    break;
                case "Student No":
                    ApplyFilter($"Convert([Student No], 'System.String') LIKE '%{searchTerm}%'");
                    break;
                case "Section":
                    ExecuteSearchQuery("s.campus", "Campus", searchTerm, joinSection: true);
                    break;
                case "Academic Year":
                    ExecuteSearchQuery("s.school_year", "School Year", searchTerm, joinSection: true);
                    break;
                case "Rank":
                    ExecuteSearchQuery("ci.rank", "Rank", searchTerm);
                    break;
                case "Platoon":
                    ExecutePlatoonSearch(searchTerm);
                    break;
                case "Battalion":
                    ExecuteBattalionSearch(searchTerm);
                    break;
                default:
                    ApplyFilter($"Convert([Student No], 'System.String') LIKE '%{searchTerm}%' OR [Full Name] LIKE '%{searchTerm}%'");
                    break;
            }
        }
        private void SelectedCadets(DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                DataGridViewRow clDGV = CadetListDGV.Rows[e.RowIndex];
                label0.Text = clDGV.Cells["Student No"].Value.ToString();
                label1.Text = clDGV.Cells["Full Name"].Value.ToString();
                label2.Text = clDGV.Cells["Email Address"].Value.ToString();
                label3.Text = clDGV.Cells["School Year"].Value.ToString();
                //label4.Text = clDGV.Cells["Class"].Value.ToString();


                studentNo = clDGV.Cells["Student No"].Value.ToString();
                fullName = clDGV.Cells["Full Name"].Value.ToString();
                emailAddress = clDGV.Cells["Email Address"].Value.ToString();
                schoolYear = clDGV.Cells["School Year"].Value.ToString();

            }
        }
        private void MoreSelectedCadets(DataGridViewCellEventArgs e)
        {
            List<string> selectedCadetsInfo = new List<string>();

            foreach (DataGridViewRow row in CadetListDGV.SelectedRows)
            {
                if (row.IsNewRow) continue;

                studentNo = row.Cells["Student No"].Value?.ToString() ?? "N/A";
                fullName = row.Cells["Full Name"].Value?.ToString() ?? "N/A";
                emailAddress = row.Cells["Email Address"].Value?.ToString() ?? "N/A";
                schoolYear = row.Cells["School Year"].Value?.ToString() ?? "N/A";

                string cadetInfo = $@"
Student No: {studentNo}
Full Name: {fullName}
Email: {emailAddress}
School Year: {schoolYear}
";

                selectedCadetsInfo.Add(cadetInfo);

                if (selectedCadetsInfo.Count == 1)
                    {
                        label0.Text = studentNo;
                        label1.Text = fullName;
                        label2.Text = emailAddress;
                        label3.Text = schoolYear;
                    }
            }

            allselectedinfo = string.Join("\n\n", selectedCadetsInfo);
            textBox1.Text = string.Join("\n\n", selectedCadetsInfo);
            //MessageBox.Show(allCadetsInfo, "Selected Cadets", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }









        private void ExecutePlatoonSearch(string searchTerm)
        {
            //OR CAST(p.plat_id AS CHAR) LIKE @searchTerm
            string formattedSearchTerm = "%" + searchTerm + "%";
            string query = @"
                        SELECT 
                            ci.cadet_id AS `Student No`, 
                            TRIM(CONCAT(IFNULL(ci.last_name, ''), ', ', 
                                        IFNULL(ci.first_name, ''), ' ', 
                                        IFNULL(ci.middle_name, ''), ' ', 
                                        IFNULL(ci.suffix, ''))) AS `Full Name`, 
                            ci.email AS `Email Address`, 
                            ci.contact_number AS `Contact No`, 
                            p.plat_name
                        FROM 
                            plat_info p
                        JOIN 
                            cadet_group cg ON p.plat_id = cg.plat_id
                        RIGHT JOIN 
                            cadet_info ci ON cg.cadet_id = ci.cadet_id
                        WHERE 
                            p.plat_name LIKE @searchTerm 

                        UNION

                        SELECT 
                            ci.cadet_id AS `Student No`, 
                            TRIM(CONCAT(IFNULL(ci.last_name, ''), ', ', 
                                        IFNULL(ci.first_name, ''), ' ', 
                                        IFNULL(ci.middle_name, ''), ' ', 
                                        IFNULL(ci.suffix, ''))) AS `Full Name`, 
                            ci.email AS `Email Address`, 
                            ci.contact_number AS `Contact No`, 
                            p.plat_name
                        FROM 
                            plat_info p
                        RIGHT JOIN 
                            cadet_info ci ON ci.cadet_id = p.leader_id
                        WHERE 
                            p.plat_name LIKE @searchTerm;
                    ";
            try
            {
                using (MySqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@searchTerm", formattedSearchTerm);
                        MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        CadetListDGV.DataSource = dataTable;

                        if (CadetListDGV.Columns.Contains("plat_name") && CadetListDGV.Columns.Contains("leader_id"))
                        {
                            CadetListDGV.Columns["plat_name"].Visible = false;
                            CadetListDGV.Columns["leader_id"].Visible = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading cadet details: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ExecuteBattalionSearch(string searchTerm)
        {
            string formattedSearchTerm = "%" + searchTerm + "%";
            string query = @"
                    SELECT 
                        ci.cadet_id AS `Student No`, 
                        TRIM(CONCAT(IFNULL(ci.last_name, ''), ', ', 
                                    IFNULL(ci.first_name, ''), ' ', 
                                    IFNULL(ci.middle_name, ''), ' ', 
                                    IFNULL(ci.suffix, ''))) AS `Full Name`, 
                        ci.email AS `Email Address`, 
                        ci.contact_number AS `Contact No`, 
                        b.xo_id,
                        b.comma_id,
                        b.per_id,
                        b.int_id,
                        b.tra_id,
                        b.logi_id,
                        b.commu_id,
                        b.civ_id,
                        b.bat_name
                    FROM 
                        bat_info b
                    RIGHT JOIN 
                        cadet_info ci ON ci.cadet_id IN (b.xo_id, b.comma_id, b.per_id, b.int_id, b.tra_id, b.logi_id, b.commu_id, b.civ_id)
                    WHERE 
                        b.bat_name LIKE @searchTerm

                    UNION ALL

                    SELECT 
                        ci.cadet_id AS `Student No`, 
                        TRIM(CONCAT(IFNULL(ci.last_name, ''), ', ', 
                                    IFNULL(ci.first_name, ''), ' ', 
                                    IFNULL(ci.middle_name, ''), ' ', 
                                    IFNULL(ci.suffix, ''))) AS `Full Name`, 
                        ci.email AS `Email Address`, 
                        ci.contact_number AS `Contact No`, 
                        NULL AS `xo_id`, 
                        NULL AS `comma_id`, 
                        NULL AS `per_id`, 
                        NULL AS `int_id`, 
                        NULL AS `tra_id`, 
                        NULL AS `logi_id`, 
                        NULL AS `commu_id`, 
                        NULL AS `civ_id`, 
                        b.bat_name
                    FROM 
                        bat_info b
                    JOIN 
                        comp_info c ON b.bat_id = c.bat_id
                    JOIN 
                        plat_info p ON c.comp_id = p.comp_id
                    JOIN 
                        cadet_group cg ON p.plat_id = cg.plat_id
                    RIGHT JOIN 
                        cadet_info ci ON cg.cadet_id = ci.cadet_id
                    WHERE 
                        b.bat_name LIKE @searchTerm;
                ";
            try
            {
                using (MySqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@searchTerm", formattedSearchTerm);
                        MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        CadetListDGV.DataSource = dataTable;

                        string[] columnsToHide = { "xo_id", "comma_id", "per_id", "int_id", "tra_id", "logi_id", "commu_id", "civ_id", "bat_name" };
                        foreach (var column in columnsToHide)
                        {
                            if (CadetListDGV.Columns.Contains(column))
                            {
                                CadetListDGV.Columns[column].Visible = false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading cadet details: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ApplyFilter(string filter)
        {
            if (CadetListDGV.DataSource is DataTable dt)
            {
                dt.DefaultView.RowFilter = null;
                dt.DefaultView.RowFilter = filter;
            }
        }

        private void ExecuteSearchQuery(string dbColumn, string hiddenColumnToHide, string searchTerm, bool joinSection = false)
        {
            string selectFields = "ci.cadet_id AS `Student No`, " +
                                  "TRIM(CONCAT(IFNULL(ci.last_name, ''), ', ', " +
                                  "IFNULL(ci.first_name, ''), ' ', " +
                                  "IFNULL(ci.middle_name, ''), ' ', " +
                                  "IFNULL(ci.suffix, ''))) AS `Full Name`, " +
                                  "ci.email AS `Email Address`, " +
                                  "ci.contact_number AS `Contact No`, " +
                                  $"{(joinSection ? $"s.{hiddenColumnToHide}" : $"ci.{hiddenColumnToHide}")}";

            string fromClause = joinSection
                ? "FROM cadet_info ci JOIN section s ON ci.section_id = s.section_id"
                : "FROM cadet_info ci";

            string query = $"SELECT {selectFields} {fromClause} WHERE {dbColumn} LIKE @searchTerm";

            try
            {
                using (MySqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@searchTerm", $"%{searchTerm}%");

                        MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        CadetListDGV.DataSource = dataTable;

                        if (CadetListDGV.Columns.Contains(hiddenColumnToHide))
                        {
                            CadetListDGV.Columns[hiddenColumnToHide].Visible = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading cadet details: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
