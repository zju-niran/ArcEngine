using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Runtime.InteropServices;

using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.SpatialAnalyst;
using ESRI.ArcGIS.GeoAnalyst;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.Geoprocessor;
using ESRI.ArcGIS.DataManagementTools;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.Analyst3D;

namespace MapControlApplication1
{
    public sealed partial class MainForm : Form
    {
        #region class private members
        private IMapControl3 m_mapControl = null;
        private string m_mapDocumentName = string.Empty;
        private IWorkspace workspace = null;  //工作空间即GeoDataBase
        private ILayer TOCRightLayer;     //用于存储TOC右键选中图层
        private Color m_FromColor = Color.Red;  //初始化色块颜色（左）
        private Color m_ToColor = Color.Blue; //初始化色块颜色（右）
        private bool fClip = false;  //记录是否处于裁剪状态
        private bool fLineofsight = false; //记录是否处于通视分析状态
        private bool fVisibility = false; //记录是否处于视域分析状态
        private bool fExtraction = false;  //记录是否处于Extraction裁剪状态
        #endregion

        #region class constructor
        public MainForm()
        {
            InitializeComponent();
            //色带初始化
            m_FromColor = Color.Red;  //初始化色块颜色（左）
            m_ToColor = Color.Blue; //初始化色块颜色（右）
            RefreshColors(m_FromColor, m_ToColor);
        }
        #endregion

        private void MainForm_Load(object sender, EventArgs e)
        {
            //get the MapControl
            m_mapControl = (IMapControl3)axMapControl1.Object;

            //disable the Save menu (since there is no document yet)
            menuSaveDoc.Enabled = false;
        }

        #region Main Menu event handlers
        private void menuNewDoc_Click(object sender, EventArgs e)
        {
            //execute New Document command
            ICommand command = new CreateNewDocument();
            command.OnCreate(m_mapControl.Object);
            command.OnClick();
        }

        private void menuOpenDoc_Click(object sender, EventArgs e)
        {
            //execute Open Document command
            ICommand command = new ControlsOpenDocCommandClass();
            command.OnCreate(m_mapControl.Object);
            command.OnClick();
        }

        private void menuSaveDoc_Click(object sender, EventArgs e)
        {
            //execute Save Document command
            if (m_mapControl.CheckMxFile(m_mapDocumentName))
            {
                //create a new instance of a MapDocument
                IMapDocument mapDoc = new MapDocumentClass();
                mapDoc.Open(m_mapDocumentName, string.Empty);

                //Make sure that the MapDocument is not readonly
                if (mapDoc.get_IsReadOnly(m_mapDocumentName))
                {
                    MessageBox.Show("Map document is read only!");
                    mapDoc.Close();
                    return;
                }

                //Replace its contents with the current map
                mapDoc.ReplaceContents((IMxdContents)m_mapControl.Map);

                //save the MapDocument in order to persist it
                mapDoc.Save(mapDoc.UsesRelativePaths, false);

                //close the MapDocument
                mapDoc.Close();
            }
        }

        private void menuSaveAs_Click(object sender, EventArgs e)
        {
            //execute SaveAs Document command
            ICommand command = new ControlsSaveAsDocCommandClass();
            command.OnCreate(m_mapControl.Object);
            command.OnClick();
        }

        private void menuExitApp_Click(object sender, EventArgs e)
        {
            //exit the application
            Application.Exit();
        }
        #endregion

        //listen to MapReplaced evant in order to update the statusbar and the Save menu
        private void axMapControl1_OnMapReplaced(object sender, IMapControlEvents2_OnMapReplacedEvent e)
        {
            //get the current document name from the MapControl
            m_mapDocumentName = m_mapControl.DocumentFilename;

            //if there is no MapDocument, diable the Save menu and clear the statusbar
            if (m_mapDocumentName == string.Empty)
            {
                menuSaveDoc.Enabled = false;
                statusBarXY.Text = string.Empty;
            }
            else
            {
                //enable the Save manu and write the doc name to the statusbar
                menuSaveDoc.Enabled = true;
                statusBarXY.Text = System.IO.Path.GetFileName(m_mapDocumentName);
            }
        }

        private void axMapControl1_OnMouseMove(object sender, IMapControlEvents2_OnMouseMoveEvent e)
        {
            statusBarXY.Text = string.Format("{0}, {1}  {2}", e.mapX.ToString("#######.##"), e.mapY.ToString("#######.##"), axMapControl1.MapUnits.ToString().Substring(4));
        }
        //连接sde数据库和初始化加载下拉框内容
        private void MI2_loadFromSDE_Click(object sender, EventArgs e)
        {
            //SDE连接数据库参数设置
            IPropertySet propertySet = new PropertySetClass();
            //propertySet.SetProperties("DBCLIENT", "oracle");
            //propertySet.SetProperties("DB_CONNECTION_PROPERTIES", "DESKTOP-5BE7H34");
            propertySet.SetProperty("SERVER", "DESKTOP-5BE7H34");
            propertySet.SetProperty("INSTANCE", "sde:oracle11g:DESKTOP-5BE7H34/orcl");
            propertySet.SetProperty("DATABASE", "sde");
            propertySet.SetProperty("USER", "sde");
            propertySet.SetProperty("PASSWORD", "Admin123");
            propertySet.SetProperty("VERSION", "sde.DEFAULT");
            propertySet.SetProperty("AUTHENTICATION_MORE", "DBMS");

            //指定SDE工作空间factory
            Type factoryType = Type.GetTypeFromProgID("esriDataSourcesGDB.SdeWorkspaceFactory");
            IWorkspaceFactory workspaceFactory = (IWorkspaceFactory)Activator.CreateInstance(factoryType);
            //根据SDE连接参数设置打开SDE工作空间
            //set the private property -- workspace
            workspace = workspaceFactory.Open(propertySet, 0);

            //string connectionString = "DB_CONNECTION_PROPERTIES=DESKTOP-5BE7H34;DATABASE=sde;PASSWORD=Admin123;VERSION=sde.DEFAULT";
            //Type factoryType = Type.GetTypeFromProgID("esriDataSourcesGBD.SdeWorkspaceFactory");
            //IWorkspaceFactory2 workspaceFactory2 = (IWorkspaceFactory2)Activator.CreateInstance(factoryType);
            //IWorkspace sdeWorkspace = workspaceFactory2.OpenFromString(connectionString, 0);

            //清除栅格目录下拉框里面的选项
            cmb_LoadRstCatalog.Items.Clear();
            cmb_LoadRstCatalog.Items.Add("非栅格目录（工作空间）");
            cmb_MosaicCatalog.Items.Clear();
            //获取数据库中的栅格目录，取出sde前缀
            IEnumDatasetName enumDatasetName = workspace.get_DatasetNames(esriDatasetType.esriDTRasterCatalog);
            IDatasetName datasetName = enumDatasetName.Next();
            while (datasetName != null)
            {
                cmb_LoadRstCatalog.Items.Add(datasetName.Name.Substring(datasetName.Name.LastIndexOf('.') + 1));
                cmb_MosaicCatalog.Items.Add(datasetName.Name.Substring(datasetName.Name.LastIndexOf('.') + 1));
                datasetName = enumDatasetName.Next();
            }
            //设置下拉框默认选项为非栅格目录（工作空间）
            if (cmb_LoadRstCatalog.Items.Count > 0)
                cmb_LoadRstCatalog.SelectedIndex = 0;
            if (cmb_MosaicCatalog.Items.Count > 0)
                cmb_MosaicCatalog.SelectedIndex = 0;

        }
        //选择栅格目录发生变化，相应的栅格图像列表也发生变化
        private void cmb_LoadRstCatalog_SelectedIndexChanged(object sender, EventArgs e)
        {
            string rstCatalogName = cmb_LoadRstCatalog.SelectedItem.ToString();
            IEnumDatasetName enumDatasetName;
            IDatasetName datasetName;
            if (cmb_LoadRstCatalog.SelectedIndex == 0)
            {
                //清除栅格图像下拉框里面的选项
                cmb_LoadRstDataset.Items.Clear();
                cmb_LoadRstDataset.Text = "";
                //获取非栅格目录（工作空间）中的栅格图像
                enumDatasetName = workspace.get_DatasetNames(esriDatasetType.esriDTRasterDataset);
                datasetName = enumDatasetName.Next();
                while (datasetName != null)
                {
                    cmb_LoadRstDataset.Items.Add(datasetName.Name.Substring(datasetName.Name.LastIndexOf('.') + 1));
                    datasetName = enumDatasetName.Next();

                }
                //设置下拉框默认选项为非栅格目录（工作空间）
                if (cmb_LoadRstDataset.Items.Count > 0)
                    cmb_LoadRstDataset.SelectedIndex = 0;
            }
            else
            {
                //接口转换IRsterWorkspaceEx
                IRasterWorkspaceEx workspaceEx = (IRasterWorkspaceEx)workspace;
                //获取栅格目录
                IRasterCatalog rasterCatalog = workspaceEx.OpenRasterCatalog(rstCatalogName);
                //接口转换IFeatureClass
                IFeatureClass featureClass = (IFeatureClass)rasterCatalog;
                //接口转换ITable
                ITable pTable = featureClass as ITable;
                //执行查询获取指针
                ICursor cursor = pTable.Search(null, true) as ICursor;
                IRow pRow = null;
                //清除下拉框的选项
                cmb_LoadRstDataset.Items.Clear();
                cmb_LoadRstDataset.Text = "";
                //循环遍历读取每一行记录
                while ((pRow = cursor.NextRow()) != null)
                {
                    int idxName = pRow.Fields.FindField("NAME");
                    cmb_LoadRstDataset.Items.Add(pRow.get_Value(idxName).ToString());
                }
                //设置默认选项
                if (cmb_LoadRstDataset.Items.Count > 0)
                    cmb_LoadRstDataset.SelectedIndex = 0;
                //释放内存空间
                System.Runtime.InteropServices.Marshal.ReleaseComObject(cursor);
            }
        }

        private void btn_NewRstCatalog_Click(object sender, EventArgs e)
        {
            if (txb_NewRstCatalog.Text.Trim() == "")
            {
                MessageBox.Show("请输入栅格目录名称！");
            }
            else
            {
                string rasCatalogName = txb_NewRstCatalog.Text.Trim();
                IRasterWorkspaceEx rasterWorkspaceEx = workspace as IRasterWorkspaceEx;
                //定义空间参考，采用WGS84投影
                ISpatialReferenceFactory spatialReferenceFactory = new SpatialReferenceEnvironmentClass();
                ISpatialReference spatialReference = spatialReferenceFactory.CreateGeographicCoordinateSystem((int)esriSRGeoCSType.esriSRGeoCS_WGS1984);
                spatialReference.SetDomain(-180, 180, -90, 90);
                //判断栅格目录是否存在
                IEnumDatasetName enumDatasetName = workspace.get_DatasetNames(esriDatasetType.esriDTRasterCatalog);
                IDatasetName datasetName = enumDatasetName.Next();
                bool isExit = false;
                //循环遍历判断是否存在该栅格目录
                while (datasetName != null)
                {
                    if (datasetName.Name.Substring(datasetName.Name.LastIndexOf('.') + 1) == rasCatalogName)
                    {
                        isExit = true;
                        MessageBox.Show("栅格目录已经存在！");
                        txb_NewRstCatalog.Focus();
                        return;
                    }
                    datasetName = enumDatasetName.Next();
                }

                //若不存在，则创建新的栅格目录
                if (isExit == false)
                {
                    //创建栅格目录字段集
                    IFields fields = CreateFields("RASTER", "SHAPE", spatialReference, spatialReference);
                    rasterWorkspaceEx.CreateRasterCatalog(rasCatalogName, fields, "SHAPE", "RASTER", "DEFAULTS");

                    //将新创建的栅格目录添加到下拉列表中，并设置为当前栅格目录
                    cmb_LoadRstCatalog.Items.Add(rasCatalogName);
                    cmb_LoadRstCatalog.SelectedIndex = cmb_LoadRstCatalog.Items.Count - 1;
                    cmb_MosaicCatalog.Items.Add(rasCatalogName);
                    cmb_LoadRstCatalog.SelectedIndex = cmb_MosaicCatalog.Items.Count - 1;
                    cmb_LoadRstDataset.Items.Clear();
                    cmb_LoadRstDataset.Text = "";
                }
                MessageBox.Show("栅格目录创建成功！");
            }
        }
        //删除选中的栅格目录
        private void btn_DeleteRstCatalog_Click(object sender, EventArgs e)
        {
            if (cmb_LoadRstCatalog.Text.Trim() == "")
            {
                MessageBox.Show("请先选择栅格目录！");
            }
            else
            {
                //在工作空间下删除指定的栅格目录
                IRasterWorkspaceEx rasterWorkspaceEx = workspace as IRasterWorkspaceEx;
                rasterWorkspaceEx.DeleteRasterCatalog(cmb_LoadRstCatalog.Text);
                //栅格目录下删除该记录
                cmb_LoadRstCatalog.Items.RemoveAt(cmb_LoadRstCatalog.SelectedIndex);
                //栅格图像目录切换到非栅格目录（工作空间）
                cmb_LoadRstCatalog.SelectedIndex = 0;
                //清空图像目录原先的内容
                cmb_LoadRstDataset.Items.Clear();
                cmb_LoadRstDataset.Text = "";
                //获取非栅格目录（工作空间）中的栅格图像
                IEnumDatasetName enumDatasetName = workspace.get_DatasetNames(esriDatasetType.esriDTRasterDataset);
                IDatasetName datasetName = enumDatasetName.Next();
                while (datasetName != null)
                {
                    cmb_LoadRstDataset.Items.Add(datasetName.Name.Substring(datasetName.Name.LastIndexOf('.') + 1));
                    datasetName = enumDatasetName.Next();

                }
                //设置下拉框默认选项为非栅格目录（工作空间）
                if (cmb_LoadRstDataset.Items.Count > 0)
                    cmb_LoadRstDataset.SelectedIndex = 0;
                MessageBox.Show("栅格目录删除成功！");
            }
        }
        //删除选中的栅格图像

