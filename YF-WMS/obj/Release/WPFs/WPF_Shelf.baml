   M S B A M L   `   `   `  ���� 
  YF-WMS    YF_WMS.WPF_Shelf   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       � Title$  仓位管理��$	��500��$	��700��.��� .+�&   System.Windows.Controls.DataGrid  -   $��datagridshelf��$
��Left=�$��
10,212,0,0q�$	��Top=�$	��248��$	��650��   AutoGenerateColumns .     CanUserAddRows .   %� IsVirtualizing .     Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ 库位编号��   Width5  /System.Windows.Controls.DataGridLengthConverter$	 100    
IsReadOnly . 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� 	 �� Path$	 Shelf_ID�  $ 库位所在区域��$	 100  .  �� $	 
Shelf_Area�  $$ 库位所在物理位置行数��$	 150  .   �� $	 	Shelf_Row�  $$ 库位所在物理位置列数��$	 150  .   �� $	 Shelf_Column�  $  库位状态(有无料箱)��$	 150  .   �� $	 Shelf_Status�
 � ContextMenu
 �� -   $��dgmenu1�� �� 	StaysOpen . .@�m� -   $��删除��m� $��撤回��m� -   $��编辑��m� $��查看库存���� $K��E只能由实施人员来修改此表，用户只能编辑部分属性��$
��Left=�$��	27,40,0,0q�$	��Top=�$	��356���� -   $��手动添加��$
��Left=�$��
57,125,0,0q�$	��Top=�$��75���� -   $��selectshelf��$
��Left=�$��23��$��242,124,0,0q� �� TextWrapping$
 Wrap=�$�� ��$	��Top=�$	��120���� -   $��查找��$
��Left=�$��367,125,0,0q�$	��Top=�$��75���� -   $��导出��$
��Left=�$��187,185,0,0q�$	��Top=�$��75���� -	   $��导入��$
��Left=�$��
57,185,0,0q�$	��Top=�$��75��