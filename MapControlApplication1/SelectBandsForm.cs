using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.DataSourcesRaster;

namespace MapControlApplication1
{
    public partial class SelectBandsForm : Form
    {
        //存储要绘制对比直方图的栅格图层对象
        private IRasterLayer m_rstlayer;
        //存储栅格图层的波段总数
        private int m_bandnum;
        //存储栅格图层的选择波段
        private int[] m_selband;

        public SelectBandsForm(IRasterLayer rstLayer)
        {
            //初始化窗体的基本组件
            InitializeComponent();
            //波段选择窗体的实现
            m_rstlayer = rstLayer;
            //从IRaster获取波段
            IRaster2 raster2 = rstLayer.Raster as IRaster2;
            IRasterDataset rasterDataset = raster2.RasterDataset;
            IRasterBandCollection rasterBandCollection = rasterDataset as IRasterBandCollection;
            m_bandnum = rasterBandCollection.Count;
            int bandindex;
            //将波段全部加入CheckListBox里面
            for (int i = 0; i < m_bandnum; i++)
            {
                bandindex = i + 1;
                cklb_CompareHistogram.Items.Add("波段" + bandindex);
            }
        }

        //点击绘制对比直方图，进行多直方图对比绘制
        private void btn_DrawCompareHistogram_Click(object sender, EventArgs e)
        {
            //检测有多少选择的波段
            int k = 0;
            for (int i = 0; i < cklb_CompareHistogram.Items.Count; i++)
            {
                if (cklb_CompareHistogram.GetItemChecked(i))
                {
                    k++;
                }
            }

            m_selband = new int[k];
            //把选择的波段索引存储在证书数组中
            int j = 0;
            for(int i = 0; i < cklb_CompareHistogram.Items.Count; i++)
            {
                if (cklb_CompareHistogram.GetItemChecked(i))
                {
                    m_selband[j] = i;
                    j++;
                }
            }
            //创建对比直方图绘制窗体对象，并展示出来
            HistogramCompareForm HistogramCompare = new HistogramCompareForm(m_rstlayer, m_selband);
            HistogramCompare.ShowDialog();
        }
    }
}