        //<summary>
        //创建栅格目录所需的字段集合
        //</summary>
        //<param name="rasterFldName">Raster字段名称</param>
        //<param name="shapeFldName">Shape字段名称</param>
        //<param name="rasterSF">Raster字段空间参考</param>
        //<param name="shapeSF">Shape字段空间参考</param>
        //<returns></returns>
        private IFields CreateFields(string rasterFldName, string shapeFldName, ISpatialReference rasterSF, ISpatialReference shapeSF)
        {
            IFields fields = new FieldsClass();
            IFieldsEdit fieldsEdit = fields as IFieldsEdit;

            IField field;
            IFieldEdit fieldEdit;

            //添加oid字段，注意字段type
            field = new FieldClass();
            fieldEdit = field as IFieldEdit;
            fieldEdit.Name_2 = "ObjectID";
            fieldEdit.Type_2 = esriFieldType.esriFieldTypeOID;
            fieldsEdit.AddField(field);

            //添加name字段，注意字段type
            field = new FieldClass();
            fieldEdit = field as IFieldEdit;
            fieldEdit.Name_2 = "NAME";
            fieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
            fieldsEdit.AddField(field);

            //添加raster字段，注意字段type
            field = new FieldClass();
            fieldEdit = field as IFieldEdit;
            fieldEdit.Name_2 = rasterFldName;
            fieldEdit.Type_2 = esriFieldType.esriFieldTypeRaster;

            //IRasterDef接口定义栅格字段
            IRasterDef rasterDef = new RasterDefClass();
            rasterDef.SpatialReference = rasterSF;
            ((IFieldEdit2)fieldEdit).RasterDef = rasterDef;
            fieldsEdit.AddField(field);

            //添加shape字段，注意字段type
            field = new FieldClass();
            fieldEdit = field as IFieldEdit;
            fieldEdit.Name_2 = shapeFldName;
            fieldEdit.Type_2 = esriFieldType.esriFieldTypeGeometry;

            //IGrometryDef和IGeometryDefEdit接口定义和编辑几何字段
            IGeometryDef geometryDef = new GeometryDefClass();
            IGeometryDefEdit geometryDefEdit = geometryDef as IGeometryDefEdit;
            geometryDefEdit.GeometryType_2 = esriGeometryType.esriGeometryPolygon;
            geometryDefEdit.SpatialReference_2 = shapeSF;
            ((IFieldEdit2)fieldEdit).GeometryDef_2 = geometryDef;
            fieldsEdit.AddField(field);

            //添加xml(元数据)字段，注意字段type
            field = new FieldClass();
            fieldEdit = field as IFieldEdit;
            fieldEdit.Name_2 = "METADATA";
            fieldEdit.Type_2 = esriFieldType.esriFieldTypeBlob;
            fieldsEdit.AddField(field);

            return fields;
        }
        //根据选定的栅格目录和栅格图层来加载相应的图像
        private void btn_LoadRstDataset_Click(object sender, EventArgs e)
        {
            if (cmb_LoadRstDataset.Items.Count == 0)
            {
                MessageBox.Show("栅格图像不存在", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (cmb_LoadRstCatalog.SelectedIndex == 0)
            {
                string rstDatasetName = cmb_LoadRstDataset.SelectedItem.ToString();
                //接口转换IRasterWorkspaceEx
                IRasterWorkspaceEx workspaceEx = (IRasterWorkspaceEx)workspace;
                //获取栅格数据集
                IRasterDataset rasterDataset = workspaceEx.OpenRasterDataset(rstDatasetName);
                //利用栅格目录项创建栅格图层
                IRasterLayer rasterLayer = new RasterLayerClass();
                rasterLayer.CreateFromDataset(rasterDataset);
                ILayer layer = rasterLayer as ILayer;
                layer.Name = cmb_LoadRstDataset.Text;
                //将图层加载到MapControl中，并缩放到当前图层
                axMapControl1.AddLayer(layer);
                axMapControl1.ActiveView.Extent = layer.AreaOfInterest;
                axMapControl1.ActiveView.Refresh();
                axTOCControl1.Update();
            }
            else
            {
                string rstCatalogName = cmb_LoadRstCatalog.SelectedItem.ToString();
                string rstDatasetName = cmb_LoadRstDataset.SelectedItem.ToString();
                //接口转换IRasterWorkspaceEx
                IRasterWorkspaceEx workspaceEx = (IRasterWorkspaceEx)workspace;
                //获取栅格目录
                IRasterCatalog rasterCatalog = workspaceEx.OpenRasterCatalog(rstCatalogName);
                //接口转换IFeatureClass
                IFeatureClass featureClass = (IFeatureClass)rasterCatalog;
                //接口转换ITable
                ITable pTable = featureClass as ITable;
                //查询条件过滤器QueryFilterClass
                IQueryFilter qf = new QueryFilterClass();
                qf.SubFields = "OBJECTID";
                qf.WhereClause = "NAME='" + rstDatasetName + "'";
                //执行查询获取指针
                ICursor cursor = pTable.Search(qf, true) as ICursor;
                IRow pRow = null;
                int rstOID = 0;
                //判断读取第一行记录
                if ((pRow = cursor.NextRow()) != null)
                {
                    int idxfld = pRow.Fields.FindField("OBJECTID");
                    rstOID = int.Parse(pRow.get_Value(idxfld).ToString());
                    //获取检索到的栅格目录项
                    IRasterCatalogItem rasterCatalogItem = (IRasterCatalogItem)featureClass.GetFeature(rstOID);
                    //利用栅格目录项创建栅格图层
                    IRasterLayer rasterLayer = new RasterLayerClass();
                    rasterLayer.CreateFromDataset(rasterCatalogItem.RasterDataset);
                    ILayer layer = rasterLayer as ILayer;
                    layer.Name = cmb_LoadRstDataset.Text;
                    //将图层加载到MapControl中，并缩放到当前图层
                    axMapControl1.AddLayer(layer);
                    axMapControl1.ActiveView.Extent = layer.AreaOfInterest;
                    axMapControl1.ActiveView.Refresh();
                    axTOCControl1.Update();
                }
                //释放内存空间
                System.Runtime.InteropServices.Marshal.ReleaseComObject(cursor);
            }
            //更新波段信息统计的图层和波段下拉框选项内容
            iniCmbItems();
        }
        //点击新栅格图像名的textbox，选择要导入的栅格图像
        private void txb_NewRstDataset_MouseDown(object sender, MouseEventArgs e)
        {
            //打开文件选择对话框，设置对话框属性
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Imag file（*.img）|*.img|Tiff file（*.tif）|*.tif|DEM file（*.flt）|*.flt";
            openFileDialog.Title = "打开影像数据";
            openFileDialog.Multiselect = false;
            string fileName = "";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                fileName = openFileDialog.FileName;
                txb_NewRstDataset.Text = fileName;
            }
        }
        //导入栅格图像
        private void btn_ImportRstDataset_Click(object sender, EventArgs e)
        {
            //获取栅格图像的路径和文件名字
            string fileName = txb_NewRstDataset.Text;
            if (fileName == "")
            {
                MessageBox.Show("请先选择本地栅格图像！");
                return;
            }
            FileInfo fileInfo = new FileInfo(fileName);
            string filePath = fileInfo.DirectoryName;
            String file = fileInfo.Name;
            string strOutName = file.Substring(0, file.LastIndexOf("."));
            //根据路径和文件名字获取栅格数据集
            if (cmb_LoadRstCatalog.SelectedIndex == 0)
            {
                //判断是否有重名现象
                IWorkspace2 workspace2 = workspace as IWorkspace2;
                //如果名称已经存在
                if (workspace2.get_NameExists(esriDatasetType.esriDTRasterDataset, strOutName))
                {
                    DialogResult result;
                    result = MessageBox.Show(this, "名为 " + strOutName + " 的栅格文件在数据库中已存在！" + "\n是否覆盖？",
                        "相同文件名", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    //如果选择确认删除，则覆盖原栅格数据
                    if (result == DialogResult.Yes)
                    {
                        IRasterWorkspaceEx rstWorkspaceEx = workspace as IRasterWorkspaceEx;
                        IDataset datasetDel = rstWorkspaceEx.OpenRasterDataset(strOutName) as IDataset;
                        //调用IDataset接口的Delete接口实现已存在栅格数据集的删除
                        datasetDel.Delete();
                        datasetDel = null;
                    }
                    else
                    {
                        MessageBox.Show("工作空间已存在同名栅格数据集，不覆盖不能导入！");
                        return;
                    }
                }
                //根据选择的栅格图像的路径打开栅格工作空间
                IWorkspaceFactory rstWorkspaceFactoryImport = new RasterWorkspaceFactoryClass();
                IRasterWorkspace rstWorkspaceImport = (IRasterWorkspace)rstWorkspaceFactoryImport.OpenFromFile(filePath, 0);
                IRasterDataset rstDatasetImport = null;
                //检测选择文件的路径是不是有效的栅格工作空间
                if (!(rstWorkspaceImport is IRasterWorkspace))
                {
                    MessageBox.Show("文件路径不是有效的栅格工作空间！");
                    return;
                }
                //根据选择的栅格图像的名字获取栅格数据集
                rstDatasetImport = rstWorkspaceImport.OpenRasterDataset(file);
                //用IRasterDataset接口的CreatureDefaultRaster的方法创建空白的栅格对象
                IRaster raster = rstDatasetImport.CreateDefaultRaster();
                //IRasterProps是和栅格属性定义有关的接口
                IRasterProps rasterProp = raster as IRasterProps;
                //IrasterStroageDef接口和栅格储存参数有关
                IRasterStorageDef storageDef = new RasterStorageDefClass();
                //指定压缩类型
                storageDef.CompressionType = esriRasterCompressionType.esriRasterCompressionLZ77;
                //设置CellSize
                IPnt pnt = new PntClass();
                pnt.SetCoords(rasterProp.MeanCellSize().X, rasterProp.MeanCellSize().Y);
                storageDef.CellSize = pnt;
                //设置栅格数据集的圆点，在最左上角一点位置
                IPoint origin = new PointClass();
                origin.PutCoords(rasterProp.Extent.XMin, rasterProp.Extent.YMax);
                storageDef.Origin = origin;
                //接口转化为和栅格存储有关的ISaveAs2
                ISaveAs2 saveAs2 = (ISaveAs2)rstDatasetImport;
                //接口转化为和栅格存储属性定义有关的IRasterStorageDef2
                IRasterStorageDef2 rasterStorageDef2 = (IRasterStorageDef2)storageDef;
                //指定压缩质量，瓦片高度和宽度
                rasterStorageDef2.CompressionQuality = 100;
                rasterStorageDef2.Tiled = true;
                rasterStorageDef2.TileHeight = 128;
                rasterStorageDef2.TileWidth = 128;
                //最后调用ISaveAs2接口的SaveAsRasterDataset方法实现栅格数据集的存储
                //指定存储名字，工作空间，存储格式和相关存储属性
                saveAs2.SaveAsRasterDataset(strOutName, workspace, "GRID", rasterStorageDef2);
            }
            else
            {
                string rasterCatalogName = cmb_LoadRstCatalog.Text;
                //打开栅格工作空间
                IWorkspaceFactory pRasterWsFac = new RasterWorkspaceFactoryClass();
                IWorkspace pWs = pRasterWsFac.OpenFromFile(filePath, 0);
                if (!(pWs is IRasterWorkspace))
                {
                    MessageBox.Show("文件路径不是有效的栅格工作空间！");
                    return;
                }
                IRasterWorkspace pRasterWs = pWs as IRasterWorkspace;
                //获取栅格数据集
                IRasterDataset pRasterDs = pRasterWs.OpenRasterDataset(file);
                //创建栅格对象
                IRaster raster = pRasterDs.CreateDefaultRaster();
                IRasterProps rasterProp = raster as IRasterProps;
                //设置栅格储存参数
                IRasterStorageDef storageDef = new RasterStorageDefClass();
                storageDef.CompressionType = esriRasterCompressionType.esriRasterCompressionLZ77;
                //设置CellSize
                IPnt pnt = new PntClass();
                pnt.SetCoords(rasterProp.MeanCellSize().X, rasterProp.MeanCellSize().Y);
                storageDef.CellSize = pnt;
                //设置栅格数据集的圆点，在最左下角一点放置
                IPoint origin = new PointClass();
                origin.PutCoords(rasterProp.Extent.XMin, rasterProp.Extent.YMax);
                storageDef.Origin = origin;

                //在RasterCatalog中添加栅格
                //打开对应的RasterCatalog
                IRasterCatalog pRasterCatalog = ((IRasterWorkspaceEx)workspace).OpenRasterCatalog(rasterCatalogName);
                //将需要导入的RasterCatalog转换成FeatureClass
                IFeatureClass pFeatureClass = (IFeatureClass)pRasterCatalog;
                //名字所在列的索引号
                int nameIndex = pRasterCatalog.NameFieldIndex;
                //栅格数据所在列的索引号
                int rasterIndex = pRasterCatalog.RasterFieldIndex;
                IFeatureBuffer pBuffer = null;
                IFeatureCursor pFeatureCursor = pFeatureClass.Insert(false);
                //创建IRasterValue接口的对象——RasterBuffer对象的rasterindex需要使用
                IRasterValue pRasterValue = new RasterValueClass();
                //设置IRasterValue的RasterDataset
                pRasterValue.RasterDataset = (IRasterDataset)pRasterDs;
                //存储参数设定
                pRasterValue.RasterStorageDef = storageDef;
                pBuffer = pFeatureClass.CreateFeatureBuffer();
                //设置RasterBuffer对象的raterindex和nameindex
                pBuffer.set_Value(rasterIndex, pRasterValue);
                pBuffer.set_Value(nameIndex, strOutName);
                //通过cursor实现feature的insert操作
                pFeatureCursor.InsertFeature(pBuffer);
                pFeatureCursor.Flush();
                //释放内存资源
                System.Runtime.InteropServices.Marshal.ReleaseComObject(pBuffer);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(pRasterValue);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(pFeatureCursor);
            }
            //在栅格图像列表中增加新的栅格图像
            cmb_LoadRstDataset.Items.Add(strOutName);
            //设置当前选项为新导入的栅格图像
            cmb_LoadRstDataset.SelectedIndex = cmb_LoadRstDataset.Items.Count - 1;
            //显示成功信息
            MessageBox.Show("导入成功！");
        }
        //在栅格目录下删除该栅格图像
        private void btn_DeleteRstDataset_Click(object sender, EventArgs e)
        {
            //获取当前选中的栅格图像
            int rstDatasetIndex = cmb_LoadRstCatalog.SelectedIndex;
            string rstDatasetName = cmb_LoadRstDataset.SelectedItem.ToString();

            //判断栅格目录下是否有图像可以删除
            if (rstDatasetName == "")
            {
                MessageBox.Show("当前栅格目录下没有栅格图像！");
                return;
            }
            //如果是非栅格目录
            if (cmb_LoadRstCatalog.SelectedIndex == 0)
            {
                IRasterWorkspaceEx rstWorkspaceEx = workspace as IRasterWorkspaceEx;
                IDataset datasetDel = rstWorkspaceEx.OpenRasterDataset(rstDatasetName) as IDataset;
                //调用IDataset接口的Delete接口实现已存在栅格数据集的删除
                datasetDel.Delete();
                datasetDel = null;
            }
            //如果是栅格目录
            else
            {
                string rasterCatalogName = cmb_LoadRstCatalog.Text;
                //在RasterCatalog中添加栅格
                //打开对应的RasterCatalog
                IRasterCatalog pRasterCatalog = ((IRasterWorkspaceEx)workspace).OpenRasterCatalog(rasterCatalogName);
                //将需要导入的RasterCatalog转换成FeatureClass
                IFeatureClass pFeatureClass = (IFeatureClass)pRasterCatalog;
                //IFields fields = pFeatureClass.Fields;
                //string fieldsName = "";
                //for (int i = 0; i < fields.FieldCount; i++)
                //{
                //    fieldsName += fields.get_Field(i).Name.ToString();
                //    fieldsName += "\n";
                //}

                //通过IQueryFilter的栅格图像名，找到对应图像的IFeatureCursor
                IQueryFilter queryFilter = new QueryFilter();
                queryFilter.WhereClause = "NAME='" + rstDatasetName + "'";//设置查询条件
                IFeatureCursor pFeatureCursor = pFeatureClass.Search(queryFilter, false);
                IFeature pFeature = pFeatureCursor.NextFeature();
                //for (int i = 0; i < fields.FieldCount; i++)
                //{
                //    fieldsName += pFeature.get_Value(i);
                //    fieldsName += "\n";
                //}
                //通过IFeatureCursor来删除栅格图像
                pFeature.Delete();
                //pFeatureCursor.DeleteFeature();报错未解决...
                //释放内存空间
                System.Runtime.InteropServices.Marshal.ReleaseComObject(pFeatureCursor);

            }
            //在栅格图像列表中删除该栅格图像
            cmb_LoadRstDataset.Items.Remove(rstDatasetName);

            if (cmb_LoadRstDataset.Items.Count > 0)
                cmb_LoadRstDataset.SelectedIndex = 0;
            else
                cmb_LoadRstDataset.Text = "";
            //显示成功信息
            MessageBox.Show("栅格图像删除成功！");
        }
        //TOCControl1右键菜单
        private void axTOCControl1_OnMouseDown(object sender, ITOCControlEvents_OnMouseDownEvent e)
        {
            try
            {
                //获取当前鼠标点击位置的相关信息
                esriTOCControlItem itemType = esriTOCControlItem.esriTOCControlItemNone;
                IBasicMap basicMap = null;
                ILayer layer = null;
                object unk = null;
                object data = null;
                //将以上定义的借口对象作为引用传入函数中，获取多个返回值
                this.axTOCControl1.HitTest(e.x, e.y, ref itemType, ref basicMap, ref layer, ref
                    	unk, ref data);
                //如若是鼠标右击，且点击位置为图层，获取多个返回值
                if (e.button == 2)
                {
                    if (itemType == esriTOCControlItem.esriTOCControlItemLayer)
                    {
                        //设置TOC选择图层
                        this.TOCRightLayer = layer;
                        this.contextMenuStrip1.Show(axTOCControl1, e.x, e.y);
                    }
                }
            }
            catch (System.Exception ex)//异常处理，输出错误信息
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //缩放到当前图层
        private void TSMI_ZoomToLayer_Click(object sender, EventArgs e)
        {
            try
            {
                //缩放到当前图层
                axMapControl1.ActiveView.Extent = TOCRightLayer.AreaOfInterest;
                //刷新当前页面
                axMapControl1.ActiveView.Refresh();
            }
            catch (System.Exception ex)//异常处理，输出错误信息
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //删除当前图层
        private void TSMI_DeleteLayer_Click(object sender, EventArgs e)
        {
            try
            {
                //删除当前图层
                axMapControl1.Map.DeleteLayer(TOCRightLayer);
                //刷新当前页面
                axMapControl1.ActiveView.Refresh();
                //更新波段信息统计的图层和波段下拉框选项内容
                iniCmbItems();
            }
            catch (System.Exception ex)//异常处理，输出错误信息
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //通过名称获得图层
        public ILayer GetLayerByName(String sLayerName)
        {
            if (sLayerName == "" || axMapControl1 == null)
            {
                return null;
            }
            //对地图对象中的所有图层进行遍历。若某一图层的名称与指定图层名相同，则返回图层
            for (int i = 0; i < axMapControl1.LayerCount; i++)
            {
                if (axMapControl1.get_Layer(i).Name == sLayerName)
                {
                    return axMapControl1.get_Layer(i);
                }
            }
            //若果没有匹配的，则函数返回空
            return null;
        }

        //当加载图层的时候，初始化tab页面里的图层和波段下拉框的选项内容
        private void iniCmbItems()
        {
            try
            {
                //拉伸方法列表默认选项为0--默认拉伸；
                cmb_StretchMethod.SelectedIndex = 0;
                //清除波段信息统计图下拉框的选项内容
                cmb_StatisticsLayer.Items.Clear();
                //清除NDVI指数计算图层下拉框的选项内容
                cmb_NDVILayer.Items.Clear();
                //清除直方图绘制图层下拉框的选项内容
                cmb_DrawHisLayer.Items.Clear();
                //清除单波段灰度增强的图层下拉框的选项内容
                cmb_StretchLayer.Items.Clear();
                //清除单波段伪彩色渲染的图层下拉框的选项内容
                cmb_RenderLayer.Items.Clear();
                //清除多波段假彩色合成的图层下拉框的选项内容
                cmb_RGBLayer.Items.Clear();

                cmb_ClassifyLayer.Items.Clear();
                cmb_GeneralLayer.Items.Clear();
                cmb_ClipLayer.Items.Clear();
                cmb_PanLayer.Items.Clear();
                cmb_MultiLayer.Items.Clear();
                cmb_TransformLayer.Items.Clear();
                cmb_HillshadeDEM.Items.Clear();
                cmb_SlopeDEM.Items.Clear();
                cmb_AspectDEM.Items.Clear();
                cmb_LineOfSightDEM.Items.Clear();
                cmb_VisibilityDEM.Items.Clear();
                cmb_NeighborhoodLayer.Items.Clear();
                cmb_ExtractionLayer.Items.Clear();
                ILayer layer = null;
                IMap map = axMapControl1.Map;
                int count = map.LayerCount;
                if (count > 0)
                {
                    //遍历地图的所有图层，获取图层名字加入下拉框
                    for (int i = 0; i < count; i++)
                    {
                        layer = map.get_Layer(i);
                        cmb_StatisticsLayer.Items.Add(layer.Name);
                        cmb_NDVILayer.Items.Add(layer.Name);
                        cmb_DrawHisLayer.Items.Add(layer.Name);
                        cmb_StretchLayer.Items.Add(layer.Name);
                        cmb_RenderLayer.Items.Add(layer.Name);
                        cmb_RGBLayer.Items.Add(layer.Name);
                        cmb_ClassifyLayer.Items.Add(layer.Name);
                        cmb_GeneralLayer.Items.Add(layer.Name);
                        cmb_ClipLayer.Items.Add(layer.Name);
                        cmb_PanLayer.Items.Add(layer.Name);
                        cmb_MultiLayer.Items.Add(layer.Name);
                        cmb_TransformLayer.Items.Add(layer.Name);
                        cmb_HillshadeDEM.Items.Add(layer.Name);
                        cmb_SlopeDEM.Items.Add(layer.Name);
                        cmb_AspectDEM.Items.Add(layer.Name);
                        cmb_LineOfSightDEM.Items.Add(layer.Name);
                        cmb_VisibilityDEM.Items.Add(layer.Name);
                        cmb_NeighborhoodLayer.Items.Add(layer.Name);
                        cmb_ExtractionLayer.Items.Add(layer.Name);
                    }
                    //设置下拉框默认选项第一个图层
                    if (cmb_StatisticsLayer.Items.Count > 0)
                    {
                        cmb_StatisticsLayer.SelectedIndex = 0;
                    }
                    if (cmb_NDVILayer.Items.Count > 0)
                    {
                        cmb_NDVILayer.SelectedIndex = 0;
                    }
                    if (cmb_DrawHisLayer.Items.Count > 0)
                    {
                        cmb_DrawHisLayer.SelectedIndex = 0;
                    }
                    if (cmb_StretchLayer.Items.Count > 0)
                    {
                        cmb_StretchLayer.SelectedIndex = 0;
                    }
                    if (cmb_RenderLayer.Items.Count > 0)
                    {
                        cmb_RenderLayer.SelectedIndex = 0;
                    }
                    if (cmb_RGBLayer.Items.Count > 0)
                    {
                        cmb_RGBLayer.SelectedIndex = 0;
                    }
                    if (cmb_ClassifyLayer.Items.Count > 0)
                    {
                        cmb_ClassifyLayer.SelectedIndex = 0;
                    }
                    if (cmb_GeneralLayer.Items.Count > 0)
                    {
                        cmb_GeneralLayer.SelectedIndex = 0;
                    }
                    if (cmb_ClipLayer.Items.Count > 0)
                    {
                        cmb_ClipLayer.SelectedIndex = 0;
                    }
                    if (cmb_PanLayer.Items.Count > 0)
                    {
                        cmb_PanLayer.SelectedIndex = 0;
                    }
                    if (cmb_MultiLayer.Items.Count > 0)
                    {
                        cmb_MultiLayer.SelectedIndex = 0;
                    }
                    if (cmb_TransformLayer.Items.Count > 0)
                    {
                        cmb_TransformLayer.SelectedIndex = 0;
                    }
                    if (cmb_HillshadeDEM.Items.Count > 0)
                    {
                        cmb_HillshadeDEM.SelectedIndex = 0;
                    }
                    if (cmb_SlopeDEM.Items.Count > 0)
                    {
                        cmb_SlopeDEM.SelectedIndex = 0;
                    }
                    if (cmb_AspectDEM.Items.Count > 0)
                    {
                        cmb_AspectDEM.SelectedIndex = 0;
                    }
                    if (cmb_LineOfSightDEM.Items.Count > 0)
                    {
                        cmb_LineOfSightDEM.SelectedIndex = 0;
                    }
                    if (cmb_VisibilityDEM.Items.Count > 0)
                    {
                        cmb_VisibilityDEM.SelectedIndex = 0;
                    }
                    if (cmb_NeighborhoodLayer.Items.Count > 0)
                    {
                        cmb_NeighborhoodLayer.SelectedIndex = 0;
                    }
                    if (cmb_ExtractionLayer.Items.Count > 0)
                    {
                        cmb_ExtractionLayer.SelectedIndex = 0;
                    }
                    //清除各波段下拉框的选项内容
                    cmb_StatisticsBand.Items.Clear();
                    cmb_DrawHisBand.Items.Clear();
                    cmb_StretchBand.Items.Clear();
                    cmb_RenderBand.Items.Clear();
                    cmb_RBand.Items.Clear();
                    cmb_GBand.Items.Clear();
                    cmb_BBand.Items.Clear();
                    //获取第一个图层的栅格波段
                    IRasterLayer rstLayer = map.get_Layer(0) as IRasterLayer;
                    //IRaster2 raster2 = rstLayer.Raster as IRaster2;
                    //IRasterDataset rstDataset = raster2.RasterDataset;
                    //IRasterBandCollection rstBandColl = rstDataset as IRasterBandCollection;
                    //波段总数
                    int bandCount = rstLayer.BandCount;
                    //添加所有波段的选项
                    cmb_StatisticsBand.Items.Add("全部波段");
                    //遍历图层的所有波段，获取波段名字加入下拉框
                    for (int i = 0; i < bandCount; i++)
                    {
                        int bandIdx = i + 1;//设置波段序号（应从1开始）
                        //添加波段下拉框的选项内容
                        cmb_StatisticsBand.Items.Add("波段" + bandIdx);
                        cmb_DrawHisBand.Items.Add("波段" + bandIdx);
                        cmb_StretchBand.Items.Add("波段" + bandIdx);
                        cmb_RenderBand.Items.Add("波段" + bandIdx);
                        cmb_RBand.Items.Add("波段" + bandIdx);
                        cmb_GBand.Items.Add("波段" + bandIdx);
                        cmb_BBand.Items.Add("波段" + bandIdx);
                    }
                    //设置下拉框默认选项为第一项
                    if (cmb_StatisticsBand.Items.Count > 0)
                    {
                        cmb_StatisticsBand.SelectedIndex = 0;
                    }
                    if (cmb_DrawHisBand.Items.Count > 0)
                    {
                        cmb_DrawHisBand.SelectedIndex = 0;
                    }
                    if (cmb_StretchBand.Items.Count > 0)
                    {
                        cmb_StretchBand.SelectedIndex = 0;
                    }
                    if (cmb_RenderBand.Items.Count > 0)
                    {
                        cmb_RenderBand.SelectedIndex = 0;
                    }
                    if (cmb_RBand.Items.Count > 0)
                    {
                        cmb_RBand.SelectedIndex = 0;
                    }
                    if (cmb_GBand.Items.Count > 0)
                    {
                        cmb_GBand.SelectedIndex = 0;
                    }
                    if (cmb_BBand.Items.Count > 0)
                    {
                        cmb_BBand.SelectedIndex = 0;
                    }
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //当遥感图像处理分析的图层下拉框的选择项发生变化，则相应的波段下拉框的选项也会发生变化
        private void selectedIndexChangeFunction(ComboBox cmbLayer, ComboBox cmbBand, string type)
        {
            try
            {
                //实现图层下拉框的波段下拉框的联动变化
                string layerName = cmbLayer.SelectedItem.ToString();
                ILayer layer = GetLayerByName(layerName);
                IRasterLayer rstLayer = layer as IRasterLayer;
                //波段总数
                int bandCount = rstLayer.BandCount;
                //清空原有波段列表
                cmbBand.Items.Clear();
                cmbBand.Text = "";
                if (cmbBand.Name == "cmb_StatisticsLayer")
                {
                    //添加所有波段的选项
                    cmbBand.Items.Add("全部波段");
                }
                //遍历图层的所有波段，获取波段名字加入下拉框
                for (int i = 0; i < bandCount; i++)
                {
                    int bandIdx = i + 1;//设置波段序号（应从1开始）
                    //添加波段下拉框的选项内容
                    cmbBand.Items.Add("波段" + bandIdx);
                }
                //设置下拉框默认选项为第一项
                if (cmbBand.Items.Count > 0)
                {
                    cmbBand.SelectedIndex = 0;
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //图层下拉框的选择变化触发事件
        private void cmb_StatisticsLayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                selectedIndexChangeFunction(cmb_StatisticsLayer, cmb_StatisticsBand, "statistics");
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void cmb_DrawHisLayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                selectedIndexChangeFunction(cmb_DrawHisLayer, cmb_DrawHisBand, null);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void cmb_StretchLayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                selectedIndexChangeFunction(cmb_StretchLayer, cmb_StretchBand, null);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void cmb_RenderLayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                selectedIndexChangeFunction(cmb_RenderLayer, cmb_RenderBand, null);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void cmb_RGBLayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                selectedIndexChangeFunction(cmb_RGBLayer, cmb_RBand, null);
                selectedIndexChangeFunction(cmb_RGBLayer, cmb_GBand, null);
                selectedIndexChangeFunction(cmb_RGBLayer, cmb_BBand, null);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //波段信息统计
        private void btn_Statistics_Click(object sender, EventArgs e)
        {
            try
            {
                //获取当前选中的栅格图层的栅格波段
                string statisticsLayerName = cmb_StatisticsLayer.SelectedItem.ToString();
                ILayer layer = GetLayerByName(statisticsLayerName);
                IRasterLayer rstLayer = layer as IRasterLayer;
                IRaster2 raster2 = rstLayer.Raster as IRaster2;
                IRasterDataset rstDataset = raster2.RasterDataset;
                IRasterBandCollection rstBandColl = rstDataset as IRasterBandCollection;

                int indexBand = cmb_StatisticsBand.SelectedIndex;
                string ststRes = "";//初始化统计结果
                //如果选择全部波段，则遍历该图层全部波段，并统计信息
                if (indexBand == 0)
                {
                    int bandCount = rstLayer.BandCount;
                    for (int i = 0; i < bandCount; i++)
                    {
                        IRasterBand rstBand = rstBandColl.Item(i);
                        //判断该波段是否已经存在统计数据
                        bool hasStat = false;
                        rstBand.HasStatistics(out hasStat);
                        //如果不存在统计数据，则进行波段信息统计
                        if (null == rstBand.Statistics || !hasStat)
                        {
                            //IRasterBandEdit rasterBand = rstBand as IRasterBandEdit;
                            //rasterBandEdit.ComputeStatsHistogram(0);
                            rstBand.ComputeStatsAndHist();
                        }
                        //获取统计结果
                        IRasterStatistics rstStat = rstBand.Statistics;
                        //获取统计信息数据，拼接结果字符串
                        double dMaxValue;
                        double dMinValue;
                        //经过ComputeStatsAndHist，也可能计算失败，Statistics仍然为空
                        if (null == rstBand.Statistics)
                        {
                            dMaxValue = 0.0;
                            dMinValue = 0.0;
                        }
                        else
                        {
                            dMaxValue = rstStat.Maximum;
                            dMinValue = rstStat.Minimum;
                        }
                        int j = i + 1;
                        ststRes += "波段" + j + "：最大值:" + dMaxValue + ";最小值：" + dMinValue + "\n";
                    }
                    //提示框输出统计结果
                    MessageBox.Show(ststRes, "统计结果", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    //获取波段
                    IRasterBand rstBand = rstBandColl.Item(indexBand - 1);
                    //判断该波段是否已经存在统计数据
                    bool hasStat = false;
                    rstBand.HasStatistics(out hasStat);
                    //如果不存在统计数据，则进行波段信息统计
                    if (null == rstBand.Statistics || !hasStat)
                    {
                        //IRasterBandEdit2 rasterBandEdit = rstBand as IRasterBandEdit2;
                        //rasterBandEdit.ComputeStatsHistogram(0);
                        rstBand.ComputeStatsAndHist();
                    }
                    //获取统计结果
                    IRasterStatistics rstStat = rstBand.Statistics;
                    //获取统计信息数据，拼接结果字符串
                    double dMaxValue = rstStat.Maximum;
                    double dMinValue = rstStat.Minimum;
                    ststRes += "波段" + (indexBand - 1) + "：最大值:" + dMaxValue + ";最小值：" + dMinValue.ToString() + "\n";

                    //提示框输出统计结果
                    MessageBox.Show(ststRes, "统计结果", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        //NDVI计算 （近红外-红外）/（近红外+红外）
        private void btn_CalculateNDVI_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;//单击时修改鼠标光标形状
            try
            {
                //获取选择的图层和波段，转换接口
                string layerName = cmb_NDVILayer.SelectedItem.ToString();
                ILayer layer = GetLayerByName(layerName);
                IRasterLayer rasterLayer = layer as IRasterLayer;
                IRaster2 raster = rasterLayer.Raster as IRaster2;
                IRasterDataset rasterDataset = raster.RasterDataset;
                IRasterBandCollection rasterBandCollection = rasterDataset as IRasterBandCollection;
                /////!!!注意 确保至少有4个波段？
                int bandCount = rasterBandCollection.Count;
                if (bandCount < 4)
                {
                    MessageBox.Show("所选图层波段数过少，无法计算", "提示");
                    return;
                }

                //获取红外波段和近红外波段，转换IGeodataset接口
                IRasterBand rasterBand4 = rasterBandCollection.Item(3);//获取第四波段，即近红外波段
                IRasterBand rasterBand3 = rasterBandCollection.Item(2);//获取第三波段，即红外波段
                IGeoDataset geoDataset4 = rasterBand4 as IGeoDataset;
                IGeoDataset geoDataset3 = rasterBand3 as IGeoDataset;

                //利用IGeodataset和math计算NDVI获得结果IGeodataset
                //创建一个用于栅格运算的类RasterMathOpClass
                IMathOp mathOp = new RasterMathOpsClass();
                //band4-band3
                IGeoDataset upDataset = mathOp.Minus(geoDataset4, geoDataset3);
                //band4+band3
                IGeoDataset downDataset = mathOp.Plus(geoDataset4, geoDataset3);
                //分子分母转为float类型
                IGeoDataset fltUpDataset = mathOp.Float(upDataset);
                IGeoDataset fltdownDataset = mathOp.Float(downDataset);
                //相除得到NDVI
                IGeoDataset resultDataset = mathOp.Divide(fltUpDataset, fltdownDataset);
                //将结果保存到一个RasterLayer中，命名为NDVI
                IRaster resRaster = resultDataset as IRaster;
                IRasterLayer resLayer = new RasterLayerClass();
                resLayer.CreateFromRaster(resRaster);
                resLayer.SpatialReference = geoDataset4.SpatialReference;
                resLayer.Name = "NDVI";
                //将此单波段图像用灰度显示，并按照最大最小值拉伸
                IRasterStretchColorRampRenderer grayStretch = null;
                if (resLayer.Renderer is IRasterStretchColorRampRenderer)
                {
                    grayStretch = resLayer.Renderer as IRasterStretchColorRampRenderer;
                }
                else
                {
                    grayStretch = new RasterStretchColorRampRendererClass();
                }
                IRasterStretch2 rstStr2 = grayStretch as IRasterStretch2;
                rstStr2.StretchType = esriRasterStretchTypesEnum.esriRasterStretch_MinimumMaximum;//设置拉伸模式为最大最小值
                resLayer.Renderer = grayStretch as IRasterRenderer;
                resLayer.Renderer.Update();

                //添加NDVI图层显示，并刷新视图
                axMapControl1.AddLayer(resLayer);
                axMapControl1.ActiveView.Extent = resLayer.AreaOfInterest;
                axMapControl1.Refresh();
                axTOCControl1.Update();
                iniCmbItems();
            }
            catch (System.Exception ex)//异常处理
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally//最后再将鼠标光标设置成默认形状
            {
                this.Cursor = Cursors.Default;
            }
        }
        //单波段直方图绘制
        private void btn_SingleBandHis_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;//单击时修改鼠标光标形状
            try
            {
                //获取选择的图层和波段对象，接口转换
                string layerName = cmb_DrawHisLayer.SelectedItem.ToString();
                int bandIndex = cmb_DrawHisBand.SelectedIndex;
                ILayer layer = GetLayerByName(layerName);
                IRasterLayer rasterLayer = layer as IRasterLayer;
                IRaster2 raster2 = rasterLayer.Raster as IRaster2;
                IRasterDataset rasterDataset = raster2.RasterDataset;
                IRasterBandCollection rasterBandCollection = rasterDataset as IRasterBandCollection;
                IRasterBand rasterBand = rasterBandCollection.Item(bandIndex);
                //计算该波段的histogram（tips：类似于计算statistics）
                bool hasStat = false;
                rasterBand.HasStatistics(out hasStat);
                if (null == rasterBand.Statistics || !hasStat || rasterBand.Histogram == null)
                {
                    //转换IRasterBandEdit2接口，调用ComputeStatsHistogram方法进行波段信息统计和直方图绘制
                    IRasterBandEdit2 rasterBandEdit = rasterBand as IRasterBandEdit2;
                    rasterBandEdit.ComputeStatsHistogram(0);
                }
                //获取每个象元值的统计个数
                double[] histo = rasterBand.Histogram.Counts as double[];
                //获取统计结果
                IRasterStatistics rasterStatistics = rasterBand.Statistics;
                //创建直方图窗体，并将象元统计、最小值、最大值作为参数传入
                HistogramForm histogramForm = new HistogramForm(histo, rasterStatistics.Minimum, rasterStatistics.Maximum);
                histogramForm.ShowDialog();
            }
            catch (System.Exception ex)//异常处理
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally//最后再将鼠标光标设置成默认形状
            {
                this.Cursor = Cursors.Default;
            }
        }
        //多波段直方图对比绘制
        private void btn_MultiBandHis_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;//单击时修改鼠标光标形状
            try
            {
                //获取当前选中的图层index
                int indexLayer = cmb_DrawHisLayer.SelectedIndex;
                //读取MapControl中的map相应图层
                ILayer layer = this.axMapControl1.Map.get_Layer(indexLayer);
                if (layer is IRasterLayer)
                {
                    IRasterLayer rasterLayer = layer as IRasterLayer;
                    SelectBandsForm SelectBands = new SelectBandsForm(rasterLayer);
                    SelectBands.ShowDialog();
                }

            }
            catch (System.Exception ex)//异常处理
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally//最后再将鼠标光标设置成默认形状
            {
                this.Cursor = Cursors.Default;
            }
        }

        //点击单波段灰度增强按钮，实现增强
        private void btn_Stretch_Click(object sender, EventArgs e)
        {
            try
            {
                //获取当前选择的图层和波段对象
                string stretchLayerName = cmb_StretchLayer.SelectedItem.ToString();
                int stretchBandIndex = cmb_StretchBand.SelectedIndex;
                ILayer layer = GetLayerByName(stretchLayerName);

                if (layer is IRasterLayer)
                {
                    //获取波段渲染信息，创建拉伸渲染类对象，设置其波段信息
                    IRasterLayer rstLayer = layer as IRasterLayer;
                    IRasterRenderer rstRenderer = rstLayer.Renderer as IRasterRenderer;
                    IRasterStretchColorRampRenderer grayRenderer = new RasterStretchColorRampRendererClass();
                    grayRenderer.BandIndex = stretchBandIndex;
                    IRasterStretch2 rstStr2 = grayRenderer as IRasterStretch2;
                    switch (cmb_StretchMethod.SelectedIndex)
                    {
                        case 0://默认拉伸
                            rstStr2.StretchType = esriRasterStretchTypesEnum.esriRasterStretch_DefaultFromSource;
                            break;
                        case 1://标准差拉伸
                            rstStr2.StretchType = esriRasterStretchTypesEnum.esriRasterStretch_DefaultFromSource;
                            break;
                        case 2://最大最小值拉伸
                            rstStr2.StretchType = esriRasterStretchTypesEnum.esriRasterStretch_MinimumMaximum;
                            break;
                        case 3://百分比最大最小值拉伸
                            rstStr2.StretchType = esriRasterStretchTypesEnum.esriRasterStretch_PercentMinimumMaximum;
                            break;
                        case 4://直方图均衡
                            rstStr2.StretchType = esriRasterStretchTypesEnum.esriRasterStretch_HistogramEqualize;
                            break;
                        case 5://直方图匹配
                            rstStr2.StretchType = esriRasterStretchTypesEnum.esriRasterStretch_HistogramSpecification;
                            break;
                        default:
                            break;
                    }
                    //设置不应用反色
                    rstStr2.Invert = false;
                    rstRenderer = grayRenderer as IRasterRenderer;
                    rstRenderer.Update();
                    rstLayer.Renderer = rstRenderer;
                }
                //刷新控件
                this.axMapControl1.ActiveView.Refresh();
                this.axMapControl1.Update();
            }
            catch (System.Exception ex)//异常处理，输出错误信息
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //根据起始颜色生成AlgorithmicColorRamp
        private IAlgorithmicColorRamp GetAlgorithmicColorRamp(Color FromColor, Color ToColor, int size)
        {
            try
            {
                //实例化接口
                IAlgorithmicColorRamp algorithmicColorRamp = new AlgorithmicColorRampClass();
                //创建起始颜色
                IRgbColor rgbFromColor = new RgbColorClass();
                rgbFromColor.Red = FromColor.R;
                rgbFromColor.Green = FromColor.G;
                rgbFromColor.Blue = FromColor.B;
                //创建终止颜色
                IRgbColor rgbToColor = new RgbColorClass();
                rgbToColor.Red = ToColor.R;
                rgbToColor.Green = ToColor.G;
                rgbToColor.Blue = ToColor.B;
                //赋值颜色
                algorithmicColorRamp.FromColor = rgbFromColor;
                algorithmicColorRamp.ToColor = rgbToColor;
                //设置色带类型的大小
                algorithmicColorRamp.Algorithm = esriColorRampAlgorithm.esriHSVAlgorithm;
                algorithmicColorRamp.Size = size;
                //需要IAlgorithmicColorRamp接口的CreateRamp函数创建色带
                bool bResult;
                algorithmicColorRamp.CreateRamp(out bResult);
                if (bResult)
                {
                    return algorithmicColorRamp;
                }
                return null;

            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
        //通过起始颜色创建色带bitmap图像
        private Bitmap CreateColorRamp(Color FromColor, Color ToColor)
        {
            try
            {
                //获取色带
                IAlgorithmicColorRamp algorithmicColorRamp =
                    GetAlgorithmicColorRamp(FromColor, ToColor, pb_ColorBar.Size.Width);
                //创建新的bitmap
                Bitmap bmpColorRamp = new Bitmap(pb_ColorBar.Size.Width, pb_ColorBar.Size.Height);
                //获取graphic对象
                Graphics graphics = Graphics.FromImage(bmpColorRamp);
                //用GDI+的方法逐一填充颜色到显示色带
                IColor color = null;
                for (int i = 0; i < pb_ColorBar.Size.Width; i++)
                {
                    //获取当前颜色
                    color = algorithmicColorRamp.get_Color(i);
                    if (color == null)
                    {
                        continue;
                    }
                    IRgbColor rgbColor = new RgbColorClass();
                    rgbColor.RGB = color.RGB;
                    Color customColor = Color.FromArgb(rgbColor.Red, rgbColor.Green, rgbColor.Blue);
                    SolidBrush solidBrush = new SolidBrush(customColor);
                    //绘制
                    graphics.FillRectangle(solidBrush, i, 0, 1, pb_ColorBar.Size.Height);
                }
                //删除graphics对象
                graphics.Dispose();
                return bmpColorRamp;
            }
            catch (System.Exception ex)//异常处理
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
        //重新绘制起始颜色，并绘制色带
        private void RefreshColors(Color FromColor, Color ToColor) //重新绘制起始颜色、终止颜色，并根据这两种颜色绘制色带
        {
            try
            {
                //初始化FromColor
                //创建bitmap
                Bitmap bmpFromColor = new Bitmap(pb_FromColor.Size.Width, pb_FromColor.Size.Height);
                //创建graphics对象
                Graphics graphicFC = Graphics.FromImage(bmpFromColor);
                SolidBrush solidBrushFC = new SolidBrush(FromColor);
                //绘制起始颜色吗，左下到右上
                graphicFC.FillRectangle(solidBrushFC, 0, 0, pb_FromColor.Size.Width, pb_FromColor.Size.Height);
                //更新图像
                this.pb_FromColor.Image = bmpFromColor;
                //删除graphics对象
                graphicFC.Dispose();

                //初始化ToColor
                //创建bitmap
                Bitmap bmpToColor = new Bitmap(pb_ToColor.Size.Width, pb_ToColor.Size.Height);
                //创建graphics对象
                Graphics graphicTC = Graphics.FromImage(bmpToColor);
                SolidBrush solidBrushTC = new SolidBrush(ToColor);
                //绘制终止颜色，左下到右上
                graphicTC.FillRectangle(solidBrushTC, 0, 0, pb_ToColor.Size.Width, pb_ToColor.Size.Height);
                //更新图像
                this.pb_ToColor.Image = bmpToColor;
                //删除graphics对象
                graphicTC.Dispose();

                //初始化色带
                Bitmap stretchRamp = CreateColorRamp(FromColor, ToColor);
                //更新图像
                this.pb_ColorBar.Image = stretchRamp;
            }
            catch (System.Exception ex)//捕获异常，并输出错误信息
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //起始颜色
        private void pb_FromColor_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.cd_FromColor.ShowDialog() == DialogResult.OK)
                {
                    m_FromColor = this.cd_FromColor.Color;
                    RefreshColors(m_FromColor, m_ToColor);
                }
            }
            catch (System.Exception ex)//异常处理
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //终止颜色
        private void pb_ToColor_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.cd_ToColor.ShowDialog() == DialogResult.OK)
                {
                    m_ToColor = this.cd_ToColor.Color;
                    RefreshColors(m_FromColor, m_ToColor);
                }
            }
            catch (System.Exception ex)//异常处理
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //单波段伪彩色渲染
        private void btn_Render_Click(object sender, EventArgs e)
        {
            try
            {
                //获取当前选中的图层index
                int indexLayer = cmb_DrawHisLayer.SelectedIndex;
                //读取MapControl中的map相应图层
                ILayer layer = this.axMapControl1.Map.get_Layer(indexLayer);
                //获取栅格图像中的raster
                IRaster raster;
                IRasterLayer rstLayer;
                if (null != layer && layer is IRasterLayer)
                {
                    rstLayer = layer as IRasterLayer;
                    raster = rstLayer.Raster;
                }
                else
                {
                    return;
                }

                //设置IRasterRenderer
                IRasterStretchColorRampRenderer stretchRenderer = new RasterStretchColorRampRendererClass();
                IRasterRenderer rasterRenderer = (IRasterRenderer)stretchRenderer;
                rasterRenderer.Raster = raster;
                //获取并设置渲染应用波段
                stretchRenderer.BandIndex = cmb_RenderBand.SelectedIndex;

                //设置拉伸类型
                IRasterStretch2 rstStretch2 = rasterRenderer as IRasterStretch2;
                rstStretch2.StretchType = esriRasterStretchTypesEnum.esriRasterStretch_HistogramEqualize; //设置拉伸方式为直方图均衡化

                //获取色带，256带
                IAlgorithmicColorRamp algorithmicColorRamp = GetAlgorithmicColorRamp(m_FromColor, m_ToColor, 256);
                IColorRamp colorRamp = algorithmicColorRamp as IColorRamp;
                //设置拉伸渲染的色带
                stretchRenderer.ColorRamp = colorRamp;

                //设置TOC中的图例
                ILegendInfo legendInfo = stretchRenderer as ILegendInfo;
                ILegendGroup legendGroup = legendInfo.get_LegendGroup(0);
                for (int i = 0; i < legendGroup.ClassCount; i++)
                {
                    ILegendClass legendClass = legendGroup.get_Class(i);
                    legendClass.Symbol = new ColorRampSymbolClass();
                    IColorRampSymbol colorRampSymbol = legendClass.Symbol as IColorRampSymbol;
                    colorRampSymbol.ColorRamp = colorRamp;
                    colorRampSymbol.ColorRampInLegendGroup = colorRamp;
                    colorRampSymbol.LegendClassIndex = i;
                    legendClass.Symbol = colorRamp as ISymbol;
                }

                //应用设置渲染
                rasterRenderer.Update();
                rstLayer.Renderer = rasterRenderer;
                rstLayer.Renderer.Update();
                //刷新控件
                this.axMapControl1.ActiveView.Refresh();
                this.axTOCControl1.Update();
            }
            catch (System.Exception ex) //捕获异常，输出错误信息
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //多波段伪彩色渲染
        private void btn_RGB_Click(object sender, EventArgs e)
        {
            try
            {
                //获取当前选中的图层index
                int indexLayer = cmb_DrawHisLayer.SelectedIndex;
                //读取MapControl中的map相应图层
                ILayer layer = this.axMapControl1.Map.get_Layer(indexLayer);
                //获取当前选择的波段index
                int indexRBand = cmb_RBand.SelectedIndex;
                int indexGBand = cmb_GBand.SelectedIndex;
                int indexBBand = cmb_BBand.SelectedIndex;
                if (layer is IRasterLayer)
                {
                    //转换成IRasterLayer接口
                    IRasterLayer pRstLyr = layer as IRasterLayer;
                    //获取载波段渲染信息
                    IRasterRenderer rstRenderer = pRstLyr.Renderer;
                    //创建RGB合成渲染类
                    IRasterRGBRenderer rgbRenderer = null;
                    if (rstRenderer is IRasterRGBRenderer)
                    {
                        rgbRenderer = rstRenderer as IRasterRGBRenderer;
                    }
                    else
                    {
                        rgbRenderer = new RasterRGBRendererClass();
                    }
                    //获取并设置RGB对应波段
                    rgbRenderer.RedBandIndex = indexRBand;
                    rgbRenderer.GreenBandIndex = indexGBand;
                    rgbRenderer.BlueBandIndex = indexBBand;
                    //更新渲染类
                    rstRenderer = rgbRenderer as IRasterRenderer;
                    rstRenderer.Update();
                    //将RGB渲染参数赋值给图层渲染器
                    pRstLyr.Renderer = rstRenderer;
                }
                //更新控件
                this.axMapControl1.ActiveView.Refresh();
                this.axTOCControl1.Update();
            }
            catch (System.Exception ex) //捕获异常，输出错误信息
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //栅格图像唯一值渲染
        public IRasterRenderer UniqueValueRenderer(IRasterDataset rasterDataset)
        {
            try
            {
                //获取栅格图像属性表及其大小
                IRaster2 raster = (IRaster2)rasterDataset.CreateDefaultRaster();
                ITable rasterTable = raster.AttributeTable;
                if (rasterTable == null)
                {
                    return null;
                }
                int tableRows = rasterTable.RowCount(null);
                //为每一个属性的唯一值创建和设置一个唯一颜色
                IRandomColorRamp colorRamp = new RandomColorRampClass();
                //设置随机色带的属性参数
                colorRamp.Size = tableRows;
                colorRamp.Seed = 100;
                //调用createRamp方法来创建色带
                bool createColorRamp;
                colorRamp.CreateRamp(out createColorRamp);
                if (createColorRamp == false)
                {
                    return null;
                }
                //创建一个唯一值渲染器
                IRasterUniqueValueRenderer uvRenderer = new RasterUniqueValueRendererClass();
                IRasterRenderer rasterRenderer = (IRasterRenderer)uvRenderer;
                //设置渲染器的栅格数据对象（属性）
                rasterRenderer.Raster = rasterDataset.CreateDefaultRaster();
                rasterRenderer.Update();
                //设置渲染器的属性值
                uvRenderer.HeadingCount = 1;
                uvRenderer.set_Heading(0, "All Data Value");
                uvRenderer.set_ClassCount(0, tableRows);
                uvRenderer.Field = "Value";//或者表格中的其他字段
                //遍历属性表格，分别设置唯一值颜色
                IRow row;
                //创建简单填充符号接口的对象，用于每一个类别的像素的颜色填充
                ISimpleFillSymbol fillSymbol;
                for (int i = 0; i < tableRows; i++)
                {
                    row = rasterTable.GetRow(i);
                    //为某一个特定的类别添加至
                    uvRenderer.AddValue(0, i, Convert.ToByte(row.get_Value(1)));
                    //为某一个特定的类别设置标签
                    uvRenderer.set_Label(0, i, Convert.ToString(row.get_Value(1)));
                    //实例化创建一个简单填充副好累的对象
                    fillSymbol = new SimpleFillSymbolClass();
                    fillSymbol.Color = colorRamp.get_Color(i);
                    //为某一个特定的类别设置渲染符号
                    uvRenderer.set_Symbol(0, i, (ISymbol)fillSymbol);
                }
                return rasterRenderer;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return null;
            }
        }

        //分类--实现栅格图像的分类操作
        private void btn_Classify_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;//单击时修改鼠标光标形状
            try
            {
                //获取输入的分类数目
                int num = int.Parse(txb_ClassNum.Text);
                //获取选中的图层
                int indexLayer = cmb_ClassifyLayer.SelectedIndex;
                ILayer layer = this.axMapControl1.get_Layer(indexLayer);
                if (layer is IRasterLayer)
                {
                    //转换成IRasterLayer接口  
                    IRasterLayer rstLayer = layer as IRasterLayer;
                    if (rstLayer.BandCount > 1)
                    {
                        //获取图层raster并转换成IRaster2接口      
                        IRaster2 raster2 = rstLayer.Raster as IRaster2;
                        //获取该raster的RasterDataset         
                        IRasterDataset rstDataset = raster2.RasterDataset;
                        //转换IGeodataset接口         
                        IGeoDataset geoDataset = rstDataset as IGeoDataset;
                        //创建多元操作组件类对象
                        IMultivariateOp mulop = new RasterMultivariateOpClass();
                        //判断保存路径是否为空
                        if (txb_ResultPath.Text == "")
                        {
                            //提示框输出统计结果
                            MessageBox.Show("请先选择分类结果的保存路径！", "重点提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        string signatureFile = txb_ResultPath.Text + "\\signatureFile";
                        string dendrogramFile = txb_ResultPath.Text + "\\dendrogramFile";

                        int NumClass = num;
                        //使用 isodata 聚类算法来确定多维属性空间中像元自然分组的特征并将结果存储在输出 ASCII 特征文件中
                        //后四个参数分别为分类数量、迭代次数、最小类大小、样本间隔
                        mulop.IsoCluster(geoDataset, signatureFile, NumClass, 20, 20, 10);
                        //分类结果数据集
                        IGeoDataset outdataset;
                        //定义missing的类型（参数undifined）
                        object missing = Type.Missing;
                        //获取分类方法索引值
                        int method = cmb_ClassifyMethod.SelectedIndex;
                        switch (method)
                        {
                            case 0:
                                //对一组栅格波段执行最大似然法分类并创建分类的输出栅格数据。
                                outdataset = mulop.MLClassify(geoDataset, signatureFile, false,
                                    esriGeoAnalysisAPrioriEnum.esriGeoAnalysisAPrioriEqual, missing, missing);
                                break;
                            default:
                                //创建概率波段的多波段栅格，并为输入特征文件中所表示的每个类对应创建一个波段。
                                outdataset = mulop.ClassProbability(geoDataset, signatureFile,
                                    esriGeoAnalysisAPrioriEnum.esriGeoAnalysisAPrioriSample, missing, missing);

                                //对一组栅格波段执行主成分分析 (PCA) 并生成单波段栅格作为输出。
                                //outdataset = mulop.PrincipalComponents(geoDataset, signatureFile,
                                //    esriGeoAnalysisAPrioriEnum.esriGeoAnalysisAPrioriSample, missing, missing);
                                break;
                        }

                        //定义输出结果栅格
                        IRaster2 outraster = (IRaster2)outdataset;
                        //对分类结果栅格数据进行唯一值渲染显示
                        IRasterRenderer rasterRenderer = UniqueValueRenderer(outraster.RasterDataset);
                        //在栅格图层中加载显示
                        IRasterLayer rasterLayer = new RasterLayerClass();
                        rasterLayer.CreateFromDataset(outraster.RasterDataset);
                        rasterLayer.Name = cmb_ClassifyLayer.SelectedItem.ToString() + cmb_ClassifyMethod.SelectedItem.ToString();
                        //设置栅格渲染器对象
                        if (rasterRenderer != null)
                        {
                            rasterLayer.Renderer = rasterRenderer;
                        }
                        //将渲染好的栅格图像加载到map中
                        if (rasterLayer != null)
                        {
                            //更新控件
                            ILayer iLayer = rasterLayer as ILayer;
                            axMapControl1.Map.AddLayer(iLayer);
                            axMapControl1.ActiveView.Refresh();
                            axTOCControl1.Update();
                            //更新combobox里面的选项（图层和波段）
                            iniCmbItems();
                            //Dendrogram
                            mulop.Dendrogram(signatureFile, dendrogramFile, true, missing);
                        }

                    }
                }
            }
            catch (System.Exception ex) //捕获异常，输出异常信息   
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }   
            finally //最后再将鼠标光标设置成默认形状    
            {
                this.Cursor = Cursors.Default;
            }
        }
        //栅格综合
        private void btn_Matrix_Click(object sender, EventArgs e)
        {
            //System.Diagnostics.Process.Start("d:\\zsyzsyzsy\\raster\\classify_signature.gsg");
            this.Cursor = Cursors.WaitCursor; //单击时修改鼠标光标形状     
            try
            {
                //获取选中的图层
                int indexLayer = cmb_ClassifyLayer.SelectedIndex;
                ILayer layer = this.axMapControl1.get_Layer(indexLayer);
                if (layer is IRasterLayer)
                {
                    //转换成IRasterLayer接口  
                    IRasterLayer rstLayer = layer as IRasterLayer;
                    //获取图层raster并转换成IRaster2接口      
                    IRaster2 raster2 = rstLayer.Raster as IRaster2;
                    //获取该raster的RasterDataset         
                    IRasterDataset rstDataset = raster2.RasterDataset;
                    //转换IGeodataset接口         
                    IGeoDataset geoDataset = rstDataset as IGeoDataset;
                    // Create the RasterGeneralizeOp object  
                    IGeneralizeOp generalizeOp = new ESRI.ArcGIS.SpatialAnalyst.RasterGeneralizeOpClass();
                    IGeoDataset outdataset;
                    //获取综合方法索引值
                    int method = cmb_GeneralMethod.SelectedIndex;
                    switch (method)
                    {
                        case 0:
                            // 聚合
                            // 生成分辨率降低版本的栅格。每个输出像元包含此像元范围内所涵盖的输入像元的总和值、最小值、最大值、平均值或中值。
                            outdataset = generalizeOp.Aggregate(geoDataset, 4, ESRI.ArcGIS.GeoAnalyst.esriGeoAnalysisStatisticsEnum.esriGeoAnalysisStatsMean, true, true);
                            break;
                        case 1:
                            // 边界清理         
                            // 通过扩展和收缩来平滑区域间的边界。
                            outdataset = generalizeOp.BoundaryClean(geoDataset, ESRI.ArcGIS.SpatialAnalyst.esriGeoAnalysisSortEnum.esriGeoAnalysisSortAscending, true);
                            break;
                        default:
                            // 众数滤波   
                            // 根据相邻像元数据值的众数替换栅格中的像元。
                            outdataset = generalizeOp.MajorityFilter(geoDataset, true, false);
                            break;
                    }

                    IRaster2 outraster = (IRaster2)outdataset;
                    IRasterLayer rasterLayer = new RasterLayerClass();
                    rasterLayer.CreateFromDataset(outraster.RasterDataset);
                    rasterLayer.Name = cmb_GeneralLayer.SelectedItem.ToString() + cmb_GeneralMethod.SelectedItem.ToString();
                    ILayer iLayer = rasterLayer as ILayer;
                    axMapControl1.Map.AddLayer(iLayer);
                    //更新控件           
                    axMapControl1.ActiveView.Refresh();
                    axTOCControl1.Update();
                    //更新combobox里面的选项（图层和波段）
                    iniCmbItems();
                }
            }
            catch (System.Exception ex) //捕获异常，输出异常信息   
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }   
            finally //最后再将鼠标光标设置成默认形状    
            {
                this.Cursor = Cursors.Default;
            }
        }
        //选择分类结果保存路径
        private void txb_ResultPath_MouseDown(object sender, MouseEventArgs e)
        {
            //打开文件选择对话框，设置对话框属性
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择文件路径";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txb_ResultPath.Text = dialog.SelectedPath;
            }
        }
        //点击选择裁剪的矢量文件
        private void txb_ClipFeature_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                //打开文件选择对话框，设置对话框属性
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Shp file（*.shp）|*.shp";
                openFileDialog.Title = "打开矢量文件";
                openFileDialog.Multiselect = false;
                string fileName = "";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    fileName = openFileDialog.FileName;
                    txb_ClipFeature.Text = fileName;
                }
            }
            catch (System.Exception ex) //捕获异常，输出异常信息   
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //进行矢量文件对栅格图像的裁剪
        private void btn_Clip_Click(object sender, EventArgs e)
        {
            try
            {
                //获取选中的图层
                int indexLayer = cmb_ClipLayer.SelectedIndex;
                ILayer layer = this.axMapControl1.get_Layer(indexLayer);
                if (layer is IRasterLayer)
                {
                    //转换成IRasterLayer接口  
                    IRasterLayer rstLayer = layer as IRasterLayer;
                    //获取图层raster并转换成IRaster2接口      
                    IRaster2 raster2 = rstLayer.Raster as IRaster2;
                    //获取该raster的RasterDataset         
                    IRasterDataset2 rstDataset = raster2.RasterDataset as IRasterDataset2;
                    IRaster raster = rstDataset.CreateFullRaster();

                    //获取矢量文件的路径和名字
                    string fileN = txb_ClipFeature.Text;
                    FileInfo fileInfo = new FileInfo(fileN);
                    string filePath = fileInfo.DirectoryName;
                    string fileName = fileInfo.Name;

                    //根据选择的矢量文件的路径打开工作空间
                    IWorkspaceFactory wsf = new ShapefileWorkspaceFactoryClass();
                    IWorkspace ws = wsf.OpenFromFile(filePath, 0);
                    IFeatureWorkspace fws = ws as IFeatureWorkspace;
                    //根据名字获取矢量要素类
                    IFeatureClass featureClass = fws.OpenFeatureClass(fileName);

                    //构造一个裁剪过滤器
                    IClipFilter2 clipRaster = new ClipFilterClass();
                    clipRaster.ClippingType = esriRasterClippingType.esriRasterClippingOutside;
                    //将矢量数据的几何属性加到过滤器中
                    IGeometry clipGeometry;
                    IFeature feature;
                    for (int i = 0; i < featureClass.FeatureCount(null); i++)
                    {
                        feature = featureClass.GetFeature(i);
                        clipGeometry = (IGeometry)feature.Shape;
                        clipRaster.Add(clipGeometry);
                    }

                    //将这个过滤器作用于栅格图像
                    IPixelOperation pixelOp = (IPixelOperation)raster;
                    pixelOp.PixelFilter = (IPixelFilter)clipRaster;

                    //如果输入的栅格中不包含Nodata，和曾经使用过的最大像素深度，则为输出文件设置像素深度和nodata赋值
                    IRasterProps rasterProps = (IRasterProps)raster;
                    rasterProps.NoDataValue = 0;
                    rasterProps.PixelType = rstPixelType.PT_USHORT;

                    //存储裁剪结果栅格图像
                    IWorkspaceFactory wsf2 = new RasterWorkspaceFactoryClass();
                    IWorkspace rstWS = wsf2.OpenFromFile(@"C:\Users\31401\Desktop\files\results", 0);                    //保存输出
                    ISaveAs saveas = (ISaveAs)raster;
                    saveas.SaveAs("clip_result.tif", rstWS, "TIFF");
                    //加载显示裁剪结果图像
                    IRaster2 outraster = raster as IRaster2;
                    IRasterLayer rasterLayer = new RasterLayerClass();
                    rasterLayer.CreateFromDataset(outraster.RasterDataset);
                    rasterLayer.Name = fileName + "裁剪" + cmb_ClipLayer.SelectedItem.ToString();
                    ILayer iLayer = rasterLayer as ILayer;
                    axMapControl1.Map.AddLayer(iLayer);
                    //更新控件           
                    axMapControl1.ActiveView.Refresh();
                    axTOCControl1.Update();
                    iniCmbItems();
                }

            }
            catch (System.Exception ex) //捕获异常，输出异常信息   
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //图像融合，高空间分辨率的单波段图像和地空间分辨率的多波段图像
        private void btn_PanSharpen_Click(object sender, EventArgs e)
        {
            try
            {
                //获取选中的图层
                int indexPanLayer = cmb_PanLayer.SelectedIndex;
                int indexMultiLayer = cmb_MultiLayer.SelectedIndex;
                ILayer panlayer = this.axMapControl1.get_Layer(indexPanLayer);
                ILayer multilayer = this.axMapControl1.get_Layer(indexMultiLayer);

                if (panlayer is IRasterLayer && multilayer is IRasterLayer)
                {
                    //转换成IRasterLayer接口  
                    IRasterLayer panRstLayer = panlayer as IRasterLayer;
                    IRasterLayer multiRstLayer = multilayer as IRasterLayer;
                    //获取图层raster并转换成IRaster2接口      
                    IRaster2 panRaster2 = panRstLayer.Raster as IRaster2;
                    IRaster2 multiRaster2 = multiRstLayer.Raster as IRaster2;
                    //获取该raster的RasterDataset         
                    IRasterDataset panRstDataset = panRaster2.RasterDataset;
                    IRasterDataset multiRstDataset = multiRaster2.RasterDataset;

                    //假设波段顺序：RGB和近红外
                    //创建全色和多光谱栅格数据集的full栅格对象
                    IRaster panRaster = ((IRasterDataset2)panRstDataset).CreateDefaultRaster();
                    IRaster multiRaster = ((IRasterDataset2)multiRstDataset).CreateDefaultRaster();
                    //设置红外波段
                    IRasterBandCollection rasterbandCol = (IRasterBandCollection)multiRaster;
                    IRasterBandCollection infredRaster = new RasterClass();
                    infredRaster.AppendBand(rasterbandCol.Item(3));

                    //设置全色波段的属性
                    IRasterProps panSharpenRasterProps = (IRasterProps)multiRaster;
                    IRasterProps panRasterProps = (IRasterProps)panRaster;
                    panSharpenRasterProps.Width = panRasterProps.Width;
                    panSharpenRasterProps.Height = panRasterProps.Height;
                    panSharpenRasterProps.Extent = panRasterProps.Extent;
                    multiRaster.ResampleMethod = rstResamplingTypes.RSP_BilinearInterpolation;

                    //创建全色锐化过滤器和设置其他参数
                    IPansharpeningFilter pansharpenFilter = new PansharpeningFilterClass();
                    pansharpenFilter.InfraredImage = (IRaster)infredRaster;
                    pansharpenFilter.PanImage = (IRaster)panRaster;
                    pansharpenFilter.PansharpeningType = esriPansharpeningType.esriPansharpeningESRI;
                    pansharpenFilter.PutWeights(0.166, 0.167, 0.167, 0.5);

                    //将全色锐化过滤器设置于多光谱栅格对象上
                    IPixelOperation pixelOp = (IPixelOperation)multiRaster;
                    pixelOp.PixelFilter = (IPixelFilter)pansharpenFilter;

                    //存储裁剪结果栅格图像
                    IWorkspaceFactory wsf = new RasterWorkspaceFactoryClass();
                    IWorkspace rstWS = wsf.OpenFromFile(@"C:\Users\31401\Desktop\files\results", 0);                    //保存输出
                    ISaveAs saveas = (ISaveAs)multiRaster;
                    saveas.SaveAs("panSharpen_result.tif", rstWS, "TIFF");
                    //加载显示裁剪结果图像
                    IRaster2 outraster = multiRaster as IRaster2;
                    IRasterLayer rasterLayer = new RasterLayerClass();
                    rasterLayer.CreateFromDataset(outraster.RasterDataset);
                    rasterLayer.Name = cmb_PanLayer.SelectedItem.ToString() + "融合" + cmb_MultiLayer.SelectedItem.ToString();
                    ILayer iLayer = rasterLayer as ILayer;
                    axMapControl1.Map.AddLayer(iLayer);
                    //更新控件           
                    axMapControl1.ActiveView.Refresh();
                    axTOCControl1.Update();
                    iniCmbItems();
                }
            }
            catch (System.Exception ex) //捕获异常，输出异常信息   
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //图像镶嵌，对一个栅格目录里的所有山栅格进行镶嵌处理
        private void btn_Mosaic_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor; //单击时修改鼠标光标形状     
            try
            {
                //定义待用的字符串变量，表示元栅格数据文件夹路径、结果栅格数据保存路径
                //特别地，先创建一个个人地理数据库，再在其中创建一个栅格目录
                //string inputFolder = @"C:\Users\31401\Desktop\files\遥感数据库技术\programming-05\图像镶嵌数据\mosaic";
                string inputCatalog = cmb_MosaicCatalog.SelectedItem.ToString();
                string outputFolder = @"C:\Users\31401\Desktop\files\遥感数据库技术\programming-05\图像镶嵌数据\results";
                string outputName = "mosaic.tif";
                string tempRasterCatalog = "temp_rc";
                string tempPGDB = "temp.mdb";
                string tempPGDBPath = outputFolder + "\\" + tempPGDB;
                string tempRasterCatalogPath = tempPGDBPath + "\\" + tempRasterCatalog;
                //输出文件夹存在与否的判断，避免重复生成相同文件而报错
                if (Directory.Exists(outputFolder) == false)//如果不存在就创建file文件夹
                {
                    Directory.CreateDirectory(outputFolder);
                }
                else 
                {
                    Directory.Delete(outputFolder, true);//删除文件夹以及文件夹中的子目录，文件   
                    Directory.CreateDirectory(outputFolder);
                }
     
                ////使用geoprocessor来创建地理数据库、栅格目录，以及加载目录到栅格目录中
                //Geoprocessor geoprocessor = new Geoprocessor();
                ////在临时文件中创建个人地理数据库
                //CreatePersonalGDB createPersonalGDB = new CreatePersonalGDB();
                //createPersonalGDB.out_folder_path = outputFolder;
                //createPersonalGDB.out_name = tempPGDB;
                ////调用geoprocessing的excute方法执行创建个人地理数据库
                //geoprocessor.Execute(createPersonalGDB, null);

                ////在新创建的个人地理数据库中创建一个非托管的栅格目录
                //CreateRasterCatalog createRasterCatalog = new CreateRasterCatalog();
                //createRasterCatalog.out_path = tempPGDBPath;
                //createRasterCatalog.out_name = tempRasterCatalog;
                //createRasterCatalog.raster_management_type = "unmanaged";
                ////调用geoprocessor的excute方法执行创建栅格目录
                //geoprocessor.Execute(createRasterCatalog, null);

                ////把用于镶嵌的原始栅格数据加载到新创建的非托管的栅格目录中
                //WorkspaceToRasterCatalog wsToRasterCatalog = new WorkspaceToRasterCatalog();
                ////设置加载栅格数据的栅格目录路径、栅格数据路径、加载的类型（是否包含子文件夹）
                //wsToRasterCatalog.in_raster_catalog = tempRasterCatalogPath;
                //wsToRasterCatalog.in_workspace = inputFolder;
                //wsToRasterCatalog.include_subdirectories = "INCLUDE_SUBDIRECTORIES";
                ////调用geoprocessor的excute方法执行创建栅格目录
                //geoprocessor.Execute(wsToRasterCatalog, null);
                
                ////打开刚刚创建的个人地理数据库，以获取栅格目录对象
                //Type t = Type.GetTypeFromProgID("esriDataSourcesGDB.AccessWorkspaceFactory");
                //System.Object obj = Activator.CreateInstance(t);
                //IWorkspaceFactory2 workspaceFactory = obj as IWorkspaceFactory2;
                //IRasterWorkspaceEx rasterWorkspaceEx = (IRasterWorkspaceEx)workspaceFactory.OpenFromFile(tempPGDBPath, 0);
                //IRasterCatalog rasterCatalog = rasterWorkspaceEx.OpenRasterCatalog(tempRasterCatalog);
                
                //接口转换IRsterWorkspaceEx
                IRasterWorkspaceEx rasterWorkspaceEx = workspace as IRasterWorkspaceEx;
                //获取栅格目录
                IRasterCatalog rasterCatalog = rasterWorkspaceEx.OpenRasterCatalog(inputCatalog);
                //把栅格目录中所有栅格图像镶嵌成为一个栅格图像/栅格数据集
                IMosaicRaster mosaicRaster = new MosaicRasterClass();
                mosaicRaster.RasterCatalog = rasterCatalog;

                //设置镶嵌的颜色映射表模式和像素值运算类型
                mosaicRaster.MosaicColormapMode = rstMosaicColormapMode.MM_MATCH;
                mosaicRaster.MosaicOperatorType = rstMosaicOperatorType.MT_LAST;

                //打开输出结果数据集保存路径的工作空间
                IWorkspaceFactory workspaceFactory2 = new RasterWorkspaceFactoryClass();
                IWorkspace rstWorkspace = workspaceFactory2.OpenFromFile(outputFolder, 0);
                //保存输出
                ISaveAs saveas = (ISaveAs)mosaicRaster;
                saveas.SaveAs(outputName, rstWorkspace, "TIFF");
                //加载显示裁剪结果图像
                IRaster outraster = mosaicRaster as IRaster;
                IRaster2 outraster2 = (IRaster2)outraster;
                IRasterLayer rasterLayer = new RasterLayerClass();

                rasterLayer.CreateFromFilePath(outputFolder+"\\"+outputName);
                rasterLayer.Name = "镶嵌结果" ;
                ILayer iLayer = rasterLayer as ILayer;
                axMapControl1.Map.AddLayer(iLayer);
                //更新控件           
                axMapControl1.ActiveView.Refresh();
                axTOCControl1.Update();
                iniCmbItems();

            }
            catch (System.Exception ex) //捕获异常，输出异常信息   
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }   
            finally //最后再将鼠标光标设置成默认形状    
            {
                this.Cursor = Cursors.Default;
            }
        }
        //图像变换
        private void btn_Transform_Click(object sender, EventArgs e)
        {
            try
            {
                //获取选中的图层
                int indexLayer = cmb_TransformLayer.SelectedIndex;
                ILayer layer = this.axMapControl1.get_Layer(indexLayer);
                double angle = double.Parse(txb_Transformangle.Text);
                if (layer is IRasterLayer)
                {
                    //转换成IRasterLayer接口  
                    IRasterLayer rstLayer = layer as IRasterLayer;
                    //获取图层raster并转换成IRaster2接口      
                    IRaster2 raster2 = rstLayer.Raster as IRaster2;
                    //获取该raster的RasterDataset         
                    IRasterDataset rstDataset = raster2.RasterDataset;
                    //转换IGeodataset接口         
                    IGeoDataset geoDataset = rstDataset as IGeoDataset;

                    //创建栅格图像变换操作接口的对象
                    ITransformationOp transop = new RasterTransformationOpClass();
                    //定义输出地理数据集的对象
                    IGeoDataset outdataset;

                    switch (cmb_TransformMethod.SelectedIndex)
                    {
                        case 0: //翻转
                            outdataset = transop.Flip(geoDataset);
                            break;
                        case 1: //镜像
                            outdataset = transop.Mirror(geoDataset);
                            break;
                        case 2: //裁剪
                            fClip = true;
                            MessageBox.Show("请使用鼠标在图上绘制", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        case 3: //旋转
                            object missing = Type.Missing;
                            outdataset = transop.Rotate(geoDataset, esriGeoAnalysisResampleEnum.esriGeoAnalysisResampleNearest,
                                angle, ref missing);
                            break;
                        default:
                            return;
                    }
                    //通过图像变换结果获取栅格数据集，进而创建栅格图层加以显示
                    IRasterDataset rasterdataset;
                    //获取结果数据集
                    rasterdataset = (IRasterDataset)outdataset;
                    //将结果保存到RasterLayer
                    IRasterLayer resLayer = new RasterLayerClass();
                    resLayer.CreateFromDataset(rasterdataset);
                    resLayer.Name = cmb_TransformLayer.SelectedItem.ToString() +"的"+ cmb_TransformMethod.SelectedItem.ToString() + "结果";
                    ILayer iLayer = resLayer as ILayer;
                    axMapControl1.Map.AddLayer(iLayer);
                    //更新控件           
                    axMapControl1.ActiveView.Refresh();
                    axTOCControl1.Update();
                    //更新combobox里面的选项（图层和波段）
                    iniCmbItems();
                }
            }
            catch (System.Exception ex) //捕获异常，输出异常信息   
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        //判断处于裁剪状态时，鼠标在地图上的按下时间即为绘制裁剪矩形响应
        private void axMapControl1_OnMouseDown(object sender, IMapControlEvents2_OnMouseDownEvent e)
        {
            try
            {
                //如果处于裁剪状态
                if(fClip == true)
                {
                    //获取屏幕显示有关的接口对象（与ActiveView有关）
                    IScreenDisplay screenDisplay = axMapControl1.ActiveView.ScreenDisplay;
                    //rubberband橡皮筋接口用来绘制图形
                    IRubberBand rubberBand = new RubberEnvelopeClass();
                    //获取绘制的几何图形IGeometry
                    IGeometry geometry = rubberBand.TrackNew(screenDisplay, null);
                    //IEnvolope接口获取几何图形的包络范围矩形
                    IEnvelope cutEnv = null;
                    cutEnv = geometry.Envelope;
                    //获取选中的图层
                    int indexLayer = cmb_TransformLayer.SelectedIndex;
                    ILayer layer = this.axMapControl1.get_Layer(indexLayer);
                    if (layer is IRasterLayer)
                    {
                        //转换成IRasterLayer接口  
                        IRasterLayer rstLayer = layer as IRasterLayer;
                        //获取图层raster并转换成IRaster2接口      
                        IGeoDataset geoDataset = rstLayer.Raster as IGeoDataset;
                        //创建栅格图像变换操作接口的对象
                        ITransformationOp transop = new RasterTransformationOpClass();
                        //定义输出地理数据集的对象
                        IGeoDataset outdataset;
                        //裁剪
                        outdataset = transop.Clip(geoDataset, cutEnv);

                        //通过图像变换结果获取栅格数据集，进而创建栅格图层加以显示
                        IRasterDataset rasterdataset;
                        //获取结果数据集
                        rasterdataset = (IRasterDataset)outdataset;
                        //将结果保存到RasterLayer
                        IRasterLayer resLayer = new RasterLayerClass();
                        resLayer.CreateFromDataset(rasterdataset);
                        resLayer.Name = cmb_TransformLayer.SelectedItem.ToString() + "的裁剪结果";
                        ILayer iLayer = resLayer as ILayer;
                        axMapControl1.Map.AddLayer(iLayer);
                        //更新控件           
                        axMapControl1.ActiveView.Refresh();
                        axTOCControl1.Update();
                        //更新combobox里面的选项（图层和波段）
                        iniCmbItems();
                    }
                    fClip = false;
                }
                else if (fExtraction == true)
                {
                    //获取选中的图层
                    int indexLayer = cmb_ExtractionLayer.SelectedIndex;
                    ILayer layer = this.axMapControl1.get_Layer(indexLayer);
                    if (layer is IRasterLayer)
                    {
                        //转换成IRasterLayer接口  
                        IRasterLayer rstLayer = layer as IRasterLayer;
                        //获取图层raster并转换成IRaster2接口      
                        IGeoDataset geoDataset = rstLayer.Raster as IGeoDataset;
                        //创建栅格图像变换操作接口的对象
                        ITransformationOp transop = new RasterTransformationOpClass();
                        //定义输出地理数据集的对象
                        IGeoDataset outdataset;
                        //鼠标点击屏幕绘制多边形
                        IPolygon pPolygon = axMapControl1.TrackPolygon() as IPolygon;
                        //创建栅格数据裁剪分析操作相关的类对象
                        IExtractionOp pExtrationOP = new RasterExtractionOpClass();
                        //执行剪切操作
                        outdataset = pExtrationOP.Polygon(geoDataset, pPolygon, true);

                        //将结果保存到RasterLayer
                        IRasterLayer resLayer = new RasterLayerClass();
                        resLayer.CreateFromRaster((IRaster)outdataset);
                        resLayer.Name = cmb_ExtractionLayer.SelectedItem.ToString() + "的裁剪结果";
                        ILayer iLayer = resLayer as ILayer;
                        axMapControl1.Map.AddLayer(iLayer);
                        //更新控件           
                        axMapControl1.ActiveView.Refresh();
                        axTOCControl1.Update();
                        //更新combobox里面的选项（图层和波段）
                        iniCmbItems();
                    }
                    fExtraction = false;
                }
                else if (fLineofsight == true)
                {
                    //获取选中的图层
                    int indexLayer = cmb_LineOfSightDEM.SelectedIndex;
                    ILayer layer = this.axMapControl1.get_Layer(indexLayer);
                    if (layer is IRasterLayer)
                    {
                        //转换成IRasterLayer接口  
                        IRasterLayer rstLayer = layer as IRasterLayer;
                        //创建栅格表面分析的对象并设置处理栅格数据对象
                        IRasterSurface pRasterSurface = new RasterSurfaceClass();
                        pRasterSurface.PutRaster(rstLayer.Raster, 0);
                        //接口转换ISurface
                        ISurface pSurface = pRasterSurface as ISurface;
                        //在地图上绘制实线，获得几何对象
                        IPolyline pPolyline = axMapControl1.TrackLine() as IPolyline;
                        IPoint pPoint = null;
                        IPolyline pVPolyline = null;
                        IPolyline pInPolyline = null;
                        //设置参数
                        object pRef = 0.13;
                        bool pBool = true;
                        //获取DEM高程
                        double pZ1 = pSurface.GetElevation(pPolyline.FromPoint);
                        double pZ2 = pSurface.GetElevation(pPolyline.ToPoint);
                        //创建IPoint对象，赋值高程和xv值
                        IPoint pPoint1 = pPolyline.FromPoint;
                        pPoint1.Z = pZ1;
                        IPoint pPoint2 = pPolyline.ToPoint;
                        pPoint2.Z = pZ2;
                        //调用ISurface接口的getlineofsight方法得到通视范围
                        pSurface.GetLineOfSight(pPoint1, pPoint2, out pPoint, out pVPolyline,
                            out pInPolyline, out pBool, false, false, ref pRef);
                        if (pVPolyline != null)
                        {
                            //如果可视范围不为null， 则进行渲染和显示
                            IElement pLineElementV = new LineElementClass();
                            pLineElementV.Geometry = pVPolyline;
                            ILineSymbol pLineSymbolV = new SimpleLineSymbolClass();
                            pLineSymbolV.Width = 2;
                            IRgbColor pColorV = new RgbColorClass();
                            pColorV.Green = 255;
                            pLineSymbolV.Color = pColorV;
                            ILineElement pLineV = pLineElementV as ILineElement;
                            pLineV.Symbol = pLineSymbolV;
                            axMapControl1.ActiveView.GraphicsContainer.AddElement(pLineElementV, 0);
                        }
                        if (pInPolyline != null)
                        {
                            //如果可视范围不为null， 则进行渲染和显示
                            IElement pLineElementIn = new LineElementClass();
                            pLineElementIn.Geometry = pInPolyline;
                            ILineSymbol pLineSymbolIn = new SimpleLineSymbolClass();
                            pLineSymbolIn.Width = 5;
                            IRgbColor pColorIn = new RgbColorClass();
                            pColorIn.Red = 255;
                            pLineSymbolIn.Color = pColorIn;
                            ILineElement pLineIn = pLineElementIn as ILineElement;
                            pLineIn.Symbol = pLineSymbolIn;
                            axMapControl1.ActiveView.GraphicsContainer.AddElement(pLineElementIn, 1);
                        }
                        axMapControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
                    }
                    fLineofsight = false;
                }
                else if (fVisibility == true)
                {
                    //获取选中的图层
                    int indexLayer = cmb_VisibilityDEM.SelectedIndex;
                    ILayer layer = this.axMapControl1.get_Layer(indexLayer);
                    if (layer is IRasterLayer)
                    {
                        //转换成IRasterLayer接口  
                        IRasterLayer rstLayer = layer as IRasterLayer;
                        //获取图层raster并转换成IRaster2接口      
                        IRaster2 raster2 = rstLayer.Raster as IRaster2;
                        //获取该raster的RasterDataset         
                        IRasterDataset rstDataset = raster2.RasterDataset;
                        //转换IGeodataset接口         
                        IGeoDataset geoDataset = rstDataset as IGeoDataset;
                        //工作空间
                        IFeatureWorkspace fcw = (IFeatureWorkspace)workspace;
                        //创建要素类的字段集合
                        IFields fields = new FieldsClass();
                        IFieldsEdit fieldsEdit = fields as IFieldsEdit;

                        //添加oid字段，注意字段type
                        IField oldfield = new FieldClass();
                        IFieldEdit oldfieldEdit = oldfield as IFieldEdit;
                        oldfieldEdit.Name_2 = "OID";
                        oldfieldEdit.Type_2 = esriFieldType.esriFieldTypeOID;
                        fieldsEdit.AddField(oldfield);

                        //IGrometryDef和IGeometryDefEdit接口定义和编辑几何字段
                        IGeometryDef geometryDef = new GeometryDefClass();
                        IGeometryDefEdit geometryDefEdit = geometryDef as IGeometryDefEdit;
                        geometryDefEdit.GeometryType_2 = esriGeometryType.esriGeometryPoint;
                        ISpatialReferenceFactory spatialReferenceFactory = new SpatialReferenceEnvironmentClass();
                        ISpatialReference spatialReference = spatialReferenceFactory.CreateProjectedCoordinateSystem((int)esriSRProjCSType.esriSRProjCS_NAD1983UTM_20N);
                        ISpatialReferenceResolution spatialReferenceResolution = (ISpatialReferenceResolution)spatialReference;
                        spatialReferenceResolution.ConstructFromHorizon();
                        spatialReferenceResolution.SetDefaultXYResolution();
                        ISpatialReferenceTolerance spatialReferenceTolerance = (ISpatialReferenceTolerance)spatialReference;
                        spatialReferenceTolerance.SetDefaultXYTolerance();
                        geometryDefEdit.SpatialReference_2 = spatialReference;

                        //添加几何字段
                        IField geometryField = new FieldClass();
                        IFieldEdit geometryFieldEdit = (IFieldEdit)geometryField;
                        geometryFieldEdit.Name_2 = "Shape";
                        geometryFieldEdit.Type_2 = esriFieldType.esriFieldTypeGeometry;
                        geometryFieldEdit.GeometryDef_2 = geometryDef;
                        fieldsEdit.AddField(geometryField);
                        //添加name字段，注意字段type
                        IField namefield = new FieldClass();
                        IFieldEdit namefieldEdit = namefield as IFieldEdit;
                        namefieldEdit.Name_2 = "NAME";
                        namefieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
                        namefieldEdit.Length_2 = 20;
                        fieldsEdit.AddField(namefield);
                        //利用IFieldChecker创建验证字段集合
                        IFieldChecker fieldChecker = new FieldCheckerClass();
                        IEnumFieldError enumFieldError = null;
                        IFields validateFields = null;
                        fieldChecker.ValidateWorkspace = (IWorkspace)fcw;
                        fieldChecker.Validate(fields, out enumFieldError, out validateFields);
                        //创建要素类
                        IFeatureClass featureClass = fcw.CreateFeatureClass("visibility_featureclass1363", validateFields, 
                            null, null, esriFeatureType.esriFTSimple, "Shape", "");

                        //鼠标点击屏幕绘制点
                        IPoint pt = axMapControl1.ToMapPoint(e.x, e.y);
                        //创建要素
                        IFeature feature = featureClass.CreateFeature();
                        feature.Shape = pt;
                        //应用适当的子类型到要素中
                        ISubtypes subtypes = (ISubtypes)featureClass;
                        IRowSubtypes rowSubtypes = (IRowSubtypes)feature;
                        if (subtypes.HasSubtype)
                        {
                            rowSubtypes.SubtypeCode = 3;
                        }
                        //初始化要素的所有默认设置
                        rowSubtypes.InitDefaultValues();
                        //实现保存
                        feature.Store();
                        //ifeatureclass转化igeodataset
                        IGeoDataset geoDataset2 = (IGeoDataset)featureClass;
                        //创建栅格数据表面分析操作相关的类对象
                        ISurfaceOp surfaceOp = new RasterSurfaceOpClass();
                        //执行视域分析操作得到结果数据集
                        IGeoDataset pGeoOutput = surfaceOp.Visibility(geoDataset, geoDataset2, esriGeoAnalysisVisibilityEnum.esriGeoAnalysisVisibilityObservers);                        //删除刚刚创建的要素类
                        IDataset dataset = featureClass as IDataset;
                        dataset.Delete();
                        //在栅格图层中加载显示
                        IRasterLayer rasterLayer = new RasterLayerClass();
                        rasterLayer.CreateFromRaster((IRaster)pGeoOutput);
                        rasterLayer.Name = cmb_VisibilityDEM.SelectedItem.ToString() + "视域分析";
                        //将栅格图像加载到map中
                        if (rasterLayer != null)
                        {
                            //更新控件
                            ILayer iLayer = rasterLayer as ILayer;
                            axMapControl1.Map.AddLayer(iLayer);
                            axMapControl1.ActiveView.Extent = iLayer.AreaOfInterest;
                            axMapControl1.ActiveView.Refresh();
                            axTOCControl1.Update();
                            //更新combobox里面的选项（图层和波段）
                            iniCmbItems();
                        }
 
                    }
                    fVisibility = false;
                }
            }
            catch (System.Exception ex) //捕获异常，输出异常信息   
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }  
        }
        //图像滤波
        private void btn_Filter_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor; //单击时修改鼠标光标形状
            try
            {
                //获取选中的图层
                int indexLayer = cmb_TransformLayer.SelectedIndex;
                ILayer layer = this.axMapControl1.get_Layer(indexLayer);
                if (layer is IRasterLayer)
                {
                    //转换成IRasterLayer接口  
                    IRasterLayer rstLayer = layer as IRasterLayer;
                    //获取图层raster并转换成IRaster2接口      
                    IRaster raster = rstLayer.Raster;

                    // 创建Function Arguments对象
                    IConvolutionFunctionArguments rasterFunctionArguments = (IConvolutionFunctionArguments)new ConvolutionFunctionArguments();
                    // 设置卷积运算模板方法
                    int filterMethodIndex = cmb_FilterMethod.SelectedIndex;
                    rasterFunctionArguments.Type = (esriRasterFilterTypeEnum)filterMethodIndex;
                      
                    // 设置输入栅格数据
                    rasterFunctionArguments.Raster = raster;
                    // 创建Raster Function对象
                    IRasterFunction rasterFunction = new ConvolutionFunction();
                    // 创建Function Raster Dataset对象
                    IFunctionRasterDataset functionRasterDataset = new FunctionRasterDataset();
                    // 创建the Function Raster Dataset的名称对象
                    IFunctionRasterDatasetName functionRasterDatasetName =
                    (IFunctionRasterDatasetName)new FunctionRasterDatasetName();
                    // 设置输出路径
                    functionRasterDatasetName.FullName = @"C:\Users\31401\Desktop\files\遥感数据库技术\programming-05\results\convolution.tif";
                    functionRasterDataset.FullName = (IName)functionRasterDatasetName;
                    // 用Raster Function和Function Arguments.初始化新的Function Raster Dataset
                    functionRasterDataset.Init(rasterFunction, rasterFunctionArguments);
                    
                    //加载显示滤波结果图像
                    IRasterLayer rasterLayer = new RasterLayerClass();
                    rasterLayer.CreateFromDataset(functionRasterDataset as IRasterDataset);
                    rasterLayer.Name = cmb_TransformLayer.SelectedItem.ToString() + cmb_FilterMethod.SelectedItem.ToString();
                    ILayer iLayer = rasterLayer as ILayer;
                    axMapControl1.Map.AddLayer(iLayer);
                    //更新控件           
                    axMapControl1.ActiveView.Refresh();
                    axTOCControl1.Update();
                    iniCmbItems();
                }
            }
            catch (System.Exception ex) //捕获异常，输出异常信息   
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }   
            finally //最后再将鼠标光标设置成默认形状    
            {
                this.Cursor = Cursors.Default;
            }
        }
        //山体阴影：给定太阳高度角和方位角，模拟地表光照值
        private void btn_HillShade_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor; //单击时修改鼠标光标形状
            try
            {
                //获取选中的DEM图层
                int indexLayer = cmb_HillshadeDEM.SelectedIndex;
                ILayer layer = this.axMapControl1.get_Layer(indexLayer);
                if (layer is IRasterLayer)
                {
                    //转换成IRasterLayer接口  
                    IRasterLayer rstLayer = layer as IRasterLayer;
                    //获取图层raster并转换成IRaster2接口      
                    IRaster raster = rstLayer.Raster;

                    //设定山体阴影参数
                    double altitude = 45;//太阳高度角
                    double azimuth = 215;//地平静线角度
                    double zfactor = (1/111111.0);//高度因子
                    // 创建Function Arguments对象
                    IHillshadeFunctionArguments rasterFunctionArguments = (IHillshadeFunctionArguments)new HillshadeFunctionArguments();
                    // 设置山体阴影运算模板方法
                    rasterFunctionArguments.DEM = raster;
                    rasterFunctionArguments.Altitude = altitude;
                    rasterFunctionArguments.Azimuth = azimuth;
                    rasterFunctionArguments.ZFactor = zfactor;

                    // 创建Raster Function对象
                    IRasterFunction rasterFunction = new HillshadeFunction();
                    // 创建Function Raster Dataset对象
                    IFunctionRasterDataset functionRasterDataset = new FunctionRasterDataset();
                    // 创建the Function Raster Dataset的名称对象
                    IFunctionRasterDatasetName functionRasterDatasetName =
                    (IFunctionRasterDatasetName)new FunctionRasterDatasetName();
                    // 设置输出路径
                    functionRasterDatasetName.FullName = @"C:\Users\31401\Desktop\files\遥感数据库技术\programming-06空间分析\results\hillshade.tif";
                    functionRasterDataset.FullName = (IName)functionRasterDatasetName;
                    // 用Raster Function和Function Arguments.初始化新的Function Raster Dataset
                    functionRasterDataset.Init(rasterFunction, rasterFunctionArguments);

                    //加载显示山体阴影结果图像
                    IRasterLayer rasterLayer = new RasterLayerClass();
                    rasterLayer.CreateFromDataset(functionRasterDataset as IRasterDataset);
                    rasterLayer.Name = cmb_HillshadeDEM.SelectedItem.ToString() + "山体阴影";
                    ILayer iLayer = rasterLayer as ILayer;
                    axMapControl1.Map.AddLayer(iLayer);
                    //更新控件           
                    axMapControl1.ActiveView.Refresh();
                    axTOCControl1.Update();
                    iniCmbItems();
                }
            }
            catch (System.Exception ex) //捕获异常，输出异常信息   
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }   
            finally //最后再将鼠标光标设置成默认形状    
            {
                this.Cursor = Cursors.Default;
            }
        }
        //坡度分析
        private void btn_Slope_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor; //单击时修改鼠标光标形状
            try
            {
                //获取选中的DEM图层
                int indexLayer = cmb_SlopeDEM.SelectedIndex;
                ILayer layer = this.axMapControl1.get_Layer(indexLayer);
                if (layer is IRasterLayer)
                {
                    //转换成IRasterLayer接口  
                    IRasterLayer rstLayer = layer as IRasterLayer;
                    //获取图层raster并转换成IRaster2接口      
                    IRaster raster = rstLayer.Raster;

                    //设定山体阴影参数
                    double zfactor = (1 / 111111.0);//高度因子
                    // 创建Function Arguments对象
                    ISlopeFunctionArguments rasterFunctionArguments = (ISlopeFunctionArguments)new SlopeFunctionArguments();
                    // 设置坡度分析运算模板方法
                    rasterFunctionArguments.DEM = raster;
                    rasterFunctionArguments.ZFactor = zfactor;

                    // 创建Raster Function对象
                    IRasterFunction rasterFunction = new SlopeFunction();
                    // 创建Function Raster Dataset对象
                    IFunctionRasterDataset functionRasterDataset = new FunctionRasterDataset();
                    // 创建the Function Raster Dataset的名称对象
                    IFunctionRasterDatasetName functionRasterDatasetName =
                    (IFunctionRasterDatasetName)new FunctionRasterDatasetName();
                    // 设置输出路径
                    functionRasterDatasetName.FullName = @"C:\Users\31401\Desktop\files\遥感数据库技术\programming-06空间分析\results\slope.tif";
                    functionRasterDataset.FullName = (IName)functionRasterDatasetName;
                    // 用Raster Function和Function Arguments.初始化新的Function Raster Dataset
                    functionRasterDataset.Init(rasterFunction, rasterFunctionArguments);

                    //加载显示山体阴影结果图像
                    IRasterLayer rasterLayer = new RasterLayerClass();
                    rasterLayer.CreateFromDataset(functionRasterDataset as IRasterDataset);
                    rasterLayer.Name = cmb_SlopeDEM.SelectedItem.ToString() + "坡度分析";
                    ILayer iLayer = rasterLayer as ILayer;
                    axMapControl1.Map.AddLayer(iLayer);
                    //更新控件           
                    axMapControl1.ActiveView.Refresh();
                    axTOCControl1.Update();
                    iniCmbItems();
                }
            }
            catch (System.Exception ex) //捕获异常，输出异常信息   
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }   
            finally //最后再将鼠标光标设置成默认形状    
            {
                this.Cursor = Cursors.Default;
            }
        }
        //坡向分析，不存在aspectfunctionargument
        private void btn_Aspect_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor; //单击时修改鼠标光标形状
            try
            {
                //获取选中的DEM图层
                int indexLayer = cmb_AspectDEM.SelectedIndex;
                ILayer layer = this.axMapControl1.get_Layer(indexLayer);
                if (layer is IRasterLayer)
                {
                    //转换成IRasterLayer接口  
                    IRasterLayer rstLayer = layer as IRasterLayer;
                    //获取图层raster并转换成IRaster2接口      
                    IRaster raster = rstLayer.Raster;

                    // 创建Raster Function对象
                    IRasterFunction rasterFunction = new AspectFunction();
                    // 创建Function Raster Dataset对象
                    IFunctionRasterDataset functionRasterDataset = new FunctionRasterDataset();
                    // 创建the Function Raster Dataset的名称对象
                    IFunctionRasterDatasetName functionRasterDatasetName =
                    (IFunctionRasterDatasetName)new FunctionRasterDatasetName();
                    // 设置输出路径
                    functionRasterDatasetName.FullName = @"C:\Users\31401\Desktop\files\遥感数据库技术\programming-06空间分析\results\aspect.tif";
                    functionRasterDataset.FullName = (IName)functionRasterDatasetName;
                    // 用Raster Function和Function Arguments.初始化新的Function Raster Dataset
                    functionRasterDataset.Init(rasterFunction, raster);

                    //加载显示山体阴影结果图像
                    IRasterLayer rasterLayer = new RasterLayerClass();
                    rasterLayer.CreateFromDataset(functionRasterDataset as IRasterDataset);
                    rasterLayer.Name = cmb_AspectDEM.SelectedItem.ToString() + "坡向分析";
                    ILayer iLayer = rasterLayer as ILayer;
                    axMapControl1.Map.AddLayer(iLayer);
                    //更新控件           
                    axMapControl1.ActiveView.Refresh();
                    axTOCControl1.Update();
                    iniCmbItems();
                }
            }
            catch (System.Exception ex) //捕获异常，输出异常信息   
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }   
            finally //最后再将鼠标光标设置成默认形状    
            {
                this.Cursor = Cursors.Default;
            }
        }
        //领域分析
        private void btn_Neighborhood_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor; //单击时修改鼠标光标形状
            try
            {
                //获取选中的DEM图层
                int indexLayer = cmb_NeighborhoodLayer.SelectedIndex;
                ILayer layer = this.axMapControl1.get_Layer(indexLayer);
                if (layer is IRasterLayer)
                {
                    //转换成IRasterLayer接口  
                    IRasterLayer rstLayer = layer as IRasterLayer;
                    //获取图层raster并转换成IRaster2接口      
                    IRaster2 raster2 = rstLayer.Raster as IRaster2;
                    //获取该raster的RasterDataset         
                    IRasterDataset rstDataset = raster2.RasterDataset;
                    //转换IGeodataset接口         
                    IGeoDataset geoDataset = rstDataset as IGeoDataset;
                    //创建栅格数据领域分析操作相关的类对象
                    INeighborhoodOp neighborhoodOP = new RasterNeighborhoodOpClass();
                    //创建栅格数据领域分析参数对象
                    IRasterNeighborhood pRasterNeighborhood = new RasterNeighborhoodClass();
                    //设置矩形领域分析范围
                    pRasterNeighborhood.SetRectangle(3, 3, esriGeoAnalysisUnitsEnum.esriUnitsCells);

                    IGeoDataset outdataset = null;
                    //获取分析方法索引值
                    int method = cmb_NeighborhoodMethod.SelectedIndex;
                    if (method < 11)//采用块领域分析
                    {
                        outdataset = neighborhoodOP.BlockStatistics(geoDataset, (esriGeoAnalysisStatisticsEnum)method, pRasterNeighborhood, true);
                    }
                    //else if (method >= 11 && method < 13)//采用焦点分析
                    else
                    {
                        outdataset = neighborhoodOP.Filter(geoDataset, (esriGeoAnalysisFilterEnum)(method-11), true);
                    }

                    //在栅格图层中加载显示
                    IRasterLayer rasterLayer = new RasterLayerClass();
                    rasterLayer.CreateFromRaster((IRaster)outdataset);
                    rasterLayer.Name = cmb_NeighborhoodLayer.SelectedItem.ToString() + cmb_NeighborhoodMethod.SelectedItem.ToString();
                    //将栅格图像加载到map中
                    if (rasterLayer != null)
                    {
                        //更新控件
                        ILayer iLayer = rasterLayer as ILayer;
                        axMapControl1.Map.AddLayer(iLayer);
                        axMapControl1.ActiveView.Extent = iLayer.AreaOfInterest;
                        axMapControl1.ActiveView.Refresh();
                        axTOCControl1.Update();
                        //更新combobox里面的选项（图层和波段）
                        iniCmbItems();
                    }
                }
            }
            catch (System.Exception ex) //捕获异常，输出异常信息   
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }   
            finally //最后再将鼠标光标设置成默认形状    
            {
                this.Cursor = Cursors.Default;
            }
        }
        //裁剪分析，修改fExtraction，打开鼠标点击绘制多边形剪切的功能
        private void btn_Extraction_Click(object sender, EventArgs e)
        {
            fExtraction = true;
            MessageBox.Show("请使用鼠标在图上绘制", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }
        //通视分析
        private void btn_LineOfSight_Click(object sender, EventArgs e)
        {
            fLineofsight = true;
            MessageBox.Show("请使用鼠标在图上绘制", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }
        //视域分析
        private void btn_Visibility_Click(object sender, EventArgs e)
        {
            fVisibility = true;
            MessageBox.Show("请使用鼠标在图上绘制", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

    }
}