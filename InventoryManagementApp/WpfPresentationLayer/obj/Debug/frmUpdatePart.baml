   M S B A M L   `   `   `  ����O  KWpfPresentationLayer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:WpfPresentationLayerWpfPresentationLayer  5   	   (    "WpfPresentationLayer.frmUpdatePart   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   4local"clr-namespace:WpfPresentationLayer     5   	   -   5          � Title$  frmUpdatePart��5   	   $	��500��6   $
��1000��6,    � WindowStartupLocation$ CenterScreen=�6P   .��5	      � 
�����p� � RowDefinitions 5
   
   �� 5      6   �� 5      6   �� 5      6   �� 5      6   �� 5      6    � ColumnDefinitions 5   
   �� 5      6!   �� 5      6!   �� 5      6!   �� 5      6!   .+�5   
   �� -   5         ��Name$ lblUpdatePartWindow��5      $��0��6-   $��0��6:   $��3��6J   $��40(�5      $��Center=�6*   $��Center=�6E   $	��600��5      $��Center=�6(   $��Italic$�6L   .��6_   Update Part Information6x   �� 5   
   -   5        $ lblUpdatePartNumber��5      $��1��6-   $��0��6:   $��Center=�5      $��Center=�67   $��20(�5      $
��Bold"�6*   .��6<   Part Number6I   �� 5   
   -   5        $ lblUpdatePartName��5      $��1��6+   $��2��68   $��Center=�5      $��Center=�67   $��20(�5      $
��Bold"�6*   .��6<   	Part Name6G   �� 5    
   -   5        $ lblUpdatePartCost��5       $��2��6+   $��0��68   $��Center=�5!      $��Center=�67   $��20(�5"      $
��Bold"�6*   .��6<   Part Cost Per Unit6P   �� 5#   
   -   5        $ lblUpdatePartDescription��5#      $��3��62   $��0��6?   $��Center=�5$      $��Center=�67   $��20(�5%      $
��Bold"�6*   .��6<   Part Description6N   �� 5&   
   -   5         ��Name$ txtUpdatePartNumber��5&      $��1��6/   $��1��6<   $��20(�6L   $	��200��5'      $��30��6*   $��5q�66   $��Center=�6A   $��Center=�5(      $��Center=�6@   
���   �
�������5)   E   �� 5*   
   -   5        $ txtUpdatePartName��5*      $��1��6-   $��3��6:   $��20(�6J   $	��200��5+      $��30��6*   $��5q�66   $��Center=�6A   $��Center=�5,      $��Center=�6@   6_   �� 5-   
   -	   5        $ txtUpdatePartCost��5-      $��2��6-   $��1��6:   $��20(�6J   $	��200��5.      $��30��6*   $��5q�66   $��Center=�6A   $��Center=�5/      $��Center=�6@   ��50      ��  �� Path$	 Txt� �� StringFormat$ C��6J   �� 51   
   -
   5        $ txtUpdatePartDescription��51      $��3��64   $��2��6A   $��1��52      $��2��6.   $��16(�6B   $	��450��53      $	��175��6*   $��5q�67   $��Center=�6B    �� TextWrapping$
 Wrap=�54      	 �� AcceptsReturn	 . 
 �� 	IsEnabled
 . .��55   :   Enter Description Here6R   �� 56   
   -   5         ��Name$ btnSubmitUpdatedPart��56      $��3��6/   $��3��6<   $	��160��57      $��75��6)   $��5q�65   $��Center=�6@   $��Center=�58      $��Center=�6?   $��24(�59      
������.��6g   Save6m   �� 5:   
   -   5        $ btnCanceltUpdatedPart��5:      $��4��60   $��3��6=   $	��160��5;      $��75��6)   $��5q�65   $��Center=�6@   $��Center=�5<      $��Center=�6?   $��24(�5=      
�������.��6h   Cancel6p   5>      5?      