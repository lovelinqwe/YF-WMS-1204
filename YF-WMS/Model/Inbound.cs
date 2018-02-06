using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace YF_WMS.Model
{
   public class Inbound
    {
        public String Inbound_ID { get; set; }          //入库记录编号
        public string Inbound_Time { get; set; }  //入库时间
        public string Supplier_Name { get; set; }   //客户名称
        public string Box_ID { get; set; }        //料箱编号
        public string Purchase_ID { get; set; }        //采购订单编号
        public string Material_ID { get; set; }        //物料编号
        public string Material_Spec { get; set; }     //物料规格
        public int Material_PQty { get; set; }        //物料采购数量
        public string Material_SerialNum { get; set; }     //物料序列号

      
    }
}
