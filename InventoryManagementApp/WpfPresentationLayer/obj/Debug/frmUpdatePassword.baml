   M S B A M L   `   `   `  ����O  KWpfPresentationLayer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:WpfPresentationLayerWpfPresentationLayer  5   	   ,    &WpfPresentationLayer.frmUpdatePassword   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   4local"clr-namespace:WpfPresentationLayer     5   	   -   5          � Title$  Update Password��5   	   $	��300��6!   $	��400��6.    � WindowStartupLocation$ CenterScreen=�5	      
�������.��5
      � 
��� ��� � RowDefinitions 5   
   �� 5      6   �� 5      6   �� 5      6   �� 5      6   �� 5      6    � ColumnDefinitions 5   
   �� 5      $	��250 �6   6-   �� 5      6!   .+�5   
   �� $��0��6   $��0��6   $��Center=�5      $��Center=�6-   $
��Bold"�5      .��6"   Please Update Your Password.6@   �� 5   
   $��1��6   $��0��6   $��Right=�5      $��Center=�5      $��10q�6+   $
��Bold"�5      
���   �.��65   Current Password:6H   �� 5   
   $��2��6   $��0��6   $��Right=�5      $��Center=�5       $��10q�6+   $
��Bold"�5!      
���   �.��65   New Password:6D   �� 5"   
   $��3��6   $��0��6   $��Right=�5#      $��Center=�5$      $��10q�6+   $
��Bold"�5%      
���   �.��65   Confirm Password:6H   �� 5&   
   -   5         ��Name$ 	btnSubmit��5&      $��4��6$   $��1��61   $	��135��6A   $
��Left=�5'      $��Center=�6,   $��5q�5(      $��5q�6   $
��Bold"�6(   $��Submit��6:   6e   G� 5)   
   -   5         G�Name$ pwdCurrentPassword��5)      $��1��62   $��1��6?   $��5q�5*      $��Center=�6!   $��Center=�5+      $	��135��6:   6H   G� 5,   
   -   5        $ pwdNewPassword��5,      $��2��6.   $��1��6;   $��5q�5-      $��Center=�6!   $��Center=�5.      $	��135��6:   5/   K   G� 50   
   -   5        $ pwdConfirmPassword��50      $��3��62   $��1��6?   $��5q�51      $��Center=�6!   $��Center=�52      $	��135��6:   53   O   54      55      