using System.ComponentModel;

namespace TaskReminder.Gui
{
    public partial class mainForm : Form
    {
        private readonly TaskRManager manager = new("tasks.json");
        private readonly BackgroundWorker backgroundWorker;

        public mainForm()
        {
            InitializeComponent();

            backgroundWorker = new BackgroundWorker
            {
                WorkerReportsProgress = true,
                WorkerSupportsCancellation = true
            };

            //backgroundWorker.DoWork += BackgroundWorker_DoWork;
            //backgroundWorker.ProgressChanged += BackgroundWorker_ProgressChanged;
            //backgroundWorker.RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted;

        }

        
    }
}
