
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Windows;
using System.IO;
using System.Text;
using System.Windows.Controls;
using System.Collections.Specialized;
using System;

namespace PointsWPF
{
    /**
	 * <summary>
     *  Interaction logic for MainWindow.xaml
     * </summary>
	 */
    public partial class MainWindow : Window
    {
        /**
		 * <summary>
         *  Observable sollection points
         * </summary>
		 */
        private ObservableCollection<Point> pointsCollection;

        /**
		 * <summary>
         *  Constructor MainWindow class
         * </summary>
		 */
        public MainWindow ( )
        {
            InitializeComponent( );
            pointsCollection = new ObservableCollection<Point>( );
            pointsGrid.ItemsSource = pointsCollection;
            pointsCollection.Add( new Point( ) );
        }

        /**
		 * <summary>
         *  Handler event click on "OpenFile" button
         *  Read data of file into table if it posible
         * </summary>
         * <param name="sender">sender object</param>
         * <param name="e">appendix information about event</param>
		 */
        private void OpenFile_Click ( object sender, RoutedEventArgs e )
        {
            statusBar.Items.Clear( );
            string initialPath = Path.GetDirectoryName( Assembly.GetExecutingAssembly( ).GetName( ).CodeBase ).Substring( 6 );
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Open points file",
                InitialDirectory = initialPath,
                Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*",
                FilterIndex = 1,
                RestoreDirectory = true
            };

            if ( openFileDialog.ShowDialog( ) == true )
            {
                try
                {
					pointsCollection = ReadFile(openFileDialog.FileName);
					statusBar.Items.Add( "File read completed successfully" );
                }
                catch( FileNotFoundException )
                {
                    statusBar.Items.Add( "File read filed" );
                }
                
                catch ( FormatException exception )
                {
                    MessageBox.Show( exception.Message, "Format exception", MessageBoxButton.OK, MessageBoxImage.Error );
                }
            }
        }

        /** 
		 * <summary>
		 * Read file and return collection of it
		 * </summary>
		 * <param name="fileName">Full path with file name</param>
		 * <returns>
		 *	ObservableCollection &lt;Point&gt; - collecrion read from file
		 * </returns>
		 */
        private ObservableCollection<Point> ReadFile ( string fileName )
        {
            ObservableCollection<Point> points = new ObservableCollection<Point>( );
            using ( StreamReader sr = new StreamReader( fileName, Encoding.Default ) )
            {
                string line = string.Empty;
                string[ ] textCoordinats = null;
                int lineNumber = 1;
                bool isValid;
                double x = 0, y = 0;

                while ( ( line = sr.ReadLine( ) ) != null  )
                {
                    textCoordinats = line.Split( ';' );
                    isValid = double.TryParse( textCoordinats[ 0 ], out x ) && double.TryParse( textCoordinats[ 1 ], out y );
                    
                    if ( !isValid )
                    {
                        throw new FormatException( $"Not parse line {lineNumber}" );
                    }
                    
                    Point point = new Point( x, y );
                    points.Add( point );
                    lineNumber++;
                }

                return points;
            }
        }

        /**
		 * <summary>
		 *  Handler event click on "Add" button
         *  Addition new row in table using method observable collection
         * </summary>
         * <param name="sender">sender object</param>
         * <param name="e">appendix information about event</param>
		 */
        private void Add_Click ( object sender, RoutedEventArgs e )
        {
            pointsCollection.Add( new Point( ) );
        }

        /**
		 * <summary>
         *  Handler event click on "Add" button
         *  Remove current row of table using method observable collection
         * </summary>
         * <param name="sender">sender object</param>
         * <param name="e">appendix information about event</param>
		 */
        private void Delete_Click ( object sender, RoutedEventArgs e )
        {
            if ( pointsGrid.SelectedItems.Count > 0 )
            {
                var point = ( Point ) pointsGrid.SelectedItems[ 0 ];
                pointsCollection.Remove( point );
            }
        }

        /**
		 * <summary>
         *  Handler event row edit of points data grid
         *  Edit value of points observable collection
         * </summary>
         * <param name="sender">sender object</param>
         * <param name="e">appendix information about event</param>
		 */
        private void PointsGrid_RowEditEnding ( object sender, DataGridRowEditEndingEventArgs e )
        {
            var indexRow = e.Row.GetIndex( );
            pointsCollection[ indexRow ] = ( Point) e.Row.Item;
        }
    }
}