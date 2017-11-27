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
    public partial class HistogramCompareForm : Form
    {
        //存储要绘制对比直方图的栅格图层对象
        private IRasterLayer m_rstlayer;
        //存储选择的波段（数组）
        private int[] m_selband;

        public HistogramCompareForm(IRasterLayer rstlayer, int[] selband)
        {
            //初始化窗体的基本组件
            InitializeComponent();
            //成员变量赋值
            m_rstlayer = rstlayer;
            m_selband = selband;
        }
        
        //对比直方图窗体的绘制paint函数
        private void HistogramCompareForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;// 获取Graphics对象
            Pen pen = new Pen(Brushes.Black, 1); //实例化细度为1的黑色画笔
            Pen pennoteline = new Pen(Brushes.Red, 1);   //实例化细度为1的红色画笔，用来做线注记

            // 绘制坐标轴
            g.DrawLine(pen, 50, 240, 320, 240);
            g.DrawLine(pen, 320, 240, 316, 236);
            g.DrawLine(pen, 320, 240, 316, 244);
            g.DrawLine(pen, 50, 240, 50, 30);
            g.DrawLine(pen, 50, 30, 46, 34);
            g.DrawLine(pen, 50, 30, 54, 34);
            g.DrawString("灰度值", new Font("宋体", 9), Brushes.Black, new PointF(318, 243));
            g.DrawString("像元数", new Font("宋体", 9), Brushes.Black, new PointF(10, 20));            //绘制并表示X轴的刻度值
            g.DrawLine(pen, 100, 240, 100, 242);
            g.DrawLine(pen, 150, 240, 150, 242);
            g.DrawLine(pen, 200, 240, 200, 242);
            g.DrawLine(pen, 250, 240, 250, 242);
            g.DrawLine(pen, 300, 240, 300, 242);
            //绘制X轴上的刻度值
            g.DrawString("0", new Font("New Timer", 8), Brushes.Black, new PointF(46, 242));
            g.DrawString("51", new Font("New Timer", 8), Brushes.Black, new PointF(92, 242));
            g.DrawString("102", new Font("New Timer", 8), Brushes.Black, new PointF(139, 242));
            g.DrawString("153", new Font("New Timer", 8), Brushes.Black, new PointF(189, 242));
            g.DrawString("204", new Font("New Timer", 8), Brushes.Black, new PointF(239, 242));
            g.DrawString("255", new Font("New Timer", 8), Brushes.Black, new PointF(289, 242));

            //绘制并标识Y轴的坐标刻度
            g.DrawLine(pen, 48, 40, 50, 40);
            g.DrawString("0", new Font("New Timer", 8), Brushes.Black, new PointF(34, 234));

            //计算所有波段的最大像元数，绘制并表示每个波段最大灰度值，同时将该点
            //与每个波段统计图的峰值连线，并标记波段信息
            int MaxBandCount = GetMaxBandCount();
            for(int i = 0; i < m_selband.Length; i++)
            {
                int j = m_selband[i];
                //调用绘制直方图的函数，进行绘制
                DrawHisto(j, MaxBandCount, g);
            }
            //释放资源
            pen.Dispose();
        }
        //绘制直方图的函数，传入参数为波段索引、最大像元数、绘图对象
        private void DrawHisto(int index, int maxst, Graphics g)
        {
            //实现绘制直方图的函数
            //获取指定index的波段
            IRaster2 raster2 = m_rstlayer.Raster as IRaster2;
            IRasterDataset rasterDataset = raster2.RasterDataset;
            IRasterBandCollection rasterBandCollection = rasterDataset as IRasterBandCollection;
            IRasterBand rasterBand = rasterBandCollection.Item(index);
            //计算该波段的histogram（tips：类似于计算statistics）
            bool hasStat = false;
            rasterBand.HasStatistics(out hasStat);
            if (null == rasterBand.Statistics || !hasStat || rasterBand.Histogram == null)
            {
                rasterBand.ComputeStatsAndHist();
            }
            double[] histo;
            if (null != rasterBand.Statistics || rasterBand.Histogram != null)
            {
                //获取每个象元值的统计个数
                histo = rasterBand.Histogram.Counts as double[];
                //计算最大像元数
                int maxCount = (int)histo[0];
                for (int j = 0; j < histo.Length; j++)
                {
                    if (maxCount < histo[j])
                    {
                        maxCount = (int)histo[j];
                    }
                }

                //画笔颜色设置
                Pen pen = new Pen(Brushes.Black, 1); //实例化细度为1的黑色画笔
                Color color = new Color();
                switch (index)
                {
                    case 0:
                        color = Color.Red;
                        break;
                    case 1:
                        color = Color.Orange;
                        break;
                    case 2:
                        color = Color.Yellow;
                        break;
                    case 3:
                        color = Color.Green;
                        break;
                    case 4:
                        color = Color.Blue;
                        break;
                    case 5:
                        color = Color.Purple;
                        break;
                    case 6:
                        color = Color.Peru;
                        break;
                }
                pen.Color = color;
                //绘制并标识最大灰度值
                int maxLine = 40 + (1 - (int)(maxCount/ maxst))*200;//计算最大值所在的y坐标
                g.DrawLine(pen, 50, maxLine, 300, maxLine);
                g.DrawString(maxCount.ToString(), new Font("New Timer", 8), Brushes.Black, new PointF(9, 34));

                //绘制直方图
                double xTemp = 0;
                double yTemp = 0;

                for (int i = 0; i < histo.Length; i++)
                {
                    xTemp = i * 1.0 / histo.Length * (300 - 50);//计算横向位置,0~250
                    yTemp = histo[i] / maxst * 200.0;//计算纵向长度并绘制,0~200
                    g.DrawLine(pen, 50 + (int)xTemp, 240, 50 + (int)xTemp, 240 - (int)yTemp);
                }
                //释放资源
                pen.Dispose();
            }
        }

        //获取所有波段的最大像元数的函数
        private int GetMaxBandCount()
        {
            int[] max = new int[m_selband.Length];
            for (int i = 0; i < m_selband.Length; i++)
            { 
                //遍历地计算和获取每个波段的直方图信息
                IRaster2 raster2 =  m_rstlayer.Raster as IRaster2;
                IRasterDataset rstDataset = raster2.RasterDataset;
                IRasterBandCollection rstBandColl = rstDataset as IRasterBandCollection;
                IRasterBand rasterBand = rstBandColl.Item(i);

                //计算该波段的histogram（tips：类似于计算statistics）
                bool hasStat = false;
                rasterBand.HasStatistics(out hasStat);
                if (null == rasterBand.Statistics || !hasStat || rasterBand.Histogram == null)
                {
                    rasterBand.ComputeStatsAndHist();
                }
                //计算过直方图和统计，但是计算失败，仍然为null
                if (null == rasterBand.Statistics || rasterBand.Histogram == null)
                {
                    max[i] = 0;
                    continue;
                }
                
                //获取每个像元值的统计个数
                double[] histo = rasterBand.Histogram.Counts as double[];

                //计算最大像元数
                int maxCount = (int)histo[0];
                for (int j = 0; j < histo.Length; j++)
                {
                    if (maxCount < histo[j])
                    {
                        maxCount = (int)histo[j];
                    }
                }
                //记录下该波段的最大像元数
                max[i] = maxCount;
            }

            //计算所有波段的最大像元数
            int maxst = max[0];
            for (int k = 0; k < max.Length; k++)
            {
                if (maxst < max[k])
                {
                    maxst = (int)max[k];
                }
            }
            //返回所有波段的最大像元数
            return maxst;
        }
    }
}
