   M S B A M L   `   `   `  ���� 
  YF-WMS    YF_WMS.WPF_SO   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       � Title$  待出库SO��$	��600��$	��950��.��� .+�&   System.Windows.Controls.DataGrid  -     Name$ 
datagridso��$
��Left=�$��10,111,-536,-90q�$	��Top=�$	��449��$	��908��   AutoGenerateColumns .     CanUserAddRows .   %� IsVirtualizing .     Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ 销售订单号��   Width5  /System.Windows.Controls.DataGridLengthConverter$	 100    
IsReadOnly . 1  +System.Windows.Controls.DataGridBoundColumn	   Binding	 �� *	FBillNo+  $ 物料编号��$	 100  . 	 �� *	FNumber+  $ 物料规格��$	 100  . 	 �� *FModel+  $ 物料名称��$	 100  . 	 �� *FName+  $ 销售数量��$	 150  . 	 �� *	FAuxQty+  $ 客户名称��$	 100  . 	 �� *FName1+  $ 销售时间��$	 100  . 	 �� *FDate+
 � ContextMenu
 �� -    ��Name$ dgmenu1�� �� 	StaysOpen . .@�m� -   $��删除��m� $��撤回��m� -   $��查看库存��m� -   $��选择料箱出库���� -   $��导出��$
��Left=�$��	19,84,0,0q�$	��Top=�$��75���� -   $��	同步ERP��$
��Left=�$��
477,28,0,0q�$	��Top=�$��75���� -    ��Name$ selectso��$
��Left=�$��23��$��	19,27,0,0q� �� TextWrapping$
 Wrap=�$�� ��$	��Top=�$	��120���� -	   $��查找��$
��Left=�$��
144,28,0,0q�$	��Top=�$��75��