   M S B A M L   `   `   `  ���� 
  YF-WMS    YF_WMS.WPF_PO   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       � Title$  待入库PO-在途物料��$	��600��$	��900��.��� .+�&   System.Windows.Controls.DataGrid  -   $��
datagridpo��$
��Left=�$��10,111,-236,0q�$	��Top=�$	��449��$	��868��   AutoGenerateColumns .     CanUserAddRows .   %� IsVirtualizing .     Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ 采购订单号��   Width5  /System.Windows.Controls.DataGridLengthConverter$	 100    
IsReadOnly . 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� 	 �� Path$	 FBillNo�  $ 	供应商��$	 100  .  �� $	 FName�  $ 物料编号��$	 100  .  �� $	 FNumber�  $ 物料规格��$	 100  .  �� $	 FModel�  $ 物料名称��$	 100  .  �� $	 FName1�  $ 采购时间��$	 100  .  �� $	 FDate�  $ 采购数量��$	 150  .  �� $	 FAuxQty�
 � ContextMenu
 �� -   $��dgmenu1�� �� 	StaysOpen . .@�m� -   $��删除��m� $��撤回��m� -   $��查看库存��m� -   $��放入料箱���� -   $��导出��$
��Left=�$��	19,84,0,0q�$	��Top=�$��75���� -   $��批量放入料箱��$
��Left=�$��
466,84,0,0q�$	��Top=�$	��126���� -   $��	同步ERP��$
��Left=�$��
477,28,0,0q�$	��Top=�$��75���� -	   $��selectpo��$
��Left=�$��23��$��	19,27,0,0q� �� TextWrapping$
 Wrap=�$�� ��$	��Top=�$	��120���� -
   $��查找��$
��Left=�$��
144,28,0,0q�$	��Top=�$��75��