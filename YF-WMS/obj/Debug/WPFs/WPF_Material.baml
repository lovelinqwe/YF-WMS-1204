   M S B A M L   `   `   `  ����
  YF-WMS    YF_WMS.WPF_Material   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  物料管理��5   	   $	��500��6   $	��670��6#   .��5      � .+�5   
   &   System.Windows.Controls.DataGrid  -   5        $��datagridmaterial��5      $
��Left=�6+   $��10,111,-418,0q�6F   $	��Top=�6]   $	��349��6u   $	��660��6�      AutoGenerateColumns .     CanUserAddRows .   %� IsVirtualizing .     Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ 物料编号��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$	 150 63      
IsReadOnly . 1  +System.Windows.Controls.DataGridBoundColumn   Binding 6?   �� *Material_ID+6_     5	      $ 物料规格��6%   $	 150 63    .  6?   �� *Material_Spec+6a     5
      $ 物料数量��6%   $	 100 63    .  6?   �� *Material_Qty+6`     5      $ 料箱编号��6%   $	 100 63    .  6?   �� *Box_ID+6Z   	 � ContextMenu	 5      �� 5      -   5        $��dgmenu1��5      
 �� 	StaysOpen
 . .@�5      m� -   5        $��删除��5      6+   m� 5      $��撤回��6   m� 5      -   5        $��编辑��5      6+   m� 5      $��查看库存��6   5      5      �� 5   
   -   5        $��手动添加��5      $
��Left=�6    $��	10,10,0,0q�6;   $	��Top=�6N   $��75��6f    J� RenderTransformOrigin$ 0.181,-0.736&�6q   6�   �� 5   
   -   5        $��查找��5      $
��Left=�6   $��
430,83,0,0q�69   $	��Top=�6M   $��75��6e   6p   �� 5   
   -   5        $��selectmeterial��5      $
��Left=�6(   $��23��6C   $��
305,83,0,0q�6O    �� TextWrapping$
 Wrap=�6c   $�� ��6w   $	��Top=�6   $	��120��6�   �� 5   
   -   5        $��导出��5      $
��Left=�6   $��	10,84,0,0q�69   $	��Top=�6L   $��75��6d   6o   �� 5   
   -	   5        $��导入��5      $
��Left=�6   $��	90,10,0,0q�69   $	��Top=�6L   $��75��6d   6o   5      5      