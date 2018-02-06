using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YF_WMS.Model
{
    class Box
    {
        public int Box_ID { set; get; }     //料箱编号
        public string Box_Color { set; get; }  //料箱颜色
        public int Box_Capacity { set; get; }  //料箱容量
        public string Box_Status { set; get; }  //料箱状态
        public string Box_Desc { set; get; }  //料箱描述
        public string Shelf_ID { set; get; }  //库位编号
    }
}
