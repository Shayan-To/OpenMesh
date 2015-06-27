using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using IO = System.IO;

namespace OpenMesh
{

    public partial class MainForm : Form
    {

        #region Initialization Logic
        static MainForm()
        {
            var T = new ShapeFileFormat[] { new Type1ShapeFileFormat(), new Type2ShapeFileFormat() };

            ShapeFileFormatKeys = new String[T.Length];
            ShapeFileFormats = new Dictionary<String, ShapeFileFormat>();

            var I = 0;
            foreach (var i in T)
            {
                ShapeFileFormatKeys[I] = i.Name;
                ShapeFileFormats.Add(i.Name, i);
                I++;
            }
        }

        public MainForm()
        {
            Console.WriteLine(1);
            this.InitializeComponent();
            Console.WriteLine(2);

            this.ShapeDrawer = this.shapeDrawingPane1.ShapeDrawer;
            this.EmptyRecentProjects = this.emptyToolStripMenuItem1;
            this.EmptyRecentShapes = this.emptyToolStripMenuItem;

            this.InitTab1();
            this.InitTab2();

            this.ReadRecents();
            Console.WriteLine(3);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Shape S;
            Console.WriteLine(4);

            //var S = new LinesShape(new Line(-1, -1, 1, 1), new Line(-1, 1, 1, -1)) { Name = "Cross" };

            //this.Project.Shape.Shapes.Add(S);
            //this.ShapeDrawer.ResetView();

            //var ColoredCode = new FortranColorizer(Code.Code1).ColorizedCode;
            //Debug.WriteLine("Hello");
            //this.richTextBox1.Rtf = ColoredCode;
            //this.richTextBox1.Text = ColoredCode;
        }

        private void InitTab1()
        {
            this.ShapeDrawer.Changed += this.ShapeDrawer_Changed;

            //this.shapeDrawingPane1.ShapeDrawer.BoardBounds = Rectangle.FromCornerSize(new PointF(), this.shapeDrawingPane1.Size.ToPointF());
            //this.shapeDrawingPane1.ShapeDrawer.ResetView();
            this.scalePane1.ShapeDrawer = this.ShapeDrawer;

            this.propertyEditor1.BaseControls.Add(this.button7);
            this.propertyEditor1.BaseControls.Add(this.button8);

            var Filter = "";
            foreach (var i in ShapeFileFormatKeys)
            {
                Filter += "|" + i + "|*.txt";
            }
            this.openFileDialog1.Filter = Filter.Substring(1);
        }

        private void InitTab2()
        {
            this.shapeTreeViewer2.ShowMoveButtons = false;
            this.shapeDrawingPane2.IsInteractive = false;

            //this.shapeDrawingPane2.ShapeDrawer.BoardBounds = Rectangle.FromCornerSize(new PointF(), this.shapeDrawingPane2.Size.ToPointF());
            //this.shapeDrawingPane2.ShapeDrawer.ResetView();

            foreach (var i in ShapeFileFormats)
            {
                this.comboBox1.Items.Add(i.Key);
                this.comboBox2.Items.Add(i.Key);
            }

            this.Project =
                new Project()
                {
                    InputFileTypeKey = ShapeFileFormatKeys[0],
                    OutputFileTypeKey = ShapeFileFormatKeys[1]
                };
            this.CloseProject();
        }
        #endregion

        #region Pan&Zoom Logic
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            if (this.ShapeDrawer.IsWorking && this.TrackBarWorks)
            {
                this.ShapeDrawer.RealScale = (Single)Math.Pow(10.0, this.trackBar1.Value / 100.0);
            }
        }

        private void ShapeDrawer_Changed(Object Sender, EventArgs E)
        {
            this.TrackBarWorks = false;
            this.trackBar1.Value = (int)(Math.Log(this.ShapeDrawer.RealScale, 10) * 100);
            this.TrackBarWorks = true;

            this.shapeDrawingPane2.ShapeDrawer.FillView();
        }

        // Fill View
        private void button4_Click(object sender, EventArgs e)
        {
            this.ShapeDrawer.FillView();
        }

        // ResetView
        private void button3_Click(object sender, EventArgs e)
        {
            this.ShapeDrawer.ResetView();
        }

        private Boolean TrackBarWorks = true;
        #endregion

