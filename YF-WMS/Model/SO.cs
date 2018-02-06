using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YF_WMS.Model
{
    class SO
    {
        public string FBillNo { set; get; }  //销售订单号
        public string FName { set; get; }     //物料名称
        public string FNumber { set; get; }     //物料编码
        public string FModel { set; get; }  //物料规格
        public string FName1 { set; get; }  //客户名称
        public string FAuxQty { set; get; }  //物料数量
        public string FDate { set; get; }  //日期
    }
}
