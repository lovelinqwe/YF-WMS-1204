using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YF_WMS.Model
{
    class Material
    {
        public string Material_ID { set; get; }  //物料编号
        public string Material_Spec { set; get; }  //物料规格
        public int Material_Qty { set; get; }     //物料数量
        public int Box_ID { set; get; }  //料箱编号

    }
}
