   M S B A M L   `   `   `  ����
  YF-WMS    YF_WMS.WPF_Box   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  WPF_Box��5   	   $	��500��6   $	��800��6&   .��5      � .+�5   
   �� -   5        $��添加��5      $
��Left=�6   $��	19,54,0,0q�69   $	��Top=�6L   $��75��6d   6o   �� 5   
   -   5        $��	selectbox��5      $
��Left=�6#   $��23��6>   $��
193,53,0,0q�6J    �� TextWrapping$
 Wrap=�6^   $�� ��6r   $	��Top=�6z   $	��120��6�   �� 5	   
   -   5        $��查找��5	      $
��Left=�6   $��
318,54,0,0q�69   $	��Top=�6M   $��75��6e   6p   �� 5
   
   -   5        $��导出��5
      $
��Left=�6   $��
21,134,0,0q�69   $	��Top=�6M   $��75��6e   6p   &   System.Windows.Controls.DataGrid  5   
   -   5        $��datagridbox��5      $
��Left=�6&   $	��275��6A   $��
10,185,0,0q�6N   $	��Top=�6b   $	��700��6z      AutoGenerateColumns .     CanUserAddRows .   %� IsVirtualizing .     Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ 料箱编号��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$	 100 63      
IsReadOnly . 1  +System.Windows.Controls.DataGridBoundColumn	   Binding	 6?   �� 
 �� Path$
 Box_ID�6_     5      $ 料箱颜色��6%   $	 100 63    . 	 6?   �� $
 	Box_Color�6b     5      $ 料箱容量��6%   $	 100 63    .  	 6?   �� $
 Box_Capacity�6e     5      $ 料箱状态��6%   $	 100 63    .  	 6?   �� $
 
Box_Status�6c     5      $ 料箱描述��6%   $	 100 63    .  	 6?   �� $
 Box_Desc�6a     5      $ 库位编号��6%   $	 100 63    . 	 6?   �� $
 Shelf_ID�6a    � ContextMenu 5      �� 5      -   5        $��dgmenu1��5       �� 	StaysOpen . .@�5      m� -   5        $��删除��5      6+   m� 5      $��撤回��6   m� 5      -   5        $��编辑��5      6+   m� 5      $��查看库存��6   5      5      �� 5   
   -	   5        $��导入��5      $
��Left=�6   $��146,134,0,0q�69   $	��Top=�6N   $��75��6f   6q   5      5      