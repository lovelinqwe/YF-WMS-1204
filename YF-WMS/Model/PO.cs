using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YF_WMS.Model
{
    class PO
    {
        public string FBillNo { set; get; }  //采购订单号
        public string FName { set; get; }     //供应商
        public string FNumber { set; get; }     //物料编码
        public string FModel { set; get; }  //物料规格
        public string FName1 { set; get; }  //物料名称
        public int FAuxQty { set; get; }  //物料数量
        public string FDate { set; get; }  //日期

    }
}
