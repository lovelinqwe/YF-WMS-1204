   M S B A M L   `   `   `  ���� 
  YF-WMS    YF_WMS.WPF_Box   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       � Title$  WPF_Box��$	��500��$	��800��.��� .+��� -   $��添加��$
��Left=�$��	19,54,0,0q�$	��Top=�$��75���� -   $��	selectbox��$
��Left=�$��23��$��
193,53,0,0q� �� TextWrapping$
 Wrap=�$�� ��$	��Top=�$	��120���� -   $��查找��$
��Left=�$��
318,54,0,0q�$	��Top=�$��75���� -   $��导出��$
��Left=�$��
21,134,0,0q�$	��Top=�$��75��&   System.Windows.Controls.DataGrid  -   $��datagridbox��$
��Left=�$	��275��$��
10,185,0,0q�$	��Top=�$	��700��   AutoGenerateColumns .     CanUserAddRows .   %� IsVirtualizing .     Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ 料箱编号��   Width5  /System.Windows.Controls.DataGridLengthConverter$	 100    
IsReadOnly . 1  +System.Windows.Controls.DataGridBoundColumn	   Binding	 �� 
 �� Path$
 Box_ID�  $ 料箱颜色��$	 100  . 	 �� $
 	Box_Color�  $ 料箱容量��$	 100  .  	 �� $
 Box_Capacity�  $ 料箱状态��$	 100  .  	 �� $
 
Box_Status�  $ 料箱描述��$	 100  .  	 �� $
 Box_Desc�  $ 库位编号��$	 100  . 	 �� $
 Shelf_ID� � ContextMenu �� -   $��dgmenu1�� �� 	StaysOpen . .@�m� -   $��删除��m� $��撤回��m� -   $��编辑��m� $��查看库存���� -	   $��导入��$
��Left=�$��146,134,0,0q�$	��Top=�$��75��