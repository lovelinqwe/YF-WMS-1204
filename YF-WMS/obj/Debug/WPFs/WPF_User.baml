   M S B A M L   `   `   `  ����
  YF-WMS    YF_WMS.WPF_User   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  用户管理��5   	   $	��500��6   $	��400��6#   .��5      � .+�5   
   �� -   5        $��添加��5      $
��Left=�6   $��	19,70,0,0q�69   $	��Top=�6L   $��75��6d   6o   �� 5   
   -   5        $��
selectuser��5      $
��Left=�6$   $��23��6?   $��
140,69,0,0q�6K    �� TextWrapping$
 Wrap=�6_   $�� ��6s   $	��Top=�6{   $	��120��6�   �� 5   
   -   5        $��查找��5      $
��Left=�6   $��
265,70,0,0q�69   $	��Top=�6M   $��75��6e   6p   �� 5	   
   -   5        $��导出��5	      $
��Left=�6   $��140,163,0,0q�69   $	��Top=�6N   $��75��6f   6q   &   System.Windows.Controls.DataGrid  5
   
   -   5        $��datagriduser��5
      $
��Left=�6'   $	��241��6B   $��
19,204,0,0q�6O   $	��Top=�6c   $	��336��6{      AutoGenerateColumns .     CanUserAddRows .   %� IsVirtualizing .     Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ 用户编号��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$	 100 63      
IsReadOnly . 1  +System.Windows.Controls.DataGridBoundColumn	   Binding	 6?   �� 
 �� Path$
 User_ID�6`     5      $ 	用户名��6%   $	 100 62    . 	 6>   �� $
 Username�6`    � ContextMenu 5      �� 5      -   5        $��dgmenu1��5       �� 	StaysOpen . .@�5      m� -   5        $��删除��5      6+   m� 5      $��撤回��6   m� 5      -   5        $��编辑��5      6+   m� 5      $��查看库存��6   5      5      �� 5   
   -	   5        $��导入��5      $
��Left=�6   $��
38,163,0,0q�69   $	��Top=�6M   $��75��6e   6p   5      5      