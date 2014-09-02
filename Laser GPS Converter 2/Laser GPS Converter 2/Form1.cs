using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Laser_GPS_Converter_2
{
	public partial class Form1 : Form
	{
		DataSet tracks;

		public Form1()
		{
			InitializeComponent();
		}

		private void Form_Load(object sender, EventArgs e)
		{
			SetOffsetDefault();
		}

		private void SetOffsetDefault()
		{
			TimeZone zone = TimeZone.CurrentTimeZone;
			n_Offset.Value = zone.GetUtcOffset(DateTime.Now).Hours;
		}

		private void btn_Load_Click(object sender, EventArgs e)
		{
			
			SetUpOpenDialog();

			list_Tracks.Items.Clear();

			DialogResult result = openFileDialog1.ShowDialog();

			if (result.Equals(DialogResult.Cancel))
			{
				return;
			}

			tracks = GetTracks();

			if (tracks.Equals(null))
			{
				//exception occurred
				return;
			}

			DataRowCollection dra = tracks.Tables["TrackPoint1"].Rows;
			for (int i = dra.Count - 1; i > -1; i--)
			{
				string l = dra[i][2] + ": " + dra[i][3].ToString().TrimEnd(' ') + " (" + Math.Round(Convert.ToDouble(dra[i][13]) / 100000, 2) + " km)";
				list_Tracks.Items.Add(l);
			}
		}

		private DataSet GetTracks()
		{
			OleDbConnection conn = null;
			DataSet t = new DataSet();
			string strAccessConn = @"Provider=Microsoft.JET.OLEDB.4.0;Data Source=" + openFileDialog1.FileName + "; Jet OLEDB:Database Password=danger";
			string strAccessSelect = "SELECT * FROM TrackPoint1";
			try
			{
				conn = new OleDbConnection(strAccessConn);
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error: Failed to create a database connection. \n{0}", ex.Message);
				return null;
			}

			try
			{
				OleDbCommand myAccessCommand = new OleDbCommand(strAccessSelect, conn);
				OleDbDataAdapter myDataAdapter = new OleDbDataAdapter(myAccessCommand);
				conn.Open();
				myDataAdapter.Fill(t, "TrackPoint1");
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error: Failed to create a database connection. \n{0}", ex.Message);
				return null;
			}
			finally
			{
				conn.Close();
			}
			return t;
		}

		private void SetUpOpenDialog()
		{
			string defaultinstalldir = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), "Laser GPS");
			if (System.IO.Directory.Exists(defaultinstalldir))
			{
				openFileDialog1.InitialDirectory = defaultinstalldir;
			}
			else
			{
				openFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
			}
		}

		private void btn_Export_Click(object sender, EventArgs e)
		{
			if (list_Tracks.SelectedIndices.Count == 0)
			{
				MessageBox.Show("No tracks are selected.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			else
			{
				DialogResult result = saveFileDialog1.ShowDialog();

				if (result == DialogResult.OK)
				{
					int[] cNumbers = new int[list_Tracks.SelectedIndices.Count];
					for (int i = 0; i < list_Tracks.SelectedItems.Count; i++)
					{
						cNumbers[i] = Int32.Parse(list_Tracks.SelectedItems[i].ToString().Substring(0, list_Tracks.SelectedItems[i].ToString().IndexOf(':')));
					}
					ExportTracks(cNumbers, n_Offset.Value);
				}
			}
		}

		private void ExportTracks(int[] t, decimal offset)
		{
			XmlWriterSettings xs = new XmlWriterSettings();
			xs.Indent = true;
			xs.NamespaceHandling = NamespaceHandling.OmitDuplicates;

			XmlWriter writer = XmlWriter.Create(saveFileDialog1.FileName, xs);
			
			writer.WriteStartDocument();
			writer.WriteStartElement("gpx", @"http://www.topografix.com/GPX/1/0");
			writer.WriteAttributeString("creator", "Laser GPS Converter");
			//writer.WriteAttributeString("xmlns", @"http://www.topografix.com/GPX/1/0");
			writer.WriteAttributeString("version", "1.0");
			//writer.WriteAttributeString("xlmns", "xsi", null, @"http://www.w3.org/2001/XMLSchema-instance");
			writer.WriteAttributeString("xsi", "schemaLocation", @"http://www.w3.org/2001/XMLSchema-instance", @"http://www.topografix.com/GPX/1/0 http://www.topografix.com/GPX/1/0/gpx.xsd");

			foreach (int i in t)
			{
				DataSet trackPoints = GetTrackPoints(i);
				DataRowCollection dra = trackPoints.Tables["TrackPoint"].Rows;
				
				writer.WriteStartElement("trk");
				writer.WriteStartElement("trkseg");
				
				foreach (DataRow dr in dra)
				{
					writer.WriteStartElement("trkpt");
					if (dr[5].ToString().Trim().Equals("N"))
						writer.WriteAttributeString("lat", dr[4].ToString());
					else
						writer.WriteAttributeString("lat", "-" + dr[4].ToString());

					if (dr[3].ToString().Trim().Equals("E"))
						writer.WriteAttributeString("lon", dr[2].ToString());
					else
						writer.WriteAttributeString("lon", "-" + dr[2].ToString());

					string[] timebits = new string[3];
					timebits = dr[1].ToString().Trim().Split(':');
					for (int j = 0; j < 3; j++)
					{
						timebits[j] = Int32.Parse(timebits[j]).ToString("D2");
					}

					string dt = dr[0].ToString().Substring(0, 10) + 'T' + String.Join(":",timebits);
					dt += offset > 0 ? '+' : '-';
					dt += ((int)offset).ToString("D2") + ':';
					dt += (offset - (int)offset == 0 ? "00" : "30");
					DateTime d = new DateTime(0, DateTimeKind.Local);
					d = DateTime.Parse(dt);

					writer.WriteElementString("time", d.ToUniversalTime().ToString("s") + 'Z');
					writer.WriteEndElement();
				}

				writer.WriteEndElement();
				writer.WriteEndElement();
			}

			writer.WriteEndDocument();
			writer.Close();
		}

		private DataSet GetTrackPoints(int cNumber)
		{
			string strAccessConn = @"Provider=Microsoft.JET.OLEDB.4.0;Data Source=" + openFileDialog1.FileName + "; Jet OLEDB:Database Password=danger";
			string strAccessSelect = "SELECT TrackPoint1.Track_Date, TrackPoint.TrackTime, TrackPoint.LongitudeN, TrackPoint.LonSign, TrackPoint.LatitudeN, TrackPoint.LatSign FROM TrackPoint, TrackPoint1 WHERE (((TrackPoint.cNumber)=[TrackPoint1].[cNumber]) AND ((TrackPoint1.cNumber)=" + cNumber + ")) ORDER BY TrackPoint.SerNO;";

			DataSet t = new DataSet();
			OleDbConnection myAccessConn = null;
			try
			{
				myAccessConn = new OleDbConnection(strAccessConn);
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error: Failed to create a database connection. \n{0}", ex.Message);
			}

			try
			{

				OleDbCommand myAccessCommand = new OleDbCommand(strAccessSelect, myAccessConn);
				OleDbDataAdapter myDataAdapter = new OleDbDataAdapter(myAccessCommand);

				myAccessConn.Open();
				myDataAdapter.Fill(t, "TrackPoint");

			}
			catch (Exception ex)
			{
				Console.WriteLine("Error: Failed to retrieve the required data from the DataBase.\n{0}", ex.Message);
			}
			finally
			{
				myAccessConn.Close();
			}

			return t;
		}

		private void list_Tracks_SelectedIndexChanged(object sender, EventArgs e)
		{
			UpdateTrackDetails();
		}

		private void UpdateTrackDetails()
		{
			//Should improve this to show different details when multiple tracks selected - count, date range, total duration
			int i = list_Tracks.Items.Count - (list_Tracks.SelectedIndex + 1);

			txt_Details.Clear();
			DataRow dr = tracks.Tables[0].Rows[i];
			txt_Details.AppendText("Started: " + dr[3].ToString().Trim());
			txt_Details.AppendText(Environment.NewLine + "Ended:   " + dr[5].ToString().Trim());
			txt_Details.AppendText(Environment.NewLine);

			DateTime t1 = ParseDate(dr, 3);
			DateTime t2 = ParseDate(dr, 5);
			TimeSpan duration = t2 - t1;
			txt_Details.AppendText(Environment.NewLine + "Duration: " + duration.ToString());

			txt_Details.AppendText(Environment.NewLine + "Distance: " + (Convert.ToDouble(dr[13]) / 100).ToString("n0") + " m");
		}

		private DateTime ParseDate(DataRow dr, int i)
		{
			string s = dr[i].ToString();
			s += n_Offset.Value > 0 ? '+' : '-';
			s += ((int)n_Offset.Value).ToString("D2") + ':';
			s += (n_Offset.Value - (int)n_Offset.Value == 0 ? "00" : "30");
			DateTime t = new DateTime(0, DateTimeKind.Local);
			t = DateTime.Parse(s);
			return t;
		}
	}
}
