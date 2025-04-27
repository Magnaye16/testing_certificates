using Org.BouncyCastle.Tls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
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

        private Bitmap _bitmapToPrint;



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


        private void button1_Click(object sender, EventArgs e)
        {
            CaptureForm();
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

      

        private void CaptureForm()
        {
            // Define the rectangle for the part of the form to capture
            Rectangle captureArea = new Rectangle(1, 1, 522, 700);

            // Create a bitmap with the size of the capture area
            using (Bitmap originalBitmap = new Bitmap(captureArea.Width, captureArea.Height))
            {
                // Create a graphics object to draw onto the bitmap
                using (Graphics g = Graphics.FromImage(originalBitmap))
                {
                    // Capture the specified part of the form
                    g.CopyFromScreen(this.PointToScreen(captureArea.Location), Point.Empty, captureArea.Size);
                }

                // Resize the bitmap to fit the desired paper size (8.5 x 11 inches at 96 DPI)
                int paperWidth = (int)(21.59 * 96 / 2.54); // Convert cm to pixels (96 DPI)
                int paperHeight = (int)(27.94 * 96 / 2.54); // Convert cm to pixels (96 DPI)
                _bitmapToPrint = new Bitmap(paperWidth, paperHeight);

                using (Graphics g = Graphics.FromImage(_bitmapToPrint))
                {
                    // Draw the original bitmap scaled to fit the paper
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.DrawImage(originalBitmap, new Rectangle(0, 0, paperWidth, paperHeight));
                }

                // Automatically save the resized image to the specified folder
                string saveFolder = @"C:\Users\asus\Pictures\Testing ROTC";
                string dateTimeStamp = DateTime.Now.ToString("yyyyMMdd");
                string newFileName = Path.Combine(saveFolder, $"{fullName}_{schoolYear}_{dateTimeStamp}.png");

                // Ensure the folder exists
                if (!Directory.Exists(saveFolder))
                {
                    Directory.CreateDirectory(saveFolder);
                }

                // Save the resized bitmap
                _bitmapToPrint.Save(newFileName, ImageFormat.Png);
                MessageBox.Show($"Captured image saved at: {newFileName}");

                //// Show Print Dialog
                //using (PrintDialog printDialog = new PrintDialog())
                //using (PrintDocument printDoc = new PrintDocument())
                //{
                //    // Set custom paper size
                //    PaperSize paperSize = new PaperSize("CustomPaper", paperWidth, paperHeight);
                //    printDoc.DefaultPageSettings.PaperSize = paperSize;

                //    // Assign the PrintPage event handler to print the bitmap
                //    printDoc.PrintPage += PrintPageHandler;
                //    printDialog.Document = printDoc;

                //    // Display the dialog and print if the user clicks OK
                //    if (printDialog.ShowDialog() == DialogResult.OK)
                //    {
                //        printDoc.Print();
                //    }
                //}

                //// Dispose of the resized bitmap after saving and printing
                //_bitmapToPrint.Dispose();
            }
        }

        private void PrintPageHandler(object sender, PrintPageEventArgs e)
        {
            if (_bitmapToPrint != null)
            {
                e.Graphics.DrawImage(_bitmapToPrint, e.MarginBounds);
            }
        }
    }
}