        #region ShapeManipulation Logic
        private ShapeBase AddIndicators(ShapeBase ShapeBase)
        {
            if (ShapeBase is ShapeWithIndicator)
            {
                return ShapeBase;
            }

            var Collection = ShapeBase as ShapeCollection;
            if (Collection != null)
            {
                for (int i = 0; i < Collection.Shapes.Count; i++)
                {
                    this.AddIndicators(Collection.Shapes[i]);
                }
                return Collection;
            }

            var Shape = ShapeBase as Shape;
            if (Shape == null || Shape is IndicatorShape)
                return ShapeBase;

            var Parent = Shape.Parent as ShapeCollection;
            if (Parent == null || Parent is ShapeWithIndicator)
            {
                return ShapeBase;
            }

            var I = Parent.Shapes.IndexOf(Shape);
            var SI = new ShapeWithIndicator(Shape);
            Parent.Shapes[I] = SI;

            return SI;
        }

        // Add Indicators
        private void button7_Click(object sender, EventArgs e)
        {
            var Selected = this.shapeTreeViewer1.SelectedShape;
            if (Selected == null)
                return;

            this.shapeTreeViewer1.SelectedShape = this.AddIndicators(Selected);
        }

        private ShapeBase RemoveNode(ShapeBase Shape)
        {
            var Parent = Shape.Parent as ShapeCollection;
            //Console.WriteLine(Shape.Parent);
            if (Parent == null)
                return Shape;

            if (Shape is IndicatorShape)
            {
                var IP = Parent as ShapeWithIndicator;
                if (IP != null && IP.Shapes.Count == 2)
                {
                    var PParent = IP.Parent as ShapeCollection;
                    if (PParent != null)
                    {
                        var I = PParent.Shapes.IndexOf(IP);
                        var J = IP.Shapes.IndexOf(Shape);

                        Shape = IP.Shapes[1 - J];
                        PParent.Shapes[I] = Shape;
                        return Shape;
                    }
                }
            }

            var II = Parent.Shapes.IndexOf(Shape);
            Parent.Shapes.RemoveAt(II);
            if (Parent.Shapes.Count == 0)
                return this.RemoveNode(Parent);

            if (II < Parent.Shapes.Count)
                return Parent.Shapes[II];
            return Parent.Shapes[II - 1];
        }

        // Remove Node
        private void button8_Click(object sender, EventArgs e)
        {
            var Shape = this.shapeTreeViewer1.SelectedShape;
            if (Shape == null)
                return;

            this.shapeTreeViewer1.SelectedShape = this.RemoveNode(Shape);
        }

        private void shapeTreeViewer1_SelectedShapeChanged(object sender, EventArgs e)
        {
            this.propertyEditor1.EditingObject = this.shapeTreeViewer1.SelectedShape;
        }
        #endregion

