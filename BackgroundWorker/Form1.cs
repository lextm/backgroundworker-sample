using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BackgroundWorker
{
    public partial class Form1 : Form
    {
        CancellationTokenSource source;

        public Form1()
        {
            InitializeComponent();
            btnCancel.Enabled = false;
        }

        private async void btnStart_Click(object sender, EventArgs e)
        {
            btnStart.Enabled = false;
            btnCancel.Enabled = true;
            source = new CancellationTokenSource();
            for (int index = 0; index <= 10; index++)
            {
                txtProgress.Text = $"{10 * index}%";
                await Task.Delay(1000); // heavy task here.
                if (source.Token.IsCancellationRequested)
                {
                    break;
                }
            }

            txtProgress.Text = source.Token.IsCancellationRequested ? "canceled" : "finsihed";
            source.Dispose();
            btnStart.Enabled = true;
            btnCancel.Enabled = false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            btnCancel.Enabled = false;
            source?.Cancel();
        }
    }
}
