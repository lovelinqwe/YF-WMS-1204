   M S B A M L   `   `   `  ���� 
  YF-WMS    YF_WMS.WPF_User   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       � Title$  用户管理��$	��500��$	��400��.��� .+��� -   $��添加��$
��Left=�$��	19,70,0,0q�$	��Top=�$��75���� -   $��
selectuser��$
��Left=�$��23��$��
140,69,0,0q� �� TextWrapping$
 Wrap=�$�� ��$	��Top=�$	��120���� -   $��查找��$
��Left=�$��
265,70,0,0q�$	��Top=�$��75���� -   $��导出��$
��Left=�$��140,163,0,0q�$	��Top=�$��75��&   System.Windows.Controls.DataGrid  -   $��datagriduser��$
��Left=�$	��241��$��
19,204,0,0q�$	��Top=�$	��336��   AutoGenerateColumns .     CanUserAddRows .   %� IsVirtualizing .     Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ 用户编号��   Width5  /System.Windows.Controls.DataGridLengthConverter$	 100    
IsReadOnly . 1  +System.Windows.Controls.DataGridBoundColumn	   Binding	 �� 
 �� Path$
 User_ID�  $ 	用户名��$	 100  . 	 �� $
 Username� � ContextMenu �� -   $��dgmenu1�� �� 	StaysOpen . .@�m� -   $��删除��m� $��撤回��m� -   $��编辑��m� $��查看库存���� -	   $��导入��$
��Left=�$��
38,163,0,0q�$	��Top=�$��75��