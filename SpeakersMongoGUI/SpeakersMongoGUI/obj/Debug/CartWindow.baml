   M S B A M L   `   `   `  ����K  GSpeakersMongoGUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:SpeakersMongoGUISpeakersMongoGUI  5   	   !    SpeakersMongoGUI.CartWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:SpeakersMongoGUI     5   	   -   5          � Title$  Корзина��5   	   $	��400��6   $	��300��6&   .��5
      �  � RowDefinitions 5   
   �� 5      $��0.15* �6   �� 5      $��0.15* �6   �� 5      �� 5      $��0.15* �6   .+�5   
   �� $��0��6   $��Center=�6"   $��Center=�6=   .+�5      �� $&�� Выбранные товары:��6   $��30(�60   $��Franklin Gothic Medium)�6>   
��� � ���6u   Z� 5      �� 5   
   $��1��6    �� Orientation$ 
Horizontal=�6"   $��Center=�6;   $��Center=�6V   .+�5      �� -   5        $��	clear_btn��5      $��40��6<   $��40��6H   $��0,0,0,0q�6S   
������ 
������ .��5      �� $��/Pictures/clear.png��6   5      5      �� 5   
   $��2��6   $��Right=�6"   .+�5      �� $	��280��6   $	��248��6'   $��0,5,0,0q�64   .��5      �� -   5         ��Name$ products_sp��5      $ Vertical=�62   $��Stretch=�6I   $��Stretch=�6e   5      5       �� 5"   
   $��3��6   $��Center=�6"   $��Right=�6=   $ 
Horizontal=�6Y   .+�5#      �� $��Итого: ��6   $��20(�6&   $��Franklin Gothic Medium)�64   
��� � ���6k   Z� �� 5$      -   5         ��Name$ 	total_lbl��5$      $��0��6'   $��20(�63   $��Franklin Gothic Medium)�6A   
��� � ���6x   Z� �� 5%      $	��₽��6   $��20(�6    $��Franklin Gothic Medium)�6.   
��� � ���6e   Z� 5&      5'      5(      