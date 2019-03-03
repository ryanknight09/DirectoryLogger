using System;
using System.IO;
using System.Threading.Tasks;
using System.Security.Permissions;
using System.Data.SQLite;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GUI_FileSysWatcher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<FileData> theList = new ObservableCollection<FileData>();
        private String extention;
        private String dir;
        private FileSystemWatcher Watcher;
        SQLiteConnection sqlite_conn;
        SQLiteCommand sqlite_cmd;

        public ObservableCollection<FileData> List
        {
            get
            {
                return this.theList;
            }
        }

        public class FileData
        {
            private string _fileName;
            private string _absPath;
            private string _eventType;
            private string _date;
            private string _ext;

            public FileData(string fileName, string absPath, string eventType, string date, string ext)
            {
                this._fileName = fileName;
                this._absPath = absPath;
                this._eventType = eventType;
                this._date = date;
                this._ext = ext;
            }

            public string FileName
            {
                get
                {
                    return this._fileName;
                }
                set
                {
                    this._fileName = value;
                }
            }

            public string Extenion
            {
                get
                {
                    return this._ext;
                }
                set
                {
                    this._ext = value;
                }
            }

            public string EventType
            {
                get
                {
                    return this._eventType;
                }
                set
                {
                    this._eventType = value;
                }
            }

            public string Date
            {
                get
                {
                    return this._date;
                }
                set
                {
                    this._date = value;
                }
            }

            public string absPath
            {
                get
                {
                    return this._absPath;
                }
                set
                {
                    this._absPath = value;
                }
            }
        }


        public MainWindow()
        {
            InitializeComponent();
            SetupDB();
        }

        private void SetupWatcher()
        {
            this.gridBox.ItemsSource = theList;
            Watcher = new FileSystemWatcher(@dir);
                Watcher.IncludeSubdirectories = true;
            Watcher.Filter = "*" + this.extention + "*";

                Watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName |
                                        NotifyFilters.DirectoryName;
           
                Watcher.Changed += new FileSystemEventHandler(OnChanged);
                Watcher.Created += new FileSystemEventHandler(OnChanged);
                Watcher.Deleted += new FileSystemEventHandler(OnChanged);
                //Watcher.Renamed += new RenamedEventHandler(OnRenamed);

                Watcher.EnableRaisingEvents = true;
        }

        private void OnChanged(Object source, FileSystemEventArgs e)
        {
            
                Dispatcher.BeginInvoke(
                  (Action)(() =>
                  {
                      FileData myData = new FileData(System.IO.Path.GetFileName(e.Name), e.FullPath, e.ChangeType.ToString(), DateTime.Now.ToLongDateString(), System.IO.Path.GetExtension(e.FullPath));
                      theList.Add(myData);
                      
                  }));
            
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            this.showDir.Content = "Directory: " + this.dir;
            SetupWatcher();
            this.Querydb.IsEnabled = false;
            this.Start.IsEnabled = false;
            this.Stop.IsEnabled = true;
        }

        private void CustomBoxExt_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.extention = "." + this.customExt.Text;
        }

        private void directoryIn_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.dir = this.directoryIn.Text;
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            this.Watcher.Dispose();
            this.showDir.Content = "******* Watcher has stopped *******";
            this.Stop.IsEnabled = false;
            this.Start.IsEnabled = true;
            this.Querydb.IsEnabled = true;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Copywrite 2018 Ryan Knight .Net FrameWork 64 bit. This" +
                            "program will watch a given directory for events that happen within it. " +
                            "To begin, select a file extension from the drop down menu or type your own into " +
                            "the text field. Next enter your directory that you wish to watch then click start. " +
                            "You can click stop at anytime and start again after at anytime. " +
                            "If you choose to add the current log to a database the log will clear but you can click " +
                            "query to search for past log files. ");
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Version 1.0, and probly the last...  :)");
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Made by Ryan Knight");
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem typeItem = (ComboBoxItem)comboBox.SelectedItem;

            if(typeItem.Content.ToString().EndsWith("Any file type"))
            {
                this.extention = ".";
            } else
            {
                this.extention = typeItem.Content.ToString();
            }
            
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Do you want to clear this window? Data will be lost without writting to database first!", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                theList.Clear();
                this.gridBox.ItemsSource = null;
                this.gridBox.Items.Refresh();
            }
            
        }

        private void SetupDB()
        {
            if (File.Exists("database.db"))
            {
                this.sqlite_conn = new SQLiteConnection("Data Source=database.db");
                this.sqlite_conn.Open();
                this.sqlite_cmd = sqlite_conn.CreateCommand();

                foreach(FileData d in theList)
                {
                    this.sqlite_cmd.CommandText = "INSERT INTO watcherLog (FileName, absPath, Event, Date, Ext) VALUES ('"+ d.FileName + "', '" + d.absPath + "', '" + d.EventType + "', '" + d.Date + "', '" + d.Extenion + "');";
                    this.sqlite_cmd.ExecuteNonQuery();
                }

                this.sqlite_conn.Close();
                
            } else
            {
                SQLiteConnection.CreateFile("database.db");
                this.sqlite_conn = new SQLiteConnection("Data Source=database.db");
                this.sqlite_conn.Open();
                this.sqlite_cmd = sqlite_conn.CreateCommand();
                this.sqlite_cmd.CommandText = "CREATE TABLE watcherLog (FileName varchar(50), absPath varchar(100), Event varchar(25), Date varchar(25), Ext varchar(10));";
                this.sqlite_cmd.ExecuteNonQuery();

                foreach (FileData d in theList)
                {
                    this.sqlite_cmd.CommandText = "INSERT INTO watcherLog (FileName, absPath, Event, Date, Ext) VALUES ('" + d.FileName + "', '" + d.absPath + "', '" + d.EventType + "', '" + d.Date + "', '" + d.Extenion + "');";
                    this.sqlite_cmd.ExecuteNonQuery();
                }

                this.sqlite_conn.Close();
            }
            
        }

        private void ToDB_Click(object sender, RoutedEventArgs e)
        {
            Window Dbw = new DatabaseWindow();
            Dbw.ShowDialog();
        }

        private void addDB_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Current log will clear after writting to database. Continue?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                SetupDB();
                this.theList.Clear();
                this.gridBox.ItemsSource = null;
                this.gridBox.Items.Refresh();
                this.showDir.Content = "Logs written to database succesfully!";
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Write current log to database before close?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                SetupDB();
                theList.Clear();
                this.gridBox.ItemsSource = null;
                this.gridBox.Items.Refresh();
                Environment.Exit(0);
            } else
            {
                Environment.Exit(0);
            }
        }
    }
}
