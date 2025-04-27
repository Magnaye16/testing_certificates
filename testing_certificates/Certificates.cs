using Org.BouncyCastle.Tls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace testing_certificates
{
    public partial class Certificates : Form
    {
        private string certificateType;
        private string studentNo;
        private string fullName;
        private string emailAddress;
        private string schoolYear;
        private string allSelectedInfo;
        private string user;
        private string userRole;


        public Certificates(string studentNo, string fullName, string email, string schoolYear, string allselectedinfo, string selectedCert, string userRole, string user)

        {
            InitializeComponent();

            // Save the selected certificate type
            this.studentNo = studentNo;
            this.fullName = fullName;
            this.emailAddress = email;
            this.schoolYear = schoolYear;
            this.allSelectedInfo = allselectedinfo;
            this.certificateType = selectedCert;
            this.user = user;
            this.userRole = userRole;

            // Call a method to setup design
            SetupCertificateDesign();

        }

        private void Certificates_Load(object sender, EventArgs e)
        {
            DateTime currentDate = DateTime.Now;
            DateTime currentMonth = DateTime.Now;
            DateTime currentYear = DateTime.Now;

            //preload the data in certificate
            label1.Text = fullName;
            label2.Text = schoolYear;
            label3.Text = emailAddress;

            label6.Text = fullName;
            label7.Text = schoolYear;
            label8.Text = emailAddress;
            //for testing
            textBox1.Text = allSelectedInfo;

            label9.Text = currentDate.ToString("dd");
            label10.Text = currentMonth.ToString("MMMM");
            label11.Text = currentYear.ToString("yyyy");
            label12.Text = user;
            label13.Text = userRole;
        }

        private void SetupCertificateDesign()
        {
            // First, hide all designs
            HideAllCertificateDesigns();

            switch (certificateType)
            {
                case "Completion":
                    CompletionCertificatePanel.Visible = true;
                    break;

                case "Excellence":
                    ExcellenceCertificatePanel.Visible = true;
                    break;

            

                default:
                    MessageBox.Show("Unknown certificate type.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
        }

        private void HideAllCertificateDesigns()
        {
            CompletionCertificatePanel.Visible = false;
            ExcellenceCertificatePanel.Visible = false;
            // etc. Hide all other designs
        }

       
    }
}
