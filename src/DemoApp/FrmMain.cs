using System;
using System.Windows.Forms;
using Tesseract;

namespace DemoApp
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtFilePath.Enabled = false;
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK) 
            {
                txtFilePath.Text = openFileDialog1.FileName;
                OCRtoText();
            }
        }

        void OCRtoText()
        {
            richTextBox1.Text = "";

            try
            {
                using (var engine = new TesseractEngine(@"./tessdata", "eng", EngineMode.Default))
                {
                    using (var img = Pix.LoadFromFile(txtFilePath.Text))
                    {
                        using (var page = engine.Process(img))
                        {
                            richTextBox1.Text = page.GetText();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Unexpected Error: " + e.Message);
            }
        }
    }
}
