
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using static System.Console;

namespace ConsolePoints
{
    public class MainMenu
    {
        private List<Point> points;
       
        public MainMenu ( )
        {
            points = new List<Point>( );
        }

        public void ShowMenu ( )
        {
            Clear( );
            WriteLine( "  Menu:" );
            WriteLine( "1.Input points" );
            WriteLine( "2.Load of file" );
            WriteLine( "3.Show points" );
            WriteLine( "0.Exit" );
            Write( "№ : " );
            string number = ReadLine( );
            Clear( );

            switch ( number )
            {
                case "1":
                    ReadPointsOfConsole( );
                    break;
                case "2":
                    LoadOfFile( );
                    break;
                case "3":
                    ShowPoints( );
                    break;
                case "0":
                    return;
            }

            ShowMenu( );
        }

        private void ReadPointsOfConsole ( )
        {
            uint countPoints = ReadCountPoints( );

            for ( int i = 0; i < countPoints; i++ )
            {
                points.Add( ReadPoint( i + 1 ) );
            }
        }

        private uint ReadCountPoints ( )
        {
            Write( "Count points = " );
            string inputValue = ReadLine( );
            bool isValidParse = uint.TryParse( inputValue, out uint intCountPoints );
            WriteLine( );

            return isValidParse ? intCountPoints : ReadCountPoints( );
        }

        private Point ReadPoint ( int indexPoint )
        {
            WriteLine( $"Point {indexPoint}:" );
            double x = ReadCoordinate( "X" );
            double y = ReadCoordinate( "Y" );
            Point point = new Point( x, y );
            WriteLine( );
            return point;
        }

        private double ReadCoordinate ( string nameCoordinate )
        {
            Write( $"{nameCoordinate} = " );
            string inputValue = ReadLine( );
            bool isValidParse = double.TryParse( inputValue, out double coordinate );
            return isValidParse ? coordinate : ReadCoordinate( nameCoordinate );
        }

        private void LoadOfFile ( )
        {
            Write( "File name : " );
            string fileName = ReadLine( );

            try
            {
                points = ReadListPointsOfFile( fileName );
                WriteLine( "File read completed successfully" );
            }
            catch ( FileNotFoundException )
            {
                WriteLine( "File not found" );
            }
            catch ( FormatException e )
            {
                WriteLine( $"Format exception: \"{e.Message}\"" );
            }

            WriteLine( "Press any key to continue..." );
            ReadKey( );
        }

        private List<Point> ReadListPointsOfFile ( string fileName )
        {
            using ( StreamReader sr = new StreamReader( fileName, Encoding.Default ) )
            {
                string line = string.Empty;
                string[ ] textCoordinats = null;
                List<Point> points = new List<Point>( );
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

        private void ShowPoints ( )
        {
            string textPoints = string.Join( "\n", points );
            WriteLine( "Points:" );
            WriteLine( textPoints );
            WriteLine( "Press any key to continue..." );
            ReadKey( );
        }
    }
}
