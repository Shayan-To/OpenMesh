using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace OpenMesh
{

    [Serializable()]
    public class Project : NotifyingObject, ISerializable
    {

        public Project()
        {
            this._InputFileName = "input.txt";
            this._OutputFileName = "output.txt";
            this._ProjectFilePath = null;
            this._Shape = new ShapeCollection() { Name = "Shapes" };
            this._ShapesForInput = new ShapeHierarchyList(this._Shape);
        }

        #region Serialization Logic
        protected Project(SerializationInfo Info, StreamingContext Context)
        {
            Utils.Deserializing();
            this._InputFileName = Info.GetString("InputFileName");
            this._InputFileTypeKey = Info.GetString("InputFileTypeKey");
            this._OutputFileName = Info.GetString("OutputFileName");
            this._OutputFileTypeKey = Info.GetString("OutputFileTypeKey");
            //this._ProjectFilePath = Info.GetString("ProjectFilePath");
            this._Shape = (ShapeCollection)Info.GetValue("Shape", typeof(ShapeCollection));
            this._ShapesForInput = (ShapeHierarchyList)Info.GetValue("ShapesToOutput", typeof(ShapeHierarchyList));
        }

        protected virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            Utils.Serializing();
            info.AddValue("InputFileName", this._InputFileName);
            info.AddValue("InputFileTypeKey", this._InputFileTypeKey);
            info.AddValue("OutputFileName", this._OutputFileName);
            info.AddValue("OutputFileTypeKey", this._OutputFileTypeKey);
            //info.AddValue("ProjectFilePath", this._ProjectFilePath);
            info.AddValue("Shape", this._Shape, typeof(ShapeCollection));
            info.AddValue("ShapesToOutput", this._ShapesForInput, typeof(ShapeHierarchyList));
        }

        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            this.GetObjectData(info, context);
        }
        #endregion

        #region ProjectFilePath Property
        private String _ProjectFilePath;

        public String ProjectFilePath
        {
            get
            {
                return this._ProjectFilePath;
            }
            set
            {
                this._ProjectFilePath = value;
                this.NotifyPropertyChanged("ProjectFilePath");
            }
        }
        #endregion

        #region InputFileName Property
        private String _InputFileName;

        public String InputFileName
        {
            get
            {
                return this._InputFileName;
            }
            set
            {
                this._InputFileName = value;
                this.NotifyPropertyChanged("InputFileName");
            }
        }
        #endregion

        #region InputFileTypeKey Property
        private String _InputFileTypeKey;

        public String InputFileTypeKey
        {
            get
            {
                return this._InputFileTypeKey;
            }
            set
            {
                this._InputFileTypeKey = value;
                this.NotifyPropertyChanged("InputFileTypeKey");
            }
        }
        #endregion

        #region OutputFileName Property
        private String _OutputFileName;

        public String OutputFileName
        {
            get
            {
                return this._OutputFileName;
            }
            set
            {
                this._OutputFileName = value;
                this.NotifyPropertyChanged("OutputFileName");
            }
        }
        #endregion

        #region OutputFileTypeKey Property
        private String _OutputFileTypeKey;

        public String OutputFileTypeKey
        {
            get
            {
                return this._OutputFileTypeKey;
            }
            set
            {
                this._OutputFileTypeKey = value;
                this.NotifyPropertyChanged("OutputFileTypeKey");
            }
        }
        #endregion

        #region ShapesForInput Property
        private ShapeHierarchyList _ShapesForInput;

        public ShapeHierarchyList ShapesForInput
        {
            get
            {
                return this._ShapesForInput;
            }
            //set
            //{
            //    this._ShapesToOutput = value;
            //    this.NotifyPropertyChanged("ShapesToOutput");
            //}
        }
        #endregion

        #region Shape Property
        private readonly ShapeCollection _Shape;

        public ShapeCollection Shape
        {
            get
            {
                return this._Shape;
            }
        }
        #endregion

    }

}
