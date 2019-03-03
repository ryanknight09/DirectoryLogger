using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Data.SQLite;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static GUI_FileSysWatcher.MainWindow;

namespace GUI_FileSysWatcher
{
    /// <summary>
    /// Interaction logic for DatabaseWindow.xaml
    /// </summary>
    public partial class DatabaseWindow : Window
    {
        private string _extension = ".";
        private ObservableCollection<FileData> theListDB = new ObservableCollection<FileData>();
        FileData dataDb;
        SQLiteConnection sqlite_conn;
        SQLiteCommand sqlite_cmd;
        SQLiteDataReader sqlite_datareader;
        public DatabaseWindow()
        {
            InitializeComponent();
            this.cleardb.IsEnabled = true;
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            if(this.gridBoxDB.Items.Count == 0)
            {
                QueryDB();
            } else
            {
                theListDB.Clear();
                this.gridBoxDB.ItemsSource = null;
                this.gridBoxDB.Items.Refresh();
                QueryDB();
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem typeItem = (ComboBoxItem)comboBox.SelectedItem;

            if (typeItem.Content.ToString().EndsWith("Any file type"))
            {
                this._extension = ".";
            }
            else
            {
                this._extension = typeItem.Content.ToString();
            }
        }

        private void extBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            this._extension = "." + this.extBox.Text;
        }

        private void QueryDB()
        {
            this.sqlite_conn = new SQLiteConnection("Data Source=database.db");
            this.sqlite_conn.Open();
            sqlite_cmd = sqlite_conn.CreateCommand();

            if (this._extension.Equals("."))
            {
                sqlite_cmd.CommandText = "SELECT * FROM watcherLog";
            }
            else
            {
                sqlite_cmd.CommandText = "SELECT * FROM watcherLog WHERE Ext = " + "'" + this._extension + "'";
            }

            sqlite_datareader = sqlite_cmd.ExecuteReader();

            while (sqlite_datareader.Read())
            {
                this.dataDb = new FileData(sqlite_datareader["FileName"].ToString(), sqlite_datareader["absPath"].ToString(),
                                           sqlite_datareader["Event"].ToString(), sqlite_datareader["Date"].ToString(),
                                           sqlite_datareader["Ext"].ToString());

                this.theListDB.Add(this.dataDb);
            }
            sqlite_conn.Close();

            this.gridBoxDB.ItemsSource = this.theListDB;
        }

        private void clearDB_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to clear the Database? It cannot be undone!!!", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                this.sqlite_conn = new SQLiteConnection("Data Source=database.db");
                this.sqlite_conn.Open();
                sqlite_cmd = sqlite_conn.CreateCommand();
                sqlite_cmd.CommandText = "DELETE FROM watcherlog";
                sqlite_datareader = sqlite_cmd.ExecuteReader();
                sqlite_conn.Close();
                this.cleardb.IsEnabled = false;
                this.showCleardb.Content = "Database has been Cleared!";
            }
                
        }
    }
}
