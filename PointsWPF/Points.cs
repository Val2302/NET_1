
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using static System.Console;

namespace PointsWPF
{
    /// <summary>
    /// Observable sollection structs of points
    /// </summary>
    public class Points : ObservableCollection<Point>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName">Full path with file name</param>
        /// <returns>
        ///     true - read file complete successfully
        ///     false - file read filed
        /// </returns>
	    public bool ReadFile( string fileName )
	    {
			try
		    {
			    Clear( );
			    string line = string.Empty;
				string[] textCoordinats;

				using ( StreamReader sr = new StreamReader( fileName, System.Text.Encoding.Default ) ) 
				{
					while ( ( line = sr.ReadLine( ) ) != null )
					{
						textCoordinats = line.Split(';');
						double.TryParse( textCoordinats[ 0 ], out double x );
						double.TryParse( textCoordinats[ 1 ], out double y );
						Add( new Point()
						{
							X = x,
							Y = y
						});
					}
				}

			    return true;
		    }
		    catch ( FileNotFoundException e )
		    {  
			    return false;
		    }
	    }

    }
}
