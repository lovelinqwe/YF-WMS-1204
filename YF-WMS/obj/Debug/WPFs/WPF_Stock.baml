   M S B A M L   `   `   `  ����
  YF-WMS    YF_WMS.WPF_Stock   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     N� Title$  库存查询��5   	   $	��920��6   $
��1120��6#   .��5      � $��
0,0,-333,0q�6   .+�5   
   &   System.Windows.Controls.DataGrid  -   5        $��datagridstock��5      $
��Left=�6(   $��
10,111,0,0q�6C   $	��Top=�6W   $	��800��6o   $
��1097��6|      AutoGenerateColumns .     CanUserAddRows .   %� IsVirtualizing .     Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ 入库编号��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$	 100 63      
IsReadOnly . 1  +System.Windows.Controls.DataGridBoundColumn   Binding 6?   �� 	 �� Path$	 
Inbound_ID�6c     5	      $ 入库时间��6%   $	 150 63    .  6?   �� $	 Inbound_Time�
 �� StringFormat$
 {0:yyyy-MM-dd HH:mm:ss}��6�     5
      $ 出库编号��6%   $	 100 63    .  6?   �� $	 Outbound_ID�6d     5      $ 出库时间��6%   $	 150 63    .  6?   �� $	 Outbound_Time�$
 {0:yyyy-MM-dd HH:mm:ss}��6�     5      $ 物料编号��6%   $	 100 63    .  6?   �� $	 Material_ID�6d     5      $ 物料规格��6%   $	 150 63    .  6?   �� $	 Material_Spec�6f     5      $ 	入库量��6%   $ 80 62    .  6=   �� $	 Material_Qty�6c     5      $ 	出库量��6%   $ 80 62    .  6=   �� $	 	delivered�6`     5      $ 库存��6%   $ 80 61    .  6<   �� $	 stock�6[     5      $ 箱号��6%   $	 100 61    .  6=   �� $	 Box_ID�6]   5      �� 5   
   -   5        $��查找��5      $
��Left=�6   $��
493,31,0,0q�69   $	��Top=�6M   $��75��6e   6p   �� 5   
   -   5        $��material_id��5      $
��Left=�6%   $��23��6@   $��
368,31,0,0q�6L    �� TextWrapping$
 Wrap=�6`   $�� ��6t   $	��Top=�6|   $	��120��6�   5      5      