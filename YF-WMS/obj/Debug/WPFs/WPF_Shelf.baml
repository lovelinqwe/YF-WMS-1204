   M S B A M L   `   `   `  ����
  YF-WMS    YF_WMS.WPF_Shelf   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  仓位管理��5   	   $	��500��6   $	��700��6#   .��5      � .+�5   
   &   System.Windows.Controls.DataGrid  -   5        $��datagridshelf��5      $
��Left=�6(   $��
10,212,0,0q�6C   $	��Top=�6W   $	��248��6o   $	��650��6|      AutoGenerateColumns .     CanUserAddRows .   %� IsVirtualizing .     Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ 库位编号��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$	 100 63      
IsReadOnly . 1  +System.Windows.Controls.DataGridBoundColumn   Binding 6?   �� 	 �� Path$	 Shelf_ID�6a     5	      $ 库位所在区域��6%   $	 100 65    .  6A   �� $	 
Shelf_Area�6e     5
      $$ 库位所在物理位置行数��6%   $	 150 69    .   6E   �� $	 	Shelf_Row�6h     5      $$ 库位所在物理位置列数��6%   $	 150 69    .   6E   �� $	 Shelf_Column�6k     5      $  库位状态(有无料箱)��6%   $	 150 69    .   6E   �� $	 Shelf_Status�6k   
 � ContextMenu
 5      �� 5      -   5        $��dgmenu1��5       �� 	StaysOpen . .@�5      m� -   5        $��删除��5      6+   m� 5      $��撤回��6   m� 5      -   5        $��编辑��5      6+   m� 5      $��查看库存��6   5      5      �� 5   
   $K��E只能由实施人员来修改此表，用户只能编辑部分属性��6   $
��Left=�63   $��	27,40,0,0q�6N   $	��Top=�6a   $	��356��6y   �� 5   
   -   5        $��手动添加��5      $
��Left=�6    $��
57,125,0,0q�6;   $	��Top=�6O   $��75��6g   6r   �� 5   
   -   5        $��selectshelf��5      $
��Left=�6%   $��23��6@   $��242,124,0,0q�6L    �� TextWrapping$
 Wrap=�6a   $�� ��6u   $	��Top=�6}   $	��120��6�   �� 5   
   -   5        $��查找��5      $
��Left=�6   $��367,125,0,0q�69   $	��Top=�6N   $��75��6f   6q   �� 5   
   -   5        $��导出��5      $
��Left=�6   $��187,185,0,0q�69   $	��Top=�6N   $��75��6f   6q   �� 5   
   -	   5        $��导入��5      $
��Left=�6   $��
57,185,0,0q�69   $	��Top=�6M   $��75��6e   6p   5      5      