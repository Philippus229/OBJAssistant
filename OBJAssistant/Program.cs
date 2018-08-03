using System;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;

namespace OBJAssistant {
	public class Program {
			[STAThread]
			public static void Main (string[] args) {
				string objPath = "";
				Console.WriteLine("Select an OBJ file");  
				OpenFileDialog openFileDialog1 = new OpenFileDialog();  
				openFileDialog1.Filter = "OBJ Files|*.obj";  
				openFileDialog1.Title = "Select an OBJ file";
				DialogResult result1 = openFileDialog1.ShowDialog();
				if (result1 == DialogResult.OK) {
					objPath = openFileDialog1.FileName;
				}
				Program p = new Program();
				p.editObjFile(objPath);
			}

			public void editObjFile(string oP) {
			string[] objLines = File.ReadAllLines (oP);
			List<string> verts = new List<string> ();
			List<string> faces = new List<string> ();
			List<string> objContents = new List<string> ();
			foreach (string line in objLines) {
				if (line.StartsWith ("v ")) {
					verts.Add (line);
				} else if (line.StartsWith ("f ")) {
					faces.Add (line);
				}
			}
			string[] vertArray = verts.ToArray ();
			int counter = 0;
			foreach (string vert in vertArray) {
				int charIndex2 = vertArray [counter].IndexOf ('.');
				while (charIndex2 > 0) {
					int charIndex = vertArray [counter].IndexOf ('.');
					char character = '.';
					while (character == '0'
					        || character == '1'
					        || character == '2'
					        || character == '3'
					        || character == '4'
					        || character == '5'
					        || character == '6'
					        || character == '7'
					        || character == '8'
					        || character == '9'
					        || character == '.') {
						vertArray [counter] = vertArray [counter].Remove (charIndex, 1);
						if ((charIndex + 1) <= vertArray [counter].Length) {
							character = vertArray [counter] [charIndex];
						} else {
							character = ' ';
						}
					}
					charIndex2 = vertArray [counter].IndexOf ('.');
				}
				counter++;
			}
			string[] faceArray = faces.ToArray ();
			for (int i = 0; i < vertArray.Length; i++) {
				objContents.Add (vertArray [i]);
			}
			objContents.Add ("");
			objContents.Add ("g 0000");
			objContents.Add ("");
			for (int i = 0; i < faceArray.Length; i++) {
				objContents.Add (faceArray [i]);
			}
			File.WriteAllText (oP, String.Empty);
			File.WriteAllLines (oP, objContents.ToArray ());
		}
	}
}

