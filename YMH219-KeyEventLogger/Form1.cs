namespace YMH219_KeyEventLogger
{
    public partial class Form1 : Form
    {
        private readonly KeyLogger logger = new KeyLogger();

        private Label lblStatus;
        private Button btnStart;
        private Button btnStop;
        private Button btnClear;
        private Button btnSave;
        private TextBox txtInput;
        private ListBox lstLog;

        public Form1()
        {
            InitializeComponent();

            Text = "Key Event Logger";
            ClientSize = new Size(500, 560);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Font = new Font("Segoe UI", 10);
            KeyPreview = true;

            lblStatus = new Label
            {
                AutoSize = false,
                Size = new Size(460, 40),
                Location = new Point(20, 15),
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 13, FontStyle.Bold),
                ForeColor = Color.White
            };

            btnStart = CreateButton("Başlat", 20);
            btnStop = CreateButton("Durdur", 135);
            btnClear = CreateButton("Temizle", 250);
            btnSave = CreateButton("Kaydet", 365);

            var lblInput = new Label
            {
                Text = "Buraya yazın (kayıt yalnızca bu pencere aktifken yapılır):",
                AutoSize = true,
                Location = new Point(20, 120)
            };

            txtInput = new TextBox
            {
                Multiline = true,
                AcceptsReturn = true,
                AcceptsTab = true,
                ScrollBars = ScrollBars.Vertical,
                Location = new Point(20, 145),
                Size = new Size(460, 60)
            };

            lstLog = new ListBox
            {
                Location = new Point(20, 220),
                Size = new Size(460, 320),
                Font = new Font("Consolas", 10),
                TabStop = false
            };

            Controls.AddRange(new Control[] { lblStatus, btnStart, btnStop, btnClear, btnSave, lblInput, txtInput, lstLog });

            btnStart.Click += (s, e) => { logger.Start(); UpdateStatus(); };
            btnStop.Click += (s, e) => { logger.Stop(); UpdateStatus(); };
            btnClear.Click += (s, e) => { logger.Clear(); lstLog.Items.Clear(); FocusInput(); };
            btnSave.Click += (s, e) => SaveLog();

            KeyDown += Form1_KeyDown;

            UpdateStatus();
            ActiveControl = txtInput;
        }

        private Button CreateButton(string text, int x)
        {
            return new Button
            {
                Text = text,
                Location = new Point(x, 70),
                Size = new Size(105, 38),
                TabStop = false
            };
        }

        private void Form1_KeyDown(object? sender, KeyEventArgs e)
        {
            var entry = logger.Log(e.KeyData);
            if (entry == null)
                return;

            lstLog.Items.Add(entry);
            lstLog.TopIndex = lstLog.Items.Count - 1;
        }

        private void SaveLog()
        {
            // Kaydedilecek bir şey yoksa uyar
            if (logger.Entries.Count == 0)
            {
                MessageBox.Show("Kaydedilecek kayıt yok.", "Bilgi",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                FocusInput();
                return;
            }

            using var dialog = new SaveFileDialog
            {
                Title = "Kayıtları Kaydet",
                Filter = "Metin dosyası (*.txt)|*.txt",
                FileName = $"tus_kayitlari_{DateTime.Now:yyyyMMdd_HHmmss}.txt"
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    logger.SaveToFile(dialog.FileName);
                    MessageBox.Show("Kayıtlar kaydedildi:\n" + dialog.FileName, "Başarılı",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (UnauthorizedAccessException)
                {
                    MessageBox.Show("Bu klasöre yazma izniniz yok. Başka bir konum seçin.", "Hata",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (IOException ex)
                {
                    MessageBox.Show("Dosya kaydedilemedi:\n" + ex.Message, "Hata",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            FocusInput();
        }

        private void UpdateStatus()
        {
            if (logger.IsRecording)
            {
                lblStatus.Text = "● KAYIT AÇIK";
                lblStatus.BackColor = Color.SeaGreen;
            }
            else
            {
                lblStatus.Text = "■ KAYIT KAPALI";
                lblStatus.BackColor = Color.Firebrick;
            }

            btnStart.Enabled = !logger.IsRecording;
            btnStop.Enabled = logger.IsRecording;
            FocusInput();
        }

        private void FocusInput()
        {
            txtInput.Focus();
        }
    }
}