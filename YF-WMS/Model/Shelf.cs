using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YF_WMS.Model
{
    class Shelf
    {
        public string Shelf_ID { set; get; }  //库位编号
        public int Shelf_Area { set; get; }     //库位所在区域
        public int Shelf_Row { set; get; }     //库位所在物理位置行数
        public int Shelf_Column { set; get; }  //库位所在物理位置列数
        public string Shelf_Status { set; get; }  //库位状态(有无料箱)

    }
}