        #region ShapeLoad Logic
        // Open Shape
        private void openExistingToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (this.openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.LoadShape(this.openFileDialog1.FileName,
                               ShapeFileFormats[ShapeFileFormatKeys[this.openFileDialog1.FilterIndex - 1]]);
            }
        }

        private void LoadShape(String Path, ShapeFileFormat Format)
        {
            try
            {
                using (var Reader = new IO.StreamReader(IO.File.Open(Path, IO.FileMode.Open, IO.FileAccess.Read, IO.FileShare.Read)))
                {
                    this.Project.Shape.Shapes.Add(Format.ReadShape(Reader));
                }
                this.AddShapeRecent(Path, Format);
                this.SaveProject();
            }
            catch (Exception ex)
            {
                ErrorDialog.Show("Could not load file.", ex, new StackTrace(true));
            }
        }
        #endregion

        #region Running Logic
        #region Run
        private void button2_Click(object sender, EventArgs e)
        {
            this.ResetRtf();
            if (!this.CheckProjectExitst(false))
            {
                this.LogRtf("Project file not found.");
                ErrorDialog.Show("Project file not found.");
                return;
            }

            this.tableLayoutPanel2.Enabled = false;

            var Success = false;
            try
            {
                var CompilerDirectory = IO.Path.Combine(Utils.GetParentPath(Application.ExecutablePath),
                                                        "MinGW");
                var CompilerPath = IO.Path.Combine(CompilerDirectory, "bin", "gfortran.exe");
                if (!IO.File.Exists(CompilerPath))
                {
                    this.LogRtf("Compiler not found.");
                    ErrorDialog.Show("Compiler not found.");
                    return;
                }

                XDocument ProjectXDocument = null;
                try
                {
                    this.LogRtf("Reading project file...");
                    var ProjectFile = IO.File.ReadAllText(this.Project.ProjectFilePath);
                    ProjectXDocument = XDocument.Parse(ProjectFile);
                }
                catch (Exception ex)
                {
                    this.LogRtf("Error reading the project file.");
                    ErrorDialog.Show("Error reading the project file.", ex, new StackTrace(true));
                    return;
                }

                var Project = ProjectXDocument.Element("CodeBlocks_project_file")
                                              .Element("Project");

                this.ExecutablePath = IO.Path.Combine(Project.Element("Build")
                                                             .Element("Target")
                                                             .Elements("Option")
                                                             .Attributes("output")
                                                             .First()
                                                             .Value
                                                             .Split('/'));
                this.ExecutablePath = IO.Path.Combine(Utils.GetParentPath(this.Project.ProjectFilePath),
                                                      this.ExecutablePath);
                this.ExecutablePath = IO.Path.ChangeExtension(this.ExecutablePath, "exe");
                this.ExecutableDirectory = Utils.GetParentPath(this.ExecutablePath);
                if (!IO.Directory.Exists(this.ExecutableDirectory))
                {
                    this.LogRtf("Creating output directory '" + this.ExecutableDirectory + "'...");
                    IO.Directory.CreateDirectory(this.ExecutableDirectory);
                }
                if (IO.File.Exists(ExecutablePath))
                {
                    try
                    {
                        this.LogRtf("Previously compiled file found. Deleting it...");
                        IO.File.Delete(ExecutablePath);
                    }
                    catch (Exception ex)
                    {
                        this.LogRtf("Cannot delete file.");
                        ErrorDialog.Show("Cannot delete file.", ex, new StackTrace(true));
                        return;
                    }
                }

                this.LogRtf("Reading source file names from project...");
                var Args = new StringBuilder(@"-Wall -o """).Append(IO.Path.GetFullPath(this.ExecutablePath)).Append('"');
                foreach (var U in Project.Elements("Unit"))
                {
                    var FileName = U.Attribute("filename").Value;
                    FileName = IO.Path.Combine(Utils.GetParentPath(this.Project.ProjectFilePath), FileName);
                    Args.Append(@" """).Append(IO.Path.GetFullPath(FileName)).Append('"');
                }

                var Prc = new Process();

                Prc.StartInfo = new ProcessStartInfo(CompilerPath, Args.ToString());
                //Prc.StartInfo = new ProcessStartInfo(@"Z:\home\shayan\Visual Studio 2010\Projects\OpenMesh\UnstructureMesh2D\2D Unstructure mesh\OpenMesh2D.exe");
                //Prc.StartInfo.WorkingDirectory = @"Z:\home\shayan\Visual Studio 2010\Projects\OpenMesh\UnstructureMesh2D\2D Unstructure mesh";
                //Prc.StartInfo.CreateNoWindow = true;
                //Prc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                //Prc.StartInfo.UseShellExecute = false;
                //Prc.StartInfo.RedirectStandardOutput = true;
                //Prc.StartInfo.RedirectStandardError = true;

                Prc.EnableRaisingEvents = true;
                Prc.Exited += Compiler_Exited;
                Prc.OutputDataReceived += this.Process_OutputDataReceived;
                Prc.ErrorDataReceived += this.Process_ErrorDataReceived;

                try
                {
                    this.LogRtf("Starting compilation: " + Prc.StartInfo.FileName + " " + Prc.StartInfo.Arguments);
                    Prc.Start();
                    Success = true;
                }
                catch (Exception ex)
                {
                    this.LogRtf("Error running the compiler.");
                    ErrorDialog.Show("Error running the compiler.", ex, new StackTrace(true));
                    return;
                }
            }
            finally
            {
                if (!Success)
                    this.tableLayoutPanel2.Enabled = true;
            }
        }

        private void Compiler_Exited(object sender, EventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action(() => { this.Compiler_Exited(sender, e); }));
                return;
            }

            var Proc = (Process)sender;
            var ExitCode = Proc.ExitCode;

            this.LogRtf("Compiler exited with exit code '" + ExitCode + "'.");
            Proc.Dispose();

            if (ExitCode != 0 || !IO.File.Exists(this.ExecutablePath))
            {
                this.LogRtf("Compilation not successful.");
                ErrorDialog.Show("Compilation not successful.");
                this.tableLayoutPanel2.Enabled = true;
                return;
            }
            this.LogRtf("Compilation successfull.");

            try
            {
                var InpFilePath = IO.Path.Combine(this.ExecutableDirectory, this.Project.InputFileName);
                var Format = ShapeFileFormats[this.Project.InputFileTypeKey];

                this.LogRtf("Writing to the input file: " + InpFilePath);
                using (var Writer = new IO.StreamWriter(IO.File.Open(InpFilePath, IO.FileMode.Create, IO.FileAccess.Write, IO.FileShare.Read)))
                {
                    Format.WriteShape(new ShapeBag(this.Project.ShapesForInput), Writer);
                }
            }
            catch (Exception ex)
            {
                this.LogRtf("Error writing the input file.");
                ErrorDialog.Show("Error writing the input file.", ex, new StackTrace(true));
            }

            var Prc = new Process();
            Prc.StartInfo = new ProcessStartInfo(this.ExecutablePath);
            Prc.StartInfo.WorkingDirectory = this.ExecutableDirectory;
            //Prc.StartInfo.CreateNoWindow = true;
            //Prc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            //Prc.StartInfo.UseShellExecute = false;
            //Prc.StartInfo.RedirectStandardOutput = true;
            //Prc.StartInfo.RedirectStandardError = true;

            Prc.EnableRaisingEvents = true;
            Prc.Exited += this.Process_Exited;
            Prc.OutputDataReceived += this.Process_OutputDataReceived;
            Prc.ErrorDataReceived += this.Process_ErrorDataReceived;

            try
            {
                this.LogRtf("Starting the program: " + Prc.StartInfo.FileName + " " + Prc.StartInfo.Arguments);
                Prc.Start();
            }
            catch (Exception ex)
            {
                this.LogRtf("Error starting the program.");
                ErrorDialog.Show("Error starting the program.", ex, new StackTrace(true));
                this.tableLayoutPanel2.Enabled = true;
            }
        }

        private void Process_Exited(object sender, EventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action(() => { this.Process_Exited(sender, e); }));
                return;
            }

            var Proc = (Process)sender;

            this.LogRtf("Program exited with exit code '" + Proc.ExitCode + "'.");
            Proc.Dispose();

            try
            {
                var OutFilePath = IO.Path.Combine(this.ExecutableDirectory, this.Project.OutputFileName);
                var Format = ShapeFileFormats[this.Project.OutputFileTypeKey];

                this.LogRtf("Reading from the output file: " + OutFilePath);
                using (var Reader = new IO.StreamReader(IO.File.Open(OutFilePath, IO.FileMode.Open, IO.FileAccess.Read, IO.FileShare.Read), true))
                {
                    try
                    {
                        this.Project.Shape.Shapes.Add(Format.ReadShape(Reader));
                    }
                    catch (Exception ex)
                    {
                        this.LogRtf("Error parsing output file.");
                        ErrorDialog.Show("Error parsing output file.", ex, new StackTrace(true));
                        return;
                    }
                }

                this.tabControl1.SelectTab(0);

                this.LogRtf("Done.");
            }
            catch (Exception ex)
            {
                this.LogRtf("Error opening the output file.");
                ErrorDialog.Show("Error opening the output file.", ex, new StackTrace(true));
            }
            finally
            {
                this.tableLayoutPanel2.Enabled = true;
            }
        }
        #endregion

        private void ResetRtf()
        {
            //            this.DataRtf = new StringBuilder(
            //@"{\rtf1\ansi\deff0{\fonttbl{\f0 Courier New;}}
            //{\colortbl\red0\green0\blue0;\red70\green70\blue70;\red0\green70\blue70;}
            //{\pard ");
            //            this.richTextBox1.Rtf = this.DataRtf.ToString() + @"\par}}";
            this.richTextBox1.Text = "";
        }

        private void LogRtf(String Data, Int32 Color = 3)
        {
            this.PrintRtf("###" + Data + '\n', Color);
        }

        private void PrintRtf(String Data, Int32 Color = 3)
        {
            //var Bl = true;
            //foreach (var l in NewLineRegex.Split(Data))
            //{
            //    Console.Write(Color);
            //    Console.WriteLine(l);

            //    if (Bl)
            //    {
            //        Bl = false;
            //    }
            //    else
            //    {
            //        this.DataRtf.AppendLine(@"\par}").Append(@"{\pard ");
            //    }
            //    this.DataRtf.Append(@"{\cf").Append(Color).Append(" ").Append(l).Append(@"}");
            //}
            //this.richTextBox1.Rtf = this.DataRtf.ToString() + @"\par}}";
            this.richTextBox1.Text += NewLineRegex.Split(Data).Aggregate("", (P, C) => P + Environment.NewLine + C);
        }

        private void Process_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action(() => { this.Process_OutputDataReceived(sender, e); }));
                return;
            }

            this.PrintRtf(e.Data, 1);
        }

        private void Process_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action(() => { this.Process_ErrorDataReceived(sender, e); }));
                return;
            }

            this.PrintRtf(e.Data, 2);
        }

        // Open Editor
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            try
            {
                this.CheckProjectExitst(false);

                var EditorDirectory = IO.Path.Combine(Utils.GetParentPath(Application.ExecutablePath),
                                                      "CodeBlocks");
                var EditorPath = IO.Path.Combine(EditorDirectory, "codeblocks.exe");
                if (!IO.File.Exists(EditorPath))
                {
                    throw new Exception("Editor not found");
                }

                var Prc = new ProcessStartInfo(EditorPath);

                if (this.Project.ProjectFilePath != null)
                {
                    Prc.Arguments = '"' + IO.Path.GetFullPath(this.Project.ProjectFilePath) + '"';
                }

                Prc.UseShellExecute = false;
                Prc.WorkingDirectory = EditorDirectory;

                var SettingsDirectory = IO.Path.Combine(EditorDirectory, "settings");
                if (!IO.Directory.Exists(SettingsDirectory))
                    IO.Directory.CreateDirectory(SettingsDirectory);
                Prc.EnvironmentVariables["APPDATA"] = SettingsDirectory;

                Process.Start(Prc);
            }
            catch (Exception ex)
            {
                ErrorDialog.Show("Error opening the code editor.", ex, new StackTrace(true));
            }
        }

        private StringBuilder DataRtf;
        private String ExecutablePath, ExecutableDirectory;
        private static readonly Regex NewLineRegex = new Regex(@"\r\n?|\n", RegexOptions.Compiled | RegexOptions.Multiline);
        //private static readonly Regex CBPUnitRegex = new Regex(@"<Unit [^>]*?filename=""(.*?)""", RegexOptions.Compiled | RegexOptions.Multiline | RegexOptions.IgnoreCase);
        //private static readonly Regex CBPCompilerRegex = new Regex(@"<Compiler[^>]*>(.*?)</Compiler>", RegexOptions.Compiled | RegexOptions.Multiline | RegexOptions.IgnoreCase);
        //private static readonly Regex CBPAddRegex = new Regex(@"<Add [^>]*?option=""(.*?)""", RegexOptions.Compiled | RegexOptions.Multiline | RegexOptions.IgnoreCase);
        #endregion

        #region ProjectEditing Logic
        private void Project_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e == null)
            {
                this.ResetRtf();
                this.textBox3.Text = this.Project.InputFileName;
                comboBox1.SelectedIndex = Array.IndexOf(ShapeFileFormatKeys, this.Project.InputFileTypeKey);
                this.textBox4.Text = this.Project.OutputFileName;
                comboBox2.SelectedIndex = Array.IndexOf(ShapeFileFormatKeys, this.Project.OutputFileTypeKey);
                this.label6.Text = this.Project.ProjectFilePath;
                return;
            }

            switch (e.PropertyName)
            {
                case "InputFileName":
                    this.textBox3.Text = this.Project.InputFileName;
                    break;
                case "InputFileTypeKey":
                    comboBox1.SelectedIndex = Array.IndexOf(ShapeFileFormatKeys, this.Project.InputFileTypeKey);
                    break;
                case "OutputFileName":
                    this.textBox4.Text = this.Project.OutputFileName;
                    break;
                case "OutputFileTypeKey":
                    comboBox2.SelectedIndex = Array.IndexOf(ShapeFileFormatKeys, this.Project.OutputFileTypeKey);
                    break;
                case "ProjectFilePath":
                    this.label6.Text = this.Project.ProjectFilePath;
                    break;
                default:
                    Console.WriteLine("###" + e.PropertyName);
                    break;
            }
        }

        // Input File Name
        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            this.Project.InputFileName = this.textBox3.Text;
        }

        // Output File Name
        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            this.Project.OutputFileName = this.textBox4.Text;
        }

        // Input File Format
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Project.InputFileTypeKey = ShapeFileFormatKeys[comboBox1.SelectedIndex];
        }

        // Output File Format
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Project.OutputFileTypeKey = ShapeFileFormatKeys[comboBox2.SelectedIndex];
        }

        private void RefreshFilesList()
        {
            this.listBox1.Items.Clear();

            var T = this.GetProjectFileNames();
            if (T != null)
            {
                foreach (var F in T)
                {
                    this.listBox1.Items.Add(IO.Path.GetFileName(F));
                }
            }
        }

        private void MainForm_Enter(object sender, EventArgs e)
        {
            this.RefreshFilesList();
        }

        private void shapeTreeViewer2_SelectedShapeChanged(object sender, EventArgs e)
        {
            ShapeWalker.Instance.Walk(this.Project.Shape, S => { S.IsSelected = false; });
        }
        #endregion

        #region Project Logic
        private void SaveProject()
        {
            if (!this.CheckProjectExitst(false))
            {
                return;
            }
            try
            {
                var Path = IO.Path.ChangeExtension(this.Project.ProjectFilePath, "omproj");
                using (var File = IO.File.Open(Path, IO.FileMode.Create, IO.FileAccess.Write, IO.FileShare.Read))
                {
                    Formatter.Serialize(File, this.Project);
                }
            }
            catch (Exception ex)
            {
                ErrorDialog.Show("Could not read project file.", ex, new StackTrace(true));
            }
            finally
            {
            }
        }

        private void OpenProject(String Path)
        {
            if (!IO.File.Exists(Path))
            {
                ErrorDialog.Show("File does not exist.");
                return;
            }

            this.AddProjectRecent(Path);

            var ProjPath = IO.Path.ChangeExtension(Path, "omproj");
            var ProjOpened = false;
            if (IO.File.Exists(ProjPath))
            {
                try
                {
                    using (var File = IO.File.Open(ProjPath, IO.FileMode.Open, IO.FileAccess.Read, IO.FileShare.Read))
                    {
                        this.Project = (Project)Formatter.Deserialize(File);
                    }
                    this.Project.ProjectFilePath = Path;
                    ProjOpened = true;
                }
                catch (Exception ex)
                {
                    ErrorDialog.Show("Could not read project file.", ex, new StackTrace(true));
                }
            }
            if (!ProjOpened)
            {
                this.Project =
                    new Project()
                    {
                        InputFileTypeKey = ShapeFileFormatKeys[0],
                        OutputFileTypeKey = ShapeFileFormatKeys[1]
                    };
                this.Project.ProjectFilePath = Path;
                this.SaveProject();
            }

            this.RefreshFilesList();
            this.tableLayoutPanel2.Enabled = true;
        }

        private void CloseProject()
        {
            this.Project.ProjectFilePath = null;
            this.tableLayoutPanel2.Enabled = false;
        }

        // New Project
        private void createNewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.saveFileDialog2.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var Path = this.saveFileDialog2.FileName;
                try
                {
                    using (var Writer = new IO.StreamWriter(IO.File.Open(Path, IO.FileMode.Create, IO.FileAccess.Write, IO.FileShare.Read)))
                    {
                        Writer.WriteLine(@"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""yes"" ?>
<CodeBlocks_project_file>
	<FileVersion major=""1"" minor=""6"" />
	<Project>
		<Option title=""FTest"" />
		<Option pch_mode=""2"" />
		<Option compiler=""gfortran"" />
		<Build>
			<Target title=""Debug"">
				<Option output=""bin/Debug/FTest"" prefix_auto=""1"" extension_auto=""1"" />
				<Option object_output=""obj/Debug/"" />
				<Option type=""1"" />
				<Option compiler=""gfortran"" />
				<Compiler>
					<Add option=""-g"" />
				</Compiler>
			</Target>
		</Build>
		<Compiler>
			<Add option=""-Wall"" />
		</Compiler>
		<Extensions>
			<code_completion />
			<envvars />
			<debugger />
			<lib_finder disable_auto=""1"" />
		</Extensions>
	</Project>
</CodeBlocks_project_file>");
                    }
                }
                catch (Exception ex)
                {
                    ErrorDialog.Show("Could not create the project.", ex, new StackTrace(true));
                }

                this.OpenProject(Path);
            }
        }

        // Open Project
        private void openExistingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.openFileDialog2.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.OpenProject(this.openFileDialog2.FileName);
            }
        }

        // Close Project
        private void closeProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.CloseProject();
        }

        private IEnumerable<String> GetProjectFileNames()
        {
            if (!this.CheckProjectExitst(false))
            {
                return null;
            }
            XDocument ProjectXDocument = null;
            try
            {
                var ProjectFile = IO.File.ReadAllText(this.Project.ProjectFilePath);
                ProjectXDocument = XDocument.Parse(ProjectFile);
            }
            catch (Exception ex)
            {
                ErrorDialog.Show("Error reading the project file.", ex, new StackTrace(true));
            }
            var Project = ProjectXDocument.Element("CodeBlocks_project_file")
                                              .Element("Project");
            return this.GetProjectFileNames(Project);
        }

        private IEnumerable<String> GetProjectFileNames(XElement ProjectXElement)
        {
            return ProjectXElement.Elements("Unit").Select(U => U.Attribute("filename").Value);
        }

        private Boolean CheckProjectExitst(Boolean Throw = true)
        {
            if (this.Project.ProjectFilePath != null && IO.File.Exists(this.Project.ProjectFilePath))
            {
                return true;
            }

            this.CloseProject();
            if (Throw)
            {
                throw new Exception("Project not found.");
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region Recents Logic
        private void ReadRecents()
        {
            var Success = false;

            try
            {
                this.RecentProjects.Clear();
                this.RecentShapes.Clear();

                var Path = IO.Path.Combine(Utils.GetParentPath(Application.ExecutablePath), "Recents.txt");
                using (var Reader = new IO.StreamReader(IO.File.Open(Path, IO.FileMode.Open, IO.FileAccess.Read, IO.FileShare.Read)))
                {
                    if (Reader.ReadLine() != "Projects:")
                    {
                        return;
                    }

                    while (true)
                    {
                        var L = Reader.ReadLine();
                        if (L == null)
                        {
                            return;
                        }
                        if (L == "")
                            break;
                        this.RecentProjects.Add(L);
                    }

                    if (Reader.ReadLine() != "Shapes:")
                    {
                        return;
                    }

                    while (true)
                    {
                        var L = Reader.ReadLine();
                        if (L == null)
                        {
                            return;
                        }
                        if (L == "")
                            break;
                        var L2 = Reader.ReadLine();
                        if (L2 == null)
                        {
                            return;
                        }
                        var I = Array.IndexOf(ShapeFileFormatKeys, L2);
                        if (I == -1)
                        {
                            return;
                        }
                        this.RecentShapes.Add(new KeyValuePair<String, Int32>(L, I));
                    }

                    Success = true;
                }
            }
            catch
            {
            }
            finally
            {
                if (!Success)
                {
                    this.RecentProjects.Clear();
                    this.RecentShapes.Clear();
                    this.openRecentToolStripMenuItem.DropDownItems.Clear();
                    this.recentShapesToolStripMenuItem.DropDownItems.Clear();
                }
                else
                {
                    this.openRecentToolStripMenuItem.DropDownItems.Clear();
                    this.recentShapesToolStripMenuItem.DropDownItems.Clear();
                    foreach (var I in this.RecentProjects)
                    {
                        this.openRecentToolStripMenuItem.DropDownItems.Add(this.GetToolStripItemForProject(I));
                    }
                    foreach (var I in this.RecentShapes)
                    {
                        this.recentShapesToolStripMenuItem.DropDownItems.Add(this.GetToolStripItemForShape(I));
                    }
                }
                if (this.openRecentToolStripMenuItem.DropDownItems.Count == 0)
                    this.openRecentToolStripMenuItem.DropDownItems.Add(this.EmptyRecentProjects);
                if (this.recentShapesToolStripMenuItem.DropDownItems.Count == 0)
                    this.recentShapesToolStripMenuItem.DropDownItems.Add(this.EmptyRecentProjects);
            }
        }

        private void WriteRecents()
        {
            try
            {
                var Path = IO.Path.Combine(Utils.GetParentPath(Application.ExecutablePath), "Recents.txt");
                using (var Writer = new IO.StreamWriter(IO.File.Open(Path, IO.FileMode.Create, IO.FileAccess.Write, IO.FileShare.Read)))
                {
                    Writer.WriteLine("Projects:");
                    foreach (var i in this.RecentProjects)
                    {
                        Writer.WriteLine(i);
                    }

                    Writer.WriteLine();
                    Writer.WriteLine("Shapes:");
                    foreach (var i in this.RecentShapes)
                    {
                        Writer.WriteLine(i.Key);
                        Writer.WriteLine(ShapeFileFormatKeys[i.Value]);
                    }
                    Writer.WriteLine();
                }
            }
            catch
            {
            }
        }

        private void AddProjectRecent(String Item)
        {
            var I = this.RecentProjects.IndexOf(Item);

            var DDItems = this.openRecentToolStripMenuItem.DropDownItems;
            if (I == -1) // New
            {
                this.RecentProjects.Insert(0, Item);
                while (this.RecentProjects.Count > 10)
                    this.RecentProjects.RemoveAt(10);

                if (DDItems.Count == 1 && Object.ReferenceEquals(DDItems[0], this.EmptyRecentProjects))
                {
                    DDItems.Clear();
                }
                DDItems.Add(this.GetToolStripItemForProject(Item));
            }
            else
            {
                this.RecentProjects.RemoveAt(I);
                this.RecentProjects.Insert(0, Item);

                var T = DDItems[I];
                DDItems.RemoveAt(I);
                DDItems.Insert(0, T);
            }

            this.WriteRecents();
        }

        private void AddShapeRecent(String Item, ShapeFileFormat Format)
        {
            this.AddShapeRecent(Item, Array.IndexOf(ShapeFileFormatKeys, Format.Name));
        }

        private void AddShapeRecent(String Item, Int32 FormatIndex)
        {
            var P = new KeyValuePair<String, Int32>(Item, FormatIndex);
            var I = this.RecentShapes.IndexOf(P);

            var DDItems = this.recentShapesToolStripMenuItem.DropDownItems;
            if (I == -1) // New
            {
                this.RecentShapes.Insert(0, P);
                while (this.RecentShapes.Count > 10)
                    this.RecentShapes.RemoveAt(10);

                if (DDItems.Count == 1 && Object.ReferenceEquals(DDItems[0], this.EmptyRecentShapes))
                {
                    DDItems.Clear();
                }
                DDItems.Add(this.GetToolStripItemForShape(P));
            }
            else
            {
                this.RecentShapes.RemoveAt(I);
                this.RecentShapes.Insert(0, P);

                var T = DDItems[I];
                DDItems.RemoveAt(I);
                DDItems.Insert(0, T);
            }

            this.WriteRecents();
        }

        private ToolStripItem GetToolStripItemForProject(String S)
        {
            var R = new ToolStripMenuItem(IO.Path.GetFileName(S));
            R.ToolTipText = S;
            R.Tag = S;
            R.Click += this.ProjectRecent_Click;
            return R;
        }

        private ToolStripItem GetToolStripItemForShape(KeyValuePair<String, Int32> P)
        {
            var K = ShapeFileFormatKeys[P.Value];

            var R = new ToolStripMenuItem('[' + K + "] " + IO.Path.GetFileName(P.Key));
            R.ToolTipText = '[' + K + "] " + P.Key;
            R.Tag = P;
            R.Click += this.ShapeRecent_Click;

            return R;
        }

        private void ProjectRecent_Click(object sender, EventArgs e)
        {
            var P = (String)((ToolStripItem)sender).Tag;
            this.OpenProject(P);
        }

        private void ShapeRecent_Click(object sender, EventArgs e)
        {
            var P = (KeyValuePair<String, Int32>)((ToolStripItem)sender).Tag;
            this.LoadShape(P.Key,
                           ShapeFileFormats[ShapeFileFormatKeys[P.Value]]);
        }
        #endregion

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShapeWalker.Instance.Walk(this.Project.Shape, S => { S.IsSelected = false; });
            toolStripButton1.Visible = this.tabControl1.SelectedIndex == 0;
            if (this.tabControl1.SelectedIndex == 0)
                this.SaveProject();
        }

        #region Project Property
        private Project _Project = new Project();

        public Project Project
        {
            get
            {
                return this._Project;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException();


                var ShapeStateChangedEventHandler = new EventHandler<ShapeHierarchyList.ShapeStateChangedEventArgs>(
                    (Sender, E) =>
                    {
                        this.shapeTreeViewer2.ShapeCheckBoxStateChanged(E.Shape);
                    });
                var ShapesChangedEventHandler = new EventHandler(
                    (Sender, E) =>
                    {
                        this.shapeDrawingPane2.ShapeDrawer.Refresh();
                        this.shapeDrawingPane2.ShapeDrawer.FillView();
                        //this.listBox1.Items.Clear();
                        //foreach (var i in SHL)
                        //{
                        //    this.listBox1.Items.Add(i.Name);
                        //}
                    });


                this._Project.PropertyChanged -= this.Project_PropertyChanged;
                value.ShapesForInput.ShapeStateChanged -= ShapeStateChangedEventHandler;
                value.ShapesForInput.Changed -= ShapesChangedEventHandler;

                this._Project = value;


                value.PropertyChanged += this.Project_PropertyChanged;

                this.shapeTreeViewer1.Shape = value.Shape;
                this.ShapeDrawer.Shape = value.Shape;

                value.ShapesForInput.ShapeStateChanged += ShapeStateChangedEventHandler;
                value.ShapesForInput.Changed += ShapesChangedEventHandler;
                this.shapeTreeViewer2.CheckBoxGetter = (S) => this._Project.ShapesForInput[S];
                this.shapeTreeViewer2.CheckBoxSetter = (S, V) => { this._Project.ShapesForInput[S] = V; };

                this.shapeDrawingPane2.ShapeDrawer.Shape = new ShapeBag(value.ShapesForInput);
                this.shapeTreeViewer2.Shape = value.Shape;

                this.propertyEditor1.EditingObject = null;

                this.Project_PropertyChanged(null, null);
            }
        }
        #endregion

        //private StringInputDialog stringInputDialog1 = new StringInputDialog();
        private readonly ShapeDrawer ShapeDrawer;
        private readonly List<String> RecentProjects = new List<String>();
        private readonly List<KeyValuePair<String, Int32>> RecentShapes = new List<KeyValuePair<String, Int32>>();
        private readonly ToolStripItem EmptyRecentProjects, EmptyRecentShapes;

        private static Dictionary<String, ShapeFileFormat> ShapeFileFormats;
        private static String[] ShapeFileFormatKeys;
        private static readonly IFormatter Formatter = new System.Runtime.Serialization.Formatters.Soap.SoapFormatter();

    }

}
