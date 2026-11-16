using System.Text;

namespace YMH219_KeyEventLogger
{
    // Kayıtları ve kayıt durumunu yöneten sınıf
    public class KeyLogger
    {
        private readonly List<KeyLogEntry> entries = new List<KeyLogEntry>();

        public IReadOnlyList<KeyLogEntry> Entries => entries;
        public bool IsRecording { get; private set; }

        public void Start() => IsRecording = true;
        public void Stop() => IsRecording = false;
        public void Clear() => entries.Clear();

        public KeyLogEntry? Log(Keys keyData)
        {
            if (!IsRecording)
                return null;

            var entry = new KeyLogEntry(DateTime.Now, FormatKey(keyData));
            entries.Add(entry);
            return entry;
        }

        // Tüm kayıtları verilen .txt dosyasına yazar
        // Hata olursa (izin yok, dosya açık vb.) exception fırlatır, Form yakalar
        public void SaveToFile(string path)
        {
            using var writer = new StreamWriter(path, false, Encoding.UTF8);

            writer.WriteLine("Key Event Logger - Tuş Kayıtları");
            writer.WriteLine($"Oluşturulma: {DateTime.Now:dd.MM.yyyy HH:mm:ss}");
            writer.WriteLine($"Toplam tuş: {entries.Count}");
            writer.WriteLine(new string('-', 40));

            foreach (var entry in entries)
                writer.WriteLine(entry.ToString());
        }

        private static string FormatKey(Keys keyData)
        {
            Keys key = keyData & Keys.KeyCode;
            var parts = new List<string>();

            if (keyData.HasFlag(Keys.Control) && key != Keys.ControlKey) parts.Add("Ctrl");
            if (keyData.HasFlag(Keys.Alt) && key != Keys.Menu) parts.Add("Alt");
            if (keyData.HasFlag(Keys.Shift) && key != Keys.ShiftKey) parts.Add("Shift");

            parts.Add(key.ToString());
            return string.Join(" + ", parts);
        }
    }
}