   M S B A M L   `   `   `  ����A  =YF-WMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:YF_WMS.WPFsYF_WMS.WPFs  5   	       YF_WMS.WPFs.WPF_SetToBox   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:YF_WMS.WPFs     5   	     � Title$  放入料箱��5   	   $	��300��6   $	��250��6#   .��5	      �  � ColumnDefinitions 5
   
   �� 5      $	��29* �6   �� 5      $	��35* �6   .+�5   
   �� -   5        $��combobox��5      $
��Left=�6#   $��	66,41,0,0q�6>   $	��Top=�6Q   $	��120��6i   $��2��6u   6�   �� 5   
   -   5        $��确定��5      $
��Left=�6   $��
56,238,0,0q�69   $	��Top=�6M   $��75��6e   $��2��6�   �� 5   
   $��	原容量��6   $
��Left=�6   $��
10,159,0,0q�69   $	��Top=�6M   �� 5   
   $��	现容量��6   $
��Left=�6   $��
10,198,0,0q�69   $	��Top=�6M   �� 5   
   -   5        $��back_box_capacity��5      $
��Left=�6+   $��23��6F   $��
82,159,0,0q�6R    �� TextWrapping$
 Wrap=�6f   $�� ��6z   $	��Top=�6�   $	��113��6�   $��2��6�   �� 5   
   -   5        $��now_box_capacity��5      $
��Left=�6*   $��23��6E   $��
82,198,0,0q�6Q   $
 Wrap=�6e   $�� ��6y   $	��Top=�6�   $	��113��6�   $��2��6�   �� 5   
   $��采购数量��6   $��
10,84,42,0q�6   $	��Top=�63   �� 5   
   $��入箱数量��6   $
��Left=�6   $��
10,128,0,0q�6:   $	��Top=�6N   �� 5   
   -   5        $��material_pqty��5      $
��Left=�6'   $��23��6B   $��	82,87,0,0q�6N   $
 Wrap=�6a   $�� ��6u   $	��Top=�6}   $	��113��6�   $��2��6�   �� 5   
   -   5        $��material_qty��5      $
��Left=�6&   $��23��6A   $��
82,128,0,0q�6M   $
 Wrap=�6a   $�� ��6u   $	��Top=�6}   $	��113��6�   $��2��6�   5      5      