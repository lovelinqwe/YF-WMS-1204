   M S B A M L   `   `   `  ����
  YF-WMS    YF_WMS.WPF_SO   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  待出库SO��5   	   $	��600��6   $	��950��6$   .��5      � .+�5   
   &   System.Windows.Controls.DataGrid  -   5          Name$ 
datagridso��5      $
��Left=�6'   $��10,111,-536,-90q�6B   $	��Top=�6[   $	��449��6s   $	��908��6�      AutoGenerateColumns .     CanUserAddRows .   %� IsVirtualizing .     Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ 采购订单号��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$	 100 64      
IsReadOnly . 1  +System.Windows.Controls.DataGridBoundColumn	   Binding	 6@   �� *Purchase_ID+6`     5	      $ 物料编号��6%   $	 100 63    . 	 6?   �� *Material_ID+6_     5
      $ 物料规格��6%   $	 100 63    . 	 6?   �� *Material_Spec+6a     5      $ 入库id��6%   $	 100 63    . 	 6?   �� *
Inbound_ID+6^     5      $ 库存��6%   $	 100 61    . 	 6=   �� *stock+6W     5      $ 入库时间��6%   $	 100 63    . 	 6?   �� *Inbound_Time+
 �� StringFormat$
 {0:yyyy-MM-dd HH:mm:ss}��6�     5      $ 箱号��6%   $	 100 61    . 	 6=   �� *Box_ID+6X    � ContextMenu 5      �� 5      -   5         ��Name$ dgmenu1��5       �� 	StaysOpen . .@�5      m� -   5        $��删除��5      6+   m� 5      $��撤回��6   m� 5      -   5        $��查看库存��5      6-   m� 5      -   5        $��选择料箱出库��5      6/   5      5      �� 5   
   -   5        $��导出��5      $
��Left=�6   $��	19,84,0,0q�69   $	��Top=�6L   $��75��6d   6o   �� 5   
   -   5        $��	同步ERP��5      $
��Left=�6!   $��
477,28,0,0q�6<   $	��Top=�6P   $��75��6h   6s   �� 5    
   -   5         ��Name$ selectso��5       $
��Left=�6$   $��23��6?   $��	19,27,0,0q�6K    �� TextWrapping$
 Wrap=�6^   $�� ��6r   $	��Top=�6z   $	��120��6�   �� 5!   
   -	   5        $��查找��5!      $
��Left=�6   $��
144,28,0,0q�69   $	��Top=�6M   $��75��6e   6p   5"      5#      