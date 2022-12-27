using System;
using System.Windows.Forms;

namespace KouCoCoa
{
    internal partial class LoggerConsole : Form
    {
        public LoggerConsole()
        {
            InitializeComponent();
            foreach (LogLevel logLevel in Enum.GetValues(typeof(LogLevel))) {
                logLevelComboBox.Items.Add(logLevel.ToString());
            }
            Utilities.SetComboBoxIndex(logLevelComboBox, Globals.RunConfig.LoggingLevel.ToString());
        }

        private bool _backLogAdded = false;

        public void AddLogLine(string logLine)
        {
            if (!_backLogAdded) {
                loggerTextBox.AppendText(Logger.FullLog);
                _backLogAdded = true;
            }
            loggerTextBox.AppendText($"{logLine}{Environment.NewLine}");
        }

        private void logLevelComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Globals.RunConfig.LoggingLevel = Enum.TryParse(logLevelComboBox.SelectedItem.ToString(), out LogLevel logLevel)
                ? logLevel : Globals.RunConfig.LoggingLevel;
        }
    }
}
