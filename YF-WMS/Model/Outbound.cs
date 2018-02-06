using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YF_WMS.Model
{
    public class Outbound
    {
        public string Outbound_ID { get; set; }          //出库记录编号
        public string Outbound_Time { get; set; }  //出库时间
        public string Custom_Name { get; set; }   //客户名称
        public string Box_ID { get; set; }        //料箱编号
        public string Sale_ID { get; set; }        //销售订单编号
        public string Material_ID { get; set; }        //物料编号
        public string Material_Spec { get; set; }     //物料规格
        public int Material_SQty { get; set; }        //物料销售数量
        public string Material_SerialNum { get; set; }     //物料序列号
    }
}
