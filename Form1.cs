using System.Configuration;
namespace Timer
{
    public partial class Form1 : Form
    {
        private int IntervalCount = 0;

        private List<string> Messages = new List<string>();

        public Form1()
        {
            InitializeComponent();
            LoadMessages();
        }

        private void LoadMessages()
        {
            var enumerator = ConfigurationManager.AppSettings.GetEnumerator();
            while (enumerator.MoveNext()) {
                var key = enumerator.Current.ToString();
                if (key == null || !key.StartsWith("message_"))
                {
                    continue;
                }
                var value = ConfigurationManager.AppSettings[key];
                if (value == null) {
                    continue;
                }
                Messages.Add(value);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "&Start")
            {
                timer1.Interval = 1000;
                timer1.Start();
                button1.Text = "&Stop";
                return;
            }
            timer1.Stop();
            IntervalCount = 0;
            button1.Text = "&Start";
            textBox1.Text = "00:00";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.Text == "&Pauze")
            {
                timer1.Stop();
                button2.Text = "&Doorgaan";
                return;
            }
            button2.Text = "&Pauze";
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            var minutes = IntervalCount / 60;
            var modulo = IntervalCount % 60;
            textBox1.Text = $"{minutes.ToString("00")}:{modulo.ToString("00")}";
            IntervalCount++;
            if (modulo == 0 && minutes > 0)
            {
                var messagePosition = minutes - 1;
                while (messagePosition >= Messages.Count())
                {
                    messagePosition -= Messages.Count();
                    if (messagePosition < 0)
                    {
                        messagePosition = 0;
                    }
                }
                label1.Text = Messages[messagePosition];
            }
        }

    }
}
